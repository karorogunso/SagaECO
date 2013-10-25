using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;


using SagaDB.Actor;
using SagaDB.Item;
using SagaLib;
using SagaMap.Manager;

namespace SagaMap
{
    public class Map
    {
        private string name;
        private uint id;

        public uint ID { get { return this.id; } }
        public string Name { get { return this.name; } }

        private Dictionary<uint, Actor> actorsByID;
        private Dictionary<uint, List<Actor>> actorsByRegion;
        private Dictionary<string, ActorPC> pcByName;

        private const uint ID_BORDER = 0x80000000;
        private const uint ID_BORDER2 = 0x3B9ACA00;//border for possession items

        private uint nextPcId;
        private uint nextNpcId;
        private uint nextItemId;      

        public enum MOVE_TYPE { START, STOP };
        public enum EVENT_TYPE { APPEAR, DISAPPEAR, MOTION, EMOTION, CHAT, SKILL, CHANGE_EQUIP, CHANGE_STATUS, ACTOR_SELECTION, YAW_UPDATE };
        public enum TOALL_EVENT_TYPE { CHAT };

        public Map(MapInfo info)
        {
            this.id = info.id;
            this.name = info.name;
            
            this.actorsByID = new Dictionary<uint, Actor>();
            this.actorsByRegion = new Dictionary<uint, List<Actor>>();
            this.pcByName = new Dictionary<string, ActorPC>();
            this.nextPcId = 0x10;
            this.nextNpcId = ID_BORDER + 1;
            this.nextItemId = 0x100;
           
        }


        public short[] GetRandomPos()
        {
            short[] ret = new short[2];

            ret[0] = (short)Global.Random.Next(-12700, +12700);
            ret[1] = (short)Global.Random.Next(-12700, +12700);
         
            return ret;
        }

        public short[] GetRandomPosAroundActor(Actor actor)
        {
            short[] ret = new short[2];

            ret[0] = (short)Global.Random.Next(actor.X - 100, actor.X + 100);
            ret[1] = (short)Global.Random.Next(actor.Y - 100, actor.Y + 100);

            return ret;
        }

        public Actor GetActor(uint id)
        {
            try
            {
                return actorsByID[id];
            }
            catch(Exception)
            {
                return null;
            }
        }

        public ActorPC GetPC(string name)
        {
            try
            {
                return pcByName[name];
            }
            catch (Exception) 
            {
                return null;
            }
        }

        private uint GetNewActorID(ActorType type)
        {
            // get an unused actorID
            uint newID = 0;
            uint startID = 0;

            if (type == ActorType.PC) {
                newID = this.nextPcId;
                startID = this.nextPcId;
            }
            else {
                if (type == ActorType.NPC)
                {
                    newID = this.nextNpcId;
                    startID = this.nextNpcId;
                }
                else
                {                    
                    newID = this.nextItemId;
                    startID = this.nextItemId;                
                }
            }

            while (this.actorsByID.ContainsKey(newID))
            {
                newID++;

                if(newID >= ID_BORDER2 && type == ActorType.PC)
                    newID = 1;

                if(newID >= UInt32.MaxValue)
                    newID = ID_BORDER + 1;

                if (newID == startID) return 0;
            }

            if (type == ActorType.PC)
                this.nextPcId = newID + 1;
            else
                if(type==ActorType .NPC)
                    this.nextNpcId = newID + 1;
                else
                    this.nextItemId = newID + 1;
                

            return newID;
        }

        public bool RegisterActor(Actor nActor)
        {
            // default: no success
            bool succes = false;
            
            // set the actorID and the actor's region on this map
            uint newID = this.GetNewActorID(nActor.type);

            if (nActor.type == ActorType.ITEM)
            {
                ActorItem item = (ActorItem)nActor;
                if (item.PossessionItem)
                    newID += ID_BORDER2;
            }

            if (newID != 0)
            {
                nActor.ActorID = newID;
                nActor.region = this.GetRegion(nActor.X, nActor.Y);

                // make the actor invisible (when the actor is ready: set it to false & call OnActorVisibilityChange)
                nActor.invisble = true;

                // add the new actor to the tables
                this.actorsByID.Add(nActor.ActorID, nActor);

                if (nActor.type == ActorType.PC && !this.pcByName.ContainsKey(nActor.Name))
                    this.pcByName.Add(nActor.Name, (ActorPC)nActor);

                if (!this.actorsByRegion.ContainsKey(nActor.region))
                    this.actorsByRegion.Add(nActor.region, new List<Actor>());

                this.actorsByRegion[nActor.region].Add(nActor);

                succes = true;
            }

            nActor.e.OnCreate(succes);
            return succes;
        }

        public bool RegisterActor(Actor nActor,uint SessionID)
        {
            // default: no success
            bool succes = false;

            // set the actorID and the actor's region on this map
            uint newID = SessionID;

            if (newID != 0)
            {
                nActor.ActorID = newID;
                nActor.region = this.GetRegion(nActor.X, nActor.Y);
                if (GetRegionPlayerCount(nActor.region) == 0 && nActor.type == ActorType.PC)
                {
                    MobAIToggle(nActor.region, true);
                }
                
                // make the actor invisible (when the actor is ready: set it to false & call OnActorVisibilityChange)
                nActor.invisble = true;

                // add the new actor to the tables
                if (!this.actorsByID.ContainsKey(nActor.ActorID)) this.actorsByID.Add(nActor.ActorID, nActor);

                if (nActor.type == ActorType.PC && !this.pcByName.ContainsKey(nActor.Name))
                    this.pcByName.Add(nActor.Name, (ActorPC)nActor);

                if (!this.actorsByRegion.ContainsKey(nActor.region))
                    this.actorsByRegion.Add(nActor.region, new List<Actor>());

                this.actorsByRegion[nActor.region].Add(nActor);

                succes = true;
            }

            nActor.e.OnCreate(succes);
            return succes;
        }

        public void OnActorVisibilityChange(Actor dActor)
        {
            if (dActor.invisble)
                this.SendEventToAllActorsWhoCanSeeActor(EVENT_TYPE.DISAPPEAR, null, dActor, false);
 
            else
                this.SendEventToAllActorsWhoCanSeeActor(EVENT_TYPE.APPEAR, null, dActor, false);
        }

        public void DeleteActor(Actor dActor)
        {
            this.SendEventToAllActorsWhoCanSeeActor(EVENT_TYPE.DISAPPEAR, null, dActor, false);

            if (dActor.type == ActorType.PC && this.pcByName.ContainsKey(dActor.Name))
                this.pcByName.Remove(dActor.Name);

            this.actorsByID.Remove(dActor.ActorID);

            if (this.actorsByRegion.ContainsKey(dActor.region))
            {
                this.actorsByRegion[dActor.region].Remove(dActor);
                if (GetRegionPlayerCount(dActor.region) == 0 && dActor.type == ActorType.PC)
                {
                    MobAIToggle(dActor.region, false);
                }
            }
                
            dActor.e.OnDelete();
        }

        // make sure only 1 thread at a time is executing this method
        public void MoveActor(MOVE_TYPE mType, Actor mActor, short[] pos, ushort dir, ushort speed)
        {
            try
            {
                // check wheter the destination is in range, if not kick the client
                if (!this.MoveStepIsInRange(mActor, pos))
                {
                    Logger.ShowError("MoveStep is not in range", null);
                    mActor.e.OnKick();
                    return;
                }

                //scroll through all actors that "could" see the mActor at "from"
                //or are going "to see" mActor, or are still seeing mActor
                for (short deltaY = -1; deltaY <= 1; deltaY++)
                {
                    for (short deltaX = -1; deltaX <= 1; deltaX++)
                    {
                        uint region = (uint)(mActor.region + (deltaX * 1000000) + deltaY);
                        if (!this.actorsByRegion.ContainsKey(region)) continue;

                        foreach (Actor actor in this.actorsByRegion[region])
                        {
                            if (actor.ActorID == mActor.ActorID) continue;

                            // A) INFORM OTHER ACTORS

                            //actor "could" see mActor at its "from" position
                            if (this.ACanSeeB(actor, mActor))
                            {
                                //actor will still be able to see mActor
                                if (this.ACanSeeB(actor, mActor, pos[0], pos[1]))
                                {
                                    if (mType == MOVE_TYPE.START)
                                        actor.e.OnActorStartsMoving(mActor, pos, dir, speed);
                                    else
                                        actor.e.OnActorStopsMoving(mActor, pos, dir, speed);
                                }
                                //actor won't be able to see mActor anymore
                                else actor.e.OnActorDisappears(mActor);
                            }
                            //actor "could not" see mActor, but will be able to see him now
                            else if (this.ACanSeeB(actor, mActor, pos[0], pos[1]))
                            {
                                actor.e.OnActorAppears(mActor);
                               
                                //send move / move stop
                                if (mType == MOVE_TYPE.START)
                                    actor.e.OnActorStartsMoving(mActor, pos, dir, speed);
                                else
                                    actor.e.OnActorStopsMoving(mActor, pos, dir, speed);
                            }

                            // B) INFORM mActor
                            //mActor "could" see actor on its "from" position
                            if (this.ACanSeeB(mActor, actor))
                            {
                                //mActor won't be able to see actor anymore
                                if (!this.ACanSeeB(mActor, pos[0], pos[1], actor))
                                    mActor.e.OnActorDisappears(actor);
                                //mAactor will still be able to see actor
                                else { }
                            }

                            else if (this.ACanSeeB(mActor, pos[0], pos[1], actor))
                            {
                                //mActor "could not" see actor, but will be able to see him now
                                //send pcinfo
                                mActor.e.OnActorAppears(actor);                                
                            }
                        }
                    }
                }

                //update x/y/z/yaw of the actor
                mActor.X = pos[0];
                mActor.Y = pos[1];
                mActor.Dir = dir;


                //update the region of the actor
                uint newRegion = this.GetRegion(pos[0], pos[1]);
                if (mActor.region != newRegion)
                {
                    this.actorsByRegion[mActor.region].Remove(mActor);
                    //turn off all the ai if the old region has no player on it
                    if (GetRegionPlayerCount(mActor.region) == 0 && mActor.type == ActorType.PC)
                    {
                        MobAIToggle(mActor.region, false);
                    }
                    mActor.region = newRegion;
                    if (GetRegionPlayerCount(mActor.region) == 0 && mActor.type == ActorType.PC)
                    {
                        MobAIToggle(mActor.region, true);
                    }

                    if (!this.actorsByRegion.ContainsKey(newRegion))
                        this.actorsByRegion.Add(newRegion, new List<Actor>());

                    this.actorsByRegion[newRegion].Add(mActor);
                }
            }
            catch(Exception)
            { }
        }

        public int GetRegionPlayerCount(uint region)
        {
            List<Actor> actors;
            int count = 0;
            if (!this.actorsByRegion.ContainsKey(region)) return 0;
            actors = this.actorsByRegion[region];
            List<int> removelist = new List<int>();
            for (int i = 0; i < actors.Count; i++)
            {
                Actor actor;
                if (actors[i] == null)
                {
                    removelist.Add(i);
                    continue;
                }
                actor = actors[i];
                if (actor.type == ActorType.PC) count++;
            }
            foreach (int i in removelist)
            {
                actors.RemoveAt(i);
            }
            return count;
        }

        public void MobAIToggle(uint region, bool toggle)
        {
            /*List<Actor> actors;
            if (!this.actorsByRegion.ContainsKey(region)) return;
            actors = this.actorsByRegion[region];
            foreach (Actor actor in actors)
            {
                if (actor.type == ActorType.NPC)
                {
                    ActorNPC npc = (ActorNPC)actor;
                    if (npc.npcType >= 10000 && npc.npcType <50000)
                    {
                        Mob mob = (Mob)npc.e;
                        if (mob.ai == null) continue;
                        switch (toggle)
                        {
                            case true:
                                mob.ai.Start();
                                break;
                            case false:
                                Tasks.MobTasks.AICommands.Attack att = null;
                                if (mob.ai.commands.ContainsKey("Attack"))
                                    att = (Tasks.MobTasks.AICommands.Attack)mob.ai.commands["Attack"];
                                if (mob.ai.commands.ContainsKey("Chase")) continue;
                                if (att != null)
                                {
                                    if (att.active == true) continue;
                                }
                                mob.ai.Pause();
                                break;
                        }
                    }
                }
            }
            */
        }

        public bool MoveStepIsInRange(Actor mActor, short[] to)
        {
            /* Disabled, until we have something better
            if (System.Math.Abs(mActor.x - to[0]) > mActor.maxMoveRange) return false;
            if (System.Math.Abs(mActor.y - to[1]) > mActor.maxMoveRange) return false;
            //we don't check for z , yet, to allow falling from great hight
            //if (System.Math.Abs(mActor.z - to[2]) > mActor.maxMoveRange) return false;
            */
             return true;
        }


        public uint GetRegion(float x, float y)
        {

            uint REGION_DIAMETER = Global.MAX_SIGHT_RANGE;

            // best case we should now load the size of the map from a config file, however that's not
            // possible yet, so we just create a region code off the values x/y

            /*
            values off x/y are like:
            x = -20 500.0f
            y =   1 000.0f
            
            before we convert them to uints we make them positive, and store the info wheter they were negative
            x  = - 25 000.0f;
            nx = 1;
            y  =  1 000.0f;
            ny = 0;
            
            no we convert them to uints
             
            ux = 25 000;
            nx = 1;
            uy =  1 000;
            ny = 0;
            
            now we do ux = (uint) ( ux / REGION_DIAMETER ) [the same for uy]
            we have:
             
            ux = 2;
            nx = 1;
            uy = 0;
            ny = 0;
             
            off this data we generate the region code:
             > we use a uint as region code
             > max value of an uint32 is 4 294 967 295
             > the syntax of the region code is ux[5digits].uy[5digits]
             if(!nx) ux = ux + 50000;
             else ux = 50000 - ux;
             if(!ny) uy = uy + 50000;
             else uy = 50000 - uy;
  
            uint regionCode = 49998 50001
            uint regionCode = 4999850001

            Note: 
             We inform an Actor(Player) about all other Actors in its own region and the 8 regions around
             this region. Because of this REGION_DIAMETER has to be MAX_SIGHT_RANGE (or greater).
             Also check SVN/SagaMap/doc/mapRegions.bmp
            */
            // init nx,ny
            bool nx = false;
            bool ny = false;
            // make x,y positive
            if (x < 0) { x = x - (2 * x); nx = true; }
            if (y < 0) { y = y - (2 * y); ny = true; }
            // convert x,y to uints
            uint ux = (uint)x;
            uint uy = (uint)y;
            // divide through REGION_DIAMETER
            ux = (uint)(ux / REGION_DIAMETER);
            uy = (uint)(uy / REGION_DIAMETER);
            // calc ux
            if (ux > 49999) ux = 49999;
            if (!nx) ux = ux + 50000;
            else ux = 50000 - ux;
            // calc uy
            if (uy > 49999) uy = 49999;
            if (!ny) uy = uy + 50000;
            else uy = 50000 - uy;
            // finally generate the region code and return it
            return (uint)((ux * 1000000) + uy);
        }

        public bool ACanSeeB(Actor A, Actor B)
        {
            if (B.invisble) return false;
            if (System.Math.Abs(A.X - B.X) > A.sightRange) return false;
            if (System.Math.Abs(A.Y - B.Y) > A.sightRange) return false;
            return true;
        }

        public bool ACanSeeB(Actor A, Actor B, float bx, float by)
        {
            if (B.invisble) return false;
            if (System.Math.Abs(A.X - bx) > A.sightRange) return false;
            if (System.Math.Abs(A.Y - by) > A.sightRange) return false;
            return true;
        }

        public bool ACanSeeB(Actor A, float ax, float ay, Actor B)
        {
            if (B.invisble) return false;
            if (System.Math.Abs(ax - B.X) > A.sightRange) return false;
            if (System.Math.Abs(ay - B.Y) > A.sightRange) return false;
            return true;
        }

        public bool ACanSeeB(Actor A, Actor B, float sightrange)
        {
            if (B.invisble) return false;
            if (System.Math.Abs(A.X - B.X) > sightrange) return false;
            if (System.Math.Abs(A.Y - B.Y) > sightrange) return false;
            return true;
        }

        public void SendVisibleActorsToActor(Actor jActor)
        {
            //search all actors which can be seen by jActor and tell jActor about them
            for (short deltaY = -1; deltaY <= 1; deltaY++)
            {
                for (short deltaX = -1; deltaX <= 1; deltaX++)
                {
                    uint region = (uint)(jActor.region + (deltaX * 1000000) + deltaY);
                    if (!this.actorsByRegion.ContainsKey(region)) continue;

                    foreach (Actor actor in this.actorsByRegion[region])
                    {
                        if (actor.ActorID == jActor.ActorID) continue;

                        //check wheter jActor can see actor, if yes: inform jActor
                        if (this.ACanSeeB(jActor, actor))
                        {
                            jActor.e.OnActorAppears(actor);                            
                        }
                            
                    }
                }
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void TeleportActor(Actor sActor, short x, short y)
        {
            this.SendEventToAllActorsWhoCanSeeActor(EVENT_TYPE.DISAPPEAR, null, sActor, false);

            this.actorsByRegion[sActor.region].Remove(sActor);
            if (GetRegionPlayerCount(sActor.region) == 0 && sActor.type == ActorType.PC)
            {
                MobAIToggle(sActor.region, false);
            }
                
            sActor.X = x;
            sActor.Y = y;
            sActor.region = this.GetRegion(x, y);
            if (GetRegionPlayerCount(sActor.region) == 0 && sActor.type == ActorType.PC)
            {
                MobAIToggle(sActor.region, true);
            }
                
            if (!this.actorsByRegion.ContainsKey(sActor.region)) this.actorsByRegion.Add(sActor.region, new List<Actor>());
            this.actorsByRegion[sActor.region].Add(sActor);

            sActor.e.OnTeleport(x, y);

            this.SendEventToAllActorsWhoCanSeeActor(EVENT_TYPE.APPEAR, null, sActor, false);
            this.SendVisibleActorsToActor(sActor);
        }

        public void SendEventToAllActorsWhoCanSeeActor(EVENT_TYPE etype, MapEventArgs args, Actor sActor, bool sendToSourceActor)
        {
            try
            {
                for (short deltaY = -1; deltaY <= 1; deltaY++)
                {
                    for (short deltaX = -1; deltaX <= 1; deltaX++)
                    {
                        uint region = (uint)(sActor.region + (deltaX * 1000000) + deltaY);
                        if (!this.actorsByRegion.ContainsKey(region)) continue;

                        foreach (Actor actor in this.actorsByRegion[region])
                        {
                            try
                            {
                                if (!sendToSourceActor && (actor.ActorID == sActor.ActorID)) continue;

                                if (this.ACanSeeB(actor, sActor))
                                {
                                    switch (etype)
                                    {
                                        case EVENT_TYPE.APPEAR:
                                            actor.e.OnActorAppears(sActor);
                                            break;

                                        case EVENT_TYPE.DISAPPEAR:
                                            actor.e.OnActorDisappears(sActor);
                                            break;

                                        case EVENT_TYPE.EMOTION:
                                            actor.e.OnActorChangeEmotion(sActor, args);
                                            break;

                                        case EVENT_TYPE.MOTION:
                                            actor.e.OnActorChangeMotion(sActor, args);
                                            break;

                                        case EVENT_TYPE.CHAT:
                                            actor.e.OnActorChat(sActor, args);
                                            break;

                                        case EVENT_TYPE.SKILL:
                                            actor.e.OnActorSkillUse(sActor, args);
                                            break;

                                        case EVENT_TYPE.CHANGE_EQUIP:
                                            actor.e.OnActorChangeEquip(sActor, args);
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Logger.ShowError(ex);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
        }

        public void SendEventToAllActors(TOALL_EVENT_TYPE etype, MapEventArgs args, Actor sActor, bool sendToSourceActor)
        {
            foreach (Actor actor in this.actorsByID.Values)
            {
                if(sActor != null) if (!sendToSourceActor && (actor.ActorID == sActor.ActorID)) continue;

                switch (etype)
                {
                    case TOALL_EVENT_TYPE.CHAT:
                        actor.e.OnActorChat(sActor, args);
                        break;                    
                    default:
                        break;
                }
            }
        }

        public void SendActorToMap(Actor mActor, Map newMap, short x, short y)
        {
            // todo: add support for multiple map servers

            // obtain the new map
            byte mapid = (byte)newMap.id;
            if (mapid == mActor.MapID)
            {
                TeleportActor(mActor, x, y);
                return;
            }
            
            // delete the actor from this map
            this.DeleteActor(mActor);

            // update the actor
            mActor.MapID = mapid;
            mActor.X = x;
            mActor.Y = y;
            
            // register the actor in the new map
            if (mActor.type != ActorType.PC)
            {
                newMap.RegisterActor(mActor);
            }
            else
            {
                newMap.RegisterActor(mActor, mActor.ActorID);
            }
        }

        public void SendActorToMap(Actor mActor, uint mapid, short x, short y)
        {
            // todo: add support for multiple map servers

            // obtain the new map
            Map newMap;
            if (mapid == mActor.MapID)
            {
                TeleportActor(mActor, x, y);
                return;
            }
            newMap = MapManager.Instance.GetMap(mapid);
            if (newMap == null)
                return;
            // delete the actor from this map
            this.DeleteActor(mActor);
            if (x == 0f && y == 0f)
            {
                short[] pos = newMap.GetRandomPos();
                x = pos[0];
                y = pos[1];                
            }
            // update the actor
            mActor.MapID = mapid;
            mActor.X = x;
            mActor.Y = y;
            
            // register the actor in the new map
            if (mActor.type != ActorType.PC)
            {
                newMap.RegisterActor(mActor);
            }
            else
            {
                newMap.RegisterActor(mActor,mActor.ActorID);
            }
        }

        private void SendActorToActor(Actor mActor, Actor tActor)
        {
            if (mActor.MapID == tActor.MapID)
                this.TeleportActor(mActor, tActor.X, tActor.Y);
            else
                this.SendActorToMap(mActor, tActor.MapID, tActor.X, tActor.Y);
        }

        public List<Actor> GetActorsArea(Actor sActor, float range, bool includeSourceActor)
        {
            List<Actor> actors = new List<Actor>();
            for (short deltaY = -1; deltaY <= 1; deltaY++)
            {
                for (short deltaX = -1; deltaX <= 1; deltaX++)
                {
                    uint region = (uint)(this.GetRegion(sActor.X, sActor.Y) + (deltaX * 1000000) + deltaY);
                    if (!this.actorsByRegion.ContainsKey(region)) continue;

                    foreach (Actor actor in this.actorsByRegion[region])
                    {
                        if (!includeSourceActor && (actor.ActorID == sActor.ActorID)) continue;

                        if (this.ACanSeeB(actor, sActor, range))
                        {
                            actors.Add(actor);
                        }
                    }
                }
            }
            return actors;
        }       
    }

    public class ChatArg : MapEventArgs
    {
        public string content;
        public MotionType motion;
        public byte loop;
        public uint emotion;
    }

    public class MoveArg : MapEventArgs
    {
        public ushort x, y, dir;
        public MoveType type;
    }
}

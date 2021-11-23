using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaMap;
using SagaMap.Scripting;

namespace SagaMap.Mob.AICommands
{    
    public class Chase : AICommand
    {
        private CommandStatus status;
        private Actor dest;
        private MobAI mob;
        
        List<MapNode> path;
        int index = 0;

        public short x = 0, y = 0;
        
        public Chase(MobAI mob, Actor dest)
        {
            try
            { 
            this.mob = mob;
            this.dest = dest;
            x = dest.X;
            y = dest.Y;
            if (mob.Mob.MapID != dest.MapID || !mob.CanMove)
            {
                this.Status = CommandStatus.FINISHED;
                return;
            }
                if (mob.Mode.RunAway)
                {
                    if (MobAI.GetLengthD(mob.Mob.X, mob.Mob.Y, dest.X, dest.Y) > 2000)
                    {
                        this.status = CommandStatus.FINISHED;
                        return;
                    }
                    if (MobAI.GetLengthD(mob.Mob.X, mob.Mob.Y, dest.X, dest.Y) < 3500)
                    {
                        if (Global.Random.Next(0, 99) <= 99)
                        {
                            int range = Global.Random.Next(800, 3500);
                            short sss = 0;
                            int count = 0;
                            do
                            {
                                count++;
                                x = (short)(mob.Mob.X - dest.X);
                                y = (short)(mob.Mob.Y - dest.Y);
                                x = (short)(mob.Mob.X + x / MobAI.GetLengthD(0, 0, x, y) * range);
                                y = (short)(mob.Mob.Y + y / MobAI.GetLengthD(0, 0, x, y) * range);
                                sss = (short)MobAI.GetLengthD(x, y, dest.X, dest.Y);
                            }
                            while (sss > 3000 && count < 30);
                        }
                        else
                        {
                            this.status = CommandStatus.FINISHED;
                            return;
                        }
                    }
                    else
                    {
                        this.Status = CommandStatus.FINISHED;
                        return;
                    }
                }
           
            path = mob.FindPath(Global.PosX16to8(mob.Mob.X, mob.map.Width), Global.PosY16to8(mob.Mob.Y, mob.map.Height), Global.PosX16to8(x, mob.map.Width), Global.PosY16to8(y, mob.map.Height));
            this.Status = CommandStatus.INIT;
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }


        }
        void returnAndInitialize()
        {
            if (mob.Mob.type == ActorType.GOLEM) return;
            short[] pos = new short[2] { mob.X_pb, mob.Y_pb };
            mob.map.MoveActor(Map.MOVE_TYPE.START, mob.Mob, pos, 1, 1000, false, MoveType.WARP2);
            mob.Mob.HP = mob.Mob.MaxHP;
            if (mob.Mob.type == ActorType.MOB)
            {
                if (((ActorMob)mob.Mob).AttackedForEvent != 0)
                    mob.Mob.e.OnActorReturning(mob.Mob);
                ((ActorMob)mob.Mob).AttackedForEvent = 0;
                mob.firstAttacker = null;
                mob.Mob.BattleStartTime = DateTime.Now;
            }
            mob.Hate.Clear();


            if (mob.Mob.Slave.Count > 0)
            {
                Actor[] s = new Actor[mob.Mob.Slave.Count + 10];
                mob.Mob.Slave.CopyTo(s);
                for (int i = 0; i < mob.Mob.Slave.Count; i++)
                {
                    if (s[i] != null)
                    {
                        if (mob.Mob.HP > 0)
                            Skill.SkillHandler.Instance.ShowEffectByActor(s[i], 4310);
                        ActorEventHandlers.MobEventHandler eh = (ActorEventHandlers.MobEventHandler)s[i].e;
                        s[i].Buff.死んだふり = true;
                        eh.OnDie(false);
                        mob.map.DeleteActor(s[i]);
                    }
                }
            }
            //清空HP相关SKILL状态
            mob.SkillOfHPClear();
            mob.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, mob.Mob, false);
        }
        public string GetName() { return "Chase"; }
        public void Update(object para)
        {
            try
            {
                MapNode node;
                if (mob.Mode.NoMove || !mob.CanMove)
                    return;
                if (this.dest.Status == null)
                {
                    this.status = CommandStatus.FINISHED;
                    return;
                }
                //if (mob.Mode.isAnAI && mob.CannotMove)
                //    return;
                if (this.dest.Status.Additions.ContainsKey("Hiding"))
                {
                    this.Status = CommandStatus.FINISHED;
                }
                if (this.dest.Status.Additions.ContainsKey("Through"))
                {
                    this.Status = CommandStatus.FINISHED;
                }
                if (this.Status == CommandStatus.FINISHED)
                    return;
                if (!mob.Mode.Active && mob.Master == null)
                {
                    if (DateTime.Now > mob.attackStamp.AddSeconds(15))
                    {
                        returnAndInitialize();
                        Status = CommandStatus.FINISHED;
                        return;
                    }
                }
                if (mob.Master != null)
                {
                    if (mob.Master.type == ActorType.MOB)
                    {
                        ActorEventHandlers.MobEventHandler e = (ActorEventHandlers.MobEventHandler)mob.Master.e;
                        if (e.AI.Hate.Count == 0 && mob.Master.SettledSlave.Count == 0)
                        {
                            returnAndInitialize();
                            mob.Master = null;
                            Status = CommandStatus.FINISHED;
                            return;
                        }
                    }
                }
                float size = 0;
                if (mob.Mode.isAnAI)
                {
                    size = mob.needlen;
                }
                else if (mob.Mob.type != ActorType.PC)
                {
                    if (((ActorMob)mob.Mob).BaseData != null)
                        size = ((ActorMob)mob.Mob).BaseData.range;
                    if (((ActorMob)mob.Mob).range != 0)
                        size = ((ActorMob)mob.Mob).range;
                    if (mob.Mob.type == ActorType.GOLEM)
                        size = mob.Mob.Range;
                }
                else
                    size = 1;

                bool ifNeko = false;
                if (mob.Mob.type == ActorType.PET)
                {
                    if (((ActorPet)mob.Mob).BaseData.mobType == SagaDB.Mob.MobType.MAGIC_CREATURE)
                    {
                        ifNeko = true;
                    }
                }
            
                if (MobAI.GetLengthD(mob.Mob.X, mob.Mob.Y, dest.X, dest.Y) <= (size * 150) && !ifNeko)
                {
                    if (!this.mob.Mode.RunAway)
                    {
                        if (Global.Random.Next(0, 99) < 70 || !this.mob.Mode.RunAway)
                        {
                            this.mob.map.FindFreeCoord(this.mob.Mob.X, this.mob.Mob.Y, out x, out y, this.mob.Mob);
                            if (this.mob.Mob.X == x && this.mob.Mob.Y == y || this.mob.Mode.RunAway)
                            {
                                this.Status = CommandStatus.FINISHED;
                                return;
                            }
                            else
                            {
                                short[] dst = new short[2] { x, y };
                                mob.map.MoveActor(Map.MOVE_TYPE.START, mob.Mob, dst, MobAI.GetDir((short)(dst[0] - x), (short)(dst[1] - y)), (ushort)(mob.Mob.Speed / 20), true);
                                return;
                            }
                        }
                    }
                }
                //if (mob.Mode.RunAway) return;
                if (index + 1 < path.Count)
                {
                    node = path[index];
                    short[] dst = new short[2] { Global.PosX8to16(node.x, mob.map.Width), Global.PosY8to16(node.y, mob.map.Height) };
                    mob.map.MoveActor(Map.MOVE_TYPE.START, mob.Mob, dst, MobAI.GetDir((short)(dst[0] - x), (short)(dst[1] - y)), (ushort)(mob.Mob.Speed / 20), true);
                    if (mob.Mode.isAnAI)
                        mob.CannotAttack = DateTime.Now.AddMilliseconds(1500);
                }
                else if(path.Count == 1 && index == 0)
                {
                    byte xs = Global.PosX16to8(mob.Mob.X, mob.map.Width);
                    byte ys = Global.PosY16to8(mob.Mob.Y, mob.map.Height);
                    node = path[index];
                    short[] dst = new short[2] { Global.PosX8to16(node.x, mob.map.Width), Global.PosY8to16(node.y, mob.map.Height) };
                    mob.map.MoveActor(Map.MOVE_TYPE.START, mob.Mob, dst, MobAI.GetDir((short)(dst[0] - x), (short)(dst[1] - y)), (ushort)(mob.Mob.Speed / 20), true);
                }
                else
                {
                    if (path.Count == 0)
                    {
                        this.Status = CommandStatus.FINISHED;
                        return;
                    }
                    node = path[path.Count - 1];
                    short[] dst = new short[2] { Global.PosX8to16(node.x, mob.map.Width), Global.PosY8to16(node.y, mob.map.Height) };
                    if (mob.map.GetActorsArea(dst[0], dst[1], 50).Count > 0 && !ifNeko)
                    {
                        this.mob.map.FindFreeCoord(dest.X, dest.Y, out x, out y, this.mob.Mob);
                        path = mob.FindPath(Global.PosX16to8(mob.Mob.X, mob.map.Width), Global.PosY16to8(mob.Mob.Y, mob.map.Height), Global.PosX16to8(x, mob.map.Width), Global.PosY16to8(y, mob.map.Height));
                        index = 0;
                        return;
                    }
                    mob.map.MoveActor(Map.MOVE_TYPE.START, mob.Mob, dst, MobAI.GetDir((short)(dst[0] - x), (short)(dst[1] - y)), (ushort)(mob.Mob.Speed / 20), true);
                    if ((MobAI.GetLengthD(mob.Mob.X, mob.Mob.Y, dest.X, dest.Y) > (50 + size * 100) && !mob.Mode.RunAway))
                    {
                        if (mob.Mob.MapID != dest.MapID)
                        {
                            this.Status = CommandStatus.FINISHED;
                            return;
                        }
                        path = mob.FindPath(Global.PosX16to8(mob.Mob.X, mob.map.Width), Global.PosY16to8(mob.Mob.Y, mob.map.Height), Global.PosX16to8(dest.X, mob.map.Width), Global.PosY16to8(dest.Y, mob.map.Height));
                        index = -1;
                    }
                    else
                    {
                        this.Status = CommandStatus.FINISHED;
                        return;
                    }
                }
                index++;
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex, null);
                this.Status = CommandStatus.FINISHED;
            }

        }

        public void FindPath()
        {
            path = mob.FindPath(Global.PosX16to8(mob.Mob.X, mob.map.Width), Global.PosY16to8(mob.Mob.Y, mob.map.Height), Global.PosX16to8(x, mob.map.Width), Global.PosY16to8(y, mob.map.Height));
            index = 0;
        }

        public CommandStatus Status
        {
            get { return status; }
            set { status = value; }
        }

        public List<MapNode> GetPath() { return path; }

        public void Dispose()
        {
            this.status = CommandStatus.FINISHED;
        }

    }
}

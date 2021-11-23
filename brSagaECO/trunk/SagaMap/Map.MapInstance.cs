using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;


using SagaDB.Actor;
using SagaDB.Item;
using SagaDB.Map;
using SagaLib;
using SagaMap.Manager;

namespace SagaMap
{
    public partial class Map
    {
        bool instance;
        bool dungeon;
        bool autoDispose;
        uint clientExitMap;
        byte clientExitX, clientExitY;
        ActorPC creator;
        Dungeon.DungeonMap dungeonMap;
        SagaDB.Ring.Ring ring;
        uint resurrectionLimit;
        public bool IsMapInstance { get { return this.instance; } set { this.instance = value; } }
        public bool IsDungeon { get { return this.dungeon; } set { this.dungeon = value; } }
        public Dungeon.DungeonMap DungeonMap { get { return this.dungeonMap; } set { this.dungeonMap = value; } }
        public uint ClientExitMap { get { return this.clientExitMap; } set { this.clientExitMap = value; } }
        public byte ClientExitX { get { return this.clientExitX; } set { this.clientExitX = value; } }
        public byte ClientExitY { get { return this.clientExitY; } set { this.clientExitY = value; } }
        public bool AutoDispose { get { return autoDispose; } set { this.autoDispose = value; } }
        public ActorPC Creator { get { return this.creator; } set { this.creator = value; } }
        public SagaDB.Ring.Ring Ring { get { return this.ring; } set { this.ring = value; } }
        public uint ResurrectionLimit { get { return this.resurrectionLimit; } set { this.resurrectionLimit = value; } }
        public void OnDestrory()
        {
            List<Actor> pcs = new List<Actor>();
            List<Actor> items = new List<Actor>();
            List<Actor> other = new List<Actor>();
            if (Mob.MobSpawnManager.Instance.Spawns.ContainsKey(this.id))
            {
                foreach (ActorMob mob in Mob.MobSpawnManager.Instance.Spawns[this.id])
                {
                    ActorEventHandlers.MobEventHandler eh = (ActorEventHandlers.MobEventHandler)mob.e;
                    Mob.AIThread.Instance.RemoveAI(eh.AI);
                    //Mob.NewAIThread.Instance.RemoveAI(eh.NewAI);
                    foreach (MultiRunTask i in mob.Tasks.Values)
                    {
                        i.Deactivate();
                    }
                    mob.Tasks.Clear();
                }
                Mob.MobSpawnManager.Instance.Spawns[this.id].Clear();
                Mob.MobSpawnManager.Instance.Spawns.Remove(this.id);
            }
            foreach (Actor i in this.actorsByID.Values)
            {
                if (i.type == ActorType.PC)
                    pcs.Add(i);
                else if (i.type == ActorType.ITEM)
                {
                    items.Add(i);
                }
                else if (i.type == ActorType.GOLEM)
                {
                    try
                    {
                        ActorGolem golem = (ActorGolem)i;
                        MapServer.charDB.SaveChar(golem.Owner, false);
                    }
                    catch (Exception ex)
                    {
                        Logger.ShowError(ex);
                    }
                }
                else
                    other.Add(i);
            }
            foreach (Actor i in other)
            {
                foreach (var item in i.Tasks.Values)
                {
                    item.Deactivate();
                }
                i.Tasks.Clear();
                i.ClearTaskAddition();
                this.DeleteActor(i);
            }

            Map map = MapManager.Instance.GetMap(clientExitMap);
            foreach (Actor i in items)
            {
                ActorItem item = (ActorItem)i;
                if (item.Item.PossessionedActor != null)
                {
                    Packets.Client.CSMG_POSSESSION_CANCEL p = new SagaMap.Packets.Client.CSMG_POSSESSION_CANCEL();
                    p.PossessionPosition = PossessionPosition.NONE;
                    ActorEventHandlers.PCEventHandler eh = (SagaMap.ActorEventHandlers.PCEventHandler)item.Item.PossessionedActor.e;
                    ActorPC posActor = item.Item.PossessionedActor;
                    if (posActor.Online)
                    {
                        eh.Client.OnPossessionCancel(p);
                    }
                    else
                    {
                        posActor.PossessionTarget = 0;
                    }
                    eh.Client.map.SendActorToMap(posActor, clientExitMap, Global.PosX8to16(clientExitX, map.Width), Global.PosY8to16(clientExitY, map.Height));
                    if (!posActor.Online)
                    {
                        MapServer.charDB.SaveChar(posActor, false, false);
                        MapServer.accountDB.WriteUser(posActor.Account);
                        MapManager.Instance.GetMap(posActor.MapID).DeleteActor(posActor);
                        Network.Client.MapClient.FromActorPC(posActor).DisposeActor();
                        posActor.Account = null;
                    }
                    if (pcs.Contains(posActor))
                        pcs.Remove(posActor);
                }
                i.Speed = Configuration.Instance.Speed;
                i.ClearTaskAddition();
                this.DeleteActor(i);
            }
            foreach (Actor i in pcs)
            {
                i.Speed = Configuration.Instance.Speed;
                try
                {
                    this.SendActorToMap(i, clientExitMap, Global.PosX8to16(clientExitX, map.Width), Global.PosY8to16(clientExitY, map.Height));
                }
                catch
                {
                    throw;
                }
            }
        }
    }
}

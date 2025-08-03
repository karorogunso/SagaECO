using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;

namespace SagaMap.Dungeon
{
    public enum DestroyType
    {
        BossDown,
        QuestCancel,
        PartyDismiss,
        PartyMemberChange,
        TimeOver
    }

    public class Dungeon
    {
        uint id;
        uint dungeonID;
        int time;
        Theme theme;
        uint startMap, endMap;
        int maxRoom, maxCross, maxFloor;
        List<DungeonMap> maps = new List<DungeonMap>();
        DungeonMap start, end;
        string spawnFile;
        Tasks.Dungeon.Dungeon task;
        ActorPC creator;

        public uint ID { get { return id; } set { this.id = value; } }
        public uint DungeonID { get { return this.dungeonID; } set { this.dungeonID = value; } }
        public int TimeLimit { get { return time; } set { this.time = value; } }
        public Theme Theme { get { return theme; } set { this.theme = value; } }
        public uint StartMap { get { return this.startMap; } set { this.startMap = value; } }
        public uint EndMap { get { return this.endMap; } set { this.endMap = value; } }
        public int MaxRoomCount { get { return this.maxRoom; } set { this.maxRoom = value; } }
        public int MaxCrossCount { get { return this.maxCross; } set { this.maxCross = value; } }
        public int MaxFloorCount { get { return this.maxFloor; } set { this.maxFloor = value; } }
        public string SpawnFile { get { return this.spawnFile; } set { this.spawnFile = value; } }
        public Tasks.Dungeon.Dungeon DestroyTask { get { return this.task; } }
        public ActorPC Creator { get { return this.creator; } set { this.creator = value; } }

        public List<DungeonMap> Maps { get { return this.maps; } }
        public DungeonMap Start { get { return this.start; } set { this.start = value; } }
        public DungeonMap End { get { return this.end; } set { this.end = value; } }

        public Dungeon Clone()
        {
            Dungeon dungeon = new Dungeon();
            dungeon.id = this.id;
            dungeon.time = this.time;
            dungeon.theme = this.theme;
            dungeon.startMap = this.startMap;
            dungeon.endMap = this.endMap;
            dungeon.maxCross = this.maxCross;
            dungeon.maxFloor = this.maxFloor;
            dungeon.maxRoom = this.maxRoom;
            dungeon.spawnFile = this.spawnFile;
            dungeon.task = new SagaMap.Tasks.Dungeon.Dungeon(dungeon, dungeon.time);
            return dungeon;
        }

        public void Destory(DestroyType type)
        {
            switch (type)
            {
                case DestroyType.BossDown:
                    {
                        foreach (Actor j in this.End.Map.Actors.Values)
                        {
                            if (j.type == ActorType.PC)
                            {
                                if (((ActorPC)j).Online)
                                {
                                    ActorEventHandlers.PCEventHandler eh = (SagaMap.ActorEventHandlers.PCEventHandler)j.e;
                                    Packets.Server.SSMG_NPC_SET_EVENT_AREA p1 = new SagaMap.Packets.Server.SSMG_NPC_SET_EVENT_AREA();
                                    if (end.Gates.ContainsKey(GateType.Exit))
                                    {
                                        p1.StartX = end.Gates[GateType.Exit].X;
                                        p1.EndX = end.Gates[GateType.Exit].X;
                                        p1.StartY = end.Gates[GateType.Exit].Y;
                                        p1.EndY = end.Gates[GateType.Exit].Y;
                                    }
                                    else
                                    {
                                        p1.StartX = (byte)(end.Map.Width / 2);
                                        p1.EndX = (byte)(end.Map.Width / 2);
                                        p1.StartY = (byte)(end.Map.Height / 2);
                                        p1.EndY =  (byte)(end.Map.Height / 2);
                                    }
                                    p1.EventID = 12001505;
                                    p1.EffectID = 9005;
                                    eh.Client.netIO.SendPacket(p1);
                                }
                            }
                        }                        
                        task.counter = (task.lifeTime - 31);
                    }
                    break;
                case DestroyType.PartyDismiss:
                    {
                        foreach (DungeonMap i in maps)
                        {
                            foreach (Actor j in i.Map.Actors.Values)
                            {
                                if (j.type == ActorType.PC)
                                {
                                    if (((ActorPC)j).Online)
                                    {
                                        ActorEventHandlers.PCEventHandler eh = (SagaMap.ActorEventHandlers.PCEventHandler)j.e;
                                        eh.Client.SendSystemMessage(Manager.LocalManager.Instance.Strings.ITD_PARTY_DISMISSED);
                                    }
                                }
                            }
                        }
                        task.counter = (task.lifeTime - 31);
                    }
                    break;
                case DestroyType.PartyMemberChange:
                    {
                        foreach (DungeonMap i in maps)
                        {
                            foreach (Actor j in i.Map.Actors.Values)
                            {
                                if (j.type == ActorType.PC)
                                {
                                    if (((ActorPC)j).Online)
                                    {
                                        ActorEventHandlers.PCEventHandler eh = (SagaMap.ActorEventHandlers.PCEventHandler)j.e;
                                        eh.Client.SendSystemMessage(string.Format("队伍成员发生异常变更!"));
                                    }
                                }
                            }
                        }
                        task.counter = (task.lifeTime - 31);
                    }
                    break;
                case DestroyType.QuestCancel:
                    {
                        foreach (DungeonMap i in maps)
                        {
                            foreach (Actor j in i.Map.Actors.Values)
                            {
                                if (j.type == ActorType.PC)
                                {
                                    if (((ActorPC)j).Online)
                                    {
                                        ActorEventHandlers.PCEventHandler eh = (SagaMap.ActorEventHandlers.PCEventHandler)j.e;
                                        eh.Client.SendSystemMessage(Manager.LocalManager.Instance.Strings.ITD_QUEST_CANCEL);
                                    }
                                }
                            }
                        }
                        task.counter = (task.lifeTime - 31);
                    }
                    break;
                case DestroyType.TimeOver:
                    {
                        foreach (DungeonMap i in maps)
                        {
                            Manager.MapManager.Instance.DeleteMapInstance(i.Map.ID);
                            i.Map.DungeonMap = null;
                            i.Map = null;
                        }
                        maps.Clear();
                        this.Creator.DungeonID = 0;
                        DungeonFactory.Instance.RemoveDungeon(this.dungeonID);
                    }
                    break;
            }
        }
    }
}

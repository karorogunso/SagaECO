using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;
using SagaDB.Actor;
using SagaDB.Map;

namespace SagaMap.Scripting
{
    public class WestFortGate : Event
    {
        public WestFortGate()
        {
            this.EventID = 0xF1000000;
        }

        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 32003001, 20, 81);
        }
    }

    public class WestFortField : Event
    {
        public WestFortField()
        {
            this.EventID = 0xF1000001;
        }

        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, 12019000, 5, 80);
        }
    }

    public class DungeonNorth : Event
    {
        public DungeonNorth()
        {
            this.EventID = 12001501;
        }

        public override void OnEvent(ActorPC pc)
        {
            Map map = Manager.MapManager.Instance.GetMap(pc.MapID);
            if (map.IsDungeon)
            {
                Dungeon.DungeonMap next = map.DungeonMap.Gates[SagaMap.Dungeon.GateType.North].ConnectedMap;
                Warp(pc, next.Map.ID, next.Gates[SagaMap.Dungeon.GateType.South].X, next.Gates[SagaMap.Dungeon.GateType.South].Y);
            }
        }
    }

    public class DungeonEast : Event
    {
        public DungeonEast()
        {
            this.EventID = 12001502;
        }

        public override void OnEvent(ActorPC pc)
        {
            Map map = Manager.MapManager.Instance.GetMap(pc.MapID);
            if (map.IsDungeon)
            {
                Dungeon.DungeonMap next = map.DungeonMap.Gates[SagaMap.Dungeon.GateType.East].ConnectedMap;
                Warp(pc, next.Map.ID, next.Gates[SagaMap.Dungeon.GateType.West].X, next.Gates[SagaMap.Dungeon.GateType.West].Y);
            }
        }
    }

    public class DungeonSouth : Event
    {
        public DungeonSouth()
        {
            this.EventID = 12001503;
        }

        public override void OnEvent(ActorPC pc)
        {
            Map map = Manager.MapManager.Instance.GetMap(pc.MapID);
            if (map.IsDungeon)
            {
                Dungeon.DungeonMap next = map.DungeonMap.Gates[SagaMap.Dungeon.GateType.South].ConnectedMap;
                Warp(pc, next.Map.ID, next.Gates[SagaMap.Dungeon.GateType.North].X, next.Gates[SagaMap.Dungeon.GateType.North].Y);
            }
        }
    }

    public class DungeonWest : Event
    {
        public DungeonWest()
        {
            this.EventID = 12001504;
        }

        public override void OnEvent(ActorPC pc)
        {
            Map map = Manager.MapManager.Instance.GetMap(pc.MapID);
            if (map.IsDungeon)
            {
                Dungeon.DungeonMap next = map.DungeonMap.Gates[SagaMap.Dungeon.GateType.West].ConnectedMap;
                Warp(pc, next.Map.ID, next.Gates[SagaMap.Dungeon.GateType.East].X, next.Gates[SagaMap.Dungeon.GateType.East].Y);
            }
        }
    }

    public class DungeonExit : Event
    {
        public DungeonExit()
        {
            this.EventID = 12001505;
        }

        public override void OnEvent(ActorPC pc)
        {
            Map map = Manager.MapManager.Instance.GetMap(pc.MapID);
            if (map.IsDungeon)
            {
                Warp(pc, map.ClientExitMap, map.ClientExitX, map.ClientExitY);
            }
        }
    }
}

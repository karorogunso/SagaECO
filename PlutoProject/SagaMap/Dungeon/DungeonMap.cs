using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaMap.Dungeon
{
    public enum MapType
    {
        Start,
        End,
        Room,
        Cross,
        Floor,
    }

    public enum Theme
    {
        Normal,
        Maimai,
        Yard,
    }

    public class DungeonMap
    {
        uint id;
        MapType type;
        Theme theme;
        Dictionary<GateType, DungeonGate> gates = new Dictionary<GateType, DungeonGate>();
        byte dir = 4;
        byte x, y;
        Map map;

        public uint ID { get { return id; } set { this.id = value; } }
        public MapType MapType { get { return type; } set { this.type = value; } }
        public Theme Theme { get { return theme; } set { theme = value; } }
        public Dictionary<GateType, DungeonGate> Gates { get { return this.gates; } }
        public Map Map { get { return this.map; } set { this.map = value; } }
        public byte Dir { get { return this.dir; } }
        public byte X { get { return this.x; } set { this.x = value; } }
        public byte Y { get { return this.y; } set { this.y = value; } }

        public int FreeGates
        {
            get
            {
                int j = 0;
                foreach (DungeonGate i in gates.Values)
                {
                    if (i.GateType == GateType.Entrance ||
                        i.GateType == GateType.Central ||
                        i.GateType == GateType.Exit)
                        continue;
                    if (i.ConnectedMap == null)
                        j++;
                }
                return j;
            }
        }

        public DungeonMap Clone()
        {
            DungeonMap newMap = new DungeonMap();
            newMap.id = this.id;
            newMap.type = this.type;
            newMap.theme = this.theme;
            foreach (GateType i in this.gates.Keys)
            {
                newMap.gates.Add(i, this.gates[i].Clone());
            }
            return newMap;
        }

        public byte GetXForGate(GateType type)
        {
            if (gates.ContainsKey(type))
            {
                switch (type)
                {
                    case GateType.North:
                        return x;
                    case GateType.East:
                        return (byte)(x + 1);
                    case GateType.South:
                        return x;
                    case GateType.West:
                        return (byte)(x - 1);
                    default:
                        return 255;
                }
            }
            else
                return 255;
        }

        public byte GetYForGate(GateType type)
        {
            if (gates.ContainsKey(type))
            {
                switch (type)
                {
                    case GateType.North:
                        return (byte)(y - 1);
                    case GateType.East:
                        return y;
                    case GateType.South:
                        return (byte)(y + 1);
                    case GateType.West:
                        return y;
                    default:
                        return 255;
                }
            }
            else
                return 255;
        }

        public void Rotate()
        {
            dir = (byte)((dir + 2) % 8);
            DungeonGate east = null, south = null, west = null, north = null;
            if (gates.ContainsKey(GateType.North))
                north = gates[GateType.North];
            if (gates.ContainsKey(GateType.East))
                east = gates[GateType.East];
            if (gates.ContainsKey(GateType.South))
                south = gates[GateType.South];
            if (gates.ContainsKey(GateType.West))
                west = gates[GateType.West];
            this.gates.Clear();
            if (north != null)
            {
                north.GateType = GateType.West;
                gates.Add(GateType.West, north);
            }
            if (east != null)
            {
                east.GateType = GateType.North;
                gates.Add(GateType.North, east);
            }
            if (south != null)
            {
                south.GateType = GateType.East;
                gates.Add(GateType.East, south);
            }
            if (west != null)
            {
                west.GateType = GateType.South;
                gates.Add(GateType.South , west);
            }            
        }
    }
}

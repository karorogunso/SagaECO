using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaMap.Dungeon
{
    public enum GateType
    {
        Entrance,
        East,
        West,
        South,
        North,
        Central,
        Exit,       
    }

    public enum Direction
    {
        In,
        Out,
    }

    public class DungeonGate
    {
        GateType type;
        byte x, y;
        uint npcID;
        DungeonMap map;
        Direction dir;

        public GateType GateType { get { return this.type; } set { this.type = value; } }
        public byte X { get { return this.x; } set { this.x = value; } }
        public byte Y { get { return this.y; } set { this.y = value; } }
        public uint NPCID { get { return this.npcID; } set { this.npcID = value; } }
        public DungeonMap ConnectedMap { get { return this.map; } set { this.map = value; } }
        public Direction Direction { get { return dir; } set { this.dir = value; } }

        public DungeonGate Clone()
        {
            DungeonGate gate = new DungeonGate();
            gate.type = this.type;
            gate.x = this.x;
            gate.y = this.y;
            gate.npcID = this.npcID;
            return gate;
        }
    }
}

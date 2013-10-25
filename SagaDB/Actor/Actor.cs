using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaDB.Actor
{
    public class Actor
    {
        uint id;
        public ActorType type;
        string name;
        public uint region;
        public bool invisble;
        short x, y;
        ushort dir;
        public uint sightRange;
        uint mapID;

        uint hp, mp, sp, max_hp, max_mp, max_sp;

        ushort speed;
        
        public ActorEventHandler e;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public uint ActorID { get { return this.id; } set { this.id = value; } }

        public uint MapID
        {
            get
            {
                return mapID;
            }
            set
            {
                mapID = value;
            }
        }

        public short X { get { return this.x; } set { this.x = value; } }
        public short Y { get { return this.y; } set { this.y = value; } }
        public ushort Dir { get { return this.dir; } set { this.dir = value; } }
        public ushort Speed { get { return this.speed; } set { this.speed = value; } }

        public uint HP { get { return this.hp; } set { this.hp = value; } }
        public uint MP { get { return this.mp; } set { this.mp = value; } }
        public uint SP { get { return this.sp; } set { this.sp = value; } }
        public uint MaxHP { get { return this.max_hp; } set { this.max_hp = value; } }
        public uint MaxMP { get { return this.max_mp; } set { this.max_mp = value; } }
        public uint MaxSP { get { return this.max_sp; } set { this.max_sp = value; } }

    }
}

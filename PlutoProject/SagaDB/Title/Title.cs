using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;

namespace SagaDB.Title
{
    public class Title
    {
        public uint ID;
        public byte difficulty;
        public string name, category;
        public int hp, sp, mp, atk_min, atk_max, matk_min, matk_max;
        public int def, mdef, hit_melee, hit_range, hit_magic, avoid_melee, avoid_range, avoid_magic;
        public int cri, cri_avoid, aspd, cspd;
        public uint PrerequisiteCount;
        public Dictionary<uint, ulong> Prerequisites = new Dictionary<uint, ulong>();
        public Dictionary<uint, ushort> Bonus = new Dictionary<uint, ushort>();

        public override string ToString()
        {
            return this.name;
        }
    }
}
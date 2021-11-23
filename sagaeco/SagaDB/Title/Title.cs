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
        public byte rate;
        public string name;
        public string typename;
        public uint hp, sp, mp, atk_min, atk_max, matk_min, matk_max;
        public uint def, mdef, hit_melee, hit_range, hit_magic, avoid_melee, avoid_range, avoid_magic;
        public uint cri, cri_avoid, aspd, cspd;
        public uint ConCount;
        public Dictionary<uint, ushort> Bouns = new Dictionary<uint, ushort>();
    }
}
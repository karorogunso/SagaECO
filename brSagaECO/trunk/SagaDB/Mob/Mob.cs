using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using SagaDB.Actor;

namespace SagaDB.Mob
{
    public class MobData
    {
        public class DropData
        {
            public uint ItemID = 0;
            public string TreasureGroup = "";
            public int Rate;
            public bool Party;
            public bool Public;
        }

        public float range;
        public int resilience;
        public uint id, pictid;
        public string name;
        public ushort speed;
        public MobType mobType;
        public ATTACK_TYPE attackType;
        public float mobSize;
        public bool fly;
        public bool undead;
        public uint hp, mp, sp;
        public byte level;
        public ushort atk_min, atk_max, matk_min, matk_max, def, def_add, mdef, mdef_add, str, mag, vit, dex, agi, intel, cri,criavd, hit_melee, hit_ranged,hit_magic,
            avoid_melee, avoid_ranged,avoid_magic;
        public short aspd, cspd;
        public uint baseExp, jobExp;

        public float physicreduce;
        public float magicreduce;

        public string[] dropRate;
        public List<DropData> dropItems = new List<DropData>();
        public List<DropData> dropItemsSpecial = new List<DropData>();
        public Dictionary<Elements, int> elements = new Dictionary<Elements, int>();
        public Dictionary<AbnormalStatus, short> abnormalStatus = new Dictionary<AbnormalStatus, short>();
        public DropData stampDrop;
        public int aiMode;
        public override string ToString()
        {
            return this.name;
        }
    }
}

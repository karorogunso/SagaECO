using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;

namespace SagaDB.Item
{

    [Serializable]
    public class Another
    {
        public uint id;
        public byte type;
        public string name;
        public byte lv;
        public List<uint> paperItems1 = new List<uint>();
        public List<uint> paperItems2 = new List<uint>();
        public uint requestItem1;
        public uint requestItem2;
        public uint awakeSkillID;
        public byte awakeSkillMaxLV;
        public Dictionary<uint, List<byte>> skills = new Dictionary<uint, List<byte>>();
        public ushort str;
        public ushort mag;
        public ushort vit;
        public ushort dex;
        public ushort agi;
        public ushort ing;
        public short hp_add;
        public short mp_add;
        public short sp_add;
        public ushort min_atk_add;
        public ushort max_atk_add;
        public ushort min_matk_add;
        public ushort max_matk_add;
        public ushort def_add;
        public ushort mdef_add;
        public ushort hit_melee_add;
        public ushort hit_magic_add;
        public ushort avoid_melee_add;
        public ushort avoid_magic_add;
    }
    public class AnotherDetail
    {
        public byte lv;
        public BitMask_Long value;
        public Dictionary<uint, ulong> skills = new Dictionary<uint, ulong>();
    }
}

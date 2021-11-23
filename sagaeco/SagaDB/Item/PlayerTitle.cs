using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;

namespace SagaDB.Item
{

    [Serializable]
    public class PlayerTitle
    {
        public uint id;
        public string titlename;
        public string firstname;
        public uint itemid;
        public byte lv;
        public ushort str;
        public ushort mag;
        public ushort vit;
        public ushort dex;
        public ushort agi;
        public ushort ing;
        public short hp_add;
        public short mp_add;
        public short sp_add;
        public short hprecov_add;
        public short mprecov_add;
        public short sprecov_add;
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
        public ushort cri_add;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaDB.Item
{
    [Serializable]
    public class EquipmentSet
    {
        uint id;
        string name;
        Dictionary<SagaDB.Item.EnumEquipSlot, uint> equipmentset = new Dictionary<EnumEquipSlot, uint>();

        SetBonus bonus = new SetBonus();

        /// <summary>
        /// 套装ID
        /// </summary>
        public uint ID { get { return id; } set { id = value; } }

        /// <summary>
        /// 套装名称
        /// </summary>
        public string Name { get { return name; } set { name = value; } }

        /// <summary>
        /// 套装套件
        /// </summary>
        public Dictionary<SagaDB.Item.EnumEquipSlot, uint> SetSlots { get { return equipmentset; } set { equipmentset = value; } }

        /// <summary>
        /// 奖励
        /// </summary>
        public SetBonus Bonus { get { return bonus; } set { bonus = value; } }
    }

    [Serializable]
    public class SetBonus
    {
        public uint str, agi, vit, _int, dex, mag, speed, aspd, cspd, def, def_add, mdef, mdef_add, mhp, mhprate, msp, msprate, mmp, mmprate;
        public uint guard, savoid, lavoid, criavoid, shit, lhit, cri;
        public uint askill1, askill2, askill3, pskill1, pskill2, pskill3;
    }
}

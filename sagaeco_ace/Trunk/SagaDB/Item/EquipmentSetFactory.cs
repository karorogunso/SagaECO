using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using SagaDB.Actor;

namespace SagaDB.Item
{
    public class EquipmentSetFactory : Factory<EquipmentSetFactory, EquipmentSet>
    {
        public EquipmentSetFactory()
        {
            this.loadingTab = "Loading EquipmentSet DataBase";
            this.loadedTab = " EquipmentSet Loaded.";
            this.databaseName = "EquipmentSet";
            this.FactoryType = FactoryType.CSV;
        }
        protected override uint GetKey(EquipmentSet item)
        {
            return item.ID;
        }
        protected override void ParseXML(System.Xml.XmlElement root, System.Xml.XmlElement current, EquipmentSet item)
        {
            throw new NotImplementedException();
        }

        protected override void ParseCSV(EquipmentSet item, string[] paras)
        {
            item.ID = uint.Parse(paras[0]);
            item.Name = paras[1];
            item.SetSlots[EnumEquipSlot.HEAD] = uint.Parse(paras[2]);
            item.SetSlots[EnumEquipSlot.HEAD_ACCE] = uint.Parse(paras[3]);
            item.SetSlots[EnumEquipSlot.FACE] = uint.Parse(paras[4]);
            item.SetSlots[EnumEquipSlot.FACE_ACCE] = uint.Parse(paras[5]);
            item.SetSlots[EnumEquipSlot.CHEST_ACCE] = uint.Parse(paras[6]);
            item.SetSlots[EnumEquipSlot.UPPER_BODY] = uint.Parse(paras[7]);
            item.SetSlots[EnumEquipSlot.LOWER_BODY] = uint.Parse(paras[8]);
            item.SetSlots[EnumEquipSlot.BACK] = uint.Parse(paras[9]);
            item.SetSlots[EnumEquipSlot.LEFT_HAND] = uint.Parse(paras[10]);
            item.SetSlots[EnumEquipSlot.RIGHT_HAND] = uint.Parse(paras[11]);
            item.SetSlots[EnumEquipSlot.SHOES] = uint.Parse(paras[12]);
            item.SetSlots[EnumEquipSlot.SOCKS] = uint.Parse(paras[13]);
            item.SetSlots[EnumEquipSlot.PET] = uint.Parse(paras[14]);
            item.SetSlots[EnumEquipSlot.EFFECT] = uint.Parse(paras[15]);
            item.Bonus.str = uint.Parse(paras[16]);
            item.Bonus.agi = uint.Parse(paras[17]);
            item.Bonus.vit = uint.Parse(paras[18]);
            item.Bonus._int = uint.Parse(paras[19]);
            item.Bonus.dex = uint.Parse(paras[20]);
            item.Bonus.mag = uint.Parse(paras[21]);
            item.Bonus.speed = uint.Parse(paras[22]);
            item.Bonus.aspd = uint.Parse(paras[23]);
            item.Bonus.cspd = uint.Parse(paras[24]);
            item.Bonus.def = uint.Parse(paras[25]);
            item.Bonus.def_add = uint.Parse(paras[26]);
            item.Bonus.mdef = uint.Parse(paras[27]);
            item.Bonus.mdef_add = uint.Parse(paras[28]);
            item.Bonus.mhp = uint.Parse(paras[29]);
            item.Bonus.msp = uint.Parse(paras[30]);
            item.Bonus.mmp = uint.Parse(paras[31]);
            item.Bonus.guard = uint.Parse(paras[32]);
            item.Bonus.savoid = uint.Parse(paras[33]);
            item.Bonus.lavoid = uint.Parse(paras[34]);
            item.Bonus.criavoid = uint.Parse(paras[35]);
            item.Bonus.shit = uint.Parse(paras[36]);
            item.Bonus.lhit = uint.Parse(paras[37]);
            item.Bonus.cri = uint.Parse(paras[38]);
            item.Bonus.askill1 = uint.Parse(paras[39]);
            item.Bonus.pskill1 = uint.Parse(paras[40]);
        }

        public SetBonus GetBonus(uint setid)
        {
            if (items.ContainsKey(setid))
                return items[setid].Bonus;
            else
            {
                Logger.ShowWarning("Equipment Set:" + setid.ToString() + " not found! Creating dummy Set Bonus.");
                return new SetBonus();
            }
        }
        public string GetSetName(uint setid)
        {
            if (items.ContainsKey(setid))
                return items[setid].Name;
            else
                return "";
        }
        public Dictionary<SagaDB.Item.EnumEquipSlot, uint> GetEquipSet(uint setid)
        {
            if (items.ContainsKey(setid))
                return items[setid].SetSlots;
            else
                return new Dictionary<EnumEquipSlot, uint>();
        }

    }
}

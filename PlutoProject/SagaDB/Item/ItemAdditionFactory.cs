using SagaLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SagaDB.Item
{
    public class ItemAdditionFactory : Factory<ItemAdditionFactory, ItemAddition>
    {
        public ItemAdditionFactory()
        {
            this.loadingTab = "Loading item addition database";
            this.loadedTab = " item additions loaded.";
            this.databaseName = "ItemAddition";
            this.FactoryType = FactoryType.CSV;
        }

        public ItemAddition GetItemAddition(uint itemID)
        {
            if (this.Items.ContainsKey(itemID))
                return this.Items[itemID];
            else
                return null;
        }

        protected override uint GetKey(ItemAddition item)
        {
            return item.ItemID;
        }

        protected override void ParseXML(XmlElement root, XmlElement current, ItemAddition item)
        {
            throw new NotImplementedException();
        }

        protected override void ParseCSV(ItemAddition item, string[] paras)
        {
            uint offset = 0;

            var bonus = paras[4 + offset];
            var bonusstr = bonus.Split(';');
            if (bonusstr.Length == 0)
                return;

            if (item.BonusString == null)
                item.BonusString = new List<string>();
            if (item.BonusList == null)
                item.BonusList = new List<ItemBonus>();

            for (int i = 0; i < bonusstr.Length; i++)
            {
                if (bonusstr[i] == "")
                    continue;
                item.BonusString.Add(bonusstr[i]);
                var str = bonusstr[i];
                var type = str.Split(' ')[0].Substring(str.Split(' ')[0].Length - 1);
                ItemBonus itemBonus = new ItemBonus();
                switch (type)
                {
                    case "s":
                        itemBonus.EffectType = byte.Parse(paras[3 + offset]);
                        itemBonus.BonusType = 0;
                        itemBonus.Attribute = str.Split(' ')[1].Split(',')[0];
                        itemBonus.Values1 = int.Parse(str.Split(' ')[1].Split(',')[1]);
                        item.BonusList.Add(itemBonus);
                        break;
                    case "2":
                        itemBonus.EffectType = byte.Parse(paras[3 + offset]);
                        itemBonus.BonusType = 1;
                        itemBonus.Attribute = str.Split(' ')[1].Split(',')[0];
                        itemBonus.Values1 = int.Parse(str.Split(' ')[1].Split(',')[1]);
                        itemBonus.Values2 = int.Parse(str.Split(' ')[1].Split(',')[2]);
                        item.BonusList.Add(itemBonus);
                        break;
                    case "3":
                        itemBonus.EffectType = byte.Parse(paras[3 + offset]);
                        itemBonus.BonusType = 2;
                        itemBonus.Attribute = str.Split(' ')[1].Split(',')[0];
                        itemBonus.Values1 = int.Parse(str.Split(' ')[1].Split(',')[1]);
                        itemBonus.Values2 = int.Parse(str.Split(' ')[1].Split(',')[2]);
                        itemBonus.Values3 = int.Parse(str.Split(' ')[1].Split(',')[3]);
                        item.BonusList.Add(itemBonus);
                        break;
                    case "4":
                        itemBonus.EffectType = byte.Parse(paras[3 + offset]);
                        itemBonus.BonusType = 3;
                        itemBonus.Attribute = str.Split(' ')[1].Split(',')[0];
                        itemBonus.Values1 = int.Parse(str.Split(' ')[1].Split(',')[1]);
                        itemBonus.Values2 = int.Parse(str.Split(' ')[1].Split(',')[2]);
                        itemBonus.Values3 = int.Parse(str.Split(' ')[1].Split(',')[3]);
                        itemBonus.Values4 = int.Parse(str.Split(' ')[1].Split(',')[4]);
                        item.BonusList.Add(itemBonus);
                        break;
                    default:
                        break;
                }
            }

            item.ID = uint.Parse(paras[0 + offset]);
            item.ItemID = uint.Parse(paras[1 + offset]);
            item.Desc = paras[2 + offset];
        }
    }
}

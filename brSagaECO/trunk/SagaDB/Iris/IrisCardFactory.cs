using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using SagaDB.Actor;

namespace SagaDB.Iris
{
    public class IrisCardFactory : Factory<IrisCardFactory, IrisCard>
    {
        public IrisCardFactory()
        {
            this.loadingTab = "Loading Iris Card database";
            this.loadedTab = " cards loaded.";
            this.databaseName = "Iris Card";
            this.FactoryType = FactoryType.CSV;
        }

        protected override void ParseXML(System.Xml.XmlElement root, System.Xml.XmlElement current, IrisCard item)
        {
            throw new NotImplementedException();
        }

        protected override uint GetKey(IrisCard item)
        {
            return item.ID;
        }

        protected override void ParseCSV(IrisCard item, string[] paras)
        {
            item.ID = uint.Parse(paras[0]);
            item.Name = paras[3];
            item.Serial = paras[5];
            item.Rarity = (Rarity)int.Parse(paras[8]);
            item.NextCard = uint.Parse(paras[10]);
            item.Rank = int.Parse(paras[11]);
            if (toBool(paras[12]))
                item.CanWeapon = true;
            if (toBool(paras[13]))
                item.CanArmor = true;
            if (toBool(paras[14]))
                item.CanNeck = true;
            for (int i = 0; i < 7; i++)
            {
                Elements element = (Elements)i;
                item.Elements.Add(element, int.Parse(paras[15 + i]));
            }
            for (int i = 0; i < 3; i++)
            {
                uint id = uint.Parse(paras[22 + i * 2]);
                if (IrisAbilityFactory.Instance.Items.ContainsKey(id))
                {
                    AbilityVector vector = IrisAbilityFactory.Instance.Items[id];
                    item.Abilities.Add(vector, int.Parse(paras[23 + i * 2]));
                }
            }
        }

        private bool toBool(string input)
        {
            if (input == "1") return true; else return false;
        }
    }
}

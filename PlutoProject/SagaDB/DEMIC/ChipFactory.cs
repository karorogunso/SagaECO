using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;

namespace SagaDB.DEMIC
{
    public class ChipFactory : Factory<ChipFactory, Chip.BaseData>
    {
        Dictionary<short, Chip.BaseData> byChipID = new Dictionary<short, Chip.BaseData>();

        public Dictionary<short, Chip.BaseData> ByChipID { get { return this.byChipID; } }
       
        public ChipFactory()
        {
            this.loadingTab = "Loading DEMIC Chip database";
            this.loadedTab = " chips loaded.";
            this.databaseName = "DEMIC Chip";
            this.FactoryType = FactoryType.CSV;
        }

        public Chip GetChip(uint itemID)
        {
            if (items.ContainsKey(itemID))
            {
                Chip chip = new Chip(items[itemID]);
                return chip;
            }
            else
            {
                Logger.ShowWarning("Cannot find chip:" + itemID.ToString());
                return null;
            }
        }

        protected override void ParseXML(System.Xml.XmlElement root, System.Xml.XmlElement current, Chip.BaseData item)
        {
            throw new NotImplementedException();
        }

        protected override uint GetKey(Chip.BaseData item)
        {
            return item.itemID;
        }

        protected override void ParseCSV(Chip.BaseData item, string[] paras)
        {
            item.chipID = short.Parse(paras[0]);
            item.itemID = uint.Parse(paras[1]);
            byChipID.Add(item.chipID, item);
            item.name = paras[2];
            item.type = byte.Parse(paras[3]);
            item.model = ModelFactory.Instance.Models[uint.Parse(paras[7])];
            item.possibleLv = byte.Parse(paras[8]);
            item.engageTaskChip = short.Parse(paras[9]);
            item.hp = short.Parse(paras[10]);
            item.mp = short.Parse(paras[11]);
            item.sp = short.Parse(paras[12]);
            item.str = short.Parse(paras[13]);
            item.mag = short.Parse(paras[14]);
            item.vit = short.Parse(paras[15]);
            item.dex = short.Parse(paras[16]);
            item.agi = short.Parse(paras[17]);
            item.intel = short.Parse(paras[18]);
            item.skill1 = uint.Parse(paras[19]);
            item.skill2 = uint.Parse(paras[20]);
            item.skill3 = uint.Parse(paras[21]);
        }
    }
}

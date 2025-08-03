using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Treasure;

namespace SagaDB.Treasure
{
    public class TreasureFactory : FactoryString<TreasureFactory,TreasureList>
    {
        public TreasureFactory()
        {
            this.loadingTab = "Loading Treasure database";
            this.loadedTab = " treasure groups loaded.";
            this.databaseName = "treasure";
            this.FactoryType = FactoryType.XML;
        }

        protected override string GetKey(TreasureList item)
        {
            return item.Name;
        }

        protected override void ParseCSV(TreasureList item, string[] paras)
        {
            throw new NotImplementedException();
        }

        protected override void ParseXML(XmlElement root, XmlElement current, TreasureList item)
        {
            switch (current.Name.ToLower())
            {
                case "treasurelist":
                    item.Name = current.Attributes[0].InnerText;                    
                    break;
                case "item":
                    TreasureItem treasure = new TreasureItem();
                    treasure.ID = uint.Parse(current.InnerText);
                    treasure.Rate = int.Parse(current.GetAttribute("rate"));
                    treasure.Count = int.Parse(current.GetAttribute("count"));
                    item.Items.Add(treasure);
                    item.TotalRate += treasure.Rate;
                    break;
            }
        }

        public TreasureItem GetRandomItem(string groupName)
        {
            if (this.items.ContainsKey(groupName))
            {
                TreasureList list = this.items[groupName];
                int ran = Global.Random.Next(0, list.TotalRate);
                int determinator = 0;
                foreach(TreasureItem i in list.Items)
                {
                    determinator += i.Rate;
                    if (ran <= determinator)
                        return i;
                }
                return null;
            }
            else
            {
                Logger.ShowDebug("Cannot find TreasureGroup:" + groupName, Logger.defaultlogger);
                return null;
            }
        }
    }
}

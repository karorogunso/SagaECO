using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Fish;

namespace SagaDB.Fish
{
    public class FishFactory : FactoryString<FishFactory, FishList>
    {
        public FishFactory()
        {
            this.loadingTab = "Loading FishList database";
            this.loadedTab = " Fish groups loaded.";
            this.databaseName = "Fish";
            this.FactoryType = FactoryType.XML;
        }
        public List<Fish> items = new List<Fish>();
        public int TotalRate;
        protected override string GetKey(FishList item)
        {
            return "钓鱼";
        }

        protected override void ParseCSV(FishList item, string[] paras)
        {
            throw new NotImplementedException();
        }

        protected override void ParseXML(XmlElement root, XmlElement current, FishList item)
        {
            switch (current.Name.ToLower())
            {
                case "item":
                    Fish fish = new Fish();
                    fish.ID = uint.Parse(current.InnerText);
                    fish.Rate = int.Parse(current.GetAttribute("rate"));
                    fish.Count = int.Parse(current.GetAttribute("count"));
                    items.Add(fish);
                    TotalRate += fish.Rate;
                    break;
            }
        }

        public Fish GetRandomItem(string groupName)
        {
            if (items != null)
            {
                int ran = Global.Random.Next(0, TotalRate);
                int determinator = 0;
                foreach (Fish i in items)
                {
                    determinator += i.Rate;
                    if (ran <= determinator)
                        return i;
                }
                return null;
            }
            else
            {
                Logger.ShowDebug("Cannot find FishGroup:" + groupName, Logger.defaultlogger);
                return null;
            }
        }
    }
}

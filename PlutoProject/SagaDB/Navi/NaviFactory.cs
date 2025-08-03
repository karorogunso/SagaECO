using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaLib.VirtualFileSystem;
using System.Xml;
namespace SagaDB.Navi
{
    public class NaviFactory : Factory<NaviFactory, Navi>
    {
        public NaviFactory()
        {
            this.loadingTab = "Loading navi database";
            this.loadedTab = " navis loaded.";
            this.databaseName = "navi";
            this.FactoryType = FactoryType.CSV;
        }
        uint i;
        protected override void ParseXML(System.Xml.XmlElement root, System.Xml.XmlElement current, Navi item)
        {
            throw new NotImplementedException();
        }

        protected override uint GetKey(Navi item)
        {
            return i;
        }

        protected override void ParseCSV(Navi item, string[] paras)
        {
            uint stepUniqueId = uint.Parse(paras[0]);
            uint categoryId = uint.Parse(paras[1]);
            uint eventId = uint.Parse(paras[2]);
            uint stepId = uint.Parse(paras[3]);
            if (!item.Categories.ContainsKey(categoryId))
            {
                item.Categories.Add(categoryId, new Category(categoryId));
            }
            Category c = item.Categories[categoryId];
            if (!c.Events.ContainsKey(eventId))
            {
                c.Events.Add(eventId, new Event(eventId));
            }
            Event e = c.Events[eventId];

            Step s = new Step(stepId, stepUniqueId, e);
            e.Steps.Add(stepId, s);
            item.UniqueSteps.Add(stepUniqueId, s);
            i++;
        }
    }
}

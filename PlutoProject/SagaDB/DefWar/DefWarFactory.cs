using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;

namespace SagaDB.DefWar
{
    public class DefWarFactory : Factory<DefWarFactory, DefWar.DefWarData>
    {
        public DefWarFactory()
        {
            this.loadingTab = "Loading Defwar database";
            this.loadedTab = " Defwars loaded.";
            this.databaseName = "Defwar";
            this.FactoryType = FactoryType.CSV;
        }

        protected override void ParseXML(System.Xml.XmlElement root, System.Xml.XmlElement current, DefWar.DefWarData item)
        {
            throw new NotImplementedException();
        }

        protected override uint GetKey(DefWar.DefWarData item)
        {
            return item.ID;
        }

        protected override void ParseCSV(DefWar.DefWarData item, string[] paras)
        {
            uint offset = 0;
            item.ID = uint.Parse(paras[0 + offset]);
            item.Title = paras[1 + offset];
        }


        public DefWar GetItem(uint id)
        {
            if (items.ContainsKey(id))
            {
                DefWar item = new DefWar(items[id]);

                return item;
            }
            return null;
        }
    }
}

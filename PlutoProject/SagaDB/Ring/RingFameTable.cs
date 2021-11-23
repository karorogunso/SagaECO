using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

using SagaLib;
using SagaDB.Actor;

namespace SagaDB.Ring
{
    public class RingFame
    {
        uint level;
        uint fame;

        public uint Level { get { return this.level; } set { this.level = value; } }

        public uint Fame { get { return this.fame; } set { this.fame = value; } }
    }

    public class RingFameTable : Factory<RingFameTable, RingFame>
    {
        public RingFameTable()
        {
            this.loadingTab = "Loading Ring Fame database";
            this.loadedTab = " entries loaded.";
            this.databaseName = " Ring fame";
            this.FactoryType = FactoryType.XML;
        }

        protected override uint GetKey(RingFame item)
        {
            return item.Level;
        }

        protected override void ParseCSV(RingFame item, string[] paras)
        {
            throw new NotImplementedException();
        }

        protected override void ParseXML(XmlElement root, XmlElement current, RingFame item)
        {
            switch (root.Name.ToLower())
            {
                case "level":
                    switch (current.Name.ToLower())
                    {
                        case "level":
                            item.Level = uint.Parse(current.InnerText);
                            break;
                        case "fame":
                            item.Fame = uint.Parse(current.InnerText);
                            break;                        
                    }
                    break;
            }
        }
    }
}

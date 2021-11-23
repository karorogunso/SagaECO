using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaLib.VirtualFileSystem;
using System.Xml;
namespace SagaDB.Experience
{
    public class PCExperienceFactory : Factory<PCExperienceFactory, PCLevel>
    {
        public PCExperienceFactory()
        {
            this.loadingTab = "Loading Experience table";
            this.loadedTab = " Experience table loaded.";
            this.databaseName = "EXP";
            this.FactoryType = FactoryType.CSV;
        }

        protected override void ParseXML(System.Xml.XmlElement root, System.Xml.XmlElement current, PCLevel item)
        {
            throw new NotImplementedException();
        }

        protected override uint GetKey(PCLevel item)
        {
            return item.level;
        }

        protected override void ParseCSV(PCLevel item, string[] paras)
        {
            item.level = byte.Parse(paras[0]);
            item.cexp = ulong.Parse(paras[1]);
            item.cexp2 = ulong.Parse(paras[2]);
            item.jexp = ulong.Parse(paras[3]);
            item.jexp2 = ulong.Parse(paras[4]);
            item.jexp3 = ulong.Parse(paras[5]);
            item.dualjexp = ulong.Parse(paras[6]);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaLib.VirtualFileSystem;
using System.Xml;
namespace SagaDB.Tamaire
{
    public class TamaireReward
    {
        public byte level;
        public ulong cexp,  jexp, cexp2, jexp3,  demcexp, demjexp;
    }
    public class TamaireExpRewardFactory : Factory<TamaireExpRewardFactory, TamaireReward>
    {
        public TamaireExpRewardFactory()
        {
            this.loadingTab = "Loading Tamaire Rewards";
            this.loadedTab = " Tamaire Rewards loaded.";
            this.databaseName = "TamaireReward";
            this.FactoryType = FactoryType.CSV;
        }

        protected override void ParseXML(System.Xml.XmlElement root, System.Xml.XmlElement current, TamaireReward item)
        {
            throw new NotImplementedException();
        }

        protected override uint GetKey(TamaireReward item)
        {
            return item.level;
        }

        protected override void ParseCSV(TamaireReward item, string[] paras)
        {
            item.level = byte.Parse(paras[0]);
            item.cexp = ulong.Parse(paras[1]);
            item.jexp = ulong.Parse(paras[2]);
            item.cexp2 = ulong.Parse(paras[3]);
            item.jexp3 = ulong.Parse(paras[4]);
            item.demcexp = ulong.Parse(paras[5]);
            item.demjexp = ulong.Parse(paras[6]);
        }
    }
}
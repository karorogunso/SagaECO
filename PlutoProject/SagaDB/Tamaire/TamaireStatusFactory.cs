using SagaDB.Actor;
using SagaLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SagaDB.Tamaire
{
    public class TamaireStatus
    {
        public byte level;
        public byte jobtype;
        public int hp, sp, mp, atk_min, atk_max, matk_min, matk_max;
        public int defpercent, mdefpercent, def,mdef,hit_melee, hit_range, avoid_melee, avoid_range, aspd, cspd, payload, capacity;
    }
    public class TamaireStatusFactory : Factory<TamaireStatusFactory, TamaireStatus>
    {
        public TamaireStatusFactory()
        {
            this.loadingTab = "Loading Tamaire Status";
            this.loadedTab = " Tamaire Status loaded.";
            this.databaseName = "TamaireStatus";
            this.FactoryType = FactoryType.CSV;
        }
        uint i;
        protected override uint GetKey(TamaireStatus item)
        {
            return i; //don't use this key as it has no physical meaning
        }

        protected override void ParseCSV(TamaireStatus item, string[] paras)
        {
            item.level = byte.Parse(paras[0]);
            item.jobtype = byte.Parse(paras[1]);
            //level + jobtype yields a unique TamaireStatus 
            item.hp = int.Parse(paras[2]);
            item.mp = int.Parse(paras[3]);
            item.sp = int.Parse(paras[4]);
            item.atk_min = int.Parse(paras[5]);
            item.atk_max = int.Parse(paras[6]);
            item.matk_min = int.Parse(paras[7]);
            item.matk_max = int.Parse(paras[8]);
            item.hit_melee = int.Parse(paras[9]);
            item.hit_range = int.Parse(paras[10]);
            item.def = int.Parse(paras[11]);
            item.defpercent = int.Parse(paras[12]);
            item.mdef = int.Parse(paras[13]);
            item.mdefpercent = int.Parse(paras[14]);
            item.avoid_melee = int.Parse(paras[15]);
            item.avoid_range = int.Parse(paras[16]);
            item.aspd = int.Parse(paras[17]);
            item.cspd = int.Parse(paras[18]);
            item.payload = int.Parse(paras[19]);
            item.capacity = int.Parse(paras[20]);
            i++;
        }

        protected override void ParseXML(XmlElement root, XmlElement current, TamaireStatus item)
        {
            throw new NotImplementedException();
        }
    }
}

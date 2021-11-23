using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaLib.VirtualFileSystem;
using System.Xml;
namespace SagaDB.Title
{
    public class TitleFactory : Factory<TitleFactory, Title>
    {
        public TitleFactory()
        {
            this.loadingTab = "Loading Title database";
            this.loadedTab = " titles loaded.";
            this.databaseName = "title";
            this.FactoryType = FactoryType.CSV;
        }

        protected override void ParseXML(System.Xml.XmlElement root, System.Xml.XmlElement current, Title item)
        {
            throw new NotImplementedException();
        }

        protected override uint GetKey(Title item)
        {
            return item.ID;
        }

        protected override void ParseCSV(Title item, string[] paras)
        {
            item.ID = uint.Parse(paras[0]);
            item.category = paras[2];
            item.difficulty = byte.Parse(paras[3]);
            item.name = paras[4];
            item.hp = int.Parse(paras[63]);
            item.mp = int.Parse(paras[64]);
            item.sp = int.Parse(paras[65]);
            item.atk_min = int.Parse(paras[66]);
            item.atk_max = int.Parse(paras[67]);
            item.matk_min = int.Parse(paras[72]);
            item.matk_max = int.Parse(paras[73]);
            item.def = int.Parse(paras[74]);
            item.mdef = int.Parse(paras[75]);
            item.hit_melee = int.Parse(paras[76]);
            item.hit_range = int.Parse(paras[77]);
            item.hit_magic = int.Parse(paras[78]);
            item.avoid_melee = int.Parse(paras[79]);
            item.avoid_range = int.Parse(paras[80]);
            item.avoid_magic = int.Parse(paras[81]);
            item.cri = int.Parse(paras[82]);
            item.cri_avoid = int.Parse(paras[83]);
            item.aspd = int.Parse(paras[84]);
            item.cspd = int.Parse(paras[85]);
            item.PrerequisiteCount = uint.Parse(paras[86]);
            int offset = 86;
            for (int i = 0; i < item.PrerequisiteCount; i++)
            {
                item.Prerequisites.Add(uint.Parse(paras[offset + 1]), ulong.Parse(paras[offset + 2]));
                offset += 2;
            }
        }
    }
}

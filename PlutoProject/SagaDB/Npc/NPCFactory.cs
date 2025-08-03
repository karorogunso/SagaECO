using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;

namespace SagaDB.Npc
{
    public class NPCFactory : Factory<NPCFactory, NPC>
    {
        public NPCFactory()
        {
            this.loadingTab = "Loading NPC database";
            this.loadedTab = " npcs loaded.";
            this.databaseName = "npc";
            this.FactoryType = FactoryType.CSV;
        }

        protected override void ParseXML(System.Xml.XmlElement root, System.Xml.XmlElement current, NPC item)
        {
            throw new NotImplementedException();
        }

        protected override uint GetKey(NPC item)
        {
            return item.ID;
        }

        protected override void ParseCSV(NPC item, string[] paras)
        {
            
            item.ID = uint.Parse(paras[0]);
            if(paras[1] == null || paras[1] == "0" || paras[1] == "")
            {
                item.Name = "_";
            }else
            {
                item.Name = paras[1];
            }
            
            item.MapID = uint.Parse(paras[2]);
            item.X = byte.Parse(paras[3]);
            item.Y = byte.Parse(paras[4]);            
        }
    }
}

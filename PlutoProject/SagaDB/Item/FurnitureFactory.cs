using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using SagaLib;

namespace SagaDB.Furniture
{
    public class FurnitureFactory : Factory<FurnitureFactory, Furniture>
    {
        public FurnitureFactory()
        {
            this.loadingTab = "Loading furniture database";
            this.loadedTab = " furnitures loaded.";
            this.databaseName = "furniture";
            this.FactoryType = FactoryType.CSV;
        }

        protected override void ParseXML(System.Xml.XmlElement root, System.Xml.XmlElement current, Furniture item)
        {
            throw new NotImplementedException();
        }

        protected override uint GetKey(Furniture item)
        {
            return item.ItemID;
        }
        public Furniture GetFurniture(uint id)
        {

            if (items.ContainsKey(id))
            {
                Furniture f =  items[id];
                return f;
            }
            else
            {
                Logger.ShowError("No Furniture Found! ("+id+")");
                return null;
            }
        }

        protected override void ParseCSV(Furniture item, string[] paras)
        {

            item.ItemID = uint.Parse(paras[0]);
            if (paras[1] == null || paras[1] == "0" || paras[1] == "")
            {
                item.Name = "_";
            }else
            {
                item.Name = paras[1];
            }
            
            item.PictID = uint.Parse(paras[2]);
            //item.Type = byte.Parse(paras[6]);
            item.EventID = uint.Parse(paras[3]);
            item.Capacity = ushort.Parse(paras[4]);
            item.DefaultMotion = ushort.Parse(paras[5]);
            for(int v = 6;v < 13; v++)
            {
                
                if(ushort.Parse(paras[v]) > 0) item.Motion.Add(ushort.Parse(paras[v]));
            }
            
        }

    }
}

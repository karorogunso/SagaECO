using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;

namespace SagaDB.Npc
{
    public class ShopFactory : Factory<ShopFactory, Shop>
    {
        public ShopFactory()
        {
            this.loadingTab = "Loading Shop database";
            this.loadedTab = " shops loaded.";
            this.databaseName = "shop";
            this.FactoryType = FactoryType.XML;
        }

        protected override void ParseXML(System.Xml.XmlElement root, System.Xml.XmlElement current, Shop item)
        {
            switch (root.Name.ToLower())
            {
                case "shop":
                    switch (current.Name.ToLower())
                    {
                        case "id":
                            item.ID = uint.Parse(current.InnerText);
                            break;
                        case "npc":
                            string[] npcs = current.InnerText.Split(',');
                            foreach (string i in npcs)
                            {
                                item.RelatedNPC.Add(uint.Parse(i));                             
                            }
                            break;
                        case "sellrate":
                            item.SellRate = uint.Parse(current.InnerText);
                            break;
                        case "buyrate":
                            item.BuyRate = uint.Parse(current.InnerText);
                            break;
                        case "buylimit":
                            item.BuyLimit = uint.Parse(current.InnerText);
                            break;
                        case "goods":
                            {
                                if ((Item.ItemFactory.Instance.GetItem(uint.Parse(current.InnerText))).BaseData.itemType != Item.ItemType.POTION
                                    && (Item.ItemFactory.Instance.GetItem(uint.Parse(current.InnerText))).BaseData.itemType != Item.ItemType.FOOD)
                                {
                                    item.Goods.Add(uint.Parse(current.InnerText));
                                }
                                else
                                {
                                    if (item.ID > 500)
                                    {
                                        item.Goods.Add(uint.Parse(current.InnerText));
                                    }
                                    else
                                    {
                                        item.Goods.Add(10022900);
                                    }
                                }
                            }
                            break;
                        case "shoptype":
                            {
                                item.ShopType = (ShopType)byte.Parse(current.InnerText);
                                
                            }
                            break;
                    }
                    break;
            }
        }

        protected override uint GetKey(Shop item)
        {
            return item.ID;
        }

        protected override void ParseCSV(Shop item, string[] paras)
        {
            throw new NotImplementedException();    
        }
    }
}

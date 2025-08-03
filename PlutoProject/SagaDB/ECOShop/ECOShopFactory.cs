using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

using SagaLib;
using SagaDB.Actor;

namespace SagaDB.ECOShop
{
    public class ECOShopFactory : Factory<ECOShopFactory, ShopCategory>
    {
        ShopItem lastItem;
        public ECOShopFactory()
        {
            this.loadingTab = "Loading ECO shop database";
            this.loadedTab = " shop caterories loaded.";
            this.databaseName = " ECO shop";
            this.FactoryType = FactoryType.XML;
        }

        protected override uint GetKey(ShopCategory item)
        {
            return item.ID;
        }

        protected override void ParseCSV(ShopCategory item, string[] paras)
        {
            throw new NotImplementedException();
        }

        protected override void ParseXML(XmlElement root, XmlElement current, ShopCategory item)
        {
            switch (root.Name.ToLower())
            {
                case "category":
                    switch (current.Name.ToLower())
                    {
                        case "id":
                            item.ID = uint.Parse(current.InnerText);
                            break;
                        case "name":
                            item.Name = current.InnerText;
                            break;                        
                    }
                    break;
                case "item":
                    switch (current.Name.ToLower())
                    {
                        case "id":
                            ShopItem newItem = new ShopItem();
                            uint itemID = uint.Parse(current.InnerText);
                            if (!item.Items.ContainsKey(itemID))
                                item.Items.Add(itemID, newItem);
                            else
                                Logger.ShowWarning(string.Format("Item:{0} already added for shop category:{1}! overwriting....", itemID, item.ID));
                            lastItem = newItem;
                            break;
                        case "points":
                            lastItem.points = uint.Parse(current.InnerText);
                            break;
                        case "comment":
                            lastItem.comment = current.InnerText;
                            break;
                        case "rentalminutes":
                            lastItem.rental = int.Parse(current.InnerText);
                            break;
                    }
                    break;
            }
        }
    }
}

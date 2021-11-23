using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

using SagaLib;
using SagaLib.VirtualFileSystem;
using SagaDB.Actor;

namespace SagaDB.FictitiousActors
{
    public class FictitiousActorsFactory : Singleton<FictitiousActorsFactory>
    {
        Dictionary<uint, List<Actor.Actor>> fictitiousactorslist = new Dictionary<uint,List<Actor.Actor>>();
        public Dictionary<uint, List<SagaDB.Actor.Actor>> FictitiousActorsList { get { return this.fictitiousactorslist; } set { this.fictitiousactorslist = value; } }

        public Dictionary<uint, Dictionary<uint, GolemShopItem>> GolemSellList = new Dictionary<uint, Dictionary<uint, GolemShopItem>>();
        public Dictionary<uint, Dictionary<uint, GolemShopItem>> GolemBuyList = new Dictionary<uint, Dictionary<uint, GolemShopItem>>();
        public void LoadActorsList(string path)
        {
            string[] file = SagaLib.VirtualFileSystem.VirtualFileSystemManager.Instance.FileSystem.SearchFile(path, "*.xml", System.IO.SearchOption.AllDirectories);
            int total = 0;
            foreach (string f in file)
                total += LoadOne(f);
            Logger.ShowInfo("Actors loaded...");
        }

        public void LoadShopLists(string path)
        {
            string[] file = SagaLib.VirtualFileSystem.VirtualFileSystemManager.Instance.FileSystem.SearchFile(path, "*.xml", System.IO.SearchOption.AllDirectories);
            int total = 0;
            foreach (string f in file)
                total += LoadShopListOne(f);
            Logger.ShowInfo("Actors loaded...");
        }

        public int LoadShopListOne(string f)
        {
            int total = 0;
            XmlDocument xml = new XmlDocument();
            try
            {
                XmlElement root;
                XmlNodeList list;
                System.IO.Stream fs = VirtualFileSystemManager.Instance.FileSystem.OpenFile(f);
                xml.Load(fs);
                root = xml["GolemShop"];
                list = root.ChildNodes;
                byte id = 0;
                byte type = 0;
                uint slotid = 0;
                GolemShopItem gsi = new GolemShopItem();
                Dictionary<uint, GolemShopItem> item = new Dictionary<uint, GolemShopItem>();
                foreach (object j in list)
                {
                    XmlElement i;
                    if (j.GetType() != typeof(XmlElement)) continue;
                    i = (XmlElement)j;
                    switch (i.Name.ToLower())
                    {
                        case "id":
                            id = byte.Parse(i.InnerText);
                            break;
                        case "type":
                            if(i.InnerText.ToLower() == "sell")
                                type = 1;
                            break;
                        case "item":
                            XmlNodeList listi = i.ChildNodes;
                            foreach (XmlElement y in listi)
                            {
                                switch (y.Name.ToLower())
                                {
                                    case "id":
                                        gsi.ItemID = uint.Parse(y.InnerText);
                                        break;
                                    case "price":
                                        gsi.Price = uint.Parse(y.InnerText);
                                        break;
                                    case "count":
                                        gsi.Count = ushort.Parse(y.InnerText);
                                        break;
                                }
                            }
                            slotid++;
                            item.Add(slotid, gsi);
                            break;
                    }
                    if (type == 1)
                    {
                        if (!GolemSellList.ContainsKey(id))
                            GolemSellList.Add(id, item);
                    }
                    else
                    {
                        if (!GolemBuyList.ContainsKey(id))
                            GolemBuyList.Add(id, item);
                    }
                }
            }
            catch (Exception ex) { SagaLib.Logger.ShowError(ex); }
            return total;
        }

        public int LoadOne(string f)
        {
            int total = 0;
            XmlDocument xml = new XmlDocument();
            try
            {
                XmlElement root;
                XmlNodeList list;
                SagaDB.Actor.Actor actor = new Actor.Actor();
                ActorFurniture fi = new ActorFurniture();
                ActorGolem Golem = new ActorGolem();
                System.IO.Stream fs = VirtualFileSystemManager.Instance.FileSystem.OpenFile(f);
                xml.Load(fs);
                root = xml["Actors"];
                list = root.ChildNodes;
                foreach (object j in list)
                {
                    XmlElement i;
                    if (j.GetType() != typeof(XmlElement)) continue;
                    i = (XmlElement)j;

                    string type = i.Attributes["Type"].Value;

                    switch (type)
                    {
                        case "PC":
                            actor = new SagaDB.Actor.ActorPC();
                            break;
                        case "FI":
                            actor = new SagaDB.Actor.ActorFurniture();
                            break;
                        case "GOLEM":
                            actor = new SagaDB.Actor.ActorGolem();
                            break;
                    }
                    XmlNodeList skills = i.ChildNodes;
                    foreach (object j2 in skills)
                    {
                        XmlElement i2;
                        if (j2.GetType() != typeof(XmlElement)) continue;
                        i2 = (XmlElement)j2;

                        switch (i2.Name.ToLower())
                        {
                            case "mapid":
                                actor.MapID = uint.Parse(i2.InnerText);
                                break;
                            case "x":
                                if (actor.type == ActorType.FURNITURE)
                                    actor.X = short.Parse(i2.InnerText);
                                else
                                {
                                    actor.X = -1;
                                    actor.X2 = byte.Parse(i2.InnerText);
                                }
                                break;
                            case "y":
                                if (actor.type == ActorType.FURNITURE)
                                    actor.Y = short.Parse(i2.InnerText);
                                else
                                {
                                    actor.Y = -1;
                                    actor.Y2 = byte.Parse(i2.InnerText);
                                }
                                break;
                            case "dir":
                                actor.Dir = (ushort)(ushort.Parse(i2.InnerText) * 45);
                                break;
                        }
                        switch (type)
                        {
                            #region PC
                            case "PC":
                                actor.type = ActorType.PC;
                                ActorPC pc = (ActorPC)actor;
                                if(pc.Equips == null)
                                pc.Equips = new uint[12];
                                pc.MaxHP = 100;
                                pc.HP = 100;
                                switch (i2.Name.ToLower())
                                {
                                    case "name":
                                        pc.Name = i2.InnerText;
                                        break;
                                    case "race":
                                        pc.Race = (PC_RACE)Enum.Parse(typeof(PC_RACE), i2.InnerText);
                                        break;
                                    case "gender":
                                        pc.Gender = (PC_GENDER)Enum.Parse(typeof(PC_GENDER), i2.InnerText);
                                        break;
                                    case "hairstyle":
                                        pc.HairStyle = ushort.Parse(i2.InnerText);
                                        break;
                                    case "haircolor":
                                        pc.HairColor = byte.Parse(i2.InnerText);
                                        break;
                                    case "wig":
                                        pc.Wig = ushort.Parse(i2.InnerText);
                                        break;
                                    case "face":
                                        pc.Face = ushort.Parse(i2.InnerText);
                                        break;
                                    case "tailstyle":
                                        pc.TailStyle = byte.Parse(i2.InnerText);
                                        break;
                                    case "wingstyle":
                                        pc.WingStyle = byte.Parse(i2.InnerText);
                                        break;
                                    case "wingcolor":
                                        pc.WingColor = byte.Parse(i2.InnerText);
                                        break;
                                    case "head_acce":
                                        pc.Equips[0] = uint.Parse(i2.InnerText);
                                        break;
                                    case "face_acce":
                                        pc.Equips[1] = uint.Parse(i2.InnerText);
                                        break;
                                    case "chest_acce":
                                        pc.Equips[2] = uint.Parse(i2.InnerText);
                                        break;
                                    case "upper_body":
                                        pc.Equips[3] = uint.Parse(i2.InnerText);
                                        break;
                                    case "lower_body":
                                        pc.Equips[4] = uint.Parse(i2.InnerText);
                                        break;
                                    case "back":
                                        pc.Equips[5] = uint.Parse(i2.InnerText);
                                        break;
                                    case "right_hand":
                                        pc.Equips[6] = uint.Parse(i2.InnerText);
                                        break;
                                    case "left_hand":
                                        pc.Equips[7] = uint.Parse(i2.InnerText);
                                        break;
                                    case "shoes":
                                        pc.Equips[8] = uint.Parse(i2.InnerText);
                                        break;
                                    case "socks":
                                        pc.Equips[9] = uint.Parse(i2.InnerText);
                                        break;
                                    case "pet":
                                        pc.Equips[10] = uint.Parse(i2.InnerText);
                                        break;
                                    case "effect":
                                        pc.Equips[11] = uint.Parse(i2.InnerText);
                                        break;
                                    case "trancel":
                                        pc.TranceID = uint.Parse(i2.InnerText);
                                        break;
                                    case "motion":
                                        pc.Motion = (MotionType)uint.Parse(i2.InnerText);
                                        pc.MotionLoop = true;
                                        break;
                                    case "shoptitle":
                                        string title = i2.InnerText;
                                        if (title != "")
                                        {
                                            pc.TInt["虚构玩家"] = 1;
                                            pc.TStr["虚构玩家店名"] = title;
                                        }
                                        break;
                                    case "titlesid":
                                        pc.AInt["称号_主语"] = int.Parse(i2.InnerText);
                                        break;
                                    case "titlecid":
                                        pc.AInt["称号_连词"] = int.Parse(i2.InnerText);
                                        break;
                                    case "titlepid":
                                        pc.AInt["称号_谓语"] = int.Parse(i2.InnerText);
                                        break;
                                    case "eventid":
                                        pc.TInt["虚拟玩家EventID"] = int.Parse(i2.InnerText);
                                        break;
                                    case "emotionid":
                                        pc.TInt["虚构玩家EmotionID"] = int.Parse(i2.InnerText);
                                        break;
                                }
                                break;
                            #endregion
                            #region FURNITURE
                            case "FI":
                                actor.type = ActorType.FURNITURE;
                                fi = (ActorFurniture)actor;
                                switch (i2.Name.ToLower())
                                {
                                    case "x":
                                        fi.X = short.Parse(i2.InnerText);
                                        break;
                                    case "y":
                                        fi.Y = short.Parse(i2.InnerText);
                                        break;
                                    case "z":
                                        fi.Z = short.Parse(i2.InnerText);
                                        break;
                                    case "xaxis":
                                        fi.Xaxis = short.Parse(i2.InnerText);
                                        break;
                                    case "yaxis":
                                        fi.Yaxis = short.Parse(i2.InnerText);
                                        break;
                                    case "zaxis":
                                        fi.Zaxis = short.Parse(i2.InnerText);
                                        break;
                                    case "name":
                                        fi.Name = i2.InnerText;
                                        break;
                                    case "motion":
                                        fi.Motion = ushort.Parse(i2.InnerText);
                                        break;
                                    case "pictid":
                                        fi.PictID = uint.Parse(i2.InnerText);
                                        break;
                                    case "itemid":
                                        fi.ItemID = uint.Parse(i2.InnerText);
                                        break;
                                }
                                break;
                            #endregion
                            #region GOLEM
                            case "GOLEM":
                                actor.type = ActorType.GOLEM;
                                Golem = (ActorGolem)actor;
                                switch (i2.Name.ToLower())
                                {
                                    case "name":
                                        Golem.Name = i2.InnerText;
                                        break;
                                    case "motion":
                                        Golem.Motion = ushort.Parse(i2.InnerText);
                                        Golem.MotionLoop = true;
                                        break;
                                    case "pictid":
                                        Golem.PictID = uint.Parse(i2.InnerText);
                                        break;
                                    case "eventid":
                                        //Golem.EventID = uint.Parse(i2.InnerText);
                                        break;
                                    case "title":
                                        Golem.Title = i2.InnerText;
                                        break;
                                    case "shoptype":
                                        string t = i2.InnerText.ToLower();
                                        if (t == "sell")
                                            Golem.GolemType = GolemType.Sell;
                                        else if(t == "buy")
                                            Golem.GolemType = GolemType.Buy;
                                        break;
                                    case "aitype":
                                        Golem.AIMode = byte.Parse(i2.InnerText);
                                        break;
                                }
                                break;
                                #endregion
                        }
                    }
                    if (actor.type == ActorType.FURNITURE)
                    {
                        if (!fictitiousactorslist.ContainsKey(fi.MapID)) this.fictitiousactorslist.Add(fi.MapID, new List<Actor.Actor>());
                        fictitiousactorslist[fi.MapID].Add(fi);
                    }
                    else
                    {
                        if (!fictitiousactorslist.ContainsKey(actor.MapID)) this.fictitiousactorslist.Add(actor.MapID, new List<Actor.Actor>());
                        fictitiousactorslist[actor.MapID].Add(actor);
                    }
                }
            }
            catch (Exception ex) { SagaLib.Logger.ShowError(ex); }
            return total;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

using SagaLib;
using SagaLib.VirtualFileSystem;
using SagaDB.Actor;


namespace SagaDB.FictitiousActors
{
    public class FictitiousActorsFactory : Singleton<FictitiousActorsFactory>
    {
        Dictionary<uint, List<SagaDB.Actor.Actor>> fictitiousactorslist = new Dictionary<uint, List<Actor.Actor>>();

        public Dictionary<uint, List<SagaDB.Actor.Actor>> FictitiousActorsList { get { return this.fictitiousactorslist; } set { this.fictitiousactorslist = value; } }

        public void LoadActorsList(string path)
        {
            string[] file = SagaLib.VirtualFileSystem.VirtualFileSystemManager.Instance.FileSystem.SearchFile(path, "*.xml", System.IO.SearchOption.AllDirectories);
            int total = 0;
            foreach (string f in file)
            {
                total += ParseXML(f);
            }
            Logger.ShowInfo(total + "FictitiousActor Group loaded.");
        }

        private int ParseXML(string f)
        {
            int total = 0;

            System.IO.Stream fs = SagaLib.VirtualFileSystem.VirtualFileSystemManager.Instance.FileSystem.OpenFile(f);

            XDocument doc = XDocument.Load(fs);
            try
            {
                var pclist = (from node in doc.Descendants("Actor")
                           where node.Attribute("Type").Value.Equals("PC")
                           select new
                           {
                               type = node.Attribute("Type").Value,
                               MapID = node.Element("MapID").Value,
                               X = -1,
                               Y = -1,
                               X2 = node.Element("x").Value,
                               Y2 = node.Element("y").Value,
                               Dir = node.Element("dir").Value,
                               Name = node.Element("Name").Value,
                               Race = node.Element("Race").Value,
                               Gender = node.Element("Gender").Value,
                               HairStyle = node.Element("HairStyle").Value,
                               HairColor = node.Element("HairColor").Value,
                               Wig = node.Element("Wig").Value,
                               Face = node.Element("Face").Value,
                               TailStyle = node.Element("TailStyle").Value,
                               WingStyle = node.Element("WingStyle").Value,
                               WingColor = node.Element("WingColor").Value,
                               Equips = new uint[] {
                                uint.Parse(node.Element("HEAD_ACCE").Value),
                                uint.Parse(node.Element("FACE_ACCE").Value),
                                uint.Parse(node.Element("CHEST_ACCE").Value),
                                uint.Parse(node.Element("UPPER_BODY").Value),
                                uint.Parse(node.Element("LOWER_BODY").Value),
                                uint.Parse(node.Element("BACK").Value),
                                uint.Parse(node.Element("RIGHT_HAND").Value),
                                uint.Parse(node.Element("LEFT_HAND").Value),
                                uint.Parse(node.Element("SHOES").Value),
                                uint.Parse(node.Element("SOCKS").Value),
                                uint.Parse(node.Element("PET").Value),
                                uint.Parse(node.Element("EFFECT").Value)
                            }

                           }).ToList();
                if (pclist.Count > 0)
                    foreach (var i in pclist)
                    {
                        ActorPC item = new ActorPC();
                        item.type = (ActorType)Enum.Parse(typeof(ActorType), "PC");
                        item.MapID = uint.Parse(i.MapID);
                        item.X = (short)i.X;
                        item.Y = (short)i.Y;
                        item.X2 = byte.Parse(i.X2);
                        item.Y2 = byte.Parse(i.Y2);
                        item.Dir = ushort.Parse(i.Dir);
                        item.Name = i.Name;
                        item.Race = (PC_RACE)Enum.Parse(typeof(PC_RACE), i.Race);
                        item.Gender = (PC_GENDER)Enum.Parse(typeof(PC_GENDER), i.Gender);
                        item.HairStyle = ushort.Parse(i.HairStyle);
                        item.HairColor = byte.Parse(i.HairColor);
                        item.Wig = ushort.Parse(i.Wig);
                        item.Face = ushort.Parse(i.Face);
                        item.TailStyle = byte.Parse(i.TailStyle);
                        item.WingStyle = byte.Parse(i.WingStyle);
                        item.WingColor = byte.Parse(i.WingColor);
                        item.equips = new uint[12];
                        item.Equips = i.Equips;
                        if (!fictitiousactorslist.ContainsKey(item.MapID))
                            this.fictitiousactorslist.Add(item.MapID, new List<Actor.Actor>());
                        fictitiousactorslist[item.MapID].Add(item);
                    }

                total += pclist.Count;

                var listfi = (from node in doc.Descendants("Actor")
                            where node.Attribute("Type").Value == "FI"
                            select new 
                            {
                                type = node.Attribute("Type").Value,
                                MapID = node.Element("MapID").Value,
                                ItemID = node.Element("ItemID").Value,
                                PictID = node.Element("PictID").Value,
                                Name = node.Element("Name").Value,
                                Motion = node.Element("Motion").Value,
                                X = node.Element("x").Value,
                                Y = node.Element("y").Value,
                                Z = node.Element("z").Value,
                                Xaxis = node.Element("Xaxis").Value,
                                Yaxis = node.Element("Yaxis").Value,
                                Zaxis = node.Element("Zaxis").Value
                            }).ToList();

                if (listfi.Count > 0)
                    foreach (var i in listfi)
                    {
                        ActorFurniture item = new ActorFurniture();
                        item.type = (ActorType)Enum.Parse(typeof(ActorType), "FURNITURE");
                        item.MapID = uint.Parse(i.MapID);
                        item.ItemID = uint.Parse(i.ItemID);
                        item.PictID = uint.Parse(i.PictID);
                        item.Name = i.Name;
                        item.Motion = ushort.Parse(i.Motion);
                        item.X = short.Parse(i.X);
                        item.Y = short.Parse(i.Y);
                        item.Xaxis = short.Parse(i.Xaxis);
                        item.Yaxis = short.Parse(i.Yaxis);
                        item.Zaxis = short.Parse(i.Zaxis);
                        if (!fictitiousactorslist.ContainsKey(item.MapID))
                            this.fictitiousactorslist.Add(item.MapID, new List<Actor.Actor>());
                        fictitiousactorslist[item.MapID].Add(item);
                    }

                total += listfi.Count;
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
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
                System.IO.Stream fs = SagaLib.VirtualFileSystem.VirtualFileSystemManager.Instance.FileSystem.OpenFile(f);
                xml.Load(fs);
                root = xml["Actors"];
                list = root.ChildNodes;
                ActorPC pc = new ActorPC();
                ActorFurniture fi = new ActorFurniture();
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
                    }
                    XmlNodeList skills = i.ChildNodes;
                    foreach (object j2 in skills)
                    {
                        XmlElement i2;
                        if (j2.GetType() != typeof(XmlElement)) continue;
                        i2 = (XmlElement)j2;

                        switch (i2.Name.ToLower())
                        {
                            case "actorid":
                                actor.ActorID = uint.Parse(i2.InnerText);
                                break;
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
                                pc = actor as ActorPC;
                                pc.equips = new uint[12];
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
                                }
                                break;
                            #endregion
                            #region FURNITURE
                            case "FT":
                                actor.type = ActorType.FURNITURE;
                                fi = actor as ActorFurniture;
                                switch (i2.Name.ToLower())
                                {
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
                        }
                    }
                    if (type == "PC")
                    {
                        if (!fictitiousactorslist.ContainsKey(pc.MapID))
                            this.fictitiousactorslist.Add(pc.MapID, new List<Actor.Actor>());
                        fictitiousactorslist[actor.MapID].Add(pc);
                    }
                    else if (type == "FI")
                    {
                        if (!fictitiousactorslist.ContainsKey(fi.MapID))
                            this.fictitiousactorslist.Add(fi.MapID, new List<Actor.Actor>());
                        fictitiousactorslist[actor.MapID].Add(fi);
                    }
                }
            }
            catch (Exception ex) { SagaLib.Logger.ShowError(ex); }
            return total;
        }
    }
}

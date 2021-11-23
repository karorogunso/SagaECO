using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

using SagaLib;
using SagaLib.VirtualFileSystem;
using ICSharpCode.SharpZipLib.Zip;

namespace SagaDB.Map
{
    public class MapInfoFactory : Singleton<MapInfoFactory>
    {
        public IConcurrentDictionary<uint, MapInfo> mapInfos = new IConcurrentDictionary<uint, MapInfo>();
        Dictionary<string, List<MapObject>> mapObjects;

        public IConcurrentDictionary<uint, MapInfo> MapInfo
        {
            get
            {
                return this.mapInfos;
            }
        }

        public Dictionary<string, List<MapObject>> MapObjects
        {
            get
            {
                return this.mapObjects;
            }
        }

        public MapInfoFactory()
        {
        }

        public void LoadMapObjects(string path)
        {
            System.IO.Stream fs = VirtualFileSystemManager.Instance.FileSystem.OpenFile(path);
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            mapObjects = (Dictionary<string, List<MapObject>>)bf.Deserialize(fs);
        }

        public void ApplyMapObject()
        {
            if (mapObjects != null)
            {
                foreach (string i in mapObjects.Keys)
                {
                    uint id = uint.Parse(i);
                    if (this.mapInfos.ContainsKey(id))
                    {
                        SagaDB.Map.MapInfo info = this.mapInfos[id];
                        foreach (MapObject j in mapObjects[i])
                        {
                            if (j.Name.IndexOf("kadoukyo") != -1)
                                continue;
                            if (j.Name.IndexOf("hasi") != -1)
                                continue;
                            int[,][] matrix = j.PositionMatrix;                                
                            for (int k = 0; k < j.Width; k++)
                            {
                                for (int l = 0; l < j.Height; l++)
                                {
                                    int x = j.X + matrix[k, l][0];
                                    int y = j.Y + matrix[k, l][1];
                                    if (x < info.width && y < info.height && x>=0 && y>=0)
                                        info.walkable[x, y] = 0;
                                }
                            }
                        }
                    }
                }
            }
        }
        public void LoadMapName(string path, System.Text.Encoding encoding)
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(VirtualFileSystemManager.Instance.FileSystem.OpenFile(path), encoding);
            Logger.ShowInfo("Loading Map Names database...");
            string[] paras;
            while (!sr.EndOfStream)
            {
                string line;
                line = sr.ReadLine();
                try
                {
                    if (line == "") continue;
                    if (line.Substring(0, 1) == "#")
                        continue;
                    paras = line.Split(',');
                    uint mapid = uint.Parse(paras[0]);
                    string name = paras[1];
                    if (mapInfos.ContainsKey(mapid))
                        mapInfos[mapid].name = name;
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                }
            }
        }
        public void LoadMapSkills(string path, System.Text.Encoding encoding)
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(VirtualFileSystemManager.Instance.FileSystem.OpenFile(path), encoding);
            Logger.ShowInfo("Loading Map Skills database...");
            string[] paras;
            while (!sr.EndOfStream)
            {
                string line;
                line = sr.ReadLine();
                try
                {
                    if (line == "") continue;
                    if (line.Substring(0, 1) == "#")
                        continue;
                    paras = line.Split(',');
                    uint mapid = uint.Parse(paras[0]);
                    uint skillid = uint.Parse(paras[1]);
                    byte skilllv = byte.Parse(paras[2]);
                    MapInfo info = mapInfos[mapid];
                    info.MapSkills.Add(skillid, skilllv);
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                }
            }
        }
            public void LoadGatherInterval(string path, System.Text.Encoding encoding)
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(VirtualFileSystemManager.Instance.FileSystem.OpenFile(path), encoding);
            Logger.ShowInfo("Loading Gather database...");
            int count = 0;
            string[] paras;
            while (!sr.EndOfStream)
            {
                string line;
                line = sr.ReadLine();
                try
                {
                    MapInfo info = null;
                    if (line == "") continue;
                    if (line.Substring(0, 1) == "#")
                        continue;
                    paras = line.Split(',');

                    for (int i = 0; i < paras.Length; i++)
                    {
                        if (paras[i] == "" || paras[i].ToLower() == "null")
                            paras[i] = "0";
                    }
                    uint mapID = uint.Parse(paras[0]);
                    if (this.mapInfos.ContainsKey(mapID))
                        info = this.mapInfos[mapID];
                    if (info == null)
                        continue;
                    for (int i = 0; i < 8; i++)
                    {
                        int min = int.Parse(paras[1 + i]);
                        if (min > 0)
                            info.gatherInterval.Add((SagaDB.Marionette.GatherType)i, min);
                    }
                    count++;
                }
                catch (Exception ex)
                {
                    Logger.ShowError("Error on parsing gather db!\r\nat line:" + line);
                    Logger.ShowError(ex);
                }
            }
            Logger.ShowInfo(count + " gather informations loaded.");
            sr.Close();
        }
        public void LoadMapFish(string path)
        {
            XmlDocument xml = new XmlDocument();
            try
            {
                XmlElement root;
                XmlNodeList list;
                xml.Load(VirtualFileSystemManager.Instance.FileSystem.OpenFile(path));
                root = xml["ECOFish"];
                list = root.ChildNodes;
                foreach (object j in list)
                {
                    XmlElement i;
                    if (j.GetType() != typeof(XmlElement)) continue;
                    i = (XmlElement)j;
                    switch (i.Name.ToLower())
                    {
                        case "map":
                            XmlNodeList list2 = i.ChildNodes;
                            uint mapid = 0;
                            byte fishtype = 0;
                            byte xS = 0,yS = 0,xE = 0,yE = 0;
                            foreach (object l in list2)
                            {
                                XmlElement k;
                                if (l.GetType() != typeof(XmlElement)) continue;
                                k = (XmlElement)l;
                                switch (k.Name.ToLower())
                                {
                                    case "mapid":
                                        mapid = uint.Parse(k.InnerText);
                                        break;
                                    case "fishtype":
                                        fishtype = byte.Parse(k.InnerText);
                                        break;
                                    case "xs":
                                        xS = byte.Parse(k.InnerText);
                                        break;
                                    case "ys":
                                        yS = byte.Parse(k.InnerText);
                                        break;
                                    case "xe":
                                        xE = byte.Parse(k.InnerText);
                                        break;
                                    case "ye":
                                        yE = byte.Parse(k.InnerText);
                                        break;
                                }
                            }
                            SagaDB.Map.MapInfo info = this.mapInfos[mapid];
                            info.id = mapid;
                            for (int infoX = xS; infoX < xE; infoX++)
                            {
                                for (int infoY = yS; infoY < yE; infoY++)
                                {
                                    info.canfish[infoX, infoY] = fishtype;
                                }
                            }

                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
        }
        public void LoadFlags(string path)
        {
            XmlDocument xml = new XmlDocument();
            try
            {
                XmlElement root;
                XmlNodeList list;
                xml.Load(VirtualFileSystemManager.Instance.FileSystem.OpenFile(path));
                root = xml["MapFlags"];
                list = root.ChildNodes;
                foreach (object j in list)
                {
                    XmlElement i;
                    if (j.GetType() != typeof(XmlElement)) continue;
                    i = (XmlElement)j;
                    switch (i.Name.ToLower())
                    {
                        case "map":
                            uint mapid = uint.Parse(i.Attributes[0].InnerText);
                            if (this.mapInfos.ContainsKey(mapid))
                                this.mapInfos[mapid].Flag.Value = int.Parse(i.InnerText);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
        }

        public void Init(string path)
        {
            Init(path, true);
        }

        public void Init(string path,bool fullinfo)
        {
            System.IO.Stream stream = VirtualFileSystemManager.Instance.FileSystem.OpenFile(path);
            ZipFile file = new ZipFile(stream);
            int total = (int)file.Count;
            stream.Position = 0;
            ZipInputStream zs = new ZipInputStream(stream);
            ZipEntry entry;
#if !Web
            string label = "Loading MapInfo.zip";
            Logger.ProgressBarShow(0, (uint)total, label);
#endif
            DateTime time = DateTime.Now;
            
            entry = zs.GetNextEntry();        
            byte[] buf = new byte[2048];
            int count = 0;
            while (entry != null)
            {
                System.IO.MemoryStream ms = new System.IO.MemoryStream((int)entry.Size);
                System.IO.BinaryReader br;
                int size;
                while (true)
                {
                    size = zs.Read(buf, 0, 2048);
                    if (size > 0)
                    {
                        ms.Write(buf, 0, size);
                    }
                    else
                    {
                        break;
                    }
                }
                ms.Flush();                
                br = new System.IO.BinaryReader(ms);
                ms.Position = 0;
                MapInfo info = new MapInfo();
                info.id = br.ReadUInt32();
                info.id = uint.Parse(System.IO.Path.GetFileNameWithoutExtension(entry.Name));
                info.name = Global.Unicode.GetString(br.ReadBytes(32)).Replace("\0", "");
                info.width = br.ReadUInt16();
                info.height = br.ReadUInt16();

                fullinfo = true;

                if (fullinfo)
                {
                    byte fire, wind, water, earth, holy, dark, neutral;
                    info.canfish = new uint[info.width, info.height];
                    info.walkable = new byte[info.width, info.height];
                    info.holy = new byte[info.width, info.height];
                    info.dark = new byte[info.width, info.height];
                    info.neutral = new byte[info.width, info.height];
                    info.fire = new byte[info.width, info.height];
                    info.wind = new byte[info.width, info.height];
                    info.water = new byte[info.width, info.height];
                    info.earth = new byte[info.width, info.height];


                    holy = br.ReadByte();
                    dark = br.ReadByte();
                    neutral = br.ReadByte();
                    fire = br.ReadByte();
                    wind = br.ReadByte();
                    water = br.ReadByte();
                    earth = br.ReadByte();
                    ms.Position += 2;
                    for (int i = 0; i < info.height; i++)
                    {
                        for (int j = 0; j < info.width; j++)
                        {
                            uint eventid = br.ReadUInt32();
                            if (eventid != 0)
                            {
                                if (!info.events.ContainsKey(eventid))
                                {
                                    info.events.Add(eventid, new byte[2] { (byte)j, (byte)i });
                                }
                                else
                                {
                                    byte[] tmp = new byte[info.events[eventid].Length + 2];
                                    info.events[eventid].CopyTo(tmp, 0);
                                    tmp[tmp.Length - 2] = (byte)j;
                                    tmp[tmp.Length - 1] = (byte)i;
                                    info.events[eventid] = tmp;
                                }
                                info.canfish[j, i] = eventid;
                            }
                            info.holy[j, i] = (byte)((br.ReadByte() + holy));
                            info.dark[j, i] = (byte)((br.ReadByte() + dark));
                            info.neutral[j, i] = (byte)((br.ReadByte() + neutral));
                            info.fire[j, i] = (byte)((br.ReadByte() + fire));
                            info.wind[j, i] = (byte)((br.ReadByte() + wind));
                            info.water[j, i] = (byte)((br.ReadByte() + water));
                            info.earth[j, i] = (byte)((br.ReadByte() + earth));
                            ms.Position += 2;
                            info.walkable[j, i] = br.ReadByte();
                            ms.Position += 3;
                        }
                    }
                }
                mapInfos.Add(info.id, info);
                ms.Close();
                entry = zs.GetNextEntry();
                count++;
#if !Web
                if ((DateTime.Now - time).TotalMilliseconds > 40)
                {
                    time = DateTime.Now;
                    Logger.ProgressBarShow((uint)count, (uint)total, label);
                }
#endif
            }
            zs.Close();
            file.Close();
#if !Web
            Logger.ProgressBarHide(count + " maps loaded."); 
#endif
        }
    }
}

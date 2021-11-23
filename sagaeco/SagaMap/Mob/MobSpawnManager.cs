using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Mob;

namespace SagaMap.Mob
{
    public class MobSpawnManager : Singleton<MobSpawnManager>
    {
        Dictionary<uint, List<ActorMob>> mobs = new Dictionary<uint, List<ActorMob>>();
        public MobSpawnManager()
        {

        }

        public Dictionary<uint, List<ActorMob>> Spawns { get { return this.mobs; } }

        public int LoadOne(string f, uint setMap)
        {
            return LoadOne(f, setMap, true, true);
        }

        public int LoadOne(string f, uint setMap,bool loadDelay,bool loadNoDelay)
        {
            int total = 0;
            XmlDocument xml = new XmlDocument();
            try
            {
                XmlElement root;
                XmlNodeList list;
                System.IO.Stream fs = SagaLib.VirtualFileSystem.VirtualFileSystemManager.Instance.FileSystem.OpenFile(f);
                xml.Load(fs);                
                root = xml["Spawns"];
                list = root.ChildNodes;
                foreach (object j in list)
                {
                    XmlElement i;
                    if (j.GetType() != typeof(XmlElement)) continue;
                    i = (XmlElement)j;
                    switch (i.Name.ToLower())
                    {
                        case "spawn":
                            XmlNodeList list2 = i.ChildNodes;
                            uint map = 0, mobid = 0;
                            byte x = 0, y = 0;
                            int amount = 0, range = 0;
                            int delay = 30;
                            int rate = 100;
                            string announce = "";

                            string attr = i.GetAttribute("rate");
                            if (attr != "")
                                rate = int.Parse(attr);
                            foreach (object l in list2)
                            {
                                XmlElement k;
                                if (l.GetType() != typeof(XmlElement)) continue;
                                k = (XmlElement)l;
                                
                                switch (k.Name.ToLower())
                                {
                                    case "id":
                                        mobid = uint.Parse(k.InnerText);
                                        break;
                                    case "map":
                                        if (setMap != 0)
                                            map = setMap;
                                        else
                                            map = uint.Parse(k.InnerText);
                                        break;
                                    case "x":
                                        x = byte.Parse(k.InnerText);
                                        break;
                                    case "y":
                                        y = byte.Parse(k.InnerText);
                                        break;
                                    case "amount":
                                        amount = int.Parse(k.InnerText) * Configuration.Instance.MobAmount;
                                        break;
                                    case "range":
                                        range = int.Parse(k.InnerText);
                                        break;
                                    case "delay":
                                        delay = int.Parse(k.InnerText);
                                        break;
                                    case "announce":
                                        announce = k.InnerText;
                                        break;
                                }
                            }
                            if (map == 0) map = setMap;
                            if (map == 0) continue;
                            if (!loadDelay && delay != 0)
                                continue;
                            if (!loadNoDelay && delay == 0)
                                continue;
                            else
                                if (delay == 0)
                                    rate = 100;
                            if (rate <= Global.Random.Next(0, 99))
                                continue;
                            if (!Configuration.Instance.HostedMaps.Contains(map))
                                continue;
                            for (int count = 0; count < amount; count++)
                            {
                                if (!MobFactory.Instance.Mobs.ContainsKey(mobid))
                                    break;
                                ActorMob mob = new ActorMob(mobid);
                                if ((10360001 <= mob.MobID && mob.MobID <= 10370055) || (mob.MobID >= 30270000 && mob.MobID <= 30290107))
                                    break;
                                if (mob.BaseData.mobType == MobType.TREE_MATERIAL || mob.BaseData.mobType == MobType.ROCK_MATERIAL || mob.BaseData.mobType == MobType.PLANT_MATERIAL)
                                    continue;
                                mob.MapID = map;
                                Map map_ = Manager.MapManager.Instance.GetMap(map);
                                int x_new, y_new;
                                if (map_ == null)
                                    continue;
                                if (x == 0 && y == 0 && range == 0)
                                    break;
                                int min_x, max_x, min_y, max_y;
                                min_x = x - range;
                                max_x = x + range;
                                min_y = y - range;
                                max_y = y + range;
                                if (min_x < 0) min_x = 0;
                                if (max_x >= map_.Width)
                                    max_x = map_.Width - 1;
                                if (min_y < 0) min_y = 0;
                                if (max_y >= map_.Height)
                                    max_y = map_.Height - 1;

                                x_new = (byte)Global.Random.Next(min_x, max_x);
                                y_new = (byte)Global.Random.Next(min_y, max_y);

                                int counter = 0;
                                try
                                {
                                    while (map_.Info.walkable[x_new, y_new] != 2)
                                    {
                                        if (counter > 1000 || range == 0)
                                        {
                                            //Logger.ShowWarning(string.Format("Cannot find free place for mob:{0} map:{1}[{2},{3}]", mobid, map, x, y), Logger.defaultlogger);
                                            break;
                                        }
                                        x_new = (byte)Global.Random.Next(min_x, max_x);
                                        y_new = (byte)Global.Random.Next(min_y, max_y);
                                        counter++;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    SagaLib.Logger.ShowError(ex);
                                }
                                if (counter > 1000)
                                    continue;
                                mob.X = Global.PosX8to16((byte)x_new, map_.Width);
                                mob.Y = Global.PosY8to16((byte)y_new, map_.Height);
                                mob.Dir = (ushort)Global.Random.Next(0, 7);
                                ActorEventHandlers.MobEventHandler eh = new ActorEventHandlers.MobEventHandler(mob);
                                mob.e = eh;
                                if (MobAIFactory.Instance.Items.ContainsKey(mob.MobID))
                                    eh.AI.Mode = MobAIFactory.Instance.Items[mob.MobID];
                                else
                                    eh.AI.Mode = new AIMode(0);
                                eh.AI.X_Ori = Global.PosX8to16(x, map_.Width);
                                eh.AI.Y_Ori = Global.PosY8to16(y, map_.Height);
                                eh.AI.X_Spawn = mob.X;
                                eh.AI.Y_Spawn = mob.Y;
                                eh.AI.MoveRange = (short)(range * 100);
                                if (OriMobSetting.Instance.Items.ContainsKey(mob.MobID))
                                {
                                    delay = Global.Random.Next(3600 * 4, 3600 * 5);
                                }
                                if (delay > 300)
                                    eh.AI.SpawnDelay = delay * 1100;
                                else
                                    eh.AI.SpawnDelay = delay * 1000;
                                eh.AI.Announce = announce;
                                //AIThread.Instance.RegisterAI(eh.AI);


                                map_.RegisterActor(mob);
                                mob.invisble = false;
                                mob.sightRange = 2500;
                                map_.OnActorVisibilityChange(mob);

                                if (OriMobSetting.Instance.Items.ContainsKey(mob.MobID))
                                {
                                    MobFactory.Instance.BossList.Add(mob);
                                }

                                List<ActorMob> lists = null;
                                if (mobs.ContainsKey(map))
                                    lists = mobs[map];
                                else
                                {
                                    lists = new List<ActorMob>();
                                    mobs.Add(map, lists);
                                }
                                lists.Add(mob);
                                total++;
                            }
                            break;
                    }
                }
                fs.Close();
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
            return total;
        }

        public int LoadAI(string f)
        {
            int total = 0;

            return total;
        }
        public void LoadSpawn(string path)
        {
            string[] file = SagaLib.VirtualFileSystem.VirtualFileSystemManager.Instance.FileSystem.SearchFile(path, "*.xml");
            int total = 0;
            foreach (string f in file)
            {
                total += LoadOne(f, 0);
            }
            Logger.ShowInfo(total.ToString() + " mobs spawned...");
        }

        public void LoadAnAI(string path)
        {
            string[] file = SagaLib.VirtualFileSystem.VirtualFileSystemManager.Instance.FileSystem.SearchFile(path, "*.xml");
            int total = 0;
            foreach (string f in file)
            {
                total += LoadAI(f);
            }
            Logger.ShowInfo(total.ToString() + " 加载新的AI...");
        }
    }
}

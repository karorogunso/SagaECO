using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

using SagaLib;
using SagaDB.Map;
using SagaDB.Actor;

namespace SagaMap.Manager
{

    public sealed class MapManager : Singleton<MapManager>
    {
        private Dictionary<uint, Map> maps;
        private Dictionary<uint, MapInfo> mapInfo;

        public int InstanceMapLifeHour = 4;

        public MapManager()
        {   
            this.maps = new Dictionary<uint, Map>();
            this.mapInfo = new Dictionary<uint, MapInfo>();            
        }

        public string GetMapName(uint mapID)
        {

            if (this.mapInfo.ContainsKey(mapID))
                return this.mapInfo[mapID].name;
            else
                return "MAP_NAME_NOT_FOUND";
        }

        public Dictionary<uint, Map> Maps
        {
            get
            {
                return this.maps;
            }
        }

        public Dictionary<uint, MapInfo> MapInfos
        {
            set
            {
                this.mapInfo = value;
            }
        }

        public uint GetMapId(string mapName)
        {
            foreach (KeyValuePair<uint, MapInfo> kv in mapInfo)
            {
                if (kv.Value.name.ToLower() == mapName.ToLower())//make the map name case insensitive
                    return kv.Key;
            }
            return 0xFFFFFFFF;
        }

        public void LoadMaps()
        {
            foreach (uint mapID in Configuration.Instance.HostedMaps)
            {
                if (this.mapInfo.ContainsKey(mapID))
                    if (!this.AddMap(new Map(this.mapInfo[mapID])))
                        Logger.ShowError("Cannot load map " + mapID, null);
            }
        }

        public uint CreateMapInstance(ActorPC creator, uint template, uint exitMap, byte exitX, byte exitY)
        {
            return CreateMapInstance(creator, template, exitMap, exitX, exitY, false);
        }

        public uint CreateMapInstance(ActorPC creator, uint template, uint exitMap, byte exitX, byte exitY, bool autoDispose)
        {
            return CreateMapInstance(creator, template, exitMap, exitX, exitY, false, 999);
        }
        public uint CreateMapInstance(ActorPC creator, uint template, uint exitMap, byte exitX, byte exitY, bool autoDispose, uint ResurrectionLimit)
        {
            return CreateMapInstance(creator, template, exitMap, exitX, exitY, false, 999,false);
        }
        public uint CreateMapInstance(ActorPC creator, uint template, uint exitMap, byte exitX, byte exitY, bool autoDispose, uint ResurrectionLimit,bool returnori)
        {
            if (!this.maps.ContainsKey(template))
                return 0;
            Map templateMap = this.maps[template];
            Map newMap = new Map(templateMap.Info);
            /*if(returnori)
            for (int i = 0; i < 999; i++)
            {
                if (!this.maps.ContainsKey((uint)(template * 100 + i)))
                {
                    newMap.ID = (uint)(template * 100 + i);
                    SagaLib.Logger.ShowInfo(newMap.ID.ToString());
                    break;
                }
            }
            else*/
            if (template == 70000000 || template == 75000000)
            {
                for (int i = (int)(template % 1000) + 1; i < 999; i++)
                {
                    if (!this.maps.ContainsKey((uint)((template / 1000 * 1000) + (template % 1000) + i)))
                    {
                        newMap.ID = (uint)((template / 1000 * 1000) + (template % 1000) + i);
                        break;
                    }
                }
            }
            else
            {
                for (int i = (int)(template % 1000) + 1; i < 999; i++)
                {
                    if (!this.maps.ContainsKey((uint)((template / 1000 * 1000) + (template % 1000) + i)))
                    {
                        newMap.ID = (uint)((template / 1000 * 1000) + (template % 1000) + i);
                        SagaLib.Logger.ShowInfo(newMap.ID.ToString() + "副本创建者：" + creator.Name);
                        break;
                    }
                }
            }

            newMap.IsMapInstance = true;
            newMap.ClientExitMap = exitMap;
            newMap.ClientExitX = exitX;
            newMap.ClientExitY = exitY;
            newMap.AutoDispose = autoDispose;
            newMap.Creator = creator;
            newMap.ResurrectionLimit = ResurrectionLimit;
            if (returnori)
            {
                newMap.returnori = true;
                
            }
            newMap.OriID = template;
            Configuration.Instance.HostedMaps.Add(newMap.ID);
            this.maps.Add(newMap.ID, newMap);
            return newMap.ID;
        }

        public uint CreateMapInstance(SagaDB.Ring.Ring ring, uint template, uint exitMap, byte exitX, byte exitY, bool autoDispose)
        {
            if (!this.maps.ContainsKey(template))
                return 0;
            Map templateMap = this.maps[template];
            Map newMap = new Map(templateMap.Info);
            for (int i = (int)(template % 1000) + 1; i < 999; i++)
            {
                if (!this.maps.ContainsKey((uint)((template / 1000 * 1000) + (template % 1000) + i)))
                {
                    newMap.ID = (uint)((template / 1000 * 1000) + (template % 1000) + i);
                    break;
                }
            }
            newMap.IsMapInstance = true;
            newMap.ClientExitMap = exitMap;
            newMap.ClientExitX = exitX;
            newMap.ClientExitY = exitY;
            newMap.AutoDispose = autoDispose;
            newMap.Ring = ring;
            Configuration.Instance.HostedMaps.Add(newMap.ID);
            this.maps.Add(newMap.ID, newMap);
            return newMap.ID;
        }
        public void CreateFFInstanceOfSer()
        {
            Map templateMap = this.maps[90001000];
            Map newMap = new Map(templateMap.Info);
            newMap.ID = 90001999;
            newMap.IsMapInstance = false;
            Configuration.Instance.HostedMaps.Add(newMap.ID);
            this.maps.Add(newMap.ID, newMap);

            templateMap = this.maps[91000000];
            newMap = new Map(templateMap.Info);
            newMap.ID = 91000999;
            newMap.IsMapInstance = false;
            Configuration.Instance.HostedMaps.Add(newMap.ID);
            this.maps.Add(newMap.ID, newMap);

            templateMap = this.maps[70000000];
            newMap = new Map(templateMap.Info);
            newMap.ID = 70000999;
            newMap.IsMapInstance = false;
            Configuration.Instance.HostedMaps.Add(newMap.ID);
            this.maps.Add(newMap.ID, newMap);
        }
        public void DisposeMapInstanceOnLogout(uint charID)
        {
            try
            {
                uint[] keys = new uint[maps.Count];
                maps.Keys.CopyTo(keys, 0);
                foreach (uint i in keys)
                {
                    if (maps.ContainsKey(i))
                    {
                        if (maps[i].AutoDispose && maps[i].IsMapInstance)
                        {
                            //临时救火，但会导致部分副本地图不会被回收！请尽快优化。
                            if (maps[i].Creator != null)
                                if (maps[i].Creator.CharID == charID)
                                    DeleteMapInstance(i);

                            /*if(maps[i].Creator == null)
                                DeleteMapInstance(i);
                            else if(!maps[i].Creator.Online)
                                DeleteMapInstance(i);
                            else if (maps[i].Creator.CharID == charID)
                                DeleteMapInstance(i);*/

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
        }

        public bool DeleteMapInstance(uint id)
        {
            if (!this.maps.ContainsKey(id))
                return false;
            Map map = this.maps[id];
            map.OnDestrory();
            this.maps.Remove(id);
            Configuration.Instance.HostedMaps.Remove(id);
            return true;
        }

        public bool AddMap(Map addMap)
        {
            foreach (Map map in this.maps.Values)
                if (addMap.ID == map.ID) return false;

            this.maps.Add(addMap.ID, addMap);
            return true;
        }

        public Map GetMap(uint mapID)
        {
            if (this.maps.ContainsKey(mapID))
            {
                return this.maps[mapID];
            }
            else
            {
                Logger.ShowDebug("Requesting unknown mapID:" + mapID.ToString(), Logger.CurrentLogger);
                return null;
            }
        }
    }
}

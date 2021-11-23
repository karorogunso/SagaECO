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
            newMap.Creator = creator;
            newMap.ResurrectionLimit = ResurrectionLimit;
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
            Map templateMap = this.maps[90000000];
            Map newMap = new Map(templateMap.Info);
            newMap.ID = 90000999;
            newMap.IsMapInstance = false;
            Configuration.Instance.HostedMaps.Add(newMap.ID);
            if (!this.maps.ContainsKey(newMap.ID))
                this.maps.Add(newMap.ID, newMap);

            templateMap = this.maps[91000000];
            newMap = new Map(templateMap.Info);
            newMap.ID = 91000999;
            newMap.IsMapInstance = false;
            Configuration.Instance.HostedMaps.Add(newMap.ID);
            if (!this.maps.ContainsKey(newMap.ID))
                this.maps.Add(newMap.ID, newMap);

            templateMap = this.maps[70000000];
            newMap = new Map(templateMap.Info);
            newMap.ID = 70000999;
            newMap.IsMapInstance = false;
            Configuration.Instance.HostedMaps.Add(newMap.ID);
            if (!this.maps.ContainsKey(newMap.ID))
                this.maps.Add(newMap.ID, newMap);
        }

        public void DisposeMapInstanceOnLogout(ActorPC creator)
        {
            uint[] keys = new uint[maps.Count];
            maps.Keys.CopyTo(keys, 0);
            foreach (uint i in keys)
            {
                if (maps[i].AutoDispose && maps[i].IsMapInstance && (maps[i].Creator.CharID == creator.CharID))
                {
                    DeleteMapInstance(i);
                }
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

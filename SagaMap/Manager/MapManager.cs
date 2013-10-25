using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

using SagaLib;

namespace SagaMap.Manager
{

    public struct MapInfo
    {
        public uint id;
        public string name;       
    }
    public sealed class MapManager : Singleton<MapManager>
    {
        private Dictionary<uint, Map> maps;
        private Dictionary<uint, MapInfo> mapInfo;


        public MapManager()
        {
            this.maps = new Dictionary<uint, Map>();
            this.mapInfo = new Dictionary<uint, MapInfo>();
            MapInfo info = new MapInfo();
            info.id = 0;
            info.name = "test";
            Map map = new Map(info);
            mapInfo.Add(0, info);
            maps.Add(0, map);
        }

        public string GetMapName(uint mapID)
        {

            if (this.mapInfo.ContainsKey(mapID))
                return this.mapInfo[mapID].name;
            else
                return "MAP_NAME_NOT_FOUND";
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

        public void LoadMaps(List<uint> maps)
        {
            foreach (uint mapID in maps)
            {
                if (this.mapInfo.ContainsKey(mapID))
                    if (!this.AddMap(new Map(this.mapInfo[mapID])))
                        Logger.ShowError("Cannot load map " + mapID, null);
            }
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

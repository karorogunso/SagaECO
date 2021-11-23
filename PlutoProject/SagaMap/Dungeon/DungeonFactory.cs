using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaMap.Manager;

namespace SagaMap.Dungeon
{
    public class DungeonFactory : Factory<DungeonFactory, Dungeon>
    {
        private Dictionary<uint, Dungeon> dungeons = new Dictionary<uint, Dungeon>();
        int count = 0;
        public DungeonFactory()
        {
            this.loadingTab = "Loading Dungeon database";
            this.loadedTab = " dungeons loaded.";
            this.databaseName = "dungeon";
            this.FactoryType = FactoryType.XML;
        }

        public Dungeon GetDungeon(uint id)
        {
            if (dungeons.ContainsKey(id))
                return dungeons[id];
            else
                return null;
        }

        public void RemoveDungeon(uint id)
        {
            if (dungeons.ContainsKey(id))
                dungeons.Remove(id);
        }

        protected override uint GetKey(Dungeon item)
        {
            return item.ID;
        }

        protected override void ParseCSV(Dungeon item, string[] paras)
        {
            throw new NotImplementedException();
        }

        protected override void ParseXML(System.Xml.XmlElement root, System.Xml.XmlElement current, Dungeon item)
        {
            switch (root.Name.ToLower())
            {
                case "dungeon":
                    switch (current.Name.ToLower())
                    {
                        case "id":
                            item.ID = uint.Parse(current.InnerText);
                            break;
                        case "timelimit":
                            item.TimeLimit = int.Parse(current.InnerText);
                            break;
                        case "theme":
                            item.Theme = (Theme)Enum.Parse(typeof(Theme), current.InnerText);
                            break;
                        case "startmap":
                            item.StartMap = uint.Parse(current.InnerText);
                            break;
                        case "endmap":
                            item.EndMap = uint.Parse(current.InnerText);
                            break;
                        case "maxroomcount":
                            item.MaxRoomCount = int.Parse(current.InnerText);
                            break;
                        case "maxcrosscount":
                            item.MaxCrossCount = int.Parse(current.InnerText);
                            break;
                        case "maxfloorcount":
                            item.MaxFloorCount = int.Parse(current.InnerText);
                            break;
                        case "spawnfile":
                            item.SpawnFile = current.InnerText;
                            break;
                    }
                    break;
            }
        }

        public Dungeon CreateDungeon(uint id, ActorPC creator, uint exitMap, byte exitX, byte exitY)
        {
            recreate:
            if (items.ContainsKey(id))
            {
                count++;
                Dungeon dungeon = items[id].Clone();
                dungeon.Creator = creator;
                dungeon.DungeonID = (uint)count;
                List<DungeonMap> rooms;
                List<DungeonMap> cross;
                List<DungeonMap> floors;
                DungeonMap[,] mapping = new DungeonMap[20, 20];
                List<DungeonMap> openMaps = new List<DungeonMap>();
                int roomcount = dungeon.MaxRoomCount;
                int crosscount = dungeon.MaxCrossCount;
                int floorcount = dungeon.MaxFloorCount;
                int endcount = 1;

                var maps =
                    from m in DungeonMapsFactory.Instance.Items.Values
                    where m.Theme == dungeon.Theme && m.MapType == MapType.Room
                    select m;
                rooms = maps.ToList();
                maps =
                    from m in DungeonMapsFactory.Instance.Items.Values
                    where m.Theme == dungeon.Theme && m.MapType == MapType.Cross
                    select m;
                cross = maps.ToList();
                maps =
                    from m in DungeonMapsFactory.Instance.Items.Values
                    where m.Theme == dungeon.Theme && m.MapType == MapType.Floor
                    select m;
                floors = maps.ToList();

                byte x, y;
                int times;
                x = (byte)Global.Random.Next(0, 19);
                y = (byte)Global.Random.Next(0, 19);

                //creating entrance
                dungeon.Start = DungeonMapsFactory.Instance.Items[dungeon.StartMap].Clone();
                dungeon.Start.Map = MapManager.Instance.GetMap(MapManager.Instance.CreateMapInstance(creator, dungeon.StartMap, exitMap, exitX, exitY));
                dungeon.Start.Map.IsDungeon = true;
                dungeon.Start.Map.DungeonMap = dungeon.Start;
                dungeon.Start.X = x;
                dungeon.Start.Y = y;
                dungeon.Maps.Add(dungeon.Start);

                mapping[x, y] = dungeon.Start;
                openMaps.Add(dungeon.Start);

                //generating the rest of maps
                while (roomcount > 0 || floorcount > 0 || crosscount > 0 || endcount > 0)
                {
                    if (openMaps.Count == 0)
                    {
                        Logger.ShowWarning("Dungeon(" + id.ToString() + "): All nodes closed, but still rooms remaining, Recreating....");
                        dungeon.Destory(DestroyType.QuestCancel);

                        goto recreate;
                    }
                    DungeonMap source = openMaps[Global.Random.Next(0, openMaps.Count - 1)];

                    //determinate the exit of the source
                    List<GateType> gates = new List<GateType>();
                    GateType nowDir, dstDir;
                    if (source.Gates.ContainsKey(GateType.North))
                        if (source.Gates[GateType.North].ConnectedMap == null)
                            if (source.GetXForGate(GateType.North) < 20 && source.GetYForGate(GateType.North) < 20)
                                if (mapping[source.GetXForGate(GateType.North), source.GetYForGate(GateType.North)] == null)
                                    gates.Add(GateType.North);
                    if (source.Gates.ContainsKey(GateType.East))
                        if (source.Gates[GateType.East].ConnectedMap == null)
                            if (source.GetXForGate(GateType.East) < 20 && source.GetYForGate(GateType.East) < 20)
                                if (mapping[source.GetXForGate(GateType.East), source.GetYForGate(GateType.East)] == null)
                                    gates.Add(GateType.East);
                    if (source.Gates.ContainsKey(GateType.South))
                        if (source.Gates[GateType.South].ConnectedMap == null)
                            if (source.GetXForGate(GateType.South) < 20 && source.GetYForGate(GateType.South) < 20)
                                if (mapping[source.GetXForGate(GateType.South), source.GetYForGate(GateType.South)] == null)
                                    gates.Add(GateType.South);
                    if (source.Gates.ContainsKey(GateType.West))
                        if (source.Gates[GateType.West].ConnectedMap == null)
                            if (source.GetXForGate(GateType.West) < 20 && source.GetYForGate(GateType.West) < 20)
                                if (mapping[source.GetXForGate(GateType.West), source.GetYForGate(GateType.West)] == null)
                                    gates.Add(GateType.West);

                    if (gates.Count == 0)
                    {
                        openMaps.Remove(source);
                        continue;
                    }

                    if (gates.Count > 1)
                        nowDir = gates[Global.Random.Next(0, gates.Count - 1)];
                    else
                        nowDir = gates[0];
                    dstDir = GateType.North;
                    switch (nowDir)
                    {
                        case GateType.North:
                            dstDir = GateType.South;
                            break;
                        case GateType.East:
                            dstDir = GateType.West;
                            break;
                        case GateType.South:
                            dstDir = GateType.North;
                            break;
                        case GateType.West:
                            dstDir = GateType.East;
                            break;
                    }

                    //choose a new map type
                    List<MapType> types = new List<MapType>();
                    if (roomcount > 0)
                        types.Add(MapType.Room);
                    if (crosscount > 0)
                        types.Add(MapType.Cross);
                    if (floorcount > 0)
                        types.Add(MapType.Floor);
                    MapType nowType;
                    if (types.Count > 0)
                        if (types.Count == 1)
                            nowType = types[0];
                        else
                            nowType = types[Global.Random.Next(0, types.Count - 1)];
                    else
                        nowType = MapType.End;
                    DungeonMap nowMap = null;
                    bool ifEnd = false;
                    switch (nowType)
                    {
                        case MapType.Room:
                            nowMap = rooms[Global.Random.Next(0, rooms.Count - 1)].Clone();
                            roomcount--;
                            break;
                        case MapType.Floor:
                            nowMap = floors[Global.Random.Next(0, floors.Count - 1)].Clone();
                            floorcount--;
                            break;
                        case MapType.Cross:
                            nowMap = cross[Global.Random.Next(0, cross.Count - 1)].Clone();
                            crosscount--;
                            break;
                        case MapType.End:
                            nowMap = DungeonMapsFactory.Instance.Items[dungeon.EndMap].Clone();
                            dungeon.End = nowMap;
                            ifEnd = true;
                            endcount--;
                            break;
                    }
                    nowMap.X = source.GetXForGate(nowDir);
                    nowMap.Y = source.GetYForGate(nowDir);
                    nowMap.Map = MapManager.Instance.GetMap(MapManager.Instance.CreateMapInstance(creator, nowMap.ID, exitMap, exitX, exitY));
                    nowMap.Map.IsDungeon = true;
                    nowMap.Map.DungeonMap = nowMap;
                    if (ifEnd)
                        Mob.MobSpawnManager.Instance.LoadOne(dungeon.SpawnFile, nowMap.Map.ID, false, true);
                    else
                        Mob.MobSpawnManager.Instance.LoadOne(dungeon.SpawnFile, nowMap.Map.ID, true, false);
                    dungeon.Maps.Add(nowMap);
                    mapping[nowMap.X, nowMap.Y] = nowMap;

                    times = Global.Random.Next(1, 3);
                    for (int i = 0; i < times; i++)
                        nowMap.Rotate();
                    times = 0;
                    while (!nowMap.Gates.ContainsKey(dstDir))
                    {
                        nowMap.Rotate();
                        times++;
                        if (times > 3)
                            break;
                    }
                    if (!nowMap.Gates.ContainsKey(dstDir))
                        continue;
                    source.Gates[nowDir].ConnectedMap = nowMap;
                    source.Gates[nowDir].Direction = Direction.In;
                    nowMap.Gates[dstDir].ConnectedMap = source;
                    nowMap.Gates[dstDir].Direction = Direction.Out;

                    if (source.FreeGates == 0 || source == dungeon.Start)
                        openMaps.Remove(source);
                    if (nowMap.FreeGates > 0)
                        openMaps.Add(nowMap);
                }

                creator.DungeonID = dungeon.DungeonID;

                if (creator.Party != null)
                {
                    foreach (ActorPC i in creator.Party.Members.Values)
                    {
                        if (i.Online)
                        {
                            ActorEventHandlers.PCEventHandler eh = (SagaMap.ActorEventHandlers.PCEventHandler)i.e;
                            eh.Client.SendSystemMessage(creator.Name + LocalManager.Instance.Strings.ITD_CREATED);
                        }
                    }
                }
                else
                {
                    ActorEventHandlers.PCEventHandler eh = (SagaMap.ActorEventHandlers.PCEventHandler)creator.e;
                    eh.Client.SendSystemMessage(creator.Name + LocalManager.Instance.Strings.ITD_CREATED);
                }

                dungeon.DestroyTask.Activate();
                dungeons.Add(dungeon.DungeonID, dungeon);
                return dungeon;
            }
            else
                return null;
        }
    }
}

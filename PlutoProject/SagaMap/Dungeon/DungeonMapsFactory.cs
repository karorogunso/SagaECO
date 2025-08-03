using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;

namespace SagaMap.Dungeon
{
    public class DungeonMapsFactory : Factory<DungeonMapsFactory, DungeonMap>
    {
        public DungeonMapsFactory()
        {
            this.loadingTab = "Loading Dungeon Map database";
            this.loadedTab = " dungeon maps loaded.";
            this.databaseName = "dungeon map";
            this.FactoryType = FactoryType.XML;
        }

        protected override uint GetKey(DungeonMap item)
        {
            return item.ID;
        }

        protected override void ParseCSV(DungeonMap item, string[] paras)
        {
            throw new NotImplementedException();
        }

        protected override void ParseXML(System.Xml.XmlElement root, System.Xml.XmlElement current, DungeonMap item)
        {
            switch (root.Name.ToLower())
            {
                case "map":
                    switch (current.Name.ToLower())
                    {
                        case "id":
                            item.ID = uint.Parse(current.InnerText);
                            break;
                        case "type":
                            item.MapType = (MapType)Enum.Parse(typeof(MapType), current.InnerText);
                            break;
                        case "theme":
                            item.Theme = (Theme)Enum.Parse(typeof(Theme), current.InnerText);
                            break;
                        case "gate":
                            GateType type = (GateType)Enum.Parse(typeof(GateType), current.GetAttribute("type"));
                            byte x = byte.Parse(current.GetAttribute("x"));
                            byte y = byte.Parse(current.GetAttribute("y"));
                            uint npcID = uint.Parse(current.InnerText);
                            if (!item.Gates.ContainsKey(type))
                            {
                                DungeonGate gate = new DungeonGate();
                                gate.GateType = type;
                                gate.X = x;
                                gate.Y = y;
                                gate.NPCID = npcID;
                                item.Gates.Add(type, gate);
                            }
                            break;
                    }
                    break;
            }
        }
    }
}

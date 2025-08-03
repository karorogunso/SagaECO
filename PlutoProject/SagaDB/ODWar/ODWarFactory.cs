using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

using SagaLib;
using SagaDB.Actor;

namespace SagaDB.ODWar
{
    public class ODWarFactory : Factory<ODWarFactory, ODWar>
    {
        public ODWarFactory()
        {
            this.loadingTab = "Loading ODWar database";
            this.loadedTab = " OD War loaded.";
            this.databaseName = "OD War";
            this.FactoryType = FactoryType.XML;
        }
       
        protected override uint GetKey(ODWar item)
        {
            return item.MapID;
        }

        protected override void ParseCSV(ODWar item, string[] paras)
        {
            throw new NotImplementedException();
        }

        protected override void ParseXML(XmlElement root, XmlElement current, ODWar item)
        {
            switch (root.Name.ToLower())
            {
                case "odwar":
                    switch (current.Name.ToLower())
                    {
                       case "map":
                            item.MapID = uint.Parse(current.InnerText);
                            break;
                        case "symboltrash":
                            item.SymbolTrash = uint.Parse(current.InnerText);
                            break;
                        case "symbol":
                            ODWar.Symbol symbol = new ODWar.Symbol();
                            symbol.id = int.Parse(current.GetAttribute("id"));
                            symbol.x = byte.Parse(current.GetAttribute("x"));
                            symbol.y = byte.Parse(current.GetAttribute("y"));
                            symbol.mobID = uint.Parse(current.InnerText);
                            item.Symbols.Add(symbol.id, symbol);
                            break;
                    }
                    break;
                case "schedules":
                    switch (current.Name.ToLower())
                    {
                        case "schedule":
                            int time = int.Parse(current.GetAttribute("time"));
                            int day = int.Parse(current.InnerText);
                            if (!item.StartTime.ContainsKey(day))
                                item.StartTime.Add(day, time);
                            break;
                    }
                    break;
                case "demchamp":
                    switch (current.Name.ToLower())
                    {
                        case "mob":
                            item.DEMChamp.Add(uint.Parse(current.InnerText));
                            break;
                    }
                    break;
                case "dem":
                    switch (current.Name.ToLower())
                    {
                        case "mob":
                            item.DEMNormal.Add(uint.Parse(current.InnerText));
                            break;
                    }
                    break;
                case "boss":
                    switch (current.Name.ToLower())
                    {
                        case "mob":
                            item.Boss.Add(uint.Parse(current.InnerText));
                            break;
                    }
                    break;
                case "wavestrong":
                    if (item.WaveStrong == null)
                        item.WaveStrong = new ODWar.Wave();
                    switch (current.Name.ToLower())
                    {
                        case "demchamp":
                            item.WaveStrong.DEMChamp = int.Parse(current.InnerText);
                            break;
                        case "dem":
                            item.WaveStrong.DEMNormal = int.Parse(current.InnerText);
                            break;
                    }
                    break;
                case "waveweak":
                    if (item.WaveWeak == null)
                        item.WaveWeak = new ODWar.Wave();
                    switch (current.Name.ToLower())
                    {
                        case "demchamp":
                            item.WaveWeak.DEMChamp = int.Parse(current.InnerText);
                            break;
                        case "dem":
                            item.WaveWeak.DEMNormal = int.Parse(current.InnerText);
                            break;
                    }
                    break;
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using SagaDB.Actor;
using SagaDB.Item;
using SagaDB.Party;
using SagaLib;
using SagaLib.VirtualFileSystem;
using SagaMap.Network.Client;
using SagaMap.Scripting;
using SagaMap.Manager;
using SagaDB.FFarden;

namespace SagaMap.Manager
{
    public class CustomMapManager : Singleton<CustomMapManager>
    {
        public SagaDB.Server.Server ser;
        ActorFurniture House;
        public CustomMapManager()
        {
            ser = new SagaDB.Server.Server();
            MapServer.charDB.GetSerFFurniture(ser);
        }

        public void CreateFF()
        {
            SagaMap.Manager.MapManager.Instance.CreateFFInstanceOfSer();
            Map map = MapManager.Instance.GetMap(90001999);
            foreach (ActorFurniture i in ser.Furnitures[FurniturePlace.GARDEN])
            {
                i.e = new ActorEventHandlers.NullEventHandler();
                map.RegisterActor(i);
                i.invisble = false;
            }
            foreach (ActorFurniture i in ser.Furnitures[FurniturePlace.HOUSE])
            {
                House = i;
                i.e = new ActorEventHandlers.NullEventHandler();
                map.RegisterActor(i);
                i.invisble = false;
            }
            map = MapManager.Instance.GetMap(91000999);
            foreach (ActorFurniture i in ser.Furnitures[FurniturePlace.ROOM])
            {
                i.e = new ActorEventHandlers.NullEventHandler();
                map.RegisterActor(i);
                i.invisble = false;
            }
        }
        public void EnterFFOnMapLoaded(MapClient client)
        {
            if (client.map.ID == 91000999 || client.map.ID == 10054000)
            {
                int sky = ScriptManager.Instance.VariableHolder.AInt["服務器FF背景"];
                int weather = ScriptManager.Instance.VariableHolder.AInt["服務器FF天氣"];
                Packets.Server.SSMG_FG_CHANGE_SKY p = new Packets.Server.SSMG_FG_CHANGE_SKY();
                p.Sky = (byte)sky;
                client.netIO.SendPacket(p);

                Packets.Server.SSMG_FG_CHANGE_WEATHER p2 = new Packets.Server.SSMG_FG_CHANGE_WEATHER();
                p2.Weather = (byte)weather;
                client.netIO.SendPacket(p2);
            }
            else if(client.map.OriID == 70000000 && client.map.Creator != null)
            {
                int sky = client.map.Creator.CInt["服務器FF背景"];
                int weather = client.map.Creator.CInt["服務器FF天氣"];
                Packets.Server.SSMG_FG_CHANGE_SKY p = new Packets.Server.SSMG_FG_CHANGE_SKY();
                p.Sky = (byte)sky;
                client.netIO.SendPacket(p);

                Packets.Server.SSMG_FG_CHANGE_WEATHER p2 = new Packets.Server.SSMG_FG_CHANGE_WEATHER();
                p2.Weather = (byte)weather;
                client.netIO.SendPacket(p2);
            }

        }
        public void SerFFRoomEnter(MapClient client)
        {
            Map map = MapManager.Instance.GetMap(91000999);
            client.Map.SendActorToMap(client.Character, 91000999, Global.PosX8to16(20,map.Width),Global.PosY8to16(37,map.Height), true);
        }
        public void SendGotoSerFFMap(MapClient client)
        {
            Packets.Server.SSMG_FF_ENTER p = new Packets.Server.SSMG_FF_ENTER();
            p.MapID = client.Character.MapID;
            p.X = Global.PosX16to8(client.Character.X, client.map.Width);
            p.Y = Global.PosY16to8(client.Character.Y, client.map.Height);
            p.Dir = (byte)(client.Character.Dir / 45);
            p.RingHouseID = 30250001;
            if (client.Character.Account.GMLevel >= 100 && client.Character.Ring != null)
                p.RingID = client.Character.Ring.ID;
            p.HouseX = 0xF6DC;
            p.HouseY = 0xFD34;
            p.HouseDir = 0xB6;
            client.netIO.SendPacket(p);
        }
        public void SerFFFurnitureCastleSetup(MapClient client, Packets.Client.CSMG_FF_CASTLE_SETUP p)
        {
            if (client.Character.Account.GMLevel < 100)
            {
                client.SendSystemMessage("您的權限不足");
                return;
            }
            else
            {
                if (ser.Furnitures[FurniturePlace.HOUSE].Count > 0)
                {
                    client.SendSystemMessage("无法重复设置");
                    return;
                }
                SagaDB.Item.Item item = client.Character.Inventory.GetItem(p.InventorySlot);
                House = new ActorFurnitureUnit();

                client.DeleteItem(p.InventorySlot, 1, false);

                House.MapID = client.Character.MapID;
                House.ItemID = item.ItemID;
                Map map = MapManager.Instance.GetMap(House.MapID);
                House.X = p.X;
                House.Z = p.Z;
                House.Yaxis = p.Yaxis;
                House.Name = item.BaseData.name;
                House.PictID = item.PictID;
                House.e = new ActorEventHandlers.NullEventHandler();
                map.RegisterActor(House);
                House.invisble = false;
                map.OnActorVisibilityChange(House);
                ser.Furnitures[FurniturePlace.HOUSE].Add(House);
                MapServer.charDB.SaveSerFF(ser);
            }
        }
        public void SerFFofFurnitureSetup(MapClient client,Packets.Client.CSMG_FF_FURNITURE_SETUP p)
        {
            if(client.Character.Account.GMLevel < 100)
            {
                client.SendSystemMessage("您的權限不足");
                return;
            }
            else
            {
                SagaDB.Item.Item item = client.Character.Inventory.GetItem(p.InventorySlot);
                ActorFurniture actor = new ActorFurniture();
                if (client.Character.Account.GMLevel < 100)
                client.DeleteItem(p.InventorySlot, 1, false);
                actor.MapID = client.Character.MapID;
                actor.ItemID = item.ItemID;
                Map map = MapManager.Instance.GetMap(actor.MapID);
                actor.X = p.X;
                actor.Y = p.Y;
                actor.Z = p.Z;
                actor.Xaxis = p.Xaxis;
                actor.Yaxis = p.Yaxis;
                actor.Zaxis = p.Zaxis;
                actor.Name = item.BaseData.name;
                actor.PictID = item.PictID;
                actor.e = new ActorEventHandlers.NullEventHandler();
                map.RegisterActor(actor);
                actor.invisble = false;
                map.OnActorVisibilityChange(actor);

                if (client.Character.MapID == 90001999)
                    ser.Furnitures[FurniturePlace.GARDEN].Add(actor);
                else if(client.Character.MapID == 91000999)
                    ser.Furnitures[FurniturePlace.ROOM].Add(actor);
                client.SendSystemMessage(string.Format(LocalManager.Instance.Strings.FG_FUTNITURE_SETUP, actor.Name, (ser.Furnitures[FurniturePlace.GARDEN].Count +
    ser.Furnitures[FurniturePlace.ROOM].Count), Configuration.Instance.MaxFurnitureCount));

                MapServer.charDB.SaveSerFF(ser);
            }
        }
        public void RemoveFurnitureCastle(MapClient client, Packets.Client.CSMG_FF_FURNITURE_REMOVE_CASTLE p)
        {
            if (client.Character.Account.GMLevel < 100)
            {
                client.SendSystemMessage("您的權限不足");
                return;
            }
            else
            {
                Map map = null;
                map = MapManager.Instance.GetMap(90001999);
                ser.Furnitures[FurniturePlace.HOUSE].Clear();
                map.DeleteActor(House);
                MapServer.charDB.SaveSerFF(ser);
                SagaDB.Item.Item item = ItemFactory.Instance.GetItem(p.ItemID);
                client.AddItem(item, false);
            }
        }
        public void RemoveFurniture(MapClient client, Packets.Client.CSMG_FF_FURNITURE_REMOVE p)
        {
            if (client.Character.Account.GMLevel < 250)
            {
                client.SendSystemMessage("哎哟——！");
                return;
            }
            else
            {
                Map map = null;
                map = MapManager.Instance.GetMap(90001999);
                if (client.Character.MapID == 91000999)
                    map = MapManager.Instance.GetMap(91000999);
                Actor actor = map.GetActor(p.ActorID);
                if (actor == null)
                    return;
                if (actor.type != ActorType.FURNITURE)
                    return;
                ActorFurniture furniture = (ActorFurniture)actor;

                if (client.Character.MapID == 90001999)
                {
                    ser.Furnitures[FurniturePlace.GARDEN].Remove(furniture);
                }
                else if (client.Character.MapID == 91000999)
                    ser.Furnitures[FurniturePlace.ROOM].Remove(furniture);
                map.DeleteActor(actor);
                SagaDB.Item.Item item = ItemFactory.Instance.GetItem(furniture.ItemID);
                item.PictID = furniture.PictID;
                MapServer.charDB.SaveSerFF(ser);
                client.AddItem(item, false);
                client.SendSystemMessage(string.Format(LocalManager.Instance.Strings.FG_FUTNITURE_REMOVE, furniture.Name, (ser.Furnitures[FurniturePlace.GARDEN].Count +
                        ser.Furnitures[FurniturePlace.ROOM].Count), Configuration.Instance.MaxFurnitureCount));
            }
        }
    }
}

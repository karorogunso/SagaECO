using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net;
using System.Net.Sockets;

using SagaDB;
using SagaDB.Item;
using SagaDB.Actor;
using SagaDB.FFarden;
using SagaLib;
using SagaMap;
using SagaMap.Manager;


namespace SagaMap.Network.Client
{
    public partial class MapClient
    {
        public void OnFFardenFurnitureUse(Packets.Client.CSMG_FF_FURNITURE_USE p)
        {
            Map map = MapManager.Instance.GetMap(this.Character.MapID);
            Actor actor = map.GetActor(p.ActorID);
            if (actor == null)
                return;
            if (actor.type != ActorType.FURNITURE)
                return;
            ActorFurniture furniture = (ActorFurniture)actor;
            if (furniture.Motion == 111)
                furniture.Motion = 622;
            else furniture.Motion = 111;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.MOTION, null, furniture, false);

            /*if(furniture.ItemID == 31164100)
           {
               byte level = 1;
               if (Character.CInt["料理EXP"] > 5000)
                   level = 2;
               if (Character.CInt["料理EXP"] > 30000)
                   level =3;
               if (Character.CInt["料理EXP"] > 150000)
                   level = 4;
               if (Character.CInt["料理EXP"] > 500000)
                   level = 5;
               Scripting.SkillEvent.Instance.Synthese(Character, 2009, level);
               SendSystemMessage("当前料理经验：" + Character.CInt["料理EXP"].ToString() + " 等级：" + level.ToString());

           }
           //Item item = ItemFactory.Instance.GetItem(furniture.ItemID);
          if (item.BaseData.eventID != 0)
           {
               EventActivate(item.BaseData.eventID);
           }*/
        }
        public void OnFFardenOtherJoin(Packets.Client.CSMG_FFGARDEN_JOIN_OTHER p)
        {
            uint ringID = MapServer.charDB.GetFFRindID(p.ff_id);
            SagaDB.Ring.Ring ring = SagaMap.Manager.RingManager.Instance.GetRing(ringID);
            if (ring == null || ring.FFarden == null)
            {
                this.SendSystemMessage("错误，当前工会没有飞空城！");
                return;
            }
            this.OnFFardenJoin(ringID);
        }
        public void OnFFardenJoin(Packets.Client.CSMG_FFGARDEN_JOIN p)
        {
            if (this.Character.Ring == null || this.Character.Ring.FFarden == null)
            {
                this.SendSystemMessage("错误，当前工会没有飞空城！");
                return;
            }
            this.OnFFardenJoin(this.Character.Ring.ID);
        }
        public void OnFFardenJoin(uint ringid)
        {
            
            SagaDB.Ring.Ring ring = SagaMap.Manager.RingManager.Instance.GetRing(ringid);
            if (ring == null || ring.FFarden == null)
            {
                this.SendSystemMessage("错误，当前工会没有飞空城！");
                return;
            }
            MapServer.charDB.GetFFurniture(ring);
            if (ring.FFarden.MapID == 0)
            {
                Map map = MapManager.Instance.GetMap(this.Character.MapID);
                ring.FFarden.MapID = MapManager.Instance.CreateMapInstance(ring, 90001000, this.Character.MapID, Global.PosX16to8(this.Character.X, map.Width), Global.PosY16to8(this.Character.Y, map.Height), false);
                map = MapManager.Instance.GetMap(ring.FFarden.MapID);
                Map ffmap = MapManager.Instance.GetMap(ring.FFarden.MapID);
                List<uint> aa = new List<uint>();
                foreach (KeyValuePair<uint, Actor> y in ffmap.Actors)
                {
                    if (y.Value.type == ActorType.FURNITURE)
                        aa.Add(y.Key);
                }
                foreach (uint i in aa)
                {
                    ffmap.DeleteActor(ffmap.Actors[i]);
                }
                foreach (ActorFurniture i in ring.FFarden.Furnitures[FurniturePlace.GARDEN])
                {
                    i.e = new ActorEventHandlers.NullEventHandler();
                    map.RegisterActor(i);
                    i.invisble = false; ;
                }
                foreach (ActorFurniture i in ring.FFarden.Furnitures[FurniturePlace.FARM])
                {
                    i.e = new ActorEventHandlers.NullEventHandler();
                    map.RegisterActor(i);
                    i.invisble = false; ;
                }
                foreach (ActorFurniture i in ring.FFarden.Furnitures[FurniturePlace.FISHERY])
                {
                    i.e = new ActorEventHandlers.NullEventHandler();
                    map.RegisterActor(i);
                    i.invisble = false; ;
                }
                foreach (ActorFurniture i in ring.FFarden.Furnitures[FurniturePlace.HOUSE])
                {
                    i.e = new ActorEventHandlers.NullEventHandler();
                    map.RegisterActor(i);
                    i.invisble = false; ;
                }
            }
            this.Character.BattleStatus = 0;
            this.Character.Speed = 200;
            MapClient.FromActorPC(this.Character).SendChangeStatus();
            
            if (Configuration.Instance.HostedMaps.Contains(this.Character.MapID))
            {
                Map newMap = MapManager.Instance.GetMap(this.Character.MapID);
                if (this.Character.Marionette != null)
                    this.MarionetteDeactivate();
                this.Map.SendActorToMap(this.Character, ring.FFarden.MapID, 20, 20, true);
            }

            /*Packet p = new Packet();
            string args = "20 44 00 00 00 01 00 00 00 00 F6 31 FF 94 00 64";
            byte[] buf = Conversions.HexStr2Bytes(args.Replace(" ", ""));
            p = new Packet();
            p.data = buf;
            this.netIO.SendPacket(p);*/
        }
        public void OnFFurnitureSetup(Packets.Client.CSMG_FF_FURNITURE_SETUP p)
        {
            if (this.Character.MapID == 90001999 || this.Character.MapID == 91000999)// 家具設定
            {
                if (this.Character.Account.GMLevel < 250)
                {
                    this.SendSystemMessage("哎哟——？");
                    return;
                }
                else
                {
                    CustomMapManager.Instance.SerFFofFurnitureSetup(this, p);
                }
            }
            if (this.Character.Ring == null || this.Character.Ring.FFarden == null)
                return;
            SagaDB.Ring.Ring ring = SagaMap.Manager.RingManager.Instance.GetRing(this.Character.Ring.ID);
            if (this.Character.MapID != ring.FFarden.MapID && this.Character.MapID != ring.FFarden.RoomMapID)
                return;
            if ((ring.FFarden.Furnitures[FurniturePlace.GARDEN].Count +
                ring.FFarden.Furnitures[FurniturePlace.ROOM].Count) < Configuration.Instance.MaxFurnitureCount)
            {
                Item item = this.Character.Inventory.GetItem(p.InventorySlot);
                ActorFurniture actor = new ActorFurniture();

                if (this.Character.Account.GMLevel < 100)
                DeleteItem(p.InventorySlot, 1, false);

                actor.MapID = this.Character.MapID;
                actor.ItemID = item.ItemID;
                Map map = MapManager.Instance.GetMap(actor.MapID);
                actor.X = p.X;
                actor.Y = p.Y;
                actor.Z = p.Z;
                actor.Xaxis = p.Xaxis;
                actor.Yaxis = p.Yaxis;
                actor.Zaxis = p.Zaxis;
                actor.Name = "【" + this.Character.Name + "】" + item.BaseData.name;
                actor.PictID = item.PictID;
                actor.e = new ActorEventHandlers.NullEventHandler();
                map.RegisterActor(actor);
                actor.invisble = false;
                map.OnActorVisibilityChange(actor);

                if (this.Character.MapID == this.Character.Ring.FFarden.MapID)
                    ring.FFarden.Furnitures[FurniturePlace.GARDEN].Add(actor);
                else
                    ring.FFarden.Furnitures[FurniturePlace.ROOM].Add(actor);
                SendSystemMessage(string.Format(LocalManager.Instance.Strings.FG_FUTNITURE_SETUP, actor.Name, (ring.FFarden.Furnitures[FurniturePlace.GARDEN].Count +
                    ring.FFarden.Furnitures[FurniturePlace.ROOM].Count), Configuration.Instance.MaxFurnitureCount));
                MapServer.charDB.SaveFF(ring);
            }
            else
            {
                SendSystemMessage(LocalManager.Instance.Strings.FG_FUTNITURE_MAX);
            }

        }
        public void OnFFFurnitureRemoveCastle(Packets.Client.CSMG_FF_FURNITURE_REMOVE_CASTLE p)
        {
            if (this.Character.MapID == 90001999)
            {
                CustomMapManager.Instance.RemoveFurnitureCastle(this, p);
                return;
            }
        }

        public void OnFFFurnitureReset(Packets.Client.CSMG_FF_FURNITURE_RESET p)
        {
            uint actorID = p.ActorID;
            Map map = MapManager.Instance.GetMap(this.Character.MapID);
            Actor actor = map.GetActor(p.ActorID);
            if (actor == null)
                return;
            if (actor.type != ActorType.FURNITURE)
                return;
            ActorFurniture furniture = (ActorFurniture)actor;
            Packets.Server.SSMG_FF_FURNITURE_RESET p2 = new Packets.Server.SSMG_FF_FURNITURE_RESET();
            p2.AID = 1;
            p2.ActorID = furniture.ActorID;
            p2.RindID = this.Character.ActorID;
            this.netIO.SendPacket(p2);
        }
        public void OnFFFurnitureRemove(Packets.Client.CSMG_FF_FURNITURE_REMOVE p)
        {
            if (this.Character.MapID == 90001999 || this.Character.MapID == 91000999)
            {
                CustomMapManager.Instance.RemoveFurniture(this,p);
                return;
            }
            if (this.Character.Ring.FFarden == null)
                return;
            if (this.Character.MapID != this.Character.Ring.FFarden.MapID && this.Character.MapID != this.Character.Ring.FFarden.RoomMapID)
                return;
            SagaDB.Ring.Ring ring = SagaMap.Manager.RingManager.Instance.GetRing(this.Character.Ring.ID);
            Map map = null;
            map = MapManager.Instance.GetMap(this.Character.MapID);
            Actor actor = map.GetActor(p.ActorID);
            if (actor == null)
                return;
            if (actor.type != ActorType.FURNITURE)
                return;
            ActorFurniture furniture = (ActorFurniture)actor;

            if (this.Character.MapID == this.Character.Ring.FFarden.MapID)
            {
                ring.FFarden.Furnitures[FurniturePlace.GARDEN].Remove(furniture);
            }
            else
                ring.FFarden.Furnitures[FurniturePlace.ROOM].Remove(furniture);
            map.DeleteActor(actor);
            Item item = ItemFactory.Instance.GetItem(furniture.ItemID);
            item.PictID = furniture.PictID;
            MapServer.charDB.SaveFF(ring);
            AddItem(item, false);
            SendSystemMessage(string.Format(LocalManager.Instance.Strings.FG_FUTNITURE_REMOVE, furniture.Name, (ring.FFarden.Furnitures[FurniturePlace.GARDEN].Count +
                    ring.FFarden.Furnitures[FurniturePlace.ROOM].Count), Configuration.Instance.MaxFurnitureCount));

        }
        public void OnFFFurnitureRoomAppear()
        {
            Packet p = new Packet();
            string args = "20 44 00 00 00 01 00 00 00 00 F6 31 FF 94 00 64";
            byte[] buf = Conversions.HexStr2Bytes(args.Replace(" ", ""));
            p = new Packet();
            p.data = buf;
            this.netIO.SendPacket(p);
        }
        public void OnFFurnitureCastleSetup(Packets.Client.CSMG_FF_CASTLE_SETUP p)
        {
            if (this.Character.MapID == 90001999)
            {
                CustomMapManager.Instance.SerFFFurnitureCastleSetup(this, p);
                return;
            }
            if (this.Character.Ring == null || this.Character.Ring.FFarden == null)
                return;
            SagaDB.Ring.Ring ring = SagaMap.Manager.RingManager.Instance.GetRing(this.Character.Ring.ID);
            if (this.Character.MapID != ring.FFarden.MapID && this.Character.MapID != ring.FFarden.RoomMapID)
                return;
            if (ring.FFarden.Furnitures[FurniturePlace.HOUSE].Count > 0)
            {
                SendSystemMessage("无法重复设置");
                return;
            }
            Item item = this.Character.Inventory.GetItem(p.InventorySlot);
            ActorFurnitureUnit actor = new ActorFurnitureUnit();

            DeleteItem(p.InventorySlot, 1, false);

            actor.MapID = this.Character.MapID;
            actor.ItemID = item.ItemID;
            Map map = MapManager.Instance.GetMap(actor.MapID);
            actor.X = p.X;
            actor.Z = p.Z;
            actor.Yaxis = p.Yaxis;
            actor.Name = "【" + this.Character.Name + "】" + item.BaseData.name;
            actor.PictID = item.PictID;
            actor.e = new ActorEventHandlers.NullEventHandler();
            map.RegisterActor(actor);
            actor.invisble = false;
            map.OnActorVisibilityChange(actor);
            if (this.Character.MapID == this.Character.Ring.FFarden.MapID)
            {
                ring.FFarden.Furnitures[FurniturePlace.HOUSE].Add(actor);
            }
            MapServer.charDB.SaveFF(ring);
        }
        public void OnFFFurnitureRoomEnter(Packets.Client.CSMG_FF_FURNITURE_ROOM_ENTER p)
        {
            if (p.data == 1)
            {
                if(this.map.ID == 90001999)
                {
                    CustomMapManager.Instance.SerFFRoomEnter(this);
                    return;
                }
                SagaDB.Ring.Ring ring = SagaMap.Manager.RingManager.Instance.GetRing(this.Character.Ring.ID);

                if (ring == null)
                    return;
                if (ring.FFarden.RoomMapID == 0)
                {
                    ring.FFarden.RoomMapID = MapManager.Instance.CreateMapInstance(ring, 91000000, ring.FFarden.MapID, 6, 7, false);
                    //spawn furnitures
                    Map map = MapManager.Instance.GetMap(ring.FFarden.RoomMapID);
                    foreach (ActorFurniture i in ring.FFarden.Furnitures[FurniturePlace.ROOM])
                    {
                        i.e = new ActorEventHandlers.NullEventHandler();
                        map.RegisterActor(i);
                        i.invisble = false;
                    }
                }
                this.Map.SendActorToMap(this.Character, ring.FFarden.RoomMapID, 20, 36, true);
            }
        }
        public void OnFFurnitureUnitSetup(Packets.Client.CSMG_FF_UNIT_SETUP p)
        {
            if (this.Character.Ring == null || this.Character.Ring.FFarden == null)
                return;
            SagaDB.Ring.Ring ring = SagaMap.Manager.RingManager.Instance.GetRing(this.Character.Ring.ID);
            if (this.Character.MapID != ring.FFarden.MapID && this.Character.MapID != ring.FFarden.RoomMapID)
                return;
            Item item = this.Character.Inventory.GetItem(p.InventorySlot);
            ActorFurnitureUnit actor = new ActorFurnitureUnit();

            DeleteItem(p.InventorySlot, 1, false);
            actor.MapID = this.Character.MapID;
            actor.ItemID = item.ItemID;
            Map map = MapManager.Instance.GetMap(actor.MapID);
            actor.X = p.X;
            actor.Y = 0;
            actor.Z = p.Z;
            actor.Xaxis = 0;
            actor.Yaxis = p.Yaxis;
            actor.Zaxis = 0;
            actor.Name = "【" + this.Character.Name + "】" + item.BaseData.name;
            actor.PictID = item.PictID;
            actor.e = new ActorEventHandlers.NullEventHandler();


            map.RegisterActor(actor);
            actor.invisble = false;
            map.OnActorVisibilityChange(actor);
            if (this.Character.MapID == this.Character.Ring.FFarden.MapID)
            {
                if(item.ItemID == 30300000)
                    ring.FFarden.Furnitures[FurniturePlace.FISHERY].Add(actor);
                else if (item.ItemID == 30260000)
                    ring.FFarden.Furnitures[FurniturePlace.FARM].Add(actor);
            }
            MapServer.charDB.SaveFF(ring);
        }
    }
}

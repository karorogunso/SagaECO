using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net;
using System.Net.Sockets;

using SagaDB;
using SagaDB.Item;
using SagaDB.Actor;
using SagaDB.FGarden;
using SagaLib;
using SagaMap;
using SagaMap.Manager;


namespace SagaMap.Network.Client
{
    public partial class MapClient
    {
        public void OnFGardenFurnitureUse(Packets.Client.CSMG_FGARDEN_FURNITURE_USE p)
        {
            Map map = MapManager.Instance.GetMap(this.Character.MapID);
            Actor actor = map.GetActor(p.ActorID);
            if (actor == null)
                return;
            if (actor.type != ActorType.FURNITURE)
                return;
            ActorFurniture furniture = (ActorFurniture)actor;
            Item item = ItemFactory.Instance.GetItem(furniture.ItemID);
            if (item.BaseData.eventID != 0)
            {
                EventActivate(item.BaseData.eventID);
            }   
        }

        public void OnFGardenFurnitureReconfig(Packets.Client.CSMG_FGARDEN_FURNITURE_RECONFIG p)
        {
            if (this.Character.FGarden == null)
                return;
            Map map = MapManager.Instance.GetMap(this.Character.MapID);
            Actor actor = map.GetActor(p.ActorID);
            if (actor == null)
                return;
            if (actor.type != ActorType.FURNITURE)
                return;
            if (this.Character.MapID != this.Character.FGarden.MapID && this.Character.MapID != this.Character.FGarden.RoomMapID)
            {
                Packets.Server.SSMG_FG_FURNITURE_RECONFIG p1 = new SagaMap.Packets.Server.SSMG_FG_FURNITURE_RECONFIG();
                p1.ActorID = actor.ActorID;
                p1.X = actor.X;
                p1.Y = actor.Y;
                p1.Z = ((ActorFurniture)actor).Z;
                p1.Dir = actor.Dir;
                this.netIO.SendPacket(p1);
                return;
            }
            map.MoveActor(Map.MOVE_TYPE.START, actor, new short[] { p.X, p.Y, p.Z }, p.Dir, 200);
        }

        public void OnFGardenFurnitureRemove(Packets.Client.CSMG_FGARDEN_FURNITURE_REMOVE p)
        {
            if (this.Character.FGarden == null)
                return;
            if (this.Character.MapID != this.Character.FGarden.MapID && this.Character.MapID != this.Character.FGarden.RoomMapID)
                return;
            Map map = null;
            map = MapManager.Instance.GetMap(this.Character.MapID);
            Actor actor = map.GetActor(p.ActorID);
            if (actor == null)
                return;
            if (actor.type != ActorType.FURNITURE)
                return;
            ActorFurniture furniture = (ActorFurniture)actor;
            map.DeleteActor(actor);
            Item item = ItemFactory.Instance.GetItem(furniture.ItemID);
            item.PictID = furniture.PictID;
            if (this.Character.MapID == this.Character.FGarden.MapID)
                this.Character.FGarden.Furnitures[FurniturePlace.GARDEN].Remove(furniture);
            else
                this.Character.FGarden.Furnitures[FurniturePlace.ROOM].Remove(furniture);
            AddItem(item, false);
            SendSystemMessage(string.Format(LocalManager.Instance.Strings.FG_FUTNITURE_REMOVE, furniture.Name, (this.Character.FGarden.Furnitures[FurniturePlace.GARDEN].Count +
                    this.Character.FGarden.Furnitures[FurniturePlace.ROOM].Count), Configuration.Instance.MaxFurnitureCount));
        }

        public void OnFGardenFurnitureSetup(Packets.Client.CSMG_FGARDEN_FURNITURE_SETUP p)
        {
            if (this.Character.FGarden == null)
                return;
            if (this.Character.MapID != this.Character.FGarden.MapID && this.Character.MapID != this.Character.FGarden.RoomMapID)
                return;
            if ((this.Character.FGarden.Furnitures[FurniturePlace.GARDEN].Count +
                this.Character.FGarden.Furnitures[FurniturePlace.ROOM].Count) < Configuration.Instance.MaxFurnitureCount)
            {
                Item item = this.Character.Inventory.GetItem(p.InventorySlot);
                ActorFurniture actor = new ActorFurniture();

                DeleteItem(p.InventorySlot, 1, false);

                actor.MapID = this.Character.MapID;
                actor.ItemID = item.ItemID;
                Map map = MapManager.Instance.GetMap(actor.MapID);
                actor.X = p.X;
                actor.Y = p.Y;
                actor.Z = p.Z;
                //actor.Dir = p.Dir;
                actor.Xaxis = p.AxleX;
                actor.Yaxis = p.AxleY;
                actor.Zaxis = p.AxleZ;
                actor.Name = item.BaseData.name;
                actor.PictID = item.PictID;
                actor.e = new ActorEventHandlers.NullEventHandler();
                map.RegisterActor(actor);
                actor.invisble = false;
                map.OnActorVisibilityChange(actor);

                if (this.Character.MapID == this.Character.FGarden.MapID)
                    this.Character.FGarden.Furnitures[FurniturePlace.GARDEN].Add(actor);
                else
                    this.Character.FGarden.Furnitures[FurniturePlace.ROOM].Add(actor);
                SendSystemMessage(string.Format(LocalManager.Instance.Strings.FG_FUTNITURE_SETUP, actor.Name, (this.Character.FGarden.Furnitures[FurniturePlace.GARDEN].Count +
                    this.Character.FGarden.Furnitures[FurniturePlace.ROOM].Count), Configuration.Instance.MaxFurnitureCount));
            }
            else
            {
                SendSystemMessage(LocalManager.Instance.Strings.FG_FUTNITURE_MAX);
            }            
        }

        public void OnFGardenEquipt(Packets.Client.CSMG_FGARDEN_EQUIPT p)
        {
            if (this.Character.FGarden == null)
                return;
            if (this.Character.MapID != this.Character.FGarden.MapID && this.Character.MapID != this.Character.FGarden.RoomMapID)
                return;
            if (p.InventorySlot != 0xFFFFFFFF)
            {
                Item item = this.Character.Inventory.GetItem(p.InventorySlot);
                if (item == null)
                    return;
                if (this.Character.FGarden.FGardenEquipments[p.Place] != 0)
                {
                    uint itemID = this.Character.FGarden.FGardenEquipments[p.Place];
                    AddItem(ItemFactory.Instance.GetItem(itemID, true), false);
                    Packets.Server.SSMG_FG_EQUIPT p1 = new SagaMap.Packets.Server.SSMG_FG_EQUIPT();
                    p1.ItemID = 0;
                    p1.Place = p.Place;
                    this.netIO.SendPacket(p1);
                }
                if (p.Place == SagaDB.FGarden.FGardenSlot.GARDEN_MODELHOUSE && this.Character.FGarden.FGardenEquipments[SagaDB.FGarden.FGardenSlot.GARDEN_MODELHOUSE] == 0)
                {
                    Packets.Server.SSMG_NPC_SET_EVENT_AREA p1 = new SagaMap.Packets.Server.SSMG_NPC_SET_EVENT_AREA();
                    p1.EventID = 10000315;
                    p1.StartX = 6;
                    p1.StartY = 7;
                    p1.EndX = 6;
                    p1.EndY = 7;
                    this.netIO.SendPacket(p1);
                }
                this.Character.FGarden.FGardenEquipments[p.Place] = item.ItemID;
                Packets.Server.SSMG_FG_EQUIPT p2 = new SagaMap.Packets.Server.SSMG_FG_EQUIPT();
                p2.ItemID = item.ItemID;
                p2.Place = p.Place;
                this.netIO.SendPacket(p2);
                DeleteItem(p.InventorySlot, 1, false);
            }
            else
            {
                uint itemID = this.Character.FGarden.FGardenEquipments[p.Place];
                if (itemID != 0)
                    AddItem(ItemFactory.Instance.GetItem(itemID, true), false);
                this.Character.FGarden.FGardenEquipments[p.Place] = 0;
                Packets.Server.SSMG_FG_EQUIPT p1 = new SagaMap.Packets.Server.SSMG_FG_EQUIPT();
                p1.ItemID = 0;
                p1.Place = p.Place;
                this.netIO.SendPacket(p1);
                if (p.Place == SagaDB.FGarden.FGardenSlot.GARDEN_MODELHOUSE)
                {
                    Packets.Server.SSMG_NPC_CANCEL_EVENT_AREA p2 = new SagaMap.Packets.Server.SSMG_NPC_CANCEL_EVENT_AREA();
                    p2.StartX = 6;
                    p2.StartY = 7;
                    p2.EndX = 6;
                    p2.EndY = 7;
                    this.netIO.SendPacket(p2);
                }
            }
        }
    }
}

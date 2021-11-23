using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using SagaDB.ODWar;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Manager;
using SagaMap.Network.Client;

namespace SagaMap.Manager
{
    public class FictitiousActorsManager : Singleton<FictitiousActorsManager>
    {
        public FictitiousActorsManager()
        {
        }

        public void regionFictitiousActors()
        {
            foreach (uint Mapid in SagaDB.FictitiousActors.FictitiousActorsFactory.Instance.FictitiousActorsList.Keys)
            {
                Map map = MapManager.Instance.GetMap(Mapid);
                foreach (Actor item in SagaDB.FictitiousActors.FictitiousActorsFactory.Instance.FictitiousActorsList[Mapid])
                {
                    if (item.type == ActorType.PC)
                    {
                        ActorPC a = (ActorPC)item;
                        if (a.Equips[0] != 0)
                            a.Inventory.Equipments[EnumEquipSlot.HEAD] = ItemFactory.Instance.GetItem(a.Equips[0]);
                        if (a.Equips[1] != 0)
                            a.Inventory.Equipments[EnumEquipSlot.FACE] = ItemFactory.Instance.GetItem(a.Equips[1]);
                        if (a.Equips[2] != 0)
                            a.Inventory.Equipments[EnumEquipSlot.CHEST_ACCE] = ItemFactory.Instance.GetItem(a.Equips[2]);
                        if (a.Equips[3] != 0)
                            a.Inventory.Equipments[EnumEquipSlot.UPPER_BODY] = ItemFactory.Instance.GetItem(a.Equips[3]);
                        if (a.Equips[4] != 0)
                            a.Inventory.Equipments[EnumEquipSlot.LOWER_BODY] = ItemFactory.Instance.GetItem(a.Equips[4]);
                        if (a.Equips[5] != 0)
                            a.Inventory.Equipments[EnumEquipSlot.BACK] = ItemFactory.Instance.GetItem(a.Equips[5]);
                        if (a.Equips[6] != 0)
                            a.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND] = ItemFactory.Instance.GetItem(a.Equips[6]);
                        if (a.Equips[7] != 0)
                            a.Inventory.Equipments[EnumEquipSlot.LEFT_HAND] = ItemFactory.Instance.GetItem(a.Equips[7]);
                        if (a.Equips[8] != 0)
                            a.Inventory.Equipments[EnumEquipSlot.SHOES] = ItemFactory.Instance.GetItem(a.Equips[8]);
                        if (a.Equips[9] != 0)
                            a.Inventory.Equipments[EnumEquipSlot.SOCKS] = ItemFactory.Instance.GetItem(a.Equips[9]);
                        if (a.Equips[10] != 0)
                            a.Inventory.Equipments[EnumEquipSlot.PET] = ItemFactory.Instance.GetItem(a.Equips[10]);
                        if (a.Equips[11] != 0)
                            a.Inventory.Equipments[EnumEquipSlot.EFFECT] = ItemFactory.Instance.GetItem(a.Equips[11]);
                        a.Fictitious = true;
                        item.X = SagaLib.Global.PosX8to16(item.X2, map.Width);
                        item.Y = SagaLib.Global.PosY8to16(item.Y2, map.Height);
                        item.e = new ActorEventHandlers.NullEventHandler();
                        map.RegisterActor(item);
                        item.invisble = false;
                        map.OnActorVisibilityChange(item);
                    }
                    else if (item.type == ActorType.FURNITURE)
                    {
                        ActorFurniture fi = (ActorFurniture)item;
                        fi.e = new ActorEventHandlers.NullEventHandler();
                        map.RegisterActor(item);
                        item.invisble = false;
                        map.OnActorVisibilityChange(item);
                    }
                }
            }
        }
    }
}

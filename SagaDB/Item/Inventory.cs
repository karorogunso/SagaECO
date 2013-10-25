using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;

namespace SagaDB.Item
{
    public class Inventory
    {
        public enum SearchType
        {
            ITEM_ID,
            SLOT_ID
        }
        private Dictionary<ContainerType, List<Item>> items = new Dictionary<ContainerType, List<Item>>();
        private Dictionary<EnumEquipSlot, Item> equipments = new Dictionary<EnumEquipSlot, Item>();
        private uint index;

        public Inventory()
        {
            items.Add(ContainerType.BODY, new List<Item>());
            items.Add(ContainerType.LEFT_BAG, new List<Item>());
            items.Add(ContainerType.RIGHT_BAG, new List<Item>());
            items.Add(ContainerType.BACK_BAG, new List<Item>());
        }
        public InventoryAddResult AddItem(ContainerType container, Item item, bool newIndex)
        {
            switch (container)
            {
                case ContainerType.BODY:
                case ContainerType.LEFT_BAG:
                case ContainerType.RIGHT_BAG:
                case ContainerType.BACK_BAG:
                    List<Item> list = items[container];
                    var query =
                        from it in list
                        where it.ItemID == item.ItemID && ((it.stack + item.stack) < 99)
                        select it;
                    if (query.Count() != 0 && item.Stackable)
                    {
                        query.First().stack += item.stack;
                        item.stack = query.First().stack;
                        item.Slot = query.First().Slot;
                        return InventoryAddResult.STACKED;
                    }
                    else
                    {
                        list.Add(item);
                        if (newIndex)
                        {
                            item.Slot = index;
                            index++;
                        }
                        return InventoryAddResult.NEW_INDEX;
                    }
                case ContainerType.BACK:
                case ContainerType.CHEST_ACCE:
                case ContainerType.FACE:
                case ContainerType.FACE_ACCE:
                case ContainerType.HEAD:
                case ContainerType.HEAD_ACCE:
                case ContainerType.LEFT_HAND:
                case ContainerType.LOWER_BODY:
                case ContainerType.PET:
                case ContainerType.RIGHT_HAND:
                case ContainerType.SHOES:
                case ContainerType.SOCKS:
                case ContainerType.UPPER_BODY:
                    if (equipments.ContainsKey((EnumEquipSlot)Enum.Parse(typeof(EnumEquipSlot), container.ToString())))
                    {
                        Logger.ShowDebug("Container:" + container.ToString() + " must be empty before adding item!", Logger.CurrentLogger);
                    }
                    else
                    {
                        equipments.Add((EnumEquipSlot)Enum.Parse(typeof(EnumEquipSlot), container.ToString()), item);
                        if (newIndex)
                        {
                            item.Slot = index;
                            index++;
                        }
                    }
                    return InventoryAddResult.NEW_INDEX;
                default:
                    throw new ArgumentException("Unsupported container!");
            }
        }

        public InventoryAddResult AddItem(ContainerType container, Item item)
        {
            return AddItem(container, item, true);
        }

        public Item GetItem(uint slotID)
        {
            for (int i = 2; i < 6; i++)
            {
                List<Item> list = items[(ContainerType)i];
                List<Item> result = new List<Item>();

                var query =
                        from it in list
                        where it.Slot == slotID
                        select it;
                result = query.ToList();

                if (result.Count() == 0)
                {
                    continue;
                }
                return result.First();
            }
            for (int i = 0; i < 13; i++)
            {
                if (!equipments.ContainsKey((EnumEquipSlot)i))
                    continue;
                Item item = equipments[(EnumEquipSlot)i];
                if (item.Slot == slotID)
                {
                    return item;
                }
            }
            return null;
        }

        public InventoryDeleteResult DeleteItem(ContainerType container, uint itemID, int count)
        {
            return DeleteItem(container, (int)itemID, count, SearchType.ITEM_ID);
        }

        public InventoryDeleteResult DeleteItem(uint slotID, int count)
        {
            for (int i = 2; i < 6; i++)
            {
                List<Item> list = items[(ContainerType)i];
                List<Item> result = new List<Item>();

                var query =
                        from it in list
                        where it.Slot == slotID
                        select it;
                result = query.ToList();                

                if (result.Count() == 0)
                {
                    continue;
                }
                Item item = result.First();
                if (item.stack > count)
                {
                    item.stack -= (byte)count;
                    return InventoryDeleteResult.STACK_UPDATED;
                }
                else
                {
                    int rest = count - item.stack;
                    list.Remove(item);
                    return InventoryDeleteResult.ALL_DELETED;
                }
            }
            for (int i = 0; i < 13; i++)
            {
                if (!items.ContainsKey((ContainerType)i))
                    continue;
                Item item = equipments[(EnumEquipSlot)i];
                if (item.Slot == slotID)
                {
                    equipments.Remove((EnumEquipSlot)i);
                    return InventoryDeleteResult.ALL_DELETED;
                }
            }
            return InventoryDeleteResult.ALL_DELETED;
        }

        private InventoryDeleteResult DeleteItem(ContainerType container, int ID, int count, SearchType type)
        {
            switch (container)
            {
                case ContainerType.BODY:
                case ContainerType.LEFT_BAG:
                case ContainerType.RIGHT_BAG:
                case ContainerType.BACK_BAG:
                    List<Item> list = items[container];
                    List<Item> result = new List<Item>();
                    switch (type)
                    {
                        case SearchType.ITEM_ID:
                            var query =
                                      from it in list
                                      where it.ItemID == ID
                                      select it;
                            result = query.ToList();
                            break;
                        case SearchType.SLOT_ID:
                            var query1 =
                                    from it in list
                                    where it.Slot == ID
                                    select it;
                            result = query1.ToList();
                            break;
                    }
                    if (result.Count() == 0)
                    {
                        throw new ArgumentException("No such item");
                    }
                    Item item = result.First();
                    if (item.stack > count)
                    {
                        item.stack -= (byte)count;
                        return InventoryDeleteResult.STACK_UPDATED;
                    }
                    else
                    {
                        int rest = count - item.stack;
                        list.Remove(item);
                        return InventoryDeleteResult.ALL_DELETED;
                    }                    
                case ContainerType.BACK:
                case ContainerType.CHEST_ACCE:
                case ContainerType.FACE:
                case ContainerType.FACE_ACCE:
                case ContainerType.HEAD:
                case ContainerType.HEAD_ACCE:
                case ContainerType.LEFT_HAND:
                case ContainerType.LOWER_BODY:
                case ContainerType.PET:
                case ContainerType.RIGHT_HAND:
                case ContainerType.SHOES:
                case ContainerType.SOCKS:
                case ContainerType.UPPER_BODY:
                    equipments.Remove((EnumEquipSlot)Enum.Parse(typeof(EnumEquipSlot), container.ToString()));
                    return InventoryDeleteResult.ALL_DELETED;                    
            }
            return InventoryDeleteResult.ALL_DELETED;
        }

        public void MoveItem(ContainerType src, uint itemID, ContainerType dst, int count)
        {
            MoveItem(src, (int)itemID, dst, count, SearchType.ITEM_ID);
        }

        public void MoveItem(ContainerType src, int slotID, ContainerType dst, int count)
        {
            MoveItem(src, (int)slotID, dst, count, SearchType.SLOT_ID);
        }

        private void MoveItem(ContainerType src, int ID, ContainerType dst, int count, SearchType type)
        {
            List<Item> list;
            if (src == dst)
            {
                Logger.ShowDebug("Source container is equal to Destination container! Transfer aborted!", Logger.CurrentLogger);
                return;
            }
            switch (src)
            {
                case ContainerType.BODY:
                case ContainerType.LEFT_BAG:
                case ContainerType.RIGHT_BAG:
                case ContainerType.BACK_BAG:
                    list = items[src];
                    break;
                case ContainerType.BACK:
                case ContainerType.CHEST_ACCE:
                case ContainerType.FACE:
                case ContainerType.FACE_ACCE:
                case ContainerType.HEAD:
                case ContainerType.HEAD_ACCE:
                case ContainerType.LEFT_HAND:
                case ContainerType.LOWER_BODY:
                case ContainerType.PET:
                case ContainerType.RIGHT_HAND:
                case ContainerType.SHOES:
                case ContainerType.SOCKS:
                case ContainerType.UPPER_BODY:
                    list = new List<Item>();
                    list.Add(equipments[(EnumEquipSlot)Enum.Parse(typeof(EnumEquipSlot), src.ToString())]);
                    equipments.Remove((EnumEquipSlot)Enum.Parse(typeof(EnumEquipSlot), src.ToString()));
                    break;
                default:
                    throw new ArgumentException("Unsupported Source Container!");
            }

            List<Item> result = new List<Item>();
            switch (type)
            {
                case SearchType.ITEM_ID:
                    var query =
                              from it in list
                              where it.ItemID == ID
                              select it;
                    result = query.ToList();
                    break;
                case SearchType.SLOT_ID:
                    var query1 =
                            from it in list
                            where it.Slot == ID
                            select it;
                    result = query1.ToList();
                    break;
            } 
            if (result.Count == 0)
            {
                throw new ArgumentException("The source container doesn't contain such item");
            }
            else
            {
                Item oldItem = result.First();
                Item newItem = oldItem.Clone();
                newItem.Slot = oldItem.Slot;
                if (count > oldItem.stack)
                    count = oldItem.stack;
                newItem.stack = (byte)count;
                oldItem.stack -= (byte)count;
                if (oldItem.stack == 0)
                    list.Remove(oldItem);
                AddItem(dst, newItem, false);
            }
        }

        public List<Item> GetContainer(ContainerType container)
        {
            switch (container)
            {
                case ContainerType.BODY:
                case ContainerType.LEFT_BAG:
                case ContainerType.RIGHT_BAG:
                case ContainerType.BACK_BAG:
                    return items[container];
                case ContainerType.BACK:
                case ContainerType.CHEST_ACCE:
                case ContainerType.FACE:
                case ContainerType.FACE_ACCE:
                case ContainerType.HEAD:
                case ContainerType.HEAD_ACCE:
                case ContainerType.LEFT_HAND:
                case ContainerType.LOWER_BODY:
                case ContainerType.PET:
                case ContainerType.RIGHT_HAND:
                case ContainerType.SHOES:
                case ContainerType.SOCKS:
                case ContainerType.UPPER_BODY:
                    Item item;
                    List<Item> newList = new List<Item>();
                    if (equipments.ContainsKey((EnumEquipSlot)Enum.Parse(typeof(EnumEquipSlot), container.ToString())))
                    {
                        item = equipments[(EnumEquipSlot)Enum.Parse(typeof(EnumEquipSlot), container.ToString())];
                        newList.Add(item);
                    }
                    return newList;                    
                default:
                    return new List<Item>();
            }
        }

        public Dictionary<EnumEquipSlot, Item> Equipments
        {
            get
            {
                return this.equipments;
            }
        }

        public ContainerType GetContainerType(uint slotID)
        {
            for (int i = 2; i < 6; i++)
            {
                List<Item> list = items[(ContainerType)i];
                List<Item> result = new List<Item>();

                var query =
                        from it in list
                        where it.Slot == slotID
                        select it;
                result = query.ToList();

                if (result.Count() == 0)
                {
                    continue;
                }
                return (ContainerType)i;
            }
            for (int i = 0; i < 13; i++)
            {
                if (!equipments.ContainsKey((EnumEquipSlot)i))
                    continue;
                Item item = equipments[(EnumEquipSlot)i];
                if (item.Slot == slotID)
                {
                    return (ContainerType)Enum.Parse(typeof(ContainerType), ((EnumEquipSlot)i).ToString());
                }
            }
            return ContainerType.OTHER_WAREHOUSE;
        }
    }
}

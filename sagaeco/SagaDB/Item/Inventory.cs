using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using SagaDB.DEMIC;

namespace SagaDB.Item
{
    [Serializable]
    public class Inventory
    {
        public enum SearchType
        {
            ITEM_ID,
            SLOT_ID
        }
        [NonSerialized]
        private Actor.ActorPC owner;
        private IConcurrentDictionary<ContainerType, List<Item>> items = new IConcurrentDictionary<ContainerType, List<Item>>();
        private IConcurrentDictionary<EnumEquipSlot, Item> equipments = new IConcurrentDictionary<EnumEquipSlot, Item>();
        private IConcurrentDictionary<EnumEquipSlot, Item> parts = new IConcurrentDictionary<EnumEquipSlot, Item>();
        private IConcurrentDictionary<WarehousePlace, List<Item>> ware = new IConcurrentDictionary<WarehousePlace, List<Item>>();

        bool needSave = false;
        bool needSaveWare = false;
        Dictionary<byte, DEMICPanel> demicChips = new Dictionary<byte, DEMICPanel>();
        Dictionary<byte, DEMICPanel> ddemicChips = new Dictionary<byte, DEMICPanel>();

        static ushort version = 6;

        private uint index = 1;
        [NonSerialized]
        public uint wareIndex = 200000001;
        private uint golemWareIndex = 300000001;
        private Item lastItem;

        private Dictionary<ContainerType, uint> payload = new Dictionary<ContainerType, uint>();
        private Dictionary<ContainerType, uint> maxPayload = new Dictionary<ContainerType, uint>();
        private Dictionary<ContainerType, uint> volume = new Dictionary<ContainerType, uint>();
        private Dictionary<ContainerType, uint> maxVolume = new Dictionary<ContainerType, uint>();

        public IConcurrentDictionary<ContainerType, List<Item>> Items { get { return this.items; } }

        public Actor.ActorPC Owner { get { return this.owner; } set { this.owner = value; } }
        /// <summary>
        /// 负重
        /// </summary>
        public Dictionary<ContainerType, uint> Payload { get { return this.payload; } }
        /// <summary>
        /// 最大负重
        /// </summary>
        public Dictionary<ContainerType, uint> MaxPayload { get { return this.maxPayload; } }
        /// <summary>
        /// 体积
        /// </summary>
        public Dictionary<ContainerType, uint> Volume { get { return this.volume; } }
        /// <summary>
        /// 最大体积
        /// </summary>
        public Dictionary<ContainerType, uint> MaxVolume { get { return this.maxVolume; } }
        /// <summary>
        /// 仓库
        /// </summary>
        public IConcurrentDictionary<WarehousePlace, List<Item>> WareHouse { get { return this.ware; } set { this.ware = value; } }

        /// <summary>
        /// 玩家的DEMIC芯片组
        /// </summary>
        public Dictionary<byte, DEMICPanel> DemicChips
        {
            get
            {
                int validCL = owner.CL;
                int pageCount = validCL / 81 + 1;
                for (int i = 0; i < pageCount; i++)
                {
                    if (!demicChips.ContainsKey((byte)i))
                        demicChips.Add((byte)i, new DEMICPanel());
                }
                return this.demicChips;
            }
        }

        /// <summary>
        /// 玩家恶魔界的ＤＥＭＩＣ芯片组
        /// </summary>
        public Dictionary<byte, DEMICPanel> DominionDemicChips { get {
            int validCL = owner.DominionCL;
            int pageCount = validCL / 81 + 1;
            for (int i = 0; i < pageCount; i++)
            {
                if (!ddemicChips.ContainsKey((byte)i))
                    ddemicChips.Add((byte)i, new DEMICPanel());
            }
            return this.ddemicChips; } }
        
        /// <summary>
        /// 检查道具栏是否是空
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                foreach (List<Item> i in items.Values)
                {
                    if (i.Count > 0)
                        return false;
                }
                return true;
            }
        }

        public bool NeedSave
        {
            get
            {
                return needSave;
            }
        }
        public bool NeedSaveWare { get { return this.needSave; } }

        /// <summary>
        /// 检查仓库是否是空
        /// </summary>
        public bool IsWarehouseEmpty
        {
            get
            {
                foreach (List<Item> i in ware.Values)
                {
                    if (i.Count > 0)
                        return false;
                }
                return true;
            }
        }

        /// <summary>
        /// 仓库目前道具总数
        /// </summary>
        public int WareTotalCount
        {
            get
            {
                int count = 0;
                foreach (List<Item> i in ware.Values)
                {
                    count += i.Count;
                }
                return count;
            }
        }


        public Inventory(Actor.ActorPC owner)
        {
            this.owner = owner;
            items.Add(ContainerType.BODY, new List<Item>());
            items.Add(ContainerType.LEFT_BAG, new List<Item>());
            items.Add(ContainerType.RIGHT_BAG, new List<Item>());
            items.Add(ContainerType.BACK_BAG, new List<Item>());
            items.Add(ContainerType.GOLEMWAREHOUSE, new List<Item>());

            ware.Add(WarehousePlace.Acropolis ,new List<Item>());
            ware.Add(WarehousePlace.FederalOfIronSouth, new List<Item>());
            ware.Add(WarehousePlace.FarEast, new List<Item>());
            ware.Add(WarehousePlace.IronSouth, new List<Item>());
            ware.Add(WarehousePlace.KingdomOfNorthan, new List<Item>());
            ware.Add(WarehousePlace.MiningCamp, new List<Item>());
            ware.Add(WarehousePlace.Morg, new List<Item>());
            ware.Add(WarehousePlace.Northan, new List<Item>());
            ware.Add(WarehousePlace.RepublicOfFarEast, new List<Item>());
            ware.Add(WarehousePlace.Tonka, new List<Item>());
            ware.Add(WarehousePlace.ECOTown, new List<Item>());
            ware.Add(WarehousePlace.MaimaiCamp, new List<Item>());
            ware.Add(WarehousePlace.MermaidsHome, new List<Item>());
            ware.Add(WarehousePlace.TowerGoesToHeaven, new List<Item>());
            ware.Add(WarehousePlace.WestFord, new List<Item>());

            payload.Add(ContainerType.BODY, 0);
            payload.Add(ContainerType.LEFT_BAG, 0);
            payload.Add(ContainerType.RIGHT_BAG, 0);
            payload.Add(ContainerType.BACK_BAG, 0);
            maxPayload.Add(ContainerType.BODY, 0);
            maxPayload.Add(ContainerType.LEFT_BAG, 0);
            maxPayload.Add(ContainerType.RIGHT_BAG, 0);
            maxPayload.Add(ContainerType.BACK_BAG, 0);
            volume.Add(ContainerType.BODY, 0);
            volume.Add(ContainerType.LEFT_BAG, 0);
            volume.Add(ContainerType.RIGHT_BAG, 0);
            volume.Add(ContainerType.BACK_BAG, 0);
            maxVolume.Add(ContainerType.BODY, 0);
            maxVolume.Add(ContainerType.LEFT_BAG, 0);
            maxVolume.Add(ContainerType.RIGHT_BAG, 0);
            maxVolume.Add(ContainerType.BACK_BAG, 0);

            demicChips.Add(0, new DEMICPanel());
            demicChips.Add(100, new DEMICPanel());
            demicChips.Add(101, new DEMICPanel());
            ddemicChips.Add(0, new DEMICPanel());
            ddemicChips.Add(100, new DEMICPanel());
            ddemicChips.Add(101, new DEMICPanel());
        }

        /// <summary>
        /// 计算目前道具栏中所有道具的空间和重量
        /// </summary>
        public void CalcPayloadVolume()
        {
            List<Item> list = items[ContainerType.BODY];
            uint pal = 0, vol = 0;
            foreach (Item i in list)
            {

                pal += (uint)(i.BaseData.weight * i.Stack);
                vol += (uint)(i.BaseData.volume * i.Stack);
            }
            if (owner.Form == SagaDB.Actor.DEM_FORM.NORMAL_FORM)
            {
                foreach (Item i in equipments.Values)
                {
                    pal += (uint)(i.BaseData.weight * i.Stack);
                    vol += (uint)(i.BaseData.equipVolume * i.Stack);
                }
            }
            else
            {
                foreach (Item i in parts.Values)
                {
                    pal += (uint)(i.BaseData.weight * i.Stack);
                    vol += (uint)(i.BaseData.equipVolume * i.Stack);
                }
            }
            payload[ContainerType.BODY] = pal;
            volume[ContainerType.BODY] = vol;

            for (int i = 3; i < 6; i++)
            {
                ContainerType type = (ContainerType)i;
                list = items[type];
                pal = 0;
                vol = 0;
                foreach (Item j in list)
                {
                    pal += (uint)(j.BaseData.weight * j.Stack);
                    vol += (uint)(j.BaseData.volume * j.Stack);
                }
                payload[type] = pal;
                volume[type] = vol;
            }
        }

        /// <summary>
        /// 向仓库添加道具
        /// </summary>
        /// <param name="place">仓库地点</param>
        /// <param name="item">要添加的道具</param>
        /// <returns>添加结果，需要注意的只有MIXED，MIXED的话，item则被改为叠加的道具，Inventory.LastItem则是多余的新道具</returns>
        public InventoryAddResult AddWareItem(WarehousePlace place, Item item)
        {
            try
            {
                needSaveWare = true;
                if (item.Stack > 0)
                {
                    Logger.LogWarehousePut(this.owner.Name + "," + this.owner.CharID, item.BaseData.name + "(" + item.ItemID + ")",
                        string.Format("WarehousePlace:{0} Count:{1}", place, item.Stack));
                }
                var query =
                    from it in ware[place]
                    where it.ItemID == item.ItemID && (it.Stack < 9999)
                    select it;
                if (query.Count() != 0 && item.Stackable)
                {
                    Item oriItem = query.First();
                    oriItem.Stack += item.Stack;
                    if (oriItem.Stack <= 9999)
                    {
                        item.Stack = oriItem.Stack;
                        item.Slot = oriItem.Slot;
                        return InventoryAddResult.STACKED;
                    }
                    else
                    {
                        ushort rest = (ushort)(oriItem.Stack - 9999);
                        if (rest > 9999)
                        {
                            Logger.ShowWarning("Adding too many item(" + item.BaseData.name + ":" + item.Stack + "), setting count to the maximal value(9999)");
                            rest = 9999;
                        }
                        oriItem.Stack = 9999;
                        item.Stack = oriItem.Stack;
                        item.Slot = oriItem.Slot;
                        Item newItem = item.Clone();
                        newItem.Stack = rest;
                        newItem.Slot = wareIndex;
                        wareIndex++;
                        ware[place].Add(newItem);
                        lastItem = newItem;
                        return InventoryAddResult.MIXED;
                    }
                }
                else
                {
                    if (item.Stack > 9999)
                    {
                        Logger.ShowWarning("Adding too many item(" + item.BaseData.name + ":" + item.Stack + "), setting count to the maximal value(9999)");
                        item.Stack = 9999;
                    }
                    ware[place].Add(item);
                    item.Slot = wareIndex;
                    wareIndex++;

                    return InventoryAddResult.NEW_INDEX;
                }
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
                return InventoryAddResult.ERROR;
            }
        }

        /// <summary>
        /// 从仓库删除物品
        /// </summary>
        /// <param name="place">仓库地点</param>
        /// <param name="slot">物品Slot</param>
        /// <param name="amount">数量</param>
        /// <returns>删除结果</returns>
        public InventoryDeleteResult DeleteWareItem(WarehousePlace place, uint slot, int amount)
        {
            needSaveWare = true;
            var query =
                      from it in ware[place]
                      where it.Slot == slot
                      select it;
            if (query.Count() == 0)
                return InventoryDeleteResult.ERROR;
            Item oriItem = query.First();
            if (oriItem.Stack > 0)
            {
                Logger.LogWarehouseGet(this.owner.Name + "(" + this.owner.CharID + ")", oriItem.BaseData.name + "(" + oriItem.ItemID + ")",
                    string.Format("WarehousePlace:{0} Count:{1}", place, oriItem.Stack));
            }
            
            
            if (oriItem.Stack > amount)
            {
                oriItem.Stack -= (ushort)amount;
                return InventoryDeleteResult.STACK_UPDATED;
            }
            else
            {
                ware[place].Remove(oriItem);
                return InventoryDeleteResult.ALL_DELETED;
            }
        }

        /// <summary>
        /// 添加道具
        /// </summary>
        /// <param name="container">容器</param>
        /// <param name="item">道具</param>
        /// <param name="newIndex">是否生成新索引</param>
        /// <returns>添加结果，需要注意的只有MIXED，MIXED的话，item则被改为叠加的道具，Inventory.LastItem则是多余的新道具</returns>
        public InventoryAddResult AddItem(ContainerType container, Item item, bool newIndex)
        {
            needSave = true;
            item.Owner = Owner;
            switch (container)
            {
                case ContainerType.BODY:
                case ContainerType.LEFT_BAG:
                case ContainerType.RIGHT_BAG:
                case ContainerType.BACK_BAG:
                case ContainerType.GOLEMWAREHOUSE:
                    List<Item> list = items[container];
                    var query =
                        from it in list
                        where it.ItemID == item.ItemID && (it.Stack < 9999)
                        select it;
                    if (query.Count() != 0 && item.Stackable)
                    {
                        Item oriItem = query.First();
                        oriItem.Stack += item.Stack;
                        if (oriItem.Stack <= 9999)
                        {//感觉有问题
                            item.Stack = oriItem.Stack;
                            item.Slot = oriItem.Slot;
                            lastItem = oriItem;
                            if (oriItem.Identified)
                                item.identified = oriItem.identified;
                            return InventoryAddResult.STACKED;
                        }
                        else
                        {
                            ushort rest = (ushort)(oriItem.Stack - 9999);
                            if (rest > 9999)
                            {
                                Logger.ShowWarning("Adding too many item(" + item.BaseData.name + ":" + item.Stack + "), setting count to the maximal value(9999)");
                                rest = 9999;
                            }
                            oriItem.Stack = 9999;
                            item.Stack = oriItem.Stack;
                            item.Slot = oriItem.Slot;
                            if (oriItem.Identified)
                                item.identified = oriItem.identified;                            
                            Item newItem = item.Clone();
                            newItem.Stack = rest;
                            if (container == ContainerType.GOLEMWAREHOUSE)
                            {
                                newItem.Slot = golemWareIndex;
                                golemWareIndex++;
                            }
                            else
                            {
                                newItem.Slot = index;
                                index++;
                            }
                            list.Add(newItem);
                            lastItem = newItem;
                            return InventoryAddResult.MIXED;
                        }
                    }
                    else
                    {
                        if (item.Stack > 9999)
                        {
                            Logger.ShowWarning("Adding too many item(" + item.BaseData.name + ":" + item.Stack + "), setting count to the maximal value(9999)");
                            item.Stack = 9999;
                        }
                        ;
                        list.Add(item);
                        lastItem = item;
                        if (newIndex)
                        {
                            if (container == ContainerType.GOLEMWAREHOUSE)
                            {
                                item.Slot = golemWareIndex;
                                golemWareIndex++;
                            }
                            else
                            {
                                item.Slot = index;
                                index++;
                            }                            
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
                case ContainerType.EFFECT:
                    if (equipments.ContainsKey((EnumEquipSlot)Enum.Parse(typeof(EnumEquipSlot), container.ToString())))
                    {
                        if (item.BaseData.itemType != ItemType.BULLET && item.BaseData.itemType != ItemType.ARROW && item.BaseData.itemType != ItemType.CARD && item.BaseData.itemType != ItemType.THROW)
                            Logger.ShowDebug("Container:" + container.ToString() + " must be empty before adding item!", Logger.CurrentLogger);
                        else
                        {
                            if (equipments[(EnumEquipSlot)Enum.Parse(typeof(EnumEquipSlot), container.ToString())].ItemID == item.ItemID)
                            {
                                equipments[(EnumEquipSlot)Enum.Parse(typeof(EnumEquipSlot), container.ToString())].Stack += item.Stack;
                            }
                            else
                            {
                                equipments[(EnumEquipSlot)Enum.Parse(typeof(EnumEquipSlot), container.ToString())] = item;
                            }
                        }
                    }
                    else
                    {
                        equipments.Add((EnumEquipSlot)Enum.Parse(typeof(EnumEquipSlot), container.ToString()), item);
                        lastItem = item; 
                        if (newIndex)
                        {
                            item.Slot = index;
                            index++;                            
                        }
                    }
                    return InventoryAddResult.NEW_INDEX;
                case ContainerType.BACK2:
                case ContainerType.CHEST_ACCE2:
                case ContainerType.FACE2:
                case ContainerType.FACE_ACCE2:
                case ContainerType.HEAD2:
                case ContainerType.HEAD_ACCE2:
                case ContainerType.LEFT_HAND2:
                case ContainerType.LOWER_BODY2:
                case ContainerType.PET2:
                case ContainerType.RIGHT_HAND2:
                case ContainerType.SHOES2:
                case ContainerType.SOCKS2:
                case ContainerType.UPPER_BODY2:
                    string name = container.ToString();
                    name = name.Substring(0, name.Length - 1);
                    if (parts.ContainsKey((EnumEquipSlot)Enum.Parse(typeof(EnumEquipSlot), name)))
                    {
                        if (item.BaseData.itemType != ItemType.BULLET && item.BaseData.itemType != ItemType.ARROW)
                            Logger.ShowDebug("Container:" + container.ToString() + " must be empty before adding item!", Logger.CurrentLogger);
                        else
                        {
                            if (parts[(EnumEquipSlot)Enum.Parse(typeof(EnumEquipSlot), name)].ItemID == item.ItemID)
                            {
                                parts[(EnumEquipSlot)Enum.Parse(typeof(EnumEquipSlot), name)].Stack += item.Stack;
                            }
                            else
                            {
                                parts[(EnumEquipSlot)Enum.Parse(typeof(EnumEquipSlot), name)] = item;
                            }
                        }
                    }
                    else
                    {
                        parts.Add((EnumEquipSlot)Enum.Parse(typeof(EnumEquipSlot), name), item);
                        lastItem = item;
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

        /// <summary>
        /// 添加道具
        /// </summary>
        /// <param name="container">容器</param>
        /// <param name="item">道具</param>
        /// <returns>添加结果，需要注意的只有MIXED，MIXED的话，item则被改为叠加的道具，Inventory.LastItem则是多余的新道具</returns>
        public InventoryAddResult AddItem(ContainerType container, Item item)
        {
            return AddItem(container, item, true);
        }
        public Item GetItem2()
        {
            //Logger.ShowError(string.Format("Get:{0}", DBID));
            for (int i = 2; i < 32; i++)
            {
                if (i < 6 || i == 31)
                {
                    List<Item> list = items[(ContainerType)i];
                    List<Item> result = new List<Item>();
                    var query1 =
        from it in list
        where it.ChangeMode == true
        select it;
                    result = query1.ToList();
                    if (result.Count() == 0)
                    {
                        continue;
                    }
                    return result.First();
                }
            }
            for (int i = 0; i < 14; i++)
            {
                if (!equipments.ContainsKey((EnumEquipSlot)i))
                    continue;
                Item item = equipments[(EnumEquipSlot)i];
                if (item.ChangeMode == true)
                {
                    //Logger.ShowError(string.Format("Get1:{0}", item.DBID));
                    return item;
                }
            }
            for (int i = 0; i < 14; i++)
            {
                if (!parts.ContainsKey((EnumEquipSlot)i))
                    continue;
                Item item = parts[(EnumEquipSlot)i];
                if (item.ChangeMode == true)
                {
                    //Logger.ShowError(string.Format("Get2:{0}", item.DBID));
                    return item;
                }
            }

            return null;
        }
        public Item GetItem(uint ID, SearchType type)
        {
            for (int i = 2; i < 32; i++)
            {
                if (i < 6 || i == 31)
                {
                    List<Item> list = items[(ContainerType)i];
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
                        continue;
                    }
                    return result.First();
                }
            }

            for (int i = 0; i < 14; i++)
            {
                if (!equipments.ContainsKey((EnumEquipSlot)i))
                    continue;
                Item item = equipments[(EnumEquipSlot)i];
                if (type == SearchType.SLOT_ID)
                {
                    if (item.Slot == ID)
                    {
                        return item;
                    }
                }
                if (type == SearchType.ITEM_ID)
                {
                    if (item.ItemID == ID)
                    {
                        return item;
                    }
                }
            }

            for (int i = 0; i < 14; i++)
            {
                if (!parts.ContainsKey((EnumEquipSlot)i))
                    continue;
                Item item = parts[(EnumEquipSlot)i];
                if (type == SearchType.SLOT_ID)
                {
                    if (item.Slot == ID)
                    {
                        return item;
                    }
                }
                if (type == SearchType.ITEM_ID)
                {
                    if (item.ItemID == ID)
                    {
                        return item;
                    }
                }
            }

            return null;
        }

        public Item GetItem(uint slotID)
        {
            return GetItem(slotID, SearchType.SLOT_ID);
        }

        public Item GetItem(WarehousePlace place, uint slotID)
        {
            var query =
                     from it in ware[place]
                     where it.Slot == slotID && it.Stack > 0
                     select it;
            if (query.Count() == 0)
                return null;
            else
                return query.First();
        }

        public InventoryDeleteResult DeleteItem(ContainerType container, uint itemID, int count)
        {
            return DeleteItem(container, (int)itemID, count, SearchType.ITEM_ID);
        }

        public InventoryDeleteResult DeleteItem(uint slotID, int count)
        {
            needSave = true;
            for (int i = 2; i < 32; i++)
            {
                if (i < 6 || i == 31)
                {
                    List<Item> list = items[(ContainerType)i];
                    List<Item> result = new List<Item>();

                    var query =
                            from it in list
                            where it.Slot == slotID && it.Stack > 0
                            select it;
                    result = query.ToList();

                    if (result.Count() == 0)
                    {
                        continue;
                    }
                    Item item = result.First();
                    if(item.Stack == 0)
                    {
                        list.Remove(item);
                        //Logger.ShowError("0 "+list.Remove(item).ToString()); ;
                        return InventoryDeleteResult.ALL_DELETED;
                    }
                    if (item.Stack > count)
                    {
                        item.Stack -= (ushort)count;
                        return InventoryDeleteResult.STACK_UPDATED;
                    }
                    else
                    {
                        //int rest = count - item.Stack;
                        list.Remove(item);
                        return InventoryDeleteResult.ALL_DELETED;
                    }
                }
            }
            for (int i = 0; i < 14; i++)
            {
                if (!equipments.ContainsKey((EnumEquipSlot)i))
                    continue;
                Item item = equipments[(EnumEquipSlot)i];
                if (item.Slot == slotID)
                {
                    if (item.Stack > 1)
                    {
                        item.Stack--;
                        return InventoryDeleteResult.STACK_UPDATED;
                    }
                    else
                    {
                        foreach (EnumEquipSlot j in item.EquipSlot)
                        {
                            equipments.Remove(j);
                        }
                        if (equipments.ContainsKey((EnumEquipSlot)i))
                            equipments.Remove((EnumEquipSlot)i);
                        return InventoryDeleteResult.ALL_DELETED;
                    }
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
                    if (item.Stack > count)
                    {
                        item.Stack -= (ushort)count;
                        return InventoryDeleteResult.STACK_UPDATED;
                    }
                    else
                    {
                        int rest = count - item.Stack;
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
                case ContainerType.EFFECT:
                    {
                        EnumEquipSlot slotE = (EnumEquipSlot)Enum.Parse(typeof(EnumEquipSlot), container.ToString());
                        if (equipments[slotE].Stack > 1)
                        {
                            equipments[slotE].Stack--;
                            return InventoryDeleteResult.STACK_UPDATED;
                        }
                        else
                        {
                            equipments.Remove(slotE);
                            return InventoryDeleteResult.ALL_DELETED;
                        }
                    }
                case ContainerType.BACK2:
                case ContainerType.CHEST_ACCE2:
                case ContainerType.FACE2:
                case ContainerType.FACE_ACCE2:
                case ContainerType.HEAD2:
                case ContainerType.HEAD_ACCE2:
                case ContainerType.LEFT_HAND2:
                case ContainerType.LOWER_BODY2:
                case ContainerType.PET2:
                case ContainerType.RIGHT_HAND2:
                case ContainerType.SHOES2:
                case ContainerType.SOCKS2:
                case ContainerType.UPPER_BODY2:
                    {
                        string name = container.ToString();
                        name = name.Substring(0, name.Length - 1);
                        EnumEquipSlot slotE = (EnumEquipSlot)Enum.Parse(typeof(EnumEquipSlot), name);
                        if (parts[slotE].Stack > 1)
                        {
                            parts[slotE].Stack--;
                            return InventoryDeleteResult.STACK_UPDATED;
                        }
                        else
                        {
                            parts.Remove(slotE);
                            return InventoryDeleteResult.ALL_DELETED;
                        }
                    }
            }
            return InventoryDeleteResult.ALL_DELETED;
        }

        public bool MoveItem(ContainerType src, uint itemID, ContainerType dst, int count)
        {
            return MoveItem(src, (int)itemID, dst, count, SearchType.ITEM_ID);
        }

        public bool MoveItem(ContainerType src, int slotID, ContainerType dst, int count)
        {
            return MoveItem(src, (int)slotID, dst, count, SearchType.SLOT_ID);
        }

        private bool MoveItem(ContainerType src, int ID, ContainerType dst, int count, SearchType type)
        {
            try
            {
                List<Item> list;
                if (src == dst)
                {
                    Logger.ShowDebug("Source container is equal to Destination container! Transfer aborted!", Logger.CurrentLogger);
                    return false;
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
                    case ContainerType.EFFECT:
                        list = new List<Item>();
                        list.Add(equipments[(EnumEquipSlot)Enum.Parse(typeof(EnumEquipSlot), src.ToString())]);
                        equipments.Remove((EnumEquipSlot)Enum.Parse(typeof(EnumEquipSlot), src.ToString()));
                        break;
                    case ContainerType.BACK2:
                    case ContainerType.CHEST_ACCE2:
                    case ContainerType.FACE2:
                    case ContainerType.FACE_ACCE2:
                    case ContainerType.HEAD2:
                    case ContainerType.HEAD_ACCE2:
                    case ContainerType.LEFT_HAND2:
                    case ContainerType.LOWER_BODY2:
                    case ContainerType.PET2:
                    case ContainerType.RIGHT_HAND2:
                    case ContainerType.SHOES2:
                    case ContainerType.SOCKS2:
                    case ContainerType.UPPER_BODY2:
                        string name = src.ToString();
                        name = name.Substring(0, name.Length - 1);
                        list = new List<Item>();
                        list.Add(parts[(EnumEquipSlot)Enum.Parse(typeof(EnumEquipSlot), name)]);
                        parts.Remove((EnumEquipSlot)Enum.Parse(typeof(EnumEquipSlot), name));
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
                    if (count > oldItem.Stack || count == 0)
                        count = oldItem.Stack;
                    newItem.Stack = (ushort)count;
                    oldItem.Stack -= (ushort)count;

                    if (oldItem.Stack == 0)
                    {
                        list.Remove(oldItem);
                        newItem.Slot = oldItem.Slot;
                        AddItem(dst, newItem, false);
                        oldItem.Slot = newItem.Slot;
                    }
                    else
                    {
                        if (oldItem.BaseData.itemType == ItemType.BULLET || oldItem.BaseData.itemType == ItemType.ARROW)//吞箭bug修复
                        {
                            //equipments.Add((EnumEquipSlot)Enum.Parse(typeof(EnumEquipSlot), src.ToString()), oldItem);
                        }
                        AddItem(dst, newItem, true);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
                return false;
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
                case ContainerType.GOLEMWAREHOUSE:
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
                case ContainerType.EFFECT:
                    {
                        Item item;
                        List<Item> newList = new List<Item>();
                        if (equipments.ContainsKey((EnumEquipSlot)Enum.Parse(typeof(EnumEquipSlot), container.ToString())))
                        {
                            item = equipments[(EnumEquipSlot)Enum.Parse(typeof(EnumEquipSlot), container.ToString())];
                            newList.Add(item);
                        }
                        return newList;
                    }
                case ContainerType.BACK2:
                case ContainerType.CHEST_ACCE2:
                case ContainerType.FACE2:
                case ContainerType.FACE_ACCE2:
                case ContainerType.HEAD2:
                case ContainerType.HEAD_ACCE2:
                case ContainerType.LEFT_HAND2:
                case ContainerType.LOWER_BODY2:
                case ContainerType.PET2:
                case ContainerType.RIGHT_HAND2:
                case ContainerType.SHOES2:
                case ContainerType.SOCKS2:
                case ContainerType.UPPER_BODY2:
                    {
                        Item item;
                        List<Item> newList = new List<Item>();
                        string name = container.ToString();
                        name = name.Substring(0, name.Length - 1);
                        if (parts.ContainsKey((EnumEquipSlot)Enum.Parse(typeof(EnumEquipSlot), name)))
                        {
                            item = parts[(EnumEquipSlot)Enum.Parse(typeof(EnumEquipSlot), name)];
                            newList.Add(item);
                        }
                        return newList;
                    }
                default:
                    return new List<Item>();
            }
        }

        public IConcurrentDictionary<EnumEquipSlot, Item> Equipments
        {
            get
            {
                return this.equipments;
            }
        }

        public IConcurrentDictionary<EnumEquipSlot, Item> Parts { get { return this.parts; } }

        public ContainerType GetContainerType(uint slotID)
        {
            for (int i = 2; i < 6; i++)
            {
                List<Item> list = items[(ContainerType)i];
                List<Item> result = new List<Item>();

                var query =
                        from it in list
                        where it.Slot == slotID && it.Stack > 0
                        select it;
                result = query.ToList();

                if (result.Count() == 0)
                {
                    continue;
                }
                return (ContainerType)i;
            }
            for (int i = 0; i < 14; i++)
            {
                if (!equipments.ContainsKey((EnumEquipSlot)i))
                    continue;
                Item item = equipments[(EnumEquipSlot)i];
                if (item.Slot == slotID)
                {
                    return (ContainerType)Enum.Parse(typeof(ContainerType), ((EnumEquipSlot)i).ToString());
                }
            }
            for (int i = 0; i < 14; i++)
            {
                if (!parts.ContainsKey((EnumEquipSlot)i))
                    continue;
                Item item = parts[(EnumEquipSlot)i];
                if (item.Slot == slotID)
                {
                    return (ContainerType)((ContainerType)(Enum.Parse(typeof(ContainerType), ((EnumEquipSlot)i).ToString())) + 200);
                }
            }
            return ContainerType.OTHER_WAREHOUSE;
        }

        public Item LastItem
        {
            get
            {
                return this.lastItem;
            }
        }

        public bool IsContainerEquip(ContainerType type)
        {
            if ((int)type >= (int)ContainerType.HEAD && (int)type <= (int)ContainerType.EFFECT)
                return true;
            else
                return false;
        }

        public bool IsContainerParts(ContainerType type)
        {
            if ((int)type >= (int)ContainerType.HEAD2 && (int)type <= (int)ContainerType.PET2)
                return true;
            else
                return false;
        }

        int panelCount(byte page, bool dominion)
        {
            int cl;
            if (dominion)
                cl = owner.DominionCL;
            else
                cl = owner.CL;
            int validCL = cl;
            if (validCL > page * 81)
            {
                int rest = (validCL - page * 81);
                if (rest > 81)
                    return 81;
                else
                    return rest;
            }
            else
                return 0;
        }

        public bool[,] validTable(byte page)
        {
            return validTable(page, owner.InDominionWorld);
        }

        public bool[,] validTable(byte page,bool dominion)
        {
            int validCount = panelCount(page,dominion);
            bool[,] table;
            int start;
            int x = 3, y = 0;
            int width = 3, height = 3;
            if (page == 0)
            {
                table = new bool[9, 9]{
                {true,true,true,false ,false ,false ,false ,false ,false },
                {true,true,true,false ,false ,false ,false ,false ,false },
                {true,true,true,false ,false ,false ,false ,false ,false },
                {false,false,false,false ,false ,false ,false ,false ,false },
                {false,false,false,false ,false ,false ,false ,false ,false },
                {false,false,false,false ,false ,false ,false ,false ,false },
                {false,false,false,false ,false ,false ,false ,false ,false },
                {false,false,false,false ,false ,false ,false ,false ,false },
                {false,false,false,false ,false ,false ,false ,false ,false },
                };
                start = 9;
                x = 3;
                y = 0;
                width = 3;
                height = 3;
            }
            else
            {
                table = new bool[9, 9];
                start = 0;
                x = 0;
                y = 0;
                width = 0;
                height = 0;
            }
            for (int i = start; i < validCount; i++)
            {
                table[x, y] = true;
                if (y < height)
                {
                    y++;
                }
                if (y == height)
                {
                    if (x >= width)
                    {
                        width++;
                        if (height == 0)
                            height++;
                        if (x > 0)
                            x = width - 1;
                        else
                            x++;
                        continue;
                    }
                }
                if (x >= 0 && y >= height)
                {
                    x--;
                }
                if (x == -1)
                {
                    if (y >= height)
                    {
                        x = width;
                        height++;
                        if (y > 0)
                            y = 0;
                        else
                            y++;
                    }
                }
            }
            return table;
        }

        public short[,] GetChipList(byte page)
        {
            return GetChipList(page, owner.InDominionWorld);
        }

        public short[,] GetChipList(byte page,bool dominion)
        {
            Dictionary<byte, DEMICPanel> chips;
            if (dominion)
                chips = ddemicChips;
            else
                chips = demicChips;

            short[,] res = new short[9, 9];
            if (chips.ContainsKey(page))
            {
                if (chips[page].EngageTask1 != 255)
                {
                    int x, y;
                    x = chips[page].EngageTask1 % 9;
                    y = chips[page].EngageTask1 / 9;
                    res[x, y] = 10000;
                }

                if (chips[page].EngageTask2 != 255)
                {
                    int x, y;
                    x = chips[page].EngageTask2 % 9;
                    y = chips[page].EngageTask2 / 9;
                    res[x, y] = 10000;
                }
                foreach (Chip i in chips[page].Chips)
                {                    
                    res[i.X, i.Y] = i.ChipID;
                }
            }
            return res;
        }

        int CountChip(short chipID,bool dominion)
        {
            DEMICPanel[] chips;
            if (dominion)
                chips = ddemicChips.Values.ToArray();
            else
                chips = demicChips.Values.ToArray();

            int res = 0;
            foreach (DEMICPanel i in chips)
            {
                foreach (Chip j in i.Chips)
                {
                    if (j.ChipID == chipID)
                        res++;
                }
            }

            return res;
        }

        /// <summary>
        /// 尝试插入芯片，成功则返回true，否则返回false
        /// </summary>
        /// <param name="page">DEMIC页</param>
        /// <param name="chip">芯片</param>
        /// <returns>是否成功</returns>
        public bool InsertChip(byte page, Chip chip)
        {
            return InsertChip(page, chip, owner.InDominionWorld);            
        }

        /// <summary>
        /// 尝试插入芯片，成功则返回true，否则返回false
        /// </summary>
        /// <param name="page">DEMIC页</param>
        /// <param name="chip">芯片</param>
        /// <param name="dominion">是否在恶魔界</param>
        /// <returns>是否成功</returns>
        public bool InsertChip(byte page, Chip chip, bool dominion)
        {
            return InsertChip(page, chip, validTable(page, dominion), dominion);
        }

        /// <summary>
        /// 尝试插入芯片，成功则返回true，否则返回false
        /// </summary>
        /// <param name="page">DEMIC页</param>
        /// <param name="chip">芯片</param>
        /// <param name="table">ＤＥＭＩＣ有效表</param>
        /// <param name="dominion">是否在恶魔界</param>
        /// <returns>是否成功</returns>
        public bool InsertChip(byte page, Chip chip,bool[,] table, bool dominion)
        {
            bool check = false;            
            Dictionary<byte, DEMICPanel> chips;
            int chipCount = CountChip(chip.ChipID, dominion);
            if (chipCount >= 10)
                return false;
            if (chip.Data.type == 30 && chipCount >= 1)
                return false;
            if (dominion)
                chips = ddemicChips;
            else
                chips = demicChips;
            if (chips.ContainsKey(page))
            {
                byte x1 = 255, y1 = 255, x2 = 255, y2 = 255;

                if (chips[page].EngageTask1 != 255)
                {
                    x1 = (byte)(chips[page].EngageTask1 % 9);
                    y1 = (byte)(chips[page].EngageTask1 / 9);
                }
                if (chips[page].EngageTask2 != 255)
                {
                    x2 = (byte)(chips[page].EngageTask2 % 9);
                    y2 = (byte)(chips[page].EngageTask2 / 9);
                }
                foreach (Chip i in chips[page].Chips)
                {

                    foreach (byte[] j in chip.Model.Cells)
                    {
                        int X = chip.X + j[0] - chip.Model.CenterX;
                        int Y = chip.Y + j[1] - chip.Model.CenterY;

                        if (!check)
                        {
                            if (x1 != 255 || y1 != 255)
                            {
                                if (X == x1 && Y == y1)
                                    return false;
                            }
                            if (x2 != 255 || y2 != 255)
                            {
                                if (X == x2 && Y == y2)
                                    return false;
                            }
                            if (!table[X, Y])
                                return false;
                        }                     
                        foreach (byte[] k in i.Model.Cells)
                        {
                            int X2 = i.X + k[0] - i.Model.CenterX;
                            int Y2 = i.Y + k[1] - i.Model.CenterY;
                            if ((X2 == X) && Y2 == Y)
                                return false;
                        }
                    }
                    check = true;
                }
                chips[page].Chips.Add(chip);
                return true;
            }
            else
                return false;
        }

        int countPossessionItem(List<Item> items)
        {
            int count = 0;
            foreach (Item i in items)
            {
                if (i.PossessionOwner != null)
                {
                    if (i.PossessionOwner.CharID != owner.CharID)
                        count++;
                }
            }
            return count;
        }

        public byte[] ToBytes()
        {
            string[] names = Enum.GetNames(typeof(ContainerType));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            System.IO.BinaryWriter bw = new System.IO.BinaryWriter(ms);
            bw.Write(version);
            bw.Write(names.Length);

            foreach (string i in names)
            {
                ContainerType container = (ContainerType)Enum.Parse(typeof(ContainerType), i);
                List<Item> list = this.GetContainer(container);
                bw.Write((int)container);
                bw.Write(list.Count - countPossessionItem(list));
                foreach (Item j in list)
                {
                    if (j.PossessionOwner != null)
                    {
                        if (j.PossessionOwner.CharID != owner.CharID)
                            continue;
                    }
                    j.ToStream(ms);
                }
            }

            bw.Write((byte)demicChips.Count);
            foreach (byte i in demicChips.Keys)
            {
                bw.Write(i);
                bw.Write(demicChips[i].EngageTask1);
                bw.Write(demicChips[i].EngageTask2);
                bw.Write((byte)demicChips[i].Chips.Count);
                foreach (Chip j in demicChips[i].Chips)
                {
                    bw.Write(j.ChipID);
                    bw.Write(j.X);
                    bw.Write(j.Y);
                }
            }
            bw.Write((byte)ddemicChips.Count);
            foreach (byte i in ddemicChips.Keys)
            {
                bw.Write(i);
                bw.Write(ddemicChips[i].EngageTask1);
                bw.Write(ddemicChips[i].EngageTask2);
                bw.Write((byte)ddemicChips[i].Chips.Count);
                foreach (Chip j in ddemicChips[i].Chips)
                {
                    bw.Write(j.ChipID);
                    bw.Write(j.X);
                    bw.Write(j.Y);
                }
            }
            ms.Flush();
            return ms.ToArray();
        }

        public void FromStream(System.IO.Stream ms)
        {
            try
            {
                System.IO.BinaryReader br = new System.IO.BinaryReader(ms);
                ushort _version = br.ReadUInt16();
                if (_version >= 1)
                {
                    int count = br.ReadInt32();
                    for (int i = 0; i < count; i++)
                    {
                        ContainerType type = (ContainerType)br.ReadInt32();
                        int count2 = br.ReadInt32();
                        for (int j = 0; j < count2; j++)
                        {
                            Item item = new Item();
                            item.FromStream(ms);
                            if ((item.RentalTime > DateTime.Now) || !item.Rental)
                                AddItem(type, item);
                        }
                    }
                }

                if (_version >= 2)
                {
                    demicChips.Clear();
                    ddemicChips.Clear();
                    byte count = br.ReadByte();
                    for (int i = 0; i < count; i++)
                    {
                        byte page = br.ReadByte();
                        DEMICPanel panel = new DEMICPanel();
                        if (_version >= 3)
                        {
                            panel.EngageTask1 = br.ReadByte();
                            panel.EngageTask2 = br.ReadByte();
                        }
                        byte count2 = br.ReadByte();
                        bool[,] table = validTable(page, false);
                        demicChips.Add(page, panel);
                        for (int j = 0; j < count2; j++)
                        {
                            Chip chip;
                            short chipID = br.ReadInt16();
                            byte x = br.ReadByte();
                            byte y = br.ReadByte();
                            if (ChipFactory.Instance.ByChipID.ContainsKey(chipID))
                            {
                                chip = new Chip(ChipFactory.Instance.ByChipID[chipID]);
                                chip.X = x;
                                chip.Y = y;
                                if (!InsertChip(page, chip, table, false))
                                    Logger.ShowWarning(string.Format("Cannot insert chip:{0} for character:{1}, droped!!!", chipID, owner.Name));
                            }
                        }
                    }
                    count = br.ReadByte();
                    for (int i = 0; i < count; i++)
                    {
                        byte page = br.ReadByte();                        
                        DEMICPanel panel = new DEMICPanel();
                        if (_version >= 3)
                        {
                            panel.EngageTask1 = br.ReadByte();
                            panel.EngageTask2 = br.ReadByte();
                        } 
                        bool[,] table = validTable(page, true);
                        byte count2 = br.ReadByte();
                        ddemicChips.Add(page, panel);
                        for (int j = 0; j < count2; j++)
                        {
                            Chip chip;
                            short chipID = br.ReadInt16();
                            byte x = br.ReadByte();
                            byte y = br.ReadByte();
                            if (ChipFactory.Instance.ByChipID.ContainsKey(chipID))
                            {
                                chip = new Chip(ChipFactory.Instance.ByChipID[chipID]);
                                chip.X = x;
                                chip.Y = y;
                                if (!InsertChip(page, chip, table, true))
                                    Logger.ShowWarning(string.Format("Cannot insert chip:{0} for character:{1}, droped!!!", chipID, owner.Name));
                            }
                        }
                    }

                    if (!demicChips.ContainsKey(100))
                        demicChips.Add(100, new DEMICPanel());
                    if (!demicChips.ContainsKey(101))
                        demicChips.Add(101, new DEMICPanel());
                }

            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
        }

        public byte[] WareToBytes()
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            System.IO.BinaryWriter bw = new System.IO.BinaryWriter(ms);
            bw.Write(version);
            bw.Write(ware.Count);
            foreach (WarehousePlace i in ware.Keys)
            {
                List<Item> list = ware[i];
                bw.Write((byte)i);
                bw.Write((ushort)list.Count);
                foreach (Item j in list)
                {
                    j.ToStream(ms);
                }
            }
            ms.Flush();
            return ms.ToArray();
        }

        public void WareFromSteam(System.IO.Stream ms)
        {
            try
            {
                System.IO.BinaryReader br = new System.IO.BinaryReader(ms);
                ushort _version = br.ReadUInt16();
                if (_version >= 1)
                {
                    int count = br.ReadInt32();
                    for (int i = 0; i < count; i++)
                    {
                        WarehousePlace place = (WarehousePlace)br.ReadByte();
                        ushort count2 = br.ReadUInt16();
                        for (int j = 0; j < count2; j++)
                        {
                            Item item = new Item();
                            item.FromStream(ms);
                            this.AddWareItem(place, item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
        }
    }
}

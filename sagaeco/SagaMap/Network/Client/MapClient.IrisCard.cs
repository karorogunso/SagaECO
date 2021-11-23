using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net;
using System.Net.Sockets;

using SagaDB;
using SagaDB.Item;
using SagaDB.Actor;
using SagaDB.Npc;
using SagaDB.Quests;
using SagaDB.Party;
using SagaDB.Iris;
using SagaLib;
using SagaMap;
using SagaMap.Manager;
using SagaMap.Packets.Client;
using SagaMap.Packets.Server;

namespace SagaMap.Network.Client
{
    public partial class MapClient
    {
        public bool irisAddSlot = false;
        public bool irisCardAssemble = false;
        uint irisAddSlotMaterial = 0;
        uint irisAddSlotItem = 0;
        uint irisCardItem = 0;

        public void OnIrisGacha(CSMG_IRIS_GACHA_DRAW p)
        {
            uint itemID = p.ItemID;
            if(CountItem(itemID) > 0)
            {
                if (IrisGachaFactory.Instance.IrisGacha.ContainsKey(itemID))
                {
                    IrisGacha gacha = IrisGachaFactory.Instance.IrisGacha[itemID];
                    Dictionary<uint, byte> cards = new Dictionary<uint, byte>();
                    DeleteItemID(itemID, 1, true);
                    foreach (var item in IrisCardFactory.Instance.Items.Values)
                        if (item.Page == gacha.pageID)
                            cards.Add(item.ID, (byte)item.Rarity);
                    List<uint> results = new List<uint>();
                    for (int i = 0; i < gacha.count; i++)
                    {
                        int lottery = Global.Random.Next(0, 1000);
                        List<uint> Lcards = new List<uint>();
                        byte rank = 1;
                        if (lottery < 5) rank = 4;
                        else if (lottery < 35) rank = 3;
                        else if (lottery < 185) rank = 2;
                        else rank = 1;
                        if (i == 9)//保底
                        {
                            rank = 2;//保底变R
                            if (lottery < 50) rank = 3;
                            if (lottery < 8) rank = 4;
                        }
                        foreach (var i2 in cards)
                            if (i2.Value == rank)
                                Lcards.Add(i2.Key);
                        uint itemid = 0;
                        if (Lcards.Count == 1)
                            itemid = Lcards[0];
                        else
                            itemid = Lcards[Global.Random.Next(0, Lcards.Count - 1)];
                        Item item = ItemFactory.Instance.GetItem(itemid);
                        item.Stack = 1;
                        item.Identified = true;
                        AddItem(item, true);
                        results.Add(itemid);
                    }
                    SSMG_IRIS_GACHA_RESULT p2 = new SSMG_IRIS_GACHA_RESULT();
                    p2.ItemIDs = results;
                    netIO.SendPacket(p2);
                }
            }
        }
        public void OnIrisCardAssembleCancel(CSMG_IRIS_CARD_ASSEMBLE_CANCEL p)
        {
            irisCardAssemble = false;
            Packet p1 = new Packet(2);
            p1.ID = 0x05DD;//remove move lock
            this.netIO.SendPacket(p1);
        }

        public void OnIrisCardAssemble(CSMG_IRIS_CARD_ASSEMBLE p)
        {
            uint cardID = p.CardID;
            if (CountItem(cardID) > 0)
            {
                if (IrisCardFactory.Instance.Items.ContainsKey(cardID))
                {
                    IrisCard card = IrisCardFactory.Instance.Items[cardID];
                    if (card.NextCard != 0)
                    {
                        int[] rates = new int[4] { 90, 60, 30, 5 };
                        int[] counts = new int[4] { 10, 2, 2, 2 };
                        int rate = rates[card.Rank];
                        int count = counts[card.Rank];
                        if (CountItem(cardID) >= count)
                        {
                            if (this.chara.Gold >= 5000)
                            {
                                this.chara.Gold -= 5000;
                                DeleteItemID(cardID, (ushort)count, true);
                                if (Global.Random.Next(0, 99) < rate)
                                {
                                    AddItem(ItemFactory.Instance.GetItem(card.NextCard), true);
                                    Packets.Server.SSMG_IRIS_CARD_ASSEMBLE_RESULT p1 = new SagaMap.Packets.Server.SSMG_IRIS_CARD_ASSEMBLE_RESULT();
                                    p1.Result = SagaMap.Packets.Server.SSMG_IRIS_CARD_ASSEMBLE_RESULT.Results.OK;
                                    this.netIO.SendPacket(p1);
                                }
                                else
                                {
                                    Packets.Server.SSMG_IRIS_CARD_ASSEMBLE_RESULT p1 = new SagaMap.Packets.Server.SSMG_IRIS_CARD_ASSEMBLE_RESULT();
                                    p1.Result = SagaMap.Packets.Server.SSMG_IRIS_CARD_ASSEMBLE_RESULT.Results.FAILED;
                                    this.netIO.SendPacket(p1);
                                    irisCardAssemble = false;
                                }
                            }
                            else
                            {
                                Packets.Server.SSMG_IRIS_CARD_ASSEMBLE_RESULT p1 = new SagaMap.Packets.Server.SSMG_IRIS_CARD_ASSEMBLE_RESULT();
                                p1.Result = SagaMap.Packets.Server.SSMG_IRIS_CARD_ASSEMBLE_RESULT.Results.NOT_ENOUGH_GOLD;
                                this.netIO.SendPacket(p1);
                                irisCardAssemble = false;
                            }
                        }
                        else
                        {
                            Packets.Server.SSMG_IRIS_CARD_ASSEMBLE_RESULT p1 = new SagaMap.Packets.Server.SSMG_IRIS_CARD_ASSEMBLE_RESULT();
                            p1.Result = SagaMap.Packets.Server.SSMG_IRIS_CARD_ASSEMBLE_RESULT.Results.NOT_ENOUGH_ITEM;
                            this.netIO.SendPacket(p1);
                            irisCardAssemble = false;
                        }
                    }
                    else
                    {
                        Packets.Server.SSMG_IRIS_CARD_ASSEMBLE_RESULT p1 = new SagaMap.Packets.Server.SSMG_IRIS_CARD_ASSEMBLE_RESULT();
                        p1.Result = SagaMap.Packets.Server.SSMG_IRIS_CARD_ASSEMBLE_RESULT.Results.NO_ITEM;
                        this.netIO.SendPacket(p1);
                        irisCardAssemble = false;
                    }
                }
            }
            else
            {
                Packets.Server.SSMG_IRIS_CARD_ASSEMBLE_RESULT p1 = new SagaMap.Packets.Server.SSMG_IRIS_CARD_ASSEMBLE_RESULT();
                p1.Result = SagaMap.Packets.Server.SSMG_IRIS_CARD_ASSEMBLE_RESULT.Results.NO_ITEM;
                this.netIO.SendPacket(p1);
                irisCardAssemble = false;
            }
        }

        public void OnIrisCardClose(CSMG_IRIS_CARD_CLOSE p)
        {
            irisCardItem = 0;
        }

        public void OnIrisCardLock(CSMG_IRIS_CARD_LOCK p)
        {
            Item item = this.chara.Inventory.GetItem(irisCardItem);
            if (item != null)
            {
                item.Locked = true;
                SendItemIdentify(item.Slot);
                Packets.Server.SSMG_IRIS_CARD_LOCK_RESULT p1 = new SagaMap.Packets.Server.SSMG_IRIS_CARD_LOCK_RESULT();
                this.netIO.SendPacket(p1);
            }
        }

        public void OnIrisCardRemove(CSMG_IRIS_CARD_REMOVE p)
        {
            Item item = this.chara.Inventory.GetItem(irisCardItem);
            if (item != null)
            {
                if (!item.Locked)
                {
                    if (p.CardSlot < item.Cards.Count)
                    {
                        Packets.Server.SSMG_IRIS_CARD_REMOVE_RESULT p1 = new SagaMap.Packets.Server.SSMG_IRIS_CARD_REMOVE_RESULT();
                        p1.Result = SagaMap.Packets.Server.SSMG_IRIS_CARD_REMOVE_RESULT.Results.OK;
                        this.netIO.SendPacket(p1);

                        IrisCard card = item.Cards[p.CardSlot];
                        AddItem(ItemFactory.Instance.GetItem(card.ID), true);
                        item.Cards.RemoveAt(p.CardSlot);
                        SendItemCardInfo(item);
                        SendItemCardAbility(item);

                    }
                    else
                    {
                        Packets.Server.SSMG_IRIS_CARD_REMOVE_RESULT p1 = new SagaMap.Packets.Server.SSMG_IRIS_CARD_REMOVE_RESULT();
                        p1.Result = SagaMap.Packets.Server.SSMG_IRIS_CARD_REMOVE_RESULT.Results.FAILED;
                        this.netIO.SendPacket(p1);
                    }
                }
                else
                {
                    Packets.Server.SSMG_IRIS_CARD_REMOVE_RESULT p1 = new SagaMap.Packets.Server.SSMG_IRIS_CARD_REMOVE_RESULT();
                    p1.Result = SagaMap.Packets.Server.SSMG_IRIS_CARD_REMOVE_RESULT.Results.FAILED;
                    this.netIO.SendPacket(p1);
                }
            }
            else
            {
                Packets.Server.SSMG_IRIS_CARD_REMOVE_RESULT p1 = new SagaMap.Packets.Server.SSMG_IRIS_CARD_REMOVE_RESULT();
                p1.Result = SagaMap.Packets.Server.SSMG_IRIS_CARD_REMOVE_RESULT.Results.FAILED;
                this.netIO.SendPacket(p1);
            }
        }

        public void OnIrisCardInsert(CSMG_IRIS_CARD_INSERT p)
        {
            Item item = this.chara.Inventory.GetItem(irisCardItem);
            if (item != null)
            {
                if (item.Cards.Count < item.CurrentSlot)
                {
                    Item card = this.chara.Inventory.GetItem(p.InventorySlot);
                    if (card != null)
                    {
                        if (card.BaseData.itemType == ItemType.IRIS_CARD)
                        {
                            if (IrisCardFactory.Instance.Items.ContainsKey(card.BaseData.id))
                            {
                                DeleteItem(card.Slot, 1, true);
                                Packets.Server.SSMG_IRIS_CARD_INSERT_RESULT p1 = new SagaMap.Packets.Server.SSMG_IRIS_CARD_INSERT_RESULT();
                                p1.Result = SagaMap.Packets.Server.SSMG_IRIS_CARD_INSERT_RESULT.Results.OK;
                                this.netIO.SendPacket(p1);
                                IrisCard cardInfo = IrisCardFactory.Instance.Items[card.BaseData.id];
                                item.Cards.Add(cardInfo);
                                SendItemCardInfo(item);
                                SendItemCardAbility(item);
                                PC.StatusFactory.Instance.CalcStatus(chara);
                            }
                            else
                            {
                                Packets.Server.SSMG_IRIS_CARD_INSERT_RESULT p1 = new SagaMap.Packets.Server.SSMG_IRIS_CARD_INSERT_RESULT();
                                p1.Result = SagaMap.Packets.Server.SSMG_IRIS_CARD_INSERT_RESULT.Results.CANNOT_SET;
                                this.netIO.SendPacket(p1);
                            }
                        }
                    }
                }
                else
                {
                    Packets.Server.SSMG_IRIS_CARD_INSERT_RESULT p1 = new SagaMap.Packets.Server.SSMG_IRIS_CARD_INSERT_RESULT();
                    p1.Result = SagaMap.Packets.Server.SSMG_IRIS_CARD_INSERT_RESULT.Results.SLOT_OVER;
                    this.netIO.SendPacket(p1);
                }
            }
        }

        public void OnIrisCardOpen(CSMG_IRIS_CARD_OPEN p)
        {
            Item item = this.chara.Inventory.GetItem(p.InventorySlot);
            if (item != null)
            {
                if (item.CurrentSlot > 0)
                {
                    irisCardItem = item.Slot;
                    Packets.Server.SSMG_IRIS_CARD_OPEN_RESULT p1 = new SagaMap.Packets.Server.SSMG_IRIS_CARD_OPEN_RESULT();
                    p1.Result = SagaMap.Packets.Server.SSMG_IRIS_CARD_OPEN_RESULT.Results.OK;
                    this.netIO.SendPacket(p1);

                    SendItemCardAbility(item);
                }
                else
                {
                    Packets.Server.SSMG_IRIS_CARD_OPEN_RESULT p1 = new SagaMap.Packets.Server.SSMG_IRIS_CARD_OPEN_RESULT();
                    p1.Result = SagaMap.Packets.Server.SSMG_IRIS_CARD_OPEN_RESULT.Results.NO_SLOT;
                    this.netIO.SendPacket(p1);
                }
            }
            else
            {
                Packets.Server.SSMG_IRIS_CARD_OPEN_RESULT p1 = new SagaMap.Packets.Server.SSMG_IRIS_CARD_OPEN_RESULT();
                p1.Result = SagaMap.Packets.Server.SSMG_IRIS_CARD_OPEN_RESULT.Results.NO_ITEM;
                this.netIO.SendPacket(p1);
            }
        }

        public void OnIrisAddSlotConfirm(CSMG_IRIS_ADD_SLOT_CONFIRM p)
        {
            if (irisAddSlot)
            {
                Item item = this.Character.Inventory.GetItem(irisAddSlotItem);
                if (item != null)
                {
                    int gold = item.BaseData.possibleLv * 1000;
                    if (this.chara.Account.AccountID == 68)
                        gold = 0;
                    uint material = irisAddSlotMaterial;
                    if (CountItem(material) > 0)
                    {
                        if (this.chara.Gold > gold)
                        {
                            if (item.CurrentSlot < 5)
                            {
                                this.chara.Gold -= gold;
                                DeleteItemID(material, 1, true);
                                int baseRate = 90 - item.CurrentSlot * 20;
                                if (baseRate < 0)
                                    baseRate = 5;
                                if (Global.Random.Next(0, 99) < baseRate)
                                {
                                    Packets.Server.SSMG_IRIS_ADD_SLOT_RESULT p1 = new SagaMap.Packets.Server.SSMG_IRIS_ADD_SLOT_RESULT();
                                    p1.Result = SagaMap.Packets.Server.SSMG_IRIS_ADD_SLOT_RESULT.Results.OK;
                                    this.netIO.SendPacket(p1);
                                    SendEffect(5145);
                                    item.CurrentSlot++;
                                    SendItemInfo(item);
                                }
                                else if (CountItem(16001500) > 0)
                                {
                                    DeleteItemID(16001500, 1, true);
                                    this.SendSystemMessage("装备打洞失败！使用了一本防爆书（打洞）。");
                                    Packets.Server.SSMG_IRIS_ADD_SLOT_RESULT p1 = new SagaMap.Packets.Server.SSMG_IRIS_ADD_SLOT_RESULT();
                                    p1.Result = SagaMap.Packets.Server.SSMG_IRIS_ADD_SLOT_RESULT.Results.Failed;
                                    this.netIO.SendPacket(p1);
                                }
                                else
                                {
                                    DeleteItem(item.Slot, 1, true);
                                    Packets.Server.SSMG_IRIS_ADD_SLOT_RESULT p1 = new SagaMap.Packets.Server.SSMG_IRIS_ADD_SLOT_RESULT();
                                    p1.Result = SagaMap.Packets.Server.SSMG_IRIS_ADD_SLOT_RESULT.Results.Failed;
                                    this.netIO.SendPacket(p1);
                                }
                            }
                            else
                            {
                                Packets.Server.SSMG_IRIS_ADD_SLOT_RESULT p1 = new SagaMap.Packets.Server.SSMG_IRIS_ADD_SLOT_RESULT();
                                p1.Result = SagaMap.Packets.Server.SSMG_IRIS_ADD_SLOT_RESULT.Results.Failed;
                                this.netIO.SendPacket(p1);
                                this.irisAddSlot = false;
                            }
                        }
                        else
                        {
                            Packets.Server.SSMG_IRIS_ADD_SLOT_RESULT p1 = new SagaMap.Packets.Server.SSMG_IRIS_ADD_SLOT_RESULT();
                            p1.Result = SagaMap.Packets.Server.SSMG_IRIS_ADD_SLOT_RESULT.Results.NOT_ENOUGH_GOLD;
                            this.netIO.SendPacket(p1);
                        }
                    }
                    else
                    {
                        Packets.Server.SSMG_IRIS_ADD_SLOT_RESULT p1 = new SagaMap.Packets.Server.SSMG_IRIS_ADD_SLOT_RESULT();
                        p1.Result = SagaMap.Packets.Server.SSMG_IRIS_ADD_SLOT_RESULT.Results.NO_RIGHT_MATERIAL;
                        this.netIO.SendPacket(p1);
                    }
                }
                else
                {
                    Packets.Server.SSMG_IRIS_ADD_SLOT_RESULT p1 = new SagaMap.Packets.Server.SSMG_IRIS_ADD_SLOT_RESULT();
                    p1.Result = SagaMap.Packets.Server.SSMG_IRIS_ADD_SLOT_RESULT.Results.NO_ITEM;
                    this.netIO.SendPacket(p1);
                    irisAddSlot = false;
                }
            }
        }

        public void OnIrisAddSlotCancel(CSMG_IRIS_ADD_SLOT_CANCEL p)
        {
            irisAddSlot = false;
        }

        public void OnIrisAddSlotItemSelect(CSMG_IRIS_ADD_SLOT_ITEM_SELECT p)
        {
            if (irisAddSlot)
            {
                Item item = this.Character.Inventory.GetItem(p.InventorySlot);
                if (item != null)
                {
                    int gold = item.BaseData.possibleLv * 1000;
                    uint material = 0;
                    if (item.BaseData.possibleLv <= 30)
                        material = 10073000;
                    else if (item.BaseData.possibleLv <= 70)
                        material = 10073100;
                    else
                        material = 10073200;
                    if (this.chara.Gold > gold)
                    {
                        if (item.CurrentSlot < 5)
                        {
                            this.irisAddSlotMaterial = material;
                            this.irisAddSlotItem = item.Slot;

                            Packets.Server.SSMG_IRIS_ADD_SLOT_MATERIAL p1 = new SagaMap.Packets.Server.SSMG_IRIS_ADD_SLOT_MATERIAL();
                            p1.Slot = 1;
                            p1.Material = material;
                            p1.Gold = gold;
                            this.netIO.SendPacket(p1);
                        }
                        else
                        {
                            Packets.Server.SSMG_IRIS_ADD_SLOT_RESULT p1 = new SagaMap.Packets.Server.SSMG_IRIS_ADD_SLOT_RESULT();
                            p1.Result = SagaMap.Packets.Server.SSMG_IRIS_ADD_SLOT_RESULT.Results.Failed;
                            this.netIO.SendPacket(p1);
                        }
                    }
                    else
                    {
                        Packets.Server.SSMG_IRIS_ADD_SLOT_RESULT p1 = new SagaMap.Packets.Server.SSMG_IRIS_ADD_SLOT_RESULT();
                        p1.Result = SagaMap.Packets.Server.SSMG_IRIS_ADD_SLOT_RESULT.Results.NOT_ENOUGH_GOLD;
                        this.netIO.SendPacket(p1);
                    }
                }
                else
                {
                    Packets.Server.SSMG_IRIS_ADD_SLOT_RESULT p1 = new SagaMap.Packets.Server.SSMG_IRIS_ADD_SLOT_RESULT();
                    p1.Result = SagaMap.Packets.Server.SSMG_IRIS_ADD_SLOT_RESULT.Results.NO_ITEM;
                    this.netIO.SendPacket(p1);
                    irisAddSlot = false;
                }
            }
        }

        public void SendItemCardInfo(Item item)
        {
            Packets.Server.SSMG_ITEM_IRIS_CARD_INFO p = new SagaMap.Packets.Server.SSMG_ITEM_IRIS_CARD_INFO();
            p.Item = item;
            this.netIO.SendPacket(p);
        }

        public void SendItemCardAbility(Item item)
        {
            Packets.Server.SSMG_IRIS_CARD_ITEM_ABILITY p = new SagaMap.Packets.Server.SSMG_IRIS_CARD_ITEM_ABILITY();
            p.Type = SagaMap.Packets.Server.SSMG_IRIS_CARD_ITEM_ABILITY.Types.Deck;
            p.AbilityVectors = item.AbilityVectors(true);
            p.VectorValues = item.VectorValues(true, false).Values.ToList();
            p.VectorLevels = item.VectorValues(true, true).Values.ToList();
            Dictionary<ReleaseAbility, int> release = item.ReleaseAbilities(true);
            p.ReleaseAbilities = release.Keys.ToList();
            p.AbilityValues = release.Values.ToList();
            if (item.EquipSlot[0] == EnumEquipSlot.RIGHT_HAND)
                p.ElementsAttack = item.IrisElements(true);
            else
                p.ElementsAttack = Item.ElementsZero();
            if (item.EquipSlot[0] == EnumEquipSlot.UPPER_BODY || item.EquipSlot[0] == EnumEquipSlot.CHEST_ACCE)
                p.ElementsDefence = item.IrisElements(true);
            else
                p.ElementsDefence = Item.ElementsZero();
            this.netIO.SendPacket(p);

            p = new SagaMap.Packets.Server.SSMG_IRIS_CARD_ITEM_ABILITY();
            p.Type = SagaMap.Packets.Server.SSMG_IRIS_CARD_ITEM_ABILITY.Types.Max;
            p.AbilityVectors = item.AbilityVectors(false);
            p.VectorValues = item.VectorValues(false, false).Values.ToList();
            p.VectorLevels = item.VectorValues(false, true).Values.ToList();
            release = item.ReleaseAbilities(false);
            p.ReleaseAbilities = release.Keys.ToList();
            p.AbilityValues = release.Values.ToList();         
            if (item.EquipSlot[0] == EnumEquipSlot.RIGHT_HAND)
                p.ElementsAttack = item.IrisElements(false);
            else
                p.ElementsAttack = Item.ElementsZero();
            if (item.EquipSlot[0] == EnumEquipSlot.UPPER_BODY || item.EquipSlot[0] == EnumEquipSlot.CHEST_ACCE)
                p.ElementsDefence = item.IrisElements(false);
            else
                p.ElementsDefence = Item.ElementsZero();
            this.netIO.SendPacket(p);

            p = new SagaMap.Packets.Server.SSMG_IRIS_CARD_ITEM_ABILITY();
            p.Type = SagaMap.Packets.Server.SSMG_IRIS_CARD_ITEM_ABILITY.Types.Total;
            p.AbilityVectors = this.chara.IrisAbilityValues.Keys.ToList();
            p.VectorValues = this.chara.IrisAbilityValues.Values.ToList();
            p.VectorLevels = this.chara.IrisAbilityLevels.Values.ToList();
            release = Item.ReleaseAbilities(this.chara.IrisAbilityLevels);
            p.ReleaseAbilities = release.Keys.ToList();
            p.AbilityValues = release.Values.ToList();
            p.ElementsAttack = this.chara.Status.attackelements_iris;
            p.ElementsDefence = this.chara.Status.elements_iris;
            this.netIO.SendPacket(p);
        }
    }
}

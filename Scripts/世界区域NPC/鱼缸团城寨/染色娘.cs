
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.FF
{
    public class S80000015 : Event
    {
        public S80000015()
        {
            this.EventID = 80000015;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "找她干什么呢？", "", "装备染色(5,000G)", "外观合成(90,000G)", "离开"))
            {
                case 1:
                    Say(pc, 131, "HI~ 我是染色娘哦！$R$R如果你带来可以更变颜色的装备$R花费5,000G我就可以为你染色。$R$R※注意：染色后，强化效果会丢失。", "艾丽娅");
                    Say(pc, 131, "啊对了，$R$R你得到的KUJI可能不是原色，$R有可能需要先染回原色哦。$R价格都一样啦", "艾丽娅");
                    switch (Select(pc, "请选择染色类型", "", "恢复原色", "从原色染成其他颜色", "离开"))
                    {
                        case 1:
                            ConfirmOriItems(pc);
                            break;
                        case 2:
                            ConfirmExchangeItems(pc);
                            break;
                    }

                    break;
                case 2:
                    Say(pc, 131, "更换外观比较贵哦，$R需要90,000G一次呢。$R$R因为部分装备可以改变装备特性，$R比如让单手武器用双手武器的外观啦！$R$R※作为外观的道具会被吃掉，$R确定要更换吗？", "艾丽娅");
                    变换外观(pc);
                    break;
                case 3:
                    break;
            }
        }

        void 变换外观(ActorPC pc)
        {
            List<SagaDB.Item.Item> OriEquips = new List<SagaDB.Item.Item>();
            List<string> Orinames = new List<string>();
            List<SagaDB.Item.Item> NewEquips = new List<SagaDB.Item.Item>();
            List<string> Newnames = new List<string>();

            List<ItemType> its = new List<ItemType>();
            ItemType it = ItemType.ACCESORY_FACE;
            switch (Select(pc, "请选择要改变外观的装备类型", "", "头部装备", "脸部装备", "背部装备", "鞋子", "袜子", "武器装备", "衣服装备", "项链", "离开"))
            {
                case 1:
                    its.Add(ItemType.ACCESORY_HEAD);
                    its.Add(ItemType.PARTS_HEAD);
                    its.Add(ItemType.HELM);
                    its.Add(ItemType.FULLFACE);
                    break;
                case 2:
                    its.Add(ItemType.ACCESORY_FACE);
                    break;
                case 3:
                    its.Add(ItemType.PARTS_BACK);
                    its.Add(ItemType.BACKPACK);
                    its.Add(ItemType.BACK_DEMON);
                    break;
                case 4:
                    its.Add(ItemType.SHOES);
                    its.Add(ItemType.BOOTS);
                    its.Add(ItemType.HALFBOOTS);
                    its.Add(ItemType.LONGBOOTS);
                    break;
                case 5:
                    its.Add(ItemType.SOCKS);
                    break;
                case 6:
                    switch (Select(pc, "请选择要改变外观的武器类型", "", "剑类/细剑类", "杖类", "书类", "矛类", "钝器/斧类", "爪类", "乐器类", "离开"))
                    {
                        case 1:
                            its.Add(ItemType.SWORD);
                            its.Add(ItemType.SHORT_SWORD);
                            its.Add(ItemType.RAPIER);
                            break;
                        case 2:
                            its.Add(ItemType.STAFF);
                            break;
                        case 3:
                            its.Add(ItemType.BOOK);
                            break;
                        case 4:
                            its.Add(ItemType.SPEAR);
                            break;
                        case 5:
                            its.Add(ItemType.AXE);
                            its.Add(ItemType.HAMMER);
                            break;
                        case 6:
                            its.Add(ItemType.CLAW);
                            break;
                        case 7:
                            its.Add(ItemType.STRINGS);
                            break;
                    }
                    break;
                case 7:
                    switch (Select(pc, "请选择合成类型", "", "上衣（可将连衣裙可以改变为上衣类）", "下衣装备", "离开"))
                    {
                        case 1:
                            its.Add(ItemType.PARTS_BODY);
                            its.Add(ItemType.ARMOR_UPPER);
                            its.Add(ItemType.OVERALLS);
                            its.Add(ItemType.ONEPIECE);
                            its.Add(ItemType.BODYSUIT);
                            its.Add(ItemType.WEDDING);
                            break;
                        case 2:
                            its.Add(ItemType.ARMOR_LOWER);
                            break;
                    }
                    break;
                case 8:
                    its.Add(ItemType.ACCESORY_NECK);
                    break;
            }
            foreach (var i in pc.Inventory.Items[ContainerType.BODY])
            {
                if (its.Contains(i.BaseData.itemType) && i.Stack > 0)
                {
                    OriEquips.Add(i);
                    Orinames.Add(i.BaseData.name);
                }
            }
            Orinames.Add("放弃");
            if (its.Count == 0) return;
            if (OriEquips.Count < 1)
            {
                Say(pc, 131, "你身上没有装备", "艾丽娅");
                return;
            }
            if (OriEquips.Count > 15)
            {
                Say(pc, 131, "你带了太多装备了！", "艾丽娅");
                return;
            }
            int set = Select(pc, "※请选择需要进行【变换外观】的原装备", "", Orinames.ToArray());
            if (set == Orinames.Count) return;
            SagaDB.Item.Item es = OriEquips[set - 1];

            foreach (var i in pc.Inventory.Items[ContainerType.BODY])
            {
                if (its.Contains(i.BaseData.itemType) && i != es && i.Stack > 0 && i.PictID == 0)
                {
                    NewEquips.Add(i);
                    Newnames.Add(i.BaseData.name);
                }
            }
            if (es.BaseData.itemType == ItemType.ACCESORY_NECK || es.BaseData.itemType == ItemType.ARMOR_LOWER || es.BaseData.itemType == ItemType.BACKPACK
                || es.BaseData.itemType == ItemType.SOCKS || es.BaseData.itemType == ItemType.SHOES || es.BaseData.itemType == ItemType.BOOTS
                || es.BaseData.itemType == ItemType.HALFBOOTS || es.BaseData.itemType == ItemType.LONGBOOTS)
            {
                NewEquips.Add(ItemFactory.Instance.GetItem(10000000));
                Newnames.Add("隐藏外观(不需要材料装备)");
            }

            Newnames.Add("放弃");
            if (NewEquips.Count < 1)
            {
                Say(pc, 131, "你身上没有装备！", "艾丽娅");
                return;
            }
            if (NewEquips.Count > 15)
            {
                Say(pc, 131, "你带了太多装备了！", "艾丽娅");
                return;
            }
            set = Select(pc, "※请选择要使用的外观", "", Newnames.ToArray());
            if (set == Newnames.Count) return;
            SagaDB.Item.Item ns = NewEquips[set - 1];

            int gold2 = 90000;
            if (Select(pc, "【" + es.BaseData.name + "】使用【" + ns.BaseData.name + "】外观？", "", "确定换外观！（" + gold2.ToString() + "G)", "还是算了") == 1)
            {
                int gold = 90000;
                if (pc.Gold < gold)
                {
                    Say(pc, 131, "你钱不够", "艾丽娅");
                    return;
                }
                if (CountItem(pc, es.ItemID) > 0 && (CountItem(pc, ns.ItemID) > 0 || ns.ItemID == 10000000))
                {
                    pc.Gold -= gold;
                    es.PictID = ns.BaseData.imageID;
                    if (ns.ItemID == 10000000)
                        es.PictID = 1000;
                    if (ns.ItemID != 10000000)
                        SagaMap.Network.Client.MapClient.FromActorPC(pc).DeleteItem(ns.Slot, 1, true);
                    SagaMap.Network.Client.MapClient.FromActorPC(pc).SendItemInfo(es.Slot);
                    PlaySound(pc, 3432, false, 100, 50);
                    SagaMap.Network.Client.MapClient.FromActorPC(pc).SendSystemMessage("外观转移完成！");
                }
            }
        }
        void ConfirmOriItems(ActorPC pc)
        {
            List<uint> SitemList = new List<uint>();
            List<string> names = new List<string>();
            foreach (var i in pc.Inventory.Items[ContainerType.BODY])
            {
                if (ExchangeFactory.Instance.OriItems.ContainsKey(i.ItemID) && i.Stack != 0 && SitemList.Count < 13 && i.Refine == 0)
                {
                    SitemList.Add(i.ItemID);
                    names.Add(i.BaseData.name);
                }
            }
            names.Add("放弃");
            if (SitemList.Count < 1)
            {
                Say(pc, 131, "你身上没有可以染色的装备呢。", "艾丽娅");
                return;
            }
            int set = Select(pc, "※请选择需要【染回原色】的装备", "", names.ToArray());
            if (set == names.Count) return;
            uint SitemID = SitemList[set - 1];
            uint TitemID = ExchangeFactory.Instance.OriItems[SitemID];
            SagaDB.Item.Item k = ItemFactory.Instance.GetItem(TitemID);
            if (Select(pc, "确定要把【" + k.BaseData.name + "】染回原色吗？", "", "是的", "算了") == 1)
            {
                if (pc.Gold < 5000)
                {
                    Say(pc, 131, "很抱歉，你的钱不足够哦", "艾丽娅");
                    return;
                }
                if (CountItem(pc, SitemID) < 1)
                {
                    Say(pc, 131, "很抱歉，你的材料不足够哦", "艾丽娅");
                    return;
                }
                pc.Gold -= 5000;
                TakeItem(pc, SitemID, 1);
                GiveItem(pc, TitemID, 1);
                Say(pc, 131, "谢谢光临~", "艾丽娅");
            }
        }
        void ConfirmExchangeItems(ActorPC pc)
        {
            List<uint> SitemList = new List<uint>();
            List<string> names = new List<string>();

            //获得原装备列表
            foreach (var i in pc.Inventory.Items[ContainerType.BODY])
            {
                if (ExchangeFactory.Instance.ExchangeItems.ContainsKey(i.ItemID) && i.Stack != 0 && SitemList.Count < 13 && i.Refine == 0)
                {
                    SitemList.Add(i.ItemID);
                    names.Add(i.BaseData.name);
                }
            }
            names.Add("放弃");
            if (SitemList.Count < 1)
            {
                Say(pc, 131, "你身上没有可以染色的装备呢。", "艾丽娅");
                return;
            }
            int set = Select(pc, "※请选择需要染色的装备", "", names.ToArray());
            if (set == names.Count) return;
            uint SitemID = SitemList[set - 1];

            //获得可变换列表
            List<uint> TitemList = ExchangeFactory.Instance.ExchangeItems[SitemID];
            names = new List<string>();
            foreach (var item in TitemList)
            {
                SagaDB.Item.Item i = ItemFactory.Instance.GetItem(item);
                names.Add(i.BaseData.name);
            }
            names.Add("放弃");
            set = Select(pc, "※要染色成什么样子呢？", "", names.ToArray());
            if (set == names.Count) return;
            uint TitemID = TitemList[set - 1];

            if (pc.Gold < 5000)
            {
                Say(pc, 131, "很抱歉，你的钱不足够哦", "艾丽娅");
                return;
            }
            if (CountItem(pc, SitemID) < 1)
            {
                Say(pc, 131, "很抱歉，你的材料不足够哦", "艾丽娅");
                return;
            }
            pc.Gold -= 5000;
            TakeItem(pc, SitemID, 1);
            GiveItem(pc, TitemID, 1);
            Say(pc, 131, "谢谢光临~", "艾丽娅");
        }
    }
}


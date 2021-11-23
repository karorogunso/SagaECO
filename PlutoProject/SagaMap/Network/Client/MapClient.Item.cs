using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net;
using System.Net.Sockets;

using SagaDB;
using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using SagaDB.Skill;
using SagaLib;
using SagaMap.Manager;
using SagaMap.Skill;
using SagaDB.EnhanceTable;
using SagaDB.DualJob;
using SagaDB.MasterEnchance;
using SagaMap.Packets.Server;

namespace SagaMap.Network.Client
{
    public partial class MapClient
    {
        public uint kujiboxID0 = 120000000;
        public uint kujinum_max = 1000;
        public bool itemEnhance = false;
        public bool itemFusion = false;
        public bool itemMasterEnhance = false;
        public uint itemFusionEffect, itemFusionView;

        public void OnItemChange(Packets.Client.CSMG_ITEM_CHANGE p)
        {



            Item item = this.Character.Inventory.GetItem(p.InventorySlot);



            if (!KujiListFactory.Instance.ItemTransformList.ContainsKey(p.ChangeID))
            {
                if (this.account.GMLevel >= 200) this.SendSystemMessage("錯誤！沒找到ChangeID！");
                return;
            }


            KujiListFactory.ItemTransform it = KujiListFactory.Instance.ItemTransformList[p.ChangeID];
            Item newitem = ItemFactory.Instance.GetItem(it.product);


            //道具锁
            if (item.ChangeMode || item.ChangeMode2)
                return;

            item.ChangeMode = true;

            //unknown
            Packets.Server.SSMG_ITEM_CHANGE_ADD p1 = new SagaMap.Packets.Server.SSMG_ITEM_CHANGE_ADD();
            this.netIO.SendPacket(p1);
            //添加道具锁
            Packets.Server.SSMG_ITEM_INFO p2 = new SagaMap.Packets.Server.SSMG_ITEM_INFO();
            p2.Item = item;
            p2.InventorySlot = p.InventorySlot;
            p2.Container = this.Character.Inventory.GetContainerType(item.Slot);
            this.netIO.SendPacket(p2);



            //添加pet道具
            //pet属性设置
            newitem.Refine = 1;
            newitem.Durability = item.Durability;
            newitem.HP = (short)(item.BaseData.atk1 + item.BaseData.matk);
            newitem.Atk1 = (short)(item.BaseData.atk1 + item.BaseData.str * 30);
            newitem.Atk2 = (short)(item.BaseData.atk2 + item.BaseData.str * 30);
            newitem.Atk3 = (short)(item.BaseData.atk3 + item.BaseData.str * 30);
            newitem.MAtk = (short)(item.BaseData.matk + item.BaseData.mag * 30);
            newitem.Def = (short)(item.BaseData.def + item.BaseData.intel * 30 + item.BaseData.str * 20);
            newitem.MDef = (short)(item.BaseData.mdef + item.BaseData.intel * 20 + item.BaseData.mag * 30);
            newitem.HitCritical = (short)(item.BaseData.atk1 + item.BaseData.matk);
            newitem.AvoidMagic = (short)(item.BaseData.atk1 + item.BaseData.matk);
            newitem.AvoidCritical = (short)(item.BaseData.atk1 + item.BaseData.matk + 3);
            newitem.AvoidMelee = (short)(item.BaseData.atk1 + item.BaseData.matk + 1);
            newitem.AvoidRanged = (short)(item.BaseData.atk1 + item.BaseData.matk + 2);
            newitem.HitMagic = (short)(item.BaseData.atk1 + item.BaseData.matk + 4);
            newitem.HitMelee = (short)(item.BaseData.atk1 + item.BaseData.matk + 5);
            newitem.HitRanged = (short)(item.BaseData.atk1 + item.BaseData.matk + 6);
            //白框
            newitem.ChangeMode2 = true;
            this.AddItem(newitem, false);

            this.SendSystemMessage(item.BaseData.name + "已成功轉換。");






            /*
            try
            {
                List<uint> slots = p.SlotList();
                if (Character.Inventory.GetItem(slots[0]) == null) return;
                if (Character.Inventory.GetItem(slots[0]).Cards.Count > 0)
                {
                    SendSystemMessage("原装备还存在卡片，无法进行升级。");
                    return;
                }

                if (!KujiListFactory.Instance.ItemTransformList.ContainsKey(p.ChangeID))
                    return;
                if (p.ChangeID < 200)
                    return;
                KujiListFactory.ItemTransform it = KujiListFactory.Instance.ItemTransformList[p.ChangeID];

                for (int i = 0; i < it.Stuffs.Count; i++)
                {
                    Item stuff = Character.Inventory.GetItem(slots[i]);
                    if (stuff.ItemID != it.Stuffs[i])
                        return;
                }
                Item newitem = ItemFactory.Instance.GetItem(it.product);
                Item oriitem = Character.Inventory.GetItem(slots[0]);

                for (int i = 0; i < slots.Count; i++)
                {
                    if (slots[i] == 0)
                        continue;
                    Item stuff = Character.Inventory.GetItem(slots[i]);
                    newitem.HP += stuff.HP;
                    newitem.MP += stuff.MP;
                    newitem.SP += stuff.SP;
                    newitem.WeightUp += stuff.WeightUp;
                    newitem.VolumeUp += stuff.VolumeUp;
                    newitem.Str += stuff.Str;
                    newitem.Dex += stuff.Dex;
                    newitem.Int += stuff.Int;
                    newitem.Vit += stuff.Vit;
                    newitem.Agi += stuff.Agi;
                    newitem.Mag += stuff.Mag;
                    newitem.Atk1 += stuff.Atk1;
                    newitem.Atk2 += stuff.Atk2;
                    newitem.Atk3 += stuff.Atk3;
                    newitem.MAtk += stuff.MAtk;
                    newitem.Def += stuff.Def;
                    newitem.MDef += stuff.MDef;
                    newitem.ASPD += stuff.ASPD;
                    newitem.CSPD += stuff.CSPD;
                    DeleteItem(slots[i], 1, true);
                }
                //继承强化
                newitem.Refine = oriitem.Refine;
                newitem.Refine_Sharp = oriitem.Refine_Sharp;
                newitem.Refine_Enchanted = oriitem.Refine_Enchanted;
                newitem.Refine_Vitality = oriitem.Refine_Vitality;
                newitem.Refine_Hit = oriitem.Refine_Hit;
                newitem.Refine_Mhit = oriitem.Refine_Mhit;
                newitem.Refine_Regeneration = oriitem.Refine_Regeneration;
                newitem.Refine_Lucky = oriitem.Refine_Lucky;
                newitem.Refine_Dexterity = oriitem.Refine_Dexterity;
                newitem.Refine_ATKrate = oriitem.Refine_ATKrate;
                newitem.Refine_MATKrate = oriitem.Refine_MATKrate;
                newitem.Refine_Def = oriitem.Refine_Def;
                newitem.Refine_Mdef = oriitem.Refine_Mdef;

                newitem.PictID = oriitem.PictID;
                AddItem(newitem, true);
            }
            catch (Exception ex)
            {
                SagaLib.Logger.ShowError(ex);
            }
            */

            //Character.TInt["套装Change的ID"] = (int)p.InventorySlot;
            //EventActivate(99660000);

            /*if (this.Character.CInt["110武器变型DBID"] != 0)
                return;
            Item item = this.Character.Inventory.GetItem(p.InventorySlot);
            //获得物品Pet数据
            uint PetID = 0;
            if (p.ChangeID == 0x1C || p.ChangeID == 0x1B)
                PetID = 10085000;//カムイ 神剣・武御雷 / 煌刃・白虎のペットチェンジ後の姿
            else if (p.ChangeID == 0x1D)
                PetID = 10085100;//リゼル 血剣ダインスレイブのペットチェンジ後の姿
            else if (p.ChangeID == 0x24 || p.ChangeID == 0x25)
                PetID = 10085600;//ヨシュア 神槍・ブリューナク / 天槍・リンドブルムのペットチェンジ後の姿
            else if (p.ChangeID == 0x1E)
                PetID = 10085200;//テリア 護聖剣・ジブリール
            else if (p.ChangeID == 0x1f)
                PetID = 10085300;//キーノ 冥王爪・オルトロスのペットチェンジ後の姿
            else if (p.ChangeID == 0x28 || p.ChangeID == 0x29)
                PetID = 10085800;//ソレイユ 月天弓・アルテミス / 轟天弓・バハムートのペットチェンジ後の姿
            else if (p.ChangeID == 0x2A || p.ChangeID == 0x2B || p.ChangeID == 0x2C)
                PetID = 10085900;//グリヴァー 烈神銃・サラマンドラ / 烈神銃・サラマンドラ（２丁）穿竜砲・ヤタガラスのペットチェンジ後の姿
            else if (p.ChangeID == 0x26 || p.ChangeID == 0x27)
                PetID = 10085701;//レネット 黒書・ネクロノミコン / 黎明杖・カドゥケウスのペットチェンジ後の姿
            else if (p.ChangeID == 0x2E)
                PetID = 10086100;//フロール 浄絃・ソウルセラフィムのペットチェンジ後の姿
            else if (p.ChangeID == 0x20 || p.ChangeID == 0x21)
                PetID = 10085400;//ツバキ 降魔槌・クレイオス / 祓魔槌・ウロボロスのペットチェンジ後の姿
            else if (p.ChangeID == 0x22 || p.ChangeID == 0x23)
                PetID = 10085500;//カノン 闇葬鎌・タルタロス / 断罪斧・レヴァイアサンのペットチェンジ後の姿、幼女
            else if (p.ChangeID == 0x2D)
                PetID = 10086000;//プリーシュ 煉獄鞭・アナフィエルのペットチェンジ後の姿
            else
                return;
            //道具锁
            if (item.ChangeMode || item.ChangeMode2)
                return;
            item.ChangeMode = true;
            this.Character.CInt["110武器变型DBID"] = 10;
            //Logger.ShowError(string.Format("60052300:{0},{1}", item.DBID, this.Character.CInt["110武器变型DBID"]));
            //unknown
            Packets.Server.SSMG_ITEM_CHANGE_ADD p1 = new SagaMap.Packets.Server.SSMG_ITEM_CHANGE_ADD();
            this.netIO.SendPacket(p1);
            //添加道具锁
            Packets.Server.SSMG_ITEM_INFO p2 = new SagaMap.Packets.Server.SSMG_ITEM_INFO();
            p2.Item = item;
            p2.InventorySlot = p.InventorySlot;
            p2.Container = this.Character.Inventory.GetContainerType(item.Slot);
            this.netIO.SendPacket(p2);

            //添加pet道具
            SagaDB.Item.Item ChangeItem = ItemFactory.Instance.GetItem(PetID);
            this.AddItem(ChangeItem, true);
            Item petitem = this.Character.Inventory.GetItem(ChangeItem.Slot);
            //pet属性设置
            petitem.Refine = 1;
            petitem.Durability = item.Durability;
            petitem.HP = (short)(item.BaseData.atk1 + item.BaseData.matk);
            petitem.Atk1 = (short)(item.BaseData.atk1 + item.BaseData.str * 30);
            petitem.Atk2 = (short)(item.BaseData.atk2 + item.BaseData.str * 30);
            petitem.Atk3 = (short)(item.BaseData.atk3 + item.BaseData.str * 30);
            petitem.MAtk = (short)(item.BaseData.matk + item.BaseData.mag * 30);
            petitem.Def = (short)(item.BaseData.def + item.BaseData.intel * 30 + item.BaseData.str * 20);
            petitem.MDef = (short)(item.BaseData.mdef + item.BaseData.intel * 20 + item.BaseData.mag * 30);
            petitem.HitCritical = (short)(item.BaseData.atk1 + item.BaseData.matk);
            petitem.AvoidMagic = (short)(item.BaseData.atk1 + item.BaseData.matk);
            petitem.AvoidCritical = (short)(item.BaseData.atk1 + item.BaseData.matk + 3);
            petitem.AvoidMelee = (short)(item.BaseData.atk1 + item.BaseData.matk + 1);
            petitem.AvoidRanged = (short)(item.BaseData.atk1 + item.BaseData.matk + 2);
            petitem.HitMagic = (short)(item.BaseData.atk1 + item.BaseData.matk + 4);
            petitem.HitMelee = (short)(item.BaseData.atk1 + item.BaseData.matk + 5);
            petitem.HitRanged = (short)(item.BaseData.atk1 + item.BaseData.matk + 6);
            //白框
            petitem.ChangeMode2 = true;
            SendItemInfo(petitem);*/
        }

        public void OnItemMasterEnhanceClose(Packets.Client.CSMG_ITEM_MASTERENHANCE_CLOSE p)
        {
            this.itemMasterEnhance = false;
            //关闭界面处理
        }

        public void SendMasterEnhanceAbleEquiList()
        {
            SSMG_ITEM_MASTERENHANCE_RESULT p2 = new SSMG_ITEM_MASTERENHANCE_RESULT();
            p2.Result = 0;
            Packet packet = new Packet(10);
            packet.data = new byte[10];
            packet.ID = 0x1f59;

            if (this.chara.Gold < 5000)
            {
                p2.Result = -1;
                this.netIO.SendPacket(p2);
                this.netIO.SendPacket(packet);
                this.itemMasterEnhance = false;
                return;
            }

            var lst = this.Character.Inventory.GetContainer(ContainerType.BODY);
            var itemlist = lst.Where(x => (x.IsWeapon || x.IsArmor) && !x.Potential).ToList();
            SSMG_ITEM_MASTERENHANCE_LIST p = new SSMG_ITEM_MASTERENHANCE_LIST();
            p.Items = itemlist;

            if (itemlist.Count > 0)
                this.netIO.SendPacket(p);
            else
            {
                p2.Result = -2;
                this.netIO.SendPacket(p2);
                this.netIO.SendPacket(packet);
                this.itemMasterEnhance = false;
            }
        }

        public void OnItemMasterEnhanceSelect(Packets.Client.CSMG_ITEM_MASTERENHANCE_SELECT p)
        {
            Item item = this.chara.Inventory.GetItem(p.InventorySlot);
            List<MasterEnhanceMaterial> lst = new List<MasterEnhanceMaterial>();

            foreach (var itemkey in MasterEnhanceMaterialFactory.Instance.Items.Keys)
            {
                if (CountItem(itemkey) > 0)
                    lst.Add(MasterEnhanceMaterialFactory.Instance.Items[itemkey]);
            }

            SSMG_ITEM_MASTERENHANCE_DETAIL p1 = new SSMG_ITEM_MASTERENHANCE_DETAIL();
            p1.Items = lst;
            this.netIO.SendPacket(p1);
        }
        public void OnItemMasterEnhanceConfirm(Packets.Client.CSMG_ITEM_MASTERENHANCE_CONFIRM p)
        {
            SSMG_ITEM_MASTERENHANCE_RESULT p2 = new SSMG_ITEM_MASTERENHANCE_RESULT();
            p2.Result = 0;

            Packet packet = new Packet(10);
            packet.data = new byte[10];
            packet.ID = 0x1f59;

            Item item = this.chara.Inventory.GetItem(p.InventorySlot);
            if (item == null)
            {
                p2.Result = -4;

                this.netIO.SendPacket(p2);
                this.netIO.SendPacket(packet);
                this.itemMasterEnhance = false;
                return;
            }

            var materialid = p.ItemID;

            if (CountItem(materialid) <= 0)
            {
                p2.Result = -3;

                this.netIO.SendPacket(p2);
                this.netIO.SendPacket(packet);
                this.itemMasterEnhance = false;
                return;
            }

            DeleteItemID(materialid, 1, true);

            var Material = MasterEnhanceMaterialFactory.Instance.Items[materialid];
            var value = (short)Global.Random.Next(Material.MinValue, Material.MaxValue);

            item.Potential = true;
            switch (Material.Ability)
            {
                case MasterEnhanceType.STR:
                    item.Str += value;
                    break;
                case MasterEnhanceType.DEX:
                    item.Dex += value;
                    break;
                case MasterEnhanceType.INT:
                    item.Int += value;
                    break;
                case MasterEnhanceType.VIT:
                    item.Vit += value;
                    break;
                case MasterEnhanceType.AGI:
                    item.Agi += value;
                    break;
                case MasterEnhanceType.MAG:
                    item.Mag += value;
                    break;
                default:
                    break;
            }

            SendEffect(5145);

            SendItemInfo(item);

            p2.Result = 0;
            this.netIO.SendPacket(p2);

            SendMasterEnhanceAbleEquiList();
        }
        public void OnItemChangeCancel(Packets.Client.CSMG_ITEM_CHANGE_CANCEL p)
        {

            Item PetItem = this.Character.Inventory.GetItem(p.InventorySlot);
            Item ChangeItem = this.Character.Inventory.GetItem2();
            ChangeItem.Durability = PetItem.Durability;
            ChangeItem.ChangeMode = false;

            SendItemInfo(ChangeItem);
            this.DeleteItem(PetItem.Slot, 1, false);

            this.SendSystemMessage(PetItem.BaseData.name + "已轉換原始的狀態。");



            // Logger.ShowError(string.Format("OnItemChangeCancel:{0}", this.Character.CInt["110武器变型DBID"]));
            /*
            if (this.Character.CInt["110武器变型DBID"] != 0)
            {

                Item PetItem = this.Character.Inventory.GetItem(p.InventorySlot);
                Item ChangeItem = this.Character.Inventory.GetItem2();
                ChangeItem.Durability = PetItem.Durability;
                ChangeItem.ChangeMode = false;
                this.Character.CInt["110武器变型DBID"] = 0;
                SendItemInfo(ChangeItem);
                this.DeleteItem(PetItem.Slot, 1, true);
            }
            else
            {
                return;
            }
            */
        }
        public void OnItemFusionCancel(Packets.Client.CSMG_ITEM_FUSION_CANCEL p)
        {
            itemFusionEffect = 0;
            itemFusionView = 0;
            this.itemFusion = false;
        }
        public void OnItemFusion(Packets.Client.CSMG_ITEM_FUSION p)
        {
            itemFusionEffect = p.EffectItem;
            itemFusionView = p.ViewItem;
            this.itemFusion = false;
        }
        public void OnItemEnhanceClose(Packets.Client.CSMG_ITEM_ENHANCE_CLOSE p)
        {
            this.itemEnhance = false;
            this.irisAddSlot = false;
        }
        public void OnItemEnhanceSelect(Packets.Client.CSMG_ITEM_ENHANCE_SELECT p)
        {
            Item item = this.chara.Inventory.GetItem(p.InventorySlot);
            if (item != null)
            {
                List<Packets.Server.EnhanceDetail> list = new List<SagaMap.Packets.Server.EnhanceDetail>();
                if (item.IsWeapon)
                {
                    if (CountItem(90000044) > 0)
                    {
                        Packets.Server.EnhanceDetail detail = new SagaMap.Packets.Server.EnhanceDetail();
                        detail.material = 90000044;
                        detail.type = SagaMap.Packets.Server.EnhanceType.Atk;
                        detail.value = FindEnhancementValue(item, 90000044);
                        list.Add(detail);
                    }
                    if (CountItem(90000045) > 0)
                    {
                        Packets.Server.EnhanceDetail detail = new SagaMap.Packets.Server.EnhanceDetail();
                        detail.material = 90000045;
                        detail.type = SagaMap.Packets.Server.EnhanceType.MAtk;
                        detail.value = FindEnhancementValue(item, 90000045);
                        list.Add(detail);
                    }
                    if (CountItem(90000046) > 0)
                    {
                        Packets.Server.EnhanceDetail detail = new SagaMap.Packets.Server.EnhanceDetail();
                        detail.material = 90000046;
                        detail.type = SagaMap.Packets.Server.EnhanceType.Cri;
                        detail.value = FindEnhancementValue(item, 90000046);
                        list.Add(detail);
                    }
                }
                if (item.IsArmor)
                {
                    if (CountItem(90000043) > 0)
                    {
                        Packets.Server.EnhanceDetail detail = new SagaMap.Packets.Server.EnhanceDetail();
                        detail.material = 90000043;
                        detail.type = SagaMap.Packets.Server.EnhanceType.HP;
                        detail.value = FindEnhancementValue(item, 90000043);
                        list.Add(detail);
                    }
                    if (CountItem(90000044) > 0)
                    {
                        Packets.Server.EnhanceDetail detail = new SagaMap.Packets.Server.EnhanceDetail();
                        detail.material = 90000044;
                        detail.type = SagaMap.Packets.Server.EnhanceType.Def;
                        detail.value = FindEnhancementValue(item, 90000044);
                        list.Add(detail);
                    }
                    if (CountItem(90000045) > 0)
                    {
                        Packets.Server.EnhanceDetail detail = new SagaMap.Packets.Server.EnhanceDetail();
                        detail.material = 90000045;
                        detail.type = SagaMap.Packets.Server.EnhanceType.MDef;
                        detail.value = FindEnhancementValue(item, 90000045);
                        list.Add(detail);
                    }
                    if (CountItem(90000046) > 0)
                    {
                        Packets.Server.EnhanceDetail detail = new SagaMap.Packets.Server.EnhanceDetail();
                        detail.material = 90000046;
                        detail.type = SagaMap.Packets.Server.EnhanceType.AvoidCri;
                        detail.value = FindEnhancementValue(item, 90000046);
                        list.Add(detail);
                    }
                }
                if (item.BaseData.itemType == ItemType.SHIELD)
                {
                    if (CountItem(90000044) > 0)
                    {
                        Packets.Server.EnhanceDetail detail = new SagaMap.Packets.Server.EnhanceDetail();
                        detail.material = 90000044;
                        detail.type = SagaMap.Packets.Server.EnhanceType.Def;
                        detail.value = FindEnhancementValue(item, 90000044);
                        list.Add(detail);
                    }
                    if (CountItem(90000045) > 0)
                    {
                        Packets.Server.EnhanceDetail detail = new SagaMap.Packets.Server.EnhanceDetail();
                        detail.material = 90000045;
                        detail.type = SagaMap.Packets.Server.EnhanceType.MDef;
                        detail.value = FindEnhancementValue(item, 90000045);
                        list.Add(detail);
                    }
                }
                if (item.BaseData.itemType == ItemType.ACCESORY_NECK)
                {
                    if (CountItem(90000045) > 0)
                    {
                        Packets.Server.EnhanceDetail detail = new SagaMap.Packets.Server.EnhanceDetail();
                        detail.material = 90000045;
                        detail.type = SagaMap.Packets.Server.EnhanceType.MDef;
                        detail.value = FindEnhancementValue(item, 90000045);
                        list.Add(detail);
                    }
                }

                Packets.Server.SSMG_ITEM_ENHANCE_DETAIL p1 = new SagaMap.Packets.Server.SSMG_ITEM_ENHANCE_DETAIL();
                p1.Items = list;
                this.netIO.SendPacket(p1);
            }
        }

        public short FindEnhancementValue(Item item, uint itemID)
        {
            short[] hps = new short[31] { 0,
                                          100, 20, 70,  30,  80,  40,  90,  50,  100, 150,
                                          150, 60, 110, 70,  200, 200, 120, 80,  130, 250,
                                          250, 90, 140, 100, 250, 250, 150, 110, 160, 400  };
            short[] atk_def_matk = new short[31] { 0,
                                           10, 3, 5,  3, 6,  3,  7,  3, 8,  13,
                                           13, 3, 9,  3, 15, 15, 10, 3, 11, 20,
                                           20, 3, 12, 3, 22, 22, 13, 3, 14, 25 };
            short[] mdef = new short[31] { 0,
                                           10, 2, 5, 2, 6,  3,  6, 3, 6, 15,
                                           15, 4, 7, 4, 10, 10, 7, 4, 7, 15,
                                           15, 5, 8, 5, 15, 15, 8, 5, 8, 25 };
            short[] cris = new short[31] { 0,
                                           5, 1, 3, 2, 4, 3, 4, 3, 5, 9,
                                           5, 1, 2, 3, 4, 5, 1, 2, 3, 4,
                                           5, 1, 2, 3, 4, 5, 1, 2, 3, 5 };
            switch (itemID)
            {
                case 90000043:
                    if (item.IsArmor)
                    {
                        short i = 0;
                        for (int j = 0; j <= item.LifeEnhance; j++)
                            i += hps[j];
                        return (short)(i + hps[item.LifeEnhance + 1]);
                    }
                    break;
                case 90000044:
                    if (item.IsWeapon)
                    {
                        short i = 0;
                        for (int j = 0; j <= item.PowerEnhance; j++)
                            i += atk_def_matk[j];
                        return (short)(i + atk_def_matk[item.PowerEnhance + 1]);
                    }
                    else
                    {
                        if (item.IsArmor || item.BaseData.itemType == ItemType.SHIELD)
                        {
                            short i = 0;
                            for (int j = 0; j <= item.PowerEnhance; j++)
                                i += atk_def_matk[j];
                            return (short)(i + atk_def_matk[item.PowerEnhance + 1]);
                        }
                    }
                    break;
                case 90000045:
                    if (item.IsWeapon)
                    {
                        short i = 0;
                        for (int j = 0; j <= item.MagEnhance; j++)
                            i += atk_def_matk[j];
                        return (short)(i + atk_def_matk[item.MagEnhance + 1]);
                    }
                    else
                    {
                        short i = 0;
                        for (int j = 0; j <= item.MagEnhance; j++)
                            i += mdef[j];
                        return (short)(i + mdef[item.MagEnhance + 1]);
                    }
                case 90000046:
                    if (item.IsWeapon)
                    {
                        short i = 0;
                        for (int j = 0; j <= item.CritEnhance; j++)
                            i += cris[j];
                        return (short)(i + cris[item.CritEnhance + 1]);
                    }
                    if (item.IsArmor)
                    {
                        short i = 0;
                        for (int j = 0; j <= item.CritEnhance; j++)
                            i += cris[j];
                        return (short)(i + cris[item.CritEnhance + 1]);
                    }
                    break;
            }
            return 0;
        }

        /// <summary>
        /// 取得玩家身上指定道具的信息
        /// </summary>
        /// <param name="ID">道具ID</param>
        /// <returns>道具清单</returns>
        protected List<SagaDB.Item.Item> GetItem(uint ID)
        {
            List<SagaDB.Item.Item> result = new List<SagaDB.Item.Item>();
            for (int i = 2; i < 6; i++)
            {
                List<SagaDB.Item.Item> list = this.chara.Inventory.Items[(ContainerType)i];
                var query = from it in list
                            where it.ItemID == ID
                            select it;
                result.AddRange(query);
            }
            return result;
        }

        public void OnItemEnhanceConfirm(Packets.Client.CSMG_ITEM_ENHANCE_CONFIRM p)
        {
            Item item = this.Character.Inventory.GetItem(p.InventorySlot);
            bool failed = false;
            Packets.Server.SSMG_ITEM_ENHANCE_RESULT p1 = new SagaMap.Packets.Server.SSMG_ITEM_ENHANCE_RESULT();
            p1.Result = 0;
            if (item != null)
            {
                if (CountItem(p.Material) > 0 && item.Refine < 30)
                {
                    if (this.Character.Gold >= 5000)
                    {
                        this.Character.Gold -= 5000;

                        Logger.ShowInfo("Refine Item:" + item.BaseData.name + "[" + p.InventorySlot + "] Protect Item: " +
                            (ItemFactory.Instance.GetItem(p.ProtectItem).ItemID != 10000000 ? ItemFactory.Instance.GetItem(p.ProtectItem).BaseData.name : "None") +
                            Environment.NewLine + "Material: " + (ItemFactory.Instance.GetItem(p.Material).ItemID != 10000000 ? ItemFactory.Instance.GetItem(p.Material).BaseData.name : "None") +
                            " SupportItem: " + (ItemFactory.Instance.GetItem(p.SupportItem).ItemID != 10000000 ? ItemFactory.Instance.GetItem(p.SupportItem).BaseData.name : "None"));


                        Logger.ShowInfo("BaseLevel: " + p.BaseLevel + " JLevel: " + p.JobLevel);
                        Logger.ShowInfo("ExpRate: " + p.ExpRate + " JExpRate: " + p.JExpRate);

                        var Material = p.Material;

                        uint supportitemid = 0;
                        var enhancesupported = false;
                        if (ItemFactory.Instance.GetItem(p.SupportItem).ItemID != 10000000)
                        {
                            enhancesupported = true;
                            supportitemid = ItemFactory.Instance.GetItem((uint)p.SupportItem).ItemID;
                        }

                        uint protectitemid = 0;
                        var enhanceprotected = false;
                        if (ItemFactory.Instance.GetItem(p.ProtectItem).ItemID != 10000000)
                        {
                            enhanceprotected = true;
                            protectitemid = ItemFactory.Instance.GetItem((uint)p.ProtectItem).ItemID;
                        }

                        //重寫！ - KK
                        //成功率加成
                        //解决了保护和辅助道具强行使用的问题 by [黑白照] 2018.07.02 
                        int finalrate = 0;
                        string used_material = "";
                        int crystal_addon = 0;
                        int skill_addon = 0;
                        int protect_addon = 0;
                        int support_addon = 0;
                        int matsuri_addon = 0;
                        int recycle_addon = 0;
                        int nextlevel = ((int)item.Refine + (int)1);

                        //BaseRate
                        finalrate += EnhanceTableFactory.Instance.Table[nextlevel].BaseRate;

                        //一般結晶
                        if (p.Material >= 90000043 && p.Material <= 90000046)
                            crystal_addon = EnhanceTableFactory.Instance.Table[nextlevel].Crystal;


                        //強化結晶
                        if (p.Material >= 90000053 && p.Material <= 90000056)
                            crystal_addon = EnhanceTableFactory.Instance.Table[nextlevel].EnhanceCrystal;

                        //超強化結晶
                        if (p.Material >= 16004500 && p.Material <= 16004800)
                            crystal_addon = EnhanceTableFactory.Instance.Table[nextlevel].SPCrystal;

                        //強化王結晶
                        if (p.Material >= 10087400 && p.Material <= 10087403)
                            crystal_addon = EnhanceTableFactory.Instance.Table[nextlevel].KingCrystal;

                        //防爆
                        if (enhanceprotected && protectitemid == 16001300)
                            protect_addon = EnhanceTableFactory.Instance.Table[nextlevel].ExplodeProtect;

                        //防重設
                        if (enhanceprotected && protectitemid == 10118200)
                            protect_addon = EnhanceTableFactory.Instance.Table[nextlevel].ResetProtect;

                        //奧義
                        if (ItemFactory.Instance.GetItem(p.ProtectItem).ItemID == 10087200)
                            support_addon = EnhanceTableFactory.Instance.Table[nextlevel].Okugi;

                        //神髓
                        if (ItemFactory.Instance.GetItem(p.ProtectItem).ItemID == 10087201)
                            support_addon = EnhanceTableFactory.Instance.Table[nextlevel].Shinzui;

                        //強化祭活動
                        if (SagaMap.Configuration.Instance.EnhanceMatsuri)
                            matsuri_addon = EnhanceTableFactory.Instance.Table[nextlevel].Matsuri;




                        //回收活動
                        //未實裝(需連動回收系統)
                        /*
                        if (SagaMap.Configuration.Instance.Recycle)
                            finalrate += EnhanceTableFactory.Instance.Table[nextlevel].Recycle;
                        */


                        //被動技能加成
                        //一般結晶
                        if (p.Material >= 90000043 && p.Material <= 90000046)
                        {

                            if (p.Material == 90000043 && this.Character.Status.Additions.ContainsKey("BoostHp"))
                            {
                                used_material = "強化技能-命";
                                skill_addon = (int)((int)(this.Character.Status.Additions["BoostHp"] as DefaultPassiveSkill).Variable["BoostHp"]);
                            }
                            if (p.Material == 90000044 && this.Character.Status.Additions.ContainsKey("BoostPower"))
                            {
                                used_material = "強化技能-力";
                                skill_addon = (int)((int)(this.Character.Status.Additions["BoostPower"] as DefaultPassiveSkill).Variable["BoostPower"]);
                            }

                            if (p.Material == 90000045 && this.Character.Status.Additions.ContainsKey("BoostMagic"))
                            {
                                used_material = "強化技能-魔";
                                skill_addon = (int)((int)(this.Character.Status.Additions["BoostMagic"] as DefaultPassiveSkill).Variable["BoostMagic"]);
                            }

                            if (p.Material == 90000046 && this.Character.Status.Additions.ContainsKey("BoostCritical"))
                            {
                                used_material = "強化技能-暴擊";
                                skill_addon = (int)((int)(this.Character.Status.Additions["BoostCritical"] as DefaultPassiveSkill).Variable["BoostCritical"]);
                            }
                        }

                        //強化結晶
                        if (p.Material >= 90000053 && p.Material <= 90000056)
                        {

                            if (p.Material == 90000053 && this.Character.Status.Additions.ContainsKey("BoostHp"))
                            {
                                used_material = "強化技能-命";
                                skill_addon = (int)((int)(this.Character.Status.Additions["BoostHp"] as DefaultPassiveSkill).Variable["BoostHp"]);
                            }
                            if (p.Material == 90000054 && this.Character.Status.Additions.ContainsKey("BoostPower"))
                            {
                                used_material = "強化技能-力";
                                skill_addon = (int)((int)(this.Character.Status.Additions["BoostPower"] as DefaultPassiveSkill).Variable["BoostPower"]);
                            }

                            if (p.Material == 90000055 && this.Character.Status.Additions.ContainsKey("BoostMagic"))
                            {
                                used_material = "強化技能-魔";
                                skill_addon = (int)((int)(this.Character.Status.Additions["BoostMagic"] as DefaultPassiveSkill).Variable["BoostMagic"]);
                            }

                            if (p.Material == 90000056 && this.Character.Status.Additions.ContainsKey("BoostCritical"))
                            {
                                used_material = "強化技能-暴擊";
                                skill_addon = (int)((int)(this.Character.Status.Additions["BoostCritical"] as DefaultPassiveSkill).Variable["BoostCritical"]);
                            }
                        }

                        //超強化結晶
                        if (p.Material >= 16004500 && p.Material <= 16004800)
                        {

                            if (p.Material == 16004600 && this.Character.Status.Additions.ContainsKey("BoostHp"))
                            {
                                used_material = "強化技能-命";
                                skill_addon = (int)((int)(this.Character.Status.Additions["BoostHp"] as DefaultPassiveSkill).Variable["BoostHp"]);
                            }
                            if (p.Material == 16004700 && this.Character.Status.Additions.ContainsKey("BoostPower"))
                            {
                                used_material = "強化技能-力";
                                skill_addon = (int)((int)(this.Character.Status.Additions["BoostPower"] as DefaultPassiveSkill).Variable["BoostPower"]);
                            }

                            if (p.Material == 16004800 && this.Character.Status.Additions.ContainsKey("BoostMagic"))
                            {
                                used_material = "強化技能-魔";
                                skill_addon = (int)((int)(this.Character.Status.Additions["BoostMagic"] as DefaultPassiveSkill).Variable["BoostMagic"]);
                            }

                            if (p.Material == 16004500 && this.Character.Status.Additions.ContainsKey("BoostCritical"))
                            {
                                used_material = "強化技能-暴擊";
                                skill_addon = (int)((int)(this.Character.Status.Additions["BoostCritical"] as DefaultPassiveSkill).Variable["BoostCritical"]);
                            }
                        }

                        //強化王結晶
                        if (p.Material >= 10087400 && p.Material <= 10087403)
                        {

                            if (p.Material == 10087400 && this.Character.Status.Additions.ContainsKey("BoostHp"))
                            {
                                used_material = "強化技能-命";
                                skill_addon = (int)((int)(this.Character.Status.Additions["BoostHp"] as DefaultPassiveSkill).Variable["BoostHp"]);
                            }
                            if (p.Material == 10087401 && this.Character.Status.Additions.ContainsKey("BoostPower"))
                            {
                                used_material = "強化技能-力";
                                skill_addon = (int)((int)(this.Character.Status.Additions["BoostPower"] as DefaultPassiveSkill).Variable["BoostPower"]);
                            }

                            if (p.Material == 10087403 && this.Character.Status.Additions.ContainsKey("BoostMagic"))
                            {
                                used_material = "強化技能-魔";
                                skill_addon = (int)((int)(this.Character.Status.Additions["BoostMagic"] as DefaultPassiveSkill).Variable["BoostMagic"]);
                            }

                            if (p.Material == 10087402 && this.Character.Status.Additions.ContainsKey("BoostCritical"))
                            {
                                used_material = "強化技能-暴擊";
                                skill_addon = (int)((int)(this.Character.Status.Additions["BoostCritical"] as DefaultPassiveSkill).Variable["BoostCritical"]);
                            }
                        }


                        //結算
                        finalrate += crystal_addon + matsuri_addon + skill_addon + protect_addon + support_addon + recycle_addon;


                        FromActorPC(Character).SendSystemMessage("----------------強化成功率結算---------------");
                        FromActorPC(Character).SendSystemMessage("正強化裝備到：" + ((int)item.Refine + (int)1));

                        FromActorPC(Character).SendSystemMessage("基本機率：" + (EnhanceTableFactory.Instance.Table[nextlevel].BaseRate / 100) + "%");
                        FromActorPC(Character).SendSystemMessage("結晶加成：" + (crystal_addon / 100) + "%");
                        FromActorPC(Character).SendSystemMessage("補助加成：" + (support_addon / 100) + "%");
                        FromActorPC(Character).SendSystemMessage("防爆加成：" + (protect_addon / 100) + "%");
                        FromActorPC(Character).SendSystemMessage("強化祭加成：" + (matsuri_addon / 100) + "%");
                        FromActorPC(Character).SendSystemMessage("被動技加成：" + used_material + " -" + (skill_addon / 100) + "%");
                        FromActorPC(Character).SendSystemMessage("回收活动加成：" + (recycle_addon / 100) + "%");

                        FromActorPC(Character).SendSystemMessage("你的最終強化成功率為：" + (finalrate / 100) + "%");
                        FromActorPC(Character).SendSystemMessage("----------------強化成功率結算---------------");

                        if (finalrate > 9999)
                            finalrate = 9999;

                        int refrate = Global.Random.Next(0, 9999);

                        if (enhanceprotected)
                        {
                            if (CountItem(protectitemid) < 1)
                            {
                                p1.Result = 0x00;
                                p1.OrignalRefine = item.Refine;
                                p1.ExpectedRefine = (ushort)(item.Refine + 1);
                                this.netIO.SendPacket(p1);
                                p1.Result = 0xfc;
                                this.netIO.SendPacket(p1);
                                this.itemEnhance = false;
                                return;
                            }
                            else
                                DeleteItemID(protectitemid, 1, true);
                        }

                        if (enhancesupported)
                        {
                            if (CountItem(supportitemid) < 1)
                            {
                                p1.Result = 0x00;
                                p1.OrignalRefine = item.Refine;
                                p1.ExpectedRefine = (ushort)(item.Refine + 1);
                                this.netIO.SendPacket(p1);
                                p1.Result = 0xfc;
                                this.netIO.SendPacket(p1);
                                this.itemEnhance = false;
                                return;
                            }
                            else
                                DeleteItemID(supportitemid, 1, true);
                        }


                        if (CountItem(Material) < 1)
                        {
                            p1.Result = 0x00;
                            p1.OrignalRefine = item.Refine;
                            p1.ExpectedRefine = (ushort)(item.Refine + 1);
                            this.netIO.SendPacket(p1);
                            p1.Result = 0xfc;
                            this.netIO.SendPacket(p1);
                            this.itemEnhance = false;
                            return;
                        }

                        if (refrate <= finalrate)
                        {
                            if (item.IsArmor)
                            {
                                switch (p.Material)
                                {
                                    //一般水晶
                                    case 90000043:
                                        item.HP = FindEnhancementValue(item, 90000043);
                                        DeleteItemID(90000043, 1, true);
                                        item.LifeEnhance++;
                                        break;
                                    case 90000044:
                                        item.Def = FindEnhancementValue(item, 90000044);
                                        DeleteItemID(90000044, 1, true);
                                        item.PowerEnhance++;
                                        break;
                                    case 90000045:
                                        item.MDef = FindEnhancementValue(item, 90000045);
                                        DeleteItemID(90000045, 1, true);
                                        item.MagEnhance++;
                                        break;
                                    case 90000046:
                                        item.AvoidCritical = FindEnhancementValue(item, 90000046);
                                        DeleteItemID(90000046, 1, true);
                                        item.CritEnhance++;
                                        break;


                                    //強化水晶
                                    case 90000053:
                                        item.HP = FindEnhancementValue(item, 90000043);
                                        DeleteItemID(90000053, 1, true);
                                        item.LifeEnhance++;
                                        break;
                                    case 90000054:
                                        item.Def = FindEnhancementValue(item, 90000054);
                                        DeleteItemID(90000054, 1, true);
                                        item.PowerEnhance++;
                                        break;
                                    case 90000055:
                                        item.MDef = FindEnhancementValue(item, 90000055);
                                        DeleteItemID(90000055, 1, true);
                                        item.MagEnhance++;
                                        break;
                                    case 90000056:
                                        item.AvoidCritical = FindEnhancementValue(item, 90000056);
                                        DeleteItemID(90000056, 1, true);
                                        item.CritEnhance++;
                                        break;


                                    //超強化水晶
                                    case 16004600:
                                        item.HP = FindEnhancementValue(item, 16004600);
                                        DeleteItemID(16004600, 1, true);
                                        item.LifeEnhance++;
                                        break;
                                    case 16004700:
                                        item.Def = FindEnhancementValue(item, 16004700);
                                        DeleteItemID(16004700, 1, true);
                                        item.PowerEnhance++;
                                        break;
                                    case 16004800:
                                        item.MDef = FindEnhancementValue(item, 16004800);
                                        DeleteItemID(16004800, 1, true);
                                        item.MagEnhance++;
                                        break;
                                    case 16004500:
                                        item.AvoidCritical = FindEnhancementValue(item, 16004500);
                                        DeleteItemID(16004500, 1, true);
                                        item.CritEnhance++;
                                        break;


                                    //強化王
                                    case 10087400:
                                        item.HP = FindEnhancementValue(item, 10087400);
                                        DeleteItemID(10087400, 1, true);
                                        item.LifeEnhance++;
                                        break;
                                    case 10087401:
                                        item.Def = FindEnhancementValue(item, 10087401);
                                        DeleteItemID(10087401, 1, true);
                                        item.PowerEnhance++;
                                        break;
                                    case 10087403:
                                        item.MDef = FindEnhancementValue(item, 10087403);
                                        DeleteItemID(10087403, 1, true);
                                        item.MagEnhance++;
                                        break;
                                    case 10087402:
                                        item.AvoidCritical = FindEnhancementValue(item, 10087402);
                                        DeleteItemID(10087402, 1, true);
                                        item.CritEnhance++;
                                        break;

                                }
                            }
                            if (item.IsWeapon)
                            {
                                switch (p.Material)
                                {
                                    //一般水晶
                                    case 90000044:
                                        item.Atk1 = FindEnhancementValue(item, 90000044);
                                        item.Atk2 = item.Atk1;
                                        item.Atk3 = item.Atk1;
                                        DeleteItemID(90000044, 1, true);
                                        item.PowerEnhance++;
                                        break;
                                    case 90000045:
                                        item.MAtk = FindEnhancementValue(item, 90000045);
                                        DeleteItemID(90000045, 1, true);
                                        item.MagEnhance++;
                                        break;
                                    case 90000046:
                                        item.HitCritical = FindEnhancementValue(item, 90000046);
                                        DeleteItemID(90000046, 1, true);
                                        item.CritEnhance++;
                                        break;

                                    //強化水晶
                                    case 90000054:
                                        item.Atk1 = FindEnhancementValue(item, 90000054);
                                        item.Atk2 = item.Atk1;
                                        item.Atk3 = item.Atk1;
                                        DeleteItemID(90000054, 1, true);
                                        item.PowerEnhance++;
                                        break;
                                    case 90000055:
                                        item.MAtk = FindEnhancementValue(item, 90000055);
                                        DeleteItemID(90000055, 1, true);
                                        item.MagEnhance++;
                                        break;
                                    case 90000056:
                                        item.HitCritical = FindEnhancementValue(item, 90000056);
                                        DeleteItemID(90000056, 1, true);
                                        item.CritEnhance++;
                                        break;

                                    //超強化水晶
                                    case 16004700:
                                        item.Atk1 = FindEnhancementValue(item, 16004700);
                                        item.Atk2 = item.Atk1;
                                        item.Atk3 = item.Atk1;
                                        DeleteItemID(16004700, 1, true);
                                        item.PowerEnhance++;
                                        break;
                                    case 16004800:
                                        item.MAtk = FindEnhancementValue(item, 16004800);
                                        DeleteItemID(16004800, 1, true);
                                        item.MagEnhance++;
                                        break;
                                    case 16004500:
                                        item.HitCritical = FindEnhancementValue(item, 16004500);
                                        DeleteItemID(16004500, 1, true);
                                        item.CritEnhance++;
                                        break;

                                    //強化王水晶
                                    case 10087401:
                                        item.Atk1 = FindEnhancementValue(item, 10087401);
                                        item.Atk2 = item.Atk1;
                                        item.Atk3 = item.Atk1;
                                        DeleteItemID(10087401, 1, true);
                                        item.PowerEnhance++;
                                        break;
                                    case 10087403:
                                        item.MAtk = FindEnhancementValue(item, 10087403);
                                        DeleteItemID(10087403, 1, true);
                                        item.MagEnhance++;
                                        break;
                                    case 10087402:
                                        item.HitCritical = FindEnhancementValue(item, 10087402);
                                        DeleteItemID(10087402, 1, true);
                                        item.CritEnhance++;
                                        break;

                                }
                            }
                            if (item.BaseData.itemType == ItemType.SHIELD)
                            {
                                switch (p.Material)
                                {
                                    //一般水晶
                                    case 90000044:
                                        item.Def = FindEnhancementValue(item, 90000044);
                                        DeleteItemID(90000044, 1, true);
                                        item.PowerEnhance++;
                                        break;
                                    case 90000045:
                                        item.MDef = FindEnhancementValue(item, 90000045);
                                        DeleteItemID(90000045, 1, true);
                                        item.MagEnhance++;
                                        break;

                                    //強化水晶
                                    case 90000054:
                                        item.Def = FindEnhancementValue(item, 90000054);
                                        DeleteItemID(90000054, 1, true);
                                        item.PowerEnhance++;
                                        break;
                                    case 90000055:
                                        item.MDef = FindEnhancementValue(item, 90000055);
                                        DeleteItemID(90000055, 1, true);
                                        item.MagEnhance++;
                                        break;
                                        ;

                                    //超強化水晶
                                    case 16004700:
                                        item.Def = FindEnhancementValue(item, 16004700);
                                        DeleteItemID(16004700, 1, true);
                                        item.PowerEnhance++;
                                        break;
                                    case 16004800:
                                        item.MDef = FindEnhancementValue(item, 16004800);
                                        DeleteItemID(16004800, 1, true);
                                        item.MagEnhance++;
                                        break;

                                    //強化王
                                    case 10087401:
                                        item.Def = FindEnhancementValue(item, 10087401);
                                        DeleteItemID(10087401, 1, true);
                                        item.PowerEnhance++;
                                        break;
                                    case 10087403:
                                        item.MDef = FindEnhancementValue(item, 10087403);
                                        DeleteItemID(10087403, 1, true);
                                        item.MagEnhance++;
                                        break;
                                }
                            }
                            if (item.BaseData.itemType == ItemType.ACCESORY_NECK)
                            {
                                switch (p.Material)
                                {
                                    case 90000045:
                                        item.MDef = FindEnhancementValue(item, 90000045);
                                        DeleteItemID(90000045, 1, true);
                                        item.MagEnhance++;
                                        break;
                                    case 90000055:
                                        item.MDef = FindEnhancementValue(item, 90000055);
                                        DeleteItemID(90000055, 1, true);
                                        item.MagEnhance++;
                                        break;
                                    case 16004800:
                                        item.MDef = FindEnhancementValue(item, 16004800);
                                        DeleteItemID(16004800, 1, true);
                                        item.MagEnhance++;
                                        break;
                                    case 10087403:
                                        item.MDef = FindEnhancementValue(item, 10087403);
                                        DeleteItemID(10087403, 1, true);
                                        item.MagEnhance++;
                                        break;
                                }
                            }

                            SendEffect(5145);
                            p1.Result = 1;

                            item.Refine++;
                            SendItemInfo(item);
                            PC.StatusFactory.Instance.CalcStatus(this.chara);
                            SendPlayerInfo();
                        }
                        else
                        {
                            failed = true;
                            SendEffect(5146);
                            if (!enhanceprotected)
                            {
                                DeleteItem(p.InventorySlot, 1, true);

                                //Delete Material..
                                if (p.Material >= 10087400 && p.Material <= 10087403)
                                {
                                    //No Delete Material
                                }
                                else if (p.Material >= 10087400 && p.Material <= 10087403)
                                {
                                    //No Delete Material
                                }
                                else
                                {
                                    DeleteItemID(p.Material, 1, true);
                                }

                            }
                            else
                            {
                                //Delete Material..
                                if (p.Material >= 10087400 && p.Material <= 10087403)
                                {
                                    //No Delete Material
                                }
                                else if (p.Material >= 10087400 && p.Material <= 10087403)
                                {
                                    //No Delete Material
                                }
                                else
                                {
                                    DeleteItemID(p.Material, 1, true);
                                }

                                //Reset Protected Only
                                if (supportitemid != 10118200)
                                {
                                    item.Clear();
                                    SendItemInfo(item);
                                }
                                
                                //Explode Protected Only
                                if (protectitemid != 16001300)
                                    DeleteItem(p.InventorySlot, 1, true);

                                PC.StatusFactory.Instance.CalcStatus(this.chara);
                                SendPlayerInfo();
                                failed = true;
                            }

                            p1.Result = 0;
                            p1.OrignalRefine = (ushort)(item.Refine + 1);
                            p1.ExpectedRefine = item.Refine;
                            this.netIO.SendPacket(p1);
                            p1.Result = 0x00;
                        }
                    }
                    else
                    {
                        p1.Result = 0x00;
                        p1.OrignalRefine = item.Refine;
                        p1.ExpectedRefine = (ushort)(item.Refine + 1);
                        this.netIO.SendPacket(p1);
                        p1.Result = 0xff;
                    }
                }
                else
                {
                    p1.Result = 0x00;
                    p1.OrignalRefine = item.Refine;
                    p1.ExpectedRefine = (ushort)(item.Refine + 1);
                    this.netIO.SendPacket(p1);
                    p1.Result = 0xfd;
                }
            }
            else
            {
                p1.Result = 0x00;
                p1.OrignalRefine = item.Refine;
                p1.ExpectedRefine = (ushort)(item.Refine + 1);
                this.netIO.SendPacket(p1);
                p1.Result = 0xfe;
            }
            if (((item.IsArmor && ((CountItem(90000044) > 0 || CountItem(90000045) > 0 || CountItem(90000046) > 0 || CountItem(90000043) > 0 || CountItem(10087400) > 0 || CountItem(10087401) > 0) || CountItem(10087402) > 0 || CountItem(10087403) > 0))
            || (item.IsWeapon && (CountItem(90000044) > 0 || CountItem(90000045) > 0 || CountItem(90000046) > 0 || CountItem(10087401) > 0) || CountItem(10087402) > 0 || CountItem(10087403) > 0))
            || (item.BaseData.itemType == ItemType.SHIELD && (CountItem(90000044) > 0 || CountItem(90000045) > 0 || CountItem(10087401) > 0 || CountItem(10087403) > 0))
            || (item.BaseData.itemType == ItemType.ACCESORY_NECK && (CountItem(90000044) > 0 || CountItem(10087403) > 0)) && item.Refine < 30 && this.Character.Gold >= 5000)
            {
                this.netIO.SendPacket(p1);
                Packets.Client.CSMG_ITEM_ENHANCE_SELECT p2 = new SagaMap.Packets.Client.CSMG_ITEM_ENHANCE_SELECT();
                p2.InventorySlot = p.InventorySlot;
                OnItemEnhanceSelect(p2);
                return;
            }
            else
            {
                p1.Result = 0x00;
                p1.OrignalRefine = item.Refine;
                p1.ExpectedRefine = (ushort)(item.Refine + 1);
                this.netIO.SendPacket(p1);
                p1.Result = 0xfd;
            }

            this.netIO.SendPacket(p1);

            if (failed)
            {
                var res = from enhanctitem in this.chara.Inventory.GetContainer(ContainerType.BODY)
                          where (((enhanctitem.IsArmor && ((CountItem(90000044) > 0 || CountItem(90000045) > 0 || CountItem(90000046) > 0 || CountItem(90000043) > 0 || CountItem(10087400) > 0 || CountItem(10087401) > 0) || CountItem(10087402) > 0 || CountItem(10087403) > 0))
            || (enhanctitem.IsWeapon && (CountItem(90000044) > 0 || CountItem(90000045) > 0 || CountItem(90000046) > 0 || CountItem(10087401) > 0) || CountItem(10087402) > 0 || CountItem(10087403) > 0))
            || (enhanctitem.BaseData.itemType == ItemType.SHIELD && (CountItem(90000044) > 0 || CountItem(90000045) > 0 || CountItem(10087401) > 0 || CountItem(10087403) > 0))
            || (enhanctitem.BaseData.itemType == ItemType.ACCESORY_NECK && (CountItem(90000044) > 0 || CountItem(10087403) > 0)) && item.Refine < 30 && this.Character.Gold >= 5000)
                          select enhanctitem;
                List<SagaDB.Item.Item> items = res.ToList();

                foreach (var itemsitem in res.ToList())
                {
                    if (itemsitem.IsArmor)
                    {
                        //生命结晶
                        if (CountItem(90000043) > 0)
                            if (!items.Exists(x => x.ItemID == 90000043))
                                items.AddRange(GetItem(90000043));
                        //力量结晶
                        if (CountItem(90000044) > 0)
                            if (!items.Exists(x => x.ItemID == 90000044))
                                items.AddRange(GetItem(90000044));
                        //会心结晶
                        if (CountItem(90000046) > 0)
                            if (!items.Exists(x => x.ItemID == 90000046))
                                items.AddRange(GetItem(90000046));
                        //魔力结晶
                        if (CountItem(90000045) > 0)
                            if (!items.Exists(x => x.ItemID == 90000045))
                                items.AddRange(GetItem(90000045));

                        //生命強化结晶
                        if (CountItem(90000053) > 0)
                            if (!items.Exists(x => x.ItemID == 90000053))
                                items.AddRange(GetItem(90000053));
                        //力量強化结晶
                        if (CountItem(90000054) > 0)
                            if (!items.Exists(x => x.ItemID == 90000054))
                                items.AddRange(GetItem(90000054));
                        //会心強化结晶
                        if (CountItem(90000056) > 0)
                            if (!items.Exists(x => x.ItemID == 90000056))
                                items.AddRange(GetItem(90000056));
                        //魔力強化结晶
                        if (CountItem(90000055) > 0)
                            if (!items.Exists(x => x.ItemID == 90000055))
                                items.AddRange(GetItem(90000055));



                        //生命超強化结晶
                        if (CountItem(16004600) > 0)
                            if (!items.Exists(x => x.ItemID == 90000053))
                                items.AddRange(GetItem(90000053));
                        //力量超強化结晶
                        if (CountItem(16004700) > 0)
                            if (!items.Exists(x => x.ItemID == 90000054))
                                items.AddRange(GetItem(90000054));
                        //会心超強化结晶
                        if (CountItem(16004500) > 0)
                            if (!items.Exists(x => x.ItemID == 90000056))
                                items.AddRange(GetItem(90000056));
                        //魔力超強化结晶
                        if (CountItem(16004800) > 0)
                            if (!items.Exists(x => x.ItemID == 90000055))
                                items.AddRange(GetItem(90000055));


                        //强化王的生命
                        if (CountItem(10087400) > 0)
                            if (!items.Exists(x => x.ItemID == 10087400))
                                items.AddRange(GetItem(10087400));
                        //强化王的力量
                        if (CountItem(10087401) > 0)
                            if (!items.Exists(x => x.ItemID == 10087401))
                                items.AddRange(GetItem(10087401));
                        //强化王的会心
                        if (CountItem(10087402) > 0)
                            if (!items.Exists(x => x.ItemID == 10087402))
                                items.AddRange(GetItem(10087402));
                        //强化王的魔力
                        if (CountItem(10087403) > 0)
                            if (!items.Exists(x => x.ItemID == 10087403))
                                items.AddRange(GetItem(10087403));

                    }
                    if (itemsitem.IsWeapon)
                    {

                        //力量结晶
                        if (CountItem(90000044) > 0)
                            if (!items.Exists(x => x.ItemID == 90000044))
                                items.AddRange(GetItem(90000044));
                        //会心结晶
                        if (CountItem(90000046) > 0)
                            if (!items.Exists(x => x.ItemID == 90000046))
                                items.AddRange(GetItem(90000046));
                        //魔力结晶
                        if (CountItem(90000045) > 0)
                            if (!items.Exists(x => x.ItemID == 90000045))
                                items.AddRange(GetItem(90000045));


                        //力量強化结晶
                        if (CountItem(90000054) > 0)
                            if (!items.Exists(x => x.ItemID == 90000054))
                                items.AddRange(GetItem(90000054));
                        //会心強化结晶
                        if (CountItem(90000056) > 0)
                            if (!items.Exists(x => x.ItemID == 90000056))
                                items.AddRange(GetItem(90000056));
                        //魔力強化结晶
                        if (CountItem(90000055) > 0)
                            if (!items.Exists(x => x.ItemID == 90000055))
                                items.AddRange(GetItem(90000055));




                        //力量超強化结晶
                        if (CountItem(16004700) > 0)
                            if (!items.Exists(x => x.ItemID == 90000054))
                                items.AddRange(GetItem(90000054));
                        //会心超強化结晶
                        if (CountItem(16004500) > 0)
                            if (!items.Exists(x => x.ItemID == 90000056))
                                items.AddRange(GetItem(90000056));
                        //魔力超強化结晶
                        if (CountItem(16004800) > 0)
                            if (!items.Exists(x => x.ItemID == 90000055))
                                items.AddRange(GetItem(90000055));


                        //强化王的力量
                        if (CountItem(10087401) > 0)
                            if (!items.Exists(x => x.ItemID == 10087401))
                                items.AddRange(GetItem(10087401));
                        //强化王的会心
                        if (CountItem(10087402) > 0)
                            if (!items.Exists(x => x.ItemID == 10087402))
                                items.AddRange(GetItem(10087402));
                        //强化王的魔力
                        if (CountItem(10087403) > 0)
                            if (!items.Exists(x => x.ItemID == 10087403))
                                items.AddRange(GetItem(10087403));



                    }
                    if (itemsitem.BaseData.itemType == ItemType.SHIELD)
                    {

                        //力量结晶
                        if (CountItem(90000044) > 0)
                            if (!items.Exists(x => x.ItemID == 90000044))
                                items.AddRange(GetItem(90000044));

                        //魔力结晶
                        if (CountItem(90000045) > 0)
                            if (!items.Exists(x => x.ItemID == 90000045))
                                items.AddRange(GetItem(90000045));


                        //力量強化结晶
                        if (CountItem(90000054) > 0)
                            if (!items.Exists(x => x.ItemID == 90000054))

                                //魔力強化结晶
                                if (CountItem(90000055) > 0)
                                    if (!items.Exists(x => x.ItemID == 90000055))
                                        items.AddRange(GetItem(90000055));



                        //力量超強化结晶
                        if (CountItem(16004700) > 0)
                            if (!items.Exists(x => x.ItemID == 90000054))
                                items.AddRange(GetItem(90000054));

                        //魔力超強化结晶
                        if (CountItem(16004800) > 0)
                            if (!items.Exists(x => x.ItemID == 90000055))
                                items.AddRange(GetItem(90000055));



                        //强化王的力量
                        if (CountItem(10087401) > 0)
                            if (!items.Exists(x => x.ItemID == 10087401))
                                items.AddRange(GetItem(10087401));

                        //强化王的魔力
                        if (CountItem(10087403) > 0)
                            if (!items.Exists(x => x.ItemID == 10087403))
                                items.AddRange(GetItem(10087403));




                    }
                    if (itemsitem.BaseData.itemType == ItemType.ACCESORY_NECK)
                    {

                        //魔力结晶
                        if (CountItem(90000045) > 0)
                            if (!items.Exists(x => x.ItemID == 90000045))
                                items.AddRange(GetItem(90000045));


                        //魔力強化结晶
                        if (CountItem(90000055) > 0)
                            if (!items.Exists(x => x.ItemID == 90000055))
                                items.AddRange(GetItem(90000055));



                        //魔力超強化结晶
                        if (CountItem(16004800) > 0)
                            if (!items.Exists(x => x.ItemID == 90000055))
                                items.AddRange(GetItem(90000055));


                        //强化王的魔力
                        if (CountItem(10087403) > 0)
                            if (!items.Exists(x => x.ItemID == 10087403))
                                items.AddRange(GetItem(10087403));
                    }

                    //Golbal Item

                    //防爆判断
                    if (CountItem(16001300) > 0)
                        if (!items.Exists(x => x.ItemID == 16001300))
                            items.AddRange(GetItem(16001300));

                    //防RESET判断
                    if (CountItem(10118200) > 0)
                        if (!items.Exists(x => x.ItemID == 10118200))
                            items.AddRange(GetItem(10118200));


                    //奥义判断
                    if (CountItem(10087200) > 0)
                        if (!items.Exists(x => x.ItemID == 10087200))
                            items.AddRange(GetItem(10087200));
                    //精髓判断
                    if (CountItem(10087201) > 0)
                        if (!items.Exists(x => x.ItemID == 10087201))
                            items.AddRange(GetItem(10087201));
                }

                items = items.OrderBy(x => x.Slot).ToList();
                if (items.Count > 0)
                {
                    Packets.Server.SSMG_ITEM_ENHANCE_LIST p2 = new SagaMap.Packets.Server.SSMG_ITEM_ENHANCE_LIST();
                    p2.Items = items;
                    this.netIO.SendPacket(p2);
                }
                else
                {
                    this.itemEnhance = false;
                    p1 = new SagaMap.Packets.Server.SSMG_ITEM_ENHANCE_RESULT();
                    p1.Result = 0x00;
                    p1.OrignalRefine = item.Refine;
                    p1.ExpectedRefine = (ushort)(item.Refine + 1);
                    this.netIO.SendPacket(p1);
                    p1.Result = 0xfd;
                    this.netIO.SendPacket(p1);
                }
            }
        }
        public void OnItemUse(Packets.Client.CSMG_ITEM_USE p)
        {
            if (this.Character.Status.Additions.ContainsKey("Meditatioon"))
            {
                this.Character.Status.Additions["Meditatioon"].AdditionEnd();
                this.Character.Status.Additions.Remove("Meditatioon");
            }
            if (this.Character.Status.Additions.ContainsKey("Hiding"))
            {
                this.Character.Status.Additions["Hiding"].AdditionEnd();
                this.Character.Status.Additions.Remove("Hiding");
            }
            if (this.Character.Status.Additions.ContainsKey("fish"))
            {
                this.Character.Status.Additions["fish"].AdditionEnd();
                this.Character.Status.Additions.Remove("fish");
            }
            if (this.Character.Status.Additions.ContainsKey("Cloaking"))
            {
                this.Character.Status.Additions["Cloaking"].AdditionEnd();
                this.Character.Status.Additions.Remove("Cloaking");
            }
            if (this.Character.Status.Additions.ContainsKey("IAmTree"))
            {
                this.Character.Status.Additions["IAmTree"].AdditionEnd();
                this.Character.Status.Additions.Remove("IAmTree");
            }
            if (this.Character.Status.Additions.ContainsKey("Invisible"))
            {
                this.Character.Status.Additions["Invisible"].AdditionEnd();
                this.Character.Status.Additions.Remove("Invisible");
            }
            Item item = this.Character.Inventory.GetItem(p.InventorySlot);
            SkillArg arg = new SkillArg();
            arg.sActor = this.Character.ActorID;
            arg.dActor = p.ActorID;
            arg.item = item;
            arg.x = p.X;
            arg.y = p.Y;
            arg.argType = SkillArg.ArgType.Item_Cast;
            arg.inventorySlot = p.InventorySlot;
            if (item == null)
                return;
            Actor dActor = this.map.GetActor(p.ActorID);

            if (Character.Account.GMLevel > 0)
                FromActorPC(Character).SendSystemMessage("道具ID：" + item.ItemID.ToString());

            if (item.BaseData.itemType == ItemType.POTION || item.BaseData.itemType == ItemType.FOOD)
            {
                if (Character.Status.Additions.ContainsKey("FOODCD"))
                {
                    FromActorPC(Character).SendSystemMessage("暂时吃不下食物了哦...(30秒CD)");
                    arg.result = -21;
                }
                else
                {
                    //Skill.Additions.Global.DefaultBuff cd = new Skill.Additions.Global.DefaultBuff(Character, "FOODCD", 30000);
                    //SkillHandler.ApplyAddition(Character, cd);
                }

            }

            if (this.Character.PossessionTarget != 0)
            {
                Actor posse = this.Map.GetActor(this.Character.PossessionTarget);
                if (posse != null)
                {
                    if (posse.type == ActorType.PC)
                    {
                        if (arg.dActor == this.Character.ActorID)
                            arg.dActor = posse.ActorID;
                    }
                    else
                    {
                        arg.result = -21;
                    }
                }
            }
            if (item.BaseData.itemType == ItemType.MARIONETTE && arg.result == 0)
            {
                if (this.Character.Marionette == null)
                {
                    if (DateTime.Now < this.Character.NextMarionetteTime)
                        arg.result = -18;
                }
                if (this.Character.Pet != null)
                {
                    if (this.Character.Pet.Ride)
                        arg.result = -32;
                }
                if (this.Character.PossessionTarget != 0 || this.Character.PossesionedActors.Count > 0)
                {
                    arg.result = -16;
                }
                if (this.chara.Race == PC_RACE.DEM)
                {
                    arg.result = -33;
                }
            }
            if (this.GetPossessionTarget() != null && arg.result == 0)
            {
                if (this.GetPossessionTarget().Buff.Dead && !(item.ItemID == 10000604 || item.ItemID == 10034104))
                    arg.result = -27;
                if (arg.result == 0)
                {
                    if (item.ItemID == 10022900)
                        arg.result = -3;
                }
            }
            if (dActor != null && arg.result == 0)
            {
                if (!dActor.Buff.Dead && (item.ItemID == 10000604 || item.ItemID == 10034104))
                {
                    arg.result = -23;
                }
            }
            if (this.scriptThread != null && arg.result == 0)
            {
                arg.result = -7;
            }

            if (this.Character.Buff.Dead && arg.result == 0)
            {
                arg.result = -9;
            }
            if (this.Character.Buff.GetReadyPossession && arg.result == 0)
                arg.result = -3;

            if (arg.result == 0)
            {
                if (this.Character.Tasks.ContainsKey("ItemCast"))
                    arg.result = -19;
            }
            if (arg.result == 0)
            {
                if (item.BaseData.itemType == ItemType.UNION_FOOD)
                {
                    if (!OnPartnerFeed(item.Slot)) return;
                }
                this.Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, arg, this.Character, true);
                uint casttime = item.BaseData.cast;
                if (item.BaseData.itemType == ItemType.POTION || item.BaseData.itemType == ItemType.FOOD)
                    casttime = 2000;
                if (item.BaseData.cast > 0)
                {
                    Tasks.PC.SkillCast task = new SagaMap.Tasks.PC.SkillCast(this, arg);
                    this.Character.Tasks.Add("ItemCast", task);
                    task.Activate();
                }
                else
                {
                    OnItemCastComplete(arg);
                }

                //Cancel Cloak
                if (this.Character.Status.Additions.ContainsKey("Cloaking"))
                    SagaMap.Skill.SkillHandler.RemoveAddition(this.Character, "Cloaking");

                

                if (this.Character.PossessionTarget != 0)
                {
                    Map map = Manager.MapManager.Instance.GetMap(this.Character.MapID);
                    Actor TargetPossessionActor = map.GetActor(this.Character.PossessionTarget);

                    if (TargetPossessionActor.Status.Additions.ContainsKey("Cloaking"))
                        SagaMap.Skill.SkillHandler.RemoveAddition(TargetPossessionActor, "Cloaking");
                }
            }
            else
            {
                this.Character.e.OnActorSkillUse(this.Character, arg);
            }
        }

        public void OnItemCastComplete(SkillArg skill)
        {
            if (this.Character.Status.Additions.ContainsKey("Meditatioon"))
            {
                this.Character.Status.Additions["Meditatioon"].AdditionEnd();
                this.Character.Status.Additions.Remove("Meditatioon");
            }
            if (this.Character.Status.Additions.ContainsKey("Hiding"))
            {
                this.Character.Status.Additions["Hiding"].AdditionEnd();
                this.Character.Status.Additions.Remove("Hiding");
            }
            if (this.Character.Status.Additions.ContainsKey("fish"))
            {
                this.Character.Status.Additions["fish"].AdditionEnd();
                this.Character.Status.Additions.Remove("fish");
            }
            if (this.Character.Status.Additions.ContainsKey("IAmTree"))
            {
                this.Character.Status.Additions["IAmTree"].AdditionEnd();
                this.Character.Status.Additions.Remove("IAmTree");
            }
            if (this.Character.Status.Additions.ContainsKey("Cloaking"))
            {
                this.Character.Status.Additions["Cloaking"].AdditionEnd();
                this.Character.Status.Additions.Remove("Cloaking");
            }
            if (this.Character.Status.Additions.ContainsKey("Invisible"))
            {
                this.Character.Status.Additions["Invisible"].AdditionEnd();
                this.Character.Status.Additions.Remove("Invisible");
            }

            if (skill.dActor != 0xFFFFFFFF)
            {
                Actor dActor = this.Map.GetActor(skill.dActor);
                this.Character.Tasks.Remove("ItemCast");
                skill.argType = SkillArg.ArgType.Item_Active;
                SkillHandler.Instance.ItemUse(this.Character, dActor, skill);
            }
            else
            {
                this.Character.Tasks.Remove("ItemCast");
                skill.argType = SkillArg.ArgType.Item_Active;
            }

            if (skill.item.BaseData.usable || skill.item.BaseData.itemType == ItemType.POTION ||
                skill.item.BaseData.itemType == ItemType.SCROLL ||
                skill.item.BaseData.itemType == ItemType.FREESCROLL)
            {
                if (skill.item.Durability > 0)
                    skill.item.Durability--;
                SendItemInfo(skill.item);
                if (skill.item.Durability == 0)
                {
                    Logger.LogItemLost(Logger.EventType.ItemUseLost, this.Character.Name + "(" + this.Character.CharID + ")", skill.item.BaseData.name + "(" + skill.item.ItemID + ")",
                           string.Format("ItemUse Count:{0}", 1), false);
                    DeleteItem(skill.inventorySlot, 1, true);
                }
            }
            this.Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, skill, this.Character, true);
            if (skill.item.BaseData.effectID != 0)
            {
                EffectArg eff = new EffectArg();
                eff.actorID = skill.dActor;
                eff.effectID = skill.item.BaseData.effectID;
                this.Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, eff, this.Character, true);
            }
            if (skill.item.ItemID >= 10047800 && skill.item.ItemID <= 10047852)
            {
                OnItemRepair(skill.item);
            }
            else if (skill.item.BaseData.activateSkill != 0)
            {
                Packets.Client.CSMG_SKILL_CAST p1 = new SagaMap.Packets.Client.CSMG_SKILL_CAST();
                p1.ActorID = skill.dActor;
                p1.SkillID = skill.item.BaseData.activateSkill;
                p1.SkillLv = 1;
                p1.X = skill.x;
                p1.Y = skill.y;
                p1.Random = (short)Global.Random.Next();
                OnSkillCast(p1, true, true);
            }
            if (skill.item.BaseData.abnormalStatus.ContainsKey(AbnormalStatus.Poisen))
            {
                if (skill.item.BaseData.abnormalStatus[AbnormalStatus.Poisen] == 100)
                {
                    if (this.Character.Status.Additions.ContainsKey("Poison"))
                    {
                        this.Character.Status.Additions["Poison"].AdditionEnd();
                        this.Character.Status.Additions.Remove("Poison");
                    }
                }
            }
            if (skill.item.BaseData.itemType == ItemType.MARIONETTE)
            {
                if (this.Character.Marionette == null)
                {
                    MarionetteActivate(skill.item.BaseData.marionetteID);
                }
                else
                {
                    if (!this.Character.Status.Additions.ContainsKey("ChangeMarionette"))
                        MarionetteDeactivate();
                    else
                        MarionetteActivate(skill.item.BaseData.marionetteID, false, false);
                    return;
                }
            }
            if (skill.item.BaseData.eventID != 0)
            {
                if (skill.item.BaseData.eventID == 90000529)
                    Character.TInt["技能块ItemID"] = (int)skill.item.ItemID;
                EventActivate(skill.item.BaseData.eventID);
            }

            if (skill.item.ItemID > kujiboxID0 && skill.item.ItemID <= kujiboxID0 + kujinum_max)
            {
                DeleteItem(skill.inventorySlot, 1, false);
                OnKujiBoxOpen(skill.item);
            }
            if (skill.item.BaseData.itemType == ItemType.GOLEM)
            {
                if (this.Character.Golem == null)
                    this.Character.Golem = new ActorGolem();
                this.Character.Golem.Item = skill.item;
                EventActivate(0xFFFFFF33);
            }
        }

        int GetKujiRare(List<Kuji> kuji)
        {//
            int min = int.MaxValue;
            for (int i = 0; i < kuji.Count; i++)
            {
                min = Math.Min(min, kuji[0].rank);
            }
            return min;
        }
        private void OnKujiBoxOpen(Item box)
        {
            uint kujiID = box.ItemID - kujiboxID0;

            if (KujiListFactory.Instance.KujiList.ContainsKey(kujiID))
            {
                List<Kuji> kujis = KujiListFactory.Instance.KujiList[kujiID];
                if (kujis.Count == 0) return;
                int rare = GetKujiRare(KujiListFactory.Instance.KujiList[kujiID]);

                List<int> rates = new List<int>();
                int r = 0;
                for (int i = 0; i < kujis.Count; i++)
                {
                    r = r + kujis[i].rate;
                    rates.Add(r);
                }
                SkillHandler.Instance.ShowEffectOnActor(Character, 8056);
                int ratemin = 0;
                int ratemax = rates[rates.Count - 1];
                int ran = Global.Random.Next(ratemin, ratemax);
                for (int i = 0; i < kujis.Count; i++)
                {
                    if (ran <= rates[i])
                    {
                        switch (kujis[i].rank)
                        {
                            case 1:
                                Character.AInt["SSS保底次数"] = 0;
                                SendSystemMessage("啧，可恶的欧洲人");
                                break;
                            case 2:
                            case 3:
                                Character.AInt["SS保底次数"] = 0;
                                SendSystemMessage("嘁，可恶的欧洲人");
                                break;
                            default:
                                switch (rare)
                                {
                                    case 1:
                                        Character.AInt["SSS保底次数"]++;
                                        SendSystemMessage("如果连续200次未抽到SSS级头赏，将获得彩虹钥匙。当前次数：" + Character.AInt["SSS保底次数"].ToString() + "/200");
                                        if (Character.AInt["SSS保底次数"] >= 200)
                                        {
                                            Character.AInt["SSS保底次数累计"]++;
                                            Character.AInt["SSS保底次数"] = 0;
                                            SendSystemMessage("由于您连续200次未抽到SSS级头赏，获得了彩虹钥匙。");
                                            Item item = ItemFactory.Instance.GetItem(950000032);
                                            AddItem(item, true);
                                        }
                                        break;
                                    case 2:
                                    case 3:
                                        Character.AInt["SS保底次数"]++;
                                        SendSystemMessage("如果连续200次未抽到SS/S级头赏，将获得金钥匙。当前次数：" + Character.AInt["SS保底次数"].ToString() + "/200");
                                        if (Character.AInt["SS保底次数"] >= 200)
                                        {
                                            Character.AInt["SS保底次数累计"]++;
                                            Character.AInt["SS保底次数"] = 0;
                                            SendSystemMessage("由于您连续200次未抽到SS/S级头赏，获得了金钥匙。");
                                            Item item = ItemFactory.Instance.GetItem(950000031);
                                            AddItem(item, true);
                                            TitleProccess(Character, 8, 1);
                                        }
                                        break;
                                    default:
                                        break;
                                }
                                break;
                        }
                        Item kuji = ItemFactory.Instance.GetItem(kujis[i].itemid);
                        AddItem(kuji, true);
                        break;
                    }
                }
            }
        }

        public void OnItemRepair(SagaDB.Item.Item item)
        {
            List<Item> RepairItems = new List<Item>();
            foreach (var i in this.Character.Inventory.Items)
            {
                foreach (Item items in i.Value)
                {
                    if (items.BaseData.repairItem == item.BaseData.id)
                    {
                        RepairItems.Add(items);
                    }
                }
            }
            Packets.Server.SSMG_ITEM_EQUIP_REPAIR_LIST p = new Packets.Server.SSMG_ITEM_EQUIP_REPAIR_LIST();
            p.Items = RepairItems;
            this.netIO.SendPacket(p);
        }

        public void OnItemDrop(Packets.Client.CSMG_ITEM_DROP p)
        {
            if (this.Character.Account.AccountID > 200)
            {
                Item itemDroped2 = this.Character.Inventory.GetItem(p.InventorySlot);
                SendSystemMessage(itemDroped2.BaseData.id.ToString());
            }
            if (this.Character.Status.Additions.ContainsKey("Meditatioon"))
            {
                this.Character.Status.Additions["Meditatioon"].AdditionEnd();
                this.Character.Status.Additions.Remove("Meditatioon");
            }
            if (this.Character.Status.Additions.ContainsKey("Hiding"))
            {
                this.Character.Status.Additions["Hiding"].AdditionEnd();
            }
            if (this.Character.Status.Additions.ContainsKey("Cloaking"))
            {
                this.Character.Status.Additions["Cloaking"].AdditionEnd();
            }
            if (this.Character.Status.Additions.ContainsKey("fish"))
            {
                this.Character.Status.Additions["fish"].AdditionEnd();
                this.Character.Status.Additions.Remove("fish");
            }
            if (this.Character.Status.Additions.ContainsKey("IAmTree"))
            {
                this.Character.Status.Additions["IAmTree"].AdditionEnd();
                this.Character.Status.Additions.Remove("IAmTree");
            }
            if (this.Character.Status.Additions.ContainsKey("Invisible"))
            {
                this.Character.Status.Additions["Invisible"].AdditionEnd();
                this.Character.Status.Additions.Remove("Invisible");
            }
            Item itemDroped = this.Character.Inventory.GetItem(p.InventorySlot);
            ushort count = p.Count;
            if (count > itemDroped.Stack)
                count = itemDroped.Stack;
            Packets.Server.SSMG_ITEM_PUT_ERROR p1 = new SagaMap.Packets.Server.SSMG_ITEM_PUT_ERROR();
            if (itemDroped.BaseData.events == 1)
            {
                p1.ErrorID = -3;
                this.netIO.SendPacket(p1);
                return;
            }
            if (this.trading == true)
            {
                p1.ErrorID = -8;
                this.netIO.SendPacket(p1);
                return;
            }

            if (itemDroped.BaseData.noTrade)
            {
                p1.ErrorID = -16;
                this.netIO.SendPacket(p1);
                return;
            }

            if (itemDroped.BaseData.itemType == ItemType.DEMIC_CHIP)
            {
                p1.ErrorID = -18;
                this.netIO.SendPacket(p1);
                return;
            }

            if (itemDroped.Stack > 0)
            {
                Logger.LogItemLost(Logger.EventType.ItemDropLost, this.Character.Name + "(" + this.Character.CharID + ")", itemDroped.BaseData.name + "(" + itemDroped.ItemID + ")",
                    string.Format("Drop Count:{0}", count), false);
            }

            InventoryDeleteResult result = this.Character.Inventory.DeleteItem(p.InventorySlot, count);
            switch (result)
            {
                case InventoryDeleteResult.STACK_UPDATED:
                    Packets.Server.SSMG_ITEM_COUNT_UPDATE p2 = new SagaMap.Packets.Server.SSMG_ITEM_COUNT_UPDATE();
                    Item item = this.Character.Inventory.GetItem(p.InventorySlot);
                    itemDroped = item.Clone();
                    itemDroped.Stack = count;
                    p2.InventorySlot = p.InventorySlot;
                    p2.Stack = item.Stack;
                    this.netIO.SendPacket(p2);
                    break;
                case InventoryDeleteResult.ALL_DELETED:
                    Packets.Server.SSMG_ITEM_DELETE p3 = new SagaMap.Packets.Server.SSMG_ITEM_DELETE();
                    p3.InventorySlot = p.InventorySlot;
                    this.netIO.SendPacket(p3);
                    break;
            }
            ActorItem actor = new ActorItem(itemDroped);
            actor.e = new ActorEventHandlers.ItemEventHandler(actor);
            actor.MapID = this.Character.MapID;
            actor.X = this.Character.X;
            actor.Y = this.Character.Y;
            if (!itemDroped.BaseData.noTrade) //7月27日更新，取消交易
            {
                actor.Owner = chara;
                actor.CreateTime = DateTime.Now;
            }
            this.map.RegisterActor(actor);
            actor.invisble = false;
            this.map.OnActorVisibilityChange(actor);
            Tasks.Item.DeleteItem task = new SagaMap.Tasks.Item.DeleteItem(actor);
            task.Activate();
            actor.Tasks.Add("DeleteItem", task);

            this.Character.Inventory.CalcPayloadVolume();
            this.SendCapacity();

            this.SendSystemMessage(string.Format(LocalManager.Instance.Strings.ITEM_DELETED, itemDroped.BaseData.name, itemDroped.Stack));
        }

        public void OnItemGet(Packets.Client.CSMG_ITEM_GET p)
        {
            if (this.Character.Status.Additions.ContainsKey("Meditatioon"))
            {
                this.Character.Status.Additions["Meditatioon"].AdditionEnd();
                this.Character.Status.Additions.Remove("Meditatioon");
            }
            if (this.Character.Status.Additions.ContainsKey("Hiding"))
            {
                this.Character.Status.Additions["Hiding"].AdditionEnd();
            }
            if (this.Character.Status.Additions.ContainsKey("Cloaking"))
            {
                this.Character.Status.Additions["Cloaking"].AdditionEnd();
            }
            if (this.Character.Status.Additions.ContainsKey("IAmTree"))
            {
                this.Character.Status.Additions["IAmTree"].AdditionEnd();
                this.Character.Status.Additions.Remove("IAmTree");
            }
            if (this.Character.Status.Additions.ContainsKey("Invisible"))
            {
                this.Character.Status.Additions["Invisible"].AdditionEnd();
                this.Character.Status.Additions.Remove("Invisible");
            }
            ActorItem item = (ActorItem)this.map.GetActor(p.ActorID);
            if (item == null)
                return;
            if (item.Owner != null)
            {
                if (item.Owner.type == ActorType.PC)
                {
                    ActorPC pc = (ActorPC)item.Owner;
                    if (pc != Character && !item.Roll)
                    {
                        if (pc.Party != null && !item.Party)
                        {
                            if (!pc.Party.IsMember(this.Character) && !item.Party)
                            {
                                if ((DateTime.Now - item.CreateTime).TotalMinutes < 1)
                                {
                                    Packets.Server.SSMG_ITEM_GET_ERROR p1 = new SagaMap.Packets.Server.SSMG_ITEM_GET_ERROR();
                                    p1.ActorID = item.ActorID;
                                    p1.ErrorID = -10;
                                    this.netIO.SendPacket(p1);
                                    return;
                                }
                            }
                        }
                        else
                        {
                            if ((DateTime.Now - item.CreateTime).TotalSeconds < 30 || item.Party)
                            {
                                Packets.Server.SSMG_ITEM_GET_ERROR p1 = new SagaMap.Packets.Server.SSMG_ITEM_GET_ERROR();
                                p1.ActorID = item.ActorID;
                                p1.ErrorID = -10;
                                this.netIO.SendPacket(p1);
                                return;
                            }
                        }
                    }
                }
            }
            if (!item.PossessionItem)
            {
                if (Character.Party != null)
                {
                    if ((item.Roll) || (Character.Party.Roll == 0 && Character.Party != null))
                    {
                        bool mes = true;
                        if (Character.Party.Roll == 0) mes = false;
                        if (item.Roll) mes = true;
                        ActorPC winner = Character;
                        int MaxRate = 0;
                        foreach (var it in Character.Party.Members.Values)
                        {
                            if (it.MapID == Character.MapID && it.Online)
                            {
                                int rate = Global.Random.Next(0, 100);
                                if (rate > MaxRate)
                                {
                                    winner = it;
                                    MaxRate = rate;
                                }
                                foreach (var item2 in Character.Party.Members.Values)
                                    if (mes && item2.Online)
                                        FromActorPC(item2).SendSystemMessage(it.Name + " 的拾取点数为:" + rate.ToString());
                            }
                        }
                        string a = "";
                        if (mes)
                            a = "的点数最大，";
                        foreach (var item2 in Character.Party.Members.Values)
                            if (item2.Online)
                                FromActorPC(item2).SendSystemMessage(winner.Name + a + " 获得了物品[" + item.Name + "]" + item.Item.Stack.ToString() + "个。");
                        item.LootedBy = winner.ActorID;
                        this.map.DeleteActor(item);
                        Logger.LogItemGet(Logger.EventType.ItemLootGet, this.Character.Name + "(" + this.Character.CharID + ")", item.Item.BaseData.name + "(" + item.Item.ItemID + ")",
                            string.Format("ItemLoot Count:{0}", item.Item.Stack), false);
                        FromActorPC(winner).AddItem(item.Item, true);

                        item.Tasks["DeleteItem"].Deactivate();
                        item.Tasks.Remove("DeleteItem");
                    }
                    else
                    {
                        item.LootedBy = this.Character.ActorID;
                        this.map.DeleteActor(item);
                        Logger.LogItemGet(Logger.EventType.ItemLootGet, this.Character.Name + "(" + this.Character.CharID + ")", item.Item.BaseData.name + "(" + item.Item.ItemID + ")",
                            string.Format("ItemLoot Count:{0}", item.Item.Stack), false);
                        AddItem(item.Item, true);

                        item.Tasks["DeleteItem"].Deactivate();
                        item.Tasks.Remove("DeleteItem");
                    }
                }
                else
                {
                    item.LootedBy = this.Character.ActorID;
                    this.map.DeleteActor(item);
                    Logger.LogItemGet(Logger.EventType.ItemLootGet, this.Character.Name + "(" + this.Character.CharID + ")", item.Item.BaseData.name + "(" + item.Item.ItemID + ")",
                        string.Format("ItemLoot Count:{0}", item.Item.Stack), false);
                    AddItem(item.Item, true);

                    item.Tasks["DeleteItem"].Deactivate();
                    item.Tasks.Remove("DeleteItem");
                }
            }
            else
            {
                foreach (EnumEquipSlot i in item.Item.EquipSlot)
                {
                    if (this.Character.Inventory.Equipments.ContainsKey(i))
                    {
                        Packets.Server.SSMG_ITEM_GET_ERROR p1 = new SagaMap.Packets.Server.SSMG_ITEM_GET_ERROR();
                        p1.ActorID = item.ActorID;
                        p1.ErrorID = -5;
                        this.netIO.SendPacket(p1);
                        return;
                    }
                }
                if (this.chara.Race == PC_RACE.DEM && this.chara.Form == DEM_FORM.MACHINA_FORM)
                {
                    Packets.Server.SSMG_ITEM_GET_ERROR p1 = new SagaMap.Packets.Server.SSMG_ITEM_GET_ERROR();
                    p1.ActorID = item.ActorID;
                    p1.ErrorID = -16;
                    this.netIO.SendPacket(p1);
                    return;

                }
                if (Math.Abs(this.Character.Level - item.Item.PossessionedActor.Level) > 30)
                {
                    Packets.Server.SSMG_ITEM_GET_ERROR p1 = new SagaMap.Packets.Server.SSMG_ITEM_GET_ERROR();
                    p1.ActorID = item.ActorID;
                    p1.ErrorID = -4;
                    this.netIO.SendPacket(p1);
                    return;
                }
                int result = CheckEquipRequirement(item.Item);
                if (result < 0)
                {
                    Packets.Server.SSMG_ITEM_EQUIP p4 = new SagaMap.Packets.Server.SSMG_ITEM_EQUIP();
                    p4.InventorySlot = 0xffffffff;
                    p4.Target = ContainerType.NONE;
                    p4.Result = result;
                    p4.Range = this.Character.Range;
                    this.netIO.SendPacket(p4);
                    return;
                }
                //临时处理手段
                SendSystemMessage("自凭依道具暂时无法捡起来");
                //MapClient.FromActorPC(PC).SendSystemMessage("伙伴 " + partner.Name + " 获得了" + exp + "点经验值");
                //item.LootedBy = this.Character.ActorID;
                //this.map.DeleteActor(item);
                //Item addItem = item.Item.Clone();
                //AddItem(addItem, true);
                //Packets.Client.CSMG_ITEM_EQUIPT p2 = new SagaMap.Packets.Client.CSMG_ITEM_EQUIPT();
                //p2.InventoryID = addItem.Slot;
                //OnItemEquipt(p2);
            }

            //this.SendItems();

            this.SendPlayerInfo();
        }

        public int CheckEquipRequirement(Item item)
        {

            if (this.Character.Buff.Dead || this.Character.Buff.Confused || this.Character.Buff.Frosen || this.Character.Buff.Paralysis || this.Character.Buff.Sleep || this.Character.Buff.Stone || this.Character.Buff.Stun)
                return -3;
            switch (item.BaseData.itemType)
            {
                case ItemType.ARROW:
                    if (this.Character.Inventory.Equipments.ContainsKey(EnumEquipSlot.RIGHT_HAND))
                    {
                        if (this.Character.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].BaseData.itemType != ItemType.BOW)
                            return -6;
                    }
                    else
                    {
                        return -6;
                    }
                    if (this.Character.Inventory.Equipments.ContainsKey(EnumEquipSlot.LEFT_HAND))
                    {
                        Item oriItem = this.Character.Inventory.Equipments[EnumEquipSlot.LEFT_HAND];
                        if (oriItem.PossessionedActor != null)
                        {
                            return -10;
                        }
                    }
                    break;
                case ItemType.BULLET:
                    if (this.Character.Inventory.Equipments.ContainsKey(EnumEquipSlot.RIGHT_HAND))
                    {
                        if (this.Character.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].BaseData.itemType != ItemType.GUN &&
                            this.Character.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].BaseData.itemType != ItemType.RIFLE &&
                            this.Character.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].BaseData.itemType != ItemType.DUALGUN)
                        {
                            return -7;
                        }
                    }
                    else
                    {
                        return -7;
                    }
                    if (this.Character.Inventory.Equipments.ContainsKey(EnumEquipSlot.LEFT_HAND))
                    {
                        Item oriItem = this.Character.Inventory.Equipments[EnumEquipSlot.LEFT_HAND];
                        if (oriItem.PossessionedActor != null)
                        {
                            return -10;
                        }
                    }
                    break;
            }
            if (item.Durability < 1 && item.maxDurability >= 1)
                return -12;
            if (this.Character.Str < item.BaseData.possibleStr)
                return -16;
            if (this.Character.Dex < item.BaseData.possibleDex)
                return -19;
            if (this.Character.Agi < item.BaseData.possibleAgi)
                return -20;
            if (this.Character.Vit < item.BaseData.possibleVit)
                return -18;
            if (this.Character.Int < item.BaseData.possibleInt)
                return -21;
            if (this.Character.Mag < item.BaseData.possibleMag)
                return -17;
            if (!item.BaseData.possibleRace[this.Character.Race])
                return -13;
            if (!item.BaseData.possibleGender[this.Character.Gender])
                return -14;
            byte lv = this.Character.Level;
            if (this.Character.Rebirth)
            {
                if (lv < (item.BaseData.possibleLv - 10))
                    return -15;
            }
            else if (lv < item.BaseData.possibleLv)
                return -15;
            if ((item.BaseData.itemType == ItemType.RIDE_PET || item.BaseData.itemType == ItemType.RIDE_PARTNER) && this.Character.Marionette != null)
                return -2;
            if (item.EquipSlot.Contains(EnumEquipSlot.RIGHT_HAND) || item.EquipSlot.Contains(EnumEquipSlot.LEFT_HAND))
            {
                if (Character.Skills3.ContainsKey(990))
                {
                    return 0;
                }
            }
            else
            {
                if (Character.Skills3.ContainsKey(991))
                {
                    return 0;
                }
            }
            if (!item.IsParts && this.chara.Race != PC_RACE.DEM)
            {
                if (this.Character.JobJoint == PC_JOB.NONE)
                {


                    if (this.Character.DualJobID != 0)
                    {
                        var dualjobinfo = DualJobInfoFactory.Instance.items[this.Character.DualJobID];
                        if (!item.BaseData.possibleJob[this.Character.Job])
                            if (!item.BaseData.possibleJob[(PC_JOB)dualjobinfo.BaseJobID])
                                if (!item.BaseData.possibleJob[(PC_JOB)dualjobinfo.ExperJobID])
                                    if (!item.BaseData.possibleJob[(PC_JOB)dualjobinfo.TechnicalJobID])
                                        if (!item.BaseData.possibleJob[(PC_JOB)dualjobinfo.ChronicleJobID])
                                            return -2;
                    }
                    else
                    {
                        if (!item.BaseData.possibleJob[this.Character.Job])
                            return -2;
                    }
                }
                else
                {
                    if (!item.BaseData.possibleJob[this.Character.JobJoint])
                        if (this.Character.DualJobID != 0)
                        {
                            var dualjobinfo = DualJobInfoFactory.Instance.items[this.Character.DualJobID];
                            if (!item.BaseData.possibleJob[(PC_JOB)dualjobinfo.BaseJobID])
                                if (!item.BaseData.possibleJob[(PC_JOB)dualjobinfo.ExperJobID])
                                    if (!item.BaseData.possibleJob[(PC_JOB)dualjobinfo.TechnicalJobID])
                                        if (!item.BaseData.possibleJob[(PC_JOB)dualjobinfo.ChronicleJobID])
                                            return -2;
                        }
                }
            }
            if (this.Character.Race == PC_RACE.DEM && this.Character.Form == DEM_FORM.MACHINA_FORM)//DEM的机械形态不能装备
                return -29;
            if (item.BaseData.possibleRebirth)
                if (!this.Character.Rebirth || this.Character.Job != this.Character.Job3)
                    return -31;
            return 0;
        }
        public void OnItemEquiptRepair(Packets.Client.CSMG_ITEM_EQUIPT_REPAIR p)
        {
            Item item = this.Character.Inventory.GetItem(p.InventoryID);
            if (CountItem(item.BaseData.repairItem) > 0)
            {
                if (item.maxDurability > item.Durability)
                {
                    item.Durability = (ushort)(item.maxDurability - 1);
                    item.maxDurability--;
                    EffectArg arg = new EffectArg();
                    arg.actorID = this.Character.ActorID;
                    arg.effectID = 8043;
                    this.Character.e.OnShowEffect(this.Character, arg);
                    this.SendItemInfo(item);
                    DeleteItemID(item.BaseData.repairItem, 1, true);
                }
            }
        }
        /// <summary>
        /// 装备卸下过程，卸下该格子里的装备对应的所有格子里的道具，并移除道具附加的技能
        /// </summary>
        public void OnItemUnequipt(EnumEquipSlot eqslot)
        {
            if (!this.Character.Inventory.Equipments.ContainsKey(eqslot))
                return;
            Item oriItem = this.Character.Inventory.Equipments[eqslot];
            CleanItemSkills(oriItem);
            foreach (EnumEquipSlot i in oriItem.EquipSlot)
            {
                if (this.Character.Inventory.Equipments.ContainsKey(i))
                {
                    if (this.Character.Inventory.Equipments[i].Stack == 0)
                    {
                        this.Character.Inventory.Equipments.Remove(i);
                    }
                    else
                    {
                        ItemMoveSub(this.Character.Inventory.Equipments[i], ContainerType.BODY, this.Character.Inventory.Equipments[i].Stack);
                    }
                }
            }
        }

        //围观梦美卖萌0.0
        //从头写！
        //重写&简化逻辑结构
        public void OnItemEquipt(Packets.Client.CSMG_ITEM_EQUIPT p)
        {
            OnItemEquipt(p.InventoryID, p.EquipSlot);
        }
        public void OnItemEquipt(uint InventoryID, byte EquipSlot)
        {
            //特殊状态解除
            if (this.Character.Status.Additions.ContainsKey("Meditatioon"))
            {
                this.Character.Status.Additions["Meditatioon"].AdditionEnd();
                this.Character.Status.Additions.Remove("Meditatioon");
            }
            if (this.Character.Status.Additions.ContainsKey("Hiding"))
            {
                this.Character.Status.Additions["Hiding"].AdditionEnd();
            }
            if (this.Character.Status.Additions.ContainsKey("Cloaking"))
            {
                this.Character.Status.Additions["Cloaking"].AdditionEnd();
            }
            if (this.Character.Status.Additions.ContainsKey("fish"))
            {
                this.Character.Status.Additions["fish"].AdditionEnd();
                this.Character.Status.Additions.Remove("fish");
            }
            if (this.Character.Status.Additions.ContainsKey("IAmTree"))
            {
                this.Character.Status.Additions["IAmTree"].AdditionEnd();
                this.Character.Status.Additions.Remove("IAmTree");
            }
            if (this.Character.Status.Additions.ContainsKey("Invisible"))
            {
                this.Character.Status.Additions["Invisible"].AdditionEnd();
                this.Character.Status.Additions.Remove("Invisible");
            }
            if (this.Character.Tasks.ContainsKey("Regeneration"))
            {
                this.Character.Tasks["Regeneration"].Deactivate();
                this.Character.Tasks.Remove("Regeneration");
            }
            if (this.Character.Tasks.ContainsKey("Scorponok"))
            {
                this.Character.Tasks["Scorponok"].Deactivate();
                this.Character.Tasks.Remove("Scorponok");
            }
            if (this.Character.Tasks.ContainsKey("自由射击"))
            {
                this.Character.Tasks["自由射击"].Deactivate();
                this.Character.Tasks.Remove("自由射击");
            }
            if (this.Character.Tasks.ContainsKey("Possession"))
            {
                this.Character.Tasks["Possession"].Deactivate();
                this.Character.Tasks.Remove("Possession");
                if (this.chara.Buff.GetReadyPossession)
                {
                    this.chara.Buff.GetReadyPossession = false;
                    this.Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, this.Character, true);

                }
            }
            //???????
            //if ((Character.Speed < 750 || Character.Speed > 1000) && Map.OriID != 70000000 && Map.OriID != 75000000 && Character.Account.GMLevel < 20)
            //{
            //    SkillHandler.SendSystemMessage(Character, "由于移动速度低于或大于正常速度，处于无法更换装备的状态。");
            //    return;
            //}
            Item item = this.Character.Inventory.GetItem(InventoryID);
            //item是这次装备的装备
            //if (item.BaseData.itemType == ItemType.BOW || item.BaseData.itemType == ItemType.RIFLE
            //    || item.BaseData.itemType == ItemType.GUN || item.BaseData.itemType == ItemType.DUALGUN)
            //    if (Character.Job != PC_JOB.HAWKEYE)
            //        return;
            //if (Character.Account.GMLevel > 200)
            //FromActorPC(Character).SendSystemMessage("道具ID：" + item.ItemID.ToString());
            //if (this.Character.Account.GMLevel <= 255)
            //{
            //    /*if(item.BaseData.itemType == ItemType.PARTNER)
            //    {
            //        if (Character.TranceID != 0)
            //            Character.TranceID = 0;
            //        else
            //            Character.TranceID = item.BaseData.petID;
            //        this.SendCharInfoUpdate();
            //        return;
            //    }*/
            //    //if ((//item.BaseData.itemType == ItemType.RIDE_PARTNER || //item.BaseData.itemType == ItemType.RIDE_PARTNER ||
            //    //        item.BaseData.itemType == ItemType.RIDE_PET || //item.BaseData.itemType == ItemType.RIDE_PET ||
            //    //        item.BaseData.itemType == ItemType.RIDE_PET_ROBOT
            //    //        ) && this.Character.Account.GMLevel <= 200 && Character.MapID != 91000999)
            //    //    return;
            //}
            ushort count = item.Stack;//count是实际移动数量，考虑弹药
            if (item == null)//不存在？卡住或者用外挂了？
            {
                return;
            }
            int result;//返回不能装备的类型
            result = CheckEquipRequirement(item);//检查装备条件

            if (Character.Account.GMLevel >= 200) result = 0;

            if (result < 0)//不能装备
            {
                Packets.Server.SSMG_ITEM_EQUIP p4 = new SagaMap.Packets.Server.SSMG_ITEM_EQUIP();
                p4.InventorySlot = 0xffffffff;
                p4.Target = ContainerType.NONE;
                p4.Result = result;
                p4.Range = this.Character.Range;
                this.netIO.SendPacket(p4);
                return;
            }
            uint oldPetHP = 0;//原宠物HP，这次不想改

            List<EnumEquipSlot> targetslots = new List<EnumEquipSlot>(); //EquipSlot involved in this item target slots
            foreach (EnumEquipSlot i in item.EquipSlot)
            {
                if (!targetslots.Contains(i))
                    targetslots.Add(i);
            }
            /* 双持等以后把封包的equipslot参数对应的位置都搞定了再说
            if (EquipSlot == 15 && item.EquipSlot.Count == 1 && item.EquipSlot.Contains(EnumEquipSlot.RIGHT_HAND) && (!item.NeedAmmo)) //只有非射击类的右手单手武器可以作为左持
            {
                if (this.chara.Inventory.Equipments.ContainsKey(EnumEquipSlot.RIGHT_HAND))
                {
                    if (!this.chara.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].BaseData.doubleHand && !this.chara.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].NeedAmmo)
                    {
                        //只有当右手有装备且是单手非射击类武器时才能激发左持
                        targetslots.Clear();
                        targetslots.Add(EnumEquipSlot.LEFT_HAND);
                    }
                }
            }*/
            //卸下
            foreach (EnumEquipSlot i in targetslots)
            {
                //检查
                if (!this.Character.Inventory.Equipments.ContainsKey(i))
                {
                    //该格子原来就是空的 直接下一个格子 特殊检查在循环外写
                    continue;
                }
                foreach (EnumEquipSlot j in this.Character.Inventory.Equipments[i].EquipSlot) //检查对应位置的之前穿的装备是否可脱下
                {
                    Item oriItem = this.Character.Inventory.Equipments[j];
                    if (!CheckPossessionForEquipMove(oriItem))
                    {
                        //装备被PY状态中不能移动,不能填装弹药
                        return;
                    }
                    if (oriItem.NeedAmmo) //取下射击类装备前检查左手 如果左手有装备必然是弹药 需取下
                    {
                        if (this.Character.Inventory.Equipments.ContainsKey(EnumEquipSlot.LEFT_HAND))
                        {
                            Item ammo = this.Character.Inventory.Equipments[EnumEquipSlot.LEFT_HAND];
                            if (!CheckPossessionForEquipMove(ammo))
                            {
                                //装备被PY状态中不能移动
                                return;
                            }
                        }
                    }
                }
                if (i == EnumEquipSlot.UPPER_BODY)
                {
                    Character.PossessionPartnerSlotIDinClothes = 0;
                    Character.Status.hp_petpy = 0;

                }
                if (i == EnumEquipSlot.RIGHT_HAND)
                {
                    Character.PossessionPartnerSlotIDinRightHand = 0;
                    Character.Status.max_atk1_petpy = 0;
                    Character.Status.min_atk1_petpy = 0;
                    Character.Status.max_matk_petpy = 0;
                    Character.Status.min_matk_petpy = 0;
                }
                if (i == EnumEquipSlot.LEFT_HAND)
                {
                    Character.PossessionPartnerSlotIDinLeftHand = 0;
                    Character.Status.def_add_petpy = 0;
                    Character.Status.mdef_add_petpy = 0;
                }
                if (i == EnumEquipSlot.CHEST_ACCE)
                {
                    Character.PossessionPartnerSlotIDinAccesory = 0;
                    Character.Status.aspd_petpy = 0;
                    Character.Status.cspd_petpy = 0;
                }

                //卸下
                if (item.IsAmmo) //填装弹药，检查原左手道具是否是同种(之前检查过故左手必然是弹药)，若是，则不需取下，后面直接填装补充，否则直接卸下
                {
                    if (this.Character.Inventory.Equipments[EnumEquipSlot.LEFT_HAND].ItemID != item.ItemID)
                    {
                        //不是同种弹药 卸下
                        CleanItemSkills(this.Character.Inventory.Equipments[EnumEquipSlot.LEFT_HAND]);
                        ItemMoveSub(this.Character.Inventory.Equipments[EnumEquipSlot.LEFT_HAND], ContainerType.BODY, this.Character.Inventory.Equipments[EnumEquipSlot.LEFT_HAND].Stack);
                    }
                    else //999检查
                    {
                        if (this.Character.Inventory.Equipments[EnumEquipSlot.LEFT_HAND].Stack + count > 999)
                        {
                            count = (ushort)(999 - this.Character.Inventory.Equipments[EnumEquipSlot.LEFT_HAND].Stack);
                        }
                    }
                }
                else if (item.BaseData.itemType == ItemType.CARD || item.BaseData.itemType == ItemType.THROW) //填装投掷武器，检查原右手道具是否是同种，若是，则不需取下，后面直接填装补充，否则直接卸下
                {
                    //若是双手的投掷类？？？ 以后再说。。。
                    if (this.Character.Inventory.Equipments.ContainsKey(EnumEquipSlot.RIGHT_HAND))
                    {
                        if (this.Character.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].ItemID != item.ItemID)
                        {
                            OnItemUnequipt(EnumEquipSlot.RIGHT_HAND);
                        }
                        else //999检查
                        {
                            if (this.Character.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].Stack + count > 999)
                            {
                                count = (ushort)(999 - this.Character.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].Stack);
                            }
                        }
                    }
                }
                else if (item.NeedAmmo) //将要装备射击类武器，需额外检查左手，左手只能装备对应的弹药种类，否则都卸下左手装备
                {
                    //弓装备前判定左手
                    if (item.BaseData.itemType == ItemType.BOW)
                    {
                        if (this.Character.Inventory.Equipments.ContainsKey(EnumEquipSlot.LEFT_HAND))
                        {
                            if (this.Character.Inventory.Equipments[EnumEquipSlot.LEFT_HAND].BaseData.itemType != ItemType.ARROW)
                            {
                                OnItemUnequipt(EnumEquipSlot.LEFT_HAND);
                            }
                        }
                    }
                    //枪类装备前判定左手
                    if (item.BaseData.itemType == ItemType.GUN || item.BaseData.itemType == ItemType.DUALGUN || item.BaseData.itemType == ItemType.RIFLE)
                    {
                        if (this.Character.Inventory.Equipments.ContainsKey(EnumEquipSlot.LEFT_HAND))
                        {
                            if (this.Character.Inventory.Equipments[EnumEquipSlot.LEFT_HAND].BaseData.itemType != ItemType.BULLET)
                            {
                                OnItemUnequipt(EnumEquipSlot.LEFT_HAND);
                            }
                        }
                    }
                    //卸下原来的右手道具
                    OnItemUnequipt(EnumEquipSlot.RIGHT_HAND);
                }
                else //将要装备的装备既不是射击武器也不是弹药也不是投掷武器
                {
                    foreach (EnumEquipSlot j in this.Character.Inventory.Equipments[i].EquipSlot)
                    {
                        Item oriItem = this.Character.Inventory.Equipments[j];
                        if ((j == EnumEquipSlot.RIGHT_HAND) || (j == EnumEquipSlot.LEFT_HAND))//手部装备需要卸下，需特别检查射击类装备相关
                        {
                            //包里东西出来
                            if (oriItem.BaseData.itemType == ItemType.HANDBAG)
                            {
                                while (this.Character.Inventory.GetContainer(ContainerType.RIGHT_BAG).Count > 0)
                                {
                                    Item content = this.Character.Inventory.GetContainer(ContainerType.RIGHT_BAG).First();
                                    ItemMoveSub(content, ContainerType.BODY, content.Stack);
                                }
                            }
                            if (oriItem.BaseData.itemType == ItemType.LEFT_HANDBAG)
                            {
                                while (this.Character.Inventory.GetContainer(ContainerType.LEFT_BAG).Count > 0)
                                {
                                    Item content = this.Character.Inventory.GetContainer(ContainerType.LEFT_BAG).First();
                                    ItemMoveSub(content, ContainerType.BODY, content.Stack);
                                }
                            }
                            //射击类相关右手判定：原来装备射击武器且将要装备的新武器（含右手）与原来的类别不同时，需卸下左手的弹药
                            if (oriItem.NeedAmmo && item.BaseData.itemType != oriItem.BaseData.itemType)
                            {
                                //取下弹药
                                if (this.Character.Inventory.Equipments.ContainsKey(EnumEquipSlot.LEFT_HAND))
                                {
                                    CleanItemSkills(this.Character.Inventory.Equipments[EnumEquipSlot.LEFT_HAND]);
                                    ItemMoveSub(this.Character.Inventory.Equipments[EnumEquipSlot.LEFT_HAND], ContainerType.BODY, this.Character.Inventory.Equipments[EnumEquipSlot.LEFT_HAND].Stack);
                                }
                            }
                            //射击类相关左手判定：原来装备射击武器且将要装备的新道具（含左手）不是对应的弹药，需卸下右手的射击武器
                            if (this.Character.Inventory.Equipments.ContainsKey(EnumEquipSlot.RIGHT_HAND) && item.EquipSlot.Contains(EnumEquipSlot.LEFT_HAND))
                            {
                                switch (this.Character.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].BaseData.itemType)
                                {
                                    case ItemType.DUALGUN:
                                    case ItemType.GUN:
                                    case ItemType.RIFLE:
                                        if (item.BaseData.itemType != ItemType.BULLET)
                                        {
                                            //取下射击武器
                                            CleanItemSkills(this.Character.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND]);
                                            ItemMoveSub(this.Character.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND], ContainerType.BODY, this.Character.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].Stack);
                                        }
                                        break;
                                    case ItemType.BOW:
                                        if (item.BaseData.itemType != ItemType.ARROW)
                                        {
                                            //取下射击武器
                                            CleanItemSkills(this.Character.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND]);
                                            ItemMoveSub(this.Character.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND], ContainerType.BODY, this.Character.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].Stack);
                                        }
                                        break;
                                }
                            }
                        }
                        else//非手部装备需要卸下
                        {
                            //宠物类装备卸下过程
                            if (j == EnumEquipSlot.PET)
                            {
                                if (this.Character.Pet != null)
                                {
                                    if (this.Character.Pet.Ride)
                                    {
                                        //oldPetHP = this.Character.Pet.HP;
                                        //this.Character.HP = oldPetHP;
                                        //this.Character.Speed = Configuration.Instance.Speed;
                                        this.Character.Pet = null;
                                    }
                                    DeletePet();
                                }
                                if (Character.Partner != null) DeletePartner();
                            }
                            //检查副职业切换
                            if (oriItem.BaseData.jointJob != PC_JOB.NONE)
                            {
                                this.Character.JobJoint = PC_JOB.NONE;
                            }
                            //推进器技能
                            if (oriItem.BaseData.itemType == ItemType.BACK_DEMON)
                            {
                                SkillHandler.RemoveAddition(this.chara, "MoveUp2");
                                SkillHandler.RemoveAddition(this.chara, "MoveUp3");
                                SkillHandler.RemoveAddition(this.chara, "MoveUp4");
                                SkillHandler.RemoveAddition(this.chara, "MoveUp5");
                            }
                            //包里东西出来
                            if (oriItem.BaseData.itemType == ItemType.BACKPACK)
                            {
                                while (this.Character.Inventory.GetContainer(ContainerType.BACK_BAG).Count > 0)
                                {
                                    Item content = this.Character.Inventory.GetContainer(ContainerType.BACK_BAG).First();
                                    ItemMoveSub(content, ContainerType.BODY, content.Stack);
                                }
                            }
                        }
                        //j位置的装备正式卸下
                        if (this.Character.Inventory.Equipments.ContainsKey(j))//检查以防之前过程中已经卸下了
                        {
                            CleanItemSkills(oriItem);
                            ItemMoveSub(this.Character.Inventory.Equipments[j], ContainerType.BODY, this.Character.Inventory.Equipments[j].Stack);
                        }
                    }
                }
            }
            //道具对应格子本来就是空着时却需要检查别的格子的特殊卸下
            if (item.NeedAmmo) //将要装备射击类武器，需额外检查左手，左手只能装备对应的弹药种类，否则都卸下左手装备
            {
                //弓装备前判定右手
                if (item.BaseData.itemType == ItemType.BOW)
                {
                    if (this.Character.Inventory.Equipments.ContainsKey(EnumEquipSlot.RIGHT_HAND))
                    {
                        if (this.Character.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].BaseData.itemType != ItemType.ARROW)
                        {
                            CleanItemSkills(this.Character.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND]);
                            ItemMoveSub(this.Character.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND], ContainerType.BODY, this.Character.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].Stack);
                        }
                    }
                }
                //枪类装备前判定右手
                if (item.BaseData.itemType == ItemType.GUN || item.BaseData.itemType == ItemType.DUALGUN || item.BaseData.itemType == ItemType.RIFLE)
                {
                    if (this.Character.Inventory.Equipments.ContainsKey(EnumEquipSlot.RIGHT_HAND))
                    {
                        if (this.Character.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].BaseData.itemType != ItemType.BULLET)
                        {
                            CleanItemSkills(this.Character.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND]);
                            ItemMoveSub(this.Character.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND], ContainerType.BODY, this.Character.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].Stack);
                        }
                    }
                }
            }
            else if (targetslots.Contains(EnumEquipSlot.LEFT_HAND) && (!item.IsAmmo)) //包含左手的非弹药道具(弹药与武器的匹配最先就检查过了)需要额外检查右手是不是射击武器，是否对应
            {
                if (this.Character.Inventory.Equipments.ContainsKey(EnumEquipSlot.RIGHT_HAND))
                {
                    if (this.Character.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].NeedAmmo)
                    {
                        CleanItemSkills(this.Character.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND]);
                        CleanItemSkills(this.Character.Inventory.Equipments[EnumEquipSlot.LEFT_HAND]);
                        ItemMoveSub(this.Character.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND], ContainerType.BODY, this.Character.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].Stack);
                    }
                }
            }

            if (count == 0) return;
            if (this.Character.Inventory.MoveItem(this.Character.Inventory.GetContainerType(item.Slot), (int)item.Slot, (ContainerType)Enum.Parse(typeof(ContainerType), item.EquipSlot[0].ToString()), count))
            {
                if (item.Stack == 0)
                {
                    Packets.Server.SSMG_ITEM_EQUIP p4 = new SagaMap.Packets.Server.SSMG_ITEM_EQUIP();
                    p4.Target = (ContainerType)Enum.Parse(typeof(ContainerType), item.EquipSlot[0].ToString());
                    p4.InventorySlot = item.Slot;
                    PC.StatusFactory.Instance.CalcRange(this.Character);
                    p4.Range = this.Character.Range;
                    this.netIO.SendPacket(p4);
                }
                else
                {
                    Packets.Server.SSMG_ITEM_COUNT_UPDATE p5 = new SagaMap.Packets.Server.SSMG_ITEM_COUNT_UPDATE();
                    p5.InventorySlot = item.Slot;
                    p5.Stack = item.Stack;
                    this.netIO.SendPacket(p5);
                }
                if (item.BaseData.itemType == ItemType.ARROW || item.BaseData.itemType == ItemType.BULLET)
                {
                    if (item.Stack == 0)
                    {
                        this.Character.Inventory.Equipments[EnumEquipSlot.LEFT_HAND].Slot = item.Slot;
                    }
                    Packets.Server.SSMG_ITEM_COUNT_UPDATE p5 = new SagaMap.Packets.Server.SSMG_ITEM_COUNT_UPDATE();
                    p5.InventorySlot = this.Character.Inventory.Equipments[EnumEquipSlot.LEFT_HAND].Slot;
                    p5.Stack = this.Character.Inventory.Equipments[EnumEquipSlot.LEFT_HAND].Stack;
                    this.netIO.SendPacket(p5);
                }
                if (item.BaseData.itemType == ItemType.CARD || item.BaseData.itemType == ItemType.THROW)
                {
                    if (item.Stack == 0)
                    {
                        this.Character.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].Slot = item.Slot;
                    }
                    Packets.Server.SSMG_ITEM_COUNT_UPDATE p5 = new SagaMap.Packets.Server.SSMG_ITEM_COUNT_UPDATE();
                    p5.InventorySlot = this.Character.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].Slot;
                    p5.Stack = this.Character.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].Stack;
                    this.netIO.SendPacket(p5);
                }
            }
            //create dummy item to take the slots
            List<EnumEquipSlot> slots = item.EquipSlot;
            if (slots.Count > 1)
            {
                for (int i = 1; i < slots.Count; i++)
                {
                    Item dummy = item.Clone();
                    dummy.Stack = 0;
                    this.Character.Inventory.AddItem((ContainerType)Enum.Parse(typeof(ContainerType), slots[i].ToString()), dummy);
                }
            }
            //renew stauts
            if (item.EquipSlot.Contains(EnumEquipSlot.RIGHT_HAND))
            {
                SendAttackType();
                SkillHandler.Instance.CastPassiveSkills(this.Character, false);
            }
            if (item.EquipSlot.Contains(EnumEquipSlot.LEFT_HAND))
            {
                SkillHandler.Instance.CastPassiveSkills(this.Character, false);
            }
            SkillHandler.Instance.CheckBuffValid(this.Character);
            if (item.BaseData.itemType == ItemType.PET || item.BaseData.itemType == ItemType.PET_NEKOMATA)
            {
                this.SendPet(item);
            }
            if (item.BaseData.itemType == ItemType.PARTNER)
            {
                this.SendPartner(item);
                Character.Inventory.Equipments[EnumEquipSlot.PET].ActorPartnerID = item.ActorPartnerID;
                PC.StatusFactory.Instance.CalcStatus(Character);
                PartnerTalking(Character.Partner, TALK_EVENT.SUMMONED, 100, 0);
            }
            if (item.BaseData.itemType == ItemType.RIDE_PET || item.BaseData.itemType == ItemType.RIDE_PARTNER || item.BaseData.itemType == ItemType.RIDE_PET_ROBOT)
            {
                ActorPet pet = new ActorPet(item.BaseData.petID, item);
                pet.Owner = this.Character;
                Character.Pet = pet;
                #region MA"匠师"模块1
                if (Character is ActorPC)
                {
                    ActorPC pc = Character as ActorPC;
                    if (SkillHandler.Instance.CheckMobType(pet, "MACHINE_RIDE_ROBOT"))
                    {
                        if (!pc.Skills2_2.ContainsKey(132) && !pc.DualJobSkill.Exists(x => x.ID == 132))
                        {
                            return;
                        }
                    }

                }
                #endregion
                pet.Ride = true;

                if (!pet.Owner.CInt.ContainsKey("PC_HUNMAN_HP"))
                {
                    pet.Owner.CInt["PC_HUNMAN_HP"] = (int)Character.HP;
                }
                pet.MaxHP = 2000;
                #region MA"匠师"模块2
                float OnDir = 1.0f;
                if (Character is ActorPC)
                {
                    ActorPC pc = Character as ActorPC;

                    if (pc.Skills3.ContainsKey(987) || pc.DualJobSkill.Exists(x => x.ID == 987))
                    {
                        var duallv = 0;
                        if (pc.DualJobSkill.Exists(x => x.ID == 987))
                            duallv = pc.DualJobSkill.FirstOrDefault(x => x.ID == 987).Level;

                        var mainlv = 0;
                        if (pc.Skills3.ContainsKey(987))
                            mainlv = pc.Skills3[987].Level;

                        //OnDir = OnDir + (float)(((Math.Max(duallv, mainlv)) - 1) * 0.05f);
                        int maxlv = (Math.Max(duallv, mainlv));
                        if (SkillHandler.Instance.CheckMobType(pet, "MACHINE_RIDE_ROBOT"))
                        {
                            OnDir = OnDir + (float)(maxlv - 1) * 0.05f;
                            pet.MaxHP += (ushort)(Character.MaxHP * OnDir);
                            pet.MaxMP += (ushort)(Character.MaxMP * OnDir);
                            pet.MaxSP += (ushort)(Character.MaxSP * OnDir);
                            pet.Status.min_atk1 += (ushort)(Character.Status.min_atk1 * OnDir);
                            pet.Status.min_atk2 += (ushort)(Character.Status.min_atk2 * OnDir);
                            pet.Status.min_atk3 += (ushort)(Character.Status.min_atk3 * OnDir);
                            pet.Status.max_atk1 += (ushort)(Character.Status.max_atk1 * OnDir);
                            pet.Status.max_atk2 += (ushort)(Character.Status.max_atk2 * OnDir);
                            pet.Status.max_atk3 += (ushort)(Character.Status.max_atk3 * OnDir);
                            pet.Status.min_matk += (ushort)(Character.Status.min_matk * OnDir);
                            pet.Status.max_matk += (ushort)(Character.Status.max_matk * OnDir);
                            pet.Status.hit_melee += (ushort)(Character.Status.hit_melee * OnDir);
                            pet.Status.hit_ranged += (ushort)(Character.Status.hit_ranged * OnDir);
                            if ((pet.Status.def + Character.Status.def * OnDir) > 90)
                                pet.Status.def = 90;
                            else
                                pet.Status.def += (ushort)(Character.Status.def * OnDir);

                            pet.Status.def_add += (short)(Character.Status.def_add * OnDir);
                            if ((pet.Status.mdef + Character.Status.mdef * OnDir) > 90)
                                pet.Status.mdef = 90;
                            else
                                pet.Status.mdef += (ushort)(Character.Status.mdef * OnDir);

                            pet.Status.mdef_add += (short)(Character.Status.mdef_add * OnDir);
                            pet.Status.avoid_melee += (ushort)(Character.Status.avoid_melee * OnDir);
                            pet.Status.avoid_ranged += (ushort)(Character.Status.avoid_ranged * OnDir);

                            if ((pet.Status.aspd + Character.Status.aspd * OnDir) > 800)
                                pet.Status.aspd = 800;
                            else
                                pet.Status.aspd += (short)(Character.Status.aspd * OnDir);
                            if ((pet.Status.cspd + Character.Status.cspd * OnDir) > 800)
                                pet.Status.cspd = 800;
                            else
                                pet.Status.cspd += (short)(Character.Status.cspd * OnDir);
                        }
                        else if (SkillHandler.Instance.CheckMobType(pet, "MACHINE_RIDE"))
                        {
                            OnDir = (float)(0.25 + (float)(maxlv * 0.01f));
                            pet.MaxHP += (ushort)(Character.MaxHP * OnDir);
                            pet.MaxMP += (ushort)(Character.MaxMP * OnDir);
                            pet.MaxSP += (ushort)(Character.MaxSP * OnDir);
                            pet.Status.min_atk1 += (ushort)(Character.Status.min_atk1 * OnDir);
                            pet.Status.min_atk2 += (ushort)(Character.Status.min_atk2 * OnDir);
                            pet.Status.min_atk3 += (ushort)(Character.Status.min_atk3 * OnDir);
                            pet.Status.max_atk1 += (ushort)(Character.Status.max_atk1 * OnDir);
                            pet.Status.max_atk2 += (ushort)(Character.Status.max_atk2 * OnDir);
                            pet.Status.max_atk3 += (ushort)(Character.Status.max_atk3 * OnDir);
                            pet.Status.min_matk += (ushort)(Character.Status.min_matk * OnDir);
                            pet.Status.max_matk += (ushort)(Character.Status.max_matk * OnDir);
                            pet.Status.hit_melee += (ushort)(Character.Status.hit_melee * OnDir);
                            pet.Status.hit_ranged += (ushort)(Character.Status.hit_ranged * OnDir);

                            if ((pet.Status.def + Character.Status.def * OnDir) > 90)
                                pet.Status.def = 90;
                            else
                                pet.Status.def += (ushort)(Character.Status.def * OnDir);

                            pet.Status.def_add += (short)(Character.Status.def_add * OnDir);

                            if ((pet.Status.mdef + Character.Status.mdef * OnDir) > 90)
                                pet.Status.mdef = 90;
                            else
                                pet.Status.mdef += (ushort)(Character.Status.mdef * OnDir);

                            pet.Status.mdef_add += (short)(Character.Status.mdef_add * OnDir);
                            pet.Status.avoid_melee += (ushort)(Character.Status.avoid_melee * OnDir);
                            pet.Status.avoid_ranged += (ushort)(Character.Status.avoid_ranged * OnDir);

                            if ((pet.Status.aspd + Character.Status.aspd * OnDir) > 800)
                                pet.Status.aspd = 800;
                            else
                                pet.Status.aspd += (short)(Character.Status.aspd * OnDir);

                            if ((pet.Status.cspd + Character.Status.cspd * OnDir) > 800)
                                pet.Status.cspd = 800;
                            else
                                pet.Status.cspd += (short)(Character.Status.cspd * OnDir);
                        }

                    }

                }
                #endregion
                /*if (oldPetHP == 0)
                    pet.HP = this.Character.HP;
                else
                    pet.HP = oldPetHP;
                pet.HP = 99;
                Character.HP = pet.MaxHP;*/
                //Character.Speed = 600;
                Character.Speed = Configuration.Instance.Speed;

                //SendSystemMessage("[提示]在骑宠时移动速度上升33%，受到的伤害提升100%，治愈量下降70%。");
                SendPetBasicInfo();
                SendPetDetailInfo();
            }
            if (item.BaseData.jointJob != PC_JOB.NONE)
            {
                this.Character.JobJoint = item.BaseData.jointJob;
            }
            //凭依，跟我没关系
            if (item.PossessionedActor != null)
            {
                PossessionArg arg = new PossessionArg();
                arg.fromID = item.PossessionedActor.ActorID;
                arg.toID = this.Character.ActorID;
                arg.result = (int)item.PossessionedActor.PossessionPosition;
                item.PossessionedActor.PossessionTarget = this.Character.ActorID;
                MapServer.charDB.SaveChar(item.PossessionedActor, false, false);
                MapServer.accountDB.WriteUser(item.PossessionedActor.Account);
                string pos = "";
                switch (item.PossessionedActor.PossessionPosition)
                {
                    case PossessionPosition.RIGHT_HAND:
                        pos = LocalManager.Instance.Strings.POSSESSION_RIGHT;
                        break;
                    case PossessionPosition.LEFT_HAND:
                        pos = LocalManager.Instance.Strings.POSSESSION_LEFT;
                        break;
                    case PossessionPosition.NECK:
                        pos = LocalManager.Instance.Strings.POSSESSION_NECK;
                        break;
                    case PossessionPosition.CHEST:
                        pos = LocalManager.Instance.Strings.POSSESSION_ARMOR;
                        break;
                }
                this.SendSystemMessage(string.Format(LocalManager.Instance.Strings.POSSESSION_DONE, pos));
                if (item.PossessionedActor.Online)
                {
                    MapClient.FromActorPC(item.PossessionedActor).SendSystemMessage(string.Format(LocalManager.Instance.Strings.POSSESSION_DONE, pos));
                }
                this.Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.POSSESSION, arg, this.Character, true);
            }
            AddItemSkills(item);
            //重新计算状态值
            PC.StatusFactory.Instance.CalcStatus(this.Character);
            this.SendPlayerInfo();
            //broadcast
            this.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHANGE_EQUIP, null, this.Character, true);
            List<Item> list = new List<Item>();
            foreach (Item i in this.chara.Inventory.Equipments.Values)
            {
                if (i.Stack == 0)
                    continue;
                if (CheckEquipRequirement(i) != 0)
                {
                    list.Add(i);
                }
            }
            foreach (Item i in list)
            {
                Packets.Client.CSMG_ITEM_MOVE p2 = new SagaMap.Packets.Client.CSMG_ITEM_MOVE();
                p2.data = new byte[9];
                p2.Count = 1;
                p2.InventoryID = i.Slot;
                p2.Target = ContainerType.BODY;
                OnItemMove(p2);
            }
        }

        public void OnItemMove(Packets.Client.CSMG_ITEM_MOVE p)
        {
            OnItemMove(p, false);
        }

        public void OnItemMove(Packets.Client.CSMG_ITEM_MOVE p, bool possessionRemove)
        {
            OnItemMove(p.InventoryID, p.Target, p.Count, possessionRemove);
        }
        public void OnItemMove(uint InventoryID, ContainerType Target, ushort Count, bool possessionRemove)
        {


            if (this.Character.Status.Additions.ContainsKey("Meditatioon"))
            {
                this.Character.Status.Additions["Meditatioon"].AdditionEnd();
                this.Character.Status.Additions.Remove("Meditatioon");
            }
            if (this.Character.Status.Additions.ContainsKey("Hiding"))
            {
                this.Character.Status.Additions["Hiding"].AdditionEnd();
                this.Character.Status.Additions.Remove("Hiding");
            }
            if (this.Character.Status.Additions.ContainsKey("Cloaking"))
            {
                this.Character.Status.Additions["Cloaking"].AdditionEnd();
                this.Character.Status.Additions.Remove("Cloaking");
            }
            if (this.Character.Status.Additions.ContainsKey("IAmTree"))
            {
                this.Character.Status.Additions["IAmTree"].AdditionEnd();
                this.Character.Status.Additions.Remove("IAmTree");
            }
            if (this.Character.Status.Additions.ContainsKey("Invisible"))
            {
                this.Character.Status.Additions["Invisible"].AdditionEnd();
                this.Character.Status.Additions.Remove("Invisible");
            }
            Item item = this.Character.Inventory.GetItem(InventoryID);
            if (Target >= ContainerType.HEAD) //移动目标错误
            {
                Packets.Server.SSMG_ITEM_CONTAINER_CHANGE p1 = new SagaMap.Packets.Server.SSMG_ITEM_CONTAINER_CHANGE();
                p1.InventorySlot = item.Slot;
                p1.Result = -3;
                p1.Target = (ContainerType)(-1);
                this.netIO.SendPacket(p1);
                return;
            }
            bool ifUnequip = this.Character.Inventory.IsContainerEquip(this.Character.Inventory.GetContainerType(item.Slot));
            //ifUnequip &= p.Count == item.Stack;
            if (ifUnequip) //如果是卸下装备而不是在不同容器中移动
            {
                //检查
                if (item.PossessionedActor != null && !possessionRemove)
                {
                    Packets.Server.SSMG_ITEM_CONTAINER_CHANGE p1 = new SagaMap.Packets.Server.SSMG_ITEM_CONTAINER_CHANGE();
                    p1.InventorySlot = item.Slot;
                    p1.Result = -4;
                    p1.Target = (ContainerType)(-1);
                    this.netIO.SendPacket(p1);
                    return;
                }
                if (this.chara.Race == PC_RACE.DEM && this.chara.Form == DEM_FORM.MACHINA_FORM)
                {
                    Packets.Server.SSMG_ITEM_CONTAINER_CHANGE p1 = new SagaMap.Packets.Server.SSMG_ITEM_CONTAINER_CHANGE();
                    p1.InventorySlot = item.Slot;
                    p1.Result = -10;
                    p1.Target = (ContainerType)(-1);
                    this.netIO.SendPacket(p1);
                    return;
                }
                if (possessionRemove)
                    return;
                //卸下相关的额外格子
                List<EnumEquipSlot> slots = item.EquipSlot;
                if (slots.Count > 1)
                {
                    for (int i = 0; i < slots.Count; i++)
                    {
                        if (this.Character.Inventory.Equipments[slots[i]].Stack == 0)
                        {
                            this.Character.Inventory.Equipments.Remove(slots[i]);
                        }
                        else
                        {
                            ItemMoveSub(this.Character.Inventory.Equipments[slots[i]], ContainerType.BODY, this.Character.Inventory.Equipments[slots[i]].Stack);
                        }
                    }
                }
                else
                {
                    if (slots[0] == EnumEquipSlot.PET)
                    {
                        if (this.Character.Pet != null)
                            DeletePet();
                        if (this.Character.Partner != null)
                            DeletePartner();
                        PC.StatusFactory.Instance.CalcStatus(Character);
                    }
                    //箱包类装备移动时内容物进入body
                    if (item.BaseData.itemType == ItemType.BACKPACK)
                    {
                        while (this.Character.Inventory.GetContainer(ContainerType.BACK_BAG).Count > 0)
                        {
                            Item content = this.Character.Inventory.GetContainer(ContainerType.BACK_BAG).First();
                            ItemMoveSub(content, ContainerType.BODY, content.Stack);
                        }
                    }
                    if (item.BaseData.itemType == ItemType.HANDBAG)
                    {
                        while (this.Character.Inventory.GetContainer(ContainerType.RIGHT_BAG).Count > 0)
                        {
                            Item content = this.Character.Inventory.GetContainer(ContainerType.RIGHT_BAG).First();
                            ItemMoveSub(content, ContainerType.BODY, content.Stack);
                        }
                    }
                    if (item.BaseData.itemType == ItemType.LEFT_HANDBAG)
                    {
                        while (this.Character.Inventory.GetContainer(ContainerType.LEFT_BAG).Count > 0)
                        {
                            Item content = this.Character.Inventory.GetContainer(ContainerType.LEFT_BAG).First();
                            ItemMoveSub(content, ContainerType.BODY, content.Stack);
                        }
                    }
                    //卸下射击武器时自动卸下弹药
                    if (item.NeedAmmo)
                    {
                        if (this.Character.Inventory.Equipments.ContainsKey(EnumEquipSlot.LEFT_HAND))
                        {
                            ItemMoveSub(this.Character.Inventory.Equipments[EnumEquipSlot.LEFT_HAND], ContainerType.BODY, this.Character.Inventory.Equipments[EnumEquipSlot.LEFT_HAND].Stack);
                        }
                    }
                }
                //删除装备附带技能
                if (item.BaseData.jointJob != PC_JOB.NONE)
                {
                    this.Character.JobJoint = PC_JOB.NONE;
                }
                if (item.BaseData.itemType == ItemType.BACK_DEMON)
                {
                    SkillHandler.RemoveAddition(this.chara, "MoveUp2");
                    SkillHandler.RemoveAddition(this.chara, "MoveUp3");
                    SkillHandler.RemoveAddition(this.chara, "MoveUp4");
                    SkillHandler.RemoveAddition(this.chara, "MoveUp5");
                }
            }
            //无体积装备时不能放入物品
            if (Target == ContainerType.BACK_BAG)
            {
                if (!this.Character.Inventory.Equipments.ContainsKey(EnumEquipSlot.BACK))
                {
                    return;
                }
                else
                {
                    if (this.Character.Inventory.Equipments[EnumEquipSlot.BACK].BaseData.volumeUp == 0)
                    {
                        return;
                    }
                }
            }
            if (Target == ContainerType.RIGHT_BAG)
            {
                if (!this.Character.Inventory.Equipments.ContainsKey(EnumEquipSlot.RIGHT_HAND))
                {
                    return;
                }
                else
                {
                    if (this.Character.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].BaseData.volumeUp == 0)
                    {
                        return;
                    }
                }
            }
            if (Target == ContainerType.LEFT_BAG)
            {
                if (!this.Character.Inventory.Equipments.ContainsKey(EnumEquipSlot.LEFT_HAND))
                {
                    return;
                }
                else
                {
                    if (this.Character.Inventory.Equipments[EnumEquipSlot.LEFT_HAND].BaseData.volumeUp == 0)
                    {
                        return;
                    }
                }
            }
            /*双持以后再说
            //双持时若卸下右手则同时卸下左手
            if (item.EquipSlot.Contains(EnumEquipSlot.RIGHT_HAND)
                && item.EquipSlot.Count == 1
                && this.Character.Inventory.Equipments.ContainsKey(EnumEquipSlot.RIGHT_HAND)
                && this.Character.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND]==item
                && this.Character.Inventory.Equipments.ContainsKey(EnumEquipSlot.LEFT_HAND)
                && this.Character.Inventory.Equipments[EnumEquipSlot.LEFT_HAND].EquipSlot.Contains(EnumEquipSlot.RIGHT_HAND))
            {
                ItemMoveSub(this.Character.Inventory.Equipments[EnumEquipSlot.LEFT_HAND], ContainerType.BODY, 1);
            }*/
            //正式移动道具
            ItemMoveSub(item, Target, Count);
            //CleanItemSkills(item);
            //PC.StatusFactory.Instance.CalcStatus(this.Character);
            //SendPlayerInfo();
            //map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHANGE_EQUIP, null, this.Character, true);
        }

        /// <summary>
        /// 道具移动，只移动对应的真实格子的道具，不影响伪道具
        /// </summary>
        /// <param name="item"></param>
        /// <param name="container"></param>
        /// <param name="count"></param>
        public void ItemMoveSub(Item item, ContainerType container, ushort count)
        {
            Packets.Server.SSMG_ITEM_DELETE p2;
            Packets.Server.SSMG_ITEM_ADD p3;

            CleanItemSkills(item);
            if (item.BaseData.itemType == ItemType.RIDE_PET || item.BaseData.itemType == ItemType.RIDE_PARTNER)
            {
                PC.StatusFactory.Instance.CalcStatus(this.Character);
                if (!Character.CInt.ContainsKey("PC_HUNMAN_HP"))
                {
                    Character.CInt["PC_HUNMAN_HP"] = 99;//防止变量更改前就骑着骑宠的人上线后寻找不到PC_HUNMAN_HP值导致0HP,理论上不可能发生,以防万一
                }
                Character.HP = (uint)Character.CInt["PC_HUNMAN_HP"];
                Character.CInt.Remove("PC_HUNMAN_HP");

            }
            bool ifUnequip = this.Character.Inventory.IsContainerEquip(this.Character.Inventory.GetContainerType(item.Slot));
            uint slot = item.Slot;
            //Logger.ShowError(this.Character.Inventory.GetContainerType(item.Slot).ToString());
            if (this.Character.Inventory.MoveItem(this.Character.Inventory.GetContainerType(item.Slot), (int)item.Slot, container, count))
            {
                //Logger.ShowError(this.Character.Inventory.GetContainerType(item.Slot).ToString());
                if (item.Stack == 0)
                {
                    if (slot == this.Character.Inventory.LastItem.Slot)
                    {
                        if (!ifUnequip)
                        {
                            p2 = new SagaMap.Packets.Server.SSMG_ITEM_DELETE();
                            p2.InventorySlot = item.Slot;
                            this.netIO.SendPacket(p2);
                            p3 = new SagaMap.Packets.Server.SSMG_ITEM_ADD();
                            p3.Container = container;
                            p3.InventorySlot = item.Slot;
                            item.Stack = count;
                            p3.Item = item;
                            this.netIO.SendPacket(p3);
                            Packets.Server.SSMG_ITEM_CONTAINER_CHANGE p1 = new SagaMap.Packets.Server.SSMG_ITEM_CONTAINER_CHANGE();
                            p1.InventorySlot = item.Slot;
                            p1.Target = container;
                            this.netIO.SendPacket(p1);
                        }
                        else
                        {
                            Packets.Server.SSMG_ITEM_CONTAINER_CHANGE p1 = new SagaMap.Packets.Server.SSMG_ITEM_CONTAINER_CHANGE();
                            p1.InventorySlot = item.Slot;
                            p1.Target = container;
                            this.netIO.SendPacket(p1);
                            Packets.Server.SSMG_ITEM_EQUIP p4 = new SagaMap.Packets.Server.SSMG_ITEM_EQUIP();
                            p4.InventorySlot = 0xffffffff;
                            p4.Target = ContainerType.NONE;
                            p4.Result = 1;
                            PC.StatusFactory.Instance.CalcRange(this.Character);
                            if (item.EquipSlot.Contains(EnumEquipSlot.RIGHT_HAND))
                            {
                                SendAttackType();
                                SkillHandler.Instance.CastPassiveSkills(this.Character);
                            }
                            p4.Range = this.Character.Range;
                            this.netIO.SendPacket(p4);
                            this.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHANGE_EQUIP, null, this.Character, true);

                            if (item.EquipSlot[0] == EnumEquipSlot.PET)
                            {
                                if (this.Character.Pet != null)
                                {
                                    if (this.Character.Pet.Ride)
                                    {
                                        //this.Character.Speed = Configuration.Instance.Speed;
                                        //this.Character.HP = this.Character.Pet.HP;
                                        this.Character.Pet = null;
                                    }
                                }
                            }
                            PC.StatusFactory.Instance.CalcStatus(this.Character);

                            this.SendPlayerInfo();
                        }
                    }
                    else
                    {
                        p2 = new SagaMap.Packets.Server.SSMG_ITEM_DELETE();
                        p2.InventorySlot = slot;
                        this.netIO.SendPacket(p2);
                        if (slot != item.Slot)
                        {
                            item = this.Character.Inventory.GetItem(item.Slot);
                            Packets.Server.SSMG_ITEM_COUNT_UPDATE p5 = new SagaMap.Packets.Server.SSMG_ITEM_COUNT_UPDATE();
                            p5.InventorySlot = item.Slot;
                            p5.Stack = item.Stack;
                            this.netIO.SendPacket(p5);
                            item = this.Character.Inventory.LastItem;
                            p3 = new SagaMap.Packets.Server.SSMG_ITEM_ADD();
                            p3.Container = container;
                            p3.InventorySlot = item.Slot;
                            p3.Item = item;
                            this.netIO.SendPacket(p3);
                        }
                        else
                        {
                            item = this.Character.Inventory.LastItem;
                            Packets.Server.SSMG_ITEM_COUNT_UPDATE p4 = new SagaMap.Packets.Server.SSMG_ITEM_COUNT_UPDATE();
                            p4.InventorySlot = item.Slot;
                            p4.Stack = item.Stack;
                            this.netIO.SendPacket(p4);
                        }
                    }
                }
                else
                {
                    Packets.Server.SSMG_ITEM_COUNT_UPDATE p1 = new SagaMap.Packets.Server.SSMG_ITEM_COUNT_UPDATE();
                    p1.InventorySlot = item.Slot;
                    p1.Stack = item.Stack;
                    this.netIO.SendPacket(p1);
                    if (this.Character.Inventory.LastItem.Stack == count)
                    {
                        p3 = new SagaMap.Packets.Server.SSMG_ITEM_ADD();
                        p3.Container = container;
                        p3.InventorySlot = this.Character.Inventory.LastItem.Slot;
                        p3.Item = this.Character.Inventory.LastItem;
                        this.netIO.SendPacket(p3);
                    }
                    else
                    {
                        item = this.Character.Inventory.LastItem;
                        Packets.Server.SSMG_ITEM_COUNT_UPDATE p4 = new SagaMap.Packets.Server.SSMG_ITEM_COUNT_UPDATE();
                        p4.InventorySlot = item.Slot;
                        p4.Stack = item.Stack;
                        this.netIO.SendPacket(p4);
                    }
                }
            }
            this.Character.Inventory.Items[ContainerType.BODY].RemoveAll(x => x.Stack == 0);
            this.Character.Inventory.CalcPayloadVolume();
            this.SendCapacity();
        }

        public bool CheckPossessionForEquipMove(Item item)
        {
            if (item.PossessionedActor != null)
            {
                Packets.Server.SSMG_ITEM_EQUIP p4 = new SagaMap.Packets.Server.SSMG_ITEM_EQUIP();
                p4.InventorySlot = 0xffffffff;
                p4.Target = ContainerType.NONE;
                p4.Result = -10;
                p4.Range = this.Character.Range;
                this.netIO.SendPacket(p4);
                return false;
            }
            else
            {
                return true;
            }
        }

        public void AddItemSkills(Item item)
        {
            if (item.BaseData.itemType == ItemType.PARTNER)
            {
                ActorPartner partner = MapServer.charDB.GetActorPartner(item.ActorPartnerID, item);
                if (partner.rebirth)
                {
                    SagaDB.Skill.Skill skill = SkillFactory.Instance.GetSkill(2443, 1);
                    if (skill != null)
                        if (!Character.Skills.ContainsKey(2443))
                            Character.Skills.Add(2443, skill);
                }
            }

            if (item.BaseData.possibleSkill != 0)//装备附带主动技能
            {
                SagaDB.Skill.Skill skill = SkillFactory.Instance.GetSkill(item.BaseData.possibleSkill, 1);
                if (skill != null)
                {
                    if (!this.Character.Skills.ContainsKey(item.BaseData.possibleSkill))
                    {
                        this.Character.Skills.Add(item.BaseData.possibleSkill, skill);
                    }
                }
            }

            if (item.BaseData.passiveSkill != 0)//装备附带被动技能
            {
                ushort skillID = item.BaseData.passiveSkill;
                byte lv = 0;
                foreach (var eq in Character.Inventory.Equipments)
                {
                    if (eq.Value.BaseData.passiveSkill == skillID && eq.Value.EquipSlot[0] == eq.Key)
                        lv++;
                }
                if (lv > 5) lv = 5;
                SagaDB.Skill.Skill skill = SkillFactory.Instance.GetSkill(item.BaseData.passiveSkill, lv);
                if (skill != null)
                {
                    if (Character.Skills.ContainsKey(skillID))
                        Character.Skills.Remove(skillID);
                    if (lv > 0)
                    {
                        Character.Skills.Add(skillID, skill);
                        if (!skill.BaseData.active)
                        {
                            SkillArg arg = new SkillArg();
                            arg.skill = skill;
                            SkillHandler.Instance.SkillCast(this.Character, this.Character, arg);
                        }
                    }
                }
                SkillHandler.Instance.CastPassiveSkills(Character);
            }
        }

        public void CleanItemSkills(Item item)
        {
            if (item.BaseData.itemType == ItemType.PARTNER)
            {
                ActorPartner partner = MapServer.charDB.GetActorPartner(item.ActorPartnerID, item);
                if (partner != null)
                {
                    if (partner.rebirth)
                    {
                        SagaDB.Skill.Skill skill = SkillFactory.Instance.GetSkill(2443, 1);
                        if (skill != null)
                            if (Character.Skills.ContainsKey(2443))
                                Character.Skills.Remove(2443);
                    }
                }
            }
            if (item.BaseData.possibleSkill != 0)//装备附带主动技能
            {
                SagaDB.Skill.Skill skill = SkillFactory.Instance.GetSkill(item.BaseData.possibleSkill, 1);
                if (skill != null)
                {
                    if (this.Character.Skills.ContainsKey(item.BaseData.possibleSkill))
                    {
                        this.Character.Skills.Remove(item.BaseData.possibleSkill);
                    }
                }
            }

            if (item.BaseData.passiveSkill != 0)//装备附带被动技能
            {
                ushort skillID = item.BaseData.passiveSkill;
                //byte lv = 0;
                //foreach (var eq in Character.Inventory.Equipments)
                //    if (eq.Value.BaseData.passiveSkill == skillID && eq.Value.EquipSlot[0] == eq.Key)
                //        lv++;
                //if (lv > 5) lv = 5;
                SagaDB.Skill.Skill skill = SkillFactory.Instance.GetSkill(item.BaseData.passiveSkill, 1);
                if (skill != null)
                {
                    if (Character.Skills.ContainsKey(skillID))
                        Character.Skills.Remove(skillID);
                    //if (lv > 0)
                    //{
                    //    Character.Skills.Add(skillID, skill);
                    //    if (!skill.BaseData.active)
                    //    {
                    //        SkillArg arg = new SkillArg();
                    //        arg.skill = skill;
                    //        SkillHandler.Instance.SkillCast(this.Character, this.Character, arg);
                    //    }
                    //}
                }
                SkillHandler.Instance.CastPassiveSkills(Character);
            }
        }

        public void SendItemAdd(Item item, ContainerType container, InventoryAddResult result, int count, bool sendMessage)
        {
            switch (result)
            {
                case InventoryAddResult.NEW_INDEX:
                    Packets.Server.SSMG_ITEM_ADD p = new SagaMap.Packets.Server.SSMG_ITEM_ADD();
                    p.Container = container;
                    p.Item = item;
                    p.InventorySlot = item.Slot;
                    this.netIO.SendPacket(p);
                    break;
                case InventoryAddResult.STACKED:
                    {
                        Packets.Server.SSMG_ITEM_COUNT_UPDATE p1 = new SagaMap.Packets.Server.SSMG_ITEM_COUNT_UPDATE();
                        p1.InventorySlot = item.Slot;
                        p1.Stack = item.Stack;
                        this.netIO.SendPacket(p1);
                    }
                    break;
                case InventoryAddResult.MIXED:
                    {
                        Packets.Server.SSMG_ITEM_COUNT_UPDATE p1 = new SagaMap.Packets.Server.SSMG_ITEM_COUNT_UPDATE();
                        p1.InventorySlot = item.Slot;
                        p1.Stack = item.Stack;
                        this.netIO.SendPacket(p1);
                        Packets.Server.SSMG_ITEM_ADD p2 = new SagaMap.Packets.Server.SSMG_ITEM_ADD();
                        p2.Container = container;
                        p2.Item = this.Character.Inventory.LastItem;
                        p2.InventorySlot = this.Character.Inventory.LastItem.Slot;
                        this.netIO.SendPacket(p2);
                    }
                    break;
                case InventoryAddResult.GOWARE:
                    SendSystemMessage("背包已满，物品『" + item.BaseData.name + "』存入了仓库");
                    break;
            }

            this.Character.Inventory.CalcPayloadVolume();
            this.SendCapacity();

            if (sendMessage)
            {
                if (item.Identified)
                    this.SendSystemMessage(string.Format(LocalManager.Instance.Strings.ITEM_ADDED, item.BaseData.name, count));
                else
                    this.SendSystemMessage(string.Format(LocalManager.Instance.Strings.ITEM_ADDED, Scripting.Event.GetItemNameByType(item.BaseData.itemType), count));
            }
        }

        public void SendItems()
        {
            string[] names = Enum.GetNames(typeof(ContainerType));
            foreach (string i in names)
            {
                ContainerType container = (ContainerType)Enum.Parse(typeof(ContainerType), i);
                List<Item> items = this.Character.Inventory.GetContainer(container);
                List<Item> trashItem = new List<Item>();
                if (container == ContainerType.BODY)//扫描并删除身上的垃圾数据
                {
                    foreach (Item j in items)
                    {
                        if (j.Stack == 0)
                            trashItem.Add(j);
                    }
                    if (trashItem.Count > 0)
                    {
                        for (int y = 0; y < trashItem.Count; y++)
                        {
                            Character.Inventory.Items[ContainerType.BODY].Remove(trashItem[y]);
                        }
                    }
                }
                foreach (Item j in items)
                {
                    if (j.Stack == 0)
                        continue;
                    //if (j.Refine == 0)
                    //    j.Clear();
                    Packets.Server.SSMG_ITEM_INFO p = new SagaMap.Packets.Server.SSMG_ITEM_INFO();
                    p.Item = j;
                    p.InventorySlot = j.Slot;
                    p.Container = container;
                    this.netIO.SendPacket(p);
                }
            }
        }

        public void SendItemInfo(uint slot)
        {
            Item item = this.Character.Inventory.GetItem(slot);
            if (item == null)
                return;
            Packets.Server.SSMG_ITEM_INFO p = new SagaMap.Packets.Server.SSMG_ITEM_INFO();
            p.Item = item;
            p.InventorySlot = item.Slot;
            p.Container = this.Character.Inventory.GetContainerType(slot);
            this.netIO.SendPacket(p);
        }

        public void SendItemInfo(Item item)
        {
            if (item == null)
                return;

            Packets.Server.SSMG_ITEM_INFO p = new SagaMap.Packets.Server.SSMG_ITEM_INFO();
            p.Item = item;
            p.InventorySlot = item.Slot;
            p.Container = this.Character.Inventory.GetContainerType(item.Slot);
            this.netIO.SendPacket(p);

            Packet packet = new Packet();
            packet.data = new byte[3];
            packet.ID = 0x0203;
            packet.offset = 2;
            packet.PutByte(02);
            this.netIO.SendPacket(packet);
        }

        public void SendItemIdentify(uint slot)
        {
            Item item = this.Character.Inventory.GetItem(slot);
            if (item == null)
                return;
            Packets.Server.SSMG_ITEM_IDENTIFY p = new SagaMap.Packets.Server.SSMG_ITEM_IDENTIFY();
            p.InventorySlot = item.Slot;
            p.Identify = item.Identified;
            p.Lock = item.Locked;
            this.netIO.SendPacket(p);
        }

        public void SendEquip()
        {
            Packets.Server.SSMG_ITEM_ACTOR_EQUIP_UPDATE p = new SagaMap.Packets.Server.SSMG_ITEM_ACTOR_EQUIP_UPDATE();
            p.Player = this.Character;
            this.netIO.SendPacket(p);
        }

        public void AddItem(Item item, bool sendMessage)
        {
            AddItem(item, sendMessage, true);
        }

        public void CleanItem()
        {
            Character.Inventory.Items[ContainerType.BODY].Clear();
            this.Character.Inventory.CalcPayloadVolume();
            this.SendCapacity();
        }

        public void AddItem(Item item, bool sendMessage, bool fullgoware)
        {
            ushort stack = item.Stack;
            //SagaLib.Logger.ShowWarning("1"+item.Stack.ToString()+item.BaseData.name);
            //if (this.Character.Inventory.Items.Count < 1000 || this.Character.Account.GMLevel > 10)
            //{
            //临时解决方案↓↓↓↓↓
            //if (this.Character.Inventory.Items[ContainerType.BODY].Count + this.Character.Inventory.Equipments.Count > 100 && fullgoware)
            //{
            //    string[] names = Enum.GetNames(typeof(ContainerType));
            //    foreach (string i in names)
            //    {
            //        ContainerType container = (ContainerType)Enum.Parse(typeof(ContainerType), i);
            //        List<Item> items = this.Character.Inventory.GetContainer(container);
            //        List<Item> trashItem = new List<Item>();
            //        if (container == ContainerType.BODY)//扫描并删除身上的垃圾数据
            //        {
            //            foreach (Item j in items)
            //            {
            //                if (j.Stack == 0)
            //                    trashItem.Add(j);
            //            }
            //            if (trashItem.Count > 0)
            //            {
            //                for (int y = 0; y < trashItem.Count; y++)
            //                {
            //                    Character.Inventory.Items[ContainerType.BODY].Remove(trashItem[y]);
            //                }
            //            }
            //        }
            //    }
            //}
            //临时解决方案↑↑↑↑↑
            if (this.Character.Inventory.Items[ContainerType.BODY].Count + this.Character.Inventory.Equipments.Count > 100 && fullgoware)
            {
                if (Character.CInt["背包满后仓库"] == 1)
                {
                    Character.Inventory.AddWareItem(WarehousePlace.Acropolis, item);
                    SendSystemMessage("背包已满，物品『" + item.BaseData.name + "』存入了第一页仓库。");
                }
                else if (Character.CInt["背包满后仓库"] == 2)
                {
                    Character.Inventory.AddWareItem(WarehousePlace.FarEast, item);
                    SendSystemMessage("背包已满，物品『" + item.BaseData.name + "』存入了第二页仓库。");
                }
                else if (Character.CInt["背包满后仓库"] == 3)
                {
                    Character.Inventory.AddWareItem(WarehousePlace.IronSouth, item);
                    SendSystemMessage("背包已满，物品『" + item.BaseData.name + "』存入了第三页仓库。");
                }
                else if (Character.CInt["背包满后仓库"] == 4)
                {
                    Character.Inventory.AddWareItem(WarehousePlace.Northan, item);
                    SendSystemMessage("背包已满，物品『" + item.BaseData.name + "』存入了第四页仓库。");
                }
                else
                {
                    Character.Inventory.AddWareItem(WarehousePlace.Northan, item);
                    SendSystemMessage("背包已满，物品『" + item.BaseData.name + "』存入了第四页仓库。");
                }
            }
            else
            {
                InventoryAddResult result = this.Character.Inventory.AddItem(ContainerType.BODY, item);
                this.SendItemAdd(item, ContainerType.BODY, result, stack, sendMessage);

                this.Character.Inventory.CalcPayloadVolume();
                this.SendCapacity();
                //this.SendItems();
            }
            /*}
            else
            {
                this.SendSystemMessage("道具栏已满，无法获得道具。");
                /*this.SendSystemMessage("（本次获得的道具可以向 吉田佳美 领取，临时道具只能保存3个，请及时处理道具栏并领取。）");
                if (this.Character.CInt["临时道具1"] == 0)
                    this.Character.CInt["临时道具1"] = (int)item.ItemID;
                else if (this.Character.CInt["临时道具2"] == 0)
                    this.Character.CInt["临时道具2"] = (int)item.ItemID;
                else if (this.Character.CInt["临时道具3"] == 0)
                    this.Character.CInt["临时道具3"] = (int)item.ItemID;*/

        }

        int CountItem(uint itemID)
        {
            SagaDB.Item.Item item = this.chara.Inventory.GetItem(itemID, Inventory.SearchType.ITEM_ID);
            if (item != null)
            {
                return item.Stack;
            }
            else
            {
                return 0;
            }
        }

        public Item DeleteItemID(uint itemID, ushort count, bool message)
        {
            Item item = this.Character.Inventory.GetItem(itemID, Inventory.SearchType.ITEM_ID);
            if (item == null) return null;
            uint slot = item.Slot;
            InventoryDeleteResult result = this.Character.Inventory.DeleteItem(item.Slot, count);
            if (item.IsEquipt)
            {
                SendEquip();
                PC.StatusFactory.Instance.CalcStatus(this.Character);
                SendPlayerInfo();
            }
            switch (result)
            {
                case InventoryDeleteResult.STACK_UPDATED:
                    Packets.Server.SSMG_ITEM_COUNT_UPDATE p1 = new SagaMap.Packets.Server.SSMG_ITEM_COUNT_UPDATE();
                    item = this.Character.Inventory.GetItem(slot);
                    p1.InventorySlot = slot;
                    p1.Stack = item.Stack;
                    this.netIO.SendPacket(p1);
                    break;
                case InventoryDeleteResult.ALL_DELETED:
                    Packets.Server.SSMG_ITEM_DELETE p2 = new SagaMap.Packets.Server.SSMG_ITEM_DELETE();
                    p2.InventorySlot = slot;
                    this.netIO.SendPacket(p2);
                    if (item.IsEquipt)
                    {
                        SendAttackType();
                        Packets.Server.SSMG_ITEM_EQUIP p4 = new SagaMap.Packets.Server.SSMG_ITEM_EQUIP();
                        p4.InventorySlot = 0xffffffff;
                        p4.Target = ContainerType.NONE;
                        p4.Result = 1;
                        p4.Range = this.Character.Range;
                        this.netIO.SendPacket(p4);
                    }
                    break;
            }
            this.Character.Inventory.CalcPayloadVolume();
            this.SendCapacity();
            if (message)
                this.SendSystemMessage(string.Format(LocalManager.Instance.Strings.ITEM_DELETED, item.BaseData.name, count));
            return item;
        }

        public void DeleteItem(uint slot, ushort count, bool message)
        {
            Item item = this.Character.Inventory.GetItem(slot);
            ContainerType container = this.Character.Inventory.GetContainerType(item.Slot);
            bool equiped = false;
            if (container >= ContainerType.HEAD && container <= ContainerType.PET)
                equiped = true;
            InventoryDeleteResult result = this.Character.Inventory.DeleteItem(slot, count);
            if (equiped)
            {
                SendEquip();
                PC.StatusFactory.Instance.CalcStatus(this.Character);
                SendPlayerInfo();
            }
            switch (result)
            {
                case InventoryDeleteResult.STACK_UPDATED:
                    Packets.Server.SSMG_ITEM_COUNT_UPDATE p1 = new SagaMap.Packets.Server.SSMG_ITEM_COUNT_UPDATE();
                    item = this.Character.Inventory.GetItem(slot);
                    p1.InventorySlot = slot;
                    p1.Stack = item.Stack;
                    this.netIO.SendPacket(p1);
                    break;
                case InventoryDeleteResult.ALL_DELETED:
                    Packets.Server.SSMG_ITEM_DELETE p2 = new SagaMap.Packets.Server.SSMG_ITEM_DELETE();
                    p2.InventorySlot = slot;
                    this.netIO.SendPacket(p2);
                    this.Character.Inventory.GetContainerType(slot);
                    if (equiped)
                    {
                        SendAttackType();
                        Packets.Server.SSMG_ITEM_EQUIP p4 = new SagaMap.Packets.Server.SSMG_ITEM_EQUIP();
                        p4.InventorySlot = 0xffffffff;
                        p4.Target = ContainerType.NONE;
                        p4.Result = 1;
                        p4.Range = this.Character.Range;
                        this.netIO.SendPacket(p4);
                        if (item.BaseData.itemType == ItemType.ARROW || item.BaseData.itemType == ItemType.BULLET)
                        {
                            Item dummy = this.Character.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].Clone();
                            dummy.Stack = 0;
                            this.Character.Inventory.AddItem(ContainerType.LEFT_HAND, dummy);
                        }
                    }
                    break;
            }
            this.Character.Inventory.CalcPayloadVolume();
            this.SendCapacity();
            if (message)
                this.SendSystemMessage(string.Format(LocalManager.Instance.Strings.ITEM_DELETED, item.BaseData.name, count));
        }

        public void SendPet(Item item)
        {
            if (item.BaseData.itemType != ItemType.BACK_DEMON && item.BaseData.itemType != ItemType.RIDE_PET && item.BaseData.itemType != ItemType.RIDE_PARTNER)
            {
                ActorPet pet = new ActorPet(item.BaseData.petID, item);
                this.Character.Pet = pet;
                //砍掉PET
                /*
                pet.MapID = this.Character.MapID;
                pet.X = this.Character.X;
                pet.Y = this.Character.Y;
                pet.Owner = this.Character;
                ActorEventHandlers.PetEventHandler eh = new ActorEventHandlers.PetEventHandler(pet);
                pet.e = eh;
                if (Mob.MobAIFactory.Instance.Items.ContainsKey(item.BaseData.petID))
                    eh.AI.Mode = Mob.MobAIFactory.Instance.Items[item.BaseData.petID];
                else
                    eh.AI.Mode = new SagaMap.Mob.AIMode(0);
                eh.AI.Start();
                //Mob.AIThread.Instance.RegisterAI(eh.AI);

                this.map.RegisterActor(pet);
                pet.invisble = false;
                this.map.OnActorVisibilityChange(pet);
                this.map.SendVisibleActorsToActor(pet);//*/
            }
        }

        public void DeletePet()
        {
            if (this.Character.Partner != null)
            {

                Manager.MapManager.Instance.GetMap(this.Character.Partner.MapID).DeleteActor(this.Character.Partner);
                this.Character.Partner = null;
                return;
            }


            if (this.Character.Pet != null)
            {
                return;
            }
            else
            {
                //AI被砍掉！
                //參考SendPet()

                ActorEventHandlers.PetEventHandler eh = (ActorEventHandlers.PetEventHandler)this.Character.Pet.e;
                eh.AI.Pause();
                eh.AI.Activated = false;
                Manager.MapManager.Instance.GetMap(this.Character.Pet.MapID).DeleteActor(this.Character.Pet);
                this.Character.Pet = null;
            }

            //Ride 沒有被定義，請考慮Default 宣告false！
            //if (this.Character.Pet.Ride)
            //return;
        }

        public void OnItemChangeSlot(Packets.Client.CSMG_ITEM_CHANGE_SLOT p)
        {

        }

    }
}
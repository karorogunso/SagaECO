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
using SagaDB.DEMIC;
using SagaLib;
using SagaMap;
using SagaMap.Manager;
using SagaMap.Skill;
using SagaMap.PC;
using SagaMap.Packets.Client;

namespace SagaMap.Network.Client
{
    public partial class MapClient
    {
        public bool demCLBuy = false;
        public bool demParts = false;

        public bool demic = false;
        public bool chipShop = false;
        uint currentChipCategory = 0;

        public void SendCL()
        {
            if (this.chara.Race == PC_RACE.DEM && this.state != SESSION_STATE.AUTHENTIFICATED)
            {
                Packets.Server.SSMG_DEM_COST_LIMIT_UPDATE p1 = new SagaMap.Packets.Server.SSMG_DEM_COST_LIMIT_UPDATE();
                p1.Result = (SagaMap.Packets.Server.SSMG_DEM_COST_LIMIT_UPDATE.Results)0;
                p1.CurrentEP = this.Character.EPUsed;
                p1.EPRequired = (short)(ExperienceManager.Instance.GetEPRequired(this.chara) - this.chara.EPUsed);
                p1.CL = this.chara.CL;
                this.netIO.SendPacket(p1);
            }
        }

        public void OnDEMCostLimitBuy(Packets.Client.CSMG_DEM_COST_LIMIT_BUY p)
        {
            if (demCLBuy)
            {
                short ep = p.EP;
                Packets.Server.SSMG_DEM_COST_LIMIT_UPDATE p1 = new SagaMap.Packets.Server.SSMG_DEM_COST_LIMIT_UPDATE();
                if (this.Character.EP >= ep)
                {
                    this.Character.EP = (uint)(this.Character.EP - ep);
                    ExperienceManager.Instance.ApplyEP(this.Character, ep);
                    PC.StatusFactory.Instance.CalcStatus(this.Character);
                    SendPlayerInfo();
                    p1.Result = SagaMap.Packets.Server.SSMG_DEM_COST_LIMIT_UPDATE.Results.OK;
                }
                else
                    p1.Result = SagaMap.Packets.Server.SSMG_DEM_COST_LIMIT_UPDATE.Results.NOT_ENOUGH_EP;
                p1.CurrentEP = this.Character.EPUsed;
                p1.EPRequired = (short)(ExperienceManager.Instance.GetEPRequired(this.chara) - this.chara.EPUsed);
                p1.CL = this.chara.CL;
                this.netIO.SendPacket(p1);
            }
        }

        public void OnDEMCostLimitClose(Packets.Client.CSMG_DEM_COST_LIMIT_CLOSE p)
        {
            demCLBuy = false;
        }

        public void OnDEMFormChange(Packets.Client.CSMG_DEM_FORM_CHANGE p)
        {
            if (this.Character.Form != p.Form)
            {
                this.Character.Form = p.Form;

                SkillHandler.Instance.CastPassiveSkills(this.chara);
                PC.StatusFactory.Instance.CalcStatus(this.chara);

                this.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHAR_INFO_UPDATE, null, this.chara, true);                
                SendPlayerInfo();
                SendAttackType();

                Packets.Server.SSMG_DEM_FORM_CHANGE p1 = new SagaMap.Packets.Server.SSMG_DEM_FORM_CHANGE();
                p1.Form = this.chara.Form;
                this.netIO.SendPacket(p1);
            }
        }

        public void OnDEMPartsUnequip(Packets.Client.CSMG_DEM_PARTS_UNEQUIP p)
        {
            if (this.chara.Race == PC_RACE.DEM && demParts)
            {
                Item item = this.Character.Inventory.GetItem(p.InventoryID);
                if (item == null)
                {
                    return;
                }
                bool ifUnequip = this.Character.Inventory.IsContainerParts(this.Character.Inventory.GetContainerType(item.Slot));
                if (ifUnequip)
                {
                    List<EnumEquipSlot> slots = item.EquipSlot;
                    if (slots.Count > 1)
                    {
                        for (int i = 1; i < slots.Count; i++)
                        {
                            this.Character.Inventory.Parts.Remove(slots[i]);
                        }
                    }

                    Packets.Server.SSMG_ITEM_DELETE p2;
                    Packets.Server.SSMG_ITEM_ADD p3;
                    uint slot = item.Slot;

                    if (this.Character.Inventory.MoveItem(this.Character.Inventory.GetContainerType(item.Slot), (int)item.Slot, ContainerType.BODY, 1))
                    {
                        if (item.Stack == 0)
                        {
                            if (slot == this.Character.Inventory.LastItem.Slot)
                            {
                                Packets.Server.SSMG_ITEM_CONTAINER_CHANGE p1 = new SagaMap.Packets.Server.SSMG_ITEM_CONTAINER_CHANGE();
                                p1.InventorySlot = item.Slot;
                                p1.Target = ContainerType.BODY;
                                this.netIO.SendPacket(p1);
                                Packets.Server.SSMG_ITEM_EQUIP p4 = new SagaMap.Packets.Server.SSMG_ITEM_EQUIP();
                                p4.InventorySlot = 0xffffffff;
                                p4.Target = ContainerType.NONE;
                                p4.Result = 3;
                                PC.StatusFactory.Instance.CalcRange(this.Character);
                                if (item.EquipSlot[0] == EnumEquipSlot.RIGHT_HAND)
                                {
                                    SendAttackType();
                                    SkillHandler.Instance.CastPassiveSkills(this.Character);
                                }
                                p4.Range = this.Character.Range;
                                this.netIO.SendPacket(p4);
                                this.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHANGE_EQUIP, null, this.Character, true);

                                PC.StatusFactory.Instance.CalcStatus(this.Character);
                                this.SendPlayerInfo();
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
                                    p3.Container = ContainerType.BODY;
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
                            if (this.Character.Inventory.LastItem.Stack == 1)
                            {
                                p3 = new SagaMap.Packets.Server.SSMG_ITEM_ADD();
                                p3.Container = ContainerType.BODY;
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
                    this.Character.Inventory.CalcPayloadVolume();
                    this.SendCapacity();
                }
            }
        }

        public void OnDEMPartsEquip(Packets.Client.CSMG_DEM_PARTS_EQUIP p)
        {
            if (this.chara.Race == PC_RACE.DEM && demParts)
            {
                Item item = this.Character.Inventory.GetItem(p.InventoryID);
                if (item == null)
                {
                    return;
                }
                int result = CheckEquipRequirement(item);
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
                foreach (EnumEquipSlot i in item.EquipSlot)
                {
                    if (this.Character.Inventory.Parts.ContainsKey(i))
                    {
                        Item oriItem = this.Character.Inventory.Parts[i];
                        
                        foreach (EnumEquipSlot j in oriItem.EquipSlot)
                        {
                            if (!this.Character.Inventory.Parts.ContainsKey(j))
                                continue;
                            Item dummyItem = this.Character.Inventory.Parts[j];
                            if (dummyItem.Stack == 0)
                            {
                                this.Character.Inventory.Parts.Remove(j);
                                continue;
                            }
                            ContainerType container = (ContainerType)(((ContainerType)Enum.Parse(typeof(ContainerType), j.ToString())) + 200);
                            if (this.Character.Inventory.MoveItem(container, (int)dummyItem.Slot, ContainerType.BODY, dummyItem.Stack))
                            {
                                Packets.Server.SSMG_ITEM_CONTAINER_CHANGE p1 = new SagaMap.Packets.Server.SSMG_ITEM_CONTAINER_CHANGE();
                                p1.InventorySlot = dummyItem.Slot;
                                p1.Target = ContainerType.BODY;
                                this.netIO.SendPacket(p1);
                                Packets.Server.SSMG_ITEM_EQUIP p4 = new SagaMap.Packets.Server.SSMG_ITEM_EQUIP();
                                p4.InventorySlot = 0xffffffff;
                                p4.Target = ContainerType.NONE;
                                p4.Result = 1;
                                p4.Range = this.Character.Range;
                                this.netIO.SendPacket(p4);

                                PC.StatusFactory.Instance.CalcStatus(this.Character);
                                this.SendPlayerInfo();
                            }
                        }
                    }
                }
                ushort count = item.Stack;
                if (count == 0) return;

                ContainerType dst = (ContainerType)(((ContainerType)Enum.Parse(typeof(ContainerType), item.EquipSlot[0].ToString())) + 200);
                if (this.Character.Inventory.MoveItem(this.Character.Inventory.GetContainerType(item.Slot), (int)item.Slot, dst, count))
                {
                    if (item.Stack == 0)
                    {
                        Packets.Server.SSMG_ITEM_EQUIP p4 = new SagaMap.Packets.Server.SSMG_ITEM_EQUIP();
                        dst = (ContainerType)((ContainerType)Enum.Parse(typeof(ContainerType), item.EquipSlot[0].ToString()));
                        p4.Target = dst;
                        p4.Result = 2;
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
                }
                List<EnumEquipSlot> slots = item.EquipSlot;
                if (slots.Count > 1)
                {
                    for (int i = 1; i < slots.Count; i++)
                    {
                        Item dummy = item.Clone();
                        dummy.Stack = 0;
                        dst = (ContainerType)(((ContainerType)Enum.Parse(typeof(ContainerType), slots[i].ToString())) + 200);
                        this.Character.Inventory.AddItem(dst, dummy);
                    }

                }
                if (item.EquipSlot[0] == EnumEquipSlot.RIGHT_HAND)
                {
                    SendAttackType();
                    SkillHandler.Instance.CastPassiveSkills(this.Character);
                }

                //SkillHandler.Instance.CheckBuffValid(this.Character);

                PC.StatusFactory.Instance.CalcStatus(this.Character);
                this.SendPlayerInfo();

                this.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHANGE_EQUIP, null, this.Character, true);               
            }
        }

        public void OnDEMPartsClose(Packets.Client.CSMG_DEM_PARTS_CLOSE p)
        {
            demParts = false;
        }

        public void OnDEMDemicInitialize(Packets.Client.CSMG_DEM_DEMIC_INITIALIZE p)
        {
            if (demic)
            {
                byte page = p.Page;
                DEMICPanel panel = null;
                bool[,] table = null;
                if (this.map.Info.Flag.Test(SagaDB.Map.MapFlags.Dominion))
                {
                    if (this.chara.Inventory.DominionDemicChips.ContainsKey(page))
                    {
                        panel = this.chara.Inventory.DominionDemicChips[page];
                        table = this.chara.Inventory.validTable(page, true);
                    }
                }
                else
                {
                    if (this.chara.Inventory.DemicChips.ContainsKey(page))
                    {
                        panel = this.chara.Inventory.DemicChips[page];
                        table = this.chara.Inventory.validTable(page, false);
                    }
                }

                Packets.Server.SSMG_DEM_DEMIC_INITIALIZED p1 = new SagaMap.Packets.Server.SSMG_DEM_DEMIC_INITIALIZED();
                p1.Page = page;

                if (panel != null)
                {
                    if (this.chara.EP > 0)
                    {
                        this.chara.EP--;
                        foreach (Chip i in panel.Chips)
                        {
                            if (i.Data.skill1 != 0)
                            {
                                if (this.chara.Skills.ContainsKey(i.Data.skill1))
                                {
                                    if (this.chara.Skills[i.Data.skill1].Level > 1)
                                        this.chara.Skills[i.Data.skill1].Level--;
                                    else
                                        this.chara.Skills.Remove(i.Data.skill1);
                                }
                            }
                            if (i.Data.skill2 != 0)
                            {
                                if (this.chara.Skills.ContainsKey(i.Data.skill2))
                                {
                                    if (this.chara.Skills[i.Data.skill2].Level > 1)
                                        this.chara.Skills[i.Data.skill2].Level--;
                                    else
                                        this.chara.Skills.Remove(i.Data.skill2);
                                }
                            }
                            if (i.Data.skill3 != 0)
                            {
                                if (this.chara.Skills.ContainsKey(i.Data.skill3))
                                {
                                    if (this.chara.Skills[i.Data.skill3].Level > 1)
                                        this.chara.Skills[i.Data.skill3].Level--;
                                    else
                                        this.chara.Skills.Remove(i.Data.skill3);
                                }
                            }
                            
                            AddItem(ItemFactory.Instance.GetItem(i.ItemID), true);
                        }
                        panel.Chips.Clear();
                        int engageTask;
                        int term = Global.Random.Next(0, 99);
                        if (term <= 10)
                            engageTask = 2;
                        else if (term <= 40)
                            engageTask = 1;
                        else
                            engageTask = 0;
                        panel.EngageTask1 = 255;
                        panel.EngageTask2 = 255;
                        for (int i = 0; i < engageTask; i++)
                        {
                            List<byte[]> valid = new List<byte[]>();
                            for (int j = 0; j < 9; j++)
                            {
                                for (int k = 0; k < 9; k++)
                                {
                                    if (table[k, j])
                                        valid.Add(new byte[] { (byte)k, (byte)j });
                                }
                            }
                            byte[] coord = valid[Global.Random.Next(0, valid.Count - 1)];
                            byte task = (byte)(coord[0] + coord[1] * 9);
                            if (i == 0)
                                panel.EngageTask1 = task;
                            else
                                panel.EngageTask2 = task;
                        }

                        SendActorHPMPSP(this.chara);

                        p1.Result = SagaMap.Packets.Server.SSMG_DEM_DEMIC_INITIALIZED.Results.OK;
                        p1.EngageTask = panel.EngageTask1;
                        p1.EngageTask2 = panel.EngageTask2;
                        
                        PC.StatusFactory.Instance.CalcStatus(this.chara);
                        SendPlayerInfo();
                    }
                    else
                        p1.Result = SagaMap.Packets.Server.SSMG_DEM_DEMIC_INITIALIZED.Results.NOT_ENOUGH_EP;
                }
                else
                    p1.Result = SagaMap.Packets.Server.SSMG_DEM_DEMIC_INITIALIZED.Results.FAILED;

                this.netIO.SendPacket(p1);
            }
        }

        public void OnDEMDemicConfirm(Packets.Client.CSMG_DEM_DEMIC_CONFIRM p)
        {
            if (demic)
            {
                short[,] chips = p.Chips;
                byte page = p.Page;
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        short chipID = chips[j, i];
                        if (ChipFactory.Instance.ByChipID.ContainsKey(chipID))
                        {
                            Chip chip = new Chip(ChipFactory.Instance.ByChipID[chipID]);
                            if (CountItem(chip.ItemID) > 0)
                            {
                                chip.X = (byte)j;
                                chip.Y = (byte)i;
                                if (this.chara.Inventory.InsertChip(page, chip))
                                {
                                    DeleteItemID(chip.ItemID, 1, true);
                                }
                            }
                        }
                    }
                }
                Packets.Server.SSMG_DEM_DEMIC_CONFIRM_RESULT p1 = new SagaMap.Packets.Server.SSMG_DEM_DEMIC_CONFIRM_RESULT();
                p1.Page = page;
                p1.Result = SagaMap.Packets.Server.SSMG_DEM_DEMIC_CONFIRM_RESULT.Results.OK;
                this.netIO.SendPacket(p1);

                PC.StatusFactory.Instance.CalcStatus(this.chara);
                SkillHandler.Instance.CastPassiveSkills(this.chara);
                SendPlayerInfo();
            }
        }

        public void OnDEMDemicClose(Packets.Client.CSMG_DEM_DEMIC_CLOSE p)
        {
            demic = false;
        }

        public void OnDEMStatsPreCalc(Packets.Client.CSMG_DEM_STATS_PRE_CALC p)
        {
            //backup
            ushort str, dex, intel, agi, vit, mag;
            str = this.Character.Str;
            dex = this.Character.Dex;
            intel = this.Character.Int;
            agi = this.Character.Agi;
            vit = this.Character.Vit;
            mag = this.Character.Mag;

            this.Character.Str = p.Str;
            this.Character.Dex = p.Dex;
            this.Character.Int = p.Int;
            this.Character.Agi = p.Agi;
            this.Character.Vit = p.Vit;
            this.Character.Mag = p.Mag;

            StatusFactory.Instance.CalcStatus(this.Character);

            {
                Packets.Server.SSMG_PLAYER_STATS_PRE_CALC p1 = new SagaMap.Packets.Server.SSMG_PLAYER_STATS_PRE_CALC();
                p1.ASPD = this.Character.Status.aspd;
                p1.ATK1Max = this.Character.Status.max_atk1;
                p1.ATK1Min = this.Character.Status.min_atk1;
                p1.ATK2Max = this.Character.Status.max_atk2;
                p1.ATK2Min = this.Character.Status.min_atk2;
                p1.ATK3Max = this.Character.Status.max_atk3;
                p1.ATK3Min = this.Character.Status.min_atk3;
                p1.AvoidCritical = this.Character.Status.avoid_critical;
                p1.AvoidMagic = this.Character.Status.avoid_magic;
                p1.AvoidMelee = this.Character.Status.avoid_melee;
                p1.AvoidRanged = this.Character.Status.avoid_ranged;
                p1.CSPD = this.Character.Status.cspd;
                p1.DefAddition = (ushort)this.Character.Status.def_add;
                p1.DefBase = this.Character.Status.def;
                p1.HitCritical = this.Character.Status.hit_critical;
                p1.HitMagic = this.Character.Status.hit_magic;
                p1.HitMelee = this.Character.Status.hit_melee;
                p1.HitRanged = this.Character.Status.hit_ranged;
                p1.MATKMax = this.Character.Status.max_matk;
                p1.MATKMin = this.Character.Status.min_matk;
                p1.MDefAddition = (ushort)this.Character.Status.mdef_add;
                p1.MDefBase = this.Character.Status.mdef;
                p1.Speed = this.Character.Speed;
                p1.HP = (ushort)this.Character.MaxHP;
                p1.MP = (ushort)this.Character.MaxMP;
                p1.SP = (ushort)this.Character.MaxSP;
                uint count = 0;
                foreach (uint i in this.Character.Inventory.MaxVolume.Values)
                {
                    count += i;
                }
                p1.Capacity = (ushort)count;
                count = 0;
                foreach (uint i in this.Character.Inventory.MaxPayload.Values)
                {
                    count += i;
                }
                p1.Payload = (ushort)count;

                this.netIO.SendPacket(p1);
            }
            {
                Packets.Server.SSMG_DEM_STATS_PRE_CALC p1 = new SagaMap.Packets.Server.SSMG_DEM_STATS_PRE_CALC();
                p1.ASPD = this.Character.Status.aspd;
                p1.ATK1Max = this.Character.Status.max_atk1;
                p1.ATK1Min = this.Character.Status.min_atk1;
                p1.ATK2Max = this.Character.Status.max_atk2;
                p1.ATK2Min = this.Character.Status.min_atk2;
                p1.ATK3Max = this.Character.Status.max_atk3;
                p1.ATK3Min = this.Character.Status.min_atk3;
                p1.AvoidCritical = this.Character.Status.avoid_critical;
                p1.AvoidMagic = this.Character.Status.avoid_magic;
                p1.AvoidMelee = this.Character.Status.avoid_melee;
                p1.AvoidRanged = this.Character.Status.avoid_ranged;
                p1.CSPD = this.Character.Status.cspd;
                p1.DefAddition = (ushort)this.Character.Status.def_add;
                p1.DefBase = this.Character.Status.def;
                p1.HitCritical = this.Character.Status.hit_critical;
                p1.HitMagic = this.Character.Status.hit_magic;
                p1.HitMelee = this.Character.Status.hit_melee;
                p1.HitRanged = this.Character.Status.hit_ranged;
                p1.MATKMax = this.Character.Status.max_matk;
                p1.MATKMin = this.Character.Status.min_matk;
                p1.MDefAddition = (ushort)this.Character.Status.mdef_add;
                p1.MDefBase = this.Character.Status.mdef;
                p1.Speed = this.Character.Speed;
                p1.HP = (ushort)this.Character.MaxHP;
                p1.MP = (ushort)this.Character.MaxMP;
                p1.SP = (ushort)this.Character.MaxSP;
                uint count = 0;
                foreach (uint i in this.Character.Inventory.MaxVolume.Values)
                {
                    count += i;
                }
                p1.Capacity = (ushort)count;
                count = 0;
                foreach (uint i in this.Character.Inventory.MaxPayload.Values)
                {
                    count += i;
                }
                p1.Payload = (ushort)count;

                this.netIO.SendPacket(p1);
            }

            //resotre
            this.Character.Str = str;
            this.Character.Dex = dex;
            this.Character.Int = intel;
            this.Character.Agi = agi;
            this.Character.Vit = vit;
            this.Character.Mag = mag;

            StatusFactory.Instance.CalcStatus(this.Character);
        }

        public void OnDEMChipCategory(Packets.Client.CSMG_DEM_CHIP_CATEGORY p)
        {
            if (chipShop)
            {
                if (ChipShopFactory.Instance.Items.ContainsKey(p.Category))
                {
                    currentChipCategory = p.Category;
                    ChipShopCategory category = ChipShopFactory.Instance.Items[p.Category];
                    Packets.Server.SSMG_DEM_CHIP_SHOP_HEADER p1 = new SagaMap.Packets.Server.SSMG_DEM_CHIP_SHOP_HEADER();
                    p1.CategoryID = p.Category;
                    this.netIO.SendPacket(p1);

                    foreach (ShopChip i in category.Items.Values)
                    {
                        Packets.Server.SSMG_DEM_CHIP_SHOP_DATA p2 = new SagaMap.Packets.Server.SSMG_DEM_CHIP_SHOP_DATA();
                        p2.EXP = (uint)i.EXP;
                        p2.JEXP = (uint)i.JEXP;
                        p2.ItemID = i.ItemID;
                        p2.Description = i.Description;
                        this.netIO.SendPacket(p2);
                    }

                    Packets.Server.SSMG_DEM_CHIP_SHOP_FOOTER p3 = new SagaMap.Packets.Server.SSMG_DEM_CHIP_SHOP_FOOTER();
                    this.netIO.SendPacket(p3);
                }
            }
        }

        public void OnDEMChipClose(Packets.Client.CSMG_DEM_CHIP_CLOSE p)
        {
            chipShop = false;
        }

        public void OnDEMChipBuy(Packets.Client.CSMG_DEM_CHIP_BUY p)
        {
            if (chipShop)
            {
                uint[] items = p.ItemIDs;
                int[] counts = p.Counts;

                for (int i = 0; i < items.Length; i++)
                {
                    var cat = from item in ChipShopFactory.Instance.Items.Values
                              where item.Items.ContainsKey(items[i])
                              select item;

                    if (cat.Count() > 0)
                    {
                        ChipShopCategory category = cat.First();
                        if (counts[i] > 0)
                        {
                            ShopChip chip = category.Items[items[i]];
                            if ((this.chara.CEXP > chip.EXP * (ulong)counts[i]) &&
                                this.chara.JEXP > chip.JEXP * (ulong)counts[i])
                            {
                                this.chara.CEXP -= (uint)(chip.EXP * (ulong)counts[i]);
                                this.chara.JEXP -= (uint)(chip.JEXP * (ulong)counts[i]);
                                SagaDB.Item.Item item = ItemFactory.Instance.GetItem(items[i]);
                                item.Stack = (ushort)counts[i];
                                AddItem(item, true);
                            }
                        }
                    }
                    SendEXP();
                }
            }
        }
    }
}

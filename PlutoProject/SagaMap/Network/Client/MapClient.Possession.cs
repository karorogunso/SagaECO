using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net;
using System.Net.Sockets;

using SagaDB;
using SagaDB.Item;
using SagaDB.Actor;
using SagaLib;
using SagaMap;
using SagaMap.Manager;


namespace SagaMap.Network.Client
{
    public partial class MapClient
    {
        public void OnPossessionRequest(Packets.Client.CSMG_POSSESSION_REQUEST p)
        {
            ActorPC target = (ActorPC)this.Map.GetActor(p.ActorID);
            PossessionPosition pos = p.PossessionPosition;
            int result = TestPossesionPosition(target, pos);
            if (result >= 0)
            {
                this.Character.Buff.GetReadyPossession = true;
                this.Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, this.Character, true);
                int reduce = 0;
                if (this.Character.Status.Additions.ContainsKey("TranceSpdUp"))
                {
                    Skill.Additions.Global.DefaultPassiveSkill passive = (Skill.Additions.Global.DefaultPassiveSkill)this.Character.Status.Additions["TranceSpdUp"];
                    reduce = passive["TranceSpdUp"];
                    
                }
                Tasks.PC.Possession task = new SagaMap.Tasks.PC.Possession(this, target, pos, p.Comment, reduce);
                this.Character.Tasks.Add("Possession", task);
                task.Activate();
            }
            else
            {
                Packets.Server.SSMG_POSSESSION_RESULT p1 = new SagaMap.Packets.Server.SSMG_POSSESSION_RESULT();
                p1.FromID = this.Character.ActorID;
                p1.ToID = 0xFFFFFFFF;
                p1.Result = result;
                this.netIO.SendPacket(p1);
            }
        }

        public void OnPossessionCancel(Packets.Client.CSMG_POSSESSION_CANCEL p)
        {
            PossessionPosition pos = p.PossessionPosition;
            switch (pos)
            {
                case PossessionPosition.NONE:
                    Actor actor = this.Map.GetActor(this.Character.PossessionTarget);
                    if (actor == null)
                        return;
                    PossessionArg arg = new PossessionArg();
                    arg.fromID = this.Character.ActorID;
                    arg.cancel = true;
                    arg.result = (int)this.Character.PossessionPosition;
                    arg.x = Global.PosX16to8(this.Character.X, Map.Width);
                    arg.y = Global.PosY16to8(this.Character.Y, Map.Height);
                    arg.dir = (byte)(this.Character.Dir / 45);
                    if (actor.type == ActorType.ITEM)
                    {
                        Item item = GetPossessionItem(this.Character, this.Character.PossessionPosition);
                        item.PossessionedActor = null;
                        item.PossessionOwner = null;
                        this.Character.PossessionTarget = 0;
                        this.Character.PossessionPosition = PossessionPosition.NONE;
                        arg.toID = 0xFFFFFFFF;
                        this.Map.DeleteActor(actor);
                    }
                    else if (actor.type == ActorType.PC)
                    {
                        ActorPC pc = (ActorPC)actor;
                        arg.toID = pc.ActorID;
                        Item item = GetPossessionItem(pc, this.Character.PossessionPosition);
                        if (item.PossessionOwner != this.Character)
                        {
                            item.PossessionedActor = null;
                            this.Character.PossessionTarget = 0;
                            this.Character.PossessionPosition = PossessionPosition.NONE;
                        }
                        else
                        {
                            Item item2 = GetPossessionItem(this.Character, this.Character.PossessionPosition);
                            item2.PossessionedActor = null;
                            item2.PossessionOwner = null;
                            this.Character.PossessionTarget = 0;
                            this.Character.PossessionPosition = PossessionPosition.NONE;
                            Packets.Client.CSMG_ITEM_MOVE p3 = new SagaMap.Packets.Client.CSMG_ITEM_MOVE();
                            p3.data = new byte[9];
                            p3.InventoryID = item.Slot;
                            p3.Target = ContainerType.BODY;
                            p3.Count = 1;
                            MapClient.FromActorPC(pc).OnItemMove(p3, true);
                            pc.Inventory.DeleteItem(item.Slot, 1);
                            
                            Packets.Server.SSMG_ITEM_DELETE p2 = new SagaMap.Packets.Server.SSMG_ITEM_DELETE();
                            p2.InventorySlot = item.Slot;
                            ((ActorEventHandlers.PCEventHandler)pc.e).Client.netIO.SendPacket(p2);
                            this.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHANGE_EQUIP, null, pc, true);
                        }
                        item.PossessionedActor = null;
                        item.PossessionOwner = null;

                        PC.StatusFactory.Instance.CalcStatus(this.Character);
                        SendPlayerInfo();
                        PC.StatusFactory.Instance.CalcStatus(pc);
                        ((ActorEventHandlers.PCEventHandler)pc.e).Client.SendPlayerInfo();
                    }
                    this.Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.POSSESSION, arg, this.Character, true);
                    break;
                default :
                    Item item3 = GetPossessionItem(this.Character, pos);
                    if (item3 == null)
                        return;
                    if (item3.PossessionedActor == null)
                        return;
                    PossessionArg arg2 = new PossessionArg();
                    arg2.fromID = item3.PossessionedActor.ActorID;
                    arg2.toID = this.Character.ActorID;
                    arg2.cancel = true;
                    arg2.result = (int)item3.PossessionedActor.PossessionPosition;
                    arg2.x = Global.PosX16to8(this.Character.X, Map.Width);
                    arg2.y = Global.PosY16to8(this.Character.Y, Map.Height);
                    arg2.dir = (byte)(this.Character.Dir / 45);


                    if (item3.PossessionOwner != this.Character && item3.PossessionOwner != null)
                    {
                        Item item4 = GetPossessionItem(item3.PossessionedActor, item3.PossessionedActor.PossessionPosition);
                        if (item4 != null)
                        {
                            item4.PossessionedActor = null;
                            item4.PossessionOwner = null;
                        }

                        Packets.Client.CSMG_ITEM_MOVE p3 = new SagaMap.Packets.Client.CSMG_ITEM_MOVE();
                        p3.data = new byte[9];
                        p3.InventoryID = item3.Slot;
                        p3.Target = ContainerType.BODY;
                        p3.Count = 1;
                        OnItemMove(p3, true);
                        this.Character.Inventory.DeleteItem(item3.Slot, 1);

                        Packets.Server.SSMG_ITEM_DELETE p2 = new SagaMap.Packets.Server.SSMG_ITEM_DELETE();
                        p2.InventorySlot = item3.Slot;
                        this.netIO.SendPacket(p2);
                        this.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHANGE_EQUIP, null, this.Character, true);

                        this.Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.POSSESSION, arg2, this.Character, true);
                        if (((ActorEventHandlers.PCEventHandler)item3.PossessionedActor.e).Client.state == SESSION_STATE.DISCONNECTED)
                        {
                            ActorItem itemactor = PossessionItemAdd(item3.PossessionedActor, item3.PossessionedActor.PossessionPosition, "");
                            item3.PossessionedActor.PossessionTarget = itemactor.ActorID;
                            MapServer.charDB.SaveChar(item3.PossessionedActor, false, false);
                            MapServer.accountDB.WriteUser(item3.PossessionedActor.Account);            
                            return;
                        }
                    }
                    else
                    {
                        Actor actor2 = map.GetActor(this.chara.PossessionTarget);
                        if (actor2 != null)
                        {
                            if (actor2.type == ActorType.ITEM)
                                this.map.DeleteActor(actor2);
                            if (!item3.PossessionedActor.Online)
                            {
                                arg2.fromID = 0xFFFFFFFF;
                            }
                        }
                        this.Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.POSSESSION, arg2, this.Character, true);
                    }
                    item3.PossessionedActor.PossessionTarget = 0;
                    item3.PossessionedActor.PossessionPosition = PossessionPosition.NONE;
                    item3.PossessionedActor = null;
                    item3.PossessionOwner = null;
                    PC.StatusFactory.Instance.CalcStatus(this.Character);
                    SendPlayerInfo();
                    break;
            }
        }

        public void PossessionPerform(ActorPC target, PossessionPosition position, string comment)
        {
            int result = TestPossesionPosition(target, position);
            if (result >= 0)
            {
                PossessionArg arg = new PossessionArg();
                arg.fromID = this.Character.ActorID;
                arg.toID = target.ActorID;
                arg.result = result;
                arg.comment = comment;
                this.Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.POSSESSION, arg, this.Character, true);

                string pos = "";
                switch (position)
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
                if (target == this.Character)
                {
                    this.Character.PossessionTarget = PossessionItemAdd(this.Character, position, comment).ActorID;
                    this.Character.PossessionPosition = position;
                }
                else
                {
                    MapClient.FromActorPC(target).SendSystemMessage(string.Format(LocalManager.Instance.Strings.POSSESSION_DONE, pos));                
                    this.Character.PossessionTarget = target.ActorID;
                    this.Character.PossessionPosition = position;
                    Item item = GetPossessionItem(target, position);
                    item.PossessionedActor = this.Character;
                }

                if (!this.Character.Tasks.ContainsKey("PossessionRecover"))
                {
                    Tasks.PC.PossessionRecover task = new SagaMap.Tasks.PC.PossessionRecover(this);
                    this.Character.Tasks.Add("PossessionRecover", task);
                    task.Activate();
                }
                Skill.SkillHandler.Instance.CastPassiveSkills(this.Character);
                PC.StatusFactory.Instance.CalcStatus(this.Character);
                SendPlayerInfo(); 
                PC.StatusFactory.Instance.CalcStatus(target);
                ((ActorEventHandlers.PCEventHandler)target.e).Client.SendPlayerInfo();                
            }
            else
            {
                Packets.Server.SSMG_POSSESSION_RESULT p1 = new SagaMap.Packets.Server.SSMG_POSSESSION_RESULT();
                p1.FromID = this.Character.ActorID;
                p1.ToID = 0xFFFFFFFF;
                p1.Result = result;
                this.netIO.SendPacket(p1);
            }
        }

        int TestPossesionPosition(ActorPC target, PossessionPosition pos)
        {
            Item item = null;
            if (this.Character.PossessionTarget != 0)
                return -1; //憑依失敗 : 憑依中です
            if (this.Character.PossesionedActors.Count != 0)
                return -2; //憑依失敗 : 宿主です
            if (target.type != ActorType.PC)
                return -3; //憑依失敗 : プレイヤーのみ憑依可能です
            ActorPC targetPC = (ActorPC)target;
            //if (Math.Abs(target.Level - this.Character.Level) > 30)
            //    return -4; //憑依失敗 : レベルが離れすぎです
            switch (pos)
            {
                case PossessionPosition.CHEST:
                    if (targetPC.Inventory.Equipments.ContainsKey(EnumEquipSlot.UPPER_BODY))
                        item = targetPC.Inventory.Equipments[EnumEquipSlot.UPPER_BODY];
                    else
                        return -5; //憑依失敗 : 装備がありません
                    break;
                case PossessionPosition.LEFT_HAND:
                    if (targetPC.Inventory.Equipments.ContainsKey(EnumEquipSlot.LEFT_HAND))
                        item = targetPC.Inventory.Equipments[EnumEquipSlot.LEFT_HAND];
                    else
                        return -5; //憑依失敗 : 装備がありません
                    break;
                case PossessionPosition.NECK:
                    if (targetPC.Inventory.Equipments.ContainsKey(EnumEquipSlot.CHEST_ACCE))
                        item = targetPC.Inventory.Equipments[EnumEquipSlot.CHEST_ACCE];
                    else
                        return -5; //憑依失敗 : 装備がありません
                    break;
                case PossessionPosition.RIGHT_HAND:
                    if (targetPC.Inventory.Equipments.ContainsKey(EnumEquipSlot.RIGHT_HAND))
                    {
                        if(targetPC.Buff.FishingState == true)
                        {
                            return -15;
                        }
                        item = targetPC.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND];
                    }
                    else
                    {
                        return -5; //憑依失敗 : 装備がありません
                    }
                    break;
            }
            if (item == null)
                return -5; //憑依失敗 : 装備がありません
            if (item.Stack == 0)
                return -5; ////憑依失敗 : 装備がありません
            if (item.PossessionedActor != null)
                return -6; //憑依失敗 : 誰かが憑依しています
            if (item.BaseData.itemType == ItemType.CARD || item.BaseData.itemType == ItemType.THROW || item.BaseData.itemType == ItemType.ARROW || item.BaseData.itemType == ItemType.BULLET)
                return -7; //憑依失敗 : 憑依不可能なアイテムです
            if (targetPC.PossesionedActors.Count >= 3)
                return -8; //憑依失敗 : 満員宿主です
            if (this.Character.Marionette != null || targetPC.Marionette != null
                || this.Character.Buff.Confused || this.Character.Buff.Frosen || this.Character.Buff.Paralysis
                || this.Character.Buff.Sleep || this.Character.Buff.Stone || this.Character.Buff.Stun)
                return -15; //憑依失敗 : 状態異常中です
            if (targetPC.PossessionTarget != 0)
                return -16; //憑依失敗 : 相手は憑依中です
            if (target.Buff.GetReadyPossession)
                return -17; //憑依失敗 : 相手はGetReadyPossession中です
            if (target.Buff.Dead)
                return -18; //憑依失敗 : 相手は行動不能状態です
            if (this.scriptThread != null || ((ActorEventHandlers.PCEventHandler)target.e).Client.scriptThread != null || target.Buff.FishingState)
                return -19; //憑依失敗 : イベント中です
            if (this.Character.Tasks.ContainsKey("ItemCast"))
                return -19; //憑依失敗 : イベント中です
            if (this.Character.MapID != target.MapID)
                return -20; //憑依失敗 : 相手とマップが違います
            if (Math.Abs(target.X - this.Character.X) > 300 || Math.Abs(target.Y - this.Character.Y) > 300)
                return -21; //憑依失敗 : 相手と離れすぎています
            if (!target.canPossession)
                return -22; //憑依失敗 : 相手が憑依不許可設定中です
            if (this.Character.Pet != null)
            {
                if (this.Character.Pet.Ride)
                    return -27; //憑依失敗 : 騎乗中です
            }
            if (this.Character.Buff.Dead)
                return -29; //憑依失敗: 憑依できない状態です
            if (this.chara.Race == PC_RACE.DEM)
                return -29; //憑依失敗 : 憑依できない状態です
            if (targetPC.Race == PC_RACE.DEM && targetPC.Form == DEM_FORM.MACHINA_FORM)
                return -31; //憑依失敗 : マシナフォームのＤＥＭキャラクターに憑依することはできません
            /*
            if (this.Character.Buff.GetReadyPossession == true || this.Character.PossessionTarget != 0)
                return -100; //憑依失敗 : 何らかの原因で出来ませんでした
            */
            return (int)pos;
        }

        Item GetPossessionItem(ActorPC target, PossessionPosition pos)
        {
            Item item = null;
            switch (pos)
            {
                case PossessionPosition.CHEST:
                    if (target.Inventory.Equipments.ContainsKey(EnumEquipSlot.UPPER_BODY))
                        item = target.Inventory.Equipments[EnumEquipSlot.UPPER_BODY];
                    break;
                case PossessionPosition.LEFT_HAND:
                    if (target.Inventory.Equipments.ContainsKey(EnumEquipSlot.LEFT_HAND))
                        item = target.Inventory.Equipments[EnumEquipSlot.LEFT_HAND];
                    break;
                case PossessionPosition.NECK:
                    if (target.Inventory.Equipments.ContainsKey(EnumEquipSlot.CHEST_ACCE))
                        item = target.Inventory.Equipments[EnumEquipSlot.CHEST_ACCE];
                    break;
                case PossessionPosition.RIGHT_HAND:
                    if (target.Inventory.Equipments.ContainsKey(EnumEquipSlot.RIGHT_HAND))
                        item = target.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND];
                    break;
            }
            return item;
        }

        ActorItem PossessionItemAdd(ActorPC target, PossessionPosition position, string comment)
        {
            Item itemDroped = GetPossessionItem(target, position);
            if (itemDroped == null) return null;
            itemDroped.PossessionedActor = target;
            itemDroped.PossessionOwner = target;
            ActorItem actor = new ActorItem(itemDroped);
            actor.e = new ActorEventHandlers.ItemEventHandler(actor);
            actor.MapID = target.MapID;
            actor.X = target.X;
            actor.Y = target.Y;
            actor.Comment = comment;
            this.Map.RegisterActor(actor);
            actor.invisble = false;
            this.Map.OnActorVisibilityChange(actor);
            return actor;
        }

        ActorPC GetPossessionTarget()
        {
            if (this.Character.PossessionTarget == 0)
                return null;
            Actor actor = this.Map.GetActor(this.Character.PossessionTarget);
            if (actor == null)
                return null;
            if (actor.type != ActorType.PC)
                return null;
            return (ActorPC)actor;
        }

        void PossessionPrepareCancel()
        {
            if (this.Character.Buff.GetReadyPossession)
            {
                this.Character.Buff.GetReadyPossession = false;
                this.Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, this.Character, true);
                if (this.Character.Tasks.ContainsKey("Possession"))
                {
                    this.Character.Tasks["Possession"].Deactivate();
                    this.Character.Tasks.Remove("Possession");
                }
            }
        }

        public void OnPossessionCatalogRequest(Packets.Client.CSMG_POSSESSION_CATALOG_REQUEST p)
        {
            List<ActorItem> list = new List<ActorItem>();
            foreach(Actor actor in this.map.Actors.Values)
            {
                if (actor is ActorItem)
                {
                    ActorItem item = (ActorItem)actor;
                    if (item.Item.PossessionedActor.PossessionPosition==p.Position)
                        list.Add(item);
                }
            }
            int pageSize = 5;
            int skip = pageSize * (p.Page - 1);
            var  items = list.Select(x=>x)
                         .Skip(skip)
                         .Take(pageSize)
                         .ToArray();
            for (int i=0;i<items.Length;i++)
            {
                Packets.Server.SSMG_POSSESSION_CATALOG p1 = new Packets.Server.SSMG_POSSESSION_CATALOG();
                p1.ActorID = items[i].ActorID;
                p1.comment = items[i].Comment;
                p1.Index = (uint)i+1;
                p1.Item = items[i].Item;
                this.netIO.SendPacket(p1);
            }
            Packets.Server.SSMG_POSSESSION_CATALOG_END p2 = new Packets.Server.SSMG_POSSESSION_CATALOG_END();
            p2.Page = p.Page;
            this.netIO.SendPacket(p2);
        }
        public void OnPossessionCatalogItemInfoRequest(Packets.Client.CSMG_POSSESSION_CATALOG_ITEM_INFO_REQUEST p)
        {
            Actor target = this.map.GetActor(p.ActorID);
            if (target!=null)
            {
                if (target is ActorItem)
                {
                    ActorItem item = (ActorItem)target;
                    Packets.Server.SSMG_POSSESSION_CATALOG_ITEM_INFO p2 = new Packets.Server.SSMG_POSSESSION_CATALOG_ITEM_INFO();
                    p2.ActorID = item.ActorID;
                    p2.ItemID = item.Item.ItemID;
                    p2.Level = item.Item.BaseData.possibleLv;
                    p2.X = Global.PosX16to8(item.X,this.map.Width);
                    p2.Y = Global.PosY16to8(item.Y, this.map.Height);
                    this.netIO.SendPacket(p2);
                }
            }
        }
        public void OnPartnerPossessionRequest(Packets.Client.CSMG_POSSESSION_PARTNER_REQUEST p)
        {
            Item partneritem = Character.Inventory.GetItem(p.InventorySlot);
            if (Character.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                if (partneritem == Character.Inventory.Equipments[EnumEquipSlot.PET])
                    return;
            ActorPartner partner = Character.Partner;
            if (partner == null) return;
            uint Pict = partneritem.BaseData.petID;
            if (Pict == partner.BaseData.pictid) return;
            if (partneritem != null)
            {
                switch (p.PossessionPosition)
                {
                    case PossessionPosition.RIGHT_HAND:
                        if (Character.Inventory.Equipments.ContainsKey(EnumEquipSlot.RIGHT_HAND))
                        {
                            Character.PossessionPartnerSlotIDinRightHand = Pict;
                            Character.Status.max_atk1_petpy = (short)(partner.Status.max_atk1 / 2);
                            Character.Status.min_atk1_petpy = (short)(partner.Status.min_atk1 / 2);
                            Character.Status.max_matk_petpy = (short)(partner.Status.max_matk / 2);
                            Character.Status.min_matk_petpy = (short)(partner.Status.min_matk / 2);
                        }
                        else
                        {
                            Character.PossessionPartnerSlotIDinRightHand = 0;
                            Character.Status.max_atk1_petpy = 0;
                            Character.Status.min_atk1_petpy = 0;
                            Character.Status.max_matk_petpy = 0;
                            Character.Status.min_matk_petpy = 0;
                        }
                        break;
                    case PossessionPosition.LEFT_HAND:
                        if (Character.Inventory.Equipments.ContainsKey(EnumEquipSlot.LEFT_HAND))
                        {
                            Character.PossessionPartnerSlotIDinLeftHand = Pict;
                            Character.Status.def_add_petpy = (short)(partner.Status.def_add / 2);
                            Character.Status.mdef_add_petpy = (short)(partner.Status.mdef_add / 2);
                        }
                        else
                        {
                            Character.PossessionPartnerSlotIDinLeftHand = 0;
                            Character.Status.def_add_petpy = 0;
                            Character.Status.mdef_add_petpy = 0;
                        }
                        break;
                    case PossessionPosition.NECK:
                        if (Character.Inventory.Equipments.ContainsKey(EnumEquipSlot.CHEST_ACCE))
                        {
                            Character.PossessionPartnerSlotIDinAccesory = Pict;
                            Character.Status.aspd_petpy = (short)(partner.Status.aspd / 2);
                            Character.Status.cspd_petpy = (short)(partner.Status.cspd / 2);
                        }
                        else
                        {
                            Character.PossessionPartnerSlotIDinAccesory = 0;
                            Character.Status.aspd_petpy = 0;
                            Character.Status.cspd_petpy = 0;
                        }
                        break;
                    case PossessionPosition.CHEST:
                        if (Character.Inventory.Equipments.ContainsKey(EnumEquipSlot.UPPER_BODY))
                        {
                            Character.PossessionPartnerSlotIDinClothes = Pict;
                            Character.Status.hp_petpy = (short)(partner.MaxHP / 20);
                        }
                        else
                        {
                            Character.PossessionPartnerSlotIDinClothes = 0;
                            Character.Status.hp_petpy = 0;
                        }
                        break;
                }
                SagaMap.PC.StatusFactory.Instance.CalcStatus(Character);
                Packets.Server.SSMG_POSSESSION_PARTNER_RESULT p1 = new Packets.Server.SSMG_POSSESSION_PARTNER_RESULT();
                p1.InventorySlot = p.InventorySlot;
                p1.Pos = p.PossessionPosition;
                this.netIO.SendPacket(p1);
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHAR_INFO_UPDATE, null, Character, true);
            }
        }
        public void OnPartnerPossessionCancel(Packets.Client.CSMG_POSSESSION_PARTNER_CANCEL p)
        {
            Packets.Server.SSMG_POSSESSION_PARTNER_CANCEL p1 = new Packets.Server.SSMG_POSSESSION_PARTNER_CANCEL();
            this.netIO.SendPacket(p1);
            switch (p.PossessionPosition)
            {
                case PossessionPosition.RIGHT_HAND:
                    Character.PossessionPartnerSlotIDinRightHand = 0;
                    break;
                case PossessionPosition.LEFT_HAND:
                    Character.PossessionPartnerSlotIDinLeftHand = 0;
                    break;
                case PossessionPosition.NECK:
                    Character.PossessionPartnerSlotIDinAccesory = 0;
                    break;
                case PossessionPosition.CHEST:
                    Character.PossessionPartnerSlotIDinClothes = 0;
                    break;
            }
            p1.Pos = p.PossessionPosition;
            this.netIO.SendPacket(p1);
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHAR_INFO_UPDATE, null, Character, true);
        }
    }
}

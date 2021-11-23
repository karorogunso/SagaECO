using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Item;
using SagaMap.Network.Client;
using SagaDB;
using SagaDB.Actor;
using SagaLib;
using SagaDB.Skill;

namespace SagaMap.Skill
{
    public partial class SkillHandler : Singleton<SkillHandler>
    {
        public void CancelSkillCast(Actor actor)
        {
            if (actor.type == ActorType.PC)
            {
                Network.Client.MapClient.FromActorPC((ActorPC)actor).SendSkillDummy();
            }
            else
            {
                if (actor.Tasks.ContainsKey("SkillCast"))
                {
                    if (actor.Tasks["SkillCast"].Activated)
                    {
                        actor.Tasks["SkillCast"].Deactivate();
                        actor.Tasks.Remove("SkillCast");
                    }
                    SkillArg arg = new SkillArg();
                    arg.sActor = actor.ActorID;
                    arg.dActor = 0;
                    arg.skill = SkillFactory.Instance.GetSkill(3311, 1);
                    arg.x = 0;
                    arg.y = 0;
                    arg.hp = new List<int>();
                    arg.sp = new List<int>();
                    arg.mp = new List<int>();
                    arg.hp.Add(0);
                    arg.sp.Add(0);
                    arg.mp.Add(0);
                    arg.flag.Add(AttackFlag.NONE);
                    //arg.affectedActors.Add(this.Character);
                    arg.argType = SkillArg.ArgType.Active;
                    Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, arg, actor, true);
                }
            }
        }

        public void SendAttackMessage(byte type, Actor sActor, string Sender, string Content)
        {
            if (sActor.type == ActorType.PC)
            {
                Packets.Server.SSMG_CHAT_JOB p = new Packets.Server.SSMG_CHAT_JOB();
                p.Type = type;
                p.Sender = Sender;
                p.Content = Content;
                MapClient.FromActorPC((ActorPC)sActor).netIO.SendPacket(p);
            }
        }
        /// <summary>
        /// 附加圣印
        /// </summary>
        /// <param name="dActor">目标</param>
        public void Seals(Actor sActor, Actor dActor)
        {
            Seals(sActor, dActor, 1);
        }
        public void Seals(Actor sActor, Actor dActor, byte count)
        {
            if (sActor.type == ActorType.PC)
            {
                if (((ActorPC)sActor).PossessionTarget != 0)//凭依时无效
                    return;
            }
            if (dActor != null)
            {
                if (sActor.Status.Additions.ContainsKey("Seals"))
                {
                    EffectArg arg = new EffectArg();
                    arg.effectID = 4238;
                    arg.actorID = dActor.ActorID;
                    if (sActor.type == ActorType.PC)
                        SagaMap.Network.Client.MapClient.FromActorPC((ActorPC)sActor).map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, arg, (ActorPC)sActor, true);
                    dActor.IsSeals = 1;
                    for (int i = 0; i < count; i++)
                    {
                        Additions.Global.Seals Seals = new Additions.Global.Seals(null, dActor, 6000);
                        ApplyAddition(dActor, Seals);
                    }
                }
            }
        }
        /// <summary>
        /// 让Actor说话
        /// </summary>
        /// <param name="actor">说话者</param>
        /// <param name="message">内容</param>
        public void ActorSpeak(Actor actor, string message)
        {
            ChatArg arg = new ChatArg();
            arg.content = message;
            SagaMap.Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHAT, arg, actor, false);
        }
        /// <summary>
        /// 让actor跳数值
        /// </summary>
        /// <param name="actor">目标</param>
        /// <param name="hp">血量，正值为伤害，负值为恢复</param>
        /// <param name="mp">法力，正值为伤害，负值为恢复</param>
        /// <param name="sp">SP，正值为伤害，负值为恢复</param>
        public void ShowVessel(Actor actor, int hp = 0, int mp = 0, int sp = 0)
        {
            SkillArg arg = new SkillArg();
            arg.affectedActors.Add(actor);
            arg.Init();
            arg.argType = SkillArg.ArgType.Item_Active;
            Item item0 = ItemFactory.Instance.GetItem(10000000);
            arg.item = item0;
            arg.hp[0] = hp;
            arg.mp[0] = mp;
            arg.sp[0] = sp;
            if (hp > 0)
                arg.flag[0] |= AttackFlag.HP_DAMAGE;
            else if (hp < 0)
            {
                arg.item = ItemFactory.Instance.GetItem(10000000);
                arg.flag[0] |= AttackFlag.HP_HEAL;
                arg.argType = SkillArg.ArgType.Item_Active;
            }
            if (mp > 0)
                arg.flag[0] |= AttackFlag.MP_DAMAGE;
            else if (mp < 0)
                arg.flag[0] |= AttackFlag.MP_HEAL;
            if (sp > 0)
                arg.flag[0] |= AttackFlag.SP_DAMAGE;
            else if (sp < 0)
                arg.flag[0] |= AttackFlag.SP_HEAL;

            SagaMap.Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, arg, actor, true);
            SagaMap.Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, arg, actor, true);
        }

        /// <summary>
        /// 武器装备破损
        /// </summary>
        /// <param name="pc">玩家</param>
        public void WeaponWorn(ActorPC pc)
        {
            uint rate = 2;
            if (pc.Status.Additions.ContainsKey("fish"))
                rate = 60;
            if (Global.Random.Next(0, 6000) < rate)
            {
                if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND))
                {
                    if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].PossessionedActor != null)
                        return;
                    if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].maxDurability == 0)
                        return;
                    EffectArg arg = new EffectArg();
                    MapClient client = MapClient.FromActorPC(pc);
                    if (pc.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].Durability <= 0 || pc.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].Durability > pc.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].maxDurability)
                    {
                        client.SendSystemMessage("武器[" + pc.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].BaseData.name + "]损毁！");
                        Packets.Server.SSMG_ITEM_DELETE p2;
                        p2 = new SagaMap.Packets.Server.SSMG_ITEM_DELETE();
                        p2.InventorySlot = pc.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].Slot;
                        client.netIO.SendPacket(p2);
                        Item oriItem = pc.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND];
                        client.ItemMoveSub(oriItem, ContainerType.BODY, oriItem.Stack);
                        if (oriItem.BaseData.repairItem == 0)
                            client.DeleteItem(pc.Inventory.LastItem.Slot, pc.Inventory.LastItem.Stack, true);
                        return;
                    }
                    arg.actorID = client.Character.ActorID;
                    arg.effectID = 8044;
                    client.Character.e.OnShowEffect(client.Character, arg);
                    pc.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].Durability -= 1;
                    client.SendSystemMessage("武器[" + pc.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].BaseData.name + "]耐久度下降！(" + pc.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].Durability +
                      "/" + pc.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].maxDurability + ")");
                    client.SendItemInfo(pc.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND]);
                }
            }
        }
        /// <summary>
        /// 防具装备破损
        /// </summary>
        /// <param name="pc">玩家</param>
        public void ArmorWorn(ActorPC pc)
        {
            if (Global.Random.Next(0, 3500) < 2)
            {
                EffectArg arg = new EffectArg();
                MapClient client = MapClient.FromActorPC(pc);
                EnumEquipSlot ArmorEnum = new EnumEquipSlot();
                switch (Global.Random.Next(1, 11))
                {
                    case 1:
                        ArmorEnum = EnumEquipSlot.BACK;
                        break;
                    case 2:
                        ArmorEnum = EnumEquipSlot.CHEST_ACCE;
                        break;
                    case 3:
                        ArmorEnum = EnumEquipSlot.FACE;
                        break;
                    case 4:
                        ArmorEnum = EnumEquipSlot.FACE_ACCE;
                        break;
                    case 5:
                        ArmorEnum = EnumEquipSlot.HEAD;
                        break;
                    case 6:
                        ArmorEnum = EnumEquipSlot.HEAD_ACCE;
                        break;
                    case 7:
                        ArmorEnum = EnumEquipSlot.LEFT_HAND;
                        break;
                    case 8:
                        ArmorEnum = EnumEquipSlot.LOWER_BODY;
                        break;
                    case 9:
                        ArmorEnum = EnumEquipSlot.SHOES;
                        break;
                    case 10:
                        ArmorEnum = EnumEquipSlot.SOCKS;
                        break;
                    case 11:
                        ArmorEnum = EnumEquipSlot.UPPER_BODY;
                        break;
                }
                if (pc.Inventory.Equipments.ContainsKey(ArmorEnum))
                {
                    if (pc.Inventory.Equipments[ArmorEnum].PossessionedActor != null)
                        return;
                    if (pc.Inventory.Equipments[ArmorEnum].maxDurability == 0)
                        return;
                    if (pc.Inventory.Equipments[ArmorEnum].Durability <= 0 || pc.Inventory.Equipments[ArmorEnum].Durability > pc.Inventory.Equipments[ArmorEnum].maxDurability)
                    {
                        client.SendSystemMessage("装备[" + pc.Inventory.Equipments[ArmorEnum].BaseData.name + "]损毁！");
                        Packets.Server.SSMG_ITEM_DELETE p2;
                        p2 = new SagaMap.Packets.Server.SSMG_ITEM_DELETE();
                        p2.InventorySlot = pc.Inventory.Equipments[ArmorEnum].Slot;
                        client.netIO.SendPacket(p2);
                        pc.Inventory.Equipments.Remove(ArmorEnum);
                        client.SendItems();
                        client.SendEquip();
                        return;
                    }
                    arg.actorID = client.Character.ActorID;
                    arg.effectID = 8044;
                    client.Character.e.OnShowEffect(client.Character, arg);
                    pc.Inventory.Equipments[ArmorEnum].Durability -= 1;
                    client.SendSystemMessage("装备[" + pc.Inventory.Equipments[ArmorEnum].BaseData.name + "]耐久度下降！(" + pc.Inventory.Equipments[ArmorEnum].Durability +
                      "/" + pc.Inventory.Equipments[ArmorEnum].maxDurability + ")");
                    client.SendItemInfo(pc.Inventory.Equipments[ArmorEnum]);
                }
            }
        }
        public void Attack(Actor sActor, Actor dActor, SkillArg arg)
        {
            Attack(sActor, dActor, arg, 1f);
        }
        public void Attack(Actor sActor, Actor dActor, SkillArg arg, float factor)
        {
            if (sActor.Status.Additions.ContainsKey("Stun") || sActor.Status.Additions.ContainsKey("Sleep") || sActor.Status.Additions.ContainsKey("Frosen") ||
                sActor.Status.Additions.ContainsKey("Stone"))
                return;
            int combo = GetComboCount(sActor);
            arg.sActor = sActor.ActorID;
            arg.dActor = dActor.ActorID;
            for (int i = 0; i < combo; i++)
            {
                arg.affectedActors.Add(dActor);
            }
            arg.type = sActor.Status.attackType;
            arg.delayRate = 1f + ((float)combo / 2);
            /*
            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND))
                {
                    if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.BOW)
                    {
                        if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.LEFT_HAND))
                        {
                            if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].BaseData.itemType == SagaDB.Item.ItemType.ARROW)
                                if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].Stack > 0)
                                    MapClient.FromActorPC(pc).DeleteItem(pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].Slot, 1, false);
                                else
                                {
                                    arg.result = -34;
                                }
                            else
                            {
                                arg.result = -34;
                            }
                        }
                        else
                        {
                            arg.result = -34;
                        }
                    }
                }
            }
            */
            if (arg.result == 0)
            {
                PhysicalAttack(sActor, arg.affectedActors, arg, Elements.Neutral, factor);
                if (((arg.flag[0] | AttackFlag.MISS) != arg.flag[0]) && ((arg.flag[0] | AttackFlag.AVOID) != arg.flag[0]))
                {
                    if (sActor.Status.Additions.ContainsKey("WithinWeeks"))//ウィークネスショット 
                    {
                        if (!isBossMob(dActor))
                        {
                            if (Global.Random.Next(0, 100) < 5)
                            {
                                Additions.Global.DefaultBuff skill = (Additions.Global.DefaultBuff)sActor.Status.Additions["WithinWeeks"];
                                int SkillLevel = skill.skill.Level;
                                switch (SkillLevel)
                                {
                                    case 1:
                                        Additions.Global.Silence Silence = new SagaMap.Skill.Additions.Global.Silence(null, dActor, 6000);
                                        ApplyAddition(dActor, Silence);
                                        break;
                                    case 2:
                                    case 3:
                                        Additions.Global.MoveSpeedDown WalkSlow = new SagaMap.Skill.Additions.Global.MoveSpeedDown(null, dActor, 6000);
                                        ApplyAddition(dActor, WalkSlow);
                                        break;
                                    case 4:
                                        Additions.Global.Confuse Confuse = new Additions.Global.Confuse(null, dActor, 3000);
                                        ApplyAddition(dActor, Confuse);
                                        break;
                                    case 5:
                                        Additions.Global.Stiff debuff = new Additions.Global.Stiff(null, dActor, 2000);
                                        ApplyAddition(dActor, debuff);
                                        break;
                                }
                            }
                        }
                    }
                    //迷惑武器（エンチャントウエポン）
                    if (sActor.Status.Additions.ContainsKey("EnchantWeapon"))
                    {

                        Additions.Global.DefaultBuff skill = (Additions.Global.DefaultBuff)sActor.Status.Additions["EnchantWeapon"];
                        int SkillLevel = skill.skill.Level;
                        switch (SkillLevel)
                        {
                            case 1:
                                //附加遲緩狀態
                                if (SkillHandler.Instance.CanAdditionApply(sActor, dActor, DefaultAdditions.鈍足, 5))
                                {
                                    Additions.Global.MoveSpeedDown WalkSlow = new SagaMap.Skill.Additions.Global.MoveSpeedDown(null, dActor, 6000);
                                    ApplyAddition(dActor, WalkSlow);
                                }
                                break;
                            case 2:
                                //附加冰冻状态
                                if (SkillHandler.Instance.CanAdditionApply(sActor, dActor, DefaultAdditions.Frosen, 10))
                                {
                                    Additions.Global.Freeze freee = new SagaMap.Skill.Additions.Global.Freeze(null, dActor, 4000);
                                    ApplyAddition(dActor, freee);
                                }
                                break;
                            case 3:
                                //附加暈眩狀態
                                if (SkillHandler.Instance.CanAdditionApply(sActor, dActor, DefaultAdditions.Stun, 15))
                                {
                                    Additions.Global.Stun stun = new SagaMap.Skill.Additions.Global.Stun(null, dActor, 2000);
                                    ApplyAddition(dActor, stun);
                                }
                                break;
                        }
                    }
                }
            }
        }
        public int TryCast(Actor sActor, Actor dActor, SkillArg arg)
        {
            if (skillHandlers.ContainsKey(arg.skill.ID))
            {
                if (sActor.type == ActorType.PC)
                    return skillHandlers[arg.skill.ID].TryCast((ActorPC)sActor, dActor, arg);
                else
                    return 0;
            }
            else
                return 0;
        }


        public void SetNextComboSkill(Actor actor, uint id)
        {
            if (actor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)actor;
                Network.Client.MapClient.FromActorPC(pc).nextCombo.Add(id);
            }
        }

        public void SkillCast(Actor sActor, Actor dActor, SkillArg arg)
        {
            arg.sActor = sActor.ActorID;
            if (arg.dActor != 0xFFFFFFFF)
                arg.dActor = dActor.ActorID;
            if (skillHandlers.ContainsKey(arg.skill.ID))
            {
                if (arg.result == 0)
                    skillHandlers[arg.skill.ID].Proc(sActor, dActor, arg, arg.skill.Level);
                if (arg.affectedActors.Count == 0 && arg.dActor != arg.sActor && arg.dActor != 0 && arg.dActor != 0xffffffff)
                {
                    arg.affectedActors.Add(dActor);
                    arg.Init();
                }
            }
            else if (MobskillHandlers.ContainsKey(arg.skill.ID))
            {
                if (arg.result == 0)
                    MobskillHandlers[arg.skill.ID].Proc(sActor, dActor, arg, arg.skill.Level);
                if (arg.affectedActors.Count == 0 && arg.dActor != arg.sActor && arg.dActor != 0 && arg.dActor != 0xffffffff)
                {
                    arg.affectedActors.Add(dActor);
                    arg.Init();
                }
            }
            else
            {
                arg.affectedActors.Add(dActor);
                arg.Init();
                Logger.ShowWarning("No defination for skill:" + arg.skill.Name + "(ID:" + arg.skill.ID + ")", null);
            }
        }

        private byte GetComboCount(Actor actor)
        {
            if (actor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)actor;
                byte combo = 1;

                if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.RIGHT_HAND))
                {
                    Item item = pc.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND];
                    switch (item.BaseData.itemType)
                    {
                        case ItemType.DUALGUN:
                        case ItemType.CLAW:
                            combo = 2;
                            break;
                        default:
                            combo = 1;
                            break;
                    }
                }
                else
                    combo = 1;

                if (Global.Random.Next(0, 99) < actor.Status.combo_rate_skill)
                    combo = (byte)actor.Status.combo_skill;
                return combo;
            }
            else
                return 1;
        }

        public void CastPassiveSkills(ActorPC pc)
        {
            PC.StatusFactory.Instance.CalcStatus(pc);
            List<string> list = pc.Status.Additions.Keys.ToList();
            foreach (string i in list)
            {
                if (pc.Status.Additions[i].GetType() == typeof(Skill.Additions.Global.DefaultPassiveSkill))
                {
                    RemoveAddition(pc, pc.Status.Additions[i]);
                }
            }

            foreach (SagaDB.Skill.Skill i in pc.Skills.Values)
            {
                if (i.BaseData.active == false)
                {
                    if (skillHandlers.ContainsKey(i.ID))
                    {
                        SkillArg arg = new SkillArg();
                        arg.skill = i;
                        skillHandlers[i.ID].Proc(pc, pc, arg, i.Level);
                    }
                }
            }

            if (!pc.Rebirth)
            {
                foreach (SagaDB.Skill.Skill i in pc.Skills2.Values)
                {
                    if (i.BaseData.active == false)
                    {
                        if (skillHandlers.ContainsKey(i.ID))
                        {
                            SkillArg arg = new SkillArg();
                            arg.skill = i;
                            skillHandlers[i.ID].Proc(pc, pc, arg, i.Level);
                        }
                    }
                }

                foreach (SagaDB.Skill.Skill i in pc.SkillsReserve.Values)
                {
                    if (i.BaseData.active == false)
                    {
                        if (skillHandlers.ContainsKey(i.ID))
                        {
                            SkillArg arg = new SkillArg();
                            arg.skill = i;
                            skillHandlers[i.ID].Proc(pc, pc, arg, i.Level);
                        }
                    }
                }
            }
            else
            {
                foreach (SagaDB.Skill.Skill i in pc.Skills2_1.Values)
                {
                    if (i.BaseData.active == false)
                    {
                        if (skillHandlers.ContainsKey(i.ID))
                        {
                            SkillArg arg = new SkillArg();
                            arg.skill = i;
                            skillHandlers[i.ID].Proc(pc, pc, arg, i.Level);
                        }
                    }
                }
                foreach (SagaDB.Skill.Skill i in pc.Skills2_2.Values)
                {
                    if (i.BaseData.active == false)
                    {
                        if (skillHandlers.ContainsKey(i.ID))
                        {
                            SkillArg arg = new SkillArg();
                            arg.skill = i;
                            skillHandlers[i.ID].Proc(pc, pc, arg, i.Level);
                        }
                    }
                }
                foreach (SagaDB.Skill.Skill i in pc.Skills3.Values)
                {
                    if (i.BaseData.active == false)
                    {
                        if (skillHandlers.ContainsKey(i.ID))
                        {
                            SkillArg arg = new SkillArg();
                            arg.skill = i;
                            skillHandlers[i.ID].Proc(pc, pc, arg, i.Level);
                        }
                    }
                }
            }
            PC.StatusFactory.Instance.CalcStatus(pc);
        }

        public void CheckBuffValid(ActorPC pc)
        {
            List<string> list = pc.Status.Additions.Keys.ToList();
            foreach (string i in list)
            {
                if (i == null)
                    continue;
                if (pc.Status.Additions[i].GetType() == typeof(Skill.Additions.Global.DefaultBuff))
                {
                    Additions.Global.DefaultBuff buff = (SagaMap.Skill.Additions.Global.DefaultBuff)pc.Status.Additions[i];
                    int result;
                    if (buff.OnCheckValid != null)
                    {
                        buff.OnCheckValid(pc, pc, out result);
                        if (result != 0)
                        {
                            RemoveAddition(pc, pc.Status.Additions[i]);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Apply a addition to an actor
        /// </summary>
        /// <param name="actor">Actor which the addition should be applied to</param>
        /// <param name="addition">Addition to be applied</param>
        public static void ApplyAddition(Actor actor, Addition addition)
        {
            if (!addition.Enabled) return;
            if (actor.Status.Additions.ContainsKey(addition.Name))
            {
                Addition oldaddition = actor.Status.Additions[addition.Name];
                if (oldaddition.Activated)
                    oldaddition.AdditionEnd();
                if (addition.IfActivate)
                {
                    addition.AdditionStart();
                    addition.StartTime = DateTime.Now;
                    addition.Activated = true;
                }
                bool blocked = ClientManager.Blocked;
                if (!blocked)
                    ClientManager.EnterCriticalArea();

                actor.Status.Additions.Remove(addition.Name);
                actor.Status.Additions.Add(addition.Name, addition);

                if (!blocked)
                    ClientManager.LeaveCriticalArea();
            }
            else
            {
                if (addition.IfActivate)
                {
                    addition.AdditionStart();
                    addition.StartTime = DateTime.Now;
                    addition.Activated = true;
                }
                bool blocked = ClientManager.Blocked;
                if (!blocked)
                    ClientManager.EnterCriticalArea();

                actor.Status.Additions.Add(addition.Name, addition);

                if (!blocked)
                    ClientManager.LeaveCriticalArea();
            }
        }

        public static void RemoveAddition(Actor actor, string name)
        {
            bool blocked = ClientManager.Blocked;
            if (!blocked)
                ClientManager.EnterCriticalArea();
            if (actor.Status.Additions.ContainsKey(name))
                RemoveAddition(actor, actor.Status.Additions[name]);
            if (!blocked)
                ClientManager.LeaveCriticalArea();
        }

        public static void RemoveAddition(Actor actor, Addition addition)
        {
            bool blocked = ClientManager.Blocked;
            if (!blocked)
                ClientManager.EnterCriticalArea();
            RemoveAddition(actor, addition, false);
            if (!blocked)
                ClientManager.LeaveCriticalArea();
        }

        public static void RemoveAddition(Actor actor, Addition addition, bool removeOnly)
        {
            bool blocked = ClientManager.Blocked;
            if (!blocked)
                ClientManager.EnterCriticalArea();
            if (actor.Status == null)
                return;
            if (actor.Status.Additions.ContainsKey(addition.Name))
            {
                if (addition.Activated && !removeOnly)
                {
                    addition.AdditionEnd();
                }
                addition.Activated = false;
                actor.Status.Additions.Remove(addition.Name);
            }
            if (!blocked)
                ClientManager.LeaveCriticalArea();
        }
        public void PushBack(Actor ori, Actor dest, int step)
        {
            PushBack(ori, dest, step, 1200);
        }
        public void PushBack(Actor ori, Actor dest, int step, ushort speed, MoveType moveType = MoveType.RUN)
        {
            Map map = Manager.MapManager.Instance.GetMap(ori.MapID);
            if (dest.type == ActorType.MOB)
            {
                SagaMap.ActorEventHandlers.MobEventHandler eh = (SagaMap.ActorEventHandlers.MobEventHandler)dest.e;
                if (eh.AI.Mode.Symbol || eh.AI.Mode.SymbolTrash)
                    return;
            }
            byte x = SagaLib.Global.PosX16to8(dest.X, map.Width);
            byte y = SagaLib.Global.PosY16to8(dest.Y, map.Height);
            int deltaX = x - SagaLib.Global.PosX16to8(ori.X, map.Width);
            int deltaY = y - SagaLib.Global.PosY16to8(ori.Y, map.Height);
            if (deltaX != 0)
                deltaX /= Math.Abs(deltaX);
            if (deltaY != 0)
                deltaY /= Math.Abs(deltaY);
            for (int i = 0; i < step; i++)
            {
                x = (byte)(x + deltaX);
                y = (byte)(y + deltaY);
                if (x >= map.Width || y >= map.Height || map.Info.walkable[x, y] != 2)
                {
                    x = (byte)(x - deltaX);
                    y = (byte)(y - deltaY);
                    break;
                }
            }
            short[] pos = new short[2];
            pos[0] = SagaLib.Global.PosX8to16(x, map.Width);
            pos[1] = SagaLib.Global.PosY8to16(y, map.Height);
            if (moveType != MoveType.RUN)
                map.MoveActor(Map.MOVE_TYPE.START, dest, pos, speed, speed, true, moveType);
            else
                map.MoveActor(Map.MOVE_TYPE.START, dest, pos, speed, speed, true);
            if (dest.type == ActorType.MOB)
            {
                ActorEventHandlers.MobEventHandler mob = (ActorEventHandlers.MobEventHandler)dest.e;
                mob.AI.OnPathInterupt();
            }
            if (dest.type == ActorType.PET || dest.type == ActorType.SHADOW)
            {
                ActorEventHandlers.PetEventHandler mob = (ActorEventHandlers.PetEventHandler)dest.e;
                mob.AI.OnPathInterupt();
            }
        }

        public void JumpBack(Actor ori, int step, ushort speed, MoveType moveType = MoveType.RUN)
        {
            Map map = Manager.MapManager.Instance.GetMap(ori.MapID);
            byte OutX, OutY;
            SkillHandler.Instance.GetTFrontPos(map, ori, out OutX, out OutY);
            byte x = SagaLib.Global.PosX16to8(ori.X, map.Width);
            byte y = SagaLib.Global.PosY16to8(ori.Y, map.Height);
            int deltaX = x - OutX;
            int deltaY = y - OutY;
            if (deltaX != 0)
                deltaX /= Math.Abs(deltaX);
            if (deltaY != 0)
                deltaY /= Math.Abs(deltaY);
            for (int i = 0; i < step; i++)
            {
                x = (byte)(x + deltaX);
                y = (byte)(y + deltaY);
                if (x >= map.Width || y >= map.Height || map.Info.walkable[x, y] != 2)
                {
                    x = (byte)(x - deltaX);
                    y = (byte)(y - deltaY);
                    break;
                }
            }
            short[] pos = new short[2];
            pos[0] = SagaLib.Global.PosX8to16(x, map.Width);
            pos[1] = SagaLib.Global.PosY8to16(y, map.Height);
            if (moveType != MoveType.RUN)
                map.MoveActor(Map.MOVE_TYPE.START, ori, pos, speed, speed, true, moveType);
            else
                map.MoveActor(Map.MOVE_TYPE.START, ori, pos, speed, speed, true);

        }

        /// <summary>
        /// 检查技能是否符合装备条件
        /// </summary>
        /// <param name="actor"></param>
        /// <param name="skill"></param>
        /// <returns></returns>
        public bool CheckSkillCanCastForWeapon(ActorPC pc, SkillArg arg)
        {
            if (arg.skill.BaseData.equipFlag.Value == 0)
                return true;

            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.RIGHT_HAND))
            {
                if (arg.skill.BaseData.equipFlag.Test((EquipFlags)Enum.Parse(typeof(EquipFlags), pc.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].BaseData.itemType.ToString())))
                    return true;
            }
            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.LEFT_HAND))
            {
                if (arg.skill.BaseData.equipFlag.Test((EquipFlags)Enum.Parse(typeof(EquipFlags), pc.Inventory.Equipments[EnumEquipSlot.LEFT_HAND].BaseData.itemType.ToString())))
                    return true;
            }
            if (arg.skill.BaseData.equipFlag.Test(EquipFlags.HAND))
                return true;
            return false;
        }

        /// <summary>
        /// 返回范围内可被攻击的对象
        /// </summary>
        /// <param name="caster">实际攻击者</param>
        /// <param name="actor">计算范围的实体</param>
        /// <param name="range">范围</param>
        /// <returns>可被攻击的对象</returns>
        public List<Actor> GetActorsAreaWhoCanBeAttackedTargets(Actor caster, Actor actor, short range)
        {
            List<Actor> actors = new List<Actor>();
            Map map = Manager.MapManager.Instance.GetMap(caster.MapID);
            return GetVaildAttackTarget(caster, map.GetActorsArea(actor, range, false));
        }

        /// <summary>
        /// 返回范围内可被攻击的对象
        /// </summary>
        /// <param name="sActor">攻击者</param>
        /// <param name="range">范围</param>
        /// <returns>可被攻击的对象</returns>
        public List<Actor> GetActorsAreaWhoCanBeAttackedTargets(Actor sActor, short range)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            return GetVaildAttackTarget(sActor, map.GetActorsArea(sActor, range, false));
        }

        /// <summary>
        /// 返回可攻击的actors
        /// </summary>
        /// <param name="sActor">攻击者</param>
        /// <param name="dActors">被攻击者们</param>
        /// <returns>可攻击的actors</returns>
        public List<Actor> GetVaildAttackTarget(Actor sActor, List<Actor> dActors)
        {
            if (dActors.Count < 1) return dActors;
            List<Actor> actors = new List<Actor>();
            foreach (var item in dActors)
            {
                if (CheckValidAttackTarget(sActor, item))
                {
                    actors.Add(item);
                }
            }
            return actors;
        }

        /// <summary>
        /// 检查是否可攻击
        /// </summary>
        /// <param name="sActor">攻击者</param>
        /// <param name="dActor">被攻击者</param>
        /// <returns></returns>
        public bool CheckValidAttackTarget(Actor sActor, Actor dActor)
        {
            if (sActor == dActor)
                return false;
            if (sActor == null || dActor == null)
                return false;
            if (dActor.type == ActorType.PC)
            {
                if (!((ActorPC)dActor).Online)
                    return false;
            }
            if (dActor.type == ActorType.ITEM)
                return false;
            if (dActor.Buff.Dead)
                return false;
            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                switch (dActor.type)
                {
                    case ActorType.MOB:
                        ActorEventHandlers.MobEventHandler eh = (ActorEventHandlers.MobEventHandler)dActor.e;
                        if (eh.AI.Mode.Symbol)
                            return false;
                        return true;
                    case ActorType.ITEM:
                    case ActorType.SKILL:
                        return false;
                    case ActorType.PC:
                        {
                            //Logger.ShowInfo("skillhandler");
                            ActorPC target = (ActorPC)dActor;
                            if ((pc.Mode == PlayerMode.COLISEUM_MODE && target.Mode == PlayerMode.COLISEUM_MODE) ||
                                (pc.Mode == PlayerMode.WRP && target.Mode == PlayerMode.WRP) ||
                                (pc.Mode == PlayerMode.KNIGHT_WAR && target.Mode == PlayerMode.KNIGHT_WAR) ||
                                ((pc.Mode == PlayerMode.KNIGHT_EAST || pc.Mode == PlayerMode.KNIGHT_FLOWER || pc.Mode == PlayerMode.KNIGHT_NORTH
                                || pc.Mode == PlayerMode.KNIGHT_ROCK || pc.Mode == PlayerMode.KNIGHT_SOUTH || pc.Mode == PlayerMode.KNIGHT_WEST)
                                && (target.Mode == PlayerMode.KNIGHT_EAST || target.Mode == PlayerMode.KNIGHT_FLOWER || target.Mode == PlayerMode.KNIGHT_NORTH
                                || target.Mode == PlayerMode.KNIGHT_ROCK || target.Mode == PlayerMode.KNIGHT_SOUTH || target.Mode == PlayerMode.KNIGHT_WEST)
                                ))
                            {
                                if ((pc.Mode == PlayerMode.KNIGHT_EAST || pc.Mode == PlayerMode.KNIGHT_FLOWER || pc.Mode == PlayerMode.KNIGHT_NORTH
                                || pc.Mode == PlayerMode.KNIGHT_ROCK || pc.Mode == PlayerMode.KNIGHT_SOUTH || pc.Mode == PlayerMode.KNIGHT_WEST)
                                && (target.Mode == PlayerMode.KNIGHT_EAST || target.Mode == PlayerMode.KNIGHT_FLOWER || target.Mode == PlayerMode.KNIGHT_NORTH
                                || target.Mode == PlayerMode.KNIGHT_ROCK || target.Mode == PlayerMode.KNIGHT_SOUTH || target.Mode == PlayerMode.KNIGHT_WEST)
                                )
                                {
                                    //Logger.ShowInfo("skillhandler2");
                                    if (pc.Mode == target.Mode)
                                        return false;
                                }
                                //Logger.ShowInfo("skillhandler3");
                                if ((pc.Party == target.Party) && pc.Party != null)
                                    return false;
                                else
                                {
                                    if (target.PossessionTarget == 0)
                                        return true;
                                    else
                                        return false;
                                }
                                //Logger.ShowInfo("skillhandler4");
                            }
                            else
                                return false;
                        }
                    case ActorType.PET:
                        {
                            ActorPet pet = (ActorPet)dActor;
                            if ((pc.Mode == PlayerMode.COLISEUM_MODE && pet.Owner.Mode == PlayerMode.COLISEUM_MODE) ||
                               (pc.Mode == PlayerMode.WRP && pet.Owner.Mode == PlayerMode.WRP) ||
                               (pc.Mode == PlayerMode.KNIGHT_WAR && pet.Owner.Mode == PlayerMode.KNIGHT_WAR))
                            {
                                if (pc.Party == pet.Owner.Party)
                                    return false;
                                else
                                    return true;
                            }
                            else
                                return false;
                        }
                    case ActorType.SHADOW:
                        {
                            ActorShadow pet = (ActorShadow)dActor;
                            if ((pc.Mode == PlayerMode.COLISEUM_MODE && pet.Owner.Mode == PlayerMode.COLISEUM_MODE) ||
                               (pc.Mode == PlayerMode.WRP && pet.Owner.Mode == PlayerMode.WRP) ||
                               (pc.Mode == PlayerMode.KNIGHT_WAR && pet.Owner.Mode == PlayerMode.KNIGHT_WAR))
                            {
                                if (pc.Party == pet.Owner.Party)
                                    return false;
                                else
                                    return true;
                            }
                            else
                                return false;
                        }
                }
            }
            else if (sActor.type == ActorType.MOB)
            {
                bool isSlaveOfPc = false;
                ActorEventHandlers.MobEventHandler eh = (SagaMap.ActorEventHandlers.MobEventHandler)sActor.e;

                if (eh.AI.Master != null)
                {
                    if (eh.AI.Master.type == ActorType.PC)
                        isSlaveOfPc = true;
                    if (dActor.type == ActorType.MOB)
                    {
                        ActorEventHandlers.MobEventHandler deh = (SagaMap.ActorEventHandlers.MobEventHandler)dActor.e;
                        if (deh.AI.Master != null)
                        {
                            if (deh.AI.Master.ActorID == eh.AI.Master.ActorID)
                                return false;
                        }
                    }
                }
                if (!isSlaveOfPc)
                {
                    switch (dActor.type)
                    {
                        case ActorType.PC:
                            ActorPC pc = (ActorPC)dActor;
                            if (pc.PossessionTarget != 0)
                                return false;
                            else
                                return true;
                        case ActorType.PET:
                        case ActorType.SHADOW:
                            return true;
                        case ActorType.MOB:
                            eh = (SagaMap.ActorEventHandlers.MobEventHandler)dActor.e;
                            if (eh.AI.Mode.Symbol)
                                return true;
                            else
                                return false;
                        default:
                            return false;
                    }
                }
                else
                {
                    switch (dActor.type)
                    {
                        case ActorType.MOB:
                            return true;
                        default:
                            return false;
                    }
                }
            }
            else if (sActor.type == ActorType.PET)
            {
                switch (dActor.type)
                {
                    case ActorType.MOB:
                        return true;
                    case ActorType.PC:
                    case ActorType.PET:
                        return false;
                }
            }
            return false;
        }
    }
}

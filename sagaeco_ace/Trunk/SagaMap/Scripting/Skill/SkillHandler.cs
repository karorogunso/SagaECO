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
                    if (actor.Tasks["SkillCast"].Activated())
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
       /// <summary>
       /// 附加圣印
       /// </summary>
       /// <param name="dActor">目标</param>
        public void Seals(Actor sActor,Actor dActor)
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
                    Additions.Global.Seals Seals = new Additions.Global.Seals(null, dActor, 15000);
                    ApplyAddition(dActor, Seals);
                }
            }
        }
        /// <summary>
        /// 恢复HP、SP、MP
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="value">值</param>
        public void Heal(Actor actor, int hp, int mp, int sp)
        {
            List<int> hpl = new List<int>();
            List<int> mpl = new List<int>();
            List<int> spl = new List<int>();
            hpl.Add(hp);
            mpl.Add(mp);
            spl.Add(sp);
            Heal(actor, hpl, mpl, spl, 1);
        }
        public void Heal(Actor actor, List<int> hp, List<int> mp, List<int> sp,byte count)
        {
            Heal(actor, hp, mp, sp, count,new SkillArg());
        }
        public void Heal(Actor actor, List<int> hp, List<int> mp, List<int> sp, byte count, SkillArg skill)
        {
            try
            {
                skill.sActor = actor.ActorID;
                skill.dActor = actor.ActorID;
                skill.hp = hp;
                skill.mp = mp;
                skill.sp = sp;
                skill.argType = SkillArg.ArgType.Actor_Active;
                skill.flag = new List<AttackFlag>();
                for (int i = 0; i < count; i++)
                {
                    skill.affectedActors.Add(actor);
                    skill.flag.Add((AttackFlag)0);
                    if (hp[i] < 0)
                        skill.flag[i] |= AttackFlag.HP_DAMAGE;
                    if (mp[i] < 0)
                        skill.flag[i] |= AttackFlag.MP_DAMAGE;
                    if (sp[i] < 0)
                        skill.flag[i] |= AttackFlag.SP_DAMAGE;
                    if (hp[i] > 0)
                        skill.flag[i] |= AttackFlag.HP_HEAL;
                    if (mp[i] > 0)
                        skill.flag[i] |= AttackFlag.MP_HEAL;
                    if (sp[i] > 0)
                        skill.flag[i] |= AttackFlag.SP_HEAL;
                    Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, skill, actor, true);
                    actor.HP += (uint)hp[i];
                    if (actor.HP < hp[i])//死亡
                    {
                        actor.HP = 0;
                    }
                    
                    if (actor.HP > actor.MaxHP)
                    {
                        actor.HP = actor.MaxHP;
                    }
                    actor.MP += (uint)mp[i];
                    if (actor.MP > actor.MaxMP)
                    {
                        actor.MP = actor.MaxMP;
                    }
                    actor.SP += (uint)sp[i];
                    if (actor.SP > actor.MaxSP)
                    {
                        actor.SP = actor.MaxSP;
                    }

                    Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, actor, true);
                }

            }
            catch (Exception ex)
            {
                SagaLib.Logger.ShowError(ex);
            }
        }
        /// <summary>
        /// 武器装备破损
        /// </summary>
        /// <param name="pc">玩家</param>
        public void WeaponWorn(ActorPC pc)
        {
            if (Global.Random.Next(0, 6000) < 2)
            {
                if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND))
                {
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
            int combo = GetComboCount(sActor);
            arg.sActor = sActor.ActorID;
            arg.dActor = dActor.ActorID;
            for (int i = 0; i < combo; i++)
            {
                arg.affectedActors.Add(dActor);
            }
            arg.type = sActor.Status.attackType;
            arg.delayRate = 1f + ((float)combo / 2); 
            PhysicalAttack(sActor, arg.affectedActors, arg, Elements.Neutral, 1f);
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
                                    Additions.Global.鈍足 WalkSlow = new SagaMap.Skill.Additions.Global.鈍足(null, dActor, 6000);
                                    ApplyAddition(dActor, WalkSlow);
                                    break;
                                case 4:
                                    Additions.Global.Confuse Confuse = new Additions.Global.Confuse(null, dActor, 3000);
                                    ApplyAddition(dActor, Confuse);
                                    break;
                                case 5:
                                    Additions.Global.硬直 debuff = new Additions.Global.硬直(null, dActor, 2000);
                                    ApplyAddition(dActor, debuff);
                                    break;
                            }
                        }
                    }
                }
                if (sActor.Status.Additions.ContainsKey("EnchantWeapon"))//迷惑武器（エンチャントウエポン）
                {
                    if (!isBossMob(dActor))
                    {
                        Additions.Global.DefaultBuff skill = (Additions.Global.DefaultBuff)sActor.Status.Additions["EnchantWeapon"];
                        int SkillLevel = skill.skill.Level;
                        switch (SkillLevel)
                        {
                            case 1:
                                //附加遲緩狀態
                                if (SagaLib.Global.Random.Next(0, 99) < 5)
                                {
                                    Additions.Global.鈍足 WalkSlow = new SagaMap.Skill.Additions.Global.鈍足(null, dActor, 6000);
                                    ApplyAddition(dActor, WalkSlow);
                                }
                                break;
                            case 2:
                                //附加冰冻状态
                                if (SagaLib.Global.Random.Next(0, 99) < 10)
                                {
                                    Additions.Global.Freeze freee = new SagaMap.Skill.Additions.Global.Freeze(null, dActor, 4000);
                                    ApplyAddition(dActor, freee);
                                }
                                break;
                            case 3:
                                //附加暈眩狀態
                                if (SagaLib.Global.Random.Next(0, 99) < 15)
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
        public void CriAttack(Actor sActor, Actor dActor, SkillArg arg)
        {

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
                skillHandlers[arg.skill.ID].Proc(sActor, dActor, arg, arg.skill.Level);
                if (arg.affectedActors.Count == 0 && arg.dActor != 0)
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
            if (actor.Status == null)
                return;
            if (actor.Status.Additions.ContainsKey(addition.Name))
            {
                actor.Status.Additions.Remove(addition.Name);
                if (addition.Activated && !removeOnly)
                {
                    addition.AdditionEnd();
                }
                addition.Activated = false;
            }
        }
        public void PushBack(Actor ori, Actor dest, int step)
        {
            PushBack(ori, dest, step, 2500);
        }
        public void PushBack(Actor ori, Actor dest, int step, ushort speed)
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

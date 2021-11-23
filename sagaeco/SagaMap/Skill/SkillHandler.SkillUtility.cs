using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Item;

using SagaDB;
using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.SkillDefinations;
using SagaMap.Network.Client;
using SagaMap.Mob;
using SagaMap.ActorEventHandlers;
using SagaDB.Mob;
using SagaDB.Skill;

using SagaMap.Manager;
namespace SagaMap.Skill
{
    public partial class SkillHandler : Singleton<SkillHandler>
    {
        //放置Skill定義中所需的共通Function
        //Place the common functions which will used by SkillDefinations

        #region Utility
        /// <summary>
        /// 让Actor立刻释放技能
        /// </summary>
        /// <param name="caster"></param>
        /// <param name="target"></param>
        /// <param name="skillID"></param>
        /// <param name="level"></param>
        public void PutSkill(Actor caster, Actor target, ushort skillID, byte level, byte x = 0, byte y = 0, bool useMPSP = false,bool sendproxy = true)
        {
            SagaDB.Skill.Skill skill = SkillFactory.Instance.GetSkill(skillID, level);
            SkillArg arg = new SkillArg()
            {
                sActor = caster.ActorID,
                dActor = target.ActorID,
                useMPSP = useMPSP,
                skill = skill,
                x = x,
                y = y,
                argType = SkillArg.ArgType.Active
            };
            SkillCast(caster, target, arg);

            Map map = GetActorMap(caster);

            if (sendproxy)
            {
                if (arg.affectedActors.Count < 32)
                    map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, arg, caster, true);
                else
                    for (int i = 0; i < arg.affectedActors.Count; i += 32)
                        map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, arg.SplitPart(i, 32), caster, true);
            }
        }
        /// <summary>
        /// 取得被憑依的角色
        /// </summary>
        /// <param name="sActor">角色</param>
        /// <returns>被憑依者</returns>
        public ActorPC GetPossesionedActor(ActorPC sActor)
        {
            if (sActor.PossessionTarget == 0)
            {
                return sActor;
            }
            else
            {
                Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
                Actor act = map.GetActor(sActor.PossessionTarget);
                if (act != null)
                {
                    if (act.type == ActorType.PC)
                    {
                        return (ActorPC)act;
                    }
                }
                return sActor;
            }
        }

        /// <summary>
        /// 吸引怪物 
        /// </summary>
        /// <param name="sActor">施放技能者</param>
        /// <param name="dActor">目標</param>
        public void AttractMob(Actor sActor, Actor dActor)
        {
            AttractMob(sActor, dActor, 1000);
        }
        /// <summary>
        /// 吸引怪物
        /// </summary>
        /// <param name="sActor">施放技能者</param>
        /// <param name="dActor">目標</param>
        /// <param name="damage">給予的傷害</param>
        public void AttractMob(Actor sActor, Actor dActor, int damage)
        {
            if (dActor.type == ActorType.MOB)
            {
                ActorEventHandlers.MobEventHandler mob = (ActorEventHandlers.MobEventHandler)dActor.e;
                mob.AI.OnAttacked(sActor, damage);
                //mob.AI.DamageTable[sActor.ActorID] += damage;
            }
        }

        /// <summary>
        /// 判斷怪物是否為Boss
        /// </summary>
        /// <param name="mob"></param>
        /// <returns></returns>
        public bool isBossMob(Actor mob)
        {
            if (mob.type != ActorType.MOB)
            {
                return false;
            }
            else
            {
                return isBossMob((ActorMob)mob);
            }
        }
        /// <summary>
        /// 判斷怪物是否為Boss
        /// </summary>
        /// <param name="mob"></param>
        /// <returns></returns>
        public bool isBossMob(ActorMob mob)
        {
            return CheckMobType(mob, "boss");
        }

        /// <summary>
        /// 檢查是否可以被賦予狀態
        /// </summary>
        /// <param name="sActor">賦予者</param>
        /// <param name="dActor">目標</param>
        /// <param name="AdditionName">狀態名稱</param>
        /// <param name="rate">原始成功率</param>
        /// <returns>是否可被賦予</returns>
        public bool CanAdditionApply(Actor sActor, Actor dActor, string AdditionName, int rate)
        {
            //王不受狀態影響
            if (SkillHandler.Instance.isBossMob(dActor))
            {
                return false;
            }
            if (rate <= 0)
            {
                return false;
            }
            float newRate = (float)rate;
            //成功率下降(抗性)
            if (dActor.Status.Additions.ContainsKey(AdditionName + "Regi"))
            {
                newRate = newRate * 0.1f;//減少90%
            }
            if (sActor != dActor)
            {
                if (AdditionName == "Poison")
                {
                    // 提升放毒成功率（毒成功率上昇）
                    if (sActor.Status.Additions.ContainsKey("PoisonRateUp"))
                    {
                        newRate = newRate * 1.5f;//增加50%
                    }
                }
            }
            //成功率上升(魔法革命、提升異常狀態成功率)
            if (sActor.Status.Additions.ContainsKey("MagHitUpCircle"))
            {
                SagaMap.Skill.SkillDefinations.Sage.MagHitUpCircle.MagHitUpCircleBuff mhucb = (SagaMap.Skill.SkillDefinations.Sage.MagHitUpCircle.MagHitUpCircleBuff)sActor.Status.Additions["MagHitUpCircle"];
                newRate = newRate * mhucb.Rate;
            }
            if (sActor.Status.Additions.ContainsKey("AllRateUp"))
            {
                SagaMap.Skill.SkillDefinations.Cabalist.AllRateUp.AllRateUpBuff arub = (SagaMap.Skill.SkillDefinations.Cabalist.AllRateUp.AllRateUpBuff)sActor.Status.Additions["AllRateUp"];
                newRate = newRate * arub.Rate;
            }
            //是否可被賦予
            return (SagaLib.Global.Random.Next(0, 99) < newRate);
        }

        /// <summary>
        /// 檢查是否可以被賦予狀態
        /// </summary>
        /// <param name="sActor">賦予者</param>
        /// <param name="dActor">目標</param>
        /// <param name="theAddition">狀態類型</param>
        /// <param name="rate">原始成功率</param>
        /// <returns>是否可被賦予</returns>
        public bool CanAdditionApply(Actor sActor, Actor dActor, DefaultAdditions theAddition, int rate)
        {
            //怪物抗性
            if (sActor.Status.control_rate_bonus != 0)
                rate += sActor.Status.control_rate_bonus;
            if (dActor.type == ActorType.MOB)
            {
                ActorMob mob = (ActorMob)dActor;
                int value = (int)theAddition;
                if (value < 9)
                {
                    AbnormalStatus dStatus = (AbnormalStatus)Enum.ToObject(typeof(AbnormalStatus), value);
                    short regiValue = mob.AbnormalStatus[dStatus];
                    if (regiValue == 100)
                    {
                        return false;//完全抵抗
                    }
                    else
                    {
                        rate = (int)(rate * (1f - regiValue / 100));
                    }
                }
            }
            return CanAdditionApply(sActor, dActor, theAddition.ToString(), rate);
        }
        /// <summary>
        /// 返回异常状态持续时间，若返回0，则本次异常判定失败
        /// </summary>
        /// <param name="sActor">攻</param>
        /// <param name="dActor">受</param>
        /// <param name="baserate">基础几率</param>
        /// <param name="time">基础持续时间</param>
        /// <param name="resistance">异常状态的种类，影响持续时间，buff类请无视</param>
        /// <param name="fixedrate">固定几率,0-100，大于0代表无视命中率和回避率</param>
        /// <returns></returns>
        public int AdditionApply(Actor sActor, Actor dActor, int baserate, int time, 异常状态 resistance = 异常状态.无, int fixedrate = 0)
        {
            short res = 0;
            if (resistance != 异常状态.无)
            {
                int value = (int)resistance;
                AbnormalStatus dStatus = (AbnormalStatus)Enum.ToObject(typeof(AbnormalStatus), value);
                res = ((ActorMob)dActor).AbnormalStatus[dStatus];
            }
            return AdditionApply(sActor.Status.hit_magic, dActor.Status.hit_magic, baserate, time, res, fixedrate);
        }
        /// <summary>
        /// 返回异常状态持续时间，若返回0，则本次异常判定失败
        /// </summary>
        /// <param name="hit">命中率</param>
        /// <param name="avoid">回避率</param>
        /// <param name="baserate">基础几率</param>
        /// <param name="time">基础持续时间</param>
        /// <param name="resistance">异常状态数值，影响持续时间，buff类请无视</param>
        /// <param name="fixedrate">固定几率,0-100，大于0代表无视命中率和回避率</param>
        /// <returns></returns>
        public int AdditionApply(int hit, int avoid, int baserate, int time, int resistance, int fixedrate = 0)
        {
            int t = 0;
            int rand = SagaLib.Global.Random.Next(0, 99);
            double rate = baserate * (hit / (hit + 0.5 * avoid + 10) * 2);
            if (rand < rate || fixedrate > 0 && rand < fixedrate)
            {
                t = time * (255 - resistance) / 255;
            }
            return t;
        }
        /// <summary>
        /// 檢查裝備是否正確
        /// </summary>
        /// <param name="sActor">人物</param>
        /// <param name="ItemType">裝備種類</param>
        /// <returns></returns>
        public bool isEquipmentRight(Actor sActor, params SagaDB.Item.ItemType[] ItemType)
        {
            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND))
                {
                    foreach (SagaDB.Item.ItemType t in ItemType)
                    {
                        if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == t)
                        {
                            return true;
                        }
                    }
                    return false;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 計算道具數量
        /// </summary>
        /// <param name="pc">人物</param>
        /// <param name="itemID">道具ID</param>
        /// <returns>數量</returns>
        public int CountItem(ActorPC pc, uint itemID)
        {
            SagaDB.Item.Item item = pc.Inventory.GetItem(itemID, Inventory.SearchType.ITEM_ID);
            if (item != null)
            {
                return item.Stack;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 取走道具
        /// </summary>
        /// <param name="pc">人物</param>
        /// <param name="itemID">道具ID</param>
        /// <param name="count">數量</param>
        public void TakeItem(ActorPC pc, uint itemID, ushort count)
        {
            MapClient client = MapClient.FromActorPC(pc);
            Logger.LogItemLost(Logger.EventType.ItemNPCLost, pc.Name + "(" + pc.CharID + ")", "(" + itemID + ")",
                    string.Format("SkillTake Count:{0}", count), true);
            client.DeleteItemID(itemID, count, true);
        }

        /// <summary>
        /// 给予玩家指定个数的道具
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="itemID">道具ID</param>
        /// <param name="count">个数</param>
        /// <param name="identified">是否鉴定</param>
        public List<SagaDB.Item.Item> GiveItem(ActorPC pc, uint itemID, ushort count, bool identified)
        {
            MapClient client = MapClient.FromActorPC(pc);
            List<SagaDB.Item.Item> ret = new List<Item>();
            for (int i = 0; i < count; i++)
            {
                SagaDB.Item.Item item = ItemFactory.Instance.GetItem(itemID);
                item.Stack = 1;
                item.Identified = true;//免鉴定
                Logger.LogItemGet(Logger.EventType.ItemNPCGet, pc.Name + "(" + pc.CharID + ")", item.BaseData.name + "(" + item.ItemID + ")",
                    string.Format("SkillGive Count:{0}", item.Stack), true);
                client.AddItem(item, true);
                ret.Add(item);
            }
            return ret;
        }

        /// <summary>
        /// 產生自動使用技能的資訊
        /// </summary>
        /// <param name="skillID">技能ID</param>
        /// <param name="level">等級</param>
        /// <param name="delay">延遲</param>
        /// <returns>自動使用技能的資訊</returns>
        public AutoCastInfo CreateAutoCastInfo(uint skillID, byte level, int delay)
        {
            AutoCastInfo info = new AutoCastInfo();
            info.delay = delay;
            info.level = level;
            info.skillID = skillID;
            return info;
        }

        /// <summary>
        /// 產生自動使用技能的資訊
        /// </summary>
        /// <param name="skillID">技能ID</param>
        /// <param name="level">等級</param>
        /// <param name="delay">延遲</param>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        /// <returns>自動使用技能的資訊</returns>
        public AutoCastInfo CreateAutoCastInfo(uint skillID, byte level, int delay, byte x, byte y)
        {
            AutoCastInfo info = new AutoCastInfo();
            info.delay = delay;
            info.level = level;
            info.skillID = skillID;
            info.x = x;
            info.y = y;
            return info;
        }
        /// <summary>
        /// 給予固定傷害
        /// </summary>
        /// <param name="sActor">攻擊者</param>
        /// <param name="dActor">被攻擊者</param>
        /// <param name="arg">技能參數</param>
        /// <param name="element">屬性</param>
        /// <param name="Damage">傷害</param>
        public void FixAttack(Actor sActor, Actor dActor, SkillArg arg, Elements element, float Damage)
        {
            List<Actor> actors = new List<Actor>();
            actors.Add(dActor);
            this.FixAttack(sActor, actors, arg, element, Damage);
        }
        /// <summary>
        /// 給予固定傷害
        /// </summary>
        /// <param name="sActor">攻擊者</param>
        /// <param name="dActor">被攻擊者</param>
        /// <param name="arg">技能參數</param>
        /// <param name="element">屬性</param>
        /// <param name="Damage">傷害</param>
        public void FixAttack(Actor sActor, List<Actor> dActor, SkillArg arg, Elements element, float Damage)
        {
            this.MagicAttack(sActor, dActor, arg, DefType.IgnoreAll, element, 50, Damage, 0, true);
        }
        /// <summary>
        /// 取得搭档
        /// </summary>
        /// <param name="sActor">玩家</param>
        /// <returns>寵物</returns>
        public ActorPartner GetPartner(Actor sActor)
        {
            if (sActor.type != ActorType.PC)
            {
                return null;
            }
            ActorPC pc = (ActorPC)sActor;
            if (pc.Partner == null)
            {
                return null;
            }
            return pc.Partner;
        }
        /// <summary>
        /// 取得寵物
        /// </summary>
        /// <param name="sActor">玩家</param>
        /// <returns>寵物</returns>
        public ActorPet GetPet(Actor sActor)
        {
            if (sActor.type != ActorType.PC)
            {
                return null;
            }
            ActorPC pc = (ActorPC)sActor;
            if (pc.Pet == null)
            {
                return null;
            }
            return (ActorPet)pc.Pet;
        }
        /// <summary>
        /// 取得寵物AI
        /// </summary>
        /// <param name="sActor">玩家</param>
        /// <returns>寵物AI</returns>
        public MobAI GetMobAI(Actor sActor)
        {
            return GetMobAI(GetPet(sActor));
        }
        /// <summary>
        /// 取得寵物AI
        /// </summary>
        /// <param name="pet">寵物</param>
        /// <returns>寵物AI</returns>
        public MobAI GetMobAI(ActorPet pet)
        {
            if (pet == null)
            {
                return null;
            }
            if (pet.Ride)
            {
                return null;
            }
            PetEventHandler peh = (PetEventHandler)pet.e;
            return peh.AI;
        }
        /// <summary>
        /// 檢查怪物類型
        /// </summary>
        /// <param name="mob">怪物</param>
        /// <param name="LowerType">怪物類型</param>
        /// <returns>類型是否正確</returns>
        public bool CheckMobType(ActorMob mob, string MobType)
        {
            return mob.BaseData.mobType.ToString().ToLower().IndexOf(MobType.ToLower()) > -1;
        }
        /// <summary>
        /// 檢查怪物類型
        /// </summary>
        /// <param name="pet">怪物</param>
        /// <param name="type">怪物類型</param>
        /// <returns>類型是否正確</returns>
        public bool CheckMobType(ActorMob pet, params SagaDB.Mob.MobType[] type)
        {
            if (pet == null)
            {
                return false;
            }
            foreach (SagaDB.Mob.MobType t in type)
            {
                if (pet.BaseData.mobType == t)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 解除憑依
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="pos">部位(None=全部)</param>
        public void PossessionCancel(ActorPC pc, PossessionPosition pos)
        {
            if (pos == PossessionPosition.NONE)
            {
                PossessionCancel(pc, PossessionPosition.CHEST);
                PossessionCancel(pc, PossessionPosition.LEFT_HAND);
                PossessionCancel(pc, PossessionPosition.NECK);
                PossessionCancel(pc, PossessionPosition.RIGHT_HAND);
            }
            else
            {
                Packets.Client.CSMG_POSSESSION_CANCEL p = new SagaMap.Packets.Client.CSMG_POSSESSION_CANCEL();
                p.PossessionPosition = pos;
                MapClient.FromActorPC(pc).OnPossessionCancel(p);
            }
        }
        /// <summary>
        /// 改變玩家大小
        /// </summary>
        /// <param name="dActor">人物</param>
        /// <param name="playersize">大小</param>
        public void ChangePlayerSize(ActorPC dActor, uint playersize)
        {
            MapClient client = MapClient.FromActorPC(dActor);
            client.Character.Size = playersize;
            client.SendPlayerSizeUpdate();
        }
        /// <summary>
        /// 在对象位置处显示特效
        /// </summary>
        /// <param name="actor">对象</param>
        /// <param name="effectID">特效ID</param>
        public void ShowEffectByActor(Actor actor, uint effectID)
        {
            Map map = MapManager.Instance.GetMap(actor.MapID);
            ShowEffect(map, actor, SagaLib.Global.PosX16to8(actor.X, map.Width), SagaLib.Global.PosY16to8(actor.Y, map.Height), effectID);
        }
        /// <summary>
        /// 在对象位置处显示特效
        /// </summary>
        /// <param name="actor">对象</param>
        /// <param name="effectID">特效ID</param>
        public void ShowEffectOnActor(Actor actor, uint effectID, Actor caster = null)
        {
            Map map = MapManager.Instance.GetMap(actor.MapID);
            ShowEffect(map, actor, effectID,caster);
        }
        /// <summary>
        /// 在指定对象处显示特效
        /// </summary>
        /// <param name="map">map</param>
        /// <param name="target">对象</param>
        /// <param name="effectID">特效ID</param>
        public void ShowEffect(Map map, Actor target, uint effectID,Actor caster = null)
        {
            EffectArg arg = new EffectArg();
            arg.effectID = effectID;
            arg.actorID = target.ActorID;
            if (caster == null)
                caster = target;
            arg.caster = caster;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, arg, target, true);
        }
        /// <summary>
        /// 在指定坐标显示特效
        /// </summary>
        /// <param name="map">map</param>
        /// <param name="x">X坐标</param>
        /// <param name="y">Y坐标</param>
        /// <param name="effectID">特效ID</param>
        public void ShowEffect(Map map, Actor actor, byte x, byte y, uint effectID)
        {
            EffectArg arg = new EffectArg();
            arg.effectID = effectID;
            arg.actorID = 0xFFFFFFFF;
            arg.x = x;
            arg.y = y;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, arg, actor, true);
        }
        /// <summary>
        /// 在指定对象处显示特效
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="target">对象</param>
        /// <param name="effectID">特效ID</param>
        public void ShowEffect(ActorPC pc, Actor target, uint effectID)
        {
            EffectArg arg = new EffectArg();
            arg.effectID = effectID;
            arg.actorID = target.ActorID;
            MapClient client = GetMapClient(pc);
            client.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, arg, pc, true);
        }

        /// <summary>
        /// 在指定坐标显示特效
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="x">X坐标</param>
        /// <param name="y">Y坐标</param>
        /// <param name="effectID">特效ID</param>
        public void ShowEffect(ActorPC pc, byte x, byte y, uint effectID)
        {
            EffectArg arg = new EffectArg();
            arg.effectID = effectID;
            arg.actorID = 0xFFFFFFFF;
            arg.x = x;
            arg.y = y;
            MapClient client = GetMapClient(pc);
            client.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, arg, pc, true);
        }

        private MapClient GetMapClient(ActorPC pc)
        {
            ActorEventHandlers.PCEventHandler eh = (SagaMap.ActorEventHandlers.PCEventHandler)pc.e;
            return eh.Client;
        }

        #endregion

        /// <summary>
        /// 是否在範圍內
        /// </summary>
        /// <param name="sActor">來源Actor</param>
        /// <param name="dActor">目的Actor</param>
        /// <param name="Range">範圍</param>
        /// <returns>是否在範圍內</returns>
        public bool isInRange(Actor sActor, Actor dActor, short Range)
        {
            if ((Math.Abs(sActor.X - dActor.X) < Range) || (Math.Abs(sActor.Y - dActor.Y) < Range))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 移動道具
        /// </summary>
        /// <param name="item">道具</param>
        public void MoveItem(Actor dActor, Item item)
        {
            if (dActor.type != ActorType.PC)
            {
                return;
            }
            Packets.Client.CSMG_ITEM_MOVE p = new SagaMap.Packets.Client.CSMG_ITEM_MOVE();
            //p.InventoryID = 
            MapClient mc = MapClient.FromActorPC((ActorPC)dActor);
            mc.OnItemMove(p);
        }

        /// <summary>
        /// 傳送至某地圖
        /// </summary>
        /// <param name="dActor">目標玩家</param>
        /// <param name="MapID">地圖</param>
        /// <param name="x">X座標</param>
        /// <param name="y">Y座標</param>
        public void Warp(Actor dActor, uint MapID, byte x, byte y)
        {
            Map map = Manager.MapManager.Instance.GetMap(dActor.MapID);
            map.SendActorToMap(dActor, MapID, SagaLib.Global.PosX8to16(x, map.Width), SagaLib.Global.PosY8to16(y, map.Height));
        }

        /// <summary>
        /// 變身成怪物
        /// </summary>
        /// <param name="sActor">目標玩家</param>
        /// <param name="MobID">怪物ID (0為變回原形)</param>
        public void TranceMob(Actor sActor, uint MobID)
        {
            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                if (MobID == 0)
                {
                    pc.TranceID = 0;
                }
                else
                {
                    MobData mob = MobFactory.Instance.GetMobData(MobID);
                    pc.TranceID = mob.pictid;
                }
                MapClient client = MapClient.FromActorPC(pc);
                client.SendCharInfoUpdate();
            }
        }

        /// <summary>
        /// 判斷是否為騎寵
        /// </summary>
        /// <param name="mob"></param>
        /// <returns></returns>
        public bool IsRidePet(Actor mob)
        {
            if (mob.type != ActorType.PET)
            {
                return false;
            }
            else
            {
                ActorPet p = (ActorPet)mob;
                return p.BaseData.mobType.ToString().ToUpper().IndexOf("RIDE") > 1;
            }
        }

        /// <summary>
        /// Actor動作
        /// </summary>
        /// <param name="dActor"></param>
        /// <param name="motion"></param>
        public void NPCMotion(Actor dActor, MotionType motion)
        {
            Packets.Server.SSMG_CHAT_MOTION p = new SagaMap.Packets.Server.SSMG_CHAT_MOTION();
            p.ActorID = dActor.ActorID;
            p.Motion = motion;
            Map map = MapManager.Instance.GetMap(dActor.MapID);
            var actors = map.GetActorsArea(dActor, 10000, true);
            foreach (Actor act in actors)
            {
                if (act.type == ActorType.PC)
                {
                    ActorPC pc = (ActorPC)act;
                    MapClient.FromActorPC(pc).netIO.SendPacket(p);
                }
            }
        }

        /// <summary>
        /// 檢查DEM的右手裝備
        /// </summary>
        /// <param name="sActor"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool CheckDEMRightEquip(Actor sActor, ItemType type)
        {
            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                if (pc.Race == PC_RACE.DEM)
                {
                    var EQItems = pc.Inventory.GetContainer(SagaDB.Item.ContainerType.RIGHT_HAND2);
                    if (EQItems.Count > 0)
                    {
                        if (EQItems[0].BaseData.itemType == type)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        #region Enums
        public enum DefaultAdditions
        {
            Sleep = 3,
            Poison = 0,
            Stun = 8,
            Silence = 4,
            Stone = 1,
            Confuse = 6,
            鈍足 = 5,
            Frosen = 7,
            硬直 = 14,
            Paralyse = 2,
            CannotMove = 15
        }
        #endregion
        #region Enums
        public enum 异常状态
        {
            无 = -1,
            中毒 = 0,
            石化 = 1,
            麻痹 = 2,
            睡眠 = 3,
            沉默 = 4,
            迟钝 = 5,
            混乱 = 6,
            冻结 = 7,
            昏迷 = 8,
            灼伤 = 9,
        }
        #endregion
    }
}

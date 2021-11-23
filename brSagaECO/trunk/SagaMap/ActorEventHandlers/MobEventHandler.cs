using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaMap.Network.Client;

using SagaLib;
using SagaDB;
using SagaDB.Actor;
using SagaDB.Item;

using SagaMap.Manager;

namespace SagaMap.ActorEventHandlers
{
    public class MobEventHandler : ActorEventHandler
    {
        public ActorMob mob;
        public Mob.MobAI AI;
        Scripting.MobCallback currentCall;
        ActorPC currentPC;
        public event Scripting.MobCallback Dying;
        public event Scripting.MobCallback Attacking;
        public event Scripting.MobCallback Moving;
        public event Scripting.MobCallback Defending;
        public event Scripting.MobCallback SkillUsing;
        public event Scripting.MobCallback Updating;

        public MobEventHandler(ActorMob mob)
        {
            this.mob = mob;
            this.AI = new SagaMap.Mob.MobAI(mob);
        }

        #region ActorEventHandler Members

        public void OnActorAppears(Actor aActor)
        {
            if (!mob.VisibleActors.Contains(aActor.ActorID))
                mob.VisibleActors.Add(aActor.ActorID);
            if (aActor.type == ActorType.PC)
            {
                if (!this.AI.Activated)
                    this.AI.Start();
            }
            if (aActor.type == ActorType.SHADOW && this.AI.Hate.Count != 0)
            {
                if (!this.AI.Hate.ContainsKey(aActor.ActorID))
                    this.AI.Hate.Add(aActor.ActorID, this.mob.MaxHP);
            }
        }
        public void OnPlayerShopChange(Actor aActor)
        {

        }
        public void OnPlayerShopChangeClose(Actor aActor)
        {

        }
        public void OnActorChangeEquip(Actor sActor, MapEventArgs args)
        {

        }
        public void OnActorChat(Actor cActor, MapEventArgs args)
        {

        }
        public void OnActorDisappears(Actor dActor)
        {
            if (mob.VisibleActors.Contains(dActor.ActorID))
                mob.VisibleActors.Remove(dActor.ActorID);
            if (dActor.type == ActorType.PC)
            {
                if (this.AI.Hate.ContainsKey(dActor.ActorID))
                    this.AI.Hate.Remove(dActor.ActorID);
            }
        }
        public void OnActorSkillUse(Actor sActor, MapEventArgs args)
        {
            SkillArg arg = (SkillArg)args;
            AI.OnSeenSkillUse(arg);
            try
            {
                if (SkillUsing != null)
                {
                    if (AI.lastAttacker != null)
                    {
                        if (AI.lastAttacker.type == ActorType.PC)
                            RunCallback(SkillUsing, (ActorPC)AI.lastAttacker);
                        else
                            RunCallback(SkillUsing, null);
                    }
                    else
                        RunCallback(SkillUsing, null);
                }
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }

        }
        public void OnActorStartsMoving(Actor mActor, short[] pos, ushort dir, ushort speed)
        {

        }
        public void OnActorStartsMoving(Actor mActor, short[] pos, ushort dir, ushort speed, MoveType moveType)
        {
        }
        public void OnActorStopsMoving(Actor mActor, short[] pos, ushort dir, ushort speed)
        {
            try
            {
                if (Moving != null)
                {
                    if (AI.lastAttacker != null)
                    {
                        if (AI.lastAttacker.type == ActorType.PC)
                            RunCallback(Moving, (ActorPC)AI.lastAttacker);
                        else
                            RunCallback(Moving, null);
                    }
                    else
                        RunCallback(Moving, null);
                }
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
        }
        public void OnCreate(bool success)
        {

        }
        public void OnActorChangeEmotion(Actor aActor, MapEventArgs args)
        {

        }
        public void OnActorChangeMotion(Actor aActor, MapEventArgs args)
        {

        }
        public void OnActorChangeWaitType(Actor aActor) { }
        public void OnDelete()
        {
            AI.Pause();
        }
        public void OnCharInfoUpdate(Actor aActor)
        {

        }
        public void OnPlayerSizeChange(Actor aActor)
        {

        }
        bool checkDropSpecial()
        {
            if (this.AI.firstAttacker != null)
            {
                if (this.AI.firstAttacker.Status != null)
                {
                    foreach (Addition i in this.AI.firstAttacker.Status.Additions.Values)
                    {
                        if (i.GetType() == typeof(Skill.Additions.Global.Knowledge))
                        {
                            Skill.Additions.Global.Knowledge know = (Skill.Additions.Global.Knowledge)i;
                            if (know.MobTypes.Contains(this.mob.BaseData.mobType))
                                return true;
                        }
                    }
                }
                else
                    return false;
            }
            else
                return false;
            return false;
        }
        public void OnDie()
        {
            OnDie(true);
        }
        public void OnDie(bool loot)
        {
            this.mob.Buff.Dead = true;
            try
            {
                if (this.mob.Owner != null)
                    this.mob.Owner.Slave.Remove(this.mob);
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
            if (this.mob.Status.Additions.ContainsKey("Rebone"))
            {
                this.mob.Buff.Dead = false;

                this.mob.HP = this.mob.MaxHP;
                Skill.SkillHandler.RemoveAddition(this.mob, "Rebone");
                this.AI.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, this.mob, false);
                Skill.Additions.Global.Zombie zombie = new SagaMap.Skill.Additions.Global.Zombie(this.mob);
                Skill.SkillHandler.ApplyAddition(this.mob, zombie);
                this.mob.Status.undead = true;
                this.AI.DamageTable.Clear();
                this.AI.Hate.Clear();
                this.AI.firstAttacker = null;
            }
            else
            {
                try
                {
                    if (Dying != null)
                    {
                        if (AI.lastAttacker != null)
                        {
                            if (AI.lastAttacker.type == ActorType.PC)
                                RunCallback(Dying, (ActorPC)AI.lastAttacker);
                            else
                                RunCallback(Dying, null);
                        }
                        else
                            RunCallback(Dying, null);
                    }
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                }
                mob.ClearTaskAddition();
                this.AI.Pause();
                if (loot)
                {
                    //special drops

                    //boss掉心
                    if (Configuration.Instance.ActiveSpecialLoot)
                        if (mob.BaseData.mobType.ToString().Contains("BOSS") && AI.SpawnDelay >= 1800000)
                        {
                            if (Global.Random.Next(0, 10000) <= Configuration.Instance.BossSpecialLootRate)
                                for (int i = 0; i < Configuration.Instance.BossSpecialLootNum; i++)
                                    this.AI.map.AddItemDrop(Configuration.Instance.BossSpecialLootID, null, this.mob, false, false);
                        }
                        else
                            if (Global.Random.Next(0, 10000) <= Configuration.Instance.NomalMobSpecialLootRate && ((ActorEventHandlers.MobEventHandler)mob.e).AI.SpawnDelay != 0)
                                for (int i = 0; i < Configuration.Instance.NomalMobSpecialLootNum; i++)
                                    this.AI.map.AddItemDrop(Configuration.Instance.NomalMobSpecialLootID, null, this.mob, false, false);

                    //drops
                    int dropDeterminator = Global.Random.Next(0, 10000);
                    int baseValue = 0, maxVlaue = 0;
                    bool stamp = false;
                    bool special = false;
                    ActorPC owner = null;
                    if (mob.type == ActorType.MOB)
                    {
                        List<Actor> actors = MapManager.Instance.GetMap(mob.MapID).GetActorsArea(mob, 12700, false).Where(x => x.type == ActorType.PC && (x as ActorPC).Online).ToList();
                        ActorEventHandlers.MobEventHandler eh = (ActorEventHandlers.MobEventHandler)mob.e;
                        if (eh.AI.firstAttacker != null && eh.AI.firstAttacker.type == ActorType.PC)
                            owner = (ActorPC)eh.AI.firstAttacker;
                    }

                    //印章因为目前掉率全都是0,所以取了最小的万分之一
                    if (mob.BaseData.stampDrop != null)
                    {
                        if (Global.Random.Next(0, 9999) <= (mob.BaseData.stampDrop.Rate * Configuration.Instance.CalcStampDropRateForPC(owner)))
                        {
                            this.AI.map.AddItemDrop(mob.BaseData.stampDrop.ItemID, null, this.mob, false, false);
                            stamp = true;
                        }
                    }

                    //dropDeterminator = this.mob.BaseData.dropItems.Sum(x => x.Rate) + this.mob.BaseData.dropItemsSpecial.Sum(x => x.Rate);
                    //特殊掉落(知识掉落)
                    if ((!stamp || Configuration.Instance.MultipleDrop) && checkDropSpecial())
                    {
                        foreach (SagaDB.Mob.MobData.DropData i in this.mob.BaseData.dropItemsSpecial)
                        {
                            dropDeterminator = Global.Random.Next(0, 9999);
                            if (!Configuration.Instance.MultipleDrop)
                            {
                                maxVlaue = baseValue + (int)(i.Rate * Configuration.Instance.CalcSpecialDropRateForPC(owner) / 100.0f);
                                if (dropDeterminator >= baseValue && dropDeterminator < maxVlaue)
                                {
                                    this.AI.map.AddItemDrop(i.ItemID, i.TreasureGroup, this.mob, i.Party, i.Public);
                                    special = true;
                                }
                                baseValue = maxVlaue;
                            }
                            else
                            {
                                if (dropDeterminator < (i.Rate * Configuration.Instance.CalcSpecialDropRateForPC(owner) / 100.0f))
                                {
                                    this.AI.map.AddItemDrop(i.ItemID, i.TreasureGroup, this.mob, i.Party, i.Public);
                                    special = true;
                                }
                            }
                        }
                    }

                    baseValue = 0;
                    maxVlaue = 0;
                    //如果已经掉落印章,并且掉落特殊物品,同时开启了多重掉落
                    if ((!stamp && !special) || Configuration.Instance.MultipleDrop)
                    {
                        if (Configuration.Instance.MultipleDrop)
                        {
                            foreach (SagaDB.Mob.MobData.DropData i in this.mob.BaseData.dropItems)
                            {
                                int denominator = mob.BaseData.dropItems.Sum(x => x.Rate);
                                if (Global.Random.Next(1, denominator) < (i.Rate * Configuration.Instance.CalcGlobalDropRateForPC(owner)))
                                    this.AI.map.AddItemDrop(i.ItemID, i.TreasureGroup, this.mob, i.Party, i.Public);
                            }

                        }
                        else
                        {
                            //如果这个怪物有掉落的话...
                            if (this.mob.BaseData.dropItems.Count > 0)
                            {
                                int denominator = mob.BaseData.dropItems.Sum(x => x.Rate);
                                int index = 0;
                                //从掉落物品里随机抽取一个
                                index = Global.Random.Next(0, this.mob.BaseData.dropItems.Count - 1);
                                //检查该物品是否掉落如果掉落则添加该物品
                                if (Global.Random.Next(1, denominator) <= this.mob.BaseData.dropItems[index].Rate * Configuration.Instance.CalcGlobalDropRateForPC(owner))
                                {
                                    SagaDB.Mob.MobData.DropData drops = this.mob.BaseData.dropItems[index];
                                    this.AI.map.AddItemDrop(drops.ItemID, drops.TreasureGroup, this.mob, drops.Party, drops.Public);
                                }
                            }
                        }
                    }
                }

                this.mob.ClearTaskAddition();
                this.AI.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, this.mob, false);
                //这里可能出问题了.检查是否有这个任务只是一个应急解决的办法,可能触发别的问题
                Tasks.Mob.DeleteCorpse task = new SagaMap.Tasks.Mob.DeleteCorpse(this.mob);
                if (!this.mob.Tasks.ContainsKey("DeleteCorpse"))
                {
                    this.mob.Tasks.Add("DeleteCorpse", task);
                    task.Activate();
                }

                if (this.AI.SpawnDelay != 0)
                {
                    //这里可能出问题了.检查是否有这个任务只是一个应急解决的办法,可能触发别的问题
                    Tasks.Mob.Respawn respawn = new SagaMap.Tasks.Mob.Respawn(this.mob, this.AI.SpawnDelay);
                    if (!this.mob.Tasks.ContainsKey("Respawn"))
                    {
                        this.mob.Tasks.Add("Respawn", respawn);
                        respawn.Activate();
                    }
                    if (this.AI.Announce != "" && this.AI.SpawnDelay >= 300)
                    {
                        Tasks.Mob.RespawnAnnounce respawnannounce = new Tasks.Mob.RespawnAnnounce(this.mob, this.AI.SpawnDelay - 300000);
                        if (!this.mob.Tasks.ContainsKey("RespawnAnnounce"))
                        {
                            this.mob.Tasks.Add("RespawnAnnounce", respawnannounce);
                            respawnannounce.Activate();
                        }
                    }
                }

                this.AI.firstAttacker = null;
                this.mob.Status.attackingActors.Clear();
                this.AI.DamageTable.Clear();
                this.mob.VisibleActors.Clear();

                if (this.AI.Mode.Symbol || this.AI.Mode.SymbolTrash)
                {
                    ODWarManager.Instance.SymbolDown(this.mob.MapID, this.mob);
                }
            }

        }
        public void OnKick()
        {
            throw new NotImplementedException();
        }
        public void OnMapLoaded()
        {
            throw new NotImplementedException();
        }
        public void OnReSpawn()
        {
            throw new NotImplementedException();
        }
        public void OnSendMessage(string from, string message)
        {
            throw new NotImplementedException();
        }
        public void OnSendWhisper(string name, string message, byte flag)
        {
            throw new NotImplementedException();
        }
        public void OnTeleport(short x, short y)
        {
            throw new NotImplementedException();
        }
        public void OnAttack(Actor aActor, MapEventArgs args)
        {
            SkillArg arg = (SkillArg)args;
            AI.OnSeenSkillUse(arg);
            try
            {
                if (Attacking != null)
                {
                    if (AI.lastAttacker != null)
                    {
                        if (AI.lastAttacker.type == ActorType.PC)
                            RunCallback(Attacking, (ActorPC)AI.lastAttacker);
                        else
                            RunCallback(Attacking, null);
                    }
                    else
                        RunCallback(Attacking, null);
                }
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
        }
        public void OnHPMPSPUpdate(Actor sActor)
        {
            //移除boss系缓慢回血的机制
            //if (Skill.SkillHandler.Instance.isBossMob(this.mob))
            //{
            //    if (!this.mob.Tasks.ContainsKey("MobRecover"))
            //    {
            //        Tasks.Mob.MobRecover MobRecover = new SagaMap.Tasks.Mob.MobRecover((ActorMob)this.mob);
            //        this.mob.Tasks.Add("MobRecover", MobRecover);
            //        MobRecover.Activate();
            //    }
            //}

            if (Defending != null)
            {
                if (AI.lastAttacker != null)
                {
                    if (AI.lastAttacker.type == ActorType.PC)
                        RunCallback(Defending, (ActorPC)AI.lastAttacker);
                    else
                        RunCallback(Defending, null);
                }
                else
                    RunCallback(Defending, null);
            }
        }
        public void OnPlayerChangeStatus(ActorPC aActor)
        {

        }
        public void OnActorChangeBuff(Actor sActor)
        {

        }
        public void OnLevelUp(Actor sActor, MapEventArgs args)
        {
        }
        public void OnPlayerMode(Actor aActor)
        {
        }
        public void OnShowEffect(Actor aActor, MapEventArgs args)
        {
        }
        public void OnActorPossession(Actor aActor, MapEventArgs args)
        {

        }
        public void OnActorPartyUpdate(ActorPC aActor)
        {

        }
        public void OnActorSpeedChange(Actor mActor)
        {

        }
        public void OnSignUpdate(Actor aActor)
        {

        }
        public void PropertyUpdate(UpdateEvent arg, int para)
        {
            switch (arg)
            {
                case UpdateEvent.SPEED:
                    AI.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SPEED_UPDATE, null, this.mob, true);
                    break;
            }
        }
        public void PropertyRead(UpdateEvent arg)
        {
        }
        public void OnActorRingUpdate(ActorPC aActor)
        { }
        public void OnActorWRPRankingUpdate(ActorPC aActor)
        { }
        public void OnActorChangeAttackType(ActorPC aActor)
        { }
        void RunCallback(Scripting.MobCallback callback, ActorPC pc)
        {
            currentCall = callback;
            currentPC = pc;
            System.Threading.Thread th = new System.Threading.Thread(Run);
            th.Start();
        }
        void Run()
        {
            ClientManager.EnterCriticalArea();
            try
            {
                if (currentCall != null)
                {
                    if (currentPC != null)
                        currentCall.Invoke(this, currentPC);
                    else
                    {
                        if (this.AI.map.Creator != null)
                            currentCall.Invoke(this, this.AI.map.Creator);
                    }
                }
            }
            catch (Exception ex)
            {
                SagaLib.Logger.ShowError(ex);
            }
            ClientManager.LeaveCriticalArea();
        }
        public void OnUpdate(Actor aActor)
        {
            try
            {
                if (Updating != null)
                {
                    if (AI.lastAttacker != null)
                    {
                        if (AI.lastAttacker.type == ActorType.PC)
                            RunCallback(Updating, (ActorPC)AI.lastAttacker);
                        else
                            RunCallback(Updating, null);
                    }
                    else
                        RunCallback(Updating, null);
                }
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
        }
        #endregion
    }
}

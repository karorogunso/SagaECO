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
        public event Scripting.MobCallback Returning;
        public event Scripting.MobCallback SkillUsing;
        public event Scripting.MobCallback Updating;
        public event Scripting.MobCallback FirstTimeDefending;

        public MobEventHandler(ActorMob mob)
        {
            this.mob = mob;
            this.AI = new SagaMap.Mob.MobAI(mob);
        }

        #region ActorEventHandler Members
        public void OnActorSkillCancel(Actor sActor)
        {

        }
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
        public void OnActorReturning(Actor sActor)
        {
            try
            {
                if (Returning != null)
                {
                    if (AI.lastAttacker != null)
                    {
                        if (AI.lastAttacker.type == ActorType.PC)
                        {
                            RunCallback(Returning, (ActorPC)AI.lastAttacker);
                            return;
                        }
                        else if (AI.lastAttacker.type == ActorType.SHADOW)
                        {
                            if (((PetEventHandler)((ActorShadow)AI.lastAttacker).e).AI.Master != null)
                            {
                                if (((PetEventHandler)((ActorShadow)AI.lastAttacker).e).AI.Master.type == ActorType.PC)
                                {
                                    ActorPC pc = (ActorPC)((PetEventHandler)((ActorShadow)AI.lastAttacker).e).AI.Master;
                                    RunCallback(Returning, pc);
                                    return;
                                }
                            }
                        }
                    }
                    RunCallback(Returning, null);
                }
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
        }

        public void OnActorSkillUse(Actor sActor, MapEventArgs args)
        {
            SkillArg arg = (SkillArg)args;
            try
            {
                AI.OnSeenSkillUse(arg);
            }
            catch { }
            try
            {
                if (SkillUsing != null)
                {
                    if (AI.lastAttacker != null)
                    {
                        if (AI.lastAttacker.type == ActorType.PC)
                        {
                            RunCallback(SkillUsing, (ActorPC)AI.lastAttacker);
                            return;
                        }
                        else if (AI.lastAttacker.type == ActorType.SHADOW)
                        {
                            if (((PetEventHandler)((ActorShadow)AI.lastAttacker).e).AI.Master != null)
                            {
                                if (((PetEventHandler)((ActorShadow)AI.lastAttacker).e).AI.Master.type == ActorType.PC)
                                {
                                    ActorPC pc = (ActorPC)((PetEventHandler)((ActorShadow)AI.lastAttacker).e).AI.Master;
                                    RunCallback(SkillUsing, (ActorPC)pc);
                                    return;
                                }
                            }
                        }
                    }
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
                        else if (AI.lastAttacker.type == ActorType.SHADOW)
                        {
                            if (((PetEventHandler)((ActorShadow)AI.lastAttacker).e).AI.Master != null)
                            {
                                if (((PetEventHandler)((ActorShadow)AI.lastAttacker).e).AI.Master.type == ActorType.PC)
                                {
                                    ActorPC pc = (ActorPC)((PetEventHandler)((ActorShadow)AI.lastAttacker).e).AI.Master;
                                    RunCallback(Moving, (ActorPC)pc);
                                }
                                else
                                    RunCallback(Moving, null);
                            }
                            else
                                RunCallback(Moving, null);
                        }
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
            if (this.mob.Buff.Dead) return;
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
            if (AI.firstAttacker != null)
            {
                if (AI.firstAttacker.type == ActorType.GOLEM)
                {
                    ActorGolem golem = (ActorGolem)AI.firstAttacker;
                    MobEventHandler ehs = (MobEventHandler)golem.e;
                    Skill.Additions.Global.OtherAddition skills = new Skill.Additions.Global.OtherAddition(null, golem, "石像击杀怪物CD", Global.Random.Next(10000, 45000));
                    skills.OnAdditionStart += (s, e) =>
                    {
                        ehs.AI.Mode.mask.SetValue(1, false);
                    };
                    skills.OnAdditionEnd += (s, e) =>
                    {
                        ehs.AI.Mode.mask.SetValue(1, true);
                    };
                    Skill.SkillHandler.ApplyAddition(golem, skills);
                }
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
                            else if (AI.lastAttacker.type == ActorType.SHADOW)
                            {
                                if (((PetEventHandler)((ActorShadow)AI.lastAttacker).e).AI.Master != null)
                                {
                                    if (((PetEventHandler)((ActorShadow)AI.lastAttacker).e).AI.Master.type == ActorType.PC)
                                    {
                                        ActorPC pc = (ActorPC)((PetEventHandler)((ActorShadow)AI.lastAttacker).e).AI.Master;
                                        RunCallback(Dying, (ActorPC)pc);
                                    }
                                    else
                                        RunCallback(Dying, null);
                                }
                                else
                                    RunCallback(Dying, null);
                            }
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
                this.AI.Pause();
                if (loot)
                {
                    //分配经验
                    ExperienceManager.Instance.ProcessMobExp(mob);
                    //drops
                    //special drops

                    //boss掉心
                    if (Configuration.Instance.ActiveSpecialLoot)
                        if (mob.BaseData.mobType.ToString().Contains("BOSS") && AI.SpawnDelay >= 1800000)
                        {
                            if (Global.Random.Next(0, 10000) <= Configuration.Instance.BossSpecialLootRate)
                                for (int i = 0; i < Configuration.Instance.BossSpecialLootNum; i++)
                                    this.AI.map.AddItemDrop(Configuration.Instance.BossSpecialLootID, null, this.mob, false, false, false);
                        }
                        else
                            if (Global.Random.Next(0, 10000) <= Configuration.Instance.NomalMobSpecialLootRate && ((ActorEventHandlers.MobEventHandler)mob.e).AI.SpawnDelay != 0)
                            for (int i = 0; i < Configuration.Instance.NomalMobSpecialLootNum; i++)
                                this.AI.map.AddItemDrop(Configuration.Instance.NomalMobSpecialLootID, null, this.mob, false, false, false);

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
                            this.AI.map.AddItemDrop(mob.BaseData.stampDrop.ItemID, null, this.mob, false, false, false);
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
                                    this.AI.map.AddItemDrop(i.ItemID, i.TreasureGroup, this.mob, i.Party, i.Public, i.Public20);
                                    special = true;
                                }
                                baseValue = maxVlaue;
                            }
                            else
                            {
                                if (dropDeterminator < (i.Rate * Configuration.Instance.CalcSpecialDropRateForPC(owner) / 100.0f))
                                {
                                    this.AI.map.AddItemDrop(i.ItemID, i.TreasureGroup, this.mob, i.Party, i.Public, i.Public20);
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

                                //这里简单的做一个头发的过滤
                                if (i.ItemID != 10000000)
                                    continue;

                                if (Global.Random.Next(1, denominator) < (i.Rate * Configuration.Instance.CalcGlobalDropRateForPC(owner)))
                                    this.AI.map.AddItemDrop(i.ItemID, i.TreasureGroup, this.mob, i.Party, i.Public, i.Public20);
                            }

                        }
                        else
                        {
                            //如果这个怪物有掉落的话...
                            if (this.mob.BaseData.dropItems.Count > 0)
                            {
                                maxVlaue = baseValue = 0;
                                bool oneshotdrop = false;
                                int denominator = Global.Random.Next(1, mob.BaseData.dropItems.Sum(x => x.Rate));

                                for (int ix = 0; ix < (int)Configuration.Instance.CalcGlobalDropRateForPC(owner); ix++)
                                {
                                    foreach (SagaDB.Mob.MobData.DropData i in this.mob.BaseData.dropItems)
                                    {
                                        if (oneshotdrop)
                                            continue;

                                        maxVlaue = baseValue + i.Rate;
                                        if (denominator >= baseValue && denominator < maxVlaue)
                                        {
                                            //这里简单的做一个头发的过滤, 掉了个头发也算是掉东西了.
                                            if (i.ItemID != 10000000)
                                            {
                                                this.AI.map.AddItemDrop(i.ItemID, i.TreasureGroup, this.mob, i.Party, i.Public, i.Public20);
                                                oneshotdrop = true;
                                            }
                                            else
                                            {
                                                if (ix == (int)Configuration.Instance.CalcGlobalDropRateForPC(owner) - 1)
                                                    oneshotdrop = true;
                                            }
                                        }
                                        baseValue = maxVlaue;
                                    }
                                }
                            }
                        }
                    }
                }

                this.mob.ClearTaskAddition();
                this.AI.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, this.mob, false);
                Tasks.Mob.DeleteCorpse task = new SagaMap.Tasks.Mob.DeleteCorpse(this.mob);
                this.mob.Tasks.Add("DeleteCorpse", task);
                task.Activate();

                if (this.AI.SpawnDelay != 0)
                {
                    Tasks.Mob.Respawn respawn = new SagaMap.Tasks.Mob.Respawn(this.mob, this.AI.SpawnDelay);
                    this.mob.Tasks.Add("Respawn", respawn);
                    respawn.Activate();

                    /*if (this.AI.Announce != "" && this.AI.SpawnDelay >= 300)
                    {
                        Tasks.Mob.RespawnAnnounce respawnannounce = new Tasks.Mob.RespawnAnnounce(this.mob, this.AI.SpawnDelay - 300000);
                        this.mob.Tasks.Add("RespawnAnnounce", respawnannounce);
                        respawnannounce.Activate();
                    }*/
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
                        {
                            RunCallback(Attacking, (ActorPC)AI.lastAttacker);
                            return;
                        }
                        else if (AI.lastAttacker.type == ActorType.SHADOW)
                        {
                            if (((PetEventHandler)((ActorShadow)AI.lastAttacker).e).AI.Master != null)
                            {
                                if (((PetEventHandler)((ActorShadow)AI.lastAttacker).e).AI.Master.type == ActorType.PC)
                                {
                                    ActorPC pc = (ActorPC)((PetEventHandler)((ActorShadow)AI.lastAttacker).e).AI.Master;
                                    RunCallback(Attacking, (ActorPC)pc);
                                    return;
                                }
                            }
                        }
                    }
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
            /*if (Skill.SkillHandler.Instance.isBossMob(this.mob))
            {
                if (!this.mob.Tasks.ContainsKey("MobRecover"))
                {
                    Tasks.Mob.MobRecover MobRecover = new SagaMap.Tasks.Mob.MobRecover((ActorMob)this.mob);
                    this.mob.Tasks.Add("MobRecover", MobRecover);
                    MobRecover.Activate();
                }
            }*///关闭怪物回复线程以节省资源
            if (sActor.HP < sActor.MaxHP * 0.05f) return;
            if (Defending != null)
            {
                if (AI.lastAttacker != null)
                {
                    if (AI.lastAttacker.type == ActorType.PC)
                    {
                        RunCallback(Defending, (ActorPC)AI.lastAttacker);
                        return;
                    }
                    else if (AI.lastAttacker.type == ActorType.SHADOW)
                    {
                        if (((PetEventHandler)((ActorShadow)AI.lastAttacker).e).AI.Master != null)
                        {
                            if (((PetEventHandler)((ActorShadow)AI.lastAttacker).e).AI.Master.type == ActorType.PC)
                            {
                                ActorPC pc = (ActorPC)((PetEventHandler)((ActorShadow)AI.lastAttacker).e).AI.Master;
                                RunCallback(Defending, pc);
                                return;
                            }
                        }
                    }
                }
                RunCallback(Defending, null);
            }
            if (FirstTimeDefending != null && !mob.FirstDefending)
            {
                mob.FirstDefending = true;
                if (AI.lastAttacker != null)
                {
                    if (AI.lastAttacker.type == ActorType.PC)
                    {
                        RunCallback(FirstTimeDefending, (ActorPC)AI.lastAttacker);
                        return;
                    }
                    else if (AI.lastAttacker.type == ActorType.SHADOW)
                    {
                        if (((PetEventHandler)((ActorShadow)AI.lastAttacker).e).AI.Master != null)
                        {
                            if (((PetEventHandler)((ActorShadow)AI.lastAttacker).e).AI.Master.type == ActorType.PC)
                            {
                                ActorPC pc = (ActorPC)((PetEventHandler)((ActorShadow)AI.lastAttacker).e).AI.Master;
                                RunCallback(FirstTimeDefending, pc);
                                return;
                            }
                        }
                    }
                }
                RunCallback(FirstTimeDefending, null);
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
        public void OnActorFurnitureSit(ActorPC aActor)
        { }
        public void OnActorFurnitureList(Object obj)
        { }
        void RunCallback(Scripting.MobCallback callback, ActorPC pc)
        {
            try
            {
                currentCall = callback;
                currentPC = pc;
                System.Threading.Thread th = new System.Threading.Thread(Run);
                th.Start();
            }
            catch (Exception ex)
            {
                SagaLib.Logger.ShowError(ex);
            }
        }
        DateTime mark = DateTime.Now;
        void Run()
        {
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
        public void OnActorPaperChange(ActorPC aActor)
        {
        }
        #endregion
    }
}

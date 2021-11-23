using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaMap.Network.Client;

using SagaLib;
using SagaDB;
using SagaDB.Actor;
using SagaDB.Item;
using SagaDB.Mob;

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
            if (!mob.VisibleActors.ContainsKey(aActor.ActorID))
                mob.VisibleActors.Add(aActor.ActorID, aActor.ActorID);
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
                    if (AI.LastAttacker != null)
                    {
                        if (AI.LastAttacker.type == ActorType.PC)
                        {
                            RunCallback(Returning, (ActorPC)AI.LastAttacker);
                            return;
                        }
                        else if (AI.LastAttacker.type == ActorType.SHADOW)
                        {
                            if (((PetEventHandler)((ActorShadow)AI.LastAttacker).e).AI.Master != null)
                            {
                                if (((PetEventHandler)((ActorShadow)AI.LastAttacker).e).AI.Master.type == ActorType.PC)
                                {
                                    ActorPC pc = (ActorPC)((PetEventHandler)((ActorShadow)AI.LastAttacker).e).AI.Master;
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
                    if (AI.LastAttacker != null)
                    {
                        if (AI.LastAttacker.type == ActorType.PC)
                        {
                            RunCallback(SkillUsing, (ActorPC)AI.LastAttacker);
                            return;
                        }
                        else if (AI.LastAttacker.type == ActorType.SHADOW)
                        {
                            if (((PetEventHandler)((ActorShadow)AI.LastAttacker).e).AI.Master != null)
                            {
                                if (((PetEventHandler)((ActorShadow)AI.LastAttacker).e).AI.Master.type == ActorType.PC)
                                {
                                    ActorPC pc = (ActorPC)((PetEventHandler)((ActorShadow)AI.LastAttacker).e).AI.Master;
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
                    if (AI.LastAttacker != null)
                    {
                        if (AI.LastAttacker.type == ActorType.PC)
                            RunCallback(Moving, (ActorPC)AI.LastAttacker);
                        else if (AI.LastAttacker.type == ActorType.SHADOW)
                        {
                            if (((PetEventHandler)((ActorShadow)AI.LastAttacker).e).AI.Master != null)
                            {
                                if (((PetEventHandler)((ActorShadow)AI.LastAttacker).e).AI.Master.type == ActorType.PC)
                                {
                                    ActorPC pc = (ActorPC)((PetEventHandler)((ActorShadow)AI.LastAttacker).e).AI.Master;
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
                if (mob.Owner != null && mob.Owner.Slave.Contains(mob))
                    mob.Owner.Slave.Remove(mob);
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
                        if (AI.LastAttacker != null)
                        {
                            if (AI.LastAttacker.type == ActorType.PC)
                                RunCallback(Dying, (ActorPC)AI.LastAttacker);
                            else if (AI.LastAttacker.type == ActorType.SHADOW)
                            {
                                if (((PetEventHandler)((ActorShadow)AI.LastAttacker).e).AI.Master != null)
                                {
                                    if (((PetEventHandler)((ActorShadow)AI.LastAttacker).e).AI.Master.type == ActorType.PC)
                                    {
                                        ActorPC pc = (ActorPC)((PetEventHandler)((ActorShadow)AI.LastAttacker).e).AI.Master;
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
                    int dropDeterminator = Global.Random.Next(0, 10000);
                    int baseValue = 0, maxVlaue = 0;
                    bool stamp = false;
                    bool special = false;
                    ActorPC owner = null;
                    /*if (mob.type == ActorType.MOB)
                    {
                        ActorEventHandlers.MobEventHandler eh = (ActorEventHandlers.MobEventHandler)mob.e;
                        if (eh.AI.firstAttacker != null && eh.AI.firstAttacker.type == ActorType.PC)
                            owner = (ActorPC)eh.AI.firstAttacker;

                        if (Global.Random.Next(0, 100) <= 50)
                        {
                            this.AI.map.AddItemDrop(47000011, null, this.mob, false, false);
                        }
                    }
                    if (mob.BaseData.stampDrop != null)//印章
                    {   
                        /*if (Global.Random.Next(0, 1000000) <= (mob.BaseData.stampDrop.Rate * Configuration.Instance.CalcStampDropRateForPC(owner)))
                        {
                            this.AI.map.AddItemDrop(mob.BaseData.stampDrop.ItemID, null, this.mob, false,false);
                            stamp = true;
                        }
                    }*/

                    if ((!stamp || Configuration.Instance.MultipleDrop) && checkDropSpecial())
                    {
                        foreach (SagaDB.Mob.MobData.DropData i in this.mob.dropItemsSpecial)//团队道具
                        {
                            if (!Configuration.Instance.MultipleDrop)
                            {
                                maxVlaue = baseValue + i.Rate;
                                if (dropDeterminator >= baseValue && dropDeterminator < maxVlaue)
                                {
                                    this.AI.map.AddItemDrop(i.ItemID, i.TreasureGroup, this.mob, i.Party, i.Public, i.Public20, i.count);
                                    special = true;
                                }
                                baseValue = maxVlaue;
                            }
                            else //个人道具掉率
                            {
                                if (Global.Random.Next(0, 10000) < (i.Rate * Configuration.Instance.CalcSpecialDropRateForPC(owner)))
                                {
                                    this.AI.map.AddItemDrop(i.ItemID, i.TreasureGroup, this.mob, i.Party, i.Public, i.Public20, i.count);
                                    special = true;
                                }
                            }
                        }
                    }

                    baseValue = 0;
                    maxVlaue = 0;

                    int usedtime = (int)(DateTime.Now - mob.BattleStartTime).TotalSeconds;
                    if ((!stamp && !special) || Configuration.Instance.MultipleDrop)
                    {
                        int clv = 10;
                        if (mob.Level > 60)
                            clv = mob.Level - 50;
                        int maxcount = clv / 4;
                        if (maxcount < 3) maxcount = 3;
                        if (maxcount > 6) maxcount = 6;

                        Map map = MapManager.Instance.GetMap(mob.MapID);
                        if (!map.IsDungeon)
                        {
                            if (mob.MobID == 10000000)//下次活动再降低原版怪掉落
                            {
                                if (Global.Random.Next(0, 10000) < 5800)
                                    AI.map.AddItemDrop(952000012, null, this.mob, false, false, false, (ushort)Global.Random.Next(1, maxcount));
                            }
                            else
                            {
                                if (Global.Random.Next(0, 10000) < 2500)
                                    AI.map.AddItemDrop(952000012, null, this.mob, false, false, false, (ushort)Global.Random.Next(1, maxcount));
                            }
                        }
                        int count = (int)(mob.MaxHP / 30000);
                        if (count > 5) count = 5;
                        if(count < 1) count = 1;
                        if (mob.MobID == 10000000)
                        {
                            /*if (Global.Random.Next(0, 10000) < 3000)
                            AI.map.AddItemDrop(110005605, null, this.mob, false, false, false, (ushort)count);*/
                        }
                        else
                        {
                            /*if (Global.Random.Next(0, 10000) < 2000)
                                AI.map.AddItemDrop(110005605, null, this.mob, false, false, false, (ushort)count);*/
                            if (OriMobSetting.Instance.Items.ContainsKey(mob.MobID) && !mob.YggMob)
                            {
                                byte attendeeCount = 0;
                                AI.DamageTable.Keys.ToList().ForEach(e =>
                                {
                                    Actor ac = AI.map.GetActor(e);
                                    if (ac != null)
                                        if (ac.type == ActorType.PC)
                                            attendeeCount++;
                                });
                                int rate = attendeeCount * 8;
                                if (Global.Random.Next(0, 100) < rate)
                                {
                                    if (Global.Random.Next(0, 100) < 80)
                                    {
                                        AI.map.AddItemDrop(950000015, null, mob, false, true, false, 1, 0, 0, 10000, false, mob.MobID);
                                        AI.map.AddItemDrop(950000015, null, mob, false, false, true, 1, 0, 0, 10000, false, mob.MobID);
                                    }
                                    else
                                    {
                                        AI.map.AddItemDrop(10020758, null, mob, false, true, false, 1, 0, 0, 10000, false, mob.MobID);
                                        AI.map.AddItemDrop(10020758, null, mob, false, false, true, 1, 0, 0, 10000, false, mob.MobID);
                                    }
                                    //TODO:增加设定好的额外掉落
                                }
                            }
                            if (Global.Random.Next(0, 10000) < 5)
                                AI.map.AddItemDrop(10020758, null, mob, false, false, false, (ushort)1, 0, 0, 10000, false, mob.MobID);
                        }

                        foreach (SagaDB.Mob.MobData.DropData i in this.mob.dropItems)
                        {
                            if ((i.GreaterThanTime == 0 && i.LessThanTime == 0) || (usedtime > i.GreaterThanTime && i.GreaterThanTime != 0) || (usedtime <= i.LessThanTime && i.LessThanTime != 0))
                            {
                                if (!Configuration.Instance.MultipleDrop)
                                {
                                    maxVlaue = baseValue + i.Rate;
                                    if (dropDeterminator >= baseValue && dropDeterminator < maxVlaue)
                                        AI.map.AddItemDrop(i.ItemID, i.TreasureGroup, this.mob, i.Party, i.Public, i.Public20, i.count);
                                    baseValue = maxVlaue;
                                }
                                else//个人道具掉率
                                {
                                    if (i.Roll)
                                    {
                                        if (Global.Random.Next(0, 10000) < i.Rate)
                                            AI.map.AddItemDrop(i.ItemID, null, this.mob, false, false, false, i.count, i.MinCount, i.MaxCount, i.Rate, i.Roll);//ROLL
                                    }
                                    else if (i.Party)
                                    {
                                        if (Global.Random.Next(0, 10000) < i.Rate)
                                            AI.map.AddItemDrop(i.ItemID, i.TreasureGroup, this.mob, i.Party, i.Public, i.Public20, i.count, i.MinCount, i.MaxCount, i.Rate);
                                    }
                                    else
                                    {
                                        if (Global.Random.Next(0, 10000) < (i.Rate * Configuration.Instance.CalcGlobalDropRateForPC(owner)))
                                            AI.map.AddItemDrop(i.ItemID, i.TreasureGroup, this.mob, i.Party, i.Public, i.Public20, i.count, i.MinCount, i.MaxCount);
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

                AI.firstAttacker = null;
                mob.Status.attackingActors.Clear();
                AI.DamageTable.Clear();
                mob.VisibleActors.Clear();

                if (mob.Slave.Count > 0)
                {
                    Actor[] s = new Actor[mob.Slave.Count + 10];
                    mob.Slave.CopyTo(s);
                    for (int i = 0; i < mob.Slave.Count; i++)
                    {
                        if (s[i] != null)
                        {
                            if (mob.HP > 0)
                                Skill.SkillHandler.Instance.ShowEffectByActor(s[i], 4310);
                            MobEventHandler eh = (MobEventHandler)s[i].e;
                            s[i].Buff.死んだふり = true;
                            eh.OnDie(false);
                            AI.map.DeleteActor(s[i]);
                        }
                    }
                }



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
            //throw new NotImplementedException();
        }

        public void OnAttack(Actor aActor, MapEventArgs args)
        {
            SkillArg arg = (SkillArg)args;
            AI.OnSeenSkillUse(arg);
            try
            {
                if (Attacking != null)
                {
                    if (AI.LastAttacker != null)
                    {
                        if (AI.LastAttacker.type == ActorType.PC)
                        {
                            RunCallback(Attacking, (ActorPC)AI.LastAttacker);
                            return;
                        }
                        else if (AI.LastAttacker.type == ActorType.SHADOW)
                        {
                            if (((PetEventHandler)((ActorShadow)AI.LastAttacker).e).AI.Master != null)
                            {
                                if (((PetEventHandler)((ActorShadow)AI.LastAttacker).e).AI.Master.type == ActorType.PC)
                                {
                                    ActorPC pc = (ActorPC)((PetEventHandler)((ActorShadow)AI.LastAttacker).e).AI.Master;
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
                if (AI.LastAttacker != null)
                {
                    if (AI.LastAttacker.type == ActorType.PC)
                    {
                        RunCallback(Defending, (ActorPC)AI.LastAttacker);
                        return;
                    }
                    else if (AI.LastAttacker.type == ActorType.SHADOW)
                    {
                        if (((PetEventHandler)((ActorShadow)AI.LastAttacker).e).AI.Master != null)
                        {
                            if (((PetEventHandler)((ActorShadow)AI.LastAttacker).e).AI.Master.type == ActorType.PC)
                            {
                                ActorPC pc = (ActorPC)((PetEventHandler)((ActorShadow)AI.LastAttacker).e).AI.Master;
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
                if (AI.LastAttacker != null)
                {
                    if (AI.LastAttacker.type == ActorType.PC)
                    {
                        RunCallback(FirstTimeDefending, (ActorPC)AI.LastAttacker);
                        return;
                    }
                    else if (AI.LastAttacker.type == ActorType.SHADOW)
                    {
                        if (((PetEventHandler)((ActorShadow)AI.LastAttacker).e).AI.Master != null)
                        {
                            if (((PetEventHandler)((ActorShadow)AI.LastAttacker).e).AI.Master.type == ActorType.PC)
                            {
                                ActorPC pc = (ActorPC)((PetEventHandler)((ActorShadow)AI.LastAttacker).e).AI.Master;
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
                    if (AI.LastAttacker != null)
                    {
                        if (AI.LastAttacker.type == ActorType.PC)
                            RunCallback(Updating, (ActorPC)AI.LastAttacker);
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

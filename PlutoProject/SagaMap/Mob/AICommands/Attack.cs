using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaMap;
using SagaMap.Scripting;

namespace SagaMap.Mob.AICommands
{
    public class Attack : AICommand
    {
        private CommandStatus status;
        private MobAI mob;
        private Actor dest;
        private bool attacking;
        MobAttack attacktask;
        public bool active;
        short x, y;
        int counter = 0;

        public Attack(MobAI mob)
        {
            this.mob = mob;
            this.Status = CommandStatus.INIT;
            int aspd = 0;
            if (this.mob.Mob.type == ActorType.MOB)
            {
                ActorMob tar = (ActorMob)this.mob.Mob;
                aspd = tar.BaseData.aspd;
            }
            if (this.mob.Mob.type == ActorType.PET)
            {
                ActorPet pet = (ActorPet)this.mob.Mob;
                aspd = pet.BaseData.aspd;
            }
            if(this.mob.Mob.type == ActorType.PARTNER)
            {
                ActorPartner pet = (ActorPartner)this.mob.Mob;
                aspd = pet.Status.aspd;
            }
            if (this.mob.Mob.type == ActorType.SHADOW || this.mob.Mob.type == ActorType.GOLEM ||
                this.mob.Mob.type == ActorType.PC)
            {
                aspd = this.mob.Mob.Status.aspd;
            }
            attacktask = new MobAttack(mob, dest);
            this.x = mob.Mob.X;
            this.y = mob.Mob.Y;
        }

        public string GetName() { return "Attack"; }

        private Actor CurrentTarget()
        {
            try
            {
                uint id = 0;
                uint hate = 0;
                Actor tmp = null;
                uint[] ids = new uint[mob.Hate.Keys.Count];
                mob.Hate.Keys.CopyTo(ids, 0);
                for (uint i = 0; i < mob.Hate.Keys.Count; i++)//Find out the actorPC with the highest hate value
                {
                    if (ids[i] == 0) continue;
                    if (ids[i] == this.mob.Mob.ActorID)
                        continue;
                    if (this.mob.Master != null)
                    {
                        if (ids[i] == this.mob.Master.ActorID && mob.Hate.Count > 1)
                            continue;
                    }
                    if (!mob.Hate.ContainsKey(ids[i]))
                        continue;
                    if (hate < mob.Hate[ids[i]])
                    {
                        hate = mob.Hate[ids[i]];
                        id = ids[i];
                        tmp = mob.map.GetActor(id);
                        if (tmp == null)
                        {
                            mob.Hate.Remove(id);
                            id = 0;
                            hate = 0;
                            active = false;
                            i = 0;
                            continue;
                        }
                        if (tmp.Status.Additions.ContainsKey("Hiding"))
                        {
                            mob.Hate.Remove(id);
                            id = 0;
                            hate = 0;
                            active = false;
                            i = 0;
                            continue;
                        }
                        if (tmp.Status.Additions.ContainsKey("Cloaking"))
                        {
                            mob.Hate.Remove(id);
                            id = 0;
                            hate = 0;
                            active = false;
                            i = 0;
                            continue;
                        }
                        if (tmp.Status.Additions.ContainsKey("Through"))
                        {
                            mob.Hate.Remove(id);
                            continue;
                        }
                        if (tmp.Status.Additions.ContainsKey("IAmTree"))
                        {
                            mob.Hate.Remove(id);
                            continue;
                        }
                        //控制partner不被攻击的语句，现在注释掉让actorpartner可以被攻击了
                        //if(tmp.type == ActorType.PARTNER)
                        //{
                        //    mob.Hate.Remove(id);
                        //    continue;
                        //}
                        active = true;

                        if (tmp.type == ActorType.PC && mob.Mob.type != ActorType.PET && mob.Mob.type != ActorType.PARTNER)
                        {
                            if (((ActorPC)tmp).PossessionTarget != 0)
                            {
                                mob.Hate.Remove(id);
                                id = 0;
                                hate = 0;
                                active = false;
                                i = 0;
                            }
                        }
                    }
                }
                if (id != 0)//Now the id is refer to the PC with the highest hate to the Mob.现在这个ID是怪物对最高仇恨者的ID
                {
                    tmp = mob.map.GetActor(id);
                    if (tmp != null)
                    {
                        if (tmp.HP == 0)
                        {
                            mob.Hate.Remove(tmp.ActorID);
                            id = 0;
                            active = false;
                        }
                    }
                }
                if(mob.Mob.type == ActorType.MOB)
                {
                    ActorMob Amob = (ActorMob)mob.Mob;
                    if(Amob.PriorityTartget != null)
                    {
                        if(Amob.MapID == Amob.PriorityTartget.MapID && Amob.HP > 0 && Amob.PriorityTartget.HP>0)
                        {
                            if (mob.Hate.Count == 0)//保存怪物战斗前位置
                            {
                                if (!mob.Mode.Active) return null;
                                mob.X_pb = mob.Mob.X;
                                mob.Y_pb = mob.Mob.Y;
                            }
                            if (!mob.Hate.ContainsKey(Amob.PriorityTartget.ActorID))
                                mob.Hate.Add(Amob.PriorityTartget.ActorID, 5);
                            mob.BackTimer = DateTime.Now;

                            return Amob.PriorityTartget;
                        }
                    }
                }
                if (id == 0)
                {
                    active = false;
                    return null;
                }
                if (dest != null)
                {
                    if (dest.ActorID != id) if (attacktask.Activated == true) attacktask.Deactivate();
                }
                return tmp;
            }
            catch (Exception ex)
            {
                SagaLib.Logger.ShowError(ex);
                return null;
            }

        }

        private void CheckAggro()
        {
            double distance = double.MaxValue;
            Actor target = null;
            bool isSlavaOfPc = false;
            if (this.mob.Master != null)
            {
                if (!this.mob.Hate.ContainsKey(this.mob.Master.ActorID))
                    this.mob.Hate.Add(this.mob.Master.ActorID, 1);
                if (this.mob.Master.type == ActorType.PC)
                    isSlavaOfPc = true;
            }
            uint[] ids = new uint[mob.Mob.VisibleActors.Count];
            mob.Mob.VisibleActors.CopyTo(ids);
            foreach (uint id in ids)
            {
                if (mob.map == null)
                    continue;
                Actor i = mob.map.GetActor(id);
                if (i == null)
                    continue;
                if (i.Buff.Transparent)
                    continue;
                if (i.MapID != this.mob.map.ID)
                    continue;
                if (i.Status.Additions.ContainsKey("IAmTree"))
                    continue;
                if (i.Status.Additions.ContainsKey("Through"))
                    continue;
                if (i.type == ActorType.PARTNER)
                    continue;
                if (i.HP == 0)
                    continue;
                if (mob.Mob.type != ActorType.PC && i.type == ActorType.MOB)
                {
                    ActorEventHandlers.MobEventHandler eh = (ActorEventHandlers.MobEventHandler)i.e;
                    if (eh.AI.Mode.Symbol && !isSlavaOfPc)
                    {
                        mob.Hate.Add(i.ActorID, 20);
                        //SendAggroEffect();
                    }
                }
                if (mob.Mob.type != ActorType.PC && mob.Mob.type != ActorType.GOLEM&& !mob.Mob.Buff.Zombie && i.type != ActorType.PC && i.type != ActorType.PET && i.type != ActorType.PARTNER && i.type != ActorType.SHADOW && !(i.type == ActorType.MOB && isSlavaOfPc))
                    continue;
                if (mob.Mob.type == ActorType.GOLEM && i.type == ActorType.PC && !mob.Mode.RunAway)
                    continue;
                if (mob.Mob.type == ActorType.GOLEM && i.type == ActorType.MOB && mob.Mode.RunAway)
                    continue;
                if (mob.Mob.type == ActorType.PC)
                {
                    if (!Skill.SkillHandler.Instance.CheckValidAttackTarget(mob.Mob, i))
                        continue;
                }
                if (isSlavaOfPc && i.type == ActorType.SHADOW)
                    continue;
                if (isSlavaOfPc && i.type == ActorType.PET)
                    continue;
                if (isSlavaOfPc && i.type == ActorType.PARTNER)
                    continue;
                if (isSlavaOfPc && i.type == ActorType.PC)
                    continue;
                if(isSlavaOfPc && i.type == ActorType.MOB)
                {
                    ActorEventHandlers.MobEventHandler ie = (ActorEventHandlers.MobEventHandler)i.e;
                    if (ie.AI.Master == this.mob.Master)
                        continue;
                }
                if (mob.Mob.type!= ActorType.PC && i.type == ActorType.PC)
                {
                    if (((ActorPC)i).PossessionTarget != 0)
                        continue;
                }
                double len = MobAI.GetLengthD(i.X, i.Y, mob.Mob.X, mob.Mob.Y);
                if (len < distance)
                {
                    byte x, y, x2, y2;
                    x = Global.PosX16to8(this.mob.Mob.X, this.mob.map.Width);
                    y = Global.PosY16to8(this.mob.Mob.Y, this.mob.map.Height);
                    x2 = Global.PosX16to8(i.X, this.mob.map.Width);
                    y2 = Global.PosY16to8(i.Y, this.mob.map.Height);

                    List<MapNode> path = this.mob.FindPath(x, y, x2, y2);
                    try
                    {
                        if (path[path.Count - 1].x == x2 && path[path.Count - 1].y == y2)
                        {
                            if (i.type == ActorType.SHADOW && target != i)
                            {
                                distance = 0;
                                target = i;
                            }
                            else
                            {
                                distance = len;
                                target = i;
                            }
                        }
                    }
                    catch { }
                }
            }
            int r = 800;
            if (mob.Mob.type == ActorType.GOLEM)
                r = 5000;
            if (distance <= r)
            {
                if (mob.Hate.Count == 0)//保存怪物战斗前位置
                {
                    mob.Mob.BattleStartTime = DateTime.Now;
                    mob.X_pb = mob.Mob.X;
                    mob.Y_pb = mob.Mob.Y;
                }
                if (!mob.Hate.ContainsKey(target.ActorID))
                    mob.Hate.Add(target.ActorID, 20);
                SendAggroEffect();
            }
        }

        void SendAggroEffect()
        {
            EffectArg arg = new EffectArg();
            arg.actorID = mob.Mob.ActorID;
            arg.effectID = 4539;
            mob.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, arg, mob.Mob, true);
        }

        private bool hasPlayerInSight()
        {
            if (mob.Mob.type == ActorType.PC)
                return true;
            for (int j = 0; j < mob.Mob.VisibleActors.Count;j++)
            {
                Actor i = mob.map.GetActor(mob.Mob.VisibleActors[j]);
                if (i == null)
                    continue;
                if (i.MapID != this.mob.map.ID)
                    continue;
                if (i.type == ActorType.PC)
                {
                    if (((ActorPC)i).Online)
                        return true;
                }
            }
            return false;
        }
        void returnAndInitialize()
        {
            if (mob.Mob.type == ActorType.GOLEM) return;
            short[] pos = new short[2] { mob.X_pb, mob.Y_pb };
            mob.map.MoveActor(Map.MOVE_TYPE.START, mob.Mob, pos, 1, 1000, false, MoveType.WARP2);
            mob.Mob.HP = mob.Mob.MaxHP;

            if (mob.Mob.type == ActorType.MOB)
            {
                if (((ActorMob)mob.Mob).AttackedForEvent != 0)
                    mob.Mob.e.OnActorReturning(mob.Mob);
                ((ActorMob)mob.Mob).AttackedForEvent = 0;
                mob.firstAttacker = null;
                mob.Mob.BattleStartTime = DateTime.Now;
            }

            //清空HP相关SKILL状态
            mob.SkillOfHPClear();
            mob.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, mob.Mob, false);
        }
        public void Update(object para)
        {
            try
            {
                ActorPartner pet = null;
                if (this.mob.Mob.type == ActorType.PARTNER)
                    pet = (ActorPartner)this.mob.Mob;
                if (this.mob.Hate.Count == 0)
                {
                    if (DateTime.Now > this.mob.BackTimer.AddSeconds(5))
                        this.mob.BackTimer = DateTime.Now;
                    if (DateTime.Now > this.mob.BackTimer.AddSeconds(3) && attacking && !mob.noreturn)//5秒后返回
                    {
                        returnAndInitialize();
                        attacking = false;
                        try
                        {
                            if (mob.Mob.Slave.Count != 0)
                            {
                                foreach (Actor a in mob.Mob.Slave)
                                {
                                    a.e.OnDie();
                                    EffectArg arg = new EffectArg();
                                    arg.actorID = a.ActorID;
                                    arg.effectID = 4310;
                                    mob.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, arg, a, true);
                                }
                            }
                        }
                        catch (Exception ex) { SagaLib.Logger.ShowError(ex); }
                        return;
                    }

                    if (!hasPlayerInSight())
                    {
                        counter++;
                        if (counter > 100)
                        {
                            this.mob.Pause();
                            counter = 0;
                            return;
                        }
                    }
                }

                if (mob.CannotAttack > DateTime.Now && mob.Mode.isAnAI)
                    return;

                if (pet != null)
                    if (!this.mob.Hate.ContainsKey(pet.Owner.ActorID))
                        this.mob.Hate.Add(pet.Owner.ActorID, 1);
                if (this.mob.Master != null)
                {
                    if (!this.mob.Hate.ContainsKey(this.mob.Master.ActorID))
                        this.mob.Hate.Add(this.mob.Master.ActorID, 1);
                }
                if (mob.Mob.Tasks.ContainsKey("AutoCast"))
                {
                    if (attacktask.Activated == true) attacktask.Deactivate();
                    //returnAndInitialize();
                    attacking = false;
                    return;
                }
                //施放主人战斗中技能，放在这个位置保证平时状态
                if ((DateTime.Now - mob.LastSkillCast).TotalSeconds >= 1)
                {
                    if (this.mob.Master != null)
                    {
                        if (this.mob.Master.type == ActorType.PC)
                        {
                            ActorPC pc = (ActorPC)this.mob.Master;
                            if (pc.BattleStatus == 1)
                            {
                                if (Global.Random.Next(0, 99) < mob.Mode.EventMasterCombatSkillRate)
                                {
                                    mob.OnShouldCastSkill(mob.Mode.EventMasterCombat, pc);
                                    mob.LastSkillCast = DateTime.Now;
                                }
                            }
                        }
                    }
                    if (pet != null)
                    {
                        ActorPC pc = pet.Owner;
                        if (pc.BattleStatus == 1)
                        {
                            if (Global.Random.Next(0, 99) < mob.Mode.EventMasterCombatSkillRate)
                            {
                                mob.OnShouldCastSkill(mob.Mode.EventMasterCombat, pc);
                                mob.LastSkillCast = DateTime.Now;
                            }
                        }
                    }
                }
                if (attacktask == null)
                    attacktask = new MobAttack(mob, dest);
                dest = CurrentTarget();
                //if (mob.Mob.type == ActorType.GOLEM)
                {
                    if (dest != null)
                    {
                        if (dest.Status != null)
                            if ((mob.Mode.Active || mob.Mob.Buff.Zombie) && (dest == null || dest == mob.Master))
                                CheckAggro();
                    }
                    else if (dest == null)
                    {
                        if ((mob.Mode.Active || mob.Mob.Buff.Zombie) && (dest == null || dest == mob.Master))
                            CheckAggro();
                    }
                }
                if (dest == null)
                {
                    mob.AIActivity = Activity.IDLE;
                    if (mob.commands.ContainsKey("Chase") == true) mob.commands.Remove("Chase"); ;
                    return;
                }
                mob.AIActivity = Activity.BUSY;
                if (mob.commands.ContainsKey("Move") == true) mob.commands.Remove("Move");

                attacktask.dActor = dest;
                if (mob.Mode.isAnAI)
                {
                    mob.OnShouldCastSkill_An(mob.Mode, this.dest);
                }
                if (mob.Mode.isNewAI)
                {
                    mob.OnShouldCastSkill_New(mob.Mode, this.dest);
                }
                if (mob.Mob.HP == 0)
                {
                    return;
                }
                else if ((DateTime.Now - mob.LastSkillCast).TotalSeconds >= 2)//施放技能，放在这个位置保证追踪模式下的技能优先
                {
                    if (Global.Random.Next(0, 99) < mob.Mode.EventAttackingSkillRate)
                    {
                        mob.OnShouldCastSkill(mob.Mode.EventAttacking, this.dest);
                        mob.LastSkillCast = DateTime.Now;
                    }
                }
                if (mob.commands.ContainsKey("Chase") == true)
                {
                    if (attacktask.Activated == true) attacktask.Deactivate();
                    //returnAndInitialize();
                    attacking = false;
                    return;
                }

                if (this.x != this.mob.Mob.X || this.y != this.mob.Mob.Y)
                {
                    short x, y;
                    this.mob.map.FindFreeCoord(this.mob.Mob.X, this.mob.Mob.Y, out x, out y, this.mob.Mob);
                    bool skip = false;
                    if (mob.Mob.type == ActorType.PET)
                    {
                        if (((ActorPet)mob.Mob).BaseData.mobType == SagaDB.Mob.MobType.MAGIC_CREATURE)
                            skip = true;
                    }
                    if ((this.mob.Mob.X == x && this.mob.Mob.Y == y) || this.mob.Mode.RunAway || skip)
                    {

                    }
                    else
                    {
                        short[] dst = new short[2] { x, y };

                        mob.map.MoveActor(Map.MOVE_TYPE.START, mob.Mob, dst, MobAI.GetDir((short)(dst[0] - x), (short)(dst[1] - y)), (ushort)(mob.Mob.Speed / 20));
                        return;
                    }
                    this.x = mob.Mob.X;
                    this.y = mob.Mob.Y;
                }
                if (dest.HP == 0)
                {
                    if (pet != null)
                    {
                        if (dest.ActorID != pet.Owner.ActorID)
                        {
                            if (mob.Hate.ContainsKey(dest.ActorID)) mob.Hate.Remove(dest.ActorID);
                            if (attacktask.Activated == true) attacktask.Deactivate();
                            attacktask = null;
                            return;
                        }
                    }
                    else
                    {
                        if (mob.Hate.ContainsKey(dest.ActorID)) mob.Hate.Remove(dest.ActorID);
                        if (attacktask.Activated == true) attacktask.Deactivate();
                        attacktask = null;
                        return;
                    }
                }
                /*ActorEventHandlers.PC_EventHandler eh = (SagaMap.ActorEventHandlers.PC_EventHandler)dest.e;
                if (eh.C.state == MapClient.SESSION_STATE.LOGGEDOFF)
                {
                    if (mob.Hate.ContainsKey(dest.id)) mob.Hate.Remove(dest.id);
                    if (attacktask.Activated() == true) attacktask.Deactivate();
                    attacktask = null;
                    return;
                }*/
                float size = 0;
                if (mob.Mode.isAnAI)
                {
                    size = mob.needlen;
                }
                else if (mob.Mob.type != ActorType.PC)
                {
                    if (((ActorMob)mob.Mob).BaseData != null)
                        size = ((ActorMob)mob.Mob).BaseData.range + 1;
                    if (((ActorMob)mob.Mob).range != 0)
                        size = ((ActorMob)mob.Mob).range + 1;
                    if (mob.Mob.type == ActorType.GOLEM)
                        size = mob.Mob.Range;
                }
                else
                    size = 1;
                bool ifChase = false;
                if (mob.Mob.type == ActorType.PET)
                {
                    if (dest.type == ActorType.PC)
                    {
                        if (((ActorPet)mob.Mob).BaseData.mobType == SagaDB.Mob.MobType.MAGIC_CREATURE && MobAI.GetLengthD(mob.Mob.X, mob.Mob.Y, dest.X, dest.Y) > 0)
                        {
                            ifChase = true;
                        }
                    }
                }
                if (MobAI.GetLengthD(mob.Mob.X, mob.Mob.Y, dest.X, dest.Y) > (size * 150) || ifChase)
                {
                    //SagaLib.Logger.ShowInfo(size.ToString());
                    if (!mob.Mode.RunAway || MobAI.GetLengthD(mob.Mob.X, mob.Mob.Y, dest.X, dest.Y) < 2000)
                    {
                        Chase chase = new Chase(this.mob, dest);
                        mob.commands.Add("Chase", chase);
                        if (attacktask.Activated == true) attacktask.Deactivate();
                        //returnAndInitialize();
                        attacking = false;
                    }
                }
                else
                {
                    if (!this.mob.Mode.RunAway || Global.Random.Next(0, 99) < 70)
                    {
                        if (mob.CanAttack)
                        {
                            if (pet != null)
                            {
                                if (dest.ActorID == pet.Owner.ActorID)
                                    return;
                            }
                            if (attacktask.Activated == false) attacktask.Activate();
                            attacking = true;
                        }
                    }
                    else
                    {
                        Chase chase = new Chase(this.mob, dest);
                        mob.commands.Add("Chase", chase);
                        if (attacktask.Activated == true) attacktask.Deactivate();
                        //returnAndInitialize();
                        attacking = false;
                    }
                }
                mob.Mob.e.OnUpdate(mob.Mob);
            }
            catch(Exception ex)
            {
                Logger.ShowError(ex);
            }
        }

        public CommandStatus Status
        {
            get { return status; }
            set { status = value; }
        }

        public void Dispose()
        {
            if (dest == null) return;
            if (attacking == true && attacktask != null) attacktask.Deactivate();
            attacktask = null;
            this.status = CommandStatus.FINISHED;
        }
    }
}
    
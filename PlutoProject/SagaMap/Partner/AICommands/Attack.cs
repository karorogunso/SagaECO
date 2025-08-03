using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaMap;
using SagaMap.Scripting;

namespace SagaMap.Partner.AICommands
{
    public class Attack : AICommand
    {
        private CommandStatus status;
        private PartnerAI partnerai;
        private Actor dest;
        private bool attacking;
        PartnerAttack attacktask;
        public bool active;
        short x, y;
        int counter = 0;

        public Attack(PartnerAI partnerai)
        {
            this.partnerai = partnerai;
            this.Status = CommandStatus.INIT;
            int aspd = 0;
            ActorPartner partner = (ActorPartner)this.partnerai.Partner;
            aspd = partner.Status.aspd;
            attacktask = new PartnerAttack(partnerai, dest);
            x = partnerai.Partner.X;
            y = partnerai.Partner.Y;
        }

        public string GetName() { return "Attack"; }

        private Actor CurrentTarget()
        {
            try
            {
                uint id = 0;
                uint hate = 0;
                Actor tmp = null;
                ActorPartner partner = (ActorPartner)partnerai.Partner;
                uint[] ids = new uint[partnerai.Hate.Keys.Count];
                partnerai.Hate.Keys.CopyTo(ids, 0);
                /*
                 * This is redundant because the partner doesn't need to search the map, which could cause weird movements. 
                 * The partner should only follow ActorPC or the target that ActorPC is attacking.
          

                for (uint i = 0; i < partnerai.Hate.Keys.Count; i++)//Find out the actorPC with the highest hate value
                {
                    if (ids[i] == 0) continue;
                    if (ids[i] == this.partnerai.Partner.ActorID)
                        continue;
                    if (this.partnerai.Master != null)
                    {
                        if (ids[i] == this.partnerai.Master.ActorID && partnerai.Hate.Count > 1)
                            continue;
                    }
                    if (!partnerai.Hate.ContainsKey(ids[i]))
                        continue;
                    if (hate < partnerai.Hate[ids[i]])
                    {
                        hate = partnerai.Hate[ids[i]];
                        id = ids[i];
                        tmp = partnerai.map.GetActor(id);
                        if (tmp == null)
                        {
                            partnerai.Hate.Remove(id);
                            id = 0;
                            hate = 0;
                            active = false;
                            i = 0;
                            continue;
                        }
                        active = true;
                    }
                }
                */

                id = partner.Owner.ActorID;

                if (partner.Owner.PartnerTartget != null && partner.ai_mode <= 1)
                {
                    id = partner.Owner.PartnerTartget.ActorID;
                }

                if (id != 0)//Now the id is refer to the PC with the highest hate to the Mob.现在这个ID是怪物对最高仇恨者的ID
                {
                    tmp = partnerai.map.GetActor(id);
                    if (tmp != null)
                    {
                        if (tmp.HP == 0)
                        {
                            partnerai.Hate.Remove(tmp.ActorID);
                            if (partner.Owner.PartnerTartget != null)
                                partner.Owner.PartnerTartget = null;
                            id = 0;
                            active = false;
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
            if (this.partnerai.Master != null)
            {
                if (!this.partnerai.Hate.ContainsKey(this.partnerai.Master.ActorID))
                    this.partnerai.Hate.Add(this.partnerai.Master.ActorID, 1);
                if (this.partnerai.Master.type == ActorType.PC)
                    isSlavaOfPc = true;
            }
            foreach (uint id in partnerai.Partner.VisibleActors)
            {
                Actor i = partnerai.map.GetActor(id);
                if (i == null)
                    continue;
                if (i.Buff.Transparent)
                    continue;
                if (i.MapID != this.partnerai.map.ID)
                    continue;
                if (i.Status.Additions.ContainsKey("IAmTree"))
                    continue;
                if (i.Status.Additions.ContainsKey("Through"))
                    continue;
                if (i.HP == 0)
                    continue;
                if (i.type == ActorType.MOB)
                {
                    ActorEventHandlers.MobEventHandler eh = (ActorEventHandlers.MobEventHandler)i.e;
                    if (eh.AI.Mode.Symbol && !isSlavaOfPc)
                    {
                        partnerai.Hate.Add(i.ActorID, 20);
                        //SendAggroEffect();
                    }
                }
                if (!partnerai.Partner.Buff.Zombie && i.type != ActorType.PC && i.type != ActorType.PET && i.type != ActorType.SHADOW && !(i.type == ActorType.MOB && isSlavaOfPc))
                    continue;
                if (isSlavaOfPc && i.type == ActorType.SHADOW)
                    continue;
                if (isSlavaOfPc && i.type == ActorType.PET)
                    continue;
                if (isSlavaOfPc && i.type == ActorType.PC)
                    continue;
                if (isSlavaOfPc && i.type == ActorType.MOB)
                {
                    ActorEventHandlers.MobEventHandler ie = (ActorEventHandlers.MobEventHandler)i.e;
                    if (ie.AI.Master == this.partnerai.Master)
                        continue;
                }
                if (i.type == ActorType.PC)
                {
                    if (((ActorPC)i).PossessionTarget != 0)
                        continue;
                }
                double len = PartnerAI.GetLengthD(i.X, i.Y, partnerai.Partner.X, partnerai.Partner.Y);
                if (len < distance)
                {
                    byte x, y, x2, y2;
                    x = Global.PosX16to8(this.partnerai.Partner.X, this.partnerai.map.Width);
                    y = Global.PosY16to8(this.partnerai.Partner.Y, this.partnerai.map.Height);
                    x2 = Global.PosX16to8(i.X, this.partnerai.map.Width);
                    y2 = Global.PosY16to8(i.Y, this.partnerai.map.Height);

                    List<MapNode> path = this.partnerai.FindPath(x, y, x2, y2);
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
            if (distance <= 1000)
            {
                if (partnerai.Hate.Count == 0)//保存怪物战斗前位置
                {
                    partnerai.X_pb = partnerai.Partner.X;
                    partnerai.Y_pb = partnerai.Partner.Y;
                }
                if (!partnerai.Hate.ContainsKey(target.ActorID))
                    partnerai.Hate.Add(target.ActorID, 20);
                //SendAggroEffect();   no need for the exclamation mark to appear above the partner's head
            }
        }

        void SendAggroEffect()
        {
            EffectArg arg = new EffectArg();
            arg.actorID = partnerai.Partner.ActorID;
            arg.effectID = 4539;
            partnerai.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, arg, partnerai.Partner, true);
        }

        private bool hasPlayerInSight()
        {
            foreach (uint id in partnerai.Partner.VisibleActors)
            {
                Actor i = partnerai.map.GetActor(id);
                if (i == null)
                    continue;
                if (i.MapID != this.partnerai.map.ID)
                    continue;
                if (i.type == ActorType.PC)
                {
                    if (((ActorPC)i).Online)
                        return true;
                }
            }
            return false;
        }

        public void Update(object para)
        {
            ActorPartner partner = null;
            if (this.partnerai.Partner.type == ActorType.PARTNER)
                partner = (ActorPartner)this.partnerai.Partner;
            if (this.partnerai.Hate.Count == 0)
            {
                if (!hasPlayerInSight())
                {
                    counter++;
                    if (counter > 100)
                    {
                        this.partnerai.Pause();
                        counter = 0;
                        return;
                    }
                }
            }


            if (partner != null)
                if (!partnerai.Hate.ContainsKey(partner.Owner.ActorID))
                    partnerai.Hate.Add(partner.Owner.ActorID, 1);
            if (partnerai.Master != null)
                if (!partnerai.Hate.ContainsKey(this.partnerai.Master.ActorID))
                    partnerai.Hate.Add(this.partnerai.Master.ActorID, 1);
            if (partnerai.Partner.Tasks.ContainsKey("AutoCast"))
            {
                if (attacktask.Activated == true) attacktask.Deactivate();
                attacking = false;
                return;
            }
            //施放主人战斗中技能，放在这个位置保证平时状态
            if ((DateTime.Now - partnerai.LastSkillCast).TotalSeconds >= 1)
            {
                if (partner != null)
                {
                    ActorPC pc = partner.Owner;
                    if (pc.BattleStatus == 1)
                    {
                        if (Global.Random.Next(0, 99) < partnerai.Mode.EventMasterCombatSkillRate)
                        {
                            partnerai.OnShouldCastSkill(partnerai.Mode.EventMasterCombat, pc);
                            partnerai.LastSkillCast = DateTime.Now;
                        }
                    }
                }
            }
            if (attacktask == null)
                attacktask = new PartnerAttack(partnerai, dest);
            dest = CurrentTarget();
            if ((partnerai.Mode.Active || partnerai.Partner.Buff.Zombie) && (dest == null || dest == partnerai.Master))
            {
                CheckAggro();
            }
            if (dest == null)
            {
                partnerai.AIActivity = Activity.IDLE;
                if (partnerai.commands.ContainsKey("Chase") == true)
                    partnerai.commands.Remove("Chase"); ;
                return;
            }
            partnerai.AIActivity = Activity.BUSY;
            if (partnerai.commands.ContainsKey("Move") == true) partnerai.commands.Remove("Move");

            attacktask.dActor = dest;
            if ((DateTime.Now - partnerai.LastSkillCast).TotalSeconds >= 10 && dest != partner.Owner)//施放技能，放在这个位置保证追踪模式下的技能优先
            {
                if (Global.Random.Next(0, 99) < partnerai.Mode.EventAttackingSkillRate)
                {
                    partnerai.OnShouldCastSkill(partnerai.Mode.EventAttacking, dest);
                    partnerai.LastSkillCast = DateTime.Now;
                }
            }
            if (partnerai.commands.ContainsKey("Chase") == true)
            {
                if (attacktask.Activated == true)
                    attacktask.Deactivate();
                attacking = false;
                return;
            }

            if (this.x != this.partnerai.Partner.X || this.y != this.partnerai.Partner.Y)
            {
                short x, y;
                this.partnerai.map.FindFreeCoord(this.partnerai.Partner.X, this.partnerai.Partner.Y, out x, out y, this.partnerai.Partner);
                bool skip = false;
                if (partnerai.Partner.type == ActorType.PET)
                {
                    if (((ActorPet)partnerai.Partner).BaseData.mobType == SagaDB.Mob.MobType.MAGIC_CREATURE)
                        skip = true;
                }
                if ((this.partnerai.Partner.X == x && this.partnerai.Partner.Y == y) || this.partnerai.Mode.RunAway || skip)
                {

                }
                else
                {
                    short[] dst = new short[2] { x, y };

                    partnerai.map.MoveActor(Map.MOVE_TYPE.START, partnerai.Partner, dst, PartnerAI.GetDir((short)(dst[0] - x), (short)(dst[1] - y)), (ushort)(partnerai.Partner.Speed / 20));
                    return;
                }
                this.x = partnerai.Partner.X;
                this.y = partnerai.Partner.Y;
            }
            if (dest.HP == 0)
            {
                if (partner != null)
                {
                    if (dest.ActorID != partner.Owner.ActorID)
                    {
                        if (partnerai.Hate.ContainsKey(dest.ActorID)) partnerai.Hate.Remove(dest.ActorID);
                        if (attacktask.Activated == true) attacktask.Deactivate();
                        attacktask = null;
                        return;
                    }
                }
                else
                {
                    if (partnerai.Hate.ContainsKey(dest.ActorID)) partnerai.Hate.Remove(dest.ActorID);
                    if (attacktask.Activated == true) attacktask.Deactivate();
                    attacktask = null;
                    return;
                }
            }
            float size;
            if (partnerai.Mode.isAnAI)
            {
                size = partnerai.needlen;
            }
            else if (partnerai.Partner.type != ActorType.PC)
            {
                size = ((ActorPartner)partnerai.Partner).BaseData.range;
            }
            else
                size = 1;
            bool ifChase = false;
            if (PartnerAI.GetLengthD(partnerai.Partner.X, partnerai.Partner.Y, dest.X, dest.Y) > (size * 150) || ifChase)
            {
                if (PartnerAI.GetLengthD(partnerai.Partner.X, partnerai.Partner.Y, dest.X, dest.Y) < 2000 && partner.TTime["攻击僵直"] < DateTime.Now)
                {
                    Chase chase = new Chase(this.partnerai, dest);
                    partnerai.commands.Add("Chase", chase);
                    if (attacktask.Activated == true) attacktask.Deactivate();
                    attacking = false;
                }
            }
            else
            {
                if (Global.Random.Next(0, 99) < 100)
                {
                    if (partnerai.CanAttack)
                    {
                        if (partner != null)
                        {
                            if (dest.ActorID == partner.Owner.ActorID)
                                return;
                        }
                        if (attacktask.Activated == false)
                            attacktask.Activate();
                        attacking = true;
                    }
                }
                else
                {
                    Chase chase = new Chase(this.partnerai, dest);
                    partnerai.commands.Add("Chase", chase);
                    if (attacktask.Activated == true) attacktask.Deactivate();
                    attacking = false;
                }
            }
            partnerai.Partner.e.OnUpdate(partnerai.Partner);
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

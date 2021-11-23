using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Skill;
using SagaMap.Manager;
using SagaMap.Skill;

namespace SagaMap.Partner
{
    public partial class PartnerAI
    {
        DateTime lastSkillCast = DateTime.Now;
        DateTime cannotAttack = DateTime.Now;
        public Actor lastAttacker;

        public DateTime LastSkillCast
        {
            get { return this.lastSkillCast; }
            set { this.lastSkillCast = value; }
        }

        public DateTime CannotAttack
        {
            get { return this.cannotAttack; }
            set { this.cannotAttack = value; }
        }

        //新增部分开始 by:TT
        Dictionary<uint, DateTime> skillCast = new Dictionary<uint, DateTime>();
        DateTime shortSkillTime = DateTime.Now;
        DateTime longSkillTime = DateTime.Now;
        public uint NextSurelySkillID = 0;
        int Sequence = 0;
        bool skillOK = false;
        /// <summary>
        /// AnAI的当前顺序
        /// </summary>
        uint NowSequence = 0;
        bool NeedNewSkillList = true;
        bool CastIsFinished = true;
        public float needlen = 1f;
        public bool skillisok = false;
        DateTime SkillWait = DateTime.Now;
        AIMode.SkillList Now_SkillList = new AIMode.SkillList();
        List<AIMode.SkillList> Temp_skillList = new List<AIMode.SkillList>();
        int SkillDelay = 0;
        public void OnShouldCastSkill_An(AIMode mode, Actor currentTarget)
        {
            try
            {
                if (this.Partner.Tasks.ContainsKey("SkillCast"))
                    return;

                #region 根据条件抽选技能列表
                if (NeedNewSkillList)
                {
                    int totalRate = 0;
                    int determinator = 0;
                    Now_SkillList = new AIMode.SkillList();
                    Temp_skillList = new List<AIMode.SkillList>();
                    foreach (KeyValuePair<uint, AIMode.SkillList> item in mode.AnAI_SkillAssemblage)
                    {
                        if (this.Partner.HP >= item.Value.MinHP * this.Partner.HP / 100 && this.Partner.HP <= item.Value.MaxHP * this.Partner.HP / 100)
                        {
                            totalRate += item.Value.Rate;
                            Temp_skillList.Add(item.Value);
                        }
                    }
                    int ran = Global.Random.Next(0, totalRate);
                    foreach (AIMode.SkillList item in Temp_skillList)
                    {
                        determinator += item.Rate;
                        if (ran <= determinator)
                        {
                            Now_SkillList = item;
                            break;
                        }
                    }
                    NeedNewSkillList = false;
                    Sequence = 1;
                }
                #endregion
                #region 按照顺序释放技能

                foreach (KeyValuePair<uint, AIMode.SkillsInfo> item in Now_SkillList.AnAI_SkillList)
                {
                    skillisok = false;
                    SagaDB.Skill.Skill skill = SkillFactory.Instance.GetSkill(item.Value.SkillID, 1);
                    if (GetLengthD(this.Partner.X, this.Partner.Y, currentTarget.X, currentTarget.Y) <= skill.Range * 145)
                        skillisok = true;
                    else
                    {
                        needlen = skill.Range;
                        needlen -= 1f;
                        if (needlen < 1f)
                            needlen = 1f;
                    }
                    if (item.Key >= Sequence)
                    {
                        //CannotAttack = DateTime.Now.AddMilliseconds(item.Value.Delay);
                        if (skillisok)
                        {
                            SkillDelay = item.Value.Delay;
                            CastIsFinished = false;
                            CastSkill(item.Value.SkillID, 1, currentTarget);
                            Sequence++;
                        }
                        else if (SkillWait <= DateTime.Now)
                        {
                                Sequence++;
                                SkillWait = DateTime.Now.AddSeconds(5);
                        }
                        
                        break;
                    }
                    if (Sequence > Now_SkillList.AnAI_SkillList.Count)
                    {
                        NeedNewSkillList = true;
                        break;
                    }
                    if (Now_SkillList.AnAI_SkillList.Count == 0)
                    {
                        NeedNewSkillList = true;
                        break;
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                SagaLib.Logger.ShowError(ex);
            }
        }
        public void OnShouldCastSkill_New(AIMode mode, Actor currentTarget)
        {
            if (this.Partner.Tasks.ContainsKey("SkillCast"))
                return;
            double len = GetLengthD(this.Partner.X, this.Partner.Y, currentTarget.X, currentTarget.Y) / 145;

            uint skillID = 0;
            int totalRate = 0;

            Dictionary<uint, AIMode.SkilInfo> temp_skillList = new Dictionary<uint, AIMode.SkilInfo>();
            Dictionary<uint, AIMode.SkilInfo> skillList = new Dictionary<uint, AIMode.SkilInfo>();

            if (mode.Distance < len && longSkillTime < DateTime.Now)
            {
                temp_skillList = mode.SkillOfLong;
                //远程
            }
            else if (shortSkillTime < DateTime.Now)
            {
                temp_skillList = mode.SkillOfShort;
                //近身
            }

            foreach (KeyValuePair<uint, AIMode.SkilInfo> i in temp_skillList)
            {
                int MaxHPLimit = (int)(this.Partner.MaxHP * (i.Value.MaxHP * 0.01f)) + 1;
                int MinHPLimit = (int)(this.Partner.MaxHP * (i.Value.MinHP * 0.01f)) + 1;
                if (MaxHPLimit >= this.Partner.HP && MinHPLimit <= this.Partner.HP)
                {
                    if (skillCast.ContainsKey(i.Key))
                    {
                        if (skillCast[i.Key] < DateTime.Now)
                        {
                            skillList.Add(i.Key, i.Value);
                            skillCast.Remove(i.Key);
                        }
                    }
                    else
                    {
                        skillList.Add(i.Key, i.Value);
                    }
                }
            }

            foreach (AIMode.SkilInfo i in skillList.Values)
            {
                totalRate += i.Rate;
            }
            int ran = 0;
            if(totalRate > 1)
            ran = Global.Random.Next(0, totalRate);
            int determinator = 0;

            foreach (uint i in skillList.Keys)
            {
                determinator += skillList[i].Rate;
                if (ran <= determinator)
                {
                    skillID = i;
                    break;
                }
            }
            if (NextSurelySkillID != 0)
            {
                skillID = NextSurelySkillID;
                NextSurelySkillID = 0;
            }
            //释放技能
            if (skillID != 0)
            {
                CastSkill(skillID, 1, currentTarget);
                if (skillOK)
                {
                    try
                    {
                        skillCast.Add(skillID, DateTime.Now.AddSeconds(skillList[skillID].CD));
                    }
                    catch {
                      
                    }
                    if (mode.Distance < len)
                    {
                        longSkillTime = DateTime.Now.AddSeconds(mode.LongCD);
                        //远程
                    }
                    else
                    {
                        shortSkillTime = DateTime.Now.AddSeconds(mode.ShortCD);
                        //近身
                    }
                    skillOK = false;
                }
            }
        }
        //新增结束

        public void OnShouldCastSkill(Dictionary<uint, int> skillList, Actor currentTarget)
        {
            if (!this.Partner.Tasks.ContainsKey("SkillCast") && skillList.Count > 0)
            {
                //确定释放的技能
                uint skillID = 0;
                int totalRate = 0;
                foreach (int i in skillList.Values)
                {
                    totalRate += i;
                }
                int ran = Global.Random.Next(0, totalRate);
                int determinator = 0;

                foreach (uint i in skillList.Keys)
                {
                    determinator += skillList[i];
                    if (ran <= determinator)
                    {
                        skillID = i;
                        break;
                    }
                }

                //释放技能
                if (skillID != 0)
                {
                    //Partner.TTime["攻击僵直"] = DateTime.Now + new TimeSpan(0, 0, 0, 3);
                    CastSkill(skillID, 1, currentTarget);
                }
            }
        }

        public void CastSkill(uint skillID, byte lv, uint target, short x, short y)
        {
            SagaDB.Skill.Skill skill = SkillFactory.Instance.GetSkill(skillID, lv);
            if (skill == null)
                return;
            if (!CanUseSkill)
                return;
            SkillArg arg = new SkillArg();
            arg.sActor = this.Partner.ActorID;

            if (target != 0xFFFFFFFF)
            {
                Actor dactor = this.map.GetActor(target);
                if (dactor == null)
                {
                    if (this.Partner.Tasks.ContainsKey("AutoCast"))
                    {
                        this.Partner.Tasks.Remove("AutoCast");
                        this.Partner.Buff.CannotMove = false;
                    } 
                    return;
                }

                if (GetLengthD(this.Partner.X, this.Partner.Y, dactor.X, dactor.Y) <= skill.Range * 145)
                {
                    if (skill.Target == 2)
                    {
                        //如果是辅助技能
                        if (skill.Support)
                        {
                            if (this.Partner.type == ActorType.PET)
                            {
                                ActorPet pet = (ActorPet)this.Partner;
                                if (pet.Owner != null)
                                    arg.dActor = pet.Owner.ActorID;
                                else
                                    arg.dActor = this.Partner.ActorID;
                            }
                            else
                            {
                                if (this.master == null)
                                    arg.dActor = this.Partner.ActorID;
                                else
                                    arg.dActor = this.master.ActorID;
                            }
                        }
                        else
                            arg.dActor = target;
                    }
                    else if (skill.Target == 1)
                    {
                        if (this.Partner.type == ActorType.PET)
                        {
                            ActorPet pet = (ActorPet)this.Partner;
                            if (pet.Owner != null)
                                arg.dActor = pet.Owner.ActorID;
                            else
                                arg.dActor = this.Partner.ActorID;
                        }
                        else
                            arg.dActor = this.Partner.ActorID;
                    }
                    else
                        arg.dActor = 0xFFFFFFFF;

                    if (arg.dActor != 0xFFFFFFFF)
                    {
                        Actor dst = map.GetActor(arg.dActor);
                        if (dst != null)
                        {
                            if (dst.Buff.Dead != skill.DeadOnly)
                            {
                                if (this.Partner.Tasks.ContainsKey("AutoCast"))
                                {
                                    this.Partner.Tasks.Remove("AutoCast");
                                    this.Partner.Buff.CannotMove = false;
                                }
                                return;
                            }
                        }
                        else
                        {
                            if (this.Partner.Tasks.ContainsKey("AutoCast"))
                            {
                                this.Partner.Tasks.Remove("AutoCast");
                                this.Partner.Buff.CannotMove = false;
                            }
                            return;
                        }
                    }

                    if (this.master != null)
                    {
                        if ((this.master.ActorID == target) && !skill.Support)
                        {
                            if (this.Partner.Tasks.ContainsKey("AutoCast"))
                            {
                                this.Partner.Tasks.Remove("AutoCast");
                                this.Partner.Buff.CannotMove = false;
                            }
                            return;
                        }
                    }

                    arg.skill = skill;
                    arg.x = Global.PosX16to8(x, this.map.Width);
                    arg.y = Global.PosY16to8(y, this.map.Height);
                    arg.argType = SkillArg.ArgType.Cast;

                    //arg.delay = (uint)(skill.CastTime * (1f - this.Mob.Status.cspd / 1000f));//怪物技能吟唱时间
                    arg.delay = (uint)skill.CastTime;
                }
                else
                {
                    if (this.Partner.Tasks.ContainsKey("AutoCast"))
                    {
                        this.Partner.Tasks.Remove("AutoCast");
                        this.Partner.Buff.CannotMove = false;
                    }                     
                    return;
                }
            }
            else
            {
                arg.dActor = 0xFFFFFFFF;
                if (GetLengthD(this.Partner.X, this.Partner.Y, x, y) <= skill.CastRange * 145)
                {
                    arg.skill = skill;
                    arg.x = Global.PosX16to8(x, this.map.Width);
                    arg.y = Global.PosY16to8(x, this.map.Height);
                    arg.argType = SkillArg.ArgType.Cast;

                    //arg.delay = (uint)(skill.CastTime * (1f - this.Mob.Status.cspd / 1000f));
                    arg.delay = (uint)skill.CastTime;
                }
                else
                {
                    if (this.Partner.Tasks.ContainsKey("AutoCast"))
                    {
                        this.Partner.Tasks.Remove("AutoCast");
                        this.Partner.Buff.CannotMove = false;
                    }
                    return;
                }
            }
            this.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, arg, this.Partner, false);
            if (skill.CastTime > 0)
            {
                if (SkillHandler.Instance.MobskillHandlers.ContainsKey(arg.skill.ID))
                {
                    Actor dactor = this.map.GetActor(target);
                    SkillHandler.Instance.MobskillHandlers[arg.skill.ID].BeforeCast(this.Partner, dactor, arg, lv);
                }
                Tasks.Partner.SkillCast task = new SagaMap.Tasks.Partner.SkillCast(this, arg);
                this.Partner.Tasks.Add("SkillCast", task);

                task.Activate();
            }
            else
            {
                OnSkillCastComplete(arg);
            }
            skillOK = true;
        }

        public void CastSkill(uint skillID, byte lv, Actor currentTarget)
        {
            CastSkill(skillID, lv, currentTarget.ActorID, currentTarget.X, currentTarget.Y);
        }

        public void CastSkill(uint skillID, byte lv, short x, short y)
        {
            CastSkill(skillID, lv, 0xFFFFFFFF, x, y);
        }

        public void AttackActor(uint actorID)
        {
            if (this.Hate.ContainsKey(actorID))
                this.Hate[actorID] = this.Partner.MaxHP;
            else
                this.Hate.Add(actorID, this.Partner.MaxHP);
        }

        public void StopAttacking()
        {
            this.Hate.Clear();
        }

        public void OnSkillCastComplete(SkillArg skill)
        {
            if(this.Mode.isAnAI)
            {
                CannotAttack = DateTime.Now.AddMilliseconds(SkillDelay);
                SkillDelay = 0;
                CastIsFinished = true;
            }
            if (skill.dActor != 0xFFFFFFFF)
            {
                Actor dActor = this.map.GetActor(skill.dActor);
                if (dActor != null)
                {
                    skill.argType = SkillArg.ArgType.Active;
                    SkillHandler.Instance.SkillCast(this.Partner, dActor, skill);
                }
            }
            else
            {
                skill.argType = SkillArg.ArgType.Active;
                SkillHandler.Instance.SkillCast(this.Partner, this.Partner, skill);
            }

            if (this.Partner.type == ActorType.PET)
                SkillHandler.Instance.ProcessPetGrowth(this.Partner, PetGrowthReason.UseSkill);
            this.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, skill, this.Partner, false);
            if (skill.skill.Effect != 0)
            {
                EffectArg eff = new EffectArg();
                eff.actorID = skill.dActor;
                eff.effectID = skill.skill.Effect;
                eff.x = skill.x;
                eff.y = skill.y;
                this.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, eff, this.Partner, false);
            }
            Partner.TTime["攻击僵直"] = DateTime.Now + new TimeSpan(0, 0, 0, 0,500);
            if (this.Partner.Tasks.ContainsKey("AutoCast"))
                this.Partner.Tasks["AutoCast"].Activate();
            else
            {
                if (skill.autoCast.Count != 0)
                {
                    this.Partner.Buff.CannotMove = true;
                    this.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, this.Partner, true);
                    Tasks.Skill.AutoCast task = new SagaMap.Tasks.Skill.AutoCast(this.Partner, skill);
                    this.Partner.Tasks.Add("AutoCast", task);
                    task.Activate();
                }
            }
        }

        public void OnPathInterupt()
        {
            if (commands.ContainsKey("Move"))
            {
                AICommands.Move command = (AICommands.Move)commands["Move"];
                command.FindPath();
            }

            if (commands.ContainsKey("Chase"))
            {
                AICommands.Chase command = (AICommands.Chase)commands["Chase"];
                command.FindPath();
            }
        }

        public void OnAttacked(Actor sActor, int damage)
        {
            if (this.actor.Buff.Dead)
                return;
            if (this.Activated == false)
            {
                this.Start();
            }
            if (sActor.ActorID == this.actor.ActorID)
                return;
            lastAttacker = sActor;
            uint tmp = (uint)(Math.Abs(damage));
            if (sActor.type == ActorType.PC)
            {
                if (((ActorPC)sActor).Skills2.ContainsKey(612))
                {
                    float rate_add = 0.1f * ((ActorPC)sActor).Skills2[612].Level;
                    tmp += (uint)(tmp * rate_add);
                }
                if (sActor.Status.Nooheito_rate > 0)//弓3转13级技能
                    tmp -= (uint)(tmp * (sActor.Status.Nooheito_rate / 100));
                if (sActor.Status.HatredUp_rate > 0)//骑士3转13级技能
                    tmp += (uint)(tmp * (sActor.Status.HatredUp_rate / 100));
            }
            if (this.Hate.ContainsKey(sActor.ActorID))
            {
                if (tmp == 0)
                    tmp = 1;
                this.Hate[sActor.ActorID] += tmp;
            }
            else
            {
                if (tmp == 0)
                    tmp = 1;
                if (this.Hate.Count == 0)//保存怪物战斗前位置
                {
                    this.X_pb = this.actor.X;
                    this.Y_pb = this.actor.Y;
                }
                this.Hate.Add(sActor.ActorID, tmp);
            }
            if (damage > 0)
            {

                if (this.DamageTable.ContainsKey(sActor.ActorID))
                {
                    this.DamageTable[sActor.ActorID] += damage;
                }
                else this.DamageTable.Add(sActor.ActorID, damage);
                if (this.DamageTable[sActor.ActorID] > Partner.MaxHP)
                    this.DamageTable[sActor.ActorID] = (int)Partner.MaxHP;
            }            

            if (firstAttacker != null)
            {
                if (firstAttacker == sActor)
                {
                    attackStamp = DateTime.Now;
                }
                else
                {
                    if ((DateTime.Now - attackStamp).TotalMinutes > 15)
                    {
                        firstAttacker = sActor;
                        attackStamp = DateTime.Now;
                    }
                }
            }
            else
            {
                firstAttacker = sActor;
                attackStamp = DateTime.Now; 
            }
        }

        public void OnSeenSkillUse(SkillArg arg)
        {
            if (map == null)
            {
                Logger.ShowWarning(string.Format("Mob:{0}({1})'s map is null!", this.Partner.ActorID, Partner.Name));
                return;
            }
            if (master != null)
            {
                for (int i = 0; i < arg.affectedActors.Count; i++)
                {
                    if (arg.affectedActors[i].ActorID == master.ActorID)
                    {
                        Actor actor = map.GetActor(arg.sActor);
                        if (actor != null)
                        {
                            this.OnAttacked(actor, arg.hp[i]);
                            if (this.Hate.Count == 1)
                                SendAggroEffect();
                        }
                    }
                }
            }
            if (this.Mode.HateHeal)
            {
                Actor actor = map.GetActor(arg.sActor);
                if (actor != null && arg.skill != null && this.Hate.Count > 0)
                {
                    if (arg.skill.Support && actor.type == ActorType.PC)
                    {
                        int damage = 0;
                        foreach (int i in arg.hp)
                        {
                            damage += -i;
                        }
                        if (damage > 0)
                        {
                            if (this.Hate.Count == 0)
                                SendAggroEffect();
                            this.OnAttacked(actor, (int)(damage));
                        }
                    }
                }
            }
            if (arg.skill != null)
            {
                if (arg.skill.Support && !this.Mode.HateHeal)
                {
                    Actor actor = map.GetActor(arg.sActor);
                    if (actor.type == ActorType.PC)
                    {
                        int damage = 0;
                        foreach (int i in arg.hp)
                        {
                            damage += -i * 2;
                        }
                        if (this.DamageTable.ContainsKey(actor.ActorID))
                        {
                            this.DamageTable[actor.ActorID] += damage;
                        }
                        else this.DamageTable.Add(actor.ActorID, damage);
                        if (this.DamageTable[actor.ActorID] > Partner.MaxHP)
                            this.DamageTable[actor.ActorID] = (int)Partner.MaxHP;
                    }
                }
                else if(arg.skill.ID == 3055)//复活
                {
                    Actor actor = map.GetActor(arg.sActor);
                    Actor dActor = map.GetActor(arg.dActor);
                    if (actor.type == ActorType.PC && dActor.type == ActorType.PC && actor !=null && dActor !=null)
                    {
                        int damage = 0;
                        damage = (int)dActor.MaxHP * 2;
                        if (this.DamageTable.ContainsKey(actor.ActorID))
                        {
                            this.DamageTable[actor.ActorID] += damage;
                        }
                        else this.DamageTable.Add(actor.ActorID, damage);
                        if (this.DamageTable[actor.ActorID] > Partner.MaxHP)
                            this.DamageTable[actor.ActorID] = (int)Partner.MaxHP;
                    }
                }
            }
            if (this.Mode.HateMagic)
            {
                Actor actor = map.GetActor(arg.sActor);
                if (actor != null && arg.skill != null)
                {
                    if (arg.skill.Magical)
                    {
                        if (actor.type == ActorType.PC)
                        {
                            if (this.Hate.Count == 0)
                                SendAggroEffect();
                            this.OnAttacked(actor, (int)(this.Partner.MaxHP / 10));
                        }
                    }
                }
            }
        }

        void SendAggroEffect()
        {
            EffectArg arg = new EffectArg();
            arg.actorID = this.Partner.ActorID;
            arg.effectID = 4539;
            this.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, arg, this.Partner, false);
        }

    }
}

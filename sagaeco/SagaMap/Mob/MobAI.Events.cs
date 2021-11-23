using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Skill;
using SagaMap.Manager;
using SagaMap.Skill;

namespace SagaMap.Mob
{
    public partial class MobAI
    {
        DateTime lastSkillCast = DateTime.Now;
        DateTime cannotAttack = DateTime.Now;
        Actor lastattacker;
        public Actor LastAttacker
        {
            set
            {
                //只有在玩家攻击时才赋值，确保该变量为非null
                if (value != null && value.type == ActorType.PARTNER)
                    lastattacker = ((ActorPartner)value).Owner;
                if (value != null && value.type == ActorType.PC)
                    lastattacker = value;
            }
            get
            {
                return lastattacker;
            }
        }
        public bool noreturn = false;

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
        public bool notInitialize = false;
        //新增部分开始 by:TT
        Dictionary<uint, DateTime> skillCast = new Dictionary<uint, DateTime>();
        DateTime shortSkillTime = DateTime.Now;
        DateTime longSkillTime = DateTime.Now;
        //public uint NextSurelySkillID = 0;
        int Sequence = 0;
        bool skillOK = false;
        List<int> skillOfHP = new List<int>();
        public void SkillOfHPClear()
        { skillOfHP.Clear(); }
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
                if (this.Mob.Tasks.ContainsKey("SkillCast"))
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
                        if (this.Mob.HP >= item.Value.MinHP * this.Mob.HP / 100 && this.Mob.HP <= item.Value.MaxHP * this.Mob.HP / 100)
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
                    if (GetLengthD(this.Mob.X, this.Mob.Y, currentTarget.X, currentTarget.Y) <= skill.Range * 145)
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
            if (this.Mob.Tasks.ContainsKey("SkillCast"))
                return;
            if (mode.SkillOfHP.Count > 0 && mode.SkillOfHP.Count > skillOfHP.Count)
            {
                uint id = 0;
                int hp = 0;
                foreach (var i in mode.SkillOfHP)
                {
                    if (hp < i.Key && !skillOfHP.Contains(i.Key))
                    {
                        hp = i.Key;
                        id = i.Value;
                    }
                }
                if (hp > 0)
                {
                    uint mobHP = (this.Mob.HP * 100) / this.Mob.MaxHP;
                    if (mobHP <= hp)
                    {
                        CastSkill(id, 1, currentTarget);
                        skillOfHP.Add(hp);
                        return;
                    }
                }
            }
            double len = GetLengthD(this.Mob.X, this.Mob.Y, currentTarget.X, currentTarget.Y) / 145;

            uint skillID = 0;
            int totalRate = 0;

            Dictionary<uint, AIMode.SkilInfo> temp_skillList = new Dictionary<uint, AIMode.SkilInfo>();
            Dictionary<uint, AIMode.SkilInfo> skillList = new Dictionary<uint, AIMode.SkilInfo>();
            int usedtime = (int)(DateTime.Now - Mob.BattleStartTime).TotalSeconds;

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
                int MaxHPLimit = (int)(this.Mob.MaxHP * (i.Value.MaxHP * 0.01f)) + 1;
                int MinHPLimit = (int)(this.Mob.MaxHP * (i.Value.MinHP * 0.01f)) + 1;
                if (MaxHPLimit >= this.Mob.HP && MinHPLimit <= this.Mob.HP)
                {
                    if (usedtime > i.Value.OverTime)
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
            }

            foreach (AIMode.SkilInfo i in skillList.Values)
            {
                totalRate += i.Rate;
            }
            int ran = 0;
            if (totalRate > 1)
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
            if(Mob.TInt["NextSurelySkillID"] != 0)
            {
                uint sID = (uint)Mob.TInt["NextSurelySkillID"];
                if (skillCast.ContainsKey(sID))
                {
                    if (skillCast[sID] < DateTime.Now)
                    {
                        skillCast.Remove(sID);
                        skillID = sID;
                    }
                }
                else
                    skillID = sID;
                Mob.TInt["NextSurelySkillID"] = 0;
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
                    catch (Exception ex)
                    {
                        Logger.ShowError(ex);
                    }
                    longSkillTime = DateTime.Now.AddSeconds(mode.LongCD);
                    //远程
                    shortSkillTime = DateTime.Now.AddSeconds(mode.ShortCD);
                    //近身 CD在使用一个技能后同时增加。
                    skillOK = false;
                }
            }
        }
        //新增结束

        public void OnShouldCastSkill(Dictionary<uint, int> skillList, Actor currentTarget)
        {
            if (!this.Mob.Tasks.ContainsKey("SkillCast") && skillList.Count > 0)
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
            arg.sActor = this.Mob.ActorID;

            if (target != 0xFFFFFFFF)
            {
                Actor dactor = this.map.GetActor(target);
                if (dactor == null)
                {
                    if (this.Mob.Tasks.ContainsKey("AutoCast"))
                    {
                        this.Mob.Tasks.Remove("AutoCast");
                        this.Mob.Buff.CannotMove = false;
                    } 
                    return;
                }

               if (GetLengthD(this.Mob.X, this.Mob.Y, dactor.X, dactor.Y) <= skill.Range * 145)
                {
                    if (skill.Target == 2)
                    {
                        //如果是辅助技能
                        if (skill.Support)
                        {
                            if (this.Mob.type == ActorType.PET)
                            {
                                ActorPet pet = (ActorPet)this.Mob;
                                if (pet.Owner != null)
                                    arg.dActor = pet.Owner.ActorID;
                                else
                                    arg.dActor = this.Mob.ActorID;
                            }
                            else
                            {
                                if (this.master == null)
                                    arg.dActor = this.Mob.ActorID;
                                else
                                    arg.dActor = this.master.ActorID;
                            }
                        }
                        else
                            arg.dActor = target;
                    }
                    else if (skill.Target == 1)
                    {
                        if (this.Mob.type == ActorType.PET)
                        {
                            ActorPet pet = (ActorPet)this.Mob;
                            if (pet.Owner != null)
                                arg.dActor = pet.Owner.ActorID;
                            else
                                arg.dActor = this.Mob.ActorID;
                        }
                        else
                            arg.dActor = this.Mob.ActorID;
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
                                if (this.Mob.Tasks.ContainsKey("AutoCast"))
                                {
                                    this.Mob.Tasks.Remove("AutoCast");
                                    this.Mob.Buff.CannotMove = false;
                                }
                                return;
                            }
                        }
                        else
                        {
                            if (this.Mob.Tasks.ContainsKey("AutoCast"))
                            {
                                this.Mob.Tasks.Remove("AutoCast");
                                this.Mob.Buff.CannotMove = false;
                            }
                            return;
                        }
                    }

                    if (this.master != null)
                    {
                        if ((this.master.ActorID == target) && !skill.Support)
                        {
                            if (this.Mob.Tasks.ContainsKey("AutoCast"))
                            {
                                this.Mob.Tasks.Remove("AutoCast");
                                this.Mob.Buff.CannotMove = false;
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
                    if (Mob.Status.Additions.ContainsKey("焚烬之火"))   //特定情况下，怪物使用技能不需要吟唱
                        arg.delay = 0;
                }
                else
                {
                    if (this.Mob.Tasks.ContainsKey("AutoCast"))
                    {
                        this.Mob.Tasks.Remove("AutoCast");
                        this.Mob.Buff.CannotMove = false;
                    }                     
                    return;
                }
            }
            else
            {
                arg.dActor = 0xFFFFFFFF;
                if (GetLengthD(this.Mob.X, this.Mob.Y, x, y) <= skill.CastRange * 145)
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
                    if (this.Mob.Tasks.ContainsKey("AutoCast"))
                    {
                        this.Mob.Tasks.Remove("AutoCast");
                        this.Mob.Buff.CannotMove = false;
                    }
                    return;
                }
            }
            this.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, arg, this.Mob, false);
            if (skill.CastTime > 0)
            {
                if (SkillHandler.Instance.MobskillHandlers.ContainsKey(arg.skill.ID))
                {
                    Actor dactor = this.map.GetActor(target);
                    SkillHandler.Instance.MobskillHandlers[arg.skill.ID].BeforeCast(this.Mob, dactor, arg, lv);
                }
                if (skill.BaseData.flag.Test(SkillFlags.NO_INTERRUPT))
                    actor.TInt["CanNotInterrupted"] = 1;
                else
                    actor.TInt["CanNotInterrupted"] = 0;
                Tasks.Mob.SkillCast task = new SagaMap.Tasks.Mob.SkillCast(this, arg);
                this.Mob.Tasks.Add("SkillCast", task);

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
                this.Hate[actorID] = this.Mob.MaxHP;
            else
                this.Hate.Add(actorID, this.Mob.MaxHP);
        }
        public Actor HighestActor()
        {
            try
            {
                uint id = 0;
                uint hate = 0;
                Actor tmp = null;
                uint[] ids = new uint[this.Hate.Keys.Count];
                this.Hate.Keys.CopyTo(ids, 0);
                for (uint i = 0; i < this.Hate.Keys.Count; i++)//Find out the actorPC with the highest hate value
                {
                    if (ids[i] == 0) continue;
                    if (ids[i] == this.Mob.ActorID)
                        continue;
                    if (this.Master != null)
                    {
                        if (ids[i] == this.Master.ActorID && this.Hate.Count > 1)
                            continue;
                    }
                    if (!this.Hate.ContainsKey(ids[i]))
                        continue;
                    if (hate < this.Hate[ids[i]])
                    {
                        hate = this.Hate[ids[i]];
                        id = ids[i];
                        tmp = this.map.GetActor(id);
                        if (tmp == null)
                        {
                            this.Hate.Remove(id);
                            id = 0;
                            hate = 0;
                            i = 0;
                            continue;
                        }
                        if (tmp.Status.Additions.ContainsKey("Hiding"))
                        {
                            this.Hate.Remove(id);
                            continue;
                        }
                        if (tmp.Status.Additions.ContainsKey("Through"))
                        {
                            this.Hate.Remove(id);
                            continue;
                        }
                        if (tmp.Status.Additions.ContainsKey("IAmTree"))
                        {
                            this.Hate.Remove(id);
                            continue;
                        }

                        if (tmp.type == ActorType.PC && this.Mob.type != ActorType.PET)
                        {
                            if (((ActorPC)tmp).PossessionTarget != 0)
                            {
                                this.Hate.Remove(id);
                                id = 0;
                                hate = 0;
                                i = 0;
                            }
                        }
                    }
                }
                if (id != 0)//Now the id is refer to the PC with the highest hate to the Mob.现在这个ID是怪物对最高仇恨者的ID
                {
                    tmp = this.map.GetActor(id);
                    if (tmp != null)
                    {
                        if (tmp.HP == 0)
                        {
                            this.Hate.Remove(tmp.ActorID);
                            id = 0;
                        }
                    }
                }
                if (id == 0)
                {
                    return null;
                }
                return tmp;
            }
            catch (Exception ex)
            {
                SagaLib.Logger.ShowError(ex);
                return null;
            }
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
                    SkillHandler.Instance.SkillCast(this.Mob, dActor, skill);
                }
            }
            else
            {
                skill.argType = SkillArg.ArgType.Active;
                SkillHandler.Instance.SkillCast(this.Mob, this.Mob, skill);
            }

            if (this.Mob.type == ActorType.PET)
                SkillHandler.Instance.ProcessPetGrowth(this.Mob, PetGrowthReason.UseSkill);
            this.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, skill, this.Mob, false);
            if (skill.skill.Effect != 0)
            {
                EffectArg eff = new EffectArg();
                eff.actorID = skill.dActor;
                eff.effectID = skill.skill.Effect;
                eff.x = skill.x;
                eff.y = skill.y;
                this.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, eff, this.Mob, false);
            }

            if (this.Mob.Tasks.ContainsKey("AutoCast"))
                this.Mob.Tasks["AutoCast"].Activate();
            else
            {
                if (skill.autoCast.Count != 0)
                {
                    this.Mob.Buff.CannotMove = true;
                    this.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, this.Mob, true);
                    Tasks.Skill.AutoCast task = new SagaMap.Tasks.Skill.AutoCast(this.Mob, skill);
                    this.Mob.Tasks.Add("AutoCast", task);
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
            LastAttacker = sActor;
            uint tmp = (uint)(Math.Abs(damage));
            if (sActor.type == ActorType.PC)
                tmp = (uint)(tmp * sActor.Status.HateRate);

            uint HateTargetId = sActor.ActorID;  //误导状态下，这个ID将会是误导目标ID，否则为攻击者ID。

            if (sActor.Status.Additions.ContainsKey("误导") && sActor.TInt["误导"] != 0 && map.GetActor((uint)sActor.TInt["误导"]) != null)  //如果误导的目标存在的话。
            {
                //这部分很可能需要更详细的逻辑，例如 误导的目标不能是partner，误导的目标必须可以被攻击等
                HateTargetId = (uint)sActor.TInt["误导"];
            }

            if (this.Hate.ContainsKey(HateTargetId))
            {
                if (tmp == 0)
                    tmp = 1;
                this.Hate[HateTargetId] += tmp;
            }
            else
            {
                if (tmp == 0)
                    tmp = 1;
                if (this.Hate.Count == 0)//保存怪物战斗前位置
                {
                    Mob.BattleStartTime = DateTime.Now;
                    this.X_pb = this.actor.X;
                    this.Y_pb = this.actor.Y;
                }
                this.Hate.Add(HateTargetId, tmp);
            }

            if (damage > 0)
            {

                if (this.DamageTable.ContainsKey(sActor.ActorID))
                {
                    this.DamageTable[sActor.ActorID] += damage;
                }
                else this.DamageTable.Add(sActor.ActorID, damage);
                if (this.DamageTable[sActor.ActorID] > Mob.MaxHP)
                    this.DamageTable[sActor.ActorID] = (int)Mob.MaxHP;
            }
            Actor fa = sActor;
            if (sActor.type == ActorType.PARTNER)
                fa = ((ActorPartner)sActor).Owner;
            if (firstAttacker != null)
            {
                if (firstAttacker == fa)
                {
                    attackStamp = DateTime.Now;
                    if(firstAttacker.type != ActorType.GOLEM)
                        firstAttacker = fa;
                }
                else
                {
                    if ((DateTime.Now - attackStamp).TotalMinutes > 15)
                    {
                        firstAttacker = fa;
                        attackStamp = DateTime.Now;
                    }
                }
            }
            else
            {
                firstAttacker = fa;
                attackStamp = DateTime.Now;
            }
        }

        public void OnSeenSkillUse(SkillArg arg)
        {
            if (map == null)
            {
                Logger.ShowWarning(string.Format("Mob:{0}({1})'s map is null!", this.Mob.ActorID, Mob.Name));
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
            if (this.Mode.HelpSameType)
            {
                Actor actor;
                ActorMob mob;
                if (this.actor.type == ActorType.MOB)
                {
                    mob = (ActorMob)this.Mob;
                    for (int i = 0; i < arg.affectedActors.Count; i++)
                    {
                        actor = arg.affectedActors[i];
                        if (actor.type == ActorType.MOB)
                        {
                            ActorMob tar = (ActorMob)actor;

                            if (tar.BaseData.mobType == mob.BaseData.mobType)
                            {
                                actor = map.GetActor(arg.sActor);
                                if (actor != null)
                                {
                                    if (actor.type == ActorType.PC)
                                    {
                                        if (this.Hate.Count == 0)
                                            SendAggroEffect();
                                        this.OnAttacked(actor, arg.hp[i]);
                                    }
                                }
                            }
                        }
                    }
                    actor = map.GetActor(arg.sActor);
                    if (actor != null)
                    {
                        if (actor.type == ActorType.MOB)
                        {
                            ActorMob tar = (ActorMob)actor;
                            if (tar.BaseData.mobType == mob.BaseData.mobType)
                            {
                                foreach (Actor i in arg.affectedActors)
                                {
                                    if (i.type != ActorType.PC)
                                        continue;
                                    if (this.Hate.Count == 0)
                                        SendAggroEffect();
                                    this.OnAttacked(i, 10);
                                }
                            }
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
                            if (this.DamageTable[actor.ActorID] > Mob.MaxHP)
                                this.DamageTable[actor.ActorID] = (int)Mob.MaxHP;
                        }
                        //else this.DamageTable.Add(actor.ActorID, damage);

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
                        if (this.DamageTable[actor.ActorID] > Mob.MaxHP)
                            this.DamageTable[actor.ActorID] = (int)Mob.MaxHP;
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
                            this.OnAttacked(actor, (int)(this.Mob.MaxHP / 10));
                        }
                    }
                }
            }
        }

        void SendAggroEffect()
        {
            EffectArg arg = new EffectArg();
            arg.actorID = this.Mob.ActorID;
            arg.effectID = 4539;
            this.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, arg, this.Mob, false);
        }

    }
}

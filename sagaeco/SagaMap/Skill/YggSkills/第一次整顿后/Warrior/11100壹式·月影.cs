using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S11100 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (SkillHandler.Instance.CheckSkillCanCastForWeapon(pc, args))
                return 0;
            return -5;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            dActor = SkillHandler.Instance.GetdActor(sActor,args);
            if (dActor == null) return;

            ActorPC Me = (ActorPC)sActor;

            if (sActor.SkillCombo == null)
                sActor.SkillCombo = new List<int>();
            float factor = 1.6f + 0.4f * level;

            if(sActor.Status.Additions.ContainsKey("刀剑宗师"))
                factor += factor * sActor.TInt["刀剑宗师提升%"] / 100f;

            if (sActor.Status.Additions.ContainsKey("连段重置"))
            {
                Addition 连段重置 = sActor.Status.Additions["连段重置"];
                TimeSpan span = new TimeSpan(0, 0, 0, 0, 10000);
                ((OtherAddition)连段重置).endTime = DateTime.Now + span;
            }
            else
            {
                OtherAddition 连段重置 = new OtherAddition(args.skill, Me, "连段重置", 30000);
                连段重置.OnAdditionEnd += (s, e) =>
                {
                    if (sActor.SkillCombo != null)
                    sActor.SkillCombo.Clear();
                    sActor.SkillCombo = null;
                    SkillHandler.Instance.ShowEffectOnActor(sActor, 5077);
                    Network.Client.MapClient.FromActorPC((ActorPC)sActor).SendSystemMessage("连段重置了。");

                };
                SkillHandler.ApplyAddition(sActor, 连段重置);
            }

            List<int> Combo = sActor.SkillCombo;
            List<int> 雲切 = new List<int>(){ 2, 3 };
            List<int> 袈裟斩 = new List<int>() { 2, 3, 2, 3 };
            List<int> 八刀一闪1 = new List<int>() { 2, 2, 3, 2, 3, 2, 3, 3 };
            List<int> 八刀一闪2 = new List<int>() { 3, 3, 2, 3, 2, 3, 2, 3 };
            if (ListEquals(Combo, 雲切) && !Me.Status.Additions.ContainsKey("雲切CD") && Me.Skills.ContainsKey(11002))
            {
                if(Me.CInt["雲切任务"] == 4 && Me.CInt["雲切任务条件"] < 777)
                {
                    Me.CInt["雲切任务条件"]++;
                    Network.Client.MapClient.FromActorPC(Me).SendSystemMessage("进阶『雲切』修炼进度：" + Me.CInt["雲切任务条件"].ToString() + "/777");
                }
                int Level = Me.Skills[11002].Level;
                OtherAddition 雲切CD = new OtherAddition(args.skill, Me, "雲切CD", 5000);
                雲切CD.OnAdditionEnd += 雲切EndEvent;
                SkillHandler.ApplyAddition(Me, 雲切CD);
                factor += 3.3f + 0.7f * Level;

                if (sActor.Status.Additions.ContainsKey("刀剑宗师"))
                    factor += factor * sActor.TInt["刀剑宗师提升%"] / 100f;

                int AttackDamage = SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, Elements.Neutral, factor);
                SkillHandler.Instance.ShowEffectOnActor(sActor, 5388);
                SkillHandler.Instance.ShowEffectOnActor(dActor, 5266, sActor);

                Map map3 = Manager.MapManager.Instance.GetMap(sActor.MapID);
                List<Actor> actors = map3.GetActorsArea(sActor, 500, true);
                foreach (var item in actors)
                {
                    if (item == null) continue;
                    if (item.type == ActorType.PC)
                    {
                        if (((ActorPC)item).Online && ((ActorPC)item).Party == Me.Party && ((ActorPC)item).HP > 0)
                        {
                            if (item.Status.Additions.ContainsKey("雲切暴击率上升"))
                            {
                                Addition 雲切暴击率上升 = item.Status.Additions["雲切暴击率上升"];
                                TimeSpan span = new TimeSpan(0, 0, 0, 0, 10000);
                                ((OtherAddition)雲切暴击率上升).endTime = DateTime.Now + span;
                            }
                            else
                            {
                                OtherAddition 雲切暴击率上升 = new OtherAddition(null, item, "雲切暴击率上升", 10000);
                                雲切暴击率上升.OnAdditionStart += (s, e) =>
                                {
                                    s.Buff.HitCriUp = true;
                                    Manager.MapManager.Instance.GetMap(Me.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, item, true);

                                    s.Status.hit_critical_skill += 15;
                                };
                                雲切暴击率上升.OnAdditionEnd += (s, e) =>
                                {
                                    s.Buff.HitCriUp = false;
                                    Manager.MapManager.Instance.GetMap(Me.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, item, true);
                                    s.Status.hit_critical_skill -= 15;
                                };
                                SkillHandler.ApplyAddition(item, 雲切暴击率上升);
                                SkillHandler.Instance.ShowEffectOnActor(item, 5153, sActor);
                            }
                        }
                    }
                }

                if(Level >= 4)
                {
                    if (sActor.Status.Additions.ContainsKey("雲切暴击伤害上升"))
                    {
                        Addition 雲切暴击伤害上升 = sActor.Status.Additions["雲切暴击伤害上升"];
                        TimeSpan span = new TimeSpan(0, 0, 0, 0, 10000);
                        ((OtherAddition)雲切暴击伤害上升).endTime = DateTime.Now + span;
                    }
                    else
                    {
                        OtherAddition 雲切暴击伤害上升 = new OtherAddition(null, sActor, "雲切暴击伤害上升", 10000);
                        雲切暴击伤害上升.OnAdditionStart += (s, e) => {
                            Network.Client.MapClient.FromActorPC((ActorPC)sActor).SendSystemMessage("暴击伤害提升了");
                            s.TInt["雲切暴击伤害提升"] = 20;
                        };
                        雲切暴击伤害上升.OnAdditionEnd += (s, e) => {
                            Network.Client.MapClient.FromActorPC((ActorPC)sActor).SendSystemMessage("暴击伤害提升效果消失");
                            s.TInt["雲切暴击伤害提升"] = 0;
                        };
                        SkillHandler.ApplyAddition(sActor, 雲切暴击伤害上升);
                        SkillHandler.Instance.ShowEffectOnActor(sActor, 4368);
                    }
                }
                if (sActor.Status.Additions.ContainsKey("刀剑宗师") && sActor.TInt["刀剑宗师提升%"] > 0)//前半段其实可以不要，但是为了好看。。
                    sActor.EP += 500;
                if (level == 4)
                {
                    uint heal = (uint)(AttackDamage * 0.1f);
                    sActor.HP += heal;
                    if (sActor.HP > sActor.MaxHP)
                        sActor.HP = sActor.MaxHP;
                    SkillHandler.Instance.ShowVessel(sActor, (int)-heal);
                }
            }
            else if (ListEquals(Combo, 袈裟斩) && !Me.Status.Additions.ContainsKey("袈裟斩CD") && Me.Skills.ContainsKey(11003))
            {
                int Level = Me.Skills[11003].Level;
                OtherAddition 袈裟斩CD = new OtherAddition(args.skill, Me, "袈裟斩CD", 10000);
                袈裟斩CD.OnAdditionEnd += 袈裟斩EndEvent;
                SkillHandler.ApplyAddition(Me, 袈裟斩CD);

                Map map2 = Manager.MapManager.Instance.GetMap(sActor.MapID);
                List<Actor> actors = map2.GetActorsArea(sActor, 200, false);
                bool 心眼效果 = false ;
                foreach (var item in actors)
                {
                    if (item.type == ActorType.SKILL && item.Name == "心眼技能体")
                    {
                        //Addition 心眼技能 = dActor.Status.Additions["心眼持续时间"];
                        //SkillHandler.RemoveAddition(dActor, "心眼持续时间");
                        if (map2.GetActor(item.ActorID) != null)
                            map2.DeleteActor(item);
                        SkillHandler.Instance.ShowEffectOnActor(sActor, 7974);
                        心眼效果 = true;
                        break;
                    }
                }
                factor += 4f + 1f * Level;

                if (sActor.Status.Additions.ContainsKey("刀剑宗师"))
                    factor += factor * sActor.TInt["刀剑宗师提升%"] / 100f;

                if (心眼效果)
                {
                    factor *= 2;
                    if (dActor.Tasks.ContainsKey("SkillCast") && !dActor.Status.Additions.ContainsKey("不可打断"))
                        SkillHandler.Instance.TitleProccess(sActor, 128, 1);
                    SkillHandler.Instance.CancelSkillCast(dActor);
                    Stun stun = new Stun(null, dActor, 5000);
                    SkillHandler.ApplyAddition(dActor, stun);
                    
                }
                int AttackDamage = SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, Elements.Neutral, factor);
                SkillHandler.Instance.ShowEffectOnActor(sActor, 5111);
                SkillHandler.Instance.ShowEffectOnActor(dActor, 5102, sActor);
                if (!dActor.Status.Additions.ContainsKey("袈裟斩伤害加深"))
                {
                    OtherAddition 袈裟斩伤害加深 = new OtherAddition(args.skill, dActor, "袈裟斩伤害加深", 15000);
                    袈裟斩伤害加深.OnAdditionStart += 袈裟斩伤害加深_OnAdditionStart;
                    袈裟斩伤害加深.OnAdditionEnd += 袈裟斩伤害加深_OnAdditionEnd;
                    SkillHandler.ApplyAddition(dActor, 袈裟斩伤害加深);
                }
                if (Level == 3)
                {
                    ActorPC pc = (ActorPC)sActor;
                    Network.Client.MapClient.FromActorPC(pc).TitleProccess(pc, 30, 1);
                }
                if(level == 4)
                {
                    uint heal = (uint)(AttackDamage * 0.1f);
                    sActor.HP += heal;
                    if (sActor.HP > sActor.MaxHP)
                        sActor.HP = sActor.MaxHP;
                    SkillHandler.Instance.ShowVessel(sActor, (int)-heal);
                }
                if (sActor.Status.Additions.ContainsKey("刀剑宗师") && sActor.TInt["刀剑宗师提升%"] > 0)
                    sActor.EP += 500;
            }
            else if ((ListEquals(Combo, 八刀一闪1) || ListEquals(Combo, 八刀一闪2)) && !Me.Status.Additions.ContainsKey("八刀一闪CD") && Me.Skills.ContainsKey(11004))
            {
                int Level = Me.Skills[11004].Level;
                OtherAddition 八刀一闪CD = new OtherAddition(args.skill, Me, "八刀一闪CD", 50000);
                八刀一闪CD.OnAdditionEnd += 八刀一闪EndEvent;
                SkillHandler.ApplyAddition(Me, 八刀一闪CD);
                factor = 1.6f + 0.4f * Level;

                if (sActor.Status.Additions.ContainsKey("刀剑宗师"))
                    factor += factor * sActor.TInt["刀剑宗师提升%"] / 100f;

                if (Me.EP >= 5500)
                {
                    sActor.EP -= 5500;
                    Manager.MapManager.Instance.GetMap(Me.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);
                    factor *= 3;
                    SkillHandler.Instance.ShowEffectOnActor(sActor, 5386);

                    if (!dActor.Status.Additions.ContainsKey("八刀一闪破甲CD"))
                    {
                        OtherAddition 八刀一闪破甲CD = new OtherAddition(args.skill, dActor, "八刀一闪破甲CD", 49000);
                        SkillHandler.ApplyAddition(dActor, 八刀一闪破甲CD);
                        OtherAddition 八刀一闪破甲 = new OtherAddition(args.skill, dActor, "八刀一闪破甲", 8000);
                        SkillHandler.ApplyAddition(dActor, 八刀一闪破甲);
                    }

                    uint heal = (uint)(sActor.MaxHP * 0.5f);
                    sActor.HP += heal;
                    if (sActor.HP > sActor.MaxHP)
                        sActor.HP = sActor.MaxHP;
                    SkillHandler.Instance.ShowVessel(sActor, (int)-heal);
                }
                args.argType = SkillArg.ArgType.Attack;
                args.type = ATTACK_TYPE.SLASH;
                args.delayRate = 1.3f;
                List<Actor> dest = new List<Actor>();
                for (int i = 0; i < 8; i++)
                    dest.Add(dActor);



                SkillHandler.Instance.PhysicalAttack(sActor, dest, args, SkillHandler.DefType.Def, Elements.Neutral, 0, factor, false, 0, false, 0, 100);
                SkillHandler.Instance.ShowEffectOnActor(sActor, 5434);
                SkillHandler.Instance.ShowEffectOnActor(sActor, 4489);
                if (sActor.Status.Additions.ContainsKey("刀剑宗师") && sActor.TInt["刀剑宗师提升%"] > 0)
                    sActor.EP += 500;
            }
            else if (Me.Status.Additions.ContainsKey("格挡成功")  && Me.Skills.ContainsKey(11007))
            {
                SkillHandler.RemoveAddition(Me, "格挡成功");
                int Level = Me.Skills[11007].Level;
                float factor2 = 0.5f + 0.5f * Level;
                int stuntime = 0;
                float suck = 0;
                float suckadd = 0.02f + 0.01f;
                for (int i = 0; i < Combo.Count; i++)
                {
                    if (Combo[i] == 2)
                        suck += suckadd;
                    if (Combo[i] == 3)
                        stuntime += 500;
                }
                SkillHandler.Instance.ShowEffectOnActor(dActor, 5399, sActor);
                SkillHandler.Instance.ShowEffectOnActor(sActor, 5417);
                //SkillHandler.Instance.ShowEffectOnActor(sActor, 5135);
                SkillHandler.Instance.ShowEffectOnActor(dActor, 5274, sActor);
                SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, Elements.Neutral, factor2);
                int heal = (int)(sActor.MaxHP * suck);
                if(heal > 0)
                {
                    sActor.HP += (uint)heal;
                    if (sActor.HP > sActor.MaxHP)
                        sActor.HP = sActor.MaxHP;
                    SkillHandler.Instance.ShowVessel(sActor, -heal);
                }
                if (stuntime > 0)
                {
                    if (SkillHandler.Instance.CanAdditionApply(Me, dActor, "Stun", 100))
                    {
                        Stun stun = new Stun(null, dActor, stuntime);
                        SkillHandler.ApplyAddition(dActor, stun);
                    }
                }

            }
            else if (Me.Skills.ContainsKey(11006))
            {
                int Level = Me.Skills[11006].Level;
                int Count = 1;
                int MaxCount = 2 + level;
                float ESfactor2 = 0.15f + 0.05f * level;
                float factor2 = 0.4f;
                int SyaCount = 0;
                for (int i = 0; i < Combo.Count; i++)
                {
                    if (Combo[i] == 2 && SyaCount < 10)
                        SyaCount++;
                    if (Combo[i] == 3 && Count < MaxCount)
                        Count++;
                }
                factor2 += ESfactor2 * SyaCount;
                if (Count > MaxCount)
                    Count = MaxCount;
                if (Count != 1)
                {
                    args.argType = SkillArg.ArgType.Attack;
                    args.type = ATTACK_TYPE.SLASH;
                    args.delayRate = 0.8f;
                    List<Actor> dest = new List<Actor>();
                    for (int i = 0; i < Count; i++)
                        dest.Add(dActor);
                    SkillHandler.Instance.ShowEffectOnActor(sActor, 4288);
                    SkillHandler.Instance.ShowEffectOnActor(sActor, 4498);
                    SkillHandler.Instance.PhysicalAttack(sActor, dest, args, SagaLib.Elements.Neutral, factor2);
                }
                else
                    SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, Elements.Neutral, factor);
 
            }
            else
                SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, Elements.Neutral, factor);
            sActor.SkillCombo = new List<int>();
            SkillHandler.Instance.SetNextComboSkill(sActor, 11101);
            SkillHandler.Instance.SetNextComboSkill(sActor, 11102);
            sActor.EP += 500;
            if (sActor.EP > sActor.MaxEP) sActor.EP = sActor.MaxEP;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);
        }

        #endregion
        bool ListEquals(List<int> A,List<int>B)
        {
            if(A.Count == B.Count)
            {
                for (int i = 0; i < A.Count; i++)
                    if (A[i] != B[i])
                        return false;
                return true;
            }
            return false;
        }
        void 雲切EndEvent(Actor actor, OtherAddition skill)
        {
            if (actor.type == ActorType.PC)
                SkillHandler.Instance.ShowEffectOnActor(actor, 5085, actor);
            Network.Client.MapClient.FromActorPC((ActorPC)actor).SendSystemMessage("『雲切』已准备就绪！");
        }
        void 袈裟斩EndEvent(Actor actor, OtherAddition skill)
        {
            if (actor.type == ActorType.PC)
                SkillHandler.Instance.ShowEffectOnActor(actor, 5170, actor);
            Network.Client.MapClient.FromActorPC((ActorPC)actor).SendSystemMessage("『袈裟斩』已准备就绪！");
        }
        void 袈裟斩伤害加深_OnAdditionEnd(Actor actor, OtherAddition skill)
        {
            actor.TInt["袈裟斩伤害加深"] = 0;
        }

        void 袈裟斩伤害加深_OnAdditionStart(Actor actor, OtherAddition skill)
        {
            actor.TInt["袈裟斩伤害加深"] = skill.skill.Level * 5 + 5;
        }
        void 八刀一闪EndEvent(Actor actor, OtherAddition skill)
        {
            if (actor.type == ActorType.PC)
                SkillHandler.Instance.ShowEffectOnActor(actor, 5182, actor);
            Network.Client.MapClient.FromActorPC((ActorPC)actor).SendSystemMessage("『八刀一闪』已准备就绪！");
        }
    }
}
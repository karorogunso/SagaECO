using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S30008 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            炎彈 sc = new 炎彈(args.skill, sActor, dActor, 2000, 0, args);
            SkillHandler.ApplyAddition(dActor, sc);
        }

        class 炎彈 : DefaultBuff
        {
            int count = 0;
            public 炎彈(SagaDB.Skill.Skill skill, Actor sActor, Actor dActor, int lifetime, int damage, SkillArg arg)
                : base(skill, sActor, dActor, "炎彈", lifetime, 200, damage, arg)
            {

                this.OnAdditionStart += this.StartEvent;
                this.OnAdditionEnd += this.EndEvent;
                this.OnUpdate2 += this.TimerUpdate;

            }

            void StartEvent(Actor actor, DefaultBuff skill)
            {
            }

            void EndEvent(Actor actor, DefaultBuff skill)
            {

            }

            void TimerUpdate(Actor sActor, Actor dActor, DefaultBuff skill, SkillArg arg, int damage)
            {
                int maxcount = 3;
                //测试去除技能同步锁ClientManager.EnterCriticalArea();
                try
                {
                    if (count < maxcount)
                    {
                        float factor = 5f;
                        Map map = SagaMap.Manager.MapManager.Instance.GetMap(sActor.MapID);
                        List<Actor> Actors = new List<Actor>();
                        switch (count)
                        {
                            case 0:
                                Actors = map.GetActorsArea(sActor, 1300, false);
                                factor = 5f;
                                break;
                            case 1:
                                Actors = map.GetActorsArea(sActor, 700, false);
                                factor = 8f;
                                break;
                            case 2:
                                Actors = map.GetActorsArea(sActor, 500, false);
                                factor = 15f;
                                break;
                        }
                        foreach (var a in Actors)
                        {
                            if (SkillHandler.Instance.CheckValidAttackTarget(sActor, a))
                            {
                                if (a.HP > 0 && !a.Buff.Dead)
                                {
                                    damage = SkillHandler.Instance.CalcDamage(false, sActor, a, arg, SkillHandler.DefType.MDef, Elements.Fire, 0, factor);
                                    SkillHandler.Instance.CauseDamage(sActor, a, damage);
                                    SkillHandler.Instance.ShowVessel(a, damage);
                                    SkillHandler.Instance.ShowEffect(SagaMap.Manager.MapManager.Instance.GetMap(dActor.MapID), a, 8059);
                                }
                            }
                        }
                        count++;
                    }
                }
                catch (Exception ex)
                {
                    SagaLib.Logger.ShowError(ex);
                }
                //测试去除技能同步锁ClientManager.LeaveCriticalArea();
            }
        }
    }
}

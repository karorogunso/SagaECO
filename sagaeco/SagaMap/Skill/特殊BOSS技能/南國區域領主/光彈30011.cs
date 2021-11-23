using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S30011 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            大炎砍傷害 sc = new 大炎砍傷害(args.skill, sActor, dActor, 2000, 0, args);
            SkillHandler.ApplyAddition(dActor, sc);
            炎鬼缠身 sc2 = new 炎鬼缠身(args.skill, sActor, dActor, 10000, 0);
            SkillHandler.ApplyAddition(dActor, sc2);
        }

        class 大炎砍傷害 : DefaultBuff
        {
            int count = 0;
            public 大炎砍傷害(SagaDB.Skill.Skill skill, Actor sActor, Actor dActor, int lifetime, int damage, SkillArg arg)
                : base(skill, sActor, dActor, "大炎砍傷害", lifetime, 200, damage, arg)
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
                        float factor =3.3f;

                        if (dActor.HP > 0 && !dActor.Buff.Dead)
                        {
                            damage = SkillHandler.Instance.CalcDamage(true, sActor, dActor, arg, SkillHandler.DefType.Def, SagaLib.Elements.Fire, 0,factor);
                            SkillHandler.Instance.CauseDamage(sActor, dActor, damage);
                            SkillHandler.Instance.ShowVessel(dActor, damage);
                            SkillHandler.Instance.ShowEffect(SagaMap.Manager.MapManager.Instance.GetMap(dActor.MapID), dActor, 8059);
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

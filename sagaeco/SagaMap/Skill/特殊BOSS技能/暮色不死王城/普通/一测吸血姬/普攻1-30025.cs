using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaMap.Skill.Additions.Global;
using SagaDB.Actor;
using SagaLib;

namespace SagaMap.Skill.SkillDefinations
{
    public class S30025 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            attack1 sc = new attack1(args.skill, sActor, dActor, 2400, 0, args);
            SkillHandler.ApplyAddition(dActor, sc);

        }

        #endregion

        class attack1 : DefaultBuff
        {
            int count = 0;
            public attack1(SagaDB.Skill.Skill skill, Actor sActor, Actor dActor, int lifetime, int damage, SkillArg arg)
                : base(skill, sActor, dActor, "attack1", lifetime, 200, damage, arg)
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
                int maxcount = 5;
                try
                {
                    if (count < maxcount)
                    {
                        float factor =1f;

                        if (dActor.HP > 0 && !dActor.Buff.Dead)
                        {
                            damage = SkillHandler.Instance.CalcDamage(true, sActor, dActor, arg, SkillHandler.DefType.Def, SagaLib.Elements.Neutral, 0, factor);
                            SkillHandler.Instance.CauseDamage(sActor, dActor, damage);
                            SkillHandler.Instance.ShowVessel(dActor, damage);
                            SkillHandler.Instance.ShowEffect(SagaMap.Manager.MapManager.Instance.GetMap(dActor.MapID), dActor, 8041);
                        }
                        count++;
                    }
                }
                catch (Exception ex)
                {
                    SagaLib.Logger.ShowError(ex);
                }
            }
        }
  
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaMap.Skill.Additions.Global;
using SagaDB.Actor;
using SagaLib;
using SagaMap.Mob;

namespace SagaMap.Skill.SkillDefinations
{
    public class S30031 : ISkill
    {

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Activator s = new Activator(sActor, dActor, args);
            s.Activate();
            SkillHandler.Instance.ActorSpeak(sActor, "看来要让你们的头脑冷静一下，大家要友好相处才是啊！");
        }
        private class Activator : MultiRunTask
        {
            Actor sActor;
            Actor dActor;
            SkillArg args;
            public Activator(Actor sActor, Actor dActor, SkillArg args)
            {
                this.sActor = sActor;
                this.dActor = dActor;
                this.args = args;
                dueTime = 1500;
            }
            public override void CallBack()
            {
                List<Actor> affected = SkillHandler.Instance.GetActorsAreaWhoCanBeAttackedTargets(sActor, 1000);
                SkillHandler.Instance.ShowEffectByActor(sActor, 7913);
                foreach (Actor i in affected)
                {
                    if (SkillHandler.Instance.CheckValidAttackTarget(sActor, i))
                    {
                        SkillHandler.Instance.ShowEffectOnActor(i, 5257);
                        SkillHandler.Instance.DoDamage(false, sActor, i, args, SkillHandler.DefType.MDef, SagaLib.Elements.Water, 0, 8f);
                        if (i.Status.Additions.ContainsKey("冰棍的冻结"))
                            continue;
                        if (i.Status.Additions.ContainsKey("冰棍"))
                        {
                            冰棍 ibuff = (冰棍)i.Status.Additions["冰棍"];
                            if (i.Plies == 1)
                            {
                                冰棍 buff = new 冰棍(args.skill, i, 30000, 2);
                                SkillHandler.ApplyAddition(i, buff);
                            }
                            else
                            {
                                ibuff.AdditionEnd();
                                冰棍的冻结 buff = new 冰棍的冻结(args.skill, i, 30000);
                                SkillHandler.ApplyAddition(i, buff);
                            }
                        }
                        else
                        {
                            冰棍 buff = new 冰棍(args.skill, i, 30000, 1);
                            SkillHandler.ApplyAddition(i, buff);
                        }
                    }
                }
                this.Deactivate();
            }
        }
    }
}

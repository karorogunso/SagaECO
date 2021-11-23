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
    public class S30029 : ISkill
    {

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Activator s = new Activator(sActor, dActor, args);
            s.Activate();
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
                //SkillHandler.Instance.ActorSpeak(sActor, "朋朋的雪球可是百发百中的，看招，雪球冲击！");
            }
            public override void CallBack()
            {
                Map map = Manager.MapManager.Instance.GetMap(dActor.MapID);

                List<Actor> actors = map.GetActorsArea(dActor, 600, true);
                List<Actor> affected = new List<Actor>();
                foreach (var item in actors)
                {
                    if (SkillHandler.Instance.CheckValidAttackTarget(sActor, item))
                        affected.Add(item);
                }

                if (affected.Count > 0)
                    foreach (Actor j in affected)
                    {
                        SkillHandler.Instance.DoDamage(false, sActor, j, args, SkillHandler.DefType.MDef, Elements.Water, 50, 6f);
                        SkillHandler.Instance.ShowEffectOnActor(dActor, 4353);
                    }
                foreach (Actor i in affected)
                {
                    if (SkillHandler.Instance.CheckValidAttackTarget(sActor, i))
                    {
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

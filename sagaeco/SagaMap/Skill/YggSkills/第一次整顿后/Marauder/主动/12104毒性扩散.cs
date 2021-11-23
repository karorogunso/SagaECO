using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S12104 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            short range = (short)(500 + sActor.TInt["毒素研究提升"] * 100);
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> dactors = map.GetActorsArea(sActor, range, false);
            Actor Target = null;
            foreach (var item in dactors)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, item))
                {
                    if(item.Status.Additions.ContainsKey("Poison1"))
                    {
                        Target = item;
                        break;
                    }
                }
            }
            if(Target != null)
            {
                if (Target.Status.Additions.ContainsKey("Poison1"))
                {
                    Addition 毒 = Target.Status.Additions["Poison1"];
                    int lefttime = 毒.RestLifeTime;
                    if (lefttime > 10000) lefttime = 10000;
                    if (lefttime < 1000) lefttime = 1000;
                    foreach (var item in dactors)
                    {
                        if (SkillHandler.Instance.CheckValidAttackTarget(sActor, item))
                        {
                            float factor = 1f;
                            if (sActor.TInt["毒素研究提升"] != 0)
                                factor = 1f + sActor.TInt["毒素研究提升"] * 0.5f;
                            int damage = SkillHandler.Instance.CalcDamage(false, sActor, dActor, args, SkillHandler.DefType.MDef, SagaLib.Elements.Holy, 50, factor);
                            if (item.Status.Additions.ContainsKey("Poison1"))
                                SkillHandler.RemoveAddition(item, "Poison1");
                            Poison1 p = new Poison1(args.skill, item, lefttime, damage);
                            SkillHandler.ApplyAddition(item, p);
                            SkillHandler.Instance.CauseDamage(sActor, item, damage * 10);//生成仇恨
                            SkillHandler.Instance.ShowEffectOnActor(item, 5114);
                        }
                    }
                }
            }
        }
    }
}

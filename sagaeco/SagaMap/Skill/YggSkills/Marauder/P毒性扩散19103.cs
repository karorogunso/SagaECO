using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S19103:ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            if (dActor.Status.Additions.ContainsKey("Poison1") ||
                dActor.Status.Additions.ContainsKey("Poison2") ||
                dActor.Status.Additions.ContainsKey("Poison3"))
            {
                Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
                List<Actor> dactors = map.GetActorsArea(dActor, 500, false);
                foreach (var item in dactors)
                {
                    if (SkillHandler.Instance.CheckValidAttackTarget(sActor, item))
                    {
                        if (!SkillHandler.Instance.isBossMob(item))
                        {
                            SkillHandler.AttackResult res = SkillHandler.AttackResult.Hit;
                            int damage = SkillHandler.Instance.CalcDamage(true, sActor, item, args, SkillHandler.DefType.Def, SagaLib.Elements.Holy, 50, 1f,out res);
                            Poison1 p = new Poison1(args.skill, item, 10000,damage);
                            SkillHandler.ApplyAddition(item, p);
                            SkillHandler.Instance.CauseDamage(sActor, item, damage);//生成仇恨
                            SkillHandler.Instance.ShowEffectOnActor(item, 5114);
                        }
                    }
                }
            }
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S19102:ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
             return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            if (SagaLib.Global.Random.Next(0, 100) < 50 && !SkillHandler.Instance.isBossMob(dActor))//几率
            {
                int damage = SkillHandler.Instance.CalcDamage(true, sActor, dActor, args, SkillHandler.DefType.Def, SagaLib.Elements.Holy, 50, 1f);
                Poison1 p = new Poison1(args.skill, dActor, 10000,damage);
                SkillHandler.Instance.CauseDamage(sActor, dActor, damage);//生成仇恨
                SkillHandler.ApplyAddition(dActor, p);
            }
        }
        #endregion
    }
}

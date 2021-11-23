using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S19201 : ISkill
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
            args.argType = SkillArg.ArgType.Attack;
            args.type = ATTACK_TYPE.SLASH;
            args.delayRate = 10f;
            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, Elements.Neutral, 4.5f);
            SkillHandler.Instance.ShowEffectOnActor(dActor, 5270);
            if (!dActor.Status.Additions.ContainsKey("ATKDOWN"))
            {
                ATKDOWN ad = new ATKDOWN(args.skill, dActor, 20000, 30);
                SkillHandler.ApplyAddition(dActor, ad);
                SkillHandler.Instance.ShowEffectByActor(dActor, 5202);
            }
            if (!dActor.Status.Additions.ContainsKey("MATKDOWN"))
            {
                MATKDOWN ad = new MATKDOWN(args.skill, dActor, 20000, 30);
                SkillHandler.ApplyAddition(dActor, ad);
            }
        }
        #endregion
    }
}

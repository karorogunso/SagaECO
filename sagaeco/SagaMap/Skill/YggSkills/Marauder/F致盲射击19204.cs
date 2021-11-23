using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S19204 : ISkill
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
            SkillHandler.Instance.ShowEffectOnActor(dActor, 5273);
            if (!dActor.Status.Additions.ContainsKey("HITMELLEDOWN"))
            {
                HITMELLEDOWN ad = new HITMELLEDOWN(args.skill, dActor, 20000, 30);
                SkillHandler.ApplyAddition(dActor, ad);
                SkillHandler.Instance.ShowEffectByActor(dActor, 5202);
            }
            if (!dActor.Status.Additions.ContainsKey("HITRANGEDOWN"))
            {
                HITRANGEDOWN ad = new HITRANGEDOWN(args.skill, dActor, 20000, 30);
                SkillHandler.ApplyAddition(dActor, ad);
            }
        }
        #endregion
    }
}

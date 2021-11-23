using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S19220 : ISkill
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
            SkillHandler.Instance.ShowEffectOnActor(sActor, 4163);
            float factor = 3.2f;
            args.argType = SkillArg.ArgType.Attack;
            args.type = ATTACK_TYPE.STAB;
            args.delayRate = (1000 - sActor.Status.aspd * 1.6f) * 0.001f;
            List<Actor> dest = new List<Actor>();
            for (int i = 0; i < 2; i++)
            {
                dest.Add(dActor);
            }
            SkillHandler.Instance.PhysicalAttack(sActor, dest, args, SagaLib.Elements.Neutral, factor);

            ((ActorPC)sActor).TInt["Scorponok暴击"]++;
            SkillHandler.Instance.ShowVessel(sActor, 0, 0, ((ActorPC)sActor).TInt["Scorponok暴击"]);
        }
        #endregion
    }
}

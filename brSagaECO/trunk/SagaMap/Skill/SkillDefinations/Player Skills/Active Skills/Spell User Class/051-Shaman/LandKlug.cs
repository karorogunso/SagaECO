using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;

namespace SagaMap.Skill.SkillDefinations.Shaman
{
    public class LandKlug:ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (SkillHandler.Instance.CheckValidAttackTarget(pc, dActor))
            {
                return 0;
            }
            else
            {
                return -14;
            }
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 0;
            factor = 1.0f + 0.3f * level;
            SkillHandler.Instance.MagicAttack(sActor, dActor, args, SagaLib.Elements.Earth, factor);
            if (level == 6)
            {
                factor = 5f;
                Skill.Additions.Global.Stone skill = new Additions.Global.Stone(args.skill, dActor, 2500);
                SkillHandler.ApplyAddition(dActor, skill);
                SkillHandler.Instance.ShowEffect((ActorPC)sActor, dActor, 4203);
            }
            
        }

        #endregion
    }
}

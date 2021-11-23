using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;

namespace SagaMap.Skill.SkillDefinations.Sorcerer
{
    public class RavingSword : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 0;
            switch (level)
            {
                case 1:
                    factor = 4.5f;
                    break;
                case 2:
                    factor = 6.5f;
                    break;
                case 3:
                    factor = 8.5f;
                    break;
            }

            SkillHandler.Instance.MagicAttack(sActor, dActor, args, SkillHandler.DefType.Def, SagaLib.Elements.Neutral, factor);
        }

        #endregion
    }
}
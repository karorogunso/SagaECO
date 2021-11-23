using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;

namespace SagaMap.Skill.SkillDefinations.Wizard
{
    public class EnergyOne:ISkill
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
            switch (level)
            {
                case 1:
                    factor = 1.9f;
                    break;
                case 2:
                    factor = 2.1f;
                    break;
                case 3:
                    factor = 2.3f;
                    break;
                case 4:
                    factor = 2.5f;
                    break;
                case 5:
                    factor = 2.7f;
                    break;
                case 6:
                    factor = 22f;
                    break;      
            }
            SkillHandler.Instance.MagicAttack(sActor, dActor, args, SagaLib.Elements.Neutral, factor);
        }

        #endregion
    }
}

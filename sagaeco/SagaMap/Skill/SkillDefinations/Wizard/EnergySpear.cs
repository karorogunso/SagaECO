using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;

namespace SagaMap.Skill.SkillDefinations.Wizard
{
    public class EnergySpear:ISkill
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
                    factor = 3.2f;
                    break;
                case 2:
                    factor = 4.0f;
                    break;
                case 3:
                    factor = 4.8f;
                    break;
                case 4:
                    factor = 5.6f;
                    break;
                case 5:
                    factor = 6.4f;
                    break;
            }
            if (level <= 5)
                SkillHandler.Instance.MagicAttack(sActor, dActor, args, SagaLib.Elements.Neutral, factor);
            else
            {
                SkillHandler.Instance.MagicAttack(sActor, dActor, args, SagaLib.Elements.Neutral, factor, 50);
            }
        }
        #endregion
    }
}

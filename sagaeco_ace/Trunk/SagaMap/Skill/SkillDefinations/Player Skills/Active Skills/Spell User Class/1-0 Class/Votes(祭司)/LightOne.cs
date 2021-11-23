using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;

namespace SagaMap.Skill.SkillDefinations.Vates
{
    public class LightOne : ISkill
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
                    factor = 1.6f;
                    break;
                case 2:
                    factor = 1.9f;
                    break;
                case 3:
                    factor = 2.2f;
                    break;
                case 4:
                    factor = 2.5f;
                    break;
                case 5:
                    factor = 2.8f;
                    break;
                case 6:
                    factor = 20f;
                    break;
            }
            if (level == 6)
                SkillHandler.Instance.Seals(sActor, dActor, 5);
            else
                SkillHandler.Instance.Seals(sActor, dActor);
            SkillHandler.Instance.MagicAttack(sActor, dActor, args, SagaLib.Elements.Holy, factor);
        }
        #endregion
    }
}

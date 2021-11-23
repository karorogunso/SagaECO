using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;

namespace SagaMap.Skill.SkillDefinations.Vates
{
    public class LightOne:ISkill
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
                    factor = 1.2f;
                    break;
                case 2:
                    factor = 1.45f;
                    break;
                case 3:
                    factor = 1.7f;
                    break;
                case 4:
                    factor = 1.95f;
                    break;
                case 5:
                    factor = 2.2f;
                    break;

            }
            SkillHandler.Instance.Seals(sActor, dActor);
            SkillHandler.Instance.MagicAttack(sActor, dActor, args, SagaLib.Elements.Holy, factor);
            
        }

        #endregion
    }
}

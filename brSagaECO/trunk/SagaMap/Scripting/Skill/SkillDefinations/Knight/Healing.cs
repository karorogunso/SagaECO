using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;

namespace SagaMap.Skill.SkillDefinations.Knight
{
    public class Healing:ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Status.Additions.ContainsKey("Spell"))
            {
                return -7;
            }
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 0;
            switch (level)
            {
                case 1:
                    factor = -1f;
                    break;
                case 2:
                    factor = -1.7f;
                    break;
                case 3:
                    factor = -2.3f;
                    break;
            }
            if (dActor.Status.undead)
            {
                factor = -factor;
            }
            SkillHandler.Instance.MagicAttack(sActor, dActor, args, SagaLib.Elements.Holy, factor);
        }

        #endregion
    }
}

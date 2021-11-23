using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;

namespace SagaMap.Skill.SkillDefinations.Knight
{
    public class Healing : ISkill
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
            uint HP_ADD = (uint)(dActor.MaxHP * 0.08f * level);
            SkillHandler.Instance.FixAttack(sActor, dActor, args, SagaLib.Elements.Holy, -HP_ADD);
        }
        #endregion
    }
}

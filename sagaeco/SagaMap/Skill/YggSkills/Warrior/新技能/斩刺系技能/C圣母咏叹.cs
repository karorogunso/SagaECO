using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S18507 : ISkill
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
            dActor = SkillHandler.Instance.GetdActor(sActor,args);
            if (dActor == null) return;

            float factor = 13;
            switch (level)
            {
                case 1:
                    factor = 13f;
                    break;
                case 2:
                    factor = 16f;
                    break;
                case 3:
                    factor = 20f;
                    break;
            }
            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, Elements.Neutral, factor);
            SkillHandler.Instance.SetNextComboSkill(sActor, 18507);
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S18602 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (!SkillHandler.Instance.CheckSkillCanCastForWeapon(pc, args))
                return -5;
            return -0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            dActor = SkillHandler.Instance.GetdActor(sActor,args);
            if (dActor == null) return;

            float factor = 5f;
            float rate = 2f;
            switch(level)
            {
                case 1:
                    factor = 5f;
                    rate = 2f;
                    break;
                case 2:
                    factor = 6f;
                    rate = 3f;
                    break;
                case 3:
                    factor = 7f;
                    rate = 4f;
                    break;
            }
            if(dActor.Status.Additions.ContainsKey("Stun"))
            {
                factor *= rate;
            }
            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, Elements.Neutral, factor);
        }
        #endregion
    }
}
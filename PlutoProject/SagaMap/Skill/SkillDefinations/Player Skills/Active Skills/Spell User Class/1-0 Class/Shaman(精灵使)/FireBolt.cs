using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;

namespace SagaMap.Skill.SkillDefinations.Shaman
{
    public class FireBolt : ISkill
    {
        bool MobUse;
        public FireBolt()
        {
            this.MobUse = false;
        }
        public FireBolt(bool MobUse)
        {
            this.MobUse = MobUse;
        }
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
            if (MobUse)
            {
                level = 5;

            }
            float[] factors = { 0, 1.15f, 1.35f, 1.55f, 1.75f, 2.0f };
            float factor = factors[level];

            SkillHandler.Instance.MagicAttack(sActor, dActor, args, SagaLib.Elements.Fire, factor);
        }

        #endregion
    }
}

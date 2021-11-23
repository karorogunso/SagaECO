using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;

namespace SagaMap.Skill.SkillDefinations.Wizard
{
    /// <summary>
    /// 沉默詛咒
    /// </summary>
    public class MagSilence:ISkill
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
            int rate = 0;
            int lifetime = 0;
            SkillHandler.Instance.MagicAttack(sActor, dActor, args, SagaLib.Elements.Neutral, 0);
            args.flag[0] = SagaLib.AttackFlag.NONE;
            switch (level)
            {
                case 1:
                    rate = 50;
                    lifetime = 2000;
                    break;
                case 2:
                    rate = 50;
                    lifetime = 3000;
                    break;
                case 3:
                    rate = 50;
                    lifetime = 4000;
                    break;
                case 4:
                    rate = 50;
                    lifetime = 5000;
                    break;
                case 5:
                    rate = 50;
                    lifetime = 6000;
                    break;
            }
            if (SkillHandler.Instance.CanAdditionApply(sActor,dActor, SkillHandler.DefaultAdditions.Silence, rate))
            {
                Additions.Global.Silence skill = new SagaMap.Skill.Additions.Global.Silence(args.skill, dActor, lifetime);
                SkillHandler.ApplyAddition(dActor, skill);
            }
        }
        #endregion
    }
}

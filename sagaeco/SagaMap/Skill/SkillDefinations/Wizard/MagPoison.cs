using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;

namespace SagaMap.Skill.SkillDefinations.Wizard
{
    /// <summary>
    /// 灼熱毒氣
    /// </summary>
    public class MagPoison:ISkill
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
                    rate = 60;
                    lifetime = 6000;
                    break;
                case 2:
                    rate = 60;
                    lifetime = 7000;
                    break;
                case 3:
                    rate = 60;
                    lifetime = 8000;
                    break;
                case 4:
                    rate = 60;
                    lifetime = 9000;
                    break;
                case 5:
                    rate = 60;
                    lifetime = 10000;
                    break;
            }
            if (SkillHandler.Instance.CanAdditionApply(sActor,dActor, SkillHandler.DefaultAdditions.Poison, rate))
            {
                Additions.Global.Poison1 skill = new SagaMap.Skill.Additions.Global.Poison1(args.skill, dActor, lifetime);
                SkillHandler.ApplyAddition(dActor, skill);
            }
        }
        #endregion
    }
}

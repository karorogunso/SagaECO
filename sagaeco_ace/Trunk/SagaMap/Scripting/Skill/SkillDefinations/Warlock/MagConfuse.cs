using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Warlock
{
    /// <summary>
    /// マジックコンフュージョン
    /// </summary>
    public class MagConfuse: ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Status.Additions.ContainsKey("MagConfuse"))
                return -30;
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
            DefaultBuff skill2 = new DefaultBuff(args.skill, sActor, "MagConfuse", 60000);
            SkillHandler.ApplyAddition(sActor, skill2);
            int rate = 0;
            int lifetime = 0;
            SkillHandler.Instance.MagicAttack(sActor, dActor, args, SagaLib.Elements.Neutral, 0);
            args.flag[0] = SagaLib.AttackFlag.NONE;
            switch (level)
            {
                case 1:
                    rate = 20;
                    lifetime = 6000;
                    break;
                case 2:
                    rate = 30;
                    lifetime = 6000;
                    break;
                case 3:
                    rate = 40;
                    lifetime = 6000;
                    break;
                case 4:
                    rate = 50;
                    lifetime = 6000;
                    break;
                case 5:
                    rate = 60;
                    lifetime = 6000;
                    break;
            }
            if (SkillHandler.Instance.CanAdditionApply(sActor,dActor, SkillHandler.DefaultAdditions.Confuse , rate))
            {
                Additions.Global.Confuse skill = new SagaMap.Skill.Additions.Global.Confuse(args.skill, dActor, lifetime);
                SkillHandler.ApplyAddition(dActor, skill);
            }
        }
        #endregion
    }
}

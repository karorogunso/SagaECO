using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
namespace SagaMap.Skill.SkillDefinations.DarkStalker
{
    /// <summary>
    /// 黑暗的波動(暗黑波动斩)（ダークワールウィンド）
    /// </summary>
    public class DarkVacuum :ISkill 
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
            SkillArg args2 = args.Clone();
            args2.type = ATTACK_TYPE.SLASH;
            float factor = 0;
            factor = 0.75f + 0.75f * level;
            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, SagaLib.Elements.Dark, factor);

            //SkillHandler.Instance.MagicAttack(sActor, dActor, args2, SagaLib.Elements.Dark, factor);
            args.AddSameActor(args2);
        }
        #endregion
    }
}

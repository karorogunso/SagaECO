using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;

namespace SagaMap.Skill.SkillDefinations.Swordman
{
    /// <summary>
    /// 致殘攻擊(スロウブロウ)
    /// </summary>
    public class SlowBlow:ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 0;
            args.type = ATTACK_TYPE.BLOW;
            factor = 1.4f;
            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, SagaLib.Elements.Neutral, factor);
            if (level > 1 && ((args.flag[0] & SagaLib.AttackFlag.HP_DAMAGE) != 0))
            {
                int rate = 0;
                int lifetime = 0;
                switch (level)
                {
                    case 2:
                        rate = 8;
                        lifetime = 3000;
                        break;
                    case 3:
                        rate = 12;
                        lifetime = 4000;
                        break;
                    case 4:
                        rate = 15;
                        lifetime = 5000;
                        break;
                    case 5:
                        rate = 20;
                        lifetime = 6000;
                        break;
                }
                if (SkillHandler.Instance.CanAdditionApply(sActor,dActor, SkillHandler.DefaultAdditions.鈍足, rate))
                {
                    Additions.Global.鈍足 skill = new SagaMap.Skill.Additions.Global.鈍足(args.skill, dActor, lifetime);
                    SkillHandler.ApplyAddition(dActor, skill);
                }
            }
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaMap.Skill.Additions.Global;
using SagaDB.Actor;

namespace SagaMap.Skill.SkillDefinations.BladeMaster
{
    /// <summary>
    ///  百鬼哭（百鬼哭）
    /// </summary>
    public class aHundredSpriteCry : ISkill 
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (SkillHandler.Instance.CheckValidAttackTarget(sActor, dActor))
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
            float factor = 1.2f + 0.2f * level;
            //int[] attackTimes = {3,3,4,4,5};
            args.argType = SkillArg.ArgType.Attack;
            args.type = ATTACK_TYPE.BLOW;
            List<Actor> dest = new List<Actor>();
            for (int i = 0; i < 3; i++)
            {
                dest.Add(dActor);
            }
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "百鬼哭", 20000);//伤害加深BUFF
            SkillHandler.ApplyAddition(dActor, skill);

            SkillHandler.Instance.PhysicalAttack(sActor, dest, args, SagaLib.Elements.Neutral, factor);
        }
        #endregion
    }
}

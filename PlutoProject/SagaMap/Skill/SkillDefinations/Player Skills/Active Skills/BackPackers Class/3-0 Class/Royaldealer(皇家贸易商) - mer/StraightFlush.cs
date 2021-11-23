using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;
using SagaMap.Skill.Additions.Global;
using SagaDB.Item;


namespace SagaMap.Skill.SkillDefinations.Royaldealer
{
    /// <summary>
    /// ストレートフラッシュ
    /// </summary>
    class StraightFlush : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 1.0f + 0.1f * level;
            int number = 0;
            if (level == 5)
            {
                number = 3;
            }
            else
            {
                number = 1;
            }
            args.argType = SkillArg.ArgType.Attack;
            args.type = ATTACK_TYPE.STAB;
            List<Actor> dest = new List<Actor>();
            for (int i = 0; i < number; i++)
                dest.Add(dActor);
            args.delayRate = 4.5f;
            SkillHandler.Instance.PhysicalAttack(sActor, dest, args, sActor.WeaponElement, factor);
            if (dActor.HP > 0)
                args.autoCast.Add(SkillHandler.Instance.CreateAutoCastInfo(2518, level, 2000));
            
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Scout
{
    public class ConThrow:ISkill
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
            //args.argType = SkillArg.ArgType.Attack;
            switch (level)
            {
                case 1:
                case 2:
                    factor = 1.1f;
                    break;
                case 3:
                case 4:
                    factor = 1.15f;
                    break;
                case 5:
                case 6:
                    factor = 1.2f;
                    break;
            }
            byte count = 2;
            if (level == 6)
                count = 12;
            args.delayRate = 5f;
            List<Actor> target = new List<Actor>();
            for (int i = 0; i < count; i++)
            {
                target.Add(dActor);
            }
            
            SkillHandler.Instance.PhysicalAttack(sActor, target, args, SagaLib.Elements.Neutral, factor);
        }
        #endregion
    }
}

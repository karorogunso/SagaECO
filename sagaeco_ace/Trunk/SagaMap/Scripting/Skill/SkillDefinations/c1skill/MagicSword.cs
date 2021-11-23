using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;

namespace SagaMap.Skill.SkillDefinations.C1skill
{

    public class MagicSword : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 1.1f+0.4f*level;
            List<Actor> list = new List<Actor>();
            list.Add(dActor);
            SkillHandler.Instance.PhysicalAttack(sActor, list, args, SkillHandler.DefType.MDef, SagaLib.Elements.Neutral, 0, factor, false);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Monster
{
    public class Brandish : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
                return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int combo;
            int min = 2, max = 4;
            float factor = 0.75f;
            args.argType = SkillArg.ArgType.Attack;
            args.type = ATTACK_TYPE.SLASH;

            combo = SagaLib.Global.Random.Next(min, max);
            List<Actor> target = new List<Actor>();
            for (int i = 0; i < combo; i++)
            {
                target.Add(dActor);
            }
            SkillHandler.Instance.PhysicalAttack(sActor, target, args, SagaLib.Elements.Neutral, factor);
        }
    }
}

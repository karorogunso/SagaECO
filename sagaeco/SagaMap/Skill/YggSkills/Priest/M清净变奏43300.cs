
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using SagaLib;
namespace SagaMap.Skill.SkillDefinations
{
    public class S43300 : ISkill
    {
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 5f;
            int rate = 20;
            List<Actor> targets = SkillHandler.Instance.GetActorsAreaWhoCanBeAttackedTargets(sActor, 300);
            SkillHandler.Instance.MagicAttack(sActor, targets, args, Elements.Holy, factor);

            if(SagaLib.Global.Random.Next(0,100)< rate)
            {
                SkillHandler.Instance.ShowEffectOnActor(sActor, 5295);
                foreach (var item in targets)
                {
                    Silence skill = new Silence(args.skill, item, 3000);
                    SkillHandler.ApplyAddition(item, skill);
                }
            }
        }
    }
}

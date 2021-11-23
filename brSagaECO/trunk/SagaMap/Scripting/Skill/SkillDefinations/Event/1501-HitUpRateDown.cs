
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Event
{
    /// <summary>
    /// 命中率（アキュラシー）
    /// </summary>
    public class HitUpRateDown : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            bool active = true;
            DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, "HitUpRateDown", active);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(sActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            actor.Status.hit_ranged_skill += 15;
            actor.Status.hit_melee_skill  += 15;
        }
        void EndEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            actor.Status.hit_ranged_skill -= 15;
            actor.Status.hit_melee_skill -= 15;
        }
        #endregion
    }
}


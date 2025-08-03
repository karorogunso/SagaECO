using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Monster
{
    /// <summary>
    /// 提升迴避率
    /// </summary>
    public class MobAvoupSelf : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 90000;
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "MobAvoupSelf", lifetime);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            //近戰迴避
            actor.Status.avoid_melee_skill += (short)20;
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            //近戰迴避
            actor.Status.avoid_melee_skill -= (short)20;
        }
        #endregion
    }
}

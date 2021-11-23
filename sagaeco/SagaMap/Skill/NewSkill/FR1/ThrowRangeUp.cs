
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Assassin
{
    /// <summary>
    /// 投擲距離提升（投擲射程上昇）
    /// </summary>
    public class ThrowRangeUp : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            bool active = true;
            /*
             * Lv   1 2 3 
             * 射程 1 2 3 
             * 
             * 投擲武器最高射程只能提升到 6
             * 有些投擲武器射程為 4 ，技能LV3時射程也不會超過 6
             * 
             */
            DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, "ThrowRangeUp", active);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(sActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
        }
        void EndEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
        }
        #endregion
    }
}


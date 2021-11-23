
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Gardener
{
    /// <summary>
    /// トピアリー
    /// </summary>
    public class Topiary : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            bool active = true;
            DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, "Topiary", active);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(sActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            /*
             * トピアリー †
                Passive 
                習得JOBLV：23 
                効果：「トピアリーツリー」を自在に刈り取ることができる。 
                トピアリーツリーはタイニー、インスマウス、マンドラニンジンの形に刈り取れる。 
                タイニー、インスマウス、マンドラニンジンの形には何度でも刈り取り戻すことが可能。
             */
        }
        void EndEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
        }
        #endregion
    }
}


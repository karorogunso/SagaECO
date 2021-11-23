
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Gardener
{
    /// <summary>
    /// わたしは木
    /// </summary>
    public class IAmTree : ISkill
    {
        uint TreeMobID = 30040003;//小樹
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = int.MaxValue;
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "IAmTree", lifetime);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            SkillHandler.Instance.TranceMob(actor, TreeMobID);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            SkillHandler.Instance.TranceMob(actor, 0);
        }
        #endregion
    }
}

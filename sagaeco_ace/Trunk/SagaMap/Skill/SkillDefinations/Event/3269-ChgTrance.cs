
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Event
{
    /// <summary>
    /// 蝙蝠變身
    /// </summary>
    public class ChgTrance : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 10000 * level;
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "ChgTrance", lifetime);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            //變身成蝙蝠
           // SkillHandler.Instance.TranceMob()
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            SkillHandler.Instance.TranceMob(actor, 0);
        }
        #endregion
    }
}

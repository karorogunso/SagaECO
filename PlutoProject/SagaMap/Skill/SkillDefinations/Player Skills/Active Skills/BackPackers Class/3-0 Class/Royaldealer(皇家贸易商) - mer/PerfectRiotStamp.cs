using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;
using SagaMap.Skill.Additions.Global;
using SagaDB.Item;


namespace SagaMap.Skill.SkillDefinations.Royaldealer
{
    /// <summary>
    /// ストレートフラッシュ
    /// </summary>
    class PerfectRiotStamp : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 8000 + 2000 * level;
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "PerfectRiotStamp", lifetime);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);

        }

        void StartEventHandler(Actor actor, DefaultBuff skill)
        {

        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {

        }
        #endregion
    }
}

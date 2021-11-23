
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Knight
{
    /// <summary>
    /// 聖騎士誓約（プロテクト）
    /// </summary>
    public class AProtect : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (sActor.PossessionTarget == 0)
            {
                return 0;
            }
            else
            {
                return -25;
            }
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 24000 + 3000 * level;
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "AProtect", lifetime);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Status.possessionTakeOver = 100;
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Status.possessionTakeOver = 0;
        }
        #endregion
    }
}

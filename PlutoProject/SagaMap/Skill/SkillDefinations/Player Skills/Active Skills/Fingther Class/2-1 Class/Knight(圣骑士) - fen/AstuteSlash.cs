
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Knight
{
    /// <summary>
    /// 砍擊防禦術（ディフェンス・スラッシュ）
    /// </summary>
    public class AstuteSlash : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 7500 + 2500 * level;
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "AstuteSlash", lifetime);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Status.damage_atk2_discount = 0.2f + 0.05f * skill.skill.Level;
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Status.damage_atk2_discount = 0;
        }
        #endregion
    }
}
/*
  BLOW,
  SLASH,
  STAB,
*/
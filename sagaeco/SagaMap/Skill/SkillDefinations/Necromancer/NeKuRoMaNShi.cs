
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Necromancer
{
    /// <summary>
    /// 黑暗魅力（ネクロマンシー）
    /// </summary>
    public class NeKuRoMaNShi : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 15000;
            int rate = 10 + 10 * level;
            if (SkillHandler.Instance.CanAdditionApply(sActor, dActor, "NeKuRoMaNShi", rate))
            {
                DefaultBuff skill = new DefaultBuff(args.skill, dActor, "NeKuRoMaNShi", lifetime);
                skill.OnAdditionStart += this.StartEventHandler;
                skill.OnAdditionEnd += this.EndEventHandler;
                SkillHandler.ApplyAddition(dActor, skill);
            }
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            //變成不死
            actor.Buff.Undead = true;
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            //恢復
            actor.Buff.Undead = false;
        }
        #endregion
    }
}

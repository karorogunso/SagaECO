
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Sorcerer
{
    /// <summary>
    /// 狂亂時間（オーバーワーク）
    /// </summary>
    public class DelayCancel : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 20000;
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "DelayCancel", lifetime);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
            //減速
            int rate = 4 * skill.skill.Level;
            int liftime2 = 5000;
            if (SkillHandler.Instance.CanAdditionApply(sActor, dActor, SkillHandler.DefaultAdditions.鈍足, rate))
            {
                Additions.Global.鈍足 skill2 = new SagaMap.Skill.Additions.Global.鈍足(args.skill, dActor, liftime2);
                SkillHandler.ApplyAddition(dActor, skill2);
            }
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Status.aspd_rate_skill += (short)(20f + 30 * skill.skill.Level);

            actor.Buff.DelayCancel = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Status.aspd_rate_skill -= (short)(20f + 30f * skill.skill.Level);

            actor.Buff.DelayCancel = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        #endregion
    }
}

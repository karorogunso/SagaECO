using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using SagaLib;
namespace SagaMap.Skill.SkillDefinations.Monster
{
    /// <summary>
    /// 再生技能无效化
    /// </summary>
    public class MobNoHeal : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (dActor.Status.Additions.ContainsKey("Sacrifice") || dActor.Status.Additions.ContainsKey("NoHeal"))
            {
                return -1;
            }
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            if (!dActor.Status.Additions.ContainsKey("Sacrifice") && !dActor.Status.Additions.ContainsKey("NoHeal"))
            {
                DefaultBuff skill = new DefaultBuff(args.skill, dActor, "NoHeal", 15000);
                skill.OnAdditionStart += this.StartEventHandler;
                skill.OnAdditionEnd += this.EndEventHandler;
                SkillHandler.ApplyAddition(dActor, skill);
            }


        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Buff.NoRegen = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Buff.NoRegen = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Sage
{
    /// <summary>
    /// 敏捷頭腦（インテレクトライズ）
    /// </summary>
    public class IntelRides : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if(sActor.ActorID==dActor.ActorID)
            {
                return -14;
            }
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = (int)(7.5 + 7.5 * level) * 1000;
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "IntelRides", lifetime);
            ActorPC pc = (ActorPC)sActor;
            if (skill.Variable.ContainsKey("IntelRides_INT"))
                skill.Variable.Remove("IntelRides_INT");
            skill.Variable.Add("IntelRides_INT", pc.Int);

            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Status.int_skill += (short)skill.Variable["IntelRides_INT"];
            //Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Status.int_skill -= (short)skill.Variable["IntelRides_INT"];
            // Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        #endregion
    }
}

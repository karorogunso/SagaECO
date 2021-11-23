using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S11103 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int life = 30000;
            if (sActor.Status.Additions.ContainsKey("速战诀"))
            {
                Addition skill = sActor.Status.Additions["速战诀"];
                TimeSpan span = new TimeSpan(0, 0, 0, 0, life);
                ((OtherAddition)skill).endTime = DateTime.Now + span;
            }
            else
            {

                OtherAddition skill = new OtherAddition(args.skill, sActor, "速战诀", life);
                skill.OnAdditionStart += this.StartEventHandler;
                skill.OnAdditionEnd += this.EndEventHandler;
                SkillHandler.ApplyAddition(sActor, skill);
            }
        }

        void StartEventHandler(Actor actor, OtherAddition skill)
        {
            if(actor.Status.Instructor > 0)//庭师
                actor.Status.aspd_rate_skill += (short)(actor.Status.Instructor * 3);

            actor.TInt["DamageRate"] = 15 + 25 * skill.skill.Level;
            actor.Buff.DelayCancel = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void EndEventHandler(Actor actor, OtherAddition skill)
        {
            actor.Status.aspd_rate_skill = 100;
            actor.TInt["DamageRate"] = 0;
            actor.Buff.DelayCancel = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        #endregion
    }
}
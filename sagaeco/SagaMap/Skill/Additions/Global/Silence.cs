using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaDB.Skill;

namespace SagaMap.Skill.Additions.Global
{
    public class Silence : DefaultBuff 
    {
        /// <summary>
        /// 沉默
        /// </summary>
        /// <param name="skill"></param>
        /// <param name="actor"></param>
        /// <param name="lifetime">持续时间，debuffee状态时间补正，至少持续10%时间</param>
        public Silence(SagaDB.Skill.Skill skill, Actor actor, int lifetime)
            : base(skill, actor, "Silence", (int)(lifetime * (1f + Math.Max((actor.Status.debuffee_bonus / 100), -0.9f))))
        {
            if (SkillHandler.Instance.isBossMob(actor))
            {
                if (!actor.Status.Additions.ContainsKey("BOSSSilence免疫"))
                {
                    DefaultBuff BOSSSilence免疫 = new DefaultBuff(skill, actor, "BOSSSilence免疫", 30000);
                    SkillHandler.ApplyAddition(actor, BOSSSilence免疫);
                }
                else
                    this.Enabled = false;
            }
            this.OnAdditionStart += this.StartEvent;
            this.OnAdditionEnd += this.EndEvent;
        }

        void StartEvent(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            actor.Buff.Silence = true;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
            SkillHandler.Instance.CancelSkillCast(actor);
        }

        void EndEvent(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            actor.Buff.Silence = false;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
    }
}

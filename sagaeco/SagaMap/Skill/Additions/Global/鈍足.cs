using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaDB.Skill;

namespace SagaMap.Skill.Additions.Global
{
    public class 鈍足 : DefaultBuff 
    {
        /// <summary>
        /// 钝足（减少移动速度，无叠加）
        /// </summary>
        /// <param name="skill"></param>
        /// <param name="actor"></param>
        /// <param name="lifetime">持续时间，debuffee状态时间补正，至少持续10%的时间</param>
        /// <param name="amount">移动速度减少百分数，效果随抗性减少，至少10%效果</param>
        public 鈍足(SagaDB.Skill.Skill skill, Actor actor, int lifetime, int amount=30)
            : base(skill, actor, "鈍足", (int)(lifetime * (1f + Math.Max((actor.Status.debuffee_bonus / 100), -0.9f))))
        {
            amount = (int)(amount * (1f - Math.Min((actor.AbnormalStatus[SagaLib.AbnormalStatus.鈍足] / 100), 0.9f)));
            amount = Math.Min(amount, 100);
            if (SkillHandler.Instance.isBossMob(actor))
            {
                if (!actor.Status.Additions.ContainsKey("BOSS鈍足免疫"))
                {
                    DefaultBuff BOSS鈍足免疫 = new DefaultBuff(skill, actor, "BOSS鈍足免疫", 30000);
                    SkillHandler.ApplyAddition(actor, BOSS鈍足免疫);
                }
                else
                    this.Enabled = false;
            }
            if (this.Variable.ContainsKey("SpeedDown"))
                this.Variable.Remove("SpeedDown");
            this.Variable.Add("SpeedDown", (int)(actor.Speed * amount / 100f));
            this.OnAdditionStart += this.StartEvent;
            this.OnAdditionEnd += this.EndEvent;
        }

        void StartEvent(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            if (skill.Variable["SpeedDown"] > 0)
            {
                actor.Status.speed_skill -= (ushort)skill.Variable["SpeedDown"];
                if (actor.type == ActorType.MOB)
                {
                    ActorEventHandlers.MobEventHandler eh = (ActorEventHandlers.MobEventHandler)actor.e;
                    //设置AI活动性可以更新AI的移动速度
                    SagaMap.Mob.Activity act = eh.AI.AIActivity;
                    eh.AI.AIActivity = act;
                }
                actor.Buff.SpeedDown = true;
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
            }
        }

        void EndEvent(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            actor.Buff.SpeedDown = false;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
            if (skill.Variable["SpeedDown"] > 0)
            {
                actor.Status.speed_skill += (ushort)skill.Variable["SpeedDown"];
                if (actor.type == ActorType.MOB)
                {
                    ActorEventHandlers.MobEventHandler eh = (ActorEventHandlers.MobEventHandler)actor.e;
                    //设置AI活动性可以更新AI的移动速度
                    SagaMap.Mob.Activity act = eh.AI.AIActivity;
                    eh.AI.AIActivity = act;
                }
            }
        }
    }
}

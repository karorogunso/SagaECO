using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Skill;

namespace SagaMap.Skill.Additions.Global
{
    public class MpDown : DefaultBuff
    {
        public MpDown(SagaDB.Skill.Skill skill, Actor sActor, Actor dActor, int lifetime, int damage)
            : base(skill, sActor, dActor, "MpDown", (int)(lifetime * (1f + Math.Max((dActor.Status.debuffee_bonus / 100), -0.9f))), 1000, damage)
        {
            
            this.OnAdditionStart += this.StartEvent;
            this.OnAdditionEnd += this.EndEvent;
            this.OnUpdate2 += this.TimerUpdate;

        }

        void StartEvent(Actor actor, DefaultBuff skill)
        {
        }

        void EndEvent(Actor actor, DefaultBuff skill)
        {

        }

        void TimerUpdate(Actor sActor,Actor dActor, DefaultBuff skill ,SkillArg arg, int damage)
        {
            //测试去除技能同步锁ClientManager.EnterCriticalArea();
            try
            {
                if (dActor.MP > 0 && !dActor.Buff.Dead)
                {
                    SkillHandler.Instance.ShowVessel(dActor, 0,damage);
                    dActor.MP -= (uint)damage;
                }
                if (dActor.MP - damage < 0)
                {
                    SkillHandler.Instance.ShowVessel(dActor, 0, (int)(damage - dActor.MP));
                    dActor.MP = 0;
                }
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
            //测试去除技能同步锁ClientManager.LeaveCriticalArea();
        }
    }
}

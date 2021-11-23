using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaDB.Skill;
using SagaLib;

namespace SagaMap.Skill.Additions.Global
{
    public class Paralyse : DefaultBuff 
    {
        /// <summary>
        /// 麻痹（全命中下降，无叠加 打断）
        /// </summary>
        /// <param name="skill"></param>
        /// <param name="actor"></param>
        /// <param name="lifetime">持续时间，debuffee状态时间补正，至少持续10%时间</param>
        /// <param name="amount">全命中下降百分数,随抗性减少，至少下降原效果的10%</param>
        public Paralyse(SagaDB.Skill.Skill skill, Actor actor, int lifetime, int amount = 10)
            : base(skill, actor, "Paralyse", (int)(lifetime * (1f + Math.Max((actor.Status.debuffee_bonus/100), -0.9f))))
        {
            amount = (int)(amount * (1 - Math.Min(actor.AbnormalStatus[AbnormalStatus.Paralyse] / 100, 0.9f)));
            amount=Math.Min(amount, 100);
            if (this.Variable.ContainsKey("ParalyseHitMelee"))
                this.Variable.Remove("ParalyseHitMelee");
            this.Variable.Add("ParalyseHitMelee", (int)(actor.Status.hit_melee_bs * amount / 100f));
            if (this.Variable.ContainsKey("ParalyseHitRange"))
                this.Variable.Remove("ParalyseHitRange");
            this.Variable.Add("ParalyseHitRange", (int)(actor.Status.hit_ranged_bs * amount / 100f));
            if (this.Variable.ContainsKey("ParalyseCri"))
                this.Variable.Remove("ParalyseCri");
            this.Variable.Add("ParalyseCri", (int)(actor.Status.hit_critical_bs * amount / 100f));
            if (this.Variable.ContainsKey("ParalyseMag"))
                this.Variable.Remove("ParalyseMag");
            this.Variable.Add("ParalyseMag", (int)(actor.Status.hit_magic_bs * amount / 100f));
            this.OnAdditionStart += this.StartEvent;
            this.OnAdditionEnd += this.EndEvent;
        }

        void StartEvent(Actor actor, DefaultBuff skill)
        {
            SkillHandler.Instance.CancelSkillCast(actor);
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            if (skill.Variable["ParalyseHitMelee"] > 0)
            {
                actor.Status.hit_melee_skill -= (short)skill.Variable["ParalyseHitMelee"];
                actor.Buff.Paralysis = true;
            }
            if (skill.Variable["ParalyseHitRange"] > 0)
            {
                actor.Status.hit_ranged_skill -= (short)skill.Variable["ParalyseHitRange"];
                actor.Buff.Paralysis = true;
            }
            if (skill.Variable["ParalyseCri"] > 0)
            {
                actor.Status.hit_critical_skill -= (short)skill.Variable["ParalyseCri"];
                actor.Buff.Paralysis = true;
            }
            if (skill.Variable["ParalyseMag"] > 0)
            {
                actor.Status.hit_magic_skill -= (short)skill.Variable["ParalyseMag"];
                actor.Buff.Paralysis = true;
            }
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void EndEvent(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            actor.Buff.Paralysis = false;
            if (skill.Variable["ParalyseHitMelee"] > 0)
            {
                actor.Status.hit_melee_skill += (short)skill.Variable["ParalyseHitMelee"];
            }
            if (skill.Variable["ParalyseHitRange"] > 0)
            {
                actor.Status.hit_ranged_skill += (short)skill.Variable["ParalyseHitRange"];
            }
            if (skill.Variable["ParalyseCri"] > 0)
            {
                actor.Status.hit_critical_skill += (short)skill.Variable["ParalyseCri"];
            }
            if (skill.Variable["ParalyseMag"] > 0)
            {
                actor.Status.hit_magic_skill += (short)skill.Variable["ParalyseMag"];
            }
            skill.Variable["ParalyseHitMelee"] = 0;
            skill.Variable["ParalyseHitRange"] = 0;
            skill.Variable["ParalyseCri"] = 0;
            skill.Variable["ParalyseMag"] = 0;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
    }
}

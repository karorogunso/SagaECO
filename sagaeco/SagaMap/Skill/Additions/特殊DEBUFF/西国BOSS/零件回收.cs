using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaDB.Skill;
using SagaLib;

namespace SagaMap.Skill.Additions.Global
{
    public class 零件回收 : DefaultDeBuff 
    {
        /// <summary>
        /// 灼烧（持续性无属性魔法伤害,无叠加）
        /// </summary>
        /// <param name="skill"></param>
        /// <param name="actor"></param>
        /// <param name="lifetime">持续时间，debuffee状态时间补正，至少10%持续时间</param>
        /// <param name="amount">原始灼伤伤害未计算魔法防御</param>
        public 零件回收(SagaDB.Skill.Skill skill, Actor actor, int lifetime, int amount=400)
            : base(skill, actor, "零件回收", (int)(lifetime * (1f + Math.Max((actor.Status.debuffee_bonus / 100), -0.9f))), 2000)
        {
            if (this.Variable.ContainsKey("零件回收Atk"))
                this.Variable.Remove("零件回收Atk");
            this.Variable.Add("零件回收Atk", amount);
            this.OnAdditionStart += this.StartEvent;
            this.OnAdditionEnd += this.EndEvent;
            this.OnUpdate += this.TimerUpdate;
        }

        void StartEvent(Actor actor, DefaultDeBuff skill)
        {
        }

        void EndEvent(Actor actor, DefaultDeBuff skill)
        {
            if (skill.Variable["零件回收Atk"] > 0)
            {               
                int damage =skill.Variable["零件回收Atk"];
                try
                {
                    if (actor.HP > 0 && !actor.Buff.Dead)
                    {
                        SkillHandler.Instance.ShowEffectOnActor(actor, 4226);
                        if (damage < 1)
                            damage = 1;
                        if (actor.HP > damage)
                        {
                            SkillHandler.Instance.ShowVessel(actor, damage);
                            actor.HP = (uint)(actor.HP - damage);
                        }
                        else
                        {
                            if (actor.HP > 1) SkillHandler.Instance.ShowVessel(actor, (int)(actor.HP - 1));
                            actor.HP = 1;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                }
            }
            skill.Variable["零件回收Atk"] = 0;
        }

        void TimerUpdate(Actor actor, DefaultDeBuff skill)
        {          
            if (skill.Variable["零件回收Atk"] > 0)
            {
                int damage = skill.Variable["零件回收Atk"];
                try
                {
                    if (actor.HP > 0 && !actor.Buff.Dead)
                    {
                        SkillHandler.Instance.ShowEffectOnActor(actor, 4226);
                        if (damage < 1)
                            damage = 1;
                        if (actor.HP > damage)
                        {
                            SkillHandler.Instance.ShowVessel(actor, damage);
                            actor.HP = (uint)(actor.HP - damage);
                        }
                        else
                        {
                            if (actor.HP > 1) SkillHandler.Instance.ShowVessel(actor, (int)(actor.HP - 1));
                            actor.HP = 1;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                }
            }
        }
    }
}

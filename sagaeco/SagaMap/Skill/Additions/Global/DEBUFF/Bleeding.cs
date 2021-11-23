using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaDB.Skill;
using SagaLib;

namespace SagaMap.Skill.Additions.Global
{
    public class Bleeding : DefaultDeBuff
    {
        /// <summary>
        /// 灼烧（持续性无属性魔法伤害,无叠加）
        /// </summary>
        /// <param name="skill"></param>
        /// <param name="actor"></param>
        /// <param name="lifetime">持续时间，debuffee状态时间补正，至少10%持续时间</param>
        /// <param name="amount">原始灼伤伤害未计算魔法防御</param>
        public Bleeding(SagaDB.Skill.Skill skill, Actor sActor, Actor dActor, int lifetime, int amount = 100)
            : base(skill, sActor, dActor, "Bleeding", lifetime,1000, amount)
        {
            if (this.Variable.ContainsKey("BleedingAtk"))
                this.Variable.Remove("BleedingAtk");
            this.Variable.Add("BleedingAtk", amount);
            this.OnAdditionStart += this.StartEvent;
            this.OnAdditionEnd += this.EndEvent;
            this.OnUpdate += this.TimerUpdate;
            dueTime = 1000;
        }

        void StartEvent(Actor actor, DefaultDeBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            //actor.Buff.Bleeding = true;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void EndEvent(Actor actor, DefaultDeBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            //actor.Buff.Bleeding = false;
            if (skill.Variable["BleedingAtk"] > 0)
            {
                int damage = SkillHandler.Instance.CalcMagDamage(actor, SkillHandler.DefType.MDef, skill.Variable["BleedingAtk"], 0);
                try
                {
                    if (actor.HP > 0 && !actor.Buff.Dead)
                    {
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
                        map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, actor, true);
                    }
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                }
            }
            skill.Variable["BleedingAtk"] = 0;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void TimerUpdate(Actor actor, DefaultDeBuff skill)
        {
            if (skill.Variable["BleedingAtk"] > 0)
            {
                Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
                int damage = SkillHandler.Instance.CalcMagDamage(actor, SkillHandler.DefType.MDef, skill.Variable["BleedingAtk"], 0);
                try
                {
                    if (actor.HP > 0 && !actor.Buff.Dead)
                    {
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
                        map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, actor, true);
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

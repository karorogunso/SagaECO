using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaDB.Skill;
using SagaLib;

namespace SagaMap.Skill.Additions.Global
{
    public class Burning : DefaultDeBuff 
    {
        /// <summary>
        /// 灼烧（持续性无属性魔法伤害,无叠加）
        /// </summary>
        /// <param name="skill"></param>
        /// <param name="actor"></param>
        /// <param name="lifetime">持续时间，debuffee状态时间补正，至少10%持续时间</param>
        /// <param name="amount">原始灼伤伤害未计算魔法防御</param>
        public Burning(SagaDB.Skill.Skill skill, Actor actor, int lifetime, int amount=100)
            : base(skill, actor, "Burning", (int)(lifetime * (1f + Math.Max((actor.Status.debuffee_bonus / 100), -0.9f))), 1000)
        {
            if (this.Variable.ContainsKey("BurningAtk"))
                this.Variable.Remove("BurningAtk");
            this.Variable.Add("BurningAtk", amount);
            this.OnAdditionStart += this.StartEvent;
            this.OnAdditionEnd += this.EndEvent;
            this.OnUpdate += this.TimerUpdate;
            dueTime = 2000;
            count = 0;
        }
        int count = 0;
        void StartEvent(Actor actor, DefaultDeBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            actor.Buff.Burning = true;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void EndEvent(Actor actor, DefaultDeBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            actor.Buff.Burning = false;
            if (skill.Variable["BurningAtk"] > 0)
            {               
                int damage = SkillHandler.Instance.CalcMagDamage(actor, SkillHandler.DefType.MDef, skill.Variable["BurningAtk"], 0);
                try
                {
                    if (actor.HP > 0 && !actor.Buff.Dead && actor.HP < actor.MaxHP)
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
            skill.Variable["BurningAtk"] = 0;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void TimerUpdate(Actor actor, DefaultDeBuff skill)
        {
            count++;
            if (count > 1)
            {
                if (skill.Variable["BurningAtk"] > 0)
                {
                    Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
                    int damage = SkillHandler.Instance.CalcMagDamage(actor, SkillHandler.DefType.MDef, skill.Variable["BurningAtk"], 0);
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
}

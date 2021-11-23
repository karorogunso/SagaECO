using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Skill;

namespace SagaMap.Skill.Additions.Global
{
    public class Poison3 : DefaultDeBuff
    {
        /// <summary>
        /// 猛毒，中毒瞬间开始伤害直到结束，叠加时间
        /// </summary>
        /// <param name="skill"></param>
        /// <param name="actor"></param>
        /// <param name="lifetime">持续时间，debuffee状态时间补正，至少持续10%时间</param>
        /// <param name="amount">猛毒伤初始值，随抗性衰减，至少发生10%伤害</param>
        public Poison3(SagaDB.Skill.Skill skill, Actor actor, int lifetime, int amount = 10)
            : base(skill, actor, "Poison3", (int)(lifetime * (1f + Math.Max((actor.Status.debuffee_bonus / 100), -0.9f))), 5000)
        {
            if (SkillHandler.Instance.isBossMob(actor))
            {
                if (!actor.Status.Additions.ContainsKey("BOSSPoison免疫"))
                {
                    DefaultBuff BOSSPoison免疫 = new DefaultBuff(skill, actor, "BOSSPoison免疫", 60000);
                    SkillHandler.ApplyAddition(actor, BOSSPoison免疫);
                }
                else
                    this.Enabled = false;
            }
            if (actor.Status.Additions.ContainsKey("Poison3"))
            {
                actor.Status.Additions["Poison3"].TotalLifeTime += (int)(lifetime * (1f + Math.Max((actor.Status.debuffee_bonus / 100), -0.9f)));
                this.Enabled = false;
            }
            else
            {
                if (this.Variable.ContainsKey("Poison3"))
                    this.Variable.Remove("Poison3");
                this.Variable.Add("Poison3", (int)(amount * (1f - 0.9f * Math.Min((actor.AbnormalStatus[SagaLib.AbnormalStatus.Poisen] / 100), 1f))));
                this.OnAdditionStart += this.StartEvent;
                this.OnAdditionEnd += this.EndEvent;
                this.OnUpdate += this.TimerUpdate;
            }
        }

        void StartEvent(Actor actor, DefaultDeBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            actor.Buff.猛毒 = true;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);          
        }

        void EndEvent(Actor actor, DefaultDeBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            try
            {
                if (actor.HP > 0 && !actor.Buff.Dead)
                {
                    if (actor.TInt["毒疗"] >= 2)
                    {
                        actor.HP += (uint)(actor.HP * 0.05f);
                        if (actor.HP > actor.MaxHP)
                            actor.HP = actor.MaxHP;
                    }
                    else
                    {
                        if (skill.Variable["Poison3"] < 1)
                            skill.Variable["Poison3"] = 1;
                        if (actor.HP > skill.Variable["Poison3"])
                        {
                            SkillHandler.Instance.ShowVessel(actor, skill.Variable["Poison3"]);
                            actor.HP = (uint)(actor.HP - skill.Variable["Poison3"]);
                        }
                        else
                        {
                            if (actor.HP > 1) SkillHandler.Instance.ShowVessel(actor, (int)(actor.HP - 1));
                            actor.HP = 1;
                        }
                        //actor.e.OnHPMPSPUpdate(actor);
                        skill.Variable["Poison3"] = 2 * skill.Variable["Poison3"];
                    }
                    map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, actor, true);
                }
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
            actor.Buff.猛毒 = false;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
           
        }

        void TimerUpdate(Actor actor, DefaultDeBuff skill)
        {
            //测试去除技能同步锁ClientManager.EnterCriticalArea();
            try
            {
                if (actor.HP > 0 && !actor.Buff.Dead)
                {
                    Map map = Manager.MapManager.Instance.GetMap(actor.MapID);

                    if (actor.TInt["毒疗"] >= 2)
                    {
                        actor.HP += (uint)(actor.HP * 0.05f);
                        if (actor.HP > actor.MaxHP)
                            actor.HP = actor.MaxHP;
                    }
                    else
                    {
                        if (skill.Variable["Poison3"] < 1)
                            skill.Variable["Poison3"] = 1;
                        if (actor.HP > skill.Variable["Poison3"])
                        {
                            SkillHandler.Instance.ShowVessel(actor, skill.Variable["Poison3"]);
                            actor.HP = (uint)(actor.HP - skill.Variable["Poison3"]);
                        }
                        else
                        {
                            if (actor.HP > 1) SkillHandler.Instance.ShowVessel(actor, (int)(actor.HP - 1));
                            actor.HP = 1;
                        }
                        //actor.e.OnHPMPSPUpdate(actor);
                        skill.Variable["Poison3"] = 2 * skill.Variable["Poison3"];
                    }
                    map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, actor, true);
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

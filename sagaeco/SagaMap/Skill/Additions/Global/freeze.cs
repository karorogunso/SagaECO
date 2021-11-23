using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaDB.Skill;
using SagaLib;

namespace SagaMap.Skill.Additions.Global
{
    public class Freeze : DefaultBuff
    {
        /// <summary>
        /// 冰冻，持续减少sp，左魔防翻倍，左物防归零，水属性加100，打断
        /// </summary>
        /// <param name="skill"></param>
        /// <param name="actor"></param>
        /// <param name="lifetime">持续时间，debuffee状态时间补正，至少持续10%时间</param>
        /// <param name="amount">sp伤害基准值，随冰冻抗性减少，至少10%效果</param>
        public Freeze(SagaDB.Skill.Skill skill, Actor actor, int lifetime, int amount = 10)
            : base(skill, actor, "Frosen", (int)(lifetime * (1f + Math.Max((actor.Status.debuffee_bonus / 100), -0.9f))), 3000)
        {
            amount = (int)(amount * (1 - Math.Min(actor.AbnormalStatus[AbnormalStatus.Frosen] / 100, 0.9f)));
            if (SkillHandler.Instance.isBossMob(actor))
            {
                if (!actor.Status.Additions.ContainsKey("BOSSFrosen免疫"))
                {
                    DefaultBuff BOSSFrosen免疫 = new DefaultBuff(skill, actor, "BOSSFrosen免疫", 30000);
                    SkillHandler.ApplyAddition(actor, BOSSFrosen免疫);
                }
                else
                    this.Enabled = false;
            }
            if (this.Variable.ContainsKey("FrosenDamage"))
                this.Variable.Remove("FrosenDamage");
            this.Variable.Add("FrosenDamage", amount);
            this.OnAdditionStart += this.StartEvent;
            this.OnAdditionEnd += this.EndEvent;
            this.OnUpdate += this.UpdateEvent;
        }

        void StartEvent(Actor actor, DefaultBuff skill)
        {
            SkillHandler.Instance.CancelSkillCast(actor);
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            actor.Buff.Frosen = true;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
            if (skill.Variable.ContainsKey("FrosenElement"))
                skill.Variable.Remove("FrosenElement");
            skill.Variable.Add("FrosenElement", 100);
            /*if (skill.Variable.ContainsKey("FrosenDefDown"))
                skill.Variable.Remove("FrosenDefDown");
            skill.Variable.Add("FrosenDefDown", actor.Status.def);
            if (skill.Variable.ContainsKey("FrosenMDefUp"))
                skill.Variable.Remove("FrosenMDefUp");
            skill.Variable.Add("FrosenMDefUp", actor.Status.mdef);*/
            actor.Status.elements_skill[SagaLib.Elements.Water] += skill.Variable["FrosenElement"];
            //actor.Status.def_skill -= (short)skill.Variable["FrosenDefDown"];
            //actor.Status.mdef_skill += (short)skill.Variable["FrosenMDefUp"];
        }

        void EndEvent(Actor actor, DefaultBuff skill)
        {
            actor.SpeedCut = 0;
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            actor.Buff.Frosen = false;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
            actor.Status.elements_skill[SagaLib.Elements.Water] -= skill.Variable["FrosenElement"];
           // actor.Status.def_skill += (short)skill.Variable["FrosenDefDown"];
            //actor.Status.mdef_skill -= (short)skill.Variable["FrosenMDefUp"];
        }
        void UpdateEvent(Actor actor, DefaultBuff skill)
        {
            try
            {
                if (actor.HP > 0 && !actor.Buff.Dead)
                {
                    Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
                    if (skill.Variable["FrosenDamage"] < 1)
                        skill.Variable["FrosenDamage"] = 1;
                    if (actor.SP > skill.Variable["FrosenDamage"])
                    {
                        SkillHandler.Instance.ShowVessel(actor, skill.Variable["FrosenDamage"]);
                        actor.SP = (uint)(actor.SP - skill.Variable["FrosenDamage"]);
                    }
                    else
                    {
                        if (actor.SP > 1) SkillHandler.Instance.ShowVessel(actor, (int)(actor.SP - 1));
                        actor.SP = 1;
                    }
                    //actor.e.OnHPMPSPUpdate(actor);
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

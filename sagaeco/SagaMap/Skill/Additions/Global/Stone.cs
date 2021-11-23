using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaDB.Skill;
using SagaLib;

namespace SagaMap.Skill.Additions.Global
{
    public class Stone : DefaultBuff
    {
        /// <summary>
        /// 石化，持续减少mp，左物防翻倍，左魔防归零，地属性加100，打断
        /// </summary>
        /// <param name="skill"></param>
        /// <param name="actor"></param>
        /// <param name="lifetime">持续时间，debuffee状态时间补正，至少持续10%时间</param>
        /// <param name="amount">mp伤害基准值，随石化抗性减少，至少10%效果</param>
        public Stone(SagaDB.Skill.Skill skill, Actor actor, int lifetime, int amount = 10)
            : base(skill, actor, "Stone", (int)(lifetime * (1f + Math.Max((actor.Status.debuffee_bonus / 100), -0.9f))), 3000)
        {
            amount = (int)(amount * (1 - Math.Min(actor.AbnormalStatus[AbnormalStatus.Stone] / 100, 0.9f)));
            if (SkillHandler.Instance.isBossMob(actor))
            {
                if (!actor.Status.Additions.ContainsKey("BOSSStone免疫"))
                {
                    DefaultBuff BOSSStone免疫 = new DefaultBuff(skill, actor, "BOSSStone免疫", 30000);
                    SkillHandler.ApplyAddition(actor, BOSSStone免疫);
                }
                else
                    this.Enabled = false;
            }
            if (this.Variable.ContainsKey("StoneDamage"))
                this.Variable.Remove("StoneDamage");
            this.Variable.Add("StoneDamage", amount);
            this.OnAdditionStart += this.StartEvent;
            this.OnAdditionEnd += this.EndEvent;
            this.OnUpdate += this.UpdateEvent;
        }

        void StartEvent(Actor actor, DefaultBuff skill)
        {
            SkillHandler.Instance.CancelSkillCast(actor);
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            actor.Buff.Stone = true;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
            /*if (skill.Variable.ContainsKey("StoneElement"))
                skill.Variable.Remove("StoneElement");
            skill.Variable.Add("StoneElement", 100);
            if (skill.Variable.ContainsKey("StoneDefUp"))
                skill.Variable.Remove("StoneDefUp");
            skill.Variable.Add("StoneDefUp", actor.Status.def);
            if (skill.Variable.ContainsKey("StoneMDefDown"))
                skill.Variable.Remove("StoneMDefDown");
            skill.Variable.Add("StoneMDefDown", actor.Status.mdef);
            actor.Status.elements_skill[SagaLib.Elements.Earth] += skill.Variable["StoneElement"];
            actor.Status.def_skill += (short)skill.Variable["StoneDefUp"];
            actor.Status.mdef_skill -= (short)skill.Variable["StoneMDefDown"];*/
        }

        void EndEvent(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            actor.Buff.Stone = false;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
            /*actor.Status.elements_skill[SagaLib.Elements.Earth] -= skill.Variable["StoneElement"];
            actor.Status.def_skill -= (short)skill.Variable["StoneDefUp"];
            actor.Status.mdef_skill += (short)skill.Variable["StoneMDefDown"];*/
        }
        void UpdateEvent(Actor actor, DefaultBuff skill)
        {
            try
            {
                if (actor.HP > 0 && !actor.Buff.Dead)
                {
                    Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
                    if (skill.Variable["StoneDamage"] < 1)
                        skill.Variable["StoneDamage"] = 1;
                    if (actor.MP > skill.Variable["StoneDamage"])
                    {
                        SkillHandler.Instance.ShowVessel(actor, 0, skill.Variable["StoneDamage"]);
                        actor.MP = (uint)(actor.MP - skill.Variable["StoneDamage"]);
                    }
                    else
                    {
                        if (actor.MP > 1) SkillHandler.Instance.ShowVessel(actor, 0, (int)(actor.MP - 1));
                        actor.MP = 1;
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

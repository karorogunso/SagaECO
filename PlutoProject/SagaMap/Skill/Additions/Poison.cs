using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Skill;

namespace SagaMap.Skill.Additions.Global
{
    public class Poison : DefaultBuff
    {
        public Poison(SagaDB.Skill.Skill skill, Actor actor, int lifetime)
            : base(skill, actor, "Poison", (int)(lifetime * (1f - actor.AbnormalStatus[SagaLib.AbnormalStatus.Poisen] / 100)), 2000)
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
            this.OnAdditionStart += this.StartEvent;
            this.OnAdditionEnd += this.EndEvent;
            this.OnUpdate += this.TimerUpdate;
        }

        void StartEvent(Actor actor, DefaultBuff skill)
        {
            //最大攻擊
            int max_atk1_add = (int)(actor.Status.max_atk_bs / 2);
            if (skill.Variable.ContainsKey("Poison_max_atk1"))
                skill.Variable.Remove("Poison_max_atk1");

            skill.Variable.Add("Poison_max_atk1", max_atk1_add);
            actor.Status.max_atk1_skill -= (short)max_atk1_add;

            //最大攻擊
            int max_atk2_add = (int)(actor.Status.max_atk_bs / 2);
            if (skill.Variable.ContainsKey("Poison_max_atk2"))
                skill.Variable.Remove("Poison_max_atk2");
            skill.Variable.Add("Poison_max_atk2", max_atk2_add);
            actor.Status.max_atk2_skill -= (short)max_atk2_add;

            //最大攻擊
            int max_atk3_add = (int)(actor.Status.max_atk_bs / 2);
            if (skill.Variable.ContainsKey("Poison_max_atk3"))
                skill.Variable.Remove("Poison_max_atk3");
            skill.Variable.Add("Poison_max_atk3", max_atk3_add);
            actor.Status.max_atk3_skill -= (short)max_atk3_add;

            //最小攻擊
            int min_atk1_add = (int)(actor.Status.min_atk_bs / 2);
            if (skill.Variable.ContainsKey("Poison_min_atk1"))
                skill.Variable.Remove("Poison_min_atk1");
            skill.Variable.Add("Poison_min_atk1", min_atk1_add);
            actor.Status.min_atk1_skill -= (short)min_atk1_add;

            //最小攻擊
            int min_atk2_add = (int)(actor.Status.min_atk_bs / 2);
            if (skill.Variable.ContainsKey("Poison_min_atk2"))
                skill.Variable.Remove("Poison_min_atk2");
            skill.Variable.Add("Poison_min_atk2", min_atk2_add);
            actor.Status.min_atk2_skill -= (short)min_atk2_add;

            //最小攻擊
            int min_atk3_add = (int)(actor.Status.min_atk_bs / 2);
            if (skill.Variable.ContainsKey("Poison_min_atk3"))
                skill.Variable.Remove("Poison_min_atk3");
            skill.Variable.Add("Poison_min_atk3", min_atk3_add);
            actor.Status.min_atk3_skill -= (short)min_atk3_add;

            //最小魔攻
            int min_matk_add = (int)(actor.Status.min_matk_bs / 2);
            if (skill.Variable.ContainsKey("Poison_min_matk"))
                skill.Variable.Remove("Poison_min_matk");
            skill.Variable.Add("Poison_min_matk", min_matk_add);
            actor.Status.min_matk_skill -= (short)min_matk_add;
            //最大魔攻
            int max_matk_add = (int)(actor.Status.max_matk_bs / 2);
            if (skill.Variable.ContainsKey("Poison_max_matk"))
                skill.Variable.Remove("Poison_max_matk");
            skill.Variable.Add("Poison_max_matk", max_matk_add);
            actor.Status.max_matk_skill -= (short)max_matk_add;

            actor.Buff.Poison = true;
            if (actor.type == ActorType.PC)
                Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
            else
                Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, false);
        }

        void EndEvent(Actor actor, DefaultBuff skill)
        {
            //最大攻擊
            actor.Status.max_atk1_skill += (short)skill.Variable["Poison_max_atk1"];

            //最大攻擊
            actor.Status.max_atk2_skill += (short)skill.Variable["Poison_max_atk2"];

            //最大攻擊
            actor.Status.max_atk3_skill += (short)skill.Variable["Poison_max_atk3"];

            //最小攻擊
            actor.Status.min_atk1_skill += (short)skill.Variable["Poison_min_atk1"];

            //最小攻擊
            actor.Status.min_atk2_skill += (short)skill.Variable["Poison_min_atk2"];

            //最小攻擊
            actor.Status.min_atk3_skill += (short)skill.Variable["Poison_min_atk3"];

            //最小魔攻
            actor.Status.min_matk_skill += (short)skill.Variable["Poison_min_matk"];

            //最大魔攻
            actor.Status.max_matk_skill += (short)skill.Variable["Poison_max_matk"];

            actor.Buff.Poison = false;
            if(actor.type == ActorType.PC)
                Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
            else
                Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, false);
        }

        void TimerUpdate(Actor actor, DefaultBuff skill)
        {
            //测试去除技能同步锁ClientManager.EnterCriticalArea();
            try
            {
                if (actor.HP > 0 && !actor.Buff.Dead)
                {
                    Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
                    int amount = (int)(actor.MaxHP / 100) * 5;
                    if (amount < 1)
                        amount = 1;
                    if (actor.HP > amount)
                        actor.HP = (uint)(actor.HP - amount);
                    else
                        actor.HP = 1;
                    actor.e.OnHPMPSPUpdate(actor);
                    //map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, actor, true);
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

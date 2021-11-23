using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Skill;

namespace SagaMap.Skill.Additions.Global
{
    public class  Medicine1: DefaultBuff
    {
        public Medicine1(SagaDB.Skill.Skill skill, Actor actor, int lifetime)
            : base(skill, actor, "Medicine1", lifetime, 10000)
        {
            this.OnAdditionStart += this.StartEvent;
            this.OnAdditionEnd += this.EndEvent;
            this.OnUpdate += this.TimerUpdate;
        }

        void StartEvent(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);

            if (skill.Variable.ContainsKey("MedicineHealing1"))
                skill.Variable.Remove("MedicineHealing1");

            int hpadd = (int)(actor.MaxHP * (actor.Status.hp_medicine / 100f));
            skill.Variable.Add("MedicineHealing1", hpadd);

        }

        void EndEvent(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
            if (actor.HP > 0 && !actor.Buff.Dead)
            {
                if (actor.HP < (actor.MaxHP - (uint)skill.Variable["MedicineHealing1"]))
                {
                    actor.HP += (uint)skill.Variable["MedicineHealing1"];
                }
                else
                {
                    actor.HP = actor.MaxHP;
                }
            }
            SkillHandler.Instance.ShowVessel(actor, -skill.Variable["MedicineHealing1"]);
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, actor, true);
            //actor.Status.hp_medicine = 0;
        }



    void TimerUpdate(Actor actor, DefaultBuff skill)
        {
            //测试去除技能同步锁ClientManager.EnterCriticalArea();
            try
            {
                if (actor.HP > 0 && !actor.Buff.Dead)
                {
                    List<Actor> list = new List<Actor>();
                    list.Add(actor);
                    Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
                    if (actor.HP < (actor.MaxHP - (uint)skill.Variable["MedicineHealing1"]))
                    {
                        actor.HP += (uint)skill.Variable["MedicineHealing1"];
                    }
                    else
                    {
                        actor.HP = actor.MaxHP;
                    }
                    //显示
                    SkillHandler.Instance.ShowVessel(actor, -skill.Variable["MedicineHealing1"]);
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Skill;

namespace SagaMap.Skill.Additions.Global
{
    public class  Medicine3: DefaultBuff
    {
        public Medicine3(SagaDB.Skill.Skill skill, Actor actor, int lifetime)
            : base(skill, actor, "Medicine3", lifetime, 2000)
        {
            this.OnAdditionStart += this.StartEvent;
            this.OnAdditionEnd += this.EndEvent;
            this.OnUpdate += this.TimerUpdate;
        }

        void StartEvent(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
           
            if (skill.Variable.ContainsKey("MedicineHealing3"))
                skill.Variable.Remove("MedicineHealing3");

            int spadd=(int)(actor.MaxSP*(actor.Status.sp_medicine/100f));
            skill.Variable.Add("MedicineHealing3", spadd);

        }

        void EndEvent(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
            if (actor.HP > 0 && !actor.Buff.Dead)
            {
                if (actor.SP < (actor.MaxSP - (uint)skill.Variable["MedicineHealing3"]))
                {
                    actor.SP += (uint)skill.Variable["MedicineHealing3"];
                }
                else
                {
                    actor.SP = actor.MaxSP;
                }                
            }
            SkillHandler.Instance.ShowVessel(actor, 0, 0, -skill.Variable["MedicineHealing3"]);
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, actor, true);
            //actor.Status.sp_medicine = 0;
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
                    if (actor.SP < (actor.MaxSP - (uint)skill.Variable["MedicineHealing3"]))
                    {
                        actor.SP += (uint)skill.Variable["MedicineHealing3"];
                    }
                    else
                    {
                        actor.SP = actor.MaxSP;
                    }           
                    map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, actor, true);
                    //显示
                    SkillHandler.Instance.ShowVessel(actor,0,0, -skill.Variable["MedicineHealing3"]);
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

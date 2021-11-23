using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Skill;

namespace SagaMap.Skill.Additions.Global
{
    public class  Medicine2: DefaultBuff
    {
        public Medicine2(SagaDB.Skill.Skill skill, Actor actor, int lifetime)
            : base(skill, actor, "Medicine2", lifetime, 2000)
        {
            this.OnAdditionStart += this.StartEvent;
            this.OnAdditionEnd += this.EndEvent;
            this.OnUpdate += this.TimerUpdate;
        }

        void StartEvent(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
           
            if (skill.Variable.ContainsKey("MedicineHealing2"))
                skill.Variable.Remove("MedicineHealing2");

            int mpadd=(int)(actor.MaxMP*(actor.Status.mp_medicine/100f));
            skill.Variable.Add("MedicineHealing2", mpadd);

        }

        void EndEvent(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
            if (actor.HP > 0 && !actor.Buff.Dead)
            {
                if (actor.MP < (actor.MaxMP - (uint)skill.Variable["MedicineHealing2"]))
                {
                    actor.MP += (uint)skill.Variable["MedicineHealing2"];
                }
                else
                {
                    actor.MP = actor.MaxMP;
                }                
            }
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, actor, true);
            SkillHandler.Instance.ShowVessel(actor,0, -skill.Variable["MedicineHealing2"]);
            //actor.Status.mp_medicine = 0;
        }

        void TimerUpdate(Actor actor, DefaultBuff skill)
        {
            //测试去除技能同步锁ClientManager.EnterCriticalArea();
            try
            {
                if (actor.HP > 0 && !actor.Buff.Dead)
                {
                    if(actor.type  == ActorType.PC)
                    {
                        ActorPC pc = (ActorPC)actor;
                        if (pc.Job == PC_JOB.HAWKEYE)
                            skill.Variable["MedicineHealing2"] = 0;
                    }
                    List<Actor> list = new List<Actor>();
                    list.Add(actor);
                    Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
                    if (actor.MP < (actor.MaxMP - (uint)skill.Variable["MedicineHealing2"]))
                    {
                        actor.MP += (uint)skill.Variable["MedicineHealing2"];
                    }
                    else
                    {
                        actor.MP = actor.MaxMP;
                    }                
                    map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, actor, true);
                    //显示
                    SkillHandler.Instance.ShowVessel(actor,0, -skill.Variable["MedicineHealing2"]);
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

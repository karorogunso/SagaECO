using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Skill;

namespace SagaMap.Skill.Additions.Global
{
    public class Medicine2 : DefaultBuff
    {
        public Medicine2(SagaDB.Skill.Skill skill, Actor actor, int lifetime)
            : base(skill, actor, "Medicine2", lifetime, (int)(2000.0f * (1.0f - (float)((float)Math.Max(actor.Status.cspd, actor.Status.aspd) / 1000f))))
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

            int mpadd = actor.Status.hp_medicine;
            skill.Variable.Add("MedicineHealing2", mpadd);
        }

        void EndEvent(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);

            uint recover = (uint)skill.Variable["MedicineHealing2"];
            if (actor.Status.Additions.ContainsKey("FoodFighter"))
            {
                DefaultPassiveSkill dps = actor.Status.Additions["FoodFighter"] as DefaultPassiveSkill;
                float rate = ((float)dps.Variable["FoodFighter"] / 100.0f + 1.0f);
                recover = (uint)((float)recover * rate);
            }
            if (actor.HP > 0 && !actor.Buff.Dead)
            {
                if (actor.MP < (actor.MaxMP - recover))
                {
                    actor.MP += recover;
                }
                else
                {
                    actor.MP = actor.MaxMP;
                }
            }
            SkillHandler.Instance.ShowVessel(actor, 0, -(int)recover);
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, actor, true);
            actor.Status.mp_medicine -= (short)skill.Variable["MedicineHealing2"];
        }

        void TimerUpdate(Actor actor, DefaultBuff skill)
        {
            //测试去除技能同步锁ClientManager.EnterCriticalArea();
            try
            {
                if (actor.HP > 0 && !actor.Buff.Dead)
                {
                    Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
                    uint recover = (uint)skill.Variable["MedicineHealing2"];
                    if (actor.Status.Additions.ContainsKey("FoodFighter"))
                    {
                        DefaultPassiveSkill dps = actor.Status.Additions["FoodFighter"] as DefaultPassiveSkill;
                        float rate = ((float)dps.Variable["FoodFighter"] / 100.0f + 1.0f);
                        recover = (uint)((float)recover * rate);
                    }
                    if (actor.HP > 0 && !actor.Buff.Dead)
                    {
                        if (actor.MP < (actor.MaxMP - recover))
                        {
                            actor.MP += recover;
                        }
                        else
                        {
                            actor.MP = actor.MaxMP;
                        }
                    }
                    SkillHandler.Instance.ShowVessel(actor, 0, -(int)recover);
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

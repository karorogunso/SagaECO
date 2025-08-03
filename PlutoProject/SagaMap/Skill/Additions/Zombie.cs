using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Skill;

namespace SagaMap.Skill.Additions.Global
{
    public class Zombie : DefaultBuff 
    {
        public Zombie(Actor actor)
            : base(null, actor, "Zombie", int.MaxValue, 10000)
        {
            this.OnAdditionStart += this.StartEvent;
            this.OnAdditionEnd += this.EndEvent;
            this.OnUpdate += this.TimerUpdate;
        }

        void StartEvent(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            actor.Buff.Zombie = true;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void EndEvent(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            actor.Buff.Zombie = false;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void TimerUpdate(Actor actor, DefaultBuff skill)
        {
            //测试去除技能同步锁ClientManager.EnterCriticalArea();
            try
            {
                if (actor.HP > 0 && !actor.Buff.Dead)
                {
                    Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
                    int amount = (int)(actor.MaxHP / 50);
                    if (amount < 1)
                        amount = 1;
                    if (actor.HP > amount)
                        actor.HP = (uint)(actor.HP - amount);
                    else
                    {
                        actor.HP = 0;
                        actor.Buff.Dead = true;
                        map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
                        actor.e.OnDie();
                        SkillHandler.RemoveAddition(actor, "Zombie");
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

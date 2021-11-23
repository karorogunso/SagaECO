using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaDB.Skill;
using SagaLib;

namespace SagaMap.Skill.Additions.Global
{
    public class HPRecovery : DefaultBuff 
    {
        private bool isMarionette = false;
        public HPRecovery(SagaDB.Skill.Skill skill, Actor actor, int lifetime, int period)
            : base(skill, actor, "HPRecovery", lifetime, period)
        {
            this.OnAdditionStart += this.StartEvent;
            this.OnAdditionEnd += this.EndEvent;
            this.OnUpdate += this.TimerUpdate;
        }
        public HPRecovery(SagaDB.Skill.Skill skill, Actor actor, int lifetime, int period, bool isMarionette)
            : base(skill, actor, isMarionette ? "Marionette_HPRecovery" : "HPRecovery", lifetime, period)
        {
            this.OnAdditionStart += this.StartEvent;
            this.OnAdditionEnd += this.EndEvent;
            this.OnUpdate += this.TimerUpdate;
            this.isMarionette = isMarionette;
        }
        
        void StartEvent(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
            if (actor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)actor;
                if (pc.Marionette != null && isMarionette)
                {
                    pc.Status.hp_recover_skill += 15;
                }
            }
        }

        void EndEvent(Actor actor, DefaultBuff skill)
        {
           Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
           map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
            if (actor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)actor;
                if (pc.Marionette == null && isMarionette)
                {
                    pc.Status.hp_recover_skill -= 15;
                }
            }
        }

        void TimerUpdate(Actor actor, DefaultBuff skill)
        {
            //测试去除技能同步锁ClientManager.EnterCriticalArea();
            try
            {
                Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
                uint hpadd = 0;
                if (isMarionette)
                {
                    ActorPC pc = (ActorPC)actor;
                    if (pc.Marionette == null)
                    {
                        this.AdditionEnd();
                    }
                    hpadd = (uint)(pc.MaxHP * (100 + ((pc.Vit +
                        pc.Status.vit_item +
                        pc.Status.vit_rev) / 3)) / 1500);
                }
                else
                {
                    hpadd = (uint)(actor.Status.hp_recover_skill / 100f * actor.MaxHP);
                }
                if (!actor.Status.Additions.ContainsKey("Sacrifice"))
                    actor.HP += hpadd;
                if (actor.HP > actor.MaxHP)
                {
                    actor.HP = actor.MaxHP;
                }
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, actor, true);
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
            //测试去除技能同步锁ClientManager.LeaveCriticalArea();
        }
    }
}

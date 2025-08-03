using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaDB.Skill;

namespace SagaMap.Skill.Additions.Global
{
    public class SPRecovery : DefaultBuff 
    {
        private bool isMarionette = false;
        public SPRecovery(SagaDB.Skill.Skill skill, Actor actor, int lifetime, int period)
            : base(skill, actor, "SPRecovery", lifetime, period)
        {
            this.OnAdditionStart += this.StartEvent;
            this.OnAdditionEnd += this.EndEvent;
            this.OnUpdate += this.TimerUpdate;
        }
        public SPRecovery(SagaDB.Skill.Skill skill, Actor actor, int lifetime, int period, bool isMarionette)
            : base(skill, actor, isMarionette ? "Marionette_SPRecovery" : "SPRecovery", lifetime, period)
        {
            this.OnAdditionStart += this.StartEvent;
            this.OnAdditionEnd += this.EndEvent;
            this.OnUpdate += this.TimerUpdate;
            this.isMarionette = isMarionette;
        }
        
        void StartEvent(Actor actor, DefaultBuff skill)
        {
            //Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            //map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void EndEvent(Actor actor, DefaultBuff skill)
        {
            //Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            //map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void TimerUpdate(Actor actor, DefaultBuff skill)
        {
            if (!actor.Buff.NoRegen)
            {
                Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
                uint spadd = 0;
                if (isMarionette)
                {
                    ActorPC pc = (ActorPC)actor;
                    if (pc.Marionette == null)
                    {
                        this.AdditionEnd();
                    }
                    spadd = (uint)(pc.MaxSP * (100 + ((pc.Int +
                        pc.Vit + pc.Status.int_item + pc.Status.int_mario +
                        pc.Status.int_rev + pc.Status.vit_rev + pc.Status.vit_mario +
                        pc.Status.vit_item) / 6)) / 2000);
                }
                else
                {
                    ActorPC pc = (ActorPC)actor;
                    spadd = (uint)((pc.MaxSP * (100 + ((pc.Int +
                        pc.Vit + pc.Status.int_item + pc.Status.int_mario +
                        pc.Status.int_rev + pc.Status.vit_rev + pc.Status.vit_mario +
                        pc.Status.vit_item) / 6)) / 2000) + pc.Status.sp_recover_skill);
                }
                actor.SP += spadd;
                if (actor.SP > actor.MaxSP)
                {
                    actor.SP = actor.MaxSP;
                }
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, actor, true);
            }
                
        }
    }
}

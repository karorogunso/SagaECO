using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaDB.Skill;
namespace SagaMap.Skill.Additions.Global
{
    /// <summary>
    /// 居合姿态
    /// </summary>
    public class SnipeMode : DefaultBuff
    {
        public SnipeMode(SagaDB.Skill.Skill skill, Actor actor, int lifetime)
            : base(skill, actor, "狙击模式", lifetime,5000)
        {
            this.OnAdditionStart += this.StartEvent;
            this.OnAdditionEnd += this.EndEvent;
            this.OnUpdate += this.TimerUpdate;
        }

        void StartEvent(Actor actor, DefaultBuff skill)
        {
            if(actor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)actor;
                SagaMap.PC.StatusFactory.Instance.CalcStatus(pc);
                SagaMap.Network.Client.MapClient.FromActorPC(pc).SendRange();
            }
            /*Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            actor.Buff.狂戦士 = true;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);*/
        }

        void EndEvent(Actor actor, DefaultBuff skill)
        {
            if (actor.type == ActorType.PC)
            {
                if (actor.Status.Additions.ContainsKey("狙击模式射程"))
                {
                    actor.Status.Additions["狙击模式射程"].AdditionEnd();
                    actor.Status.Additions.Remove("狙击模式射程");
                }
                ActorPC pc = (ActorPC)actor;
                SagaMap.PC.StatusFactory.Instance.CalcStatus(pc);
                SagaMap.Network.Client.MapClient.FromActorPC(pc).SendRange();
            }
        }
        void TimerUpdate(Actor actor, DefaultBuff skill)
        {
            if (actor.EP >= 15)
                actor.EP -= 15;
            else
            {
                actor.EP = 0;
                this.AdditionEnd();
            }
            actor.e.OnHPMPSPUpdate(actor);
        }
    }
}

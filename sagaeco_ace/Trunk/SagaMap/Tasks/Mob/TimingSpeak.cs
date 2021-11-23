using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using SagaLib;
using SagaDB.Actor;
using SagaMap.Network.Client;
using SagaMap.Manager;

namespace SagaMap.Tasks.Mob
{
    public class TimingSpeak : MultiRunTask
    {
        private Actor actor;
        string message;
        public TimingSpeak(Actor actor, int delay, string message)
        {
            this.dueTime = delay;
            this.period = delay;
            this.actor = actor;
            this.message = message;
        }

        public override void CallBack()
        {
            try
            {
                if (actor != null)
                {
                    SagaMap.Skill.SkillHandler.Instance.ActorSpeak(actor, message);
                }
                this.Deactivate();
            }
            catch (Exception) { }
        }
    }
}

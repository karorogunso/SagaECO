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
    public class RespawnAnnounce : MultiRunTask
    {
        private ActorMob mob;
        public RespawnAnnounce(ActorMob mob, int delay)
        {
            this.dueTime = delay;
            this.period = delay;
            this.mob = mob;
        }

        public override void CallBack()
        {
            try
            {
                ActorEventHandlers.MobEventHandler eh = (SagaMap.ActorEventHandlers.MobEventHandler)mob.e;
                if (eh.AI.Announce != "")
                {
                    foreach (MapClient i in MapClientManager.Instance.OnlinePlayer)
                    {
                        i.SendAnnounce(eh.AI.Announce);
                    }
                }
                this.Deactivate();
            }
            catch (Exception) { }
        }
    }
}

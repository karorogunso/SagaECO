using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using SagaLib;
using SagaDB.Actor;
using SagaMap.Network.Client;

namespace SagaMap.Tasks.Partner
{
    public class TalkAtFreeTime : MultiRunTask
    {
        private MapClient client;
        int count;
        public TalkAtFreeTime(MapClient client)
        {
            this.dueTime = 50000;
            this.period = 50000;
            this.client = client;
            count = 0;
        }

        public override void CallBack()
        {
            ClientManager.EnterCriticalArea();
            try
            {
                if (client.Character.Partner == null)
                    Deactivate();
                ActorPartner partner = client.Character.Partner;
                if (count < 10)
                {
                    if (!partner.Status.Additions.ContainsKey("NotAtFreeTime"))
                    {
                        count++;
                        if (Global.Random.Next(0, 100) < 20)
                            client.PartnerTalking(partner, MapClient.TALK_EVENT.NORMAL, 100);
                    }
                }
                if (partner.Status.Additions.ContainsKey("NotAtFreeTime"))
                    count = 0;
            }
            catch (Exception)
            {
                this.Deactivate();
            }
            ClientManager.LeaveCriticalArea();
        }
    }
}

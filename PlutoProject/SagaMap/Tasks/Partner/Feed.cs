using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using SagaLib;
using SagaDB.Actor;
using SagaMap.Network.Client;

namespace SagaMap.Tasks.Partner
{
    public class Feed : MultiRunTask
    {
        private ActorPartner partner;
        MapClient mc;
        public Feed(MapClient mc ,ActorPartner partner,uint nextfeedtime)
        {
            this.dueTime = (int)(nextfeedtime * 1000);
            this.period = 5000;
            this.partner = partner;
            this.mc = mc;
        }

        public override void CallBack()
        {
            ClientManager.EnterCriticalArea();
            try
            {
                partner.reliabilityuprate = 100;

                partner.Tasks.Remove("Feed");
                this.Deactivate();

                SagaMap.Partner.StatusFactory.Instance.CalcPartnerStatus(partner);
                mc.SendPetBasicInfo();
                mc.SendPetDetailInfo();
                SagaMap.PC.StatusFactory.Instance.CalcStatus(mc.Character);
            }
            catch (Exception)
            {
                this.Deactivate();
            }
            ClientManager.LeaveCriticalArea();
        }
    }
}

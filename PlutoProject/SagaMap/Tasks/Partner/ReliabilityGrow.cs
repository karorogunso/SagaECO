using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using SagaLib;
using SagaDB.Actor;


namespace SagaMap.Tasks.Partner
{
    public class ReliabilityGrow : MultiRunTask
    {
        private ActorPartner partner;
        public ReliabilityGrow(ActorPartner partner)
        {
            this.dueTime = (int)(60 * 1000);
            this.period = (int)(60 * 1000);
            this.partner = partner;
        }

        public override void CallBack()
        {
            ClientManager.EnterCriticalArea();
            try
            {
                //Manager.ExperienceManager.Instance.ApplyPartnerReliabilityEXP(partner, 60);
            }
            catch (Exception)
            {
                this.Deactivate();
            }
            ClientManager.LeaveCriticalArea();
        }
    }
}

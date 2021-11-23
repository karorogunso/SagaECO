using SagaMap.Network.Client;
using SagaMap.Scripting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scripts
{
    public class OutPutFame : Event
    {
        public OutPutFame()
        {
            this.EventID = 9600452;
        }

        public override void OnEvent(SagaDB.Actor.ActorPC pc)
        {
            MapClient client = ((SagaMap.ActorEventHandlers.PCEventHandler)pc.e).Client;
            client.SendSystemMessage("Your Fame now is " + pc.Fame.ToString());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;
using SagaDB.Actor;
using SagaDB.Map;

namespace SagaMap.Scripting
{
    public class Item : Event
    {
        public delegate void OnEventHandler(ActorPC pc);
        public OnEventHandler Handler;

        protected void Init(uint eventID, OnEventHandler handler)
        {
            Item newItem = new Item();
            newItem.EventID = eventID;
            newItem.Handler += handler;
            if (!Manager.ScriptManager.Instance.Events.ContainsKey(eventID))
            {
                Manager.ScriptManager.Instance.Events.Add(eventID, newItem);
            }
        }

        public override void OnEvent(ActorPC pc)
        {
            if (this.Handler != null)
                this.Handler.Invoke(pc);
        }
    }
}

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
    public abstract class EventActor:Event
    {
        ActorEvent actor;

        public ActorEvent Actor { get { return this.actor; } set { this.actor = value; } }

        public abstract EventActor Clone();
    }
}

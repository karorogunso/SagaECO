using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaDB.Actor
{
    public class ActorEvent : Actor
    {
        ActorPC caster;
        uint eventID;
        string title;
        ActorEventTypes atype;
        uint tent_mapID;

        public ActorEvent(ActorPC caster)
        {
            this.type = ActorType.EVENT;
            this.caster = caster;
        }

        public ActorEventTypes Type { get { return this.atype; } set { this.atype = value; } }
        public string Title { get { return this.title; } set { this.title = value; if (this.e != null)e.PropertyUpdate(UpdateEvent.EVENT_TITLE, 0); } }
        public uint EventID { get { return this.eventID; } set { this.eventID = value; } }
        public ActorPC Caster { get { return this.caster; } }
        public uint TentMapID { get { return this.tent_mapID; } set { this.tent_mapID = value; } }
    }
}

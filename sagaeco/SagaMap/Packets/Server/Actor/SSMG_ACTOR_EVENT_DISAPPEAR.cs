using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Map;


namespace SagaMap.Packets.Server
{
    public class SSMG_ACTOR_EVENT_DISAPPEAR : Packet
    {
        public SSMG_ACTOR_EVENT_DISAPPEAR()
        {
            this.data = new byte[6];
            this.offset = 2;
            this.ID = 0x0BB9;
          
        }

        public uint ActorID
        {
            set
            {                
                this.PutUInt(value, 2);
            }
        }
    }
}


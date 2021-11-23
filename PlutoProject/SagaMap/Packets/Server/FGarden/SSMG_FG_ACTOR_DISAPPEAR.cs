using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.FGarden;


namespace SagaMap.Packets.Server
{
    public class SSMG_FG_ACTOR_DISAPPEAR : Packet
    {
        public SSMG_FG_ACTOR_DISAPPEAR()
        {
            this.data = new byte[6];
            this.offset = 2;

            this.ID = 0x1C0D;
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


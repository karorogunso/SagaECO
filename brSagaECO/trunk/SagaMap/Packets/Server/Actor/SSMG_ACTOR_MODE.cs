using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_ACTOR_MODE : Packet
    {
        public SSMG_ACTOR_MODE()
        {
            this.data = new byte[14];
            this.offset = 2;
            this.ID = 0x0FA7;   
        }

        public uint ActorID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }

        public int Mode1
        {
            set
            {
                this.PutInt(value, 6);
            }
        }

        public int Mode2
        {
            set
            {
                this.PutInt(value, 10);
            }
        }

    }
}


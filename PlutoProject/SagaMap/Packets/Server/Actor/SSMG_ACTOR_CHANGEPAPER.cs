using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_ACTOR_CHANGEPAPER : Packet
    {
        public SSMG_ACTOR_CHANGEPAPER()
        {
            this.data = new byte[8];
            this.offset = 2;
            this.ID = 0x23AE;   
        }

        public uint ActorID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }
        public ushort paperID
        {
            set
            {
                this.PutUShort(value, 6);
            }
        }
    }
}


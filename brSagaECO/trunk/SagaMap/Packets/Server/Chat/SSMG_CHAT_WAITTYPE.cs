using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_CHAT_WAITTYPE : Packet
    {
        public SSMG_CHAT_WAITTYPE()
        {
            this.data = new byte[8];
            this.offset = 2;
            this.ID = 0x121E;
        }

        public uint ActorID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }
        public ushort type
        {
            set
            {
                this.PutUShort(value, 6);
            }
        }
    }
}


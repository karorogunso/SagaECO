using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_CHAT_EXPRESSION : Packet
    {
        public SSMG_CHAT_EXPRESSION()
        {
            this.data = new byte[9];
            this.offset = 2;
            this.ID = 0x1D0C;
        }

        public uint ActorID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }

        public byte Motion
        {
            set
            {
                this.PutByte(value, 6);
            }
        }

        public byte Loop
        {
            set
            {
                this.PutByte(value, 7);
            }
        }

        public byte Special
        {
            set
            {
                this.PutByte(value, 8);
            }
        }
    }
}


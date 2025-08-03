using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_AAA_VOICE : Packet
    {
        public SSMG_AAA_VOICE()
        {
            this.data = new byte[3];
            this.offset = 2;
            this.ID = 0x1FB4;
        }

        public ushort VoiceID
        {
            set
            {
                this.PutUShort(value, 2);
            }
        }
    }
}


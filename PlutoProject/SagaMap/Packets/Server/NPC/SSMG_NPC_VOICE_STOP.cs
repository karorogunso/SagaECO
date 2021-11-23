using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_NPC_VOICE_STOP : Packet
    {
        public SSMG_NPC_VOICE_STOP()
        {
            this.data = new byte[6];
            this.offset = 2;
            this.ID = 0x0623;
        }

        public uint VoiceID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }
    }
}


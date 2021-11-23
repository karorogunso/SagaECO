using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_CHAT_EMOTION : Packet
    {
        public SSMG_CHAT_EMOTION()
        {
            this.data = new byte[10];
            this.offset = 2;
            this.ID = 0x1217;   
        }

        public uint ActorID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }

        public uint Emotion
        {
            set
            {
                this.PutUInt(value, 6);
            }
        }
    }
}


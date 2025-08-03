using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap.Scripting;

namespace SagaMap.Scripting
{
    public enum FadeType
    {
        In,
        Out
    }

    public enum FadeEffect
    {
        Black,
        White
    }
}

namespace SagaMap.Packets.Server
{
    public class SSMG_NPC_FADE : Packet
    {
        public SSMG_NPC_FADE()
        {
            this.data = new byte[10];
            this.offset = 2;
            this.ID = 0x05FE;
        }

        public FadeType FadeType
        {
            set
            {
                this.PutByte((byte)value, 2);
            }
        }

        public FadeEffect FadeEffect
        {
            set
            {
                this.PutByte((byte)value, 3);
            }
        }

    }
}


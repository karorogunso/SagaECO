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
            this.data = new byte[4];
            this.offset = 2;
            if (Configuration.Instance.Version>=SagaLib.Version.Saga18)
                this.ID = 0x05FE;
            else
                this.ID = 0x05FA;
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


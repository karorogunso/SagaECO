using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_CHAT_EXPRESSION_UNLOCK : Packet
    {
        public SSMG_CHAT_EXPRESSION_UNLOCK()
        {
            if (Configuration.Instance.Version >= SagaLib.Version.Saga18)
                this.data = new byte[15];
            else
            this.data = new byte[6];
            this.offset = 2;
            this.ID = 0x1D06;
        }

        public uint unlock
        {
            set
            {
                if (Configuration.Instance.Version >= SagaLib.Version.Saga18)
                {
                    this.PutByte(0x03, 2);
                    this.PutUInt(value, 3);
                }
                    this.PutUInt(value, 2);
            }
        }

        public uint unlock2
        {
            set
            {
                if (Configuration.Instance.Version >= SagaLib.Version.Saga18)
                    this.PutUInt(value, 4);
            }
        }
    }
}


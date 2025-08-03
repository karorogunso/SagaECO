using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap.Manager;

namespace SagaMap.Packets.Server
{
    public class SSMG_ODWAR_RESULT : Packet
    {
        public SSMG_ODWAR_RESULT()
        {
            this.data = new byte[16];
            this.offset = 2;
            this.ID = 0x1B85;
            this.PutByte(1, 2);
        }

        public bool Win
        {
            set
            {
                if (value)
                    this.PutByte(1, 3);
            }
        }

        public uint EXP
        {
            set
            {
                this.PutUInt(value, 4);
            }
        }

        public uint JEXP
        {
            set
            {
                this.PutUInt(value, 8);
            }
        }

        public uint CP
        {
            set
            {
                this.PutUInt(value, 12);
            }
        }


    }
}


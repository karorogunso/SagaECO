using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_SKILL_CAST_CANCEL : Packet
    {
        public SSMG_SKILL_CAST_CANCEL()
        {
            this.data = new byte[7];
            this.offset = 2;
            this.ID = 0x138A;   
        }

        public uint ActorID
        {
            set
            {
                PutUInt(value, 2);
            }
        }
        public byte result
        {
            set
            {
                PutByte(value, 6);
            }
        }
    }
}


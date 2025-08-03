using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;

namespace SagaMap.Packets.Server
{
    public class SSMG_ABYSSTEAM_REPLY : Packet
    {
        public SSMG_ABYSSTEAM_REPLY()
        {
            this.data = new byte[3];
            this.offset = 2;
            this.ID = 0x22F0;
        }
        public byte Result
        {
            set
            {
                this.PutByte(value, 2);
            }
        }
    }
}
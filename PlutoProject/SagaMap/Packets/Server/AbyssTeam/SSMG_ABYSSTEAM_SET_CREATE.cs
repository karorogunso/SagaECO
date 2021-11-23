using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;

namespace SagaMap.Packets.Server
{
    public class SSMG_ABYSSTEAM_SET_CREATE : Packet
    {
        public SSMG_ABYSSTEAM_SET_CREATE()
        {
            this.data = new byte[3];
            this.offset = 2;
            this.ID = 0x22EA;
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
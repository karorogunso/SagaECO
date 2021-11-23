using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;

namespace SagaMap.Packets.Server
{
    public class SSMG_FISHING_RESULT : Packet
    {
        public SSMG_FISHING_RESULT()
        {
            this.data = new byte[10];
            this.offset = 2;
            this.ID = 0x2167;
            this.PutByte(1, 2);
            this.PutByte(1, 4);
            this.PutByte(1, 9);
        }

        public byte IsSucceed
        {
            set
            {
                this.PutByte(value, 3);
            }
        }
        public uint ItemID
        {
            set
            {
                this.PutUInt(value, 5);
            }
        }
    }
}


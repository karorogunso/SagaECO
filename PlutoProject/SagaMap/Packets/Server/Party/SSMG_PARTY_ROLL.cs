using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Party;


namespace SagaMap.Packets.Server
{
    public class SSMG_PARTY_ROLL : Packet
    {
        
        public SSMG_PARTY_ROLL()
        {
            this.data = new byte[7];
            this.offset = 2;
            this.ID = 0x1A00;
        }

        public byte status
        {
            set
            {
                PutByte(value, 6);
            }
        }
    }
}


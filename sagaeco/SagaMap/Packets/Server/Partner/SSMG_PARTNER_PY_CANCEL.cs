using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_PARTNER_PY_CANCEL : Packet
    {
        public SSMG_PARTNER_PY_CANCEL()
        {
            this.data = new byte[3];
            this.offset = 2;
            this.ID = 0x17A5;
        }
        public byte Type
        {
            set
            {
                this.PutByte(value, 2);
            }
        }
    }
}
        

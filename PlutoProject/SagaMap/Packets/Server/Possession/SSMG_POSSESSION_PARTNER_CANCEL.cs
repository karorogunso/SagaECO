using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_POSSESSION_PARTNER_CANCEL : Packet
    {
        public SSMG_POSSESSION_PARTNER_CANCEL()
        {
            this.data = new byte[3];
            this.offset = 2;
            this.ID = 0x17A5;
        }
        public PossessionPosition Pos
        {
            set
            {
                this.PutByte((Byte)value, 2);
            }
        }
    }
}
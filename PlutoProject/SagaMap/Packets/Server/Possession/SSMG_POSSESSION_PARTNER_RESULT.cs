using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_POSSESSION_PARTNER_RESULT : Packet
    {
        public SSMG_POSSESSION_PARTNER_RESULT()
        {
            this.data = new byte[11];
            this.offset = 2;
            this.ID = 0x17A3;
        }

        public uint InventorySlot
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }
        public PossessionPosition Pos
        {
            set
            {
                this.PutByte((Byte)value, 6);
            }
        }
        public uint FromID
        {
            set
            {
                this.PutUInt(value, 6);
            }
        }
    }
}
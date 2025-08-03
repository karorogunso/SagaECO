using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_PARTNER_AI_MODE_SELECTION : Packet
    {
        public SSMG_PARTNER_AI_MODE_SELECTION()
        {
            this.data = new byte[8];
            this.offset = 2;
            this.ID = 0x2181;
        }

        public uint PartnerInventorySlot
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }
        /// <summary>
        /// start at 0
        /// </summary>
        public byte AIMode
        {
            set
            {
                this.PutByte(value, 6);
            }
        }

        public byte unknown0
        {
            set
            {
                this.PutByte(value, 7);
            }
        }
    }
}
        

using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_PARTNER_AI_SETUP_RESULT : Packet
    {
        public SSMG_PARTNER_AI_SETUP_RESULT()
        {
            this.data = new byte[7];
            this.offset = 2;
            this.ID = 0x2185;
        }
        /// <summary>
        /// 0 for success, 1 for failure
        /// </summary>
        public byte Success
        {
            set
            {
                this.PutByte(value, 2);
            }
        }

        public uint PartnerInventorySlot
        {
            set
            {
                this.PutUInt(value, 3);
            }
        }
    }
}
        

using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_PARTNER_CUBE_UPDATE : Packet
    {
        public SSMG_PARTNER_CUBE_UPDATE()
        {
            this.data = new byte[8];
            this.offset = 2;
            this.ID = 0x2187;
        }

        public uint PartnerInventorySlot
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }
        public ushort CubeUniqueID
        {
            set
            {
                this.PutUShort(value, 6);
            }
        }
    }
}
        

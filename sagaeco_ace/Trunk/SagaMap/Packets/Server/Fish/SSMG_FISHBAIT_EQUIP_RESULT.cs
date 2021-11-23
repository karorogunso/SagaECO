using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;

namespace SagaMap.Packets.Server
{
    public class SSMG_FISHBAIT_EQUIP_RESULT : Packet
    {
        public SSMG_FISHBAIT_EQUIP_RESULT()
        {
            this.data = new byte[7];
            this.offset = 2;
            this.ID = 0x216D;
        }

        public uint InventoryID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }
        public byte IsEquip
        {
            set
            {
                this.PutByte(value, 6);
            }
        }
    }
}


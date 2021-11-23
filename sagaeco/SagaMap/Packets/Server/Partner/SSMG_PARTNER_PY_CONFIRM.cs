using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_PARTNER_PY_CONFIRM : Packet
    {
        public SSMG_PARTNER_PY_CONFIRM()
        {
            this.data = new byte[11];
            this.offset = 2;
            this.ID = 0x17A3;
        }
        public uint SlotID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }
        public byte Type
        {
            set
            {
                this.PutByte(value, 6);
            }
        }
        public uint unknown
        {
            set
            {
                this.PutUInt(value, 7);
            }
        }
    }
}
        

using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_PARTNER_PY_BROAD : Packet
    {
        public SSMG_PARTNER_PY_BROAD()
        {
            this.data = new byte[19];
            this.offset = 2;
            this.ID = 0x17A3;
        }
        public uint ActorID
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
        public uint PictID
        {
            set
            {
                this.PutUInt(value, 7);
                this.PutUInt(value, 11);
            }
        }
    }
}
        

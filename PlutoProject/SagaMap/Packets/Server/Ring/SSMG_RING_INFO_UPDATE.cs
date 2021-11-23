using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Ring;


namespace SagaMap.Packets.Server
{
    public class SSMG_RING_INFO_UPDATE : Packet
    {
        public SSMG_RING_INFO_UPDATE()
        {
            this.data = new byte[12];
            this.offset = 2;
            this.ID = 0x1AD8;
        }

        public uint RingID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }

        public uint Fame
        {
            set
            {
                this.PutUInt(value, 6);
            }
        }

        public byte CurrentMember
        {
            set
            {
                this.PutByte(value, 10);
            }
        }

        public byte MaxMember
        {
            set
            {
                this.PutByte(value, 11);
            }
        }
    }
}


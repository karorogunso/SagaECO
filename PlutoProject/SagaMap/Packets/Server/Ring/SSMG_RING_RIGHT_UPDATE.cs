using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Ring;


namespace SagaMap.Packets.Server
{
    public class SSMG_RING_RIGHT_UPDATE : Packet
    {
        public SSMG_RING_RIGHT_UPDATE()
        {
            this.data = new byte[14];
            this.offset = 2;
            this.ID = 0x1AD7;
        }

        public uint Unknown
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }

        public uint CharID
        {
            set
            {
                this.PutUInt(value, 6);
            }
        }

        public int Right
        {
            set
            {
                this.PutInt(value, 10);
            }
        }
    }
}


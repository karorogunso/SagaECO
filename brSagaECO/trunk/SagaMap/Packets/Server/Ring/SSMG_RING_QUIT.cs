using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Ring;


namespace SagaMap.Packets.Server
{
    public class SSMG_RING_QUIT : Packet
    {
        public enum Reasons
        {
            DISSOLVE = 1,
            LEAVE,
            KICK
        }

        public SSMG_RING_QUIT()
        {
            this.data = new byte[12];
            this.offset = 2;
            this.ID = 0x1ACD;

            this.PutByte(1, 6);
        }

        public uint RingID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }

        public Reasons Reason
        {
            set
            {
                this.PutInt((int)value, 8);
            }
        }
    }
}


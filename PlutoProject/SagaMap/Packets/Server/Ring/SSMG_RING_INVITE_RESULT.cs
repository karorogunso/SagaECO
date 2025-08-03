using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Ring;


namespace SagaMap.Packets.Server
{
    public class SSMG_RING_INVITE_RESULT : Packet
    {
        public enum RESULT
        {
            OK = 0,
            CANNOT_FIND_TARGET = -1,
            SERVER_ERROR = -2,
            TARGET_NO_RING_INVITE = -3,
            TARGET_ALREADY_IN_RING = -4,
            NO_RING = -5,
            NO_RIGHT = -6,
        }

        public SSMG_RING_INVITE_RESULT()
        {
            this.data = new byte[6];
            this.offset = 2;
            this.ID = 0x1AB3;
        }

        public int Result
        {
            set
            {
                this.PutInt(value, 2);
            }
        }
    }
}


using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap.Manager;

namespace SagaMap.Packets.Server
{
    public class SSMG_IRIS_CARD_INSERT_RESULT : Packet
    {
        public enum Results
        {
            OK = 0,
            CANNOT_SET_NOW = -1,
            CANNOT_SET = -2,
            SLOT_OVER = -3,
        }

        public SSMG_IRIS_CARD_INSERT_RESULT()
        {
            this.data = new byte[6];
            this.offset = 2;
            this.ID = 0x1DB7;
        }

        public Results Result
        {
            set
            {
                this.PutInt((int)value, 2);
            }
        }
    }
}


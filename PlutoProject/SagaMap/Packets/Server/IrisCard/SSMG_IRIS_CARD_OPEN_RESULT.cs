using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap.Manager;

namespace SagaMap.Packets.Server
{
    public class SSMG_IRIS_CARD_OPEN_RESULT : Packet
    {
        public enum Results
        {
            OK = 0,
            NO_ITEM = -1,
            CANNOT_SET_NOW = -2,
            NO_SLOT = -3,
        }

        public SSMG_IRIS_CARD_OPEN_RESULT()
        {
            this.data = new byte[6];
            this.offset = 2;
            this.ID = 0x1DB1;
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


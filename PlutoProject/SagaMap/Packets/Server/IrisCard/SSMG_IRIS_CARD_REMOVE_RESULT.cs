using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap.Manager;

namespace SagaMap.Packets.Server
{
    public class SSMG_IRIS_CARD_REMOVE_RESULT : Packet
    {
        public enum Results
        {
            OK = 0,
            CANNOT_REMOVE_NOW = -1,
            FAILED = -2,
        }

        public SSMG_IRIS_CARD_REMOVE_RESULT()
        {
            this.data = new byte[6];
            this.offset = 2;
            this.ID = 0x1DBC;
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


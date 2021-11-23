using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap.Manager;

namespace SagaMap.Packets.Server
{
    public class SSMG_IRIS_ADD_SLOT_RESULT : Packet
    {
        public enum Results
        {
            OK = 1,
            Failed = 0,
            NOT_ENOUGH_GOLD = -1,
            NO_ITEM = -2,
            NO_MATERIAL = -3,
            NO_RIGHT_MATERIAL = -4
        }

        public SSMG_IRIS_ADD_SLOT_RESULT()
        {
            this.data = new byte[10];
            this.offset = 2;
            this.ID = 0x13E6;
        }

        public int Result
        {
            set
            {
                this.PutByte((byte)value, 2);
            }
        }
    }
}


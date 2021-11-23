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
            NO_ITEM = 254,
            NO_MATERIAL = 253,
            NO_RIGHT_MATERIAL = -4
        }

        public SSMG_IRIS_ADD_SLOT_RESULT()
        {
            this.data = new byte[3];
            this.offset = 2;
            this.ID = 0x13E4;
        }

        public Results Result
        {
            set
            {
                this.PutByte((byte)value, 2);
                this.PutByte(0x00);
                this.PutByte(0x64);
            }
        }
    }
}


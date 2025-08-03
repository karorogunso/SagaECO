using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Iris;
using SagaMap.Manager;

namespace SagaMap.Packets.Server
{
    public class SSMG_IRIS_CARD_ASSEMBLE_RESULT : Packet
    {
        public enum Results
        {
            OK = 1,
            FAILED = 0,
            NOT_ENOUGH_GOLD = -1,
            ITEM_FULL = -2,
            NO_ITEM = -3,
            SUCCESS_NOT_ENOUGH_ITEM = 0xfd,
        }

        public SSMG_IRIS_CARD_ASSEMBLE_RESULT()
        {
            this.data = new byte[3];
            this.offset = 2;
            this.ID = 0x140C;
        }

        public Results Result
        {
            set
            {
                this.PutByte((byte)value, 2);
                this.PutUShort(0x64);
            }
        }
    }
}


using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Ring;


namespace SagaMap.Packets.Server
{
    public class SSMG_RING_EMBLEM_UPLOAD_RESULT : Packet
    {
        public enum Results
        {
            OK = 0,
            SERVER_ERROR = -1,
            WRONG_FORMAT = -2,
            FAME_NOT_ENOUGH = -3,
        }

        public SSMG_RING_EMBLEM_UPLOAD_RESULT()
        {
            this.data = new byte[6];
            this.offset = 2;
            this.ID = 0x1ADC;
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


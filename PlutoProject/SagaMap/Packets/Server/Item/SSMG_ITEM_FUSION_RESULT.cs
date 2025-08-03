using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_ITEM_FUSION_RESULT : Packet
    {
        public enum FusionResult
        {
            OK = 0,
            FAILED = -1,
            CANCELED = -2,
            NOT_ENOUGH_GOLD = -3,
            NOT_FIT = -4,
            TYPE_NOT_FIT = -5,
            GENDER_NOT_FIT = -6,
            JOB_NOT_FIT = -7,
            KNIGHT_NOT_FIT = -8,
            EVENT_ITEM = -9,
            LV_TOO_LOW = -10,
            UNKNOWN_ERROR = -30,
        }

        public SSMG_ITEM_FUSION_RESULT()
        {
            this.data = new byte[3];
            this.offset = 2;
            this.ID = 0x13DA;
        }

        public FusionResult Result
        {
            set
            {
                this.PutByte((byte)value, 2);
            }
        }
    }
}


using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.FGarden;


namespace SagaMap.Packets.Server
{
    public class SSMG_DEM_COST_LIMIT_UPDATE : Packet
    {
        public enum Results
        {
            OK = 0,
            FAILED = -1,
            NOT_ENOUGH_EP = -2,
            CL_MAXIMUM = -3,
            LV_MAXIMUM = -4,
        }

        public SSMG_DEM_COST_LIMIT_UPDATE()
        {
            this.data = new byte[9];
            this.offset = 2;
            this.ID = 0x1E5D;
        }

        public Results Result
        {
            set
            {
                this.PutByte((byte)value, 2);
            }
        }

        public short CurrentEP
        {
            set
            {
                this.PutShort(value, 3);
            }
        }

        public short EPRequired
        {
            set
            {
                this.PutShort(value, 5);
            }
        }

        public short CL
        {
            set
            {
                this.PutShort(value, 7);
            }
        }
    }
}


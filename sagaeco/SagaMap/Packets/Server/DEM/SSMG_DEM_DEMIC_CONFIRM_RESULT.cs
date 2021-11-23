using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.FGarden;


namespace SagaMap.Packets.Server
{
    public class SSMG_DEM_DEMIC_CONFIRM_RESULT : Packet
    {
        public enum Results
        {
            OK,
            FAILED = -1,
            TOO_MANY_ITEMS = -2,
            NOT_ENOUGH_EP = -3,
        }

        public SSMG_DEM_DEMIC_CONFIRM_RESULT()
        {
            this.data = new byte[4];
            this.offset = 2;
            this.ID = 0x1E4F;
            
        }

        public byte Page
        {
            set
            {
                this.PutByte(value, 2);
            }
        }

        public Results Result
        {
            set
            {
                this.PutByte((byte)value, 3);
            }
        }
    }
}


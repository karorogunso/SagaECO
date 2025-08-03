using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.FGarden;


namespace SagaMap.Packets.Server
{
    public class SSMG_DEM_COST_LIMIT : Packet
    {
        public SSMG_DEM_COST_LIMIT()
        {
            this.data = new byte[6];
            this.offset = 2;
            this.ID = 0x1E5A;
        }

        public short CurrentEP
        {
            set
            {
                this.PutShort(value, 2);
            }
        }

        public short EPRequired
        {
            set
            {
                this.PutShort(value, 4);
            }
        }
    }
}


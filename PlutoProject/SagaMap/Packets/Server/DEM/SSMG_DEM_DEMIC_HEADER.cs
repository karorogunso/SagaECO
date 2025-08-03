using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.FGarden;


namespace SagaMap.Packets.Server
{
    public class SSMG_DEM_DEMIC_HEADER : Packet
    {
        public SSMG_DEM_DEMIC_HEADER()
        {
            this.data = new byte[12];
            this.offset = 2;
            this.ID = 0x1E46;

            this.PutShort(9, 4);//Unknown1
            this.PutShort(0x1C0, 6);//Unknown2
            this.PutShort(0xD0, 8);//Unknown2

        }

        public short CL
        {
            set
            {
                this.PutShort(value, 2);
            }
        }
    }
}


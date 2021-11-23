using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.FGarden;
using SagaDB.DEMIC;

namespace SagaMap.Packets.Server
{
    public class SSMG_DEM_CHIP_SHOP_HEADER : Packet
    {
        public SSMG_DEM_CHIP_SHOP_HEADER()
        {
            this.data = new byte[6];
            this.offset = 2;
            this.ID = 0x0639;            
        }

        public uint CategoryID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }
    }
}


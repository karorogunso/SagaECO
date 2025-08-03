using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.FGarden;


namespace SagaMap.Packets.Server
{
    public class SSMG_DEM_DEMIC_FOOTER : Packet
    {
        public SSMG_DEM_DEMIC_FOOTER()
        {
            this.data = new byte[2];
            this.offset = 2;
            this.ID = 0x1E4B;
        }
    }
}


using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Ring;


namespace SagaMap.Packets.Server
{
    public class SSMG_TEST_EVOLVE_OPEN3 : Packet
    {
        public SSMG_TEST_EVOLVE_OPEN3()
        {
            this.data = new byte[4];
            this.offset = 2;
            this.ID = 0x1EDC;
            this.PutUShort(0x6E, 2);
        }
    }
}


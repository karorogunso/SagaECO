using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Ring;


namespace SagaMap.Packets.Server
{
    public class SSMG_TEST_EVOLVE_OPEN2 : Packet
    {
        public SSMG_TEST_EVOLVE_OPEN2()
        {
            this.data = new byte[3];
            this.offset = 2;
            this.ID = 0x0605;
            this.PutByte(1, 2);
        }
    }
}


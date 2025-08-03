using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Party;


namespace SagaMap.Packets.Server
{
    public class SSMG_PPROTECT_CREATED_RESULT : Packet
    {
        public SSMG_PPROTECT_CREATED_RESULT()
        {
            this.data = new byte[3];
            //this.offset = 2;
            this.ID = 0x2362;
        }
    }
}


using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Party;


namespace SagaMap.Packets.Server
{
    public class SSMG_PPROTECT_INITI : Packet
    {
        public SSMG_PPROTECT_INITI()
        {
            this.data = new byte[2];
            //this.offset = 2;
            this.ID = 0x235A;
        }
    }
}


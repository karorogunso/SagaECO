using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.PProtect;


namespace SagaMap.Packets.Server
{
    public class SSMG_PPROTECT_READY_RESULT : Packet
    {
        public SSMG_PPROTECT_READY_RESULT()
        {
            this.data = new byte[3];
            this.offset = 2;
            this.ID = 0x2375;
        }

        public byte Code
        {
            set
            {
                this.PutByte(value, 2);
            }
        }


    }
}


using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.PProtect;


namespace SagaMap.Packets.Server
{
    public class SSMG_PPROTECT_READY : Packet
    {
        public SSMG_PPROTECT_READY()
        {
            this.data = new byte[4];
            this.offset = 2;
            this.ID = 0x2370;
        }

        public byte Index
        {
            set
            {
                this.PutByte(value, 2);
            }
        }

        public byte Code
        {
            set
            {
                this.PutByte(value, 3);
            }
        }
    }
}


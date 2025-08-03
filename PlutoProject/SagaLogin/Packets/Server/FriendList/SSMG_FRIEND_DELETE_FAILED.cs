using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;

using SagaLib;

namespace SagaLogin.Packets.Server
{
    public class SSMG_FRIEND_DELETE : Packet
    {
        public SSMG_FRIEND_DELETE()
        {
            this.data = new byte[6];
            this.ID = 0x00D8;
        }

        public uint CharID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }
    }
}


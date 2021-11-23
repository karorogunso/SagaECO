using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;

using SagaLib;

namespace SagaLogin.Packets.Server
{
    public class SSMG_FRIEND_ADD_OK : Packet
    {
        public SSMG_FRIEND_ADD_OK()
        {
            this.data = new byte[6];
            this.ID = 0x00D5;
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


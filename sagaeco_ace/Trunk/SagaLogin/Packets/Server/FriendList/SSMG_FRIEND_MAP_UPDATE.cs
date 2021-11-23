using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;

using SagaLib;

namespace SagaLogin.Packets.Server
{
    public class SSMG_FRIEND_MAP_UPDATE : Packet
    {
        public SSMG_FRIEND_MAP_UPDATE()
        {
            this.data = new byte[10];
            this.ID = 0x00E8;
        }

        public uint CharID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }

        public uint MapID
        {
            set
            {
                this.PutUInt(value, 6);
            }
        }
    }
}


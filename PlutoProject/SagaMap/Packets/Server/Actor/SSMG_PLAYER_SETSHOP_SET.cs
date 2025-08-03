using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_PLAYER_SETSHOP_SET : Packet
    {
        public SSMG_PLAYER_SETSHOP_SET()
        {
            this.data = new byte[3];
            this.offset = 2;
            this.ID = 0x181E;
        }
    }
}


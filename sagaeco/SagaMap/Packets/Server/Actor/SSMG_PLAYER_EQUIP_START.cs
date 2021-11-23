using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_PLAYER_EQUIP_START : Packet
    {
        public SSMG_PLAYER_EQUIP_START()
        {
            this.data = new byte[3];
            this.ID = 0x0263;
        }
    }
}
        

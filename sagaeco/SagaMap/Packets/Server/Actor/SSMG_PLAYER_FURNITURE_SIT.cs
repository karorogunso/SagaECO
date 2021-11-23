using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_PLAYER_FURNITURE_SIT : Packet
    {
        public SSMG_PLAYER_FURNITURE_SIT()
        {
            this.data = new byte[14];
            this.ID = 0x2065;
        }

        public uint ActorID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }
        public uint FurnitureID
        {
            set
            {
                this.PutUInt(value, 6);
            }
        }
        public int unknown
        {
            set
            {
                this.PutInt(value, 10);
            }
        }
    }
}
        

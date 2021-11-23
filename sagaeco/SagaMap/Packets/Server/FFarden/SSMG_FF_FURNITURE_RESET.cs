using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.FGarden;
using SagaMap.Manager;

namespace SagaMap.Packets.Server
{
    public class SSMG_FF_FURNITURE_RESET : Packet
    {
        public SSMG_FF_FURNITURE_RESET()
        {
            this.data = new byte[14];
            this.offset = 2;
            this.ID = 0x2062;
        }

        public uint AID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }
        public uint ActorID
        {
            set
            {
                this.PutUInt(value, 6);
            }
        }
        public uint RindID
        {
            set
            {
                this.PutUInt(value, 10);
            }
        }
    }
}

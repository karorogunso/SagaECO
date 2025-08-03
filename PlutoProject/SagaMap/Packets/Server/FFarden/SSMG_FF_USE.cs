using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.FGarden;
using SagaMap.Manager;

namespace SagaMap.Packets.Server
{
    public class SSMG_FF_USE : Packet
    {
        public SSMG_FF_USE()
        {
            this.data = new byte[10];
            this.offset = 2;
            this.ID = 0x205C;
        }
        public uint actorID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }
        public ushort motion
        {
            set
            {
                this.PutUShort(value, 6);
                this.PutUShort(value, 8);
            }
        }
    }
}

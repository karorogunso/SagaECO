using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.FGarden;


namespace SagaMap.Packets.Server
{
    public class SSMG_FF_CASTLE_APPEAR : Packet
    {
        public SSMG_FF_CASTLE_APPEAR()
        {
            this.data = new byte[16];
            this.offset = 2;
            this.ID = 0x2044;
        }

        public uint ActorID
        {
            set
            {
                this.PutUInt(1, 2);
            }
        }

        public uint UnknownID
        {
            set
            {
                this.PutUInt(0, 6);
            }
        }
        public ushort X
        {
            set
            {
                this.PutUShort(value, 10);
            }
        }
        public ushort Z
        {
            set
            {
                this.PutUShort(value, 12);
            }
        }
        public ushort Yaxis
        {
            set
            {
                this.PutUShort(value, 14);
            }
        }

    }
}


using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.FGarden;


namespace SagaMap.Packets.Server
{
    public class SSMG_FG_FURNITURE_MOTION : Packet
    {
        public SSMG_FG_FURNITURE_MOTION()
        {
            this.data = new byte[10];
            this.offset = 2;
            this.ID = 0x1C08;
        }

        public uint ActorID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }

        public ushort Motion
        {
            set
            {
                this.PutUShort(value, 6);
            }
        }

        public ushort EndMotion
        {
            set
            {
                this.PutUShort(value, 8);
            }
        }

        public short Z
        {
            set
            {
                this.PutShort(value, 10);
            }
        }

        public ushort Dir
        {
            set
            {
                this.PutUShort(value, 12);
            }
        }
    }
}


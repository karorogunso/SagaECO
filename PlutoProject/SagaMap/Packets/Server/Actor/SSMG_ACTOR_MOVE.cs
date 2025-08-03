using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_ACTOR_MOVE : Packet
    {
        public SSMG_ACTOR_MOVE()
        {
            this.data = new byte[14];
            this.offset = 2;
            this.ID = 0x11F9;   
        }

        public uint ActorID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }

        public short X
        {
            set
            {
                this.PutShort(value, 6);
            }
        }

        public short Y
        {
            set
            {
                this.PutShort(value, 8);
            }
        }

        public ushort Dir
        {
            set
            {
                this.PutUShort(value, 10);
            }
        }

        public MoveType MoveType
        {
            set
            {
                this.PutUShort((ushort)value, 12);
            }
        }


    }
}


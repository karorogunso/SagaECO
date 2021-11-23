using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_ACTOR_PC_APPEAR : Packet
    {
        public SSMG_ACTOR_PC_APPEAR()
        {
            this.data = new byte[24];
            this.offset = 2;
            this.ID = 0x120C;   
        }

        public uint ActorID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }

        public byte X
        {
            set
            {
                this.PutByte(value, 6);
            }
        }

        public byte Y
        {
            set
            {
                this.PutByte(value, 7);
            }
        }

        public ushort Speed
        {
            set
            {
                this.PutUShort(value, 8);
            }
        }

        public byte Dir
        {
            set
            {
                this.PutByte(value, 10);
            }
        }

        public uint PossessionActorID
        {
            set
            {
                this.PutUInt(value, 11);
            }
        }

        public PossessionPosition PossessionPosition
        {
            set
            {
                this.PutByte((byte)value, 15);
            }
        }

        public uint HP
        {
            set
            {
                this.PutUInt(value, 16);
            }
        }

        public uint MaxHP
        {
            set
            {
                this.PutUInt(value, 20);
            }
        }
    }
}


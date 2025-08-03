using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_ACTOR_MOB_APPEAR : Packet
    {
        public SSMG_ACTOR_MOB_APPEAR()
        {
            this.data = new byte[31];
            this.offset = 2;
            this.ID = 0x1220;   
        }

        public uint ActorID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }

        public uint MobID
        {
            set
            {
                this.PutUInt(value, 6);
            }
        }

        public byte X
        {
            set
            {
                this.PutByte(value, 10);
            }
        }

        public byte Y
        {
            set
            {
                this.PutByte(value, 11);
            }
        }

        public ushort Speed
        {
            set
            {
                this.PutUShort(value, 12);
            }
        }

        public byte Dir
        {
            set
            {
                this.PutByte(value, 14);
            }
        }

        public uint HP
        {
            set
            {
                this.PutUInt(value, 15);
                this.PutUInt(value, 23);
            }
        }        

        public uint MaxHP
        {
            set
            {
                this.PutUInt(value, 19);
                this.PutUInt(value, 27);
            }
        }
    }
}


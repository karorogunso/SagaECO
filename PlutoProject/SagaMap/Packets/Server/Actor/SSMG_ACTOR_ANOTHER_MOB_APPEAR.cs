using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_ACTOR_ANOTHER_MOB_APPEAR : Packet
    {
        public SSMG_ACTOR_ANOTHER_MOB_APPEAR()
        {
            this.data = new byte[35];
            this.offset = 2;
            this.ID = 0x2328;   
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
        public uint Camp
        {
            set
            {
                this.PutUInt(value, 10);
            }
        }
        public byte X
        {
            set
            {
                this.PutByte(value, 14);
            }
        }

        public byte Y
        {
            set
            {
                this.PutByte(value, 15);
            }
        }

        public ushort Speed
        {
            set
            {
                this.PutUShort(value, 16);
            }
        }

        public byte Dir
        {
            set
            {
                this.PutByte(value, 18);
            }
        }

        public uint HP
        {
            set
            {
                //this.PutUInt(value, 19);
                this.PutUInt(value, 23);
            }
        }        

        public uint MaxHP
        {
            set
            {
                //this.PutUInt(value, 19);
                this.PutUInt(value, 31);
            }
        }
    }
}


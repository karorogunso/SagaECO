using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_NPC_SHOW_EFFECT : Packet
    {
        public SSMG_NPC_SHOW_EFFECT()
        {
            this.data = new byte[25];
            this.offset = 2;
            this.ID = 0x0600;
            this.PutByte(0xff, 10);
            this.PutByte(0xff, 11);
            this.PutUInt(0xffffffff, 12);
            this.PutByte(0xff, 18);
            this.PutUInt(0xffffffff, 19);
            this.PutByte(0xff, 23);
        }

        public uint ActorID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }

        public uint EffectID
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

        public ushort height
        {
            set
            {
                this.PutUShort(value, 16);
            }
        }

        public bool OneTime
        {
            set
            {
                if (value)
                    this.PutByte(1, 24);
                else
                    this.PutByte(0, 24);
            }
        }
    }
}


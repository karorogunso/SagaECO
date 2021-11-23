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
            this.data = new byte[14];
            this.offset = 2;
            this.ID = 0x05FC;
            this.PutByte(0xff, 10);
            this.PutByte(0xff, 11);
            this.PutByte(1, 12);
            this.PutByte(0xA0, 13);
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

        public bool OneTime
        {
            set
            {
                if (value)
                    this.PutByte(1, 12);
                else
                    this.PutByte(0, 12);
            }
        }
    }
}


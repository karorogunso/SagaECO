using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_NPC_SS_MODE : Packet
    {
        public SSMG_NPC_SS_MODE()
        {
            this.data = new byte[18];
            this.offset = 2;
            this.ID = 0x0606;
        }

        public ushort Toggle
        {
            set
            {
                this.PutUShort(value, 4);
            }
        }

        public ushort UI
        {
            set
            {
                this.PutUShort(value, 8);
            }
        }
        public ushort X
        {
            set
            {
                this.PutUShort(value, 12);
            }
        }
        public ushort Y
        {
            set
            {
                this.PutUShort(value, 16);
            }
        }
        public byte unknown
        {
            set
            {
                this.PutByte(value, 18);
            }
        }
    }
}


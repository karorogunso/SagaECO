using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_CHAT_EXEMOTION_UNLOCK : Packet
    {
        public SSMG_CHAT_EXEMOTION_UNLOCK()
        {
            this.data = new byte[67];
            this.offset = 2;
            this.ID = 0x1CE8;
            this.PutByte(0x10, 2);
        }

        public uint List1
        {
            set
            {
                this.PutUInt(value, 3);
            }
        }

        public uint List2
        {
            set
            {
                this.PutUInt(value, 7);
            }
        }
        public uint List3
        {
            set
            {
                this.PutUInt(value, 11);
            }
        }
        public uint List4
        {
            set
            {
                this.PutUInt(value, 15);
            }
        }
        public uint List5
        {
            set
            {
                this.PutUInt(value, 19);
            }
        }
    }
}


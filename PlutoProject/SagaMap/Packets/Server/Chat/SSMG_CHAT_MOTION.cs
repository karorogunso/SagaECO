using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_CHAT_MOTION : Packet
    {
        public SSMG_CHAT_MOTION()
        {
            this.data = new byte[14];
            this.offset = 2;
            this.ID = 0x121C;   
        }

        public uint ActorID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }

        public MotionType Motion
        {
            set
            {
                this.PutUShort((ushort)value, 6);
            }
        }

        public byte Loop
        {
            set
            {
                this.PutByte(value, 8);
            }
        }

        public uint motionspeed
        {
            set
            {
                this.PutUInt(value, 10);
            }
        }
    }
}


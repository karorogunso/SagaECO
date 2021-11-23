using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap.Scripting;

namespace SagaMap.Packets.Server
{
    public class SSMG_NPC_MOVE : Packet
    {
        public SSMG_NPC_MOVE()
        {
            this.data = new byte[18];
            this.offset = 2;
            this.ID = 0x05E5;

            this.PutByte(0xFF, 17);//unknown
        }
        public uint NPCID
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
        public byte Type
        {
            set
            {
                this.PutByte(value, 12);
            }
        }

        public ushort Motion
        {
            set
            {
                this.PutUShort(value, 13);
            }
        }

        public ushort MotionSpeed
        {
            set
            {
                this.PutUShort(value, 15);
            }
        }
    }
}


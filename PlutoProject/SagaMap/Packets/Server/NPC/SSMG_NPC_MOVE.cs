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
            this.data = new byte[20];
            this.offset = 2;
            this.ID = 0x05E9;

            //PutShort(0x12, 11);//unknown
            //PutShort(0x0A, 15);//unknown
            PutByte(0xFF, 18);//unknown
            PutByte(0xFF, 19);//unknown
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

        public ushort ShowType
        {
            set
            {
                PutUShort(value, 11);
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

        public byte Type
        {
            set
            {
                this.PutByte(value, 17);
            }
        }

    }
}


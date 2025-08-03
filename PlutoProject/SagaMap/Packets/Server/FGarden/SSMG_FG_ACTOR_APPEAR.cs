using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.FGarden;


namespace SagaMap.Packets.Server
{
    public class SSMG_FG_ACTOR_APPEAR : Packet
    {
        public SSMG_FG_ACTOR_APPEAR(byte type)
        {
            this.data = new byte[45];
            this.offset = 2;
            if (type == 1)
                this.ID = 0x1BEF;
            else if (type == 3)
                this.ID = 0x2058;
            else
                this.ID = 0x1C03;
            this.PutByte(0, 14);
            this.PutByte(1, 6);//count always 1 in COF!!!
            this.PutByte(1, 11);//always!!!
            this.PutByte(1, 16);//always!!!
            this.PutByte(1, 21);
            this.PutByte(0, 22);//unknown
            this.PutByte(1, 23);
            this.PutByte(1, 26);
            this.PutByte(1, 29);
            this.PutByte(1, 32);
            this.PutByte(1, 35);
            this.PutByte(1, 38);
            this.PutByte(1, 41);
            this.PutByte(1, 44);
        }

        public uint MapID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }
        public uint ActorID
        {
            set
            {
                this.PutUInt(value, 7);
            }
        }

        public uint ItemID
        {
            set
            {
                this.PutUInt(value, 12);
            }
        }

        public uint PictID
        {
            set
            {
                this.PutUInt(value, 17);
            }
        }

        public short X
        {
            set
            {
                this.PutShort(value, 24);
            }
        }

        public short Y
        {
            set
            {
                this.PutShort(value, 27);
            }
        }

        public short Z
        {
            set
            {
                this.PutShort(value, 30);
            }
        }

        public short Xaxis
        {
            set
            {
                this.PutShort(value, 33);
            }
        }
        public short Yaxis
        {
            set
            {
                this.PutShort(value, 36);
            }
        }
        public short Zaxis
        {
            set
            {
                this.PutShort(value, 39);
            }
        }

        public ushort Motion
        {
            set
            {
                this.PutUShort(value, 42);
            }
        }

        public string Name
        {
            set
            {
                byte[] name = Global.Unicode.GetBytes(value + "\0");
                byte[] buf = new byte[58 + name.Length];
                this.data.CopyTo(buf, 0);
                this.data = buf;
                this.PutByte((byte)name.Length, 45);
                this.PutBytes(name, 46);
            }
        }
    }
}


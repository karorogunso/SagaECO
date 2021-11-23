using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.FGarden;


namespace SagaMap.Packets.Server
{
    public class SSMG_FG_ACTOR_APPEAR_OLD : Packet
    {
        public SSMG_FG_ACTOR_APPEAR_OLD(byte type)
        {
            this.data = new byte[29];
            this.offset = 2;
            if (type == 1)
                this.ID = 0x1BEF;
            else if (type == 3)
                this.ID = 0x2058;
            else
                this.ID = 0x1C03;
            this.PutByte(0, 14);
        }

        public uint ActorID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }

        public uint ItemID
        {
            set
            {
                this.PutUInt(value, 6);
            }
        }

        public uint PictID
        {
            set
            {
                this.PutUInt(value, 10);
            }
        }

        public short X
        {
            set
            {
                this.PutShort(value, 15);
            }
        }

        public short Y
        {
            set
            {
                this.PutShort(value, 17);
            }
        }

        public short Z
        {
            set
            {
                this.PutShort(value, 19);
            }
        }

        public short Xaxis
        {
            set
            {
                this.PutShort(value, 21);
            }
        }
        public short Yaxis
        {
            set
            {
                this.PutShort(value, 23);
            }
        }
        public short Zaxis
        {
            set
            {
                this.PutShort(value, 25);
            }
        }

        public ushort Motion
        {
            set
            {
                this.PutUShort(value, 27);
            }
        }

        public string Name
        {
            set
            {
                byte[] name = Global.Unicode.GetBytes(value + "\0");
                byte[] buf = new byte[30 + name.Length];
                this.data.CopyTo(buf, 0);
                this.data = buf;
                this.PutByte((byte)name.Length, 29);
                this.PutBytes(name, 30);
            }
        }
    }
}


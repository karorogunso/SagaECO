using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.FGarden;


namespace SagaMap.Packets.Server
{
    public class SSMG_FF_ACTOR_APPEAR : Packet
    {
        public SSMG_FF_ACTOR_APPEAR(byte type)
        {
            this.data = new byte[38];
            this.offset = 2;
            if (type == 1)
                this.ID = 0x1BEF;
            else if (type == 3)
                this.ID = 0x2058;
            else
                this.ID = 0x1C03;
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

        public uint UnknownID
        {
            set
            {
                this.PutUInt(value, 14);
            }
        }
        public short X
        {
            set
            {
                this.PutShort(value, 18);
            }
        }
        public short Y
        {
            set
            {
                this.PutShort(value, 20);
            }
        }
        public short Z
        {
            set
            {
                this.PutShort(value, 22);
            }
        }
        public short Xaxis
        {
            set
            {
                this.PutShort(value, 24);
            }
        }
        public short Yaxis
        {
            set
            {
                this.PutShort(value, 26);
            }
        }
        public short Zaxis
        {
            set
            {
                this.PutShort(value, 28);
            }
        }


        public short Motion
        {
            set
            {
                this.PutShort(value, 31);
            }
        }
        public string Name
        {
            set
            {
                byte[] name = Global.Unicode.GetBytes(value + "\0");
                byte[] buf = new byte[34 + name.Length + 5];
                this.data.CopyTo(buf, 0);
                this.data = buf;
                this.PutByte((byte)name.Length, 33);
                this.PutBytes(name, 34);
            }
        }
    }
}


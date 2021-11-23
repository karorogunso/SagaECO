using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_GOLEM_ACTOR_APPEAR : Packet
    {
        public SSMG_GOLEM_ACTOR_APPEAR()
        {
            this.data = new byte[30];
            this.offset = 2;
            this.ID = 0x17D4;
        }

        public uint PictID
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

        public ushort Speed
        {
            set
            {
                this.PutUShort(value, 12);
            }
        }

        public byte Dir
        {
            set
            {
                this.PutByte(value, 14);
            }
        }

        public uint GolemID
        {
            set
            {
                this.PutUInt(value, 15);
            }
        }

        public GolemType GolemType
        {
            set
            {
                this.PutByte((byte)value, 19);
            }
        }

        public string CharName
        {
            set
            {
                byte[] name = Global.Unicode.GetBytes(value + "\0");
                byte[] buf = new byte[30 + name.Length];
                this.data.CopyTo(buf, 0);
                this.data = buf;

                this.PutByte((byte)name.Length, 20);
                this.PutBytes(name, 21);
            }
        }

        public string Title
        {
            set
            {
                byte[] title = Global.Unicode.GetBytes(value + "\0");
                byte len = GetByte(20);
                byte[] buf = new byte[30 + len + title.Length];
                this.data.CopyTo(buf, 0);
                this.data = buf;

                this.PutByte((byte)title.Length, (ushort)(21 + len));
                this.PutBytes(title, (ushort)(22 + len));
            }
        }

        public uint Unknown
        {
            set
            {
                byte len = GetByte(20);
                len += GetByte((ushort)(21 + len));
                this.PutUInt(value, (ushort)(22 + len));
                this.PutUInt(value, (ushort)(26 + len));
            }
        }
    }
}


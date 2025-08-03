using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Ring;


namespace SagaMap.Packets.Server
{
    public class SSMG_RING_INVITE : Packet
    {
        public SSMG_RING_INVITE()
        {
            this.data = new byte[8];
            this.offset = 2;
            this.ID = 0x1AAF;
        }

        public uint CharID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }

        public string CharName
        {
            set
            {
                byte[] buf = Global.Unicode.GetBytes(value + "\0");
                byte[] buff = new byte[8 + buf.Length];
                this.data.CopyTo(buff, 0);
                this.data = buff;

                this.PutByte((byte)buf.Length, 6);
                this.PutBytes(buf, 7);
            }
        }

        public string RingName
        {
            set
            {
                byte offset = this.GetByte(6);
                byte[] buf = Global.Unicode.GetBytes(value + "\0");
                byte[] buff = new byte[8 + offset + buf.Length];
                this.data.CopyTo(buff, 0);
                this.data = buff;

                this.PutByte((byte)buf.Length, (ushort)(7 + offset));
                this.PutBytes(buf, (ushort)(8 + offset));
            }
        }
    }
}


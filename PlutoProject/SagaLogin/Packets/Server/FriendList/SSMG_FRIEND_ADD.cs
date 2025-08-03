using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;

using SagaLib;

namespace SagaLogin.Packets.Server
{
    public class SSMG_FRIEND_ADD : Packet
    {
        public SSMG_FRIEND_ADD()
        {
            this.data = new byte[7];
            this.ID = 0x00D3;
        }

        public uint CharID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }

        public string Name
        {
            set
            {
                byte[] buf = Global.Unicode.GetBytes(value + "\0");
                byte[] buff = new byte[7 + buf.Length];
                this.data.CopyTo(buff, 0);
                this.data = buff;
                this.PutByte((byte)buf.Length, 6);
                this.PutBytes(buf, 7);
            }
        }
    }
}


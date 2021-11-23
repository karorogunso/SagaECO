using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Party;


namespace SagaMap.Packets.Server
{
    public class SSMG_PARTY_DELETE : Packet
    {
        public enum Result
        {
            DISMISSED = 1,
            QUIT = 2,
            KICKED = 3,
        }

        public SSMG_PARTY_DELETE()
        {
            this.data = new byte[11];
            this.offset = 2;
            this.ID = 0x19DD;
        }

        public uint PartyID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }

        public string PartyName
        {
            set
            {
                byte[] buf = Global.Unicode.GetBytes(value + "\0");
                byte[] buff = new byte[11 + buf.Length];
                this.data.CopyTo(buff, 0);
                this.data = buff;
                this.PutByte((byte)buf.Length, 6);
                this.PutBytes(buf, 7);
            }
        }

        public Result Reason
        {
            set
            {
                byte size = GetByte(6);
                this.PutInt((int)value, (ushort)(7 + size));
            }
        }
    }
}


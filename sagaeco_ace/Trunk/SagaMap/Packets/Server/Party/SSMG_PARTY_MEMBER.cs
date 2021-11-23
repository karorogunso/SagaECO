using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Party;


namespace SagaMap.Packets.Server
{
    public class SSMG_PARTY_MEMBER : Packet
    {

        public SSMG_PARTY_MEMBER()
        {
            this.data = new byte[12];
            this.offset = 2;
            this.ID = 0x19E1;
        }

        public int PartyIndex
        {
            set
            {
                this.PutInt(value, 2);
            }
        }

        public uint CharID
        {
            set
            {
                this.PutUInt(value, 6);
            }
        }

        public string CharName
        {
            set
            {
                byte[] buf = Global.Unicode.GetBytes(value + "\0");
                byte[] buff = new byte[12 + buf.Length];
                this.data.CopyTo(buff, 0);
                this.data = buff;
                this.PutByte((byte)buf.Length, 10);
                this.PutBytes(buf, 11);
            }
        }

        public bool Leader
        {
            set
            {
                byte size = this.GetByte(10);
                if (value)
                    this.PutByte(1, (ushort)(11 + size));
                else
                    this.PutByte(0, (ushort)(11 + size));
            }
        }
    }
}


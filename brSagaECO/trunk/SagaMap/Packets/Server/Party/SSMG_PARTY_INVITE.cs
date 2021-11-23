using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Party;


namespace SagaMap.Packets.Server
{
    public class SSMG_PARTY_INVITE : Packet
    {
        public SSMG_PARTY_INVITE()
        {
            this.data = new byte[9];
            this.offset = 2;
            this.ID = 0x19CA;
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
                byte[] buff = new byte[9 + buf.Length];
                this.data.CopyTo(buff, 0);
                this.data = buff;
                byte size = (byte)buf.Length;
                this.PutByte(size, 6);
                this.PutBytes(buf, 7);

                //unknown byte
                this.PutByte(1, (ushort)(7 + size));
            }
        }
    }
}


using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Quests;

namespace SagaMap.Packets.Server
{
    public class SSMG_TRADE_REQUEST : Packet
    {        
        public SSMG_TRADE_REQUEST()
        {
            this.data = new byte[2];
            this.offset = 2;
            this.ID = 0x0A0C;
        }

        public string Name
        {
            set
            {
                value = value + "\0";
                byte[] buf = Global.Unicode.GetBytes(value);
                byte[] buff = new byte[3 + buf.Length];
                this.data.CopyTo(buff, 0);
                this.data = buff;
                this.PutByte((byte)buf.Length, 2);
                this.PutBytes(buf, 3);
            }
        }
    }
}


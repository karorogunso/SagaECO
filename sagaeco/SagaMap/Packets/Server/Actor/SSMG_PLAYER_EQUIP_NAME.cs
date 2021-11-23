using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_PLAYER_EQUIP_NAME : Packet
    {
        public SSMG_PLAYER_EQUIP_NAME()
        {
            this.data = new byte[4];
            this.ID = 0x0264;
        }

        public string ActorName
        {
            set
            {
                byte[] buf, buff;

                buf = Global.Unicode.GetBytes(value + "\0");
                size = (byte)buf.Length;
                buff = new byte[this.data.Length - 1 + size];
                this.data.CopyTo(buff, 0);
                this.data = buff;

                this.PutByte((byte)size, 2);
                this.PutBytes(buf, 3);
            }
        }
    }
}
        

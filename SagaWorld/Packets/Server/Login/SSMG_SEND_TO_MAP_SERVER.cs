using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaWorld.Packets.Server
{
    public class SSMG_SEND_TO_MAP_SERVER : Packet
    {
       
        public SSMG_SEND_TO_MAP_SERVER()
        {
            this.data = new byte[6];
            this.offset = 2;
            this.ID = 0x33;
        }

        public byte ServerID
        {
            set
            {
                this.PutByte(value, 2);
            }
        }

        public string IP
        {
            set
            {
                byte[] buf = Global.Unicode.GetBytes(value);
                byte size = (byte)(buf.Length + 1);
                byte[] buff = new byte[size + 8];
                this.data.CopyTo(buff, 0);
                this.data = buff;
                this.PutByte(size, 3);
                this.PutBytes(buf, 4);
            }
        }

        private byte GetDataOffset()
        {
            byte size = this.GetByte(3);
            return (byte)(4 + size);
        }

        public int Port
        {
            set
            {
                this.PutInt(value, GetDataOffset());
            }
        }
    }
}


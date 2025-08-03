using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap.Manager;

namespace SagaMap.Packets.Server
{
    public class SSMG_COMMUNITY_RECRUIT_REQUEST : Packet
    {
       
        public SSMG_COMMUNITY_RECRUIT_REQUEST()
        {
            this.data = new byte[7];
            this.offset = 2;
            this.ID = 0x1BAD;
        }

        public uint CharID
        {
            set
            {
                PutUInt(value, 2);
            }
        }

        public string CharName
        {
            set
            {
                byte[] name = Global.Unicode.GetBytes(value + "\0");
                byte[] buf = new byte[7 + name.Length];
                this.data.CopyTo(buf, 0);
                this.data = buf;
                this.PutByte((byte)name.Length, 6);
                this.PutBytes(name, 7);
            }
        }
    }
}


using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;

namespace SagaMap.Packets.Server
{
    public class SSMG_ABYSSTEAM_REGIST_APPROVAL : Packet
    {
        public SSMG_ABYSSTEAM_REGIST_APPROVAL()
        {
            this.data = new byte[12];
            this.offset = 2;
            this.ID = 0x22EE;
        }
        public uint CharID
        {
            set
            {
                this.PutUInt(value, 3);
            }
        }
        public string Name
        {
            set
            {
                byte[] name = Encoding.UTF8.GetBytes(value + "\0");
                byte[] buf = new byte[this.data.Length + name.Length + 1];
                this.data.CopyTo(buf, 0);
                this.data = buf;
                this.offset = 7;
                this.PutByte((byte)name.Length);
                this.PutBytes(name);
            }
        }
        public byte Level
        {
            set
            {
                this.offset++;
                this.PutByte(value);
            }
        }
        public PC_JOB Job
        {
            set
            {
                this.offset++;
                this.PutShort((short)value);
            }
        }
    }
}
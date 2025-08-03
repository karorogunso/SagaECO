using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Login
{
    public class INTERN_LOGIN_REGISTER : Packet
    {
        public INTERN_LOGIN_REGISTER()
        {
            this.data = new byte[8];
            this.offset = 2;
            this.ID = 0xFFF0;
            
        }

        public string Password
        {
            set
            {
                byte[] buf = Global.Unicode.GetBytes(value);
                this.PutByte((byte)buf.Length, 2);
                byte[] buff = new byte[this.data.Length + buf.Length];
                this.data.CopyTo(buff, 0);
                this.data = buff;
                this.PutBytes(buf, 3);
            } 
        }

        public List<uint> HostedMaps
        {
            set
            {
                ushort index;
                index = (ushort)(3 + GetByte(2));
                byte[] buf = Global.Unicode.GetBytes(Configuration.Instance.Host);
                this.PutByte((byte)buf.Length, index);
                byte[] buff = new byte[this.data.Length + buf.Length];
                this.data.CopyTo(buff, 0);
                this.data = buff;
                this.PutBytes(buf, (ushort)(index + 1));
                index = (ushort)(index + 1 + buf.Length);
                this.PutInt(Configuration.Instance.Port, index);

                buff = new byte[this.data.Length + value.Count * 4 + 1];
                this.data.CopyTo(buff, 0);
                this.data = buff;

                this.PutByte((byte)value.Count, (ushort)(index + 4));
                for (int i = 0; i < value.Count; i++)
                {
                    this.PutUInt(value[i], (ushort)(index + 5 + i * 4));
                }
            }
        }
    }
}


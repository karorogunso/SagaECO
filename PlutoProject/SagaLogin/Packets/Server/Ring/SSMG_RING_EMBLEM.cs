using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;

using SagaLib;

namespace SagaLogin.Packets.Server
{
    public class SSMG_RING_EMBLEM : Packet
    {
        public SSMG_RING_EMBLEM()
        {
            this.data = new byte[16];
            this.ID = 0x010A;
        }

        /// <summary>
        /// 0 is not up to date, 1 is latest
        /// </summary>
        public int Result
        {
            set
            {
                PutInt(value, 2);
            }
        }

        public uint RingID
        {
            set
            {
                PutUInt(value, 6);
            }
        }

        /// <summary>
        /// 0 is exists, 1 is doesn't exists
        /// </summary>
        public byte Result2
        {
            set
            {
                PutByte(value, 10);
            }
        }

        public byte[] Data
        {
            set
            {
                byte[] buf = new byte[20 + value.Length];
                this.data.CopyTo(buf, 0);
                this.data = buf;
                PutByte(0xFD, 11);
                PutInt(value.Length, 12);
                PutBytes(value, 16);
            }
        }

        public DateTime UpdateTime
        {
            set
            {
                uint date = (uint)(value - new DateTime(1970, 1, 1)).TotalSeconds;
                if (GetByte(11) == 0xFD)
                {
                    int len = GetInt(12);
                    PutUInt(date, (ushort)(16 + len));
                }
                else
                    PutUInt(date, 12);
            }
        }
    }
}


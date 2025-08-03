using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Ring;


namespace SagaMap.Packets.Server
{
    public class SSMG_RING_INFO : Packet
    {
        public enum Reason
        {
            CREATE,
            JOIN,
            NONE,
            UPDATED,
        }
        
        public SSMG_RING_INFO()
        {
            this.data = new byte[28];
            this.offset = 2;
            this.ID = 0x1ACC;
        }

        public void Ring(Ring ring, Reason reason)
        {
            this.PutUInt(ring.ID, 2);
            byte[] buf = Global.Unicode.GetBytes(ring.Name + "\0");
            byte[] buff = new byte[28 + buf.Length];
            byte size = (byte)buf.Length;
            this.data.CopyTo(buff, 0);
            this.data = buff;
            this.PutByte(size, 6);
            this.PutBytes(buf, 7);
            this.PutUInt(0);
            this.PutByte(2);
            this.PutInt(0x0D);
            //this.PutUInt(0);
            this.PutUInt(ring.Fame);
            this.PutInt(ring.MemberCount);
            this.PutInt(ring.MaxMemberCount);
        }
    }
}


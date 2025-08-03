using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Ring;


namespace SagaMap.Packets.Server
{
    public class SSMG_RING_MEMBER_INFO : Packet
    {
         public SSMG_RING_MEMBER_INFO()
        {
            this.data = new byte[17];
            this.offset = 2;
            this.ID = 0x1ACE;
        }

         public void Member(ActorPC value, Ring ring)
         {
             int index;
             if (ring != null)
                 index = ring.IndexOf(value);
             else
                 index = -1;
             this.PutInt(index, 2);
             this.PutUInt(value.CharID);
             byte[] buf = Global.Unicode.GetBytes(value.Name + "\0");
             byte[] buff = new byte[17 + buf.Length];
             this.data.CopyTo(buff, 0);
             this.data = buff;

             this.PutByte((byte)buf.Length);
             this.PutBytes(buf);
             this.PutByte((byte)value.Race);
             this.PutByte((byte)value.Gender);
             if (index != -1)
                 this.PutInt(ring.Rights[index].Value);

         }
    }
}


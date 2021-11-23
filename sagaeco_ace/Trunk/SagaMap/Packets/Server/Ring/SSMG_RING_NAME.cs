using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Ring;


namespace SagaMap.Packets.Server
{
    public class SSMG_RING_NAME : Packet
    {        
        public SSMG_RING_NAME()
        {
            this.data = new byte[12];
            this.offset = 2;
            this.ID = 0x1AD1;
        }

        public ActorPC Player
        {
            set
            {
                this.PutUInt(value.ActorID, 2);
                if (value.Ring != null)
                    this.PutUInt(value.Ring.ID);
                else
                    this.PutUInt(0);

                byte[] buf;
                if (value.Ring != null)
                    buf = Global.Unicode.GetBytes(value.Ring.Name + "\0");
                else
                    buf = new byte[1];
                byte[] buff = new byte[12 + buf.Length]; 
                this.data.CopyTo(buff, 0);
                this.data = buff;
                this.PutByte((byte)buf.Length);
                this.PutBytes(buf);
                if (value.Ring != null)
                    if (value == value.Ring.Leader)
                        this.PutByte(1);
            }
        }
    }
}


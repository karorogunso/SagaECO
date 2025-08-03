using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Map;


namespace SagaMap.Packets.Server
{
    public class SSMG_ACTOR_EVENT_TITLE_CHANGE : Packet
    {
        public SSMG_ACTOR_EVENT_TITLE_CHANGE()
        {
            this.data = new byte[7];
            this.offset = 2;
            this.ID = 0x0BBA;
          
        }

        public ActorEvent Actor
        {
            set
            {
                byte[] title = Global.Unicode.GetBytes(value.Title + "\0");
                byte[] buf = new byte[7 + title.Length];
                this.data.CopyTo(buf, 0);
                this.data = buf;
                this.PutUInt(value.ActorID, 2);                
                this.PutByte((byte)title.Length);
                this.PutBytes(title);
            }
        }
    }
}


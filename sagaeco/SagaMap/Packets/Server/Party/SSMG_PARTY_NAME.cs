using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Party;


namespace SagaMap.Packets.Server
{
    public class SSMG_PARTY_NAME : Packet
    {
        
        public SSMG_PARTY_NAME()
        {
            this.data = new byte[8];
            this.offset = 2;
            this.ID = 0x19D9;
        }

        public void Party(Party party, Actor pc)
        {
            this.PutUInt(pc.ActorID, 2);
            byte[] buf;
            if (party != null)
                buf = Global.Unicode.GetBytes(party.Name + "\0");
            else
                buf = new byte[1];
            byte[] buff = new byte[8 + buf.Length];
            byte size = (byte)buf.Length;
            this.data.CopyTo(buff, 0);
            this.data = buff;
            this.PutByte(size, 6);
            this.PutBytes(buf, 7);
        }
    }
}


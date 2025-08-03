using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Party;


namespace SagaMap.Packets.Server
{
    public class SSMG_PARTY_INFO : Packet
    {
        
        public SSMG_PARTY_INFO()
        {
            this.data = new byte[13];
            this.offset = 2;
            this.ID = 0x19DC;
        }

        public void Party(Party party, ActorPC pc)
        {
            this.PutUInt(party.ID, 2);
            byte[] buf = Global.Unicode.GetBytes(party.Name + "\0");
            byte[] buff = new byte[13 + buf.Length];
            byte size = (byte)buf.Length;
            this.data.CopyTo(buff, 0);
            this.data = buff;
            this.PutByte(size, 6);
            this.PutBytes(buf, 7);
            if (party.Leader == pc)
                this.PutByte(1, (ushort)(7 + size));
            else
                this.PutByte(0, (ushort)(7 + size));
            this.PutByte((byte)party.IndexOf(pc), (ushort)(8 + size));
            this.PutInt(party.MemberCount, (ushort)(9 + size));
        }
    }
}


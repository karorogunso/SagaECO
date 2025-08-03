using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Party;


namespace SagaMap.Packets.Server
{
    public class SSMG_PARTY_MEMBER_STATE : Packet
    {

        public SSMG_PARTY_MEMBER_STATE()
        {
            this.data = new byte[14];
            this.offset = 2;
            this.ID = 0x19E6;
        }

        public uint PartyIndex
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }

        public uint CharID
        {
            set
            {
                this.PutUInt(value, 6);
            }
        }

        public bool Online
        {
            set
            {
                if (value)
                    this.PutUInt(1, 10);
                else
                    this.PutUInt(0, 10);
            }
        }
    }
}


using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Party;


namespace SagaMap.Packets.Server
{
    public class SSMG_PARTY_MEMBER_POSITION : Packet
    {

        public SSMG_PARTY_MEMBER_POSITION()
        {
            this.data = new byte[22];
            this.offset = 2;
            this.ID = 0x19F0;
        }

        public byte PartyIndex
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

        public uint MapID
        {
            set
            {
                this.PutUInt(value, 10);
            }
        }

        public byte X
        {
            set
            {
                this.PutUInt(value, 14);
            }
        }

        public byte Y
        {
            set
            {
                this.PutUInt(value, 18);
            }
        }
    }
}


using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Party;


namespace SagaMap.Packets.Server
{
    public class SSMG_PARTY_MEMBER_HPMPSP : Packet
    {

        public SSMG_PARTY_MEMBER_HPMPSP()
        {
            this.data = new byte[34];
            this.offset = 2;
            this.ID = 0x19EB;
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

        public uint HP
        {
            set
            {
                this.PutUInt(value, 10);
            }
        }

        public uint MaxHP
        {
            set
            {
                this.PutUInt(value, 14);
            }
        }

        public uint MP
        {
            set
            {
                this.PutUInt(value, 18);
            }
        }

        public uint MaxMP
        {
            set
            {
                this.PutUInt(value, 22);
            }
        }

        public uint SP
        {
            set
            {
                this.PutUInt(value, 26);
            }
        }

        public uint MaxSP
        {
            set
            {
                this.PutUInt(value, 30);
            }
        }

    }
}


using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Party;


namespace SagaMap.Packets.Server
{
    public class SSMG_PARTY_MEMBER_DUNGEON_POSITION : Packet
    {

        public SSMG_PARTY_MEMBER_DUNGEON_POSITION()
        {
            this.data = new byte[13];
            this.offset = 2;
            this.ID = 0x1C84;
        }

       public uint CharID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }

        public uint MapID
        {
            set
            {
                this.PutUInt(value, 6);
            }
        }

        public byte X
        {
            set
            {
                this.PutByte(value, 10);
            }
        }

        public byte Y
        {
            set
            {
                this.PutByte(value, 11);
            }
        }

        public byte Dir
        {
            set
            {
                this.PutByte(value, 12);
            }
        }
    }
}


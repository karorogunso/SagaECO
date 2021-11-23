using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_PLAYER_MAX_HPMPSP : Packet
    {
        public SSMG_PLAYER_MAX_HPMPSP()
        {
            this.data = new byte[35];
            this.offset = 2;
            this.ID = 0x0221;
        }

        public uint ActorID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }

        public uint MaxHP
        {
            set
            {
                this.PutByte(3, 6);
                this.PutUInt(0, 7);
                this.PutUInt(value, 11);
            }
        }

        public uint MaxMP
        {
            set
            {
                this.PutUInt(0, 15);
                this.PutUInt(value, 19);
            }
        }

        public uint MaxSP
        {
            set
            {
                this.PutUInt(0, 23);
                this.PutUInt(value, 27);
            }
        }

        public uint MaxEP
        {
            set
            {
                this.PutUInt(value, 31);
            }
        }
    }
}
        

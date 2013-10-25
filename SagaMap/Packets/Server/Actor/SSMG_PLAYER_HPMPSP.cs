using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_PLAYER_HPMPSP : Packet
    {
        public SSMG_PLAYER_HPMPSP()
        {
            this.data = new byte[19];
            this.offset = 2;
            this.ID = 0x021C;
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
                this.PutUInt(value, 7);
            }
        }

        public uint MaxMP
        {
            set
            {
                this.PutUInt(value, 11);
            }
        }

        public uint MaxSP
        {
            set
            {
                this.PutUInt(value, 15);
            }
        }        
    }
}
        

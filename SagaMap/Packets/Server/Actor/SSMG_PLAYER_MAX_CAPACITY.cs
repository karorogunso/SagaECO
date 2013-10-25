using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_PLAYER_MAX_CAPACITY : Packet
    {
        public SSMG_PLAYER_MAX_CAPACITY()
        {
            this.data = new byte[36];
            this.offset = 2;
            this.ID = 0x0231;
        }

        public uint CapacityBody
        {
            set
            {
                this.PutByte(4, 2);
                this.PutUInt(value, 3);
            }
        }

        public uint CapacityRight
        {
            set
            {
                this.PutUInt(value, 7);
            }
        }

        public uint CapacityLeft
        {
            set
            {
                this.PutUInt(value, 11);
            }
        }

        public uint CapacityBack
        {
            set
            {
                this.PutUInt(value, 15);
            }
        }

        public uint PayloadBody
        {
            set
            {
                this.PutByte(4, 19);
                this.PutUInt(value, 20);
            }
        }

        public uint PayloadRight
        {
            set
            {
                this.PutUInt(value, 24);
            }
        }

        public uint PayloadLeft
        {
            set
            {
                this.PutUInt(value, 28);
            }
        }

        public uint PayloadBack
        {
            set
            {
                this.PutUInt(value, 32);
            }
        }
    }
}
        

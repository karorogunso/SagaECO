using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_PARTNER_HPMPSP_STATUS : Packet
    {
        public SSMG_PARTNER_HPMPSP_STATUS()
        {
            this.data = new byte[32];
            this.offset = 2;
            this.ID = 0x218E;
        }
        public uint ActorID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }
        /// <summary>
        /// usually 3
        /// </summary>
        public byte NPLength
        {
            set
            {
                this.PutByte(value, 6);
            }
        }
        public uint HP
        {
            set
            {
                this.PutUInt(value, 7);
            }
        }
        public uint MP
        {
            set
            {
                this.PutUInt(value, 11);
            }
        }
        public uint SP
        {
            set
            {
                this.PutUInt(value, 15);
            }
        }
        public byte NPLength2
        {
            set
            {
                this.PutByte(value, 19);
            }
        }
        public uint MAXHP
        {
            set
            {
                this.PutUInt(value, 20);
            }
        }
        public uint MAXMP
        {
            set
            {
                this.PutUInt(value, 24);
            }
        }
        public uint MAXSP
        {
            set
            {
                this.PutUInt(value, 28);
            }
        }

        
    }
}
        

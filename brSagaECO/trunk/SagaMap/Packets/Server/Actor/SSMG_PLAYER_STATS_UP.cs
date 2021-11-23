using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_PLAYER_STATS_UP : Packet
    {
        public SSMG_PLAYER_STATS_UP()
        {
            this.data = new byte[22];
            this.offset = 2;
            this.ID = 0x0209;
            this.PutByte(0x01, 2);
            this.PutByte(0x08, 3);
        }

        public ushort Str
        {
            set
            {
                this.PutUShort(value, 4);
            }
        }

        public ushort Dex
        {
            set
            {
                this.PutUShort(value, 6);
            }
        }

        public ushort Int
        {
            set
            {
                this.PutUShort(value, 8);
            }
        }

        public ushort Vit
        {
            set
            {
                this.PutUShort(value, 10);
            }
        }

        public ushort Agi
        {
            set
            {
                this.PutUShort(value, 12);
            }
        }

        public ushort Mag
        {
            set
            {
                this.PutUShort(value, 14);
            }
        }

        public ushort Luk
        {
            set
            {
                this.PutUShort(value, 16);
            }
        }

        public ushort Cha
        {
            set
            {
                this.PutUShort(value, 18);
            }
        }

        public ushort PointRemaining
        {
            set
            {
                this.PutUShort(value, 20);
            }
        }
    }
}
        

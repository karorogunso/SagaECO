using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Npc;

namespace SagaMap.Packets.Server
{
    public class SSMG_NPC_SHOP_BUY : Packet
    {
        int num;
        public SSMG_NPC_SHOP_BUY(int num)
        {
            this.data = new byte[16 + num * 4];
            this.offset = 2;
            this.ID = 0x05FD;
            this.num = num;
        }

        public uint Rate
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }

        public List<uint> Goods
        {
            set
            {
                    this.PutByte((byte)value.Count, 6);
                for (int i = 0; i < value.Count; i++)
                {
                    this.PutUInt(value[i], (ushort)(7 + i * 4));
                }
            }
        }

        public uint Gold
        {
            set
            {
                this.PutUInt(value, (ushort)(7 + num * 4));
            }
        }

        public uint Bank
        {
            set
            {
                this.PutUInt(value, (ushort)(11 + num * 4));
            }
        }

        public ShopType Type
        {
            set
            {
                this.PutByte((byte)value, (ushort)(15 + num * 4));
            }
        }
    }
}


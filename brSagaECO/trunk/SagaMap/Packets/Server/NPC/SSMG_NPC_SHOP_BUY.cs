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
            if (Configuration.Instance.Version >= SagaLib.Version.Saga18)
            {
                this.data = new byte[24 + num * 4];
                this.ID = 0x0601;
            }
            else
            {
                this.data = new byte[16 + num * 4];
                this.ID = 0x05FD;
            }
            this.offset = 2;
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

        public ulong Gold
        {
            set
            {
                if (Configuration.Instance.Version >= SagaLib.Version.Saga18)
                    this.PutULong(value, (ushort)(11 + num * 4));
                else
                    this.PutUInt((uint)value, (ushort)(7 + num * 4));
            }
        }

        public ulong Bank
        {
            set
            {
                if (Configuration.Instance.Version >= SagaLib.Version.Saga18)
                    this.PutULong(value, (ushort)(15 + num * 4));
                else
                    this.PutUInt((uint)value, (ushort)(11 + num * 4));
            }
        }

        public ShopType Type
        {
            set
            {
                if (Configuration.Instance.Version >= SagaLib.Version.Saga18)
                    this.PutByte((byte)value, (ushort)(23 + num * 4));
                else
                    this.PutByte((byte)value, (ushort)(15 + num * 4));
            }
        }
    }
}


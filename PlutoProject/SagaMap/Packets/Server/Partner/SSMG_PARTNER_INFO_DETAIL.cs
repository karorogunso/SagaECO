using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_PARTNER_INFO_DETAIL: Packet
    {
        public SSMG_PARTNER_INFO_DETAIL()
        {
            this.data = new byte[58];
            this.offset = 2;
            this.ID = 0x217B;
            this.PutByte(3, 6);
            this.PutByte(0x13, 19);
        }

        public uint InventorySlot
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }

        public byte unknown0 //has sth to do with part of data length
        {
            set
            {
                this.PutByte(value, 6);
            }
        }
        public uint MaxHP
        {
            set
            {
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

        public ushort MoveSpeed
        {
            set
            {
                this.PutUShort(value, 20);
            }
        }

        public ushort MinPhyATK
        {
            set
            {
                this.PutUShort(value, 22);
                this.PutUShort(value, 24);//没显示
                this.PutUShort(value, 26);//没显示
            }
        }

        public ushort MaxPhyATK
        {
            set
            {
                this.PutUShort(value, 28);
                this.PutUShort(value, 30);//没显示
                this.PutUShort(value, 32);//没显示
            }
        }

        public ushort MinMAGATK
        {
            set
            {
                this.PutUShort(value, 34);
            }
        }

        public ushort MaxMAGATK
        {
            set
            {
                this.PutUShort(value, 36);
            }
        }
        public ushort DEF
        {
            set
            {
                this.PutUShort(value, 38);
            }
        }
        public short DEFAdd
        {
            set
            {
                this.PutShort(value, 40);
            }
        }
        public ushort MDEF
        {
            set
            {
                this.PutUShort(value, 42);
            }
        }
        public short MDEFAdd
        {
            set
            {
                this.PutShort(value, 44);
            }
        }
        public ushort ShortHit
        {
            set
            {
                this.PutUShort(value, 46);
            }
        }
        public ushort LongHit
        {
            set
            {
                this.PutUShort(value, 48);
            }
        }
        public ushort ShortAvoid
        {
            set
            {
                this.PutUShort(value, 50);
            }
        }
        public ushort LongAvoid
        {
            set
            {
                this.PutUShort(value, 52);
            }
        }
        public short ASPD
        {
            set
            {
                this.PutShort(value, 54);
            }
        }
        public short CSPD
        {
            set
            {
                this.PutShort(value, 56);
            }
        }
    }
}
        

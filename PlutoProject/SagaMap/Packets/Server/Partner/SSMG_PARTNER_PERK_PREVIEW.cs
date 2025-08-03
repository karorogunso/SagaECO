using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_PARTNER_PERK_PREVIEW: Packet
    {
        private byte perklistlen = 6;
        public SSMG_PARTNER_PERK_PREVIEW()
        {
            this.data = new byte[67];
            this.offset = 2;
            this.ID = 0x217D;
            this.PutByte(3, 6);
            this.PutByte(0x13, 28);
            this.PutByte(perklistlen, 21);
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

        public ushort Perkpoints
        {
            set
            {
                this.PutUShort(value, 19);
            }
        }

        public byte Perk0
        {
            set
            {
                this.PutByte(value, 22);
            }
        }
        public byte Perk1
        {
            set
            {
                this.PutByte(value, 23);
            }
        }
        public byte Perk2
        {
            set
            {
                this.PutByte(value, 24);
            }
        }
        public byte Perk3
        {
            set
            {
                this.PutByte(value, 25);
            }
        }
        public byte Perk4
        {
            set
            {
                this.PutByte(value, 26);
            }
        }
        public byte Perk5
        {
            set
            {
                this.PutByte(value, 27);
            }
        }

        public ushort MoveSpeed
        {
            set
            {
                this.PutUShort(value, 29);
            }
        }

        public ushort MinPhyATK
        {
            set
            {
                this.PutUShort(value, 31);
                this.PutUShort(value, 33);//没显示
                this.PutUShort(value, 35);//没显示
            }
        }

        public ushort MaxPhyATK
        {
            set
            {
                this.PutUShort(value, 37);
                this.PutUShort(value, 39);//没显示
                this.PutUShort(value, 41);//没显示
            }
        }

        public ushort MinMAGATK
        {
            set
            {
                this.PutUShort(value, 43);
            }
        }

        public ushort MaxMAGATK
        {
            set
            {
                this.PutUShort(value, 45);
            }
        }
        public ushort DEF
        {
            set
            {
                this.PutUShort(value, 47);
            }
        }
        public ushort DEFAdd
        {
            set
            {
                this.PutUShort(value, 49);
            }
        }
        public ushort MDEF
        {
            set
            {
                this.PutUShort(value, 51);
            }
        }
        public ushort MDEFAdd
        {
            set
            {
                this.PutUShort(value, 53);
            }
        }
        public ushort ShortHit
        {
            set
            {
                this.PutUShort(value, 55);
            }
        }
        public ushort LongHit
        {
            set
            {
                this.PutUShort(value, 57);
            }
        }
        public ushort ShortAvoid
        {
            set
            {
                this.PutUShort(value, 59);
            }
        }
        public ushort LongAvoid
        {
            set
            {
                this.PutUShort(value, 61);
            }
        }
        public short ASPD
        {
            set
            {
                this.PutShort(value, 63);
            }
        }
        public short CSPD
        {
            set
            {
                this.PutShort(value, 65);
            }
        }
    }
}
        

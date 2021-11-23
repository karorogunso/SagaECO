using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_PARTNER_PERK_CONFIRM: Packet
    {
        private byte perklistlen = 6;
        public SSMG_PARTNER_PERK_CONFIRM()
        {
            this.data = new byte[68];
            this.offset = 2;
            this.ID = 0x217F;
            this.PutByte(3, 9);
            this.PutByte(0x13, 29);
            this.PutByte(perklistlen, 22);
        }
        public byte unknown0
        {
            set
            {
                this.PutByte(0, 2);
            }
        }

        public uint InventorySlot
        {
            set
            {
                this.PutUInt(value, 3);
            }
        }

        public ushort Perkpoints
        {
            set
            {
                this.PutUShort(value, 7);
            }
        }

        public byte unknown1 //has sth to do with part of data length
        {
            set
            {
                this.PutByte(3, 9);
            }
        }

        public uint MaxHP
        {
            set
            {
                this.PutUInt(value, 10);
            }
        }

        public uint MaxMP
        {
            set
            {
                this.PutUInt(value, 14);
            }
        }

        public uint MaxSP
        {
            set
            {
                this.PutUInt(value, 18);
            }
        }

        public byte Perk0
        {
            set
            {
                this.PutByte(value, 23);
            }
        }
        public byte Perk1
        {
            set
            {
                this.PutByte(value, 24);
            }
        }
        public byte Perk2
        {
            set
            {
                this.PutByte(value, 25);
            }
        }
        public byte Perk3
        {
            set
            {
                this.PutByte(value, 26);
            }
        }
        public byte Perk4
        {
            set
            {
                this.PutByte(value, 27);
            }
        }
        public byte Perk5
        {
            set
            {
                this.PutByte(value, 28);
            }
        }

        public ushort MoveSpeed
        {
            set
            {
                this.PutUShort(value, 30);
            }
        }

        public ushort MinPhyATK
        {
            set
            {
                this.PutUShort(value, 32);
                this.PutUShort(value, 34);//没显示
                this.PutUShort(value, 36);//没显示
            }
        }

        public ushort MaxPhyATK
        {
            set
            {
                this.PutUShort(value, 38);
                this.PutUShort(value, 40);//没显示
                this.PutUShort(value, 42);//没显示
            }
        }

        public ushort MinMAGATK
        {
            set
            {
                this.PutUShort(value, 44);
            }
        }

        public ushort MaxMAGATK
        {
            set
            {
                this.PutUShort(value, 46);
            }
        }
        public ushort DEF
        {
            set
            {
                this.PutUShort(value, 48);
            }
        }
        public ushort DEFAdd
        {
            set
            {
                this.PutUShort(value, 50);
            }
        }
        public ushort MDEF
        {
            set
            {
                this.PutUShort(value, 52);
            }
        }
        public ushort MDEFAdd
        {
            set
            {
                this.PutUShort(value, 54);
            }
        }
        public ushort ShortHit
        {
            set
            {
                this.PutUShort(value, 56);
            }
        }
        public ushort LongHit
        {
            set
            {
                this.PutUShort(value, 58);
            }
        }
        public ushort ShortAvoid
        {
            set
            {
                this.PutUShort(value, 60);
            }
        }
        public ushort LongAvoid
        {
            set
            {
                this.PutUShort(value, 62);
            }
        }
        public short ASPD
        {
            set
            {
                this.PutShort(value, 64);
            }
        }
        public short CSPD
        {
            set
            {
                this.PutShort(value, 66);
            }
        }
    }
}
        

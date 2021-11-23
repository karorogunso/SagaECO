using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_ACTOR_UNIONPET_STATE : Packet
    {
        public SSMG_ACTOR_UNIONPET_STATE()
        {
            this.data = new byte[58];
            this.offset = 2;
            this.ID = 0x217B;
            this.PutByte(3, 6);
            this.PutByte(0x13, 19);
        }

        public uint unknownID
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
        public ushort DEFAdd
        {
            set
            {
                this.PutUShort(value, 40);
            }
        }
        public ushort MDEF
        {
            set
            {
                this.PutUShort(value, 42);
            }
        }
        public ushort MDEFAdd
        {
            set
            {
                this.PutUShort(value, 44);
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


using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_POSSESSION_CATALOG_ITEM_INFO : Packet
    {
        public SSMG_POSSESSION_CATALOG_ITEM_INFO()
        {
            this.data = new byte[56];
            this.offset = 2;
            this.ID = 0x1792;

            this.PutByte(0x03, 12);
            this.PutByte(0x0E, 25);
        }
        public byte Result
        {
            set
            {
                this.PutByte(value,2);
            }
        }
        public uint ActorID
        {
            set
            {
                this.PutUInt(value, 3);
            }
        }
        public uint ItemID
        {
            set
            {
                this.PutUInt(value, 7);
            }
        }
        public byte Level
        {
            set
            {
                this.PutByte(value, 11);
            }
        }
        public int HP
        {
            set
            {
                this.PutInt(value, 13);
            }
        }
        public int MP
        {
            set
            {
                this.PutInt(value, 17);
            }
        }
        public int SP
        {
            set
            {
                this.PutInt(value, 21);
            }
        }
        public short MinATK1
        {
            set
            {
                this.PutShort(value, 26);
            }
        }
        public short MaxATK1
        {
            set
            {
                this.PutShort(value, 28);
            }
        }
        public short MinATK2
        {
            set
            {
                this.PutShort(value, 30);
            }
        }
        public short MaxATK2
        {
            set
            {
                this.PutShort(value,32);
            }
        }
        public short MinATK3
        {
            set
            {
                this.PutShort(value, 34);
            }
        }
        public short MaxATK3
        {
            set
            {
                this.PutShort(value, 36);
            }
        }
        public short MinMATK
        {
            set
            {
                this.PutShort(value, 38);
            }
        }
        public short MaxMATK
        {
            set
            {
                this.PutShort(value, 40);
            }
        }
        public short SHIT
        {
            set
            {
                this.PutShort(value, 42);
            }
        }
        public short LHIT
        {
            set
            {
                this.PutShort(value, 44);
            }
        }
        public short DEF
        {
            set
            {
                this.PutShort(value, 46);
            }
        }
        public short MDEF
        {
            set
            {
                this.PutShort(value, 48);
            }
        }
        public short SAVOID
        {
            set
            {
                this.PutShort(value, 50);
            }
        }
        public short LAVOID
        {
            set
            {
                this.PutShort(value, 52);
            }
        }
        public byte X
        {
            set
            {
                this.PutByte(value,54);
            }
        }
        public byte Y
        {
            set
            {
                this.PutByte(value,55);
            }
        }
    }
}
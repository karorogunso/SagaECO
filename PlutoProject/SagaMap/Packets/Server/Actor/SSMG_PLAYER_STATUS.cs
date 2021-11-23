using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_PLAYER_STATUS : Packet
    {
        public SSMG_PLAYER_STATUS()
        {
            this.data = new byte[53];
            this.offset = 2;
            this.ID = 0x0212;
        }

        public ushort StrBase
        {
            set
            {
                this.PutByte(8, 2);
                this.PutByte(8, 19);
                this.PutByte(8, 36);
                this.PutUShort(value, 3);
            }
        }

        public ushort DexBase
        {
            set
            {
                this.PutUShort(value, 5);
            }
        }

        public ushort IntBase
        {
            set
            {
                this.PutUShort(value, 7);
            }
        }

        public ushort VitBase
        {
            set
            {
                this.PutUShort(value, 9);
            }
        }

        public ushort AgiBase
        {
            set
            {
                this.PutUShort(value, 11);
            }
        }

        public ushort MagBase
        {
            set
            {
                this.PutUShort(value, 13);
            }
        }

        public ushort LukBase
        {
            set
            {
                this.PutUShort(value, 15);
            }
        }

        public ushort ChaBase
        {
            set
            {
                this.PutUShort(value, 17);
            }
        }

        public short StrRevide
        {
            set
            {
                this.PutShort(value, 20);
            }
        }

        public short DexRevide
        {
            set
            {
                this.PutShort(value, 22);
            }
        }

        public short IntRevide
        {
            set
            {
                this.PutShort(value, 24);
            }
        }

        public short VitRevide
        {
            set
            {
                this.PutShort(value, 26);
            }
        }

        public short AgiRevide
        {
            set
            {
                this.PutShort(value, 28);
            }
        }

        public short MagRevide
        {
            set
            {
                this.PutShort(value, 30);
            }
        }

        public short LukRevide
        {
            set
            {
                this.PutShort(value, 32);
            }
        }

        public short ChaRevide
        {
            set
            {
                this.PutShort(value, 34);
            }
        }

        public ushort StrBonus
        {
            set
            {
                this.PutUShort(value, 37);
            }
        }

        public ushort DexBonus
        {
            set
            {
                this.PutUShort(value, 39);
            }
        }

        public ushort IntBonus
        {
            set
            {
                this.PutUShort(value, 41);
            }
        }

        public ushort VitBonus
        {
            set
            {
                this.PutUShort(value, 43);
            }
        }

        public ushort AgiBonus
        {
            set
            {
                this.PutUShort(value, 45);
            }
        }

        public ushort MagBonus
        {
            set
            {
                this.PutUShort(value, 47);
            }
        }

        public ushort LukBonus
        {
            set
            {
                this.PutUShort(value, 49);
            }
        }

        public ushort ChaBonus
        {
            set
            {
                this.PutUShort(value, 51);
            }
        }
    }
}
        

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

        public ushort StrRevide
        {
            set
            {
                this.PutByte(8, 19);
                this.PutUShort(value, 20);
            }
        }

        public ushort DexRevide
        {
            set
            {
                this.PutUShort(value, 22);
            }
        }

        public ushort IntRevide
        {
            set
            {
                this.PutUShort(value, 24);
            }
        }

        public ushort VitRevide
        {
            set
            {
                this.PutUShort(value, 26);
            }
        }

        public ushort AgiRevide
        {
            set
            {
                this.PutUShort(value, 28);
            }
        }

        public ushort MagRevide
        {
            set
            {
                this.PutUShort(value, 30);
            }
        }

        public ushort LukRevide
        {
            set
            {
                this.PutUShort(value, 32);
            }
        }

        public ushort ChaRevide
        {
            set
            {
                this.PutUShort(value, 34);
            }
        }

        public ushort StrBonus
        {
            set
            {
                this.PutByte(8, 36);
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
        

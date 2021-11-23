using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_PLAYER_TITLE_LIST : Packet
    {
        public SSMG_PLAYER_TITLE_LIST()
        {
            data = new byte[214];//8bytes unknowns
            offset = 2;
            ID = 0x2549;
            PutByte(3, 2);
            PutByte(3, 27);
            PutByte(10, 52);
            PutByte(10, 133);
        }
        public ulong Page1
        {
            set
            {
                PutULong(value, 53);
            }
        }
        public ulong Page2
        {
            set
            {
                PutULong(value, 61);
            }
        }
        public ulong Page3
        {
            set
            {
                PutULong(value, 69);
            }
        }
        public ulong Page4
        {
            set
            {
                PutULong(value, 77);
            }
        }
        public ulong Page5
        {
            set
            {
                PutULong(value, 85);
            }
        }

        public ulong NPage1
        {
            set
            {
                PutULong(value, 134);
            }
        }
        public ulong NPage2
        {
            set
            {
                PutULong(value, 142);
            }
        }
        public ulong NPage3
        {
            set
            {
                PutULong(value, 150);
            }
        }
        public ulong NPage4
        {
            set
            {
                PutULong(value, 158);
            }
        }
        public ulong NPage5
        {
            set
            {
                PutULong(value, 166);
            }
        }
    }
}
        

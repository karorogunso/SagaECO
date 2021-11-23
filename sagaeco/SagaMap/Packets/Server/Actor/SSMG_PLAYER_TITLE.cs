using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_PLAYER_TITLE : Packet
    {
        public SSMG_PLAYER_TITLE()
        {
            this.data = new byte[20];//8bytes unknowns
            this.offset = 2;
            this.ID = 0x2419;
            PutByte(4, 3);
        }

        public uint TSubID
        {
            set
            {
                PutUInt(value, 4);
            }
        }
        public uint TConjID
        {
            set
            {
                PutUInt(value, 8);
            }
        }
        public uint TPredID
        {
            set
            {
                PutUInt(value, 12);
            }
        }
        public uint TBallteID
        {
            set
            {
                PutUInt(value, 16);
            }
        }
    }
}
        

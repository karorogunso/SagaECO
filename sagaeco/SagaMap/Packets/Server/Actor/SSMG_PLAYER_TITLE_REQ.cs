using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_PLAYER_TITLE_REQ : Packet
    {
        public SSMG_PLAYER_TITLE_REQ()
        {
            this.data = new byte[15];//8bytes unknowns
            this.offset = 2;
            this.ID = 0x241C;
        }

        public uint tID
        {
            set
            {
                PutUInt(value, 2);
            }
        }
        public byte mark
        {
            set
            {
                PutByte(value, 6);
            }
        }
        public ulong task
        {
            set
            {
                PutULong(value, 7);
            }
        }
    }
}
        

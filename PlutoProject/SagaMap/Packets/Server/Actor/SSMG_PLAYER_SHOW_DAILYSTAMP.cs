using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_PLAYER_SHOW_DAILYSTAMP : Packet
    {
        public SSMG_PLAYER_SHOW_DAILYSTAMP()
        {
            this.data = new byte[3];
            this.offset = 2;
            this.ID = 0x1F72;
            //this.PutByte(1, 2);
        }

        public byte Type
        {
            set
            {
                this.PutByte(value, 2);
            }
        }

    }
}
        

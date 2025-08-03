using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Quests;

namespace SagaMap.Packets.Server
{
    public class SSMG_QUEST_COUNT_UPDATE : Packet
    {        
        public SSMG_QUEST_COUNT_UPDATE()
        {
            this.data = new byte[15];
            this.offset = 2;
            this.ID = 0x1973;
            this.PutByte(3, 2);
        }

        public int Count1
        {
            set
            {
                this.PutInt(value, 3);
            }
        }

        public int Count2
        {
            set
            {
                this.PutInt(value, 7);
            }
        }

        public int Count3
        {
            set
            {
                this.PutInt(value, 11);
            }
        }
    }
}


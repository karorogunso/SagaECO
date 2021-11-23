using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Quests;

namespace SagaMap.Packets.Server
{
    public class SSMG_QUEST_RESTTIME_UPDATE : Packet
    {        
        public SSMG_QUEST_RESTTIME_UPDATE()
        {
            this.data = new byte[6];
            this.offset = 2;
            this.ID = 0x1978;            
        }

        public int RestTime
        {
            set
            {
                this.PutInt(value, 2);
            }
        }
    }
}


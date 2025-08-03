using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Quests;

namespace SagaMap.Packets.Server
{
    public class SSMG_QUEST_DELETE : Packet
    {        
        public SSMG_QUEST_DELETE()
        {
            this.data = new byte[2];
            this.offset = 2;
            this.ID = 0x198C;            
        }        
    }
}


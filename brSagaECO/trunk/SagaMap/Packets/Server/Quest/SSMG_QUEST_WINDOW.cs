using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Quests;

namespace SagaMap.Packets.Server
{
    public class SSMG_QUEST_WINDOW : Packet
    {        
        public SSMG_QUEST_WINDOW()
        {
            this.data = new byte[2];
            this.offset = 2;
            this.ID = 0x196A;            
        }        
    }
}


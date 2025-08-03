using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Quests;

namespace SagaMap.Packets.Server
{
    public class SSMG_QUEST_STATUS_UPDATE : Packet
    {        
        public SSMG_QUEST_STATUS_UPDATE()
        {
            this.data = new byte[3];
            this.offset = 2;
            this.ID = 0x1987;            
        }

        public SagaDB.Quests.QuestStatus Status
        {
            set
            {
                this.PutByte((byte)value, 2);
            }
        }
    }
}


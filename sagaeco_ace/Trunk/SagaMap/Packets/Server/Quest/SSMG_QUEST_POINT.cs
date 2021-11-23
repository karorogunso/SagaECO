using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Quests;

namespace SagaMap.Packets.Server
{
    public class SSMG_QUEST_POINT : Packet
    {        
        public SSMG_QUEST_POINT()
        {
            this.data = new byte[12];
            this.offset = 2;
            this.ID = 0x196E;            
        }

        public ushort QuestPoint
        {
            set
            {
                this.PutUShort(value, 2);
            }
        }

        public uint ResetTime
        {
            set
            {
                this.PutUInt(value, 4);
            }
        }
    }
}


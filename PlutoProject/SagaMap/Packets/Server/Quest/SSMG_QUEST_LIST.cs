using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Quests;

namespace SagaMap.Packets.Server
{
    public class SSMG_QUEST_LIST : Packet
    {        
        public SSMG_QUEST_LIST()
        {
            this.data = new byte[2];
            this.offset = 2;
            this.ID = 0x1964;            
        }

        public List<QuestInfo> Quests
        {
            set
            {
                //ADWORD QuestID
                this.PutByte((byte)value.Count);
                foreach (QuestInfo i in value)
                {
                    this.PutUInt(i.ID);
                }
                
                //ABYTE QuestType
                this.PutByte((byte)value.Count);
                foreach (var item in value)
                {
                    this.PutByte((byte)item.QuestType);
                }


                //ATSTR QuestName
                this.PutByte((byte)value.Count);
                foreach (var item in value)
                {
                    this.PutTSTR(item.Name);
                }

                //ADWORD QuestTime
                this.PutByte((byte)value.Count);
                foreach (var item in value)
                {
                    this.PutInt(item.TimeLimit);
                }


                //ABYTE QuestLevel
                this.PutByte((byte)value.Count);
                foreach (var item in value)
                {
                    this.PutByte(item.MinLevel);
                }
            }
        }
    }
}


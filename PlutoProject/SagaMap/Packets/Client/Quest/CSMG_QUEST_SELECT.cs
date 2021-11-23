using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_QUEST_SELECT : Packet
    {
        public CSMG_QUEST_SELECT()
        {
            this.offset = 2;
        }

        public uint QuestID
        {
            get
            {
                return this.GetUInt(2);
            }
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_QUEST_SELECT();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnQuestSelect(this);
        }

    }
}
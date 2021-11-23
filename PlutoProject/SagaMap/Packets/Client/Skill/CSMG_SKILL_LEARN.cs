using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_SKILL_LEARN : Packet
    {
        public CSMG_SKILL_LEARN()
        {
            this.offset = 2;
        }

        public ushort SkillID
        {
            get
            {
                return this.GetUShort(2);
            }
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_SKILL_LEARN();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnSkillLearn(this);
        }

    }
}
using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_SKILL_RANGE_ATTACK : Packet
    {
        public CSMG_SKILL_RANGE_ATTACK()
        {
            this.offset = 2;
        }

        public uint ActorID
        {
            get
            {
                return this.GetUInt(2);
            }
        }

        public override Packet New()
        {
            return new CSMG_SKILL_RANGE_ATTACK();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnSkillRangeAttack(this);
        }

    }
}
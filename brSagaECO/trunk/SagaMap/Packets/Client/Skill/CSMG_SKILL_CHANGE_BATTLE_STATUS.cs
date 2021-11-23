using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_SKILL_CHANGE_BATTLE_STATUS : Packet
    {
        public CSMG_SKILL_CHANGE_BATTLE_STATUS()
        {
            this.offset = 2;
        }

        public byte Status
        {
            get
            {
                return this.GetByte(2);
            }
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_SKILL_CHANGE_BATTLE_STATUS();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnSkillChangeBattleStatus(this);
        }

    }
}
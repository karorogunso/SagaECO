using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_SKILL_ATTACK : Packet
    {
        public CSMG_SKILL_ATTACK()
        {
            this.offset = 2;
        }
        public uint acid = 0;
        public uint ActorID
        {
            set
            {
                acid = value;
            }
            get
            {
                if(this.data!=null)
                acid = this.GetUInt(2);
                return acid;
            }
        }

        public short Random
        {
            get
            {
                return this.GetShort(6);
            }
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_SKILL_ATTACK();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnSkillAttack(this,false);
        }

    }
}
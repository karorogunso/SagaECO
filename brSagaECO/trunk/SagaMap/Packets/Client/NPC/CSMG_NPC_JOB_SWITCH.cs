using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_NPC_JOB_SWITCH : Packet
    {
        public CSMG_NPC_JOB_SWITCH()
        {
            this.offset = 2;
        }

        public int Unknown
        {
            get
            {
                return GetInt(2);
            }
        }

        public uint ItemUseCount
        {
            get
            {
                return GetUInt(6);
            }
        }

        public ushort[] Skills
        {
            get
            {
                byte count = GetByte(10);
                ushort[] skills = new ushort[count];
                for (int i = 0; i < count; i++)
                {
                    skills[i] = GetUShort((ushort)(11 + i * 2));
                }
                return skills;
            }
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_NPC_JOB_SWITCH();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnNPCJobSwitch(this);
        }

    }
}
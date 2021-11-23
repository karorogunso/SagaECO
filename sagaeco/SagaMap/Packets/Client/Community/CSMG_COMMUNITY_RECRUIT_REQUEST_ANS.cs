using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_COMMUNITY_RECRUIT_REQUEST_ANS : Packet
    {
        public CSMG_COMMUNITY_RECRUIT_REQUEST_ANS()
        {
            this.offset = 2;
        }

        public bool Accept
        {
            get
            {
                return (GetUInt(2) == 1);
            }
        }

        public uint CharID
        {
            get
            {
                return this.GetUInt(6);
            }
        }

        public string CharName
        {
            get
            {
                return Global.Unicode.GetString(this.GetBytes(this.GetByte(10), 11));
            }
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_COMMUNITY_RECRUIT_REQUEST_ANS();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnRecruitRequestAns(this);
        }

    }
}
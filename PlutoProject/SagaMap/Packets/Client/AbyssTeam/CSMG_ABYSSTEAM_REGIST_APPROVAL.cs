using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_ABYSSTEAM_REGIST_APPROVAL : Packet
    {
        public CSMG_ABYSSTEAM_REGIST_APPROVAL()
        {
            this.offset = 2;
        }

        public override Packet New()
        {
            return new CSMG_ABYSSTEAM_REGIST_APPROVAL();
        }
        public byte Result
        {
            get
            {
                return this.GetByte(2);
            }
        }
        public uint CharID
        {
            get
            {
                return this.GetUInt(3);
            }
        }
        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnAbyssTeamRegistApproval(this);
        }

    }
}
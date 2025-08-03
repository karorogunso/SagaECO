using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_ABYSSTEAM_REGIST_REQUEST : Packet
    {
        public CSMG_ABYSSTEAM_REGIST_REQUEST()
        {
            this.offset = 2;
        }

        public override Packet New()
        {
            return new CSMG_ABYSSTEAM_REGIST_REQUEST();
        }
        public uint LeaderID
        {
            get
            {
                return this.GetUInt(3);
            }
        }
        public string Password
        {
            get
            {
                byte Length = this.GetByte(7);
                return Encoding.UTF8.GetString(this.GetBytes(Length, 8)).Replace("/0", "");
            }
        }
        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnAbyssTeamRegistRequest(this);
        }

    }
}
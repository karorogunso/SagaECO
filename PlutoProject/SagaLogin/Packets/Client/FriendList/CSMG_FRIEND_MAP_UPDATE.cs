using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaLogin;
using SagaLogin.Network.Client;

namespace SagaLogin.Packets.Client
{
    public class CSMG_FRIEND_MAP_UPDATE : Packet
    {
        public CSMG_FRIEND_MAP_UPDATE()
        {
            this.offset = 2;
        }

        public uint MapID
        {
            get
            {
                return this.GetUInt(2);
            }
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaLogin.Packets.Client.CSMG_FRIEND_MAP_UPDATE();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((LoginClient)(client)).OnFriendMapUpdate(this);
        }

    }
}
using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaLogin;
using SagaLogin.Network.Client;

namespace SagaLogin.Packets.Client
{
    public class CSMG_FRIEND_DELETE : Packet
    {
        public CSMG_FRIEND_DELETE()
        {
            this.offset = 2;
        }

        public uint CharID
        {
            get
            {
                return this.GetUInt(2);
            }
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaLogin.Packets.Client.CSMG_FRIEND_DELETE();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((LoginClient)(client)).OnFriendDelete(this);
        }

    }
}
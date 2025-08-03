using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaLogin;
using SagaLogin.Network.Client;

namespace SagaLogin.Packets.Client
{
    public class CSMG_FRIEND_ADD_REPLY : Packet
    {
        public CSMG_FRIEND_ADD_REPLY()
        {
            this.offset = 2;
        }

        public uint Reply
        {
            get
            {
                return this.GetUInt(2);
            }
        }

        public uint CharID
        {
            get
            {
                return this.GetUInt(6);
            }
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaLogin.Packets.Client.CSMG_FRIEND_ADD_REPLY();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((LoginClient)(client)).OnFriendAddReply(this);
        }

    }
}
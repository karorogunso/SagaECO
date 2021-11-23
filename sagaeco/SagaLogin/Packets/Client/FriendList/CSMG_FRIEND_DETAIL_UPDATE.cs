using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaLogin;
using SagaLogin.Network.Client;

namespace SagaLogin.Packets.Client
{
    public class CSMG_FRIEND_DETAIL_UPDATE : Packet
    {
        public CSMG_FRIEND_DETAIL_UPDATE()
        {
            this.offset = 2;
        }

        public PC_JOB Job
        {
            get
            {
                return  (PC_JOB)this.GetUShort(2);
            }
        }

        public byte Level
        {
            get
            {
                return (byte)this.GetUShort(4);
            }
        }

        public byte JobLevel
        {
            get
            {
                return (byte)this.GetUShort(6);
            }
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaLogin.Packets.Client.CSMG_FRIEND_DETAIL_UPDATE();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((LoginClient)(client)).OnFriendDetailUpdate(this);
        }

    }
}
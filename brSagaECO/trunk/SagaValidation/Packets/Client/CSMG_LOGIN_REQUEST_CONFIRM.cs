using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaValidation;
using SagaValidation.Network.Client;


using SagaDB.Actor;

namespace SagaValidation.Packets.Client
{
    public class CSMG_LOGIN_REQUEST_CONFIRM : Packet
    {
        public CSMG_LOGIN_REQUEST_CONFIRM()
        {
            this.offset = 2;
        }
        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaValidation.Packets.Client.CSMG_LOGIN_REQUEST_CONFIRM();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((ValidationClient)(client)).OnConfirmLogin(this);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaLogin;
using SagaLogin.Network.Client;

using SagaDB.Actor;

namespace SagaLogin.Packets.Client
{
    public class CSMG_REQUEST_MAP_SERVER : Packet
    {
        public CSMG_REQUEST_MAP_SERVER()
        {
            this.offset = 2;
        }

        public uint Slot
        {
            get
            {
                return this.GetUInt(2);
            }
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaLogin.Packets.Client.CSMG_REQUEST_MAP_SERVER();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((LoginClient)(client)).OnRequestMapServer(this);
        }

    }
}
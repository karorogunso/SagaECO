using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_LOGOUT : Packet
    {
        public enum Results
        {
            OK,
            CANCEL = 0xf9,
            FAILED = 0xff,
        }

        public CSMG_LOGOUT()
        {
            this.offset = 8;
        }

        public Results Result
        {
            get
            {
                return (Results)this.GetByte(2);
            }
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_LOGOUT();
        }
        
        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnLogout(this);
        }

    }
}
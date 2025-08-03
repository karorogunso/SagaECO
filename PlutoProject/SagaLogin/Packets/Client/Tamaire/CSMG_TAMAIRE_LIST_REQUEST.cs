using System;
using System.Collections.Generic;
using System.Text;
using SagaLogin.Network.Client;
using SagaLib;

namespace SagaLogin.Packets.Client
{
    public class CSMG_TAMAIRE_LIST_REQUEST : Packet
    {
        public CSMG_TAMAIRE_LIST_REQUEST()
        {
            this.offset = 2;
        }


        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaLogin.Packets.Client.CSMG_TAMAIRE_LIST_REQUEST();
        }

        public byte page
        {
            get
            {
                return GetByte(3);
            }
        }

        public byte minlevel
        {
            get
            {
                return GetByte(5);
            }
        }

        public byte maxlevel
        {
            get
            {
                return GetByte(6);
            }
        }

        public byte JobType
        {
            get
            {
                return GetByte(7);
            }
        }

        public override void Parse(SagaLib.Client client)
        {
            ((LoginClient)(client)).OnTamaireListRequest(this);
        }

    }
}
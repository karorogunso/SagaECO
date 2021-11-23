using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.FGarden;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_CHAR_FORM : Packet
    {
        public CSMG_CHAR_FORM()
        {
            this.offset = 2;
        }

        public byte tailstyle
        {
            get
            {
                return this.GetByte(2);
            }
        }
        public byte wingstyle
        {
            get
            {
                return this.GetByte(3);
            }
        }

        public byte wingcolor
        {
            get
            {
                return this.GetByte(4);
            }
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_CHAR_FORM();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnCharFormChange(this);

        }

    }
}
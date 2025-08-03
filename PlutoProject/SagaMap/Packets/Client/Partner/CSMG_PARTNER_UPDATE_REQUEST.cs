using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.FGarden;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_PARTNER_UPDATE_REQUEST : Packet
    {
        public CSMG_PARTNER_UPDATE_REQUEST()
        {
            this.offset = 2;
        }

        public uint PartnerItemID
        {
            get
            {
                return this.GetUInt(2);
            }
            set
            {
                this.PutUInt(value, 2);
            }
        }

        public ushort unknown0
        {
            get
            {
                return this.GetUShort(6);
            }
            set
            {
                this.PutUShort(value, 6);
            }
        }
        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_PARTNER_UPDATE_REQUEST();
        }

        public override void Parse(SagaLib.Client client)
        {
            //((MapClient)(client)).netIO.Disconnect();
        }

    }
}
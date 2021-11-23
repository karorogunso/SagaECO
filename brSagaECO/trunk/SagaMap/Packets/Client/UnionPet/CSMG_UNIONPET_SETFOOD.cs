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
    public class CSMG_UNIONPET_SETFOOD : Packet
    {
        public CSMG_UNIONPET_SETFOOD()
        {
            this.offset = 2;
        }

        public uint ItemID
        {
            get
            {
                return this.GetUInt(3);
            }
        }
        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_UNIONPET_SETFOOD();
        }

        public override void Parse(SagaLib.Client client)
        {
            //((MapClient)(client)).netIO.Disconnect();
        }

    }
}
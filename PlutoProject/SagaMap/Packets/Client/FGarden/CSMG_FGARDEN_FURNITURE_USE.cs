using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.FGarden;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_FGARDEN_FURNITURE_USE : Packet
    {
        public CSMG_FGARDEN_FURNITURE_USE()
        {
            this.offset = 2;
        }

        public uint ActorID
        {
            get
            {
                return this.GetUInt(2);
            }
        }

       public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_FGARDEN_FURNITURE_USE();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnFGardenFurnitureUse(this);
        }

    }
}
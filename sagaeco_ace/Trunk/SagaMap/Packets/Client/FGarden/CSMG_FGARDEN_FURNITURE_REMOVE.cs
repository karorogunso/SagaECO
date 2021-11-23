using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.FGarden;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_FGARDEN_FURNITURE_REMOVE : Packet
    {
        public CSMG_FGARDEN_FURNITURE_REMOVE()
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
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_FGARDEN_FURNITURE_REMOVE();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnFGardenFurnitureRemove(this);
        }

    }
}
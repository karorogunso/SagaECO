using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.FGarden;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_FF_FURNITURE_REMOVE_CASTLE : Packet
    {
        public CSMG_FF_FURNITURE_REMOVE_CASTLE()
        {
            this.offset = 2;
        }

        public uint ItemID
        {
            get
            {
                return this.GetUInt(2);
            }
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_FF_FURNITURE_REMOVE_CASTLE();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnFFFurnitureRemoveCastle(this);
        }

    }
}
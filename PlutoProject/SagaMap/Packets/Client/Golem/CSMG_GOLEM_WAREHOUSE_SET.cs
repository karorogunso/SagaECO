using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.FGarden;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_GOLEM_WAREHOUSE_SET : Packet
    {
        public CSMG_GOLEM_WAREHOUSE_SET()
        {
            this.offset = 2;
        }

        public string Title
        {
            get
            {
                return Global.Unicode.GetString(this.GetBytes(this.GetByte(2), 3)).Replace("\0", "");
            }
        }

       public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_GOLEM_WAREHOUSE_SET();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnGolemWarehouseSet(this);
        }

    }
}
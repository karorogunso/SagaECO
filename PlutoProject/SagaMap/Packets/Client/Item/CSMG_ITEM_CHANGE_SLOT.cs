using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_ITEM_CHANGE_SLOT : Packet
    {
        public CSMG_ITEM_CHANGE_SLOT()
        {
            this.offset = 2;
        }


        public override Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_ITEM_CHANGE_SLOT();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnItemChangeSlot(this);
        }
    }
}

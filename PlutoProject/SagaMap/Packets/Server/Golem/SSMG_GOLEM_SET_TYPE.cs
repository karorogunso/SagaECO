using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_GOLEM_SET_TYPE : Packet
    {
        public SSMG_GOLEM_SET_TYPE()
        {
            this.data = new byte[4];
            this.offset = 2;
            this.ID = 0x17DF;
        }

        public GolemType GolemType
        {
            set
            {
                this.PutUShort((ushort)value, 2);
            }
        }
    }
}


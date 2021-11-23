using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;

namespace SagaMap.Packets.Server
{
    public class SSMG_BOND_INVITE_MASTER_RESULT : Packet
    {
        public SSMG_BOND_INVITE_MASTER_RESULT()
        {
            this.data = new byte[6];
            this.offset = 2;
            this.ID = 0x1FE1;
        }
        public int Result
        {
            set
            {
                this.PutInt(value, 2);
            }
        }
    }
}
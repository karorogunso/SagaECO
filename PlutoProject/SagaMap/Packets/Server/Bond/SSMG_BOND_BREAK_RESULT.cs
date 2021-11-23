using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;

namespace SagaMap.Packets.Server
{
    public class SSMG_BOND_BREAK_RESULT : Packet
    {
        public SSMG_BOND_BREAK_RESULT()
        {
            this.data = new byte[6];
            this.offset = 2;
            this.ID = 0x1FE9;
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
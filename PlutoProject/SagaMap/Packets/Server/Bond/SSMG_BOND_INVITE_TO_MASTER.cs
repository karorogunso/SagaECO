using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;

namespace SagaMap.Packets.Server
{
    public class SSMG_BOND_INVITE_TO_MASTER : Packet
    {
        public SSMG_BOND_INVITE_TO_MASTER()
        {
            this.data = new byte[6];
            this.offset = 2;
            this.ID = 0x1FE6;
        }
        public uint PupilinID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }
    }
}
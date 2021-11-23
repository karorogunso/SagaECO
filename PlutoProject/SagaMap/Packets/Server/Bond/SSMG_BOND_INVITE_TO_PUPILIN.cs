using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;

namespace SagaMap.Packets.Server
{
    public class SSMG_BOND_INVITE_TO_PUPILIN : Packet
    {
        public SSMG_BOND_INVITE_TO_PUPILIN()
        {
            this.data = new byte[6];
            this.offset = 2;
            this.ID = 0x1FE2;
        }
        public uint MasterID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }
    }
}
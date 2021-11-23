using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;

namespace SagaMap.Packets.Server
{
    public class SSMG_BOND_INFO_INDEX : Packet
    {
        public SSMG_BOND_INFO_INDEX()
        {
            this.data = new byte[7];
            this.offset = 2;
            this.ID = 0x1FEA;
        }
        public uint TargetCharID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }
        public byte Index
        {
            set
            {
                this.PutByte(value, 6);
            }
        }
    }
}
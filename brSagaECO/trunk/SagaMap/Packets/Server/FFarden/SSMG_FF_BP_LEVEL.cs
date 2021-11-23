using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.FGarden;
using SagaMap.Manager;

namespace SagaMap.Packets.Server
{
    public class SSMG_FF_BP_LEVEL : Packet
    {
        //BP系教派等级
        public SSMG_FF_BP_LEVEL()
        {
            this.data = new byte[10];
            this.offset = 2;
            this.ID = 0x2021;
        }
        public uint level
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }
        public uint value
        {
            set
            {
                this.PutUInt(value, 6);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.FGarden;

using SagaMap.Scripting;

namespace SagaMap.Scripting
{
    public enum FG_Sky
    {
        Default,
        Evening,
        Night,
        Cosmos,
    }
}

namespace SagaMap.Packets.Server
{
    public class SSMG_FG_CHANGE_SKY : Packet
    {
        public SSMG_FG_CHANGE_SKY()
        {
            this.data = new byte[3];
            this.offset = 2;
            this.ID = 0x13BD;
        }

        public byte Sky
        {
            set
            {
                this.PutByte((byte)value, 2);
            }
        }
    }
}


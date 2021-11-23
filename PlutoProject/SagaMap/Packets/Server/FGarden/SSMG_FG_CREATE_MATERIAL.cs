using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Party;


namespace SagaMap.Packets.Server
{
    public class SSMG_FG_CREATE_MATERIAL : Packet
    {
        public SSMG_FG_CREATE_MATERIAL()
        {
            this.data = new byte[6];
            this.offset = 2;
            this.ID = 0x1C34;
        }

        public BitMask<SagaMap.Scripting.FGardenParts> Parts
        {
            set
            {
                this.PutInt(value.Value, 2);
            }
        }
    }
}


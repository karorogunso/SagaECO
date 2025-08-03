using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Iris;
using SagaMap.Manager;

namespace SagaMap.Packets.Server
{
    public class SSMG_IRIS_GACHA_UI_OPEN : Packet
    {
        public SSMG_IRIS_GACHA_UI_OPEN()
        {
            this.data = new byte[16];
            this.offset = 2;
            this.ID = 0x1DD8;
        }

    }
}


using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.FGarden;

using SagaMap.Scripting;

namespace SagaMap.Scripting
{
    public enum FG_Weather
    {
        None,
        Rain,
        Snow
    }
}

namespace SagaMap.Packets.Server
{
    public class SSMG_FG_CHANGE_WEATHER : Packet
    {
        public SSMG_FG_CHANGE_WEATHER()
        {
            this.data = new byte[3];
            this.offset = 2;
            this.ID = 0x13BC;
        }

        public byte Weather
        {
            set
            {
                this.PutByte((byte)value, 2);
            }
        }
    }
}


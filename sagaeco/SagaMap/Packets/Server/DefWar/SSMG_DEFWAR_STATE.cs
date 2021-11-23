using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;


namespace SagaMap.Packets.Server
{
    public class SSMG_DEFWAR_STATE : Packet
    {

        public SSMG_DEFWAR_STATE()
        {
            this.data = new byte[7];
            this.offset = 2;
            this.ID = 0x1BCA;
        }



        public uint MapID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }

        /// <summary>
        /// 百分比
        /// </summary>
        public byte Rate
        {
            set
            {
                this.PutUInt(value, 6);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaLib;


namespace SagaMap.Packets.Server.AnotherBook
{
    public class SSMG_ANOTHERBOOK_OPEN_RESULT : Packet
    {
        public SSMG_ANOTHERBOOK_OPEN_RESULT()
        {
            this.data = new byte[436];
            this.ID = 0x23A5;
            this.offset = 2;
        }
    }
}

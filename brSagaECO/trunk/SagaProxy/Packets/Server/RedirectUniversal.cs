using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SagaLib;

namespace SagaProxy.Packets.Server
{
    public class RedirectUniversal : Packet
    {
        public RedirectUniversal()
        {
            this.size = 8;
            this.offset = 8;
        }

    }
}

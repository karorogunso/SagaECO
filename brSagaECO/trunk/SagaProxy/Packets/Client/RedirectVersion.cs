using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SagaLib;

namespace SagaProxy.Packets.Client
{
    public class RedirectVersion : Packet
    {
        public RedirectVersion()
        {
            this.data = new byte[10];
            this.offset = 2;
            this.ID = 0x0001;
        }

        public long Version
        {
            set
            {
                this.PutLong(value, 2);
            }
        }
    }
}

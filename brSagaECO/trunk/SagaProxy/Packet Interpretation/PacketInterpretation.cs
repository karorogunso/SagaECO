using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SagaProxy
{
    public class PacketInterpretation
    {
        int opCode;
        string name;

        public override string ToString()
        {
            return this.name;
        }

        public int OpCode { get { return this.opCode; } set { this.opCode = value; } }
        public string Name { get { return this.name; } set { this.name = value; } }
    }
}

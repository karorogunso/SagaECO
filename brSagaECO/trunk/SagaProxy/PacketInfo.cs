using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SagaProxy
{
    public class PacketInfo
    {
        string origin, server;
        int index, opCode,  length;
        string  name, data;

        public string Origin
        {
            get { return origin; }
        }
        public string Server
        {
            get { return server; }
        }
        public int Index
        {
            get { return index; }
        }
        public int OpCode
        {
            get { return opCode; }
        }
        public int Length
        {
            get { return length; }
        }
        public string Name
        {
            get
            {
                switch (origin)
                {
                    case "Client":
                        switch (server)
                        {
                            case "Map":
                                if (MapClientPacketFactory.Instance.Items.ContainsKey((uint)opCode))
                                    name = MapClientPacketFactory.Instance.Items[(uint)opCode].Name;
                                break;
                            case "Login":
                                if (LoginClientPacketFactory.Instance.Items.ContainsKey((uint)opCode))
                                    name = LoginClientPacketFactory.Instance.Items[(uint)opCode].Name;
                                break;
                            case "Validation":
                                if (ValidationClientPacketFactory.Instance.Items.ContainsKey((uint)opCode))
                                    name = ValidationClientPacketFactory.Instance.Items[(uint)opCode].Name;
                                break;
                        }
                        break;
                    case "Server":
                        switch (server)
                        {
                            case "Map":
                                if (MapServerPacketFactory.Instance.Items.ContainsKey((uint)opCode))
                                    name = MapServerPacketFactory.Instance.Items[(uint)opCode].Name;
                                break;
                            case "Login":
                                if (LoginServerPacketFactory.Instance.Items.ContainsKey((uint)opCode))
                                    name = LoginServerPacketFactory.Instance.Items[(uint)opCode].Name;
                                break;
                            case "Validation":
                                if (ValidationServerPacketFactory.Instance.Items.ContainsKey((uint)opCode))
                                    name = ValidationServerPacketFactory.Instance.Items[(uint)opCode].Name;
                                break;
                        }
                        break;
                }
                return name;
            }
        }
        public string Data
        {
            get { return data; }
        }

        public PacketInfo(string origin, string server, int index, int opCode, int length, string data)
        {
            this.origin = origin;
            this.server = server;
            this.opCode = opCode;
            this.length = length;
            this.data = data;
            this.name = string.Empty;
            this.index = index;
        }

        public override string ToString()
        {
            return $"{Origin},{Server},{Index},0x{OpCode:X4},{Length},{Name},{Data}";
        }
    }
}

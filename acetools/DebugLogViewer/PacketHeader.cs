using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DebugLogViewer
{
    public enum PacketType
    {
        Client,
        Server,
    }

    public class PacketHeader
    {
        public PacketType Type;
        public bool hasInventory;
        public long inventoryOffset;
        public DateTime time;
        public string name;
        public Dictionary<string, string> properties = new Dictionary<string, string>();
        public SagaLib.Packet content;
    }
}

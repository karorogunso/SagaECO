using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
namespace PacketProxy
{
    [Serializable]
    public class PacketContainer : Singleton<PacketContainer>
    {
        public List<Packet> packetsClient = new List<Packet>();
        public List<Packet> packetsClientLogin = new List<Packet>();
        public List<Packet> packetsLogin = new List<Packet>();
        public List<Packet> packetsMap = new List<Packet>();
        public List<Packet> packets = new List<Packet>();
        public List<Packet> packets2 = new List<Packet>();

        public PacketContainer()
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SagaLib;
namespace TomatoProxyTool
{
    [Serializable]
    public class PacketContainer : Singleton<PacketContainer>
    {
        public List<Packet> packetsClient = new List<Packet>();
        public List<Packet> packetsClientLogin = new List<Packet>();
        public List<Packet> packetsLogin = new List<Packet>();
        public List<Packet> packetunsplitedLogin = new List<Packet>();
        public List<Packet> packetsMap = new List<Packet>();
        public List<Packet> packetunsplitedMap = new List<Packet>();
        public List<Packet> packets = new List<Packet>();
        public List<Packet> packets2 = new List<Packet>();
        public List<byte[]> aesKey = new List<byte[]>();

        public PacketContainer()
        {

        }
    }
}

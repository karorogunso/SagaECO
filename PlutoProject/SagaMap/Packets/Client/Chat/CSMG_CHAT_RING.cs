using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_CHAT_RING : Packet
    {
        public CSMG_CHAT_RING()
        {
            this.offset = 2;
        }

        public string Sender
        {
            get
            {
                byte size = this.GetByte(2);
                string buf = Global.Unicode.GetString(this.GetBytes(size, 3));
                return buf.Replace("\0", "");
            }
        }

        public string Content
        {
            get
            {
                byte sender = this.GetByte(2);
                byte size = this.GetByte((ushort)(3 + sender));
                string buf = Global.Unicode.GetString(this.GetBytes(size, (ushort)(4 + sender)));
                return buf.Replace("\0", "");
            }
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_CHAT_RING();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnChatRing(this);
        }

    }
}
using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_CHAT_PUBLIC : Packet
    {
        public CSMG_CHAT_PUBLIC()
        {
            this.offset = 2;
        }

        public string Content
        {
            get
            {
                byte size = this.GetByte(2);
                size--;
                return Global.Unicode.GetString(this.GetBytes(size, 3));
            }
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_CHAT_PUBLIC();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnChat(this);
        }

    }
}
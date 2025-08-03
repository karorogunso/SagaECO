using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_NPC_INPUTBOX : Packet
    {
        public CSMG_NPC_INPUTBOX()
        {
            this.offset = 2;
        }

        public string Content
        {
            get
            {
                return Global.Unicode.GetString(this.GetBytes(this.GetByte(2), 3)).Replace("\0", "");
            }
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_NPC_INPUTBOX();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnNPCInputBox(this);
        }

    }
}
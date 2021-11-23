using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaLogin;
using SagaLogin.Network.Client;

namespace SagaLogin.Packets.Client
{
    public class CSMG_CHAT_WHISPER : Packet
    {
        public CSMG_CHAT_WHISPER()
        {
            this.offset = 2;
        }

        public string Receiver
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
            return (SagaLib.Packet)new SagaLogin.Packets.Client.CSMG_CHAT_WHISPER();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((LoginClient)(client)).OnChatWhisper(this);
        }

    }
}
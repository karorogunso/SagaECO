using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net;
using System.Net.Sockets;

using SagaDB;
using SagaDB.Item;
using SagaDB.Actor;
using SagaLib;
using SagaLogin;
using SagaLogin.Manager;

namespace SagaLogin.Network.Client
{
    public partial class LoginClient : SagaLib.Client
    {
        public void OnChatWhisper(Packets.Client.CSMG_CHAT_WHISPER p)
        {
            if (this.selectedChar == null) return;
            LoginClient client = LoginClientManager.Instance.FindClient(p.Receiver);
            if (client != null)
            {
                Packets.Server.SSMG_CHAT_WHISPER p1 = new SagaLogin.Packets.Server.SSMG_CHAT_WHISPER();
                p1.Sender = this.selectedChar.Name;
                p1.Content = p.Content;
                client.netIO.SendPacket(p1);
            }
            else
            {
                Packets.Server.SSMG_CHAT_WHISPER_FAILED p1 = new SagaLogin.Packets.Server.SSMG_CHAT_WHISPER_FAILED();
                p1.Receiver = p.Receiver;
                p1.Result = 0xFFFFFFFF;
                this.netIO.SendPacket(p1);
            }
        }
    }
}

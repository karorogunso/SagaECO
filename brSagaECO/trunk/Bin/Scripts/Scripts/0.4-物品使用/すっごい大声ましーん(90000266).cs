using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB;
using SagaDB.Actor;
using SagaMap.Network.Client;
using SagaMap.Scripting;
using SagaMap.Manager;
using SagaScript.Chinese.Enums;
namespace SagaMap
{
    public class S90000266 : Event
    {
        public S90000266()
        {
            this.EventID = 90000266;
        }
        public MapClient client;

        public override void OnEvent(ActorPC pc)
        {
            string input_g = InputBox(pc, "ÄÚÈÝ®¹", InputType.PetRename);
            if (input_g != null)
            {
                foreach (MapClient i in MapClientManager.Instance.OnlinePlayer)
                {
                    Packets.Server.SSMG_CHAT_WHOLE p = new SagaMap.Packets.Server.SSMG_CHAT_WHOLE();
                    p.Sender = pc.Name;
                    p.Content = input_g;
                    i.netIO.SendPacket(p);
                }
                TakeItem(pc, 16002600, 1);
            }
        }
    }
}

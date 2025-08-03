using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
namespace SagaScript.M10071000
{
    public class S13000248 : Event
    {
        public S13000248()
        {
            this.EventID = 13000248;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "たこ焼き、売ってますデス$R;" +
            "$Rアナタも買わないデスか？$R;" +
            "$Rきっと美味しいデスよ？$R;", "たこ焼き屋");
            OpenShopBuy(pc, 253);

        }
    }
}
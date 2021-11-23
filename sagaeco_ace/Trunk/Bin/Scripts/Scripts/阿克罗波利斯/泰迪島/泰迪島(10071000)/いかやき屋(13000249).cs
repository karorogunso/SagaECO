using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
namespace SagaScript.M10071000
{
    public class S13000249 : Event
    {
        public S13000249()
        {
            this.EventID = 13000249;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "焼きそばいかがっすか～？$R;" +
            "$Rえ？ イカ焼き？$R;" +
            "いやね、売れ切れちったから$R;" +
            "焼きそば屋にしたんだよ$R;", "いかやき屋");
            OpenShopBuy(pc, 255);

        }
    }
}
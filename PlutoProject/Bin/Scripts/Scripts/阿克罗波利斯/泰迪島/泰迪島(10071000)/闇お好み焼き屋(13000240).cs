using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
namespace SagaScript.M10071000
{
    public class S13000240 : Event
    {
        public S13000240()
        {
            this.EventID = 13000240;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "我が一族に伝わる$R;" +
            "究極のお好み焼きだ$R;" +
            "$Rこれを食すことができるのを$R;" +
            "光栄に思うが良い！$R;" +
            "$P…ということで、$R;" +
            "お好み焼きいかがですか？$R;", "闇お好み焼き屋");
            OpenShopBuy(pc, 251);

        }
    }
}
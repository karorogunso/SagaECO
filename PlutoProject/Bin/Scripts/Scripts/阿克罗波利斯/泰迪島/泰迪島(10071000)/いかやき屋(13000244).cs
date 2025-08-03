using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
namespace SagaScript.M10071000
{
    public class S13000244 : Event
    {
        public S13000244()
        {
            this.EventID = 13000244;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "いかやきが売り切れちゃってねぇ$R;" +
            "$R仕方が無いから、カキ氷やってるよ！$R;" +
            "お一つ、どうだい？$R;", "いかやき屋");
            OpenShopBuy(pc, 257);

        }
    }
}
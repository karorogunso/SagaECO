using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
namespace SagaScript.M10071000
{
    public class S13000245 : Event
    {
        public S13000245()
        {
            this.EventID = 13000245;
        }

        public override void OnEvent(ActorPC pc)
        {

            Say(pc, 131, "ライバルの多いたこ焼きは止めて$R;" +
            "カキ氷を始めたのよ…$R;" +
            "$Rうふふ、$R;" +
            "鉄板で焼いた新感覚のカキ氷…$R;" +
            "賞味してみない…？$R;", "たこ焼き屋");

            OpenShopBuy(pc, 258);

        }
    }
}
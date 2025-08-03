using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
namespace SagaScript.M10071000
{
    public class S13000251 : Event
    {
        public S13000251()
        {
            this.EventID = 13000251;
        }

        public override void OnEvent(ActorPC pc)
        {

            Say(pc, 131, "味も良くて量も多い$R;" +
            "絶対お得なお好み焼きですっ！$R;" +
            "$R隣の小さいやつなんて$R;" +
            "食べてる場合じゃないですよ！$R;", "ジャンボお好み焼き屋");

            OpenShopBuy(pc, 259);

        }
    }
}
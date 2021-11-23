using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
namespace SagaScript.M10071000
{
    public class S13000250 : Event
    {
        public S13000250()
        {
            this.EventID = 13000250;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "いらっしゃい！$R;" +
            "思わず昇天！ 凄味をご賞味あれ！$R;" +
            "$Rデカイだけの大味お好み焼きなんかより、$R;" +
            "質の良い本当の味を提供するよ！$R;", "お好み焼き屋");
            OpenShopBuy(pc, 256);

        }
    }
}
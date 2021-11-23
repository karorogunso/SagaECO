using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M31300000
{
    public class S11001845 : Event
    {
        public S11001845()
        {
            this.EventID = 11001845;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "絶対っ！ピンク！$R;" +
            "あたし、「浮き輪（ピンク）」を$R;" +
            "手に入れるまでこの街から$R;" +
            "一歩も出ないんだからっ！$R;", "エミルの冒険者");

            Say(pc, 11001846, 0, "お、落ち着きなよ。$R;" +
            "$R他のお客さんがこっちを$R;" +
            "見ているじゃないか……。$R;", "タイタニアの冒険者");

            Say(pc, 11001847, 0, "……やれやれ。$R;", "ドミニオンの冒険者");
        }


    }
}



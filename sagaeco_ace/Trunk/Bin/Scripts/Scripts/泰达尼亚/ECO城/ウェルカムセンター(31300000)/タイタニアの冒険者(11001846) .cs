using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M31300000
{
    public class S11001846 : Event
    {
        public S11001846()
        {
            this.EventID = 11001846;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "リーダーに大富豪で連敗しちゃって$R;" +
            "もう、大赤字ですよ……。$R;", "タイタニアの冒険者");

            Say(pc, 11001845, 0, "なに言ってんの？$R;" +
            "あんたが弱すぎるから$R;" +
            "いけないんじゃないっ。$R;", "エミルの冒険者");
        }


    }
}



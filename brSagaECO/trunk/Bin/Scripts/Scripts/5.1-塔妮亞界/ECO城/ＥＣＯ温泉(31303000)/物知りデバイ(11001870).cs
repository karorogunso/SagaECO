using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M31304000
{
    public class S11001870 : Event
    {
        public S11001870()
        {
            this.EventID = 11001870;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "真是碰巧、你也是$R;" +
            "來見這個「西瑞安」的麼？$R;" +
            "$R這個西瑞安、象徵著$R;" +
            "塔尼亞世界、肯定是神話上的生物喔。$R;", "見識廣博的德拜");
            /*
            Say(pc, 0, "いやぁ、あなたも$R;" +
            "この「キリオン」を見に来たのですか？$R;" +
            "$Rこのキリオン、タイタニア世界を$R;" +
            "象徴する、神話上の生物なんですよ。$R;", "物知りデバイ");
            */
        }


    }
}



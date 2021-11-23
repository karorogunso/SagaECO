using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
namespace SagaScript.M30166000
{
    public class S11001978 : Event
    {
        public S11001978()
        {
            this.EventID = 11001978;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11001976, 131, "ようこそ、秘密の隠れ家へ！$R;" +
            "……ってところかな。$R;", "壊し屋バキア");

            Say(pc, 131, "部屋の奥にいる女の子が$R;" +
            "ここの主任研究者の$R;" +
            "ジーニャ博士よ。$R;" +
            "向こうにいるマリオネットは$R;" +
            "ジーニャ博士の執事なの。$R;", "灰のコルチカ");

            Say(pc, 11001976, 131, "まぁ、先ずはジーニャに挨拶でも$R;" +
            "してやってくれよ。$R;" +
            "アイツ、内気で人見知りするくせに$R;" +
            "さびしがり屋でなぁ……。$R;", "壊し屋バキア");

            Say(pc, 131, "バキア、いらないことは$R;" +
            "言わなくていいの！$R;" +
            "$Pあの子、頭はいいんだけど$R;" +
            "まだまだ若いからね。$R;" +
            "いろいろ大変な事もあるみたいよ。$R;" +
            "だから、君も友達になってあげてよ。$R;", "灰のコルチカ");

            Say(pc, 11001976, 131, "あ、そうそう。入り口からも$R;" +
            "外に出ることは出来るけど、$R;" +
            "外に出るときは、部屋の奥から$R;" +
            "出てくれると助かるぜ。$R;", "壊し屋バキア");

        }
    }
}

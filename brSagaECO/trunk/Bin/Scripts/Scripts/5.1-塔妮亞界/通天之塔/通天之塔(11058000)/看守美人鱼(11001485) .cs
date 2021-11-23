using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M11058000
{
    public class S11001485 : Event
    {
        public S11001485()
        {
            this.EventID = 11001485;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "300年前、塔尼亞王$R;" +
            "把這扇門封印……$R;" +
            "因而封印了我們一族所在的海域$R;" +
            "$R 從此之後、塔尼亞族$R;" +
            "就沒有來過島上……。$R;", "看守美人魚");

            Say(pc, 131, "不好意思呢。$R;" +
            "你是沒有惡意的對吧$R;" +
            "抑制不了憎惡塔尼亞的感情。$R;" +
            "$R 請向那邊走。$R;", "看守美人魚");

            // 02/09/2015 by hoshinokanade
            /*
            Say(pc, 131, "300年前、タイタニアの王が$R;" +
            "この扉を封印し……$R;" +
            "我々ごとこの海域を封印したのだ。$R;" +
            "$Rそれ以来、タイタニアが$R;" +
            "この島に来たことはない……。$R;", "見張りのマーメイド");

            Say(pc, 131, "すまぬ。$R;" +
            "お前が悪いわけではないのだろうが$R;" +
            "タイタニアを憎む気持ちが抑えられぬ。$R;" +
            "$Rあっちに行ってくれ。$R;", "見張りのマーメイド");
            */
        }
    }
}





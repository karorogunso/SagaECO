using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30010002
{
    public class S11000629 : Event
    {
        public S11000629()
        {
            this.EventID = 11000629;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "明白了嗎？$R;" +
                "$R我們愛伊斯族，$R;" +
                "是在充滿生命力的大地水晶裡$R;" +
                "誕生的。$R;" +
                "$P發現奇怪的水晶，$R;" +
                "就拿給我們族長看看吧$R;" +
                "$P哎！不過$R;" +
                "這裡的濃湯真好吃呀$R;");
        }
    }
}

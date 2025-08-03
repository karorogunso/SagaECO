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
            Say(pc, 131, "明白了吗？$R;" +
                "$R我们冰精灵族，$R;" +
                "是在充满生命力的大地水晶里$R;" +
                "诞生的。$R;" +
                "$P发现奇怪的水晶，$R;" +
                "就拿给我们族长看看吧$R;" +
                "$P哎！不过$R;" +
                "这里的浓汤真好喝呀$R;");
        }
    }
}

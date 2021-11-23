using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30133000
{
    public class S11000744 : Event
    {
        public S11000744()
        {
            this.EventID = 11000744;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11000744, 190, "那边的总管过得好吗?$R;" +
                "$R呜呼呼!这真是秘密喔$R;" +
                "我们总管好像喜欢盗贼总管！$R;");
            ShowEffect(pc, 11000745, 4501);
            Say(pc, 11000745, 190, "什么？真的吗？$R;" +
                "$R我们总管也好像看上了$R;" +
                "$R盗贼总管呢$R;");
            Say(pc, 11000744, 190, "那样的话，是对手吗？$R;" +
                "$R盗贼总管会对谁动心呢?$R;");
            Say(pc, 11000745, 190, "那样阿！$R;" +
                "听说最近总管和解答大叔很要好啊…$R;");
            Say(pc, 11000744, 190, "哇!$R总管人气真旺啊!$R;");
        }
    }
}
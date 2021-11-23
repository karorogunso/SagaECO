using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30165000
{
    public class S11000387 : Event
    {
        public S11000387()
        {
            this.EventID = 11000387;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "米粒阿里阿先生能到我们国家来一趟$R;" +
                "在那里慢慢聊吧$R;");
            Say(pc, 11000386, 131, "什么？$R;" +
                "米粒阿里阿先生$R;" +
                "得先到我们国家来啊$R;");
            Say(pc, 11000387, 131, "什么？$R;");
            Say(pc, 11000385, 131, "两位请安静点吧！$R;");
        }
    }
}
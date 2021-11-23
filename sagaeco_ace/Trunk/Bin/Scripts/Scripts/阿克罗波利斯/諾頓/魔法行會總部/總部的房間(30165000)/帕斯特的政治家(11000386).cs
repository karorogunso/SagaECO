using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30165000
{
    public class S11000386 : Event
    {
        public S11000386()
        {
            this.EventID = 11000386;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "米粒阿里阿先生$R;" +
                "听我说好吗$R;");
            Say(pc, 11000387, 131, "看，我先来的$R;" +
                "$R先听我的才对$R;");
            Say(pc, 11000386, 131, "什么？$R;");
            Say(pc, 11000385, 131, "别再打了！$R;");
        }
    }
}
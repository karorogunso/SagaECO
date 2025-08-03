using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30076000
{
    public class S11000293 : Event
    {
        public S11000293()
        {
            this.EventID = 11000293;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "从酒馆里来的这个请求书$R;" +
                "到底是什么？$R;" +
                "$R这个是不容许的阿$R;");
            Say(pc, 11000292, 131, "现在不能飞，也没什么可以做$R;" +
                "$R帮帮忙吧，这样也不行吗?$R;");
        }
    }
}
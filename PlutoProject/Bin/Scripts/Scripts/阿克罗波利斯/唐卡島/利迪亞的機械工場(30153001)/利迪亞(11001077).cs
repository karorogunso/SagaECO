using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30153001
{
    public class S11001077 : Event
    {
        public S11001077()
        {
            this.EventID = 11001077;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "哎呀，真是的，为什么不行呢$R;" +
                "$R到底哪里不对呢？$R;" +
                "真是一塌糊涂。$R;" +
                "啊，气死我了。$R;" +
                "$P哇，啊，$R;" +
                "听不见，听不见呀。$R;" +
                "$R生气！！！！！！$R;");
        }
    }
}
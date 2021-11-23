using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30162001
{
    public class S11000256 : Event
    {
        public S11000256()
        {
            this.EventID = 11000256;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "蔷微是诺森王国的国花$R;" +
                "您不觉得很漂亮吗…$R;");
        }
    }
}

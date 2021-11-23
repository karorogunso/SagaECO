using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30160000
{
    public class S11000229 : Event
    {
        public S11000229()
        {
            this.EventID = 11000229;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "这里是女王礼宾室$R;" +
                "注意！对女王陛下不要失礼了$R;");
        }
    }
}
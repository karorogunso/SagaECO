using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30160000
{
    public class S11000230 : Event
    {
        public S11000230()
        {
            this.EventID = 11000230;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "前面是女王礼宾室$R;" +
                "注意！对女王陛下不要失礼了$R;");
        }
    }
}
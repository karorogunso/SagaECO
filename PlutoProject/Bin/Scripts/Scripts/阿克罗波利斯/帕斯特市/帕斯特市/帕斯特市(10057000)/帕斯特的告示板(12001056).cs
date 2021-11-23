using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10057000
{
    public class S12001056 : Event
    {
        public S12001056()
        {
            this.EventID = 12001056;
        }

        public override void OnEvent(ActorPC pc)
        {
            OpenBBS(pc, 7, 0);
            /*
            Say(pc, 131, "現在伺服器忙碌$R;" +
                "稍候再試吧$R;");
             */
        }
    }
}
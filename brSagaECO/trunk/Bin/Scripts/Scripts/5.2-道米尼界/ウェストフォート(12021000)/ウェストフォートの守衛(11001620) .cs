using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M12021000
{
    public class S11001620 : Event
    {
        public S11001620()
        {
            this.EventID = 11001620;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "終于來了呢。$R;" +
            "這裏是西部要塞城。$R;" +
            "$R對我們道米尼族來說$R;" +
            "可以稱得上是最後的希望了。$R;" +
            "$P夜間是不允許出入西部要塞城的，$R;" +
            "所以請注意。$R;", "西部要塞的守衛");
        }

    }

}
            
            
        
     
    
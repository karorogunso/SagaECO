using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10057000
{
    public class S11000758 : Event
    {
        public S11000758()
        {
            this.EventID = 11000758;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 159, "今天也希望是豐收！$R;");
            Say(pc, 131, "在這裡祈禱的話$R;" +
                "一定會好運！$R;");
        }
    }
}
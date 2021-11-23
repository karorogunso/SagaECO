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
            Say(pc, 159, "今天也希望是丰收！$R;");
            Say(pc, 131, "在这里祈祷的话$R;" +
                "一定会好运！$R;");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30082000
{
    public class S11000300 : Event
    {
        public S11000300()
        {
            this.EventID = 11000300;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "老闆看著哪，真是的。$R;");
        }
    }
}
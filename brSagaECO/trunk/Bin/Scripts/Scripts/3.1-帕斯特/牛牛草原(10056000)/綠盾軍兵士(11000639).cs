using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10056000
{
    public class S11000639 : Event
    {
        public S11000639()
        {
            this.EventID = 11000639;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "從這裡開始非常危險$R;" +
                "請繞道而行$R;");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10057000
{
    public class S11000761 : Event
    {
        public S11000761()
        {
            this.EventID = 11000761;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 255, "嘿呦！嘿呦！$R;");
        }
    }
}
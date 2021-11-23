using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M30020004
{
    public class S11000596 : Event
    {
        public S11000596()
        {
            this.EventID = 11000596;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 501, "嘿嘿!!$R;");
        }
    }
}
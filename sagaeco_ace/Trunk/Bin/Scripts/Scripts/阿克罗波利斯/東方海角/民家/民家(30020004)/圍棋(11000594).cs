using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M30020004
{
    public class S11000594 : Event
    {
        public S11000594()
        {
            this.EventID = 11000594;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 111, "嘮嘮嘮嘮!!!$R;");
        }
    }
}
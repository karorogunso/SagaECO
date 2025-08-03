using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30074000
{
    public class S11000291 : Event
    {
        public S11000291()
        {
            this.EventID = 11000291;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "我们是空军啊！$R;");
        }
    }
}
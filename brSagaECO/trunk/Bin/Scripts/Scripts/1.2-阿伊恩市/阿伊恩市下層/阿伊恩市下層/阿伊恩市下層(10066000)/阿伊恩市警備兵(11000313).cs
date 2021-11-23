using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10066000
{
    public class S11000313 : Event
    {
        public S11000313()
        {
            this.EventID = 11000313;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "這裡閒人勿進$R;" +
                "一般人不能進來$R;" +
                "想進來就去議會得到許可，再來吧$R;");
        }
    }
}
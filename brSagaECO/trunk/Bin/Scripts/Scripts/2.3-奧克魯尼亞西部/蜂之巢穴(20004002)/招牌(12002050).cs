using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M20004002
{
    public class S12002050 : Event
    {
        public S12002050()
        {
            this.EventID = 12002050;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "這蜂巢裡的針葉，對恢復HP有幫助$R;" +
                "用這個來做藥水不行嗎？$R;");
        }
    }
}

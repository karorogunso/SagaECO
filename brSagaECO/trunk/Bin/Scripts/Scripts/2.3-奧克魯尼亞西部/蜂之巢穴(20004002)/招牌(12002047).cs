using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M20004002
{
    public class S12002047 : Event
    {
        public S12002047()
        {
            this.EventID = 12002047;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "佈滿招牌的地牢$R;" +
                "好像改這樣的名字也不錯$R;");
        }
    }
}

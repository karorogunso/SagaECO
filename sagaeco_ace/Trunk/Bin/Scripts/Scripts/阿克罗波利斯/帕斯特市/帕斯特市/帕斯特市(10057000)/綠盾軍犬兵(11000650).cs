using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10057000
{
    public class S11000650 : Event
    {
        public S11000650()
        {
            this.EventID = 11000650;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "我是军犬兵！$R;" +
                "$R天天训练那些狗狗们$R;" +
                "使他们能变成一队活耀的军犬$R;");
        }
    }
}
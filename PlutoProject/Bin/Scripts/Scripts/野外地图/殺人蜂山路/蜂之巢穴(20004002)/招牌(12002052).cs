using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M20004002
{
    public class S12002052 : Event
    {
        public S12002052()
        {
            this.EventID = 12002052;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "受蚂蜂时攻击别苦恼$R;" +
                "解决方法，去问一问前辈吧$R;" +
                "最好提升命中$R;" +
                "好！今天就开始训练吧！$R;");
        }
    }
}

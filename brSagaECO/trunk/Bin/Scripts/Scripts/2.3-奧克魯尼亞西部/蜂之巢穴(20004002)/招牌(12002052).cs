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
            Say(pc, 131, "受螞蜂時攻擊別苦惱$R;" +
                "解决方法，去問一問前輩吧$R;" +
                "最好提升命中$R;" +
                "好！今天就開始訓練吧！$R;");
        }
    }
}

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
            Say(pc, 131, "这蜂巢里的针叶，对恢复HP有帮助$R;" +
                "用这个来做药水不行吗？$R;");
        }
    }
}

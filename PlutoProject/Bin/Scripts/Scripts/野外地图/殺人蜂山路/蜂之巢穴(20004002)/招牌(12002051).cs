using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M20004002
{
    public class S12002051 : Event
    {
        public S12002051()
        {
            this.EventID = 12002051;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "有时候也有一些蜜蜂$R;" +
                 "飞得太快撞到这墙壁$R;" +
                 "所以要注意别冲得太快！$R;");
        }
    }
}

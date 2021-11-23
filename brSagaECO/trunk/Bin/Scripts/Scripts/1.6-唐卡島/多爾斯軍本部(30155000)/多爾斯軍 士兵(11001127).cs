using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30155000
{
    public class S11001127 : Event
    {
        public S11001127()
        {
            this.EventID = 11001127;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "這裡是多爾斯軍本部。$R;" +
                "沒有事情的話，請回吧。$R;");
            Warp(pc, 10062000, 174, 122);
        }
    }
}
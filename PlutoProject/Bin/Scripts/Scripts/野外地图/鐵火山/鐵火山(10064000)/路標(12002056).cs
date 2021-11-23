using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10064000
{
    public class S12002056 : Event
    {
        public S12002056()
        {
            this.EventID = 12002056;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "再向前走，就是 『艾恩萨乌斯联邦』$R;");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M魔物印章收集冊
{
    public class S10029387 : Event
    {
        public S10029387()
        {
            this.EventID = 10029387;
        }

        public override void OnEvent(ActorPC pc)
        {
            ShowUI(pc, UIType.Stamp);
        }
    }
}
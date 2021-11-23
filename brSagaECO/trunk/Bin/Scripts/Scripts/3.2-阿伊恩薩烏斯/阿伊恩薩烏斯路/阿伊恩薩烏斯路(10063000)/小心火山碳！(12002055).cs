using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10063000
{
    public class S12002055 : Event
    {
        public S12002055()
        {
            this.EventID = 12002055;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "從這裡開始注意火山碳！$R;");
        }
    }
}
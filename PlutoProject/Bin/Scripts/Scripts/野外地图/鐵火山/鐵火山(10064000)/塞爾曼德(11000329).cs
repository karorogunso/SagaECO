using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10064000
{
    public class S11000329 : Event
    {
        public S11000329()
        {
            this.EventID = 11000329;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "最近是不是有点干燥呢？$R;" +
                "在脚底擦点润肤霜才行啊。$R;");
        }
    }
}

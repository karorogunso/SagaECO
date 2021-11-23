using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M10018100
{
    public class S12002028 : Event
    {
        public S12002028()
        {
            this.EventID = 12002028;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "合成专家$R;");
        }
    }
}
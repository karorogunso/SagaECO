using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10001000
{
    public class S12002014 : Event
    {
        public S12002014()
        {
            this.EventID = 12002014;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "西邊不死之島$R;");
        }
    }
}

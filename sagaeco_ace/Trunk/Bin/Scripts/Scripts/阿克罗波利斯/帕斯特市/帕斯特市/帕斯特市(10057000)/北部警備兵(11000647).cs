using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10057000
{
    public class S11000647 : Event
    {
        public S11000647()
        {
            this.EventID = 11000647;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "北边有奇怪的城堡$R;" +
                "很危险喔，最好不要去$R;" +
                "$R小心点也没有坏处$R;");
        }
    }
}
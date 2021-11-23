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
            Say(pc, 131, "北邊有奇怪的城堡$R;" +
                "很危險喔，最好不要去$R;" +
                "$R小心點也没有壞處$R;");
        }
    }
}
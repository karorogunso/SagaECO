using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10062000
{
    public class S11001057 : Event
    {
        public S11001057()
        {
            this.EventID = 11001057;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "飛空庭真漂亮！$R;" +
                "我呀，一定會成為$R飛空庭駕駛員的！$R;");
        }
    }
}
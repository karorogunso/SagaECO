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
            Say(pc, 131, "飞空庭真漂亮！$R;" +
                "我呀，一定会成为$R飞空庭驾驶员的！$R;");
        }
    }
}
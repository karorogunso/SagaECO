using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M21003000
{
    public class S11001491 : Event
    {
        public S11001491()
        {
            this.EventID = 11001491;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "不太喜欢泰达尼亚的人……。$R;" +
            "把我们抛弃了……。$R;", "美人鱼");
        }


    }
}



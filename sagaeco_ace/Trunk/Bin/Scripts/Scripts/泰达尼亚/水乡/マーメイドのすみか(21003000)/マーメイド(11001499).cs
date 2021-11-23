using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M21003000
{
    public class S11001499 : Event
    {
        public S11001499()
        {
            this.EventID = 11001499;
        }

        public override void OnEvent(ActorPC pc)
        {

            Say(pc, 131, "……。$R;" +
            "（伪装一下没有坏处、实际上$R;" +
            "我们以人鱼的肉为目标$R;" +
            "怎么了……？）$R;" +
            "$R（不能大意呢。）$R;", "美人鱼");
        }


    }
}



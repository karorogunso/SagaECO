using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M21003000
{
    public class S11001504 : Event
    {
        public S11001504()
        {
            this.EventID = 11001504;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 134, "啊啊…那個雕像的位置是$R;" +
            "現在、是在什麽地方呢……。$R;", "美人魚");

        }


    }
}



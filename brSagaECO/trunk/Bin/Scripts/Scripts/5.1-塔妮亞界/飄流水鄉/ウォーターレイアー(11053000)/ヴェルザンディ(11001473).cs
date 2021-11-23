using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M11053000
{
    public class S11001473 : Event
    {
        public S11001473()
        {
            this.EventID = 11001473;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "要打开岛上人们的心扉$R;" +
            "是非常困难的事情。$R;" +
            "$R如果可以像以前的人和人魚們$R;" +
            "那樣交流的話$R;" +
            "那樣就好了……。$R;", "ヴェルザンディ");

        }


    }
}



using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M30020001
{
    public class S11000335 : Event
    {
        public S11000335()
        {
            this.EventID = 11000335;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11000434, 131, "它的名字是基尼$R;" +
                "$R非常慢吞吞的家伙$R;");
            Say(pc, 501, "汪汪汪!$R;");
        }
    }
}
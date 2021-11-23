using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M30020001
{
    public class S11000331 : Event
    {
        public S11000331()
        {
            this.EventID = 11000331;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11000434, 131, "它的名字是丁丁$R;" +
                "很喜欢跟随人$R;");
            Say(pc, 500, "汪汪!$R;");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M30020001
{
    public class S11000332 : Event
    {
        public S11000332()
        {
            this.EventID = 11000332;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11000434, 131, "它的名字是佐恩!$R;" +
                "比较安静胆小$R;");
            Say(pc, 500, "嗯?$R;");
        }
    }
}
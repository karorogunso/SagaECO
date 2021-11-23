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
    public class S11001069 : Event
    {
        public S11001069()
        {
            this.EventID = 11001069;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "总有一天我也会在这条街上$R开设自己的工作室$R;" +
                "$R成为一个优秀的工匠哦。$R;");
            Say(pc, 11001120, 131, "开玩笑的吧？$R;" +
                "$R你怎么可能成为优秀的工匠？$R;" +
                "不要误会，你怎么可能…$R;" +
                "$R中途一定会放弃的。$R;");
            Say(pc, 131, "太…太过分了。$R;");
        }
    }
}
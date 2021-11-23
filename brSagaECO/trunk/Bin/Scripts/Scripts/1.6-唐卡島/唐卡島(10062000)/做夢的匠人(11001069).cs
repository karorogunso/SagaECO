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
            Say(pc, 131, "總有一天我也會在這條街上$R開設自己的工作室$R;" +
                "$R成為一個優秀的工匠唷。$R;");
            Say(pc, 11001120, 131, "開玩笑的吧？$R;" +
                "$R你怎麼可能成為優秀的工匠？$R;" +
                "不要誤會，你怎麼可能…$R;" +
                "$R中途一定會放棄的。$R;");
            Say(pc, 131, "太…太過分了。$R;");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30091000
{
    public class S11000741 : Event
    {
        public S11000741()
        {
            this.EventID = 11000741;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "知道吗？我！我看到了小精灵！！！$R;");
            Say(pc, 11000742, 131, "真的？$R;");
            Say(pc, 131, "带著小小的翅膀！$R;");
            Say(pc, 11000742, 131, "呜啊！在哪里看到的？$R;");
            Say(pc, 131, "东方地牢！$R;");
            Say(pc, 11000742, 131, "什么？不可以进去！$R;" +
                "老师不是那样讲的嘛？！$R;");
            Say(pc, 131, "只是进去捡掉进去的球而已$R;");
            Say(pc, 11000742, 131, "骗人的吧？$R;");
        }
    }
}
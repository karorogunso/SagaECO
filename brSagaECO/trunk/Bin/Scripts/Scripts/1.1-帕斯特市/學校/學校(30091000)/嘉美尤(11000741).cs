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
            Say(pc, 131, "知道嗎？我！我看到了尼姆！！！$R;");
            Say(pc, 11000742, 131, "真的？$R;");
            Say(pc, 131, "帶著小小的翅膀！$R;");
            Say(pc, 11000742, 131, "嗚啊！在哪裡看到的？$R;");
            Say(pc, 131, "東方地牢！$R;");
            Say(pc, 11000742, 131, "什麽？不可以進去！$R;" +
                "老師不是那樣講的嘛？！$R;");
            Say(pc, 131, "只是進去撿掉進去的球而已$R;");
            Say(pc, 11000742, 131, "騙人的吧？$R;");
        }
    }
}
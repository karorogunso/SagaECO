using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10017000
{
    public class S12002011 : Event
    {
        public S12002011()
        {
            this.EventID = 12002011;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "什么都没写？？$R;");
            switch (Select(pc, "后面写了些什么…", "", "看看吧！", "不看！"))
            {
                case 1:
                    Say(pc, 131, "把『牛奶』煮热$R;" +
                        "放『砂糖』和『蜂蜜』$R;" +
                        "$R泠却后就完成了$R;");
                    break;
                case 2:
                    break;
            }
        }
    }
}

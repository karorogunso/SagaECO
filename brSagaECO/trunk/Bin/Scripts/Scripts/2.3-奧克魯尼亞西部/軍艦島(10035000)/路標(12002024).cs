using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10035000
{
    public class S12002024 : Event
    {
        public S12002024()
        {
            this.EventID = 12002024;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "『荒廢礦村』$R;");
            switch (Select(pc, "後面寫了些什麽…", "", "看看吧！", "不看！"))
            {
                case 1:
                    Say(pc, 131, "把『馬鈴薯』煮成泥狀$R;" +
                        "$P加一片『蘋果』和『鹽』$R;" +
                        "然後再加『生菜』$R;" +
                        "$P就成為非常美味的『沙拉』了!$R;");
                    break;
                case 2:
                    break;
            }
        }
    }
}
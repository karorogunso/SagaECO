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
            Say(pc, 131, "『废炭矿』$R;");
            switch (Select(pc, "后面写了些什么…", "", "看看吧！", "不看！"))
            {
                case 1:
                    Say(pc, 131, "把『马铃薯』煮成泥状$R;" +
                        "$P加一片『苹果』和『盐』$R;" +
                        "然后再加『生菜』$R;" +
                        "$P就成为非常美味的『沙拉』了!$R;");
                    break;
                case 2:
                    break;
            }
        }
    }
}
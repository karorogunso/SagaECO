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
    public class S11001080 : Event
    {
        public S11001080()
        {
            this.EventID = 11001080;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "呜哗~~$R;" +
                "$R唐卡虽然是漂亮的地方，…$R;" +
                "但山丘较多，实在太累了$R;");
            Say(pc, 11001021, 131, "已经到了大厦，再加把劲吧。$R;");
        }
    }
}
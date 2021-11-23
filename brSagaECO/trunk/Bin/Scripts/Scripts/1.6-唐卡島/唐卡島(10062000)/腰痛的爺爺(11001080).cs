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
            Say(pc, 131, "嗚嘩~~$R;" +
                "$R唐卡雖然是漂亮的地方，…$R;" +
                "但山丘較多，實在太累了$R;");
            Say(pc, 11001021, 131, "已經到了大廈，再加把勁吧。$R;");
        }
    }
}
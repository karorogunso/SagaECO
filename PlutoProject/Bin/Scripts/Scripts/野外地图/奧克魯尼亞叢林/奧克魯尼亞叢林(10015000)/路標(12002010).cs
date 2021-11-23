using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10015000
{
    public class S12002010 : Event
    {
        public S12002010()
        {
            this.EventID = 12002010;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "现在开始要小心『咕咕』$R;" +
                "$P让『咕咕』看到会被它攻击的$R;" +
                "是性格暴燥的大型魔物$R;" +
                "不适合做宠物$R;");
        }
    }
}

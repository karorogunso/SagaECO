using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:奧克魯尼亞北部平原(10014000) NPC基本信息:路標(12002008) X:62 Y:253
namespace SagaScript.M10014000
{
    public class S12002008 : Event
    {
        public S12002008()
        {
            this.EventID = 12002008;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, 0, "现在开始要小心「巴乌」。$R;" +
                          "$P让「巴乌」看到会被它攻击的。$R;" +
                          "它是性格暴燥的魔物$R;" +
                          "不太适合当做宠物。$R;", " "); 
        }
    }
}

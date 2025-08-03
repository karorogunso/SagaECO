using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M20004002
{
    public class S12002045 : Event
    {
        public S12002045()
        {
            this.EventID = 12002045;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "昆虫中，工蜂诞生的一瞬间$R;" +
                "看过没有？$R;" +
                "没看过的话就幸运多了$R;" +
                "$P怎样出生？$R;" +
                "见过草蜢出生的人$R;" +
                "很容易想像得到吧……呕！！$R;" +
                "$P我不会再到这里来的！$R;" +
                "                   冒险者嘉嘉$R;");
        }
    }
}

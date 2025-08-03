using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10032000
{
    public class S12002023 : Event
    {
        public S12002023()
        {
            this.EventID = 12002023;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "小心前面乌特纳胡的巴乌喔！！$R;");
            switch (Select(pc, "后面写了些什么…", "", "看看吧！", "不看！"))
            {
                case 1:
                    Say(pc, 131, "用『矿泉水』和『蔬菜』$R;" +
                        "可以做出『野菜汁』$R;" +
                        "$R比想像的好吃呢！$R;");
                    break;
                case 2:
                    break;
            }
        }
    }
}

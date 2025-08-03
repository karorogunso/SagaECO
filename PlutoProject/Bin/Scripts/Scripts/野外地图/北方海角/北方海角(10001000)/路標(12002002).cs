using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10001000
{
    public class S12002002 : Event
    {
        public S12002002()
        {
            this.EventID = 12002002;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "西方斯诺普雪原$R;");
            switch (Select(pc, "后面写了些什么…", "", "看看吧！", "不看！"))
            {
                case 1:
                    Say(pc, 131, "用『牛油』炒『蟹粉』$R;" +
                        "$P把『蟹粉』放到大锅里$R;" +
                        "放适量的水$R;" +
                        "$P然后放『骨头』$R;" +
                        "煮两三个小时就可以了$R;" +
                        "$P配『面包树果实』一起吃的话$R;" +
                        "很好吃的$R;");
                    break;
                case 2:
                    break;
            }
        }
    }
}

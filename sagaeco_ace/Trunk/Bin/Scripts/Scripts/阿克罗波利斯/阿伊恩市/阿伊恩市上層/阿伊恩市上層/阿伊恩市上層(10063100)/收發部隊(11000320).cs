using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10063100
{
    public class S11000320 : Event
    {
        public S11000320()
        {
            this.EventID = 11000320;
        }

        public override void OnEvent(ActorPC pc)
        {
            NavigateCancel(pc);
            Say(pc, 131, "我们是收发部队。$R;" +
                "知道关于这个城市的所有资料。$R;");
            switch (Select(pc, "要介绍吗？", "", "空军本部", "议会", "佣兵军团本部", "不"))
            {
                case 1:
                    Say(pc, 131, "那个地方$R;" +
                        "就是空军本部啊。$R;");
                    break;
                case 2:
                    Say(pc, 131, "中央的大建筑物$R;" +
                        "就是议会喔。$R;");
                    Navigate(pc, 45, 103);
                    break;
                case 3:
                    Say(pc, 131, "从议会再往前走一会儿$R;" +
                        "就是佣兵军团本部喔。$R;");
                    Navigate(pc, 44, 156);
                    break;
            }
        }
    }
}
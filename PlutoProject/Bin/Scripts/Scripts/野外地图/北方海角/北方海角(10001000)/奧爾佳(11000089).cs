using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10001000
{
    public class S11000089 : Event
    {
        public S11000089()
        {
            this.EventID = 11000089;
            this.questTransportSource = "是不是有什么要转交给我的?$R;" +
                                       "虽然很年轻可是您真的很厉害啊$R;" +
                                       "谢谢您了喔$R;";
            this.transport = "那就拜托您了$R;";
            this.questTransportDest = "您带了东西给我?$R;" +
                                      "虽然很年轻，不过小姐可真是厉害$R;" +
                                      "谢谢您了喔$R;";
            this.questTransportCompleteSrc = "已经帮我转交给对方了吗！？$R;" +
                                             "真的谢谢啊!$R;" +
                                             "$R请去任务服务台领取报酬吧！$R;";
        }

        public override void OnEvent(ActorPC pc)
        {
            int selection;
            selection = Global.Random.Next(1, 2);
            switch (selection)
            {
                case 1:
                    Say(pc, 131, "从这里往北方看，将会看到诺森岛$R;" +
                        "诺森岛非常的冷啊，要注意保暖喔$R;");
                    break;
                case 2:
                    Say(pc, 131, "你问我冷不冷?$R;" +
                        "全靠精力充沛$R;" +
                        "只要有精力，下雪也不是问题$R;");
                    break;
            }
        }
    }
}

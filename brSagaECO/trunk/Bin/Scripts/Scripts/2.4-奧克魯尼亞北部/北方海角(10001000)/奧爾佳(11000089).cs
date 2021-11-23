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
            this.questTransportSource ="是不是有什麽要轉交給我的?$R;" +
                                       "雖然年紀不大可是您真的很厲害啊$R;" +
                                       "謝謝您了喔$R;";
            this.transport = "那就拜託您了$R;";
            this.questTransportDest = "您帶了東西給我?$R;" +
                                      "雖然年紀不大，不過小姐可真是厲害$R;" +
                                      "謝謝您了喔$R;";
            this.questTransportCompleteSrc = "已經幫我轉交給對方了嗎！？$R;" +
                                             "真的謝謝阿!$R;" +
                                             "$R請去任務服務台領取報酬吧！$R;";
        }

        public override void OnEvent(ActorPC pc)
        {
            int selection;
            selection = Global.Random.Next(1, 2);
            switch (selection)
            {
                case 1:
                    Say(pc, 131, "從這裡往北方看，將會看到諾頓島$R;" +
                        "諾頓島非常的冷啊，要注意保暖喔$R;");
                    break;
                case 2:
                    Say(pc, 131, "你問我冷不冷?$R;" +
                        "全靠精力充沛$R;" +
                        "只要有精力，下雪也不是問題$R;");
                    break;
            }
        }
    }
}

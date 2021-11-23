using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10001000
{
    public class S11000172 : Event
    {
        public S11000172()
        {
            this.EventID = 11000172;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "這裡開始是『寶拉熊』$R;" +
                "棲息的危險地帶$R;" +
                "$R趕緊回去比較好!$R;" +
                "$P……我只是警告而已$R;" +
                "$R因爲不是禁止出入的地區$R;" +
                "所以無法阻止$R;");
            switch (Select(pc, "問什麽嗎?", "", "『寶拉熊』是什麽?", "此外的情報?", "沒什麽好問的"))
            {
                case 1:
                    Say(pc, 131, "寶拉熊是非常殘暴的白熊$R;" +
                        "力氣大而且愛攻擊人$R;" +
                        "$R每年都有被害者，你也小心點的好$R;");
                    break;
                case 2:

                    Say(pc, 131, "偶爾能發現比寶拉熊更大的腳印$R;" +
                        "$R那到底是什麽腳印呢?$R;");
                    break;
            }
        }
    }
}

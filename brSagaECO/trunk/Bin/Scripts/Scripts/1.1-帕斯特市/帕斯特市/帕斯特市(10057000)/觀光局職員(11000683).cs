using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10057000
{
    public class S11000683 : Event
    {
        public S11000683()
        {
            this.EventID = 11000683;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "歡迎光臨農業國家帕斯特$R;" +
                "需要什麽幫助?$R;");
            switch (Select(pc, "要給您帶路嗎？", "", "不用了", "管理局", "農夫行會總部", "綠色防備軍本部", "商店區", "學校"))
            {
                case 2:
                    Say(pc, 131, "帕斯特管理局在這裡$R;" +
                        "$R他們會給年輕的冒險家$R;" +
                        "委託簡單的事情的$R;");
                    break;
                case 3:
                    Say(pc, 131, "這裡是農夫行會總部吧$R;" +
                        "不僅是農夫行會$R;" +
                        "$R也有生產系行會分會$R;" +
                        "好好利用吧！$R;");
                    Navigate(pc, 174, 100);
                    break;
                case 4:
                    Say(pc, 131, "綠色防備軍的本部就是那個建築$R;" +
                        "$R綠色防備軍如其名$R;" +
                        "是負責這個國家防禦和$R;" +
                        "城市安全的國家！$R;" +
                        "$P就連喜歡小狗的人聚在一起$R;" +
                        "他們也管的！$R;");
                    Navigate(pc, 28, 100);
                    break;
                case 5:
                    Say(pc, 131, "是咖啡館和武器商區的集中地$R;" +
                        "非常繁華$R;" +
                        "帶您到商店區的入口吧$R;" +
                        "請隨著箭頭方向走$R;");
                    Navigate(pc, 141, 103);
                    break;
                case 6:
                    Say(pc, 131, "掛著大鐘的建築物就是學校$R;" +
                        "誰都可以上學的$R;" +
                        "去看看吧！$R;");
                    Navigate(pc, 147, 119);
                    break;
            }
        }
    }
}
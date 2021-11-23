using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10057000
{
    public class S11000672 : Event
    {
        public S11000672()
        {
            this.EventID = 11000672;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "歡迎光臨", "", "買東西", "賣東西", "相談", "什麽都不要"))
            {
                case 1:
                    Say(pc, 131, "多買點!$R;");
                    OpenShopBuy(pc, 147);
                    break;
                case 2:
                    Say(pc, 131, "什麽都買呢!$R;");
                    OpenShopSell(pc, 147);
                    break;
                case 3:
                    Say(pc, 131, "傳説很久之前在那個森林裡$R;" +
                        "住著尼姆$R;" +
                        "$P但是因為很久之前的一場戰爭$R;" +
                        "森林被污染了，所以消失了$R;" +
                        "$P可現在，偶爾有些小孩子説$R;" +
                        "看到了尼姆，引起了一些騷動$R;");
                    break;
            }
        }
    }
}
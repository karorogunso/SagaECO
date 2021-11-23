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
            switch (Select(pc, "欢迎光临", "", "买东西", "卖东西", "谈话", "什么都不要"))
            {
                case 1:
                    Say(pc, 131, "多买点!$R;");
                    OpenShopBuy(pc, 147);
                    break;
                case 2:
                    Say(pc, 131, "什么都买呢!$R;");
                    OpenShopSell(pc, 147);
                    break;
                case 3:
                    Say(pc, 131, "传説很久之前在那个森林里$R;" +
                        "住著小精灵$R;" +
                        "$P但是因为很久之前的一场战争$R;" +
                        "森林被污染了，所以消失了$R;" +
                        "$P可现在，偶尔有些小孩子説$R;" +
                        "看到了小精灵，引起了一些骚动$R;");
                    break;
            }
        }
    }
}
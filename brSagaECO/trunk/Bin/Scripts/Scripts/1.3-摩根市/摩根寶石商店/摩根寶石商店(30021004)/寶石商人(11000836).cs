using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30021004
{
    public class S11000836 : Event
    {
        public S11000836()
        {
            this.EventID = 11000836;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "歡迎光臨", "", "買泰斯曼", "買東西", "賣東西", "什麼也不做"))
            {
                case 1:
                    Say(pc, 131, "在倉庫裡發現舊的泰斯曼$R;" +
                        "效果可能差一點，便宜點賣您吧。$R;");
                    OpenShopBuy(pc, 199);
                    break;
                case 2:
                    OpenShopBuy(pc, 173);
                    break;
                case 3:
                    OpenShopSell(pc, 173);
                    break;
                case 4:
                    break;
            }

        }
    }
}
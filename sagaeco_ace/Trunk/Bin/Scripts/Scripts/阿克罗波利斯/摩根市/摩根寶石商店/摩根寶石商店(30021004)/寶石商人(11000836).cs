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
            switch (Select(pc, "欢迎光临", "", "买宝石护身符", "买东西", "卖东西", "什么也不做"))
            {
                case 1:
                    Say(pc, 131, "在仓库里发现旧的宝石护身符$R;" +
                        "效果可能差一点，便宜点卖您吧。$R;");
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
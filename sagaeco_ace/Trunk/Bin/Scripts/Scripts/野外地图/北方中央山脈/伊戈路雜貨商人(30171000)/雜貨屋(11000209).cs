using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30171000
{
    public class S11000209 : Event
    {
        public S11000209()
        {
            this.EventID = 11000209;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "快来！$R来到这种地方真了不起呀$R;" +
                "$P您也看到了，因为这个地方偏僻，$R没有什么好的商品，就看看吧$R;");
            switch (Select(pc, "买一件吗?", "", "买东西", "卖东西", "什么也不做"))
            {
                case 1:
                    OpenShopBuy(pc, 80);
                    break;
                case 2:
                    OpenShopSell(pc, 80);
                    break;
            }
        }
    }
}

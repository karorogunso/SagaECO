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
            Say(pc, 131, "快來！$R來到這種地方真了不起呀$R;" +
                "$P您也看到了，因為這個地方偏僻，$R沒有什麼好的商品，就看看吧$R;");
            switch (Select(pc, "買一件嗎?", "", "買東西", "賣東西", "什麼也不做"))
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

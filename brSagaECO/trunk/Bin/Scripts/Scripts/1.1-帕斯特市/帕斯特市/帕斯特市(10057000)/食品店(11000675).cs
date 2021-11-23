using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10057000
{
    public class S11000675 : Event
    {
        public S11000675()
        {
            this.EventID = 11000675;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "有新鮮的食物喔", "", "買東西", "賣東西", "訂餐", "什麽都不要"))
            {
                case 1:
                    OpenShopBuy(pc, 146);
                    break;
                case 2:
                    Say(pc, 131, "謝謝！$R;");
                    OpenShopSell(pc, 146);
                    break;
                case 3:
                    Synthese(pc, 2040, 5);
                    break;
            }
        }
    }
}
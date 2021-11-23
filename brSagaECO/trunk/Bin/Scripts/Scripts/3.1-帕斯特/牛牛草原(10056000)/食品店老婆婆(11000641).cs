using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10056000
{
    public class S11000641 : Event
    {
        public S11000641()
        {
            this.EventID = 11000641;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "有新鮮的食物喔！", "", "買東西", "賣東西", "訂購料理", "什麽都不要"))
            {
                case 1:
                    OpenShopBuy(pc, 146);
                    Say(pc, 131, "還有需要的嗎？$R;");
                    break;
                case 2:
                    OpenShopSell(pc, 146);
                    Say(pc, 131, "這裡找零錢！$R;");
                    break;
                case 3:
                    Synthese(pc, 2040, 5);
                    break;
            }
        }
    }
}
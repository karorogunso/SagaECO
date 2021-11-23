using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10066000
{
    public class S11000571 : Event
    {
        public S11000571()
        {
            this.EventID = 11000571;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "快來！", "", "買東西", "賣東西", "訂購料理", "商談", "什麼也不做"))
            {
                case 1:
                    OpenShopBuy(pc, 17);
                    break;
                case 2:
                    OpenShopSell(pc, 17);
                    break;
                case 3:
                    Synthese(pc, 2040, 5);
                    break;
                case 4:
                    Say(pc, 131, "蔬菜呢，煮熟會更好吃$R;" +
                        "$R我做菜很用心的$R;" +
                        "經常過來吧$R;");
                    break;
            }
        }
    }
}
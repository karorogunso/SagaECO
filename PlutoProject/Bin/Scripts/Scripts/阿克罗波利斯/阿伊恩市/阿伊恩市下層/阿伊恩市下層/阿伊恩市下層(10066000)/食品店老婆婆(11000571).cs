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
            switch (Select(pc, "快来！", "", "买东西", "卖东西", "订购料理", "商谈", "什么也不做"))
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
                    Say(pc, 131, "蔬菜呢，煮熟会更好吃$R;" +
                        "$R我做菜很用心的$R;" +
                        "经常过来吧$R;");
                    break;
            }
        }
    }
}
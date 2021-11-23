using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10054000
{
    public class S11000660 : Event
    {
        public S11000660()
        {
            this.EventID = 11000660;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "喂，便宜一點賣給您，看看再走啊!", "", "買東西", "賣東西", "什麼也不做"))
            {
                case 1:
                    OpenShopBuy(pc, 128);
                    Say(pc, 131, "…從哪裡弄來的，$R不說您也知道吧？$R;");
                    break;
                case 2:
                    OpenShopSell(pc, 128);
                    Say(pc, 131, "嘿嘿嘿……$R;");
                    break;
            }
        }
    }
}
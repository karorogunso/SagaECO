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
            switch (Select(pc, "喂，便宜一点卖给您，看看再走啊!", "", "买东西", "卖东西", "什么也不做"))
            {
                case 1:
                    OpenShopBuy(pc, 128);
                    Say(pc, 131, "…从哪里弄来的，$R不说您也知道吧？$R;");
                    break;
                case 2:
                    OpenShopSell(pc, 128);
                    Say(pc, 131, "嘿嘿嘿……$R;");
                    break;
            }
        }
    }
}
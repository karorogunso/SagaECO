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
    public class S11000661 : Event
    {
        public S11000661()
        {
            this.EventID = 11000661;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "小心点，别弄坏!", "", "买东西", "卖东西", "什么也不做"))
            {
                case 1:
                    OpenShopBuy(pc, 131);
                    Say(pc, 131, "别说是在这里买的。$R;");
                    break;
                case 2:
                    OpenShopSell(pc, 131);
                    Say(pc, 131, "再来啊!$R;");
                    break;
            }
        }
    }
}
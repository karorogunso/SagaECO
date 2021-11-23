using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10062000
{
    public class S11001037 : Event
    {
        public S11001037()
        {
            this.EventID = 11001037;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "来这里做什么呢？", "", "买东西", "卖东西", "委托料理", "什么也不做"))
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
                    break;
            }
        }
    }
}
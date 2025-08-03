using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10059100
{
    public class S11001272 : Event
    {
        public S11001272()
        {
            this.EventID = 11001272;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "快来呀", "", "买东西", "卖东西", "什么也不做"))
            {
                case 1:
                    OpenShopBuy(pc, 294);
                    break;
                case 2:
                    OpenShopSell(pc, 294);
                    break;
                case 3:
                    break;
            }
        }
    }
}
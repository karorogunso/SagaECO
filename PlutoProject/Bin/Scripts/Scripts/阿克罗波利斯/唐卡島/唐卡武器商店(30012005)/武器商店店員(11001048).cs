using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30012005
{
    public class S11001048 : Event
    {
        public S11001048()
        {
            this.EventID = 11001048;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "找什么呢？", "", "买东西", "卖东西", "什么也不做"))
            {
                case 1:
                    OpenShopBuy(pc, 209);
                    break;
                case 2:
                    OpenShopSell(pc, 209);
                    break;
                case 3:
                    break;
            }
        }
    }
}
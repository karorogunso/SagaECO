using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30020005
{
    public class S11000691 : Event
    {
        public S11000691()
        {
            this.EventID = 11000691;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "欢迎光临!", "", "买东西", "卖东西", "什么都不做"))
            {
                case 1:
                    OpenShopBuy(pc, 142);
                    break;
                case 2:
                    OpenShopSell(pc, 142);
                    break;
            }
        }
    }
}
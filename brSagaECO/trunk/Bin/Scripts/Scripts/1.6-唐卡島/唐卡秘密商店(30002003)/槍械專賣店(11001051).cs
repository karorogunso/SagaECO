using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30014003
{
    public class S11001051 : Event
    {
        public S11001051()
        {
            this.EventID = 11001051;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "怎麼知道這裡的…?", "", "買東西", "賣東西", "什麼也不做"))
            {
                case 1:
                    OpenShopBuy(pc, 169);
                    break;
                case 2:
                    OpenShopSell(pc, 169);
                    break;
                case 3:
                    break;
            }
        }
    }
}
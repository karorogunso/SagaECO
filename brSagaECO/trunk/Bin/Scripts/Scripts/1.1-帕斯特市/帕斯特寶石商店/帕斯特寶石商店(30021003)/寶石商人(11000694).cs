using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30021003
{
    public class S11000694 : Event
    {
        public S11000694()
        {
            this.EventID = 11000694;
        }

        public override void OnEvent(ActorPC pc)
        {

            switch (Select(pc, "歡迎光臨!", "", "買東西", "賣東西", "什麽都不做"))
            {
                case 1:
                    OpenShopBuy(pc, 141);
                    break;
                case 2:
                    OpenShopSell(pc, 141);
                    break;
            }
        }
    }
}
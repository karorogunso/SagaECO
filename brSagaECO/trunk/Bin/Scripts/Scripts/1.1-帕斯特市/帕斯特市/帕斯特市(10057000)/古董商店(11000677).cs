using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10057000
{
    public class S11000677 : Event
    {
        public S11000677()
        {
            this.EventID = 11000677;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "歡迎光臨!", "", "買東西", "賣東西", "什麽都不做"))
            {
                case 1:
                    OpenShopBuy(pc, 138);
                    break;
                case 2:
                    OpenShopSell(pc, 138);
                    break;
            }
        }
    }
}
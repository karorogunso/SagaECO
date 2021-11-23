using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30012003
{
    public class S11000695 : Event
    {
        public S11000695()
        {
            this.EventID = 11000695;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "歡迎光臨!", "", "買東西", "賣東西", "什麽都不做"))
            {
                case 1:
                    OpenShopBuy(pc, 134);
                    break;
                case 2:
                    OpenShopSell(pc, 134);
                    break;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30012004
{
    public class S11000850 : Event
    {
        public S11000850()
        {
            this.EventID = 11000850;

        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "有很多好的武器！", "", "買東西", "賣東西", "什麼也不做"))
            {
                case 1:
                    OpenShopBuy(pc, 168);
                    break;
                case 2:
                    OpenShopSell(pc, 168);
                    break;
                case 3:
                    break;
            }
        }
    }
}
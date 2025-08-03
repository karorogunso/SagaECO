using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30020006
{
    public class S11000837 : Event
    {
        public S11000837()
        {
            this.EventID = 11000837;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "什么事呢？", "", "买东西", "卖东西", "什么也不做"))
            {
                case 1:
                    OpenShopBuy(pc, 172);
                    break;
                case 2:
                    OpenShopSell(pc, 172);
                    break;
                case 3:
                    break;
            }
        }
    }
}
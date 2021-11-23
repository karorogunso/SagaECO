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
    public class S11001038 : Event
    {
        public S11001038()
        {
            this.EventID = 11001038;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "快来呀", "", "买东西", "卖东西", "什么也不做"))
            {
                case 1:
                    OpenShopBuy(pc, 10);
                    break;
                case 2:
                    OpenShopSell(pc, 10);
                    break;
                case 3:
                    break;
            }
        }
    }
}
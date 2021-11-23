using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30001002
{
    public class S11000690 : Event
    {
        public S11000690()
        {
            this.EventID = 11000690;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "欢迎光临!", "", "买东西", "卖东西", "什么都不做"))
            {
                case 1:
                    OpenShopBuy(pc, 137);
                    break;
                case 2:
                    OpenShopSell(pc, 137);
                    break;
            }
        }
    }
}
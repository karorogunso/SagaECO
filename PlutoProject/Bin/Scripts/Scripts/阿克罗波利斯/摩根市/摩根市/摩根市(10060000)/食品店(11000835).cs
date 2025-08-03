using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10060000
{
    public class S11000835 : Event
    {
        public S11000835()
        {
            this.EventID = 11000835;

        }

        public override void OnEvent(ActorPC pc)
        {

            switch (Select(pc, "欢迎光临", "", "买东西", "卖东西", "什么也不做"))
            {
                case 1:
                    OpenShopBuy(pc, 171);
                    break;
                case 2:
                    OpenShopSell(pc, 171);
                    break;
                case 3:
                    break;
            }
        }
    }
}
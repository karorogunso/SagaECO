using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30002002
{
    public class S11000846 : Event
    {
        public S11000846()
        {
            this.EventID = 11000846;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "欢迎光临…", "", "买东西", "卖东西", "什么也不做"))
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
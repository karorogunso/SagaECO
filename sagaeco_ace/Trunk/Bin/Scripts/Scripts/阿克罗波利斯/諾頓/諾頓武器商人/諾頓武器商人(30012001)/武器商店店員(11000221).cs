using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30012001
{
    public class S11000221 : Event
    {
        public S11000221()
        {
            this.EventID = 11000221;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "有很多好武器喔！", "", "买东西", "卖东西", "什么也不做"))
            {
                case 1:
                    OpenShopBuy(pc, 71);
                    break;
                case 2:
                    OpenShopSell(pc, 71);
                    break;
                case 3:
                    break;
            }
        }
    }
}

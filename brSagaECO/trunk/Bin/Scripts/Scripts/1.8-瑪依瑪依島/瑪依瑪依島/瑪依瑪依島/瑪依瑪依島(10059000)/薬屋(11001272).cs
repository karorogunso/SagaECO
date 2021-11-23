using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10059000
{
    public class S11001272 : Event
    {
        public S11001272()
        {
            this.EventID = 11001272;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "いらっしゃい", "", "買い物をする", "物を売る", "何もしない"))
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
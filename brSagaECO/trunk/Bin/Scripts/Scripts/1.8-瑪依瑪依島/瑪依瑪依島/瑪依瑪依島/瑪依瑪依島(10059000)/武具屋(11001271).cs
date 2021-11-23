using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10059000
{
    public class S11001271 : Event
    {
        public S11001271()
        {
            this.EventID = 11001271;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "なんのようだい？", "", "買い物をする", "物を売る", "何もしない"))
            {
                case 1:
                    OpenShopBuy(pc, 209);
                    break;
                case 2:
                    OpenShopSell(pc, 209);
                    break;
                case 3:
                    break;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10059100
{
    public class S11001270 : Event
    {
        public S11001270()
        {
            this.EventID = 11001270;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "おやおや、よく来たねぇ。", "", "買い物をする", "物を売る", "料理を頼む", "何もしない"))
            {
                case 1:
                    OpenShopBuy(pc, 17);
                    break;
                case 2:
                    OpenShopSell(pc, 17);
                    break;
                case 3:
                    Synthese(pc, 2040, 5);
                    break;
                case 4:
                    break;
            }
        }
    }
}
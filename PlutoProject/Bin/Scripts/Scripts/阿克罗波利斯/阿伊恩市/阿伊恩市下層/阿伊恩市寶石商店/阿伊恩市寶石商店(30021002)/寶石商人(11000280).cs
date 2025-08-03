using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30021002
{
    public class S11000280 : Event
    {
        public S11000280()
        {
            this.EventID = 11000280;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "要什么呢？", "", "买东西", "卖东西", "什么都不做"))
            {
                case 1:
                    OpenShopBuy(pc, 99);
                    break;
                case 2:
                    OpenShopSell(pc, 99);
                    break;
            }
            Say(pc, 131, "下次再光临啊！$R;");
        }
    }
}
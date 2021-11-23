using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30020002
{
    public class S11000218 : Event
    {
        public S11000218()
        {
            this.EventID = 11000218;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "歡迎光臨！", "", "買東西", "賣東西", "什麼也不做"))
            {
                case 1:
                    OpenShopBuy(pc, 77);
                    break;
                case 2:
                    OpenShopSell(pc, 77);
                    break;
            }
            Say(pc, 131, "歡迎再來$R;");
        }
    }
}

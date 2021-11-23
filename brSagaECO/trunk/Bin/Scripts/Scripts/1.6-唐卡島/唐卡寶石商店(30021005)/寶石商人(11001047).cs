using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30021005
{
    public class S11001047 : Event
    {
        public S11001047()
        {
            this.EventID = 11001047;
        }

        public override void OnEvent(ActorPC pc)
        {//EVT11001047
            switch (Select(pc, "歡迎光臨！", "", "買東西", "賣東西", "什麼也不做"))
            {
                case 1:
                    OpenShopBuy(pc, 18);
                    Say(pc, 131, "再來啊$R;");
                    break;
                case 2:
                    OpenShopSell(pc, 18);
                    Say(pc, 131, "再來啊$R;");
                    break;
                case 3:
                    break;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10066000
{
    public class S11000572 : Event
    {
        public S11000572()
        {
            this.EventID = 11000572;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "歡迎光臨，找什麼啊 ？", "", "買東西", "賣東西", "什麼也不做"))
            {
                case 1:
                    OpenShopBuy(pc, 07);
                    break;
                case 2:
                    OpenShopSell(pc, 07);
                    break;
            }
            Say(pc, 131, "再來啊！$R;");
        }
    }
}
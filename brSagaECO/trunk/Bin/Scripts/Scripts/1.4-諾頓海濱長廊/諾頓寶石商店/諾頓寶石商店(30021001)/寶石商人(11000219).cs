using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30021001
{
    public class S11000219 : Event
    {
        public S11000219()
        {
            this.EventID = 11000219;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "有沒有什麼需要的呀？", "", "買東西", "賣東西", "什麼也不做"))
            {
                case 1:
                    OpenShopBuy(pc, 78);
                    break;
                case 2:
                    OpenShopSell(pc, 78);
                    break;
            }
            Say(pc, 131, "歡迎再來$R;");
        }
    }
}

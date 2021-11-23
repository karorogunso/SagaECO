using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10054000
{
    public class S11000661 : Event
    {
        public S11000661()
        {
            this.EventID = 11000661;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "小心點，別弄壞!", "", "買東西", "賣東西", "什麼也不做"))
            {
                case 1:
                    OpenShopBuy(pc, 131);
                    Say(pc, 131, "不能說是在這裡買的。$R;");
                    break;
                case 2:
                    OpenShopSell(pc, 131);
                    Say(pc, 131, "再來啊!$R;");
                    break;
            }
        }
    }
}
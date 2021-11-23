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
    public class S11000663 : Event
    {
        public S11000663()
        {
            this.EventID = 11000663;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "…", "", "買東西", "賣東西", "什麼也不做"))
            {
                case 1:
                    OpenShopBuy(pc, 129);
            Say(pc, 131, "事情辦完的話，就回去吧$R;");
                    break;
                case 2:
                    OpenShopSell(pc, 129);
                    Say(pc, 131, "事情辦完的話，就回去吧$R;");
                    break;
                case 3:
                    Say(pc, 131, "喂，把你的手打開$R;" +
                        "$P好，什麼也沒拿$R;" +
                        "$R好了，你可以走了$R;");
                    break;
            }
        }
    }
}
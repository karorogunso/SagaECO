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
            switch (Select(pc, "…", "", "买东西", "卖东西", "什么也不做"))
            {
                case 1:
                    OpenShopBuy(pc, 129);
                    Say(pc, 131, "事情办完的话，就回去吧$R;");
                    break;
                case 2:
                    OpenShopSell(pc, 129);
                    Say(pc, 131, "事情办完的话，就回去吧$R;");
                    break;
                case 3:
                    Say(pc, 131, "喂，把你的手打开$R;" +
                        "$P好，什么也没拿$R;" +
                        "$R好了，你可以走了$R;");
                    break;
            }
        }
    }
}
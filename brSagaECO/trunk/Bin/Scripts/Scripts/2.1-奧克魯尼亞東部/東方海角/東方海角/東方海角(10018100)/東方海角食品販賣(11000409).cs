using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M10018100
{
    public class S11000409 : Event
    {
        public S11000409()
        {
            this.EventID = 11000409;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "怎麽做呢?", "", "買東西", "賣東西", "放棄"))
            {
                case 1:
                    OpenShopBuy(pc, 30);
                    break;
                case 2:
                    OpenShopSell(pc, 30);
                    break;
            }
            Say(pc, 131, "再來玩吧$R;");
        }
    }
}
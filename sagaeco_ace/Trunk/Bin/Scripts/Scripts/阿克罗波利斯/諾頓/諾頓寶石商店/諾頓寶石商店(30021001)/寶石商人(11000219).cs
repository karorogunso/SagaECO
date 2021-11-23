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
            switch (Select(pc, "有没有什么需要的呀？", "", "买东西", "卖东西", "什么也不做"))
            {
                case 1:
                    OpenShopBuy(pc, 78);
                    break;
                case 2:
                    OpenShopSell(pc, 78);
                    break;
            }
            Say(pc, 131, "欢迎再来$R;");
        }
    }
}

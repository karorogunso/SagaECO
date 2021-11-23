using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30020003
{
    public class S11000279 : Event
    {
        public S11000279()
        {
            this.EventID = 11000279;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "欢迎光临！", "", "买东西", "卖东西", "什么也不做"))
            {
                case 1:
                    OpenShopBuy(pc, 96);
                    break;
                case 2:
                    OpenShopSell(pc, 96);
                    break;
            }
            Say(pc, 131, "再来啊！$R;");
        }
    }
}
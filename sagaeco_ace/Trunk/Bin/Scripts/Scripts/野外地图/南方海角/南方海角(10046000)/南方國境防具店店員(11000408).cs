using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
namespace SagaScript.M10046000
{
    public class S11000408 : Event
    {
        public S11000408()
        {
            this.EventID = 11000408;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "防御武器的话还是艾恩萨乌斯的东西是最好了!", "", "买东西", "卖东西", "放弃"))
            {
                case 1:
                    OpenShopBuy(pc, 32);
                    break;
                case 2:
                    OpenShopSell(pc, 32);
                    break;
                case 3:
                    return;
            }
            Say(pc, 131, "要不要也去看看$R亲爱的妻子的武器商店啊?$R;");
        }
    }
}
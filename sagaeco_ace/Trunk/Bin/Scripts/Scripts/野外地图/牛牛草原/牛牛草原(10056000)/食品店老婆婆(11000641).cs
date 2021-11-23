using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10056000
{
    public class S11000641 : Event
    {
        public S11000641()
        {
            this.EventID = 11000641;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "有新鲜的食物喔！", "", "买东西", "卖东西", "订购料理", "什么都不要"))
            {
                case 1:
                    OpenShopBuy(pc, 146);
                    Say(pc, 131, "还有需要的吗？$R;");
                    break;
                case 2:
                    OpenShopSell(pc, 146);
                    Say(pc, 131, "这里找零钱！$R;");
                    break;
                case 3:
                    Synthese(pc, 2040, 5);
                    break;
            }
        }
    }
}
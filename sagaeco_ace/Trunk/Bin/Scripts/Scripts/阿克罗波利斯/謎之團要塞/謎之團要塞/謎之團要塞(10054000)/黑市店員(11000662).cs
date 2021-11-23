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
    public class S11000662 : Event
    {
        public S11000662()
        {
            this.EventID = 11000662;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "…呼呼呼", "", "买东西", "卖东西", "什么也不做"))
            {
                case 1:
                    OpenShopBuy(pc, 130);
                    Say(pc, 131, "呵呵呵$R;" +
                        "这次的实验不知道有没有成功呢？$R;");
                    break;
                case 2:
                    OpenShopSell(pc, 130);
                    Say(pc, 131, "呵呵！$R;");
                    break;
            }
        }
    }
}
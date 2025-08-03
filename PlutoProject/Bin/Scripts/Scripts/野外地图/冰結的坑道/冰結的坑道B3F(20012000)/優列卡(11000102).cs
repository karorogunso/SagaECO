using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M20012000
{
    public class S11000102 : Event
    {
        public S11000102()
        {
            this.EventID = 11000102;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "啊啊啊…$R;" +
                "因为魔物在到处乱逛$R;" +
                "太可怕了$R;" +
                "$P但是不能只发抖阿!$R;" +
                "$R越是这个时候越要加把劲$R;" +
                "就算是生意也要做做看$R;");
            switch (Select(pc, "欢迎光临……", "", "买东西", "卖东西", "什么都不做"))
            {
                case 1:
                    OpenShopBuy(pc, 65);
                    break;
                case 2:
                    Say(pc, 131, "呀!计算器忘带了…$R;" +
                        "没有计算器的话我不能计算啊$R;" +
                        "这真是灾难啊$R;");
                    break;
                case 3:
                    Say(pc, 131, "啊啊…$R;" +
                        "我在这里呆到什么时候啊?$R;");
                    break;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
namespace SagaScript.M10046000
{
    public class S11000164 : Event
    {
        public S11000164()
        {
            this.EventID = 11000164;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "要购买艾恩萨乌斯的武器吗?", "", "购入", "卖东西", "放弃"))
            {
                case 1:
                    OpenShopBuy(pc, 31);
                    break;
                case 2:
                    OpenShopSell(pc, 31);
                    break;
                case 3:
                    return;
            }
            Say(pc, 131, "也看一下防具吧$R;");
        }
    }
}
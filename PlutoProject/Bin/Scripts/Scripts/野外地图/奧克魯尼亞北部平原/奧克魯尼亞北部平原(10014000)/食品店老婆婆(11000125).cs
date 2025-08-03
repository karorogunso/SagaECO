using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:奧克魯尼亞北部平原(10014000) NPC基本信息:食品店老婆婆(11000125) X:38 Y:120
namespace SagaScript.M10014000
{
    public class S11000125 : Event
    {
        public S11000125()
        {
            this.EventID = 11000125;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "啊啊，来的正好!", "", "买东西", "卖东西", "订购料理", "进行谈话", "什么也不做"))
            {
                case 1:
                    OpenShopBuy(pc, 17);
                    break;

                case 2:
                    OpenShopSell(pc, 17);
                    break;

                case 3:
                    Synthese(pc, 2040, 5);
                    break;

                case 4:
                    Say(pc, 11000125, 131, "像鸡蛋那样的食品啊…$R;" +
                                           "如果烹调一下的话味道会更好。$R;" +
                                           "$R我会精心制作的! 随时来找我吧$R;", "食品店老婆婆");
                    break;

                case 5:
                    break;
            }
        }
    }
}

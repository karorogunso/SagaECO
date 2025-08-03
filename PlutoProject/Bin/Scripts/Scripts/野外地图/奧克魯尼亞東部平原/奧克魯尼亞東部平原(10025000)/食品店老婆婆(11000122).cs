using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:奧克魯尼亞東部平原(10025000) NPC基本信息:食品店老婆婆(11000122) X:38 Y:120
namespace SagaScript.M10025000
{
    public class S11000122 : Event
    {
        public S11000122()
        {
            this.EventID = 11000122;
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
                    Say(pc, 11000122, 131, "像鸡蛋那样的食品啊…$R;" +
                                           "如果烹调一下的话味道会更好。$R;" +
                                           "$R我会精心制作的! 随时来找我吧$R;", "食品店老婆婆");
                    break;

                case 5:
                    break;
            }
        }
    }
}

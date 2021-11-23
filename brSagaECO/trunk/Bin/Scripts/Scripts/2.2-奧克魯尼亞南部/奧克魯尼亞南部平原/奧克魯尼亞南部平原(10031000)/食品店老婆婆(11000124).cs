using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:奧克魯尼亞南部平原(10031000) NPC基本信息:食品店老婆婆(11000124)- X:135 Y:38
namespace SagaScript.M10031000
{
    public class S11000124 : Event
    {
        public S11000124()
        {
            this.EventID = 11000124;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "啊啊，來的正好!", "", "買東西", "賣東西", "訂購料理", "進行談話", "什麽也不做"))
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
                    Say(pc, 11000124, 131, "像雞蛋那樣的食品啊…$R;" +
                                           "如果烹調一下的話味道會更好。$R;" +
                                           "$R我會精心製作的! 隨時來找我吧$R;", "食品店老婆婆");
                    break;

                case 5:
                    break;
            }
        }
    }
}

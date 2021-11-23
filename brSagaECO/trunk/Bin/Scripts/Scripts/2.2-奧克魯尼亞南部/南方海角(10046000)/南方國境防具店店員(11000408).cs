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
            switch (Select(pc, "防禦武器的話還是阿伊恩薩烏斯的東西是最好了!", "", "買東西", "賣東西", "放棄"))
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
            Say(pc, 131, "要不要也去看看$R親愛的妻子的武器商店阿?$R;");
        }
    }
}
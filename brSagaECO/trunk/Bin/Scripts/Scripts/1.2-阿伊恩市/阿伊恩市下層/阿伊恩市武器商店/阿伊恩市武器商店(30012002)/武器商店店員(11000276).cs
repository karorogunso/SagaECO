using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30012002
{
    public class S11000276 : Event
    {
        public S11000276()
        {
            this.EventID = 11000276;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.Job == PC_JOB.TATARABE
                || pc.Job == PC_JOB.BLACKSMITH
                || pc.Job == PC_JOB.MACHINERY) 
            {
                Say(pc, 131, "哦，原來您是礦工呢。$R;" +
                    "$R礦工通常身世很可憐阿$R;" +
                    "就給您看看別的地方拿不到的$R;" +
                    "特別武器吧。$R;");
                if (Select(pc, "歡迎光臨！", "", "觀看", "給我看看普通的武器") == 1)
                {
                        OpenShopBuy(pc, 91);
                        return;
                }
            }
            switch (Select(pc, "有很多好武器呢！", "", "買東西", "賣東西", "什麼也不做"))
            {
                case 1:
                    OpenShopBuy(pc, 90);
                    break;
                case 2:
                    OpenShopSell(pc, 90);
                    break;
            }
        }
    }
}
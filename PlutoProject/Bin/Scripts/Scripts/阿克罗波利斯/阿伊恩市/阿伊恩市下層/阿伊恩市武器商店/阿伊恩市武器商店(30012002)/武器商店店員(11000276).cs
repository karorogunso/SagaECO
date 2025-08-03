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
                Say(pc, 131, "哦，原来您是矿工呢。$R;" +
                    "$R矿工通常身世很可怜啊$R;" +
                    "就给您看看别的地方拿不到的$R;" +
                    "特别武器吧。$R;");
                if (Select(pc, "欢迎光临！", "", "观看", "给我看看普通的武器") == 1)
                {
                    OpenShopBuy(pc, 91);
                    return;
                }
            }
            switch (Select(pc, "有很多好武器呢！", "", "买东西", "卖东西", "什么也不做"))
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
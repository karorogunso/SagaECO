using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M12021000
{
    public class S11001581 : Event
    {
        public S11001581()
        {
            this.EventID = 11001581;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.WRPRanking <= 10)
            {
                Say(pc, 131, "排行上榜者啊！$R;" +
                "那也來看一下這邊的商品吧！" +
                "$R都是些在這個世道$R;" +
                "很難入手的東西哦。$R;", "雜貨店");
                switch (Select(pc, "來吧，請慢慢地看。", "", "購物（排行上榜者專用）", "購物", "賣東西", "交換『合成失敗物』", "什麽都不做"))
                {
                    case 1:
                        OpenShopBuy(pc, 272);
                        break;
                    case 2:
                        OpenShopBuy(pc, 285);
                        break;
                    case 3:
                        OpenShopSell(pc, 285);
                        break;

                }
                Say(pc, 131, "怎麽樣，在戰亂時$R;" +
                "要弄到這些東西是非常$R;" +
                "困難地事哦。$R;" +
                "$R還要再來光顧哦！$R;", "雜貨店");
                return;
            }
            switch (Select(pc, "歡迎光臨！這裏是雜貨店哦！", "", "購物", "賣東西", "交換『合成失敗物』", "什麽都不做"))
            {
                case 1:
                    OpenShopBuy(pc, 285);
                    break;
                case 2:
                    OpenShopSell(pc, 285);
                    break;
            }
        }

    }

}
            
            
        
     
    
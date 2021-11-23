using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30097000
{
    public class S11000851 : Event
    {
        public S11000851()
        {
            this.EventID = 11000851;

        }

        public override void OnEvent(ActorPC pc)
        {

            /*
            if (_6a69)
            {
                Say(pc, 131, "客人…$R;" +
                    "這個月的銷售情況不太好$R;" +
                    "$R能不能買點東西呢？$R;");
                switch (Select(pc, "怎麼辦呢?", "", "看袋子", "看防具", "賣東西", "什麼也不做"))
                {
                    case 1:
                        OpenShopBuy(pc, 184);
                        break;
                    case 2:
                        OpenShopBuy(pc, 197);
                        break;
                    case 3:
                        OpenShopSell(pc, 184);
                        break;
                    case 4:
                        break;
                }
                Say(pc, 131, "太謝謝了$R;");
                return;
            }
            */
            int a = pc.Level * 1000;
            if (pc.Gold < a)
            {
                Say(pc, 131, "客人？$R;" +
                    "$R我們店只賣名牌的呀$R;" +
                    "$R那點錢根本不夠，$R太小看我們店了吧$R;");
                return;
            }
            switch (Select(pc, "歡迎光臨", "", "買東西", "賣東西", "什麼也不做"))
            {
                case 1:
                    OpenShopBuy(pc, 180);
                    Say(pc, 131, "哎呀！客人！$R;" +
                        "這麼快就看完了？$R;" +
                        "$R這邊的東西$R;" +
                        "很適合客人您的唷！！$R;");
                    switch (Select(pc, "怎麼辦呢？", "", "好了", "什麼東西？"))
                    {
                        case 1:
                            Say(pc, 131, "謝謝$R;" +
                                "下次光臨唷$R;");
                            break;
                        case 2:
                            OpenShopBuy(pc, 180);
                            break;
                    }
                    break;
                case 2:
                    OpenShopSell(pc, 180);
                    Say(pc, 131, "謝謝$R;" +
                        "下次光臨唷$R;");
                    break;
                case 3:
                    break;
            }

        }
    }
}
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
                    "$R我们店只卖名牌的呀$R;" +
                    "$R那点钱根本不够，$R太小看我们店了吧$R;");
                return;
            }
            switch (Select(pc, "欢迎光临", "", "买东西", "卖东西", "什么也不做"))
            {
                case 1:
                    OpenShopBuy(pc, 180);
                    Say(pc, 131, "哎呀！客人！$R;" +
                        "这么快就看完了？$R;" +
                        "$R这边的东西$R;" +
                        "很适合客人您的哦！！$R;");
                    switch (Select(pc, "怎么办呢？", "", "好了", "什么东西？"))
                    {
                        case 1:
                            Say(pc, 131, "谢谢$R;" +
                                "下次光临$R;");
                            break;
                        case 2:
                            OpenShopBuy(pc, 180);
                            break;
                    }
                    break;
                case 2:
                    OpenShopSell(pc, 180);
                    Say(pc, 131, "谢谢$R;" +
                        "下次光临$R;");
                    break;
                case 3:
                    break;
            }

        }
    }
}
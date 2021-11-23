using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30171001
{
    public class S11000217 : Event
    {
        public S11000217()
        {
            this.EventID = 11000217;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<BBDLFlags> mask = new BitMask<BBDLFlags>(pc.CMask["BBDL"]);
            if (mask.Test(BBDLFlags.驚慌的商人第一次對話) && CountItem(pc, 10002204) >= 1)
            {
                switch (Select(pc, "歡迎光臨！", "", "買東西", "什麼也不做"))
                {
                    case 1:
                        TakeItem(pc, 10002204, 1);
                        OpenShopBuy(pc, 107);
                        Say(pc, 131, "還需要什麼的話$R;" +
                            "就把濃湯拿來吧$R;" +
                            "$R我會等著的$R;");
                        break;
                    case 2:
                        break;
                }

                return;
            }
            if (CountItem(pc, 10002204) >= 1)
            {
                Say(pc, 131, "那是不是巴列麗拿做的雜菜濃湯？$R;" +
                    "$R能不能給我呀？$R;");
                switch (Select(pc, "怎麼辦呢？", "", "給他", "不給"))
                {
                    case 1:
                        mask.SetValue(BBDLFlags.驚慌的商人第一次對話, true);
                        Say(pc, 131, "巴列麗拿雜菜濃湯$R;" +
                            "$R真好吃呀$R;" +
                            "除了有毒性成分以外$R;" +
                            "沒有什麼可挑剔的$R;" +
                            "$P真是太感謝了$R;" +
                            "$R雖然不能算做謝禮，$R;" +
                            "但還是看一下我的商品吧$R;");
                        switch (Select(pc, "歡迎光臨！", "", "買東西", "什麼也不做"))
                        {
                            case 1:
                                TakeItem(pc, 10002204, 1);
                                OpenShopBuy(pc, 107);
                                Say(pc, 131, "還需要什麼的話$R;" +
                                    "就把濃湯拿來吧$R;" +
                                    "$R我會等著的$R;");
                                break;
                            case 2:
                                break;
                        }

                        break;
                    case 2:
                        Say(pc, 131, "呀！太過分了$R;");
                        break;
                }
                return;
            }
            Say(pc, 131, "冷死了…$R;" +
                "$R啊！真想吃巴列麗拿做的雜菜濃湯$R;");
        }
    }
}

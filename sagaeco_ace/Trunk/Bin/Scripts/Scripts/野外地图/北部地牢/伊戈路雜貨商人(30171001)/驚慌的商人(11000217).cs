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
                switch (Select(pc, "欢迎光临！", "", "买东西", "什么也不做"))
                {
                    case 1:
                        TakeItem(pc, 10002204, 1);
                        OpenShopBuy(pc, 107);
                        Say(pc, 131, "还需要什么的话$R;" +
                            "就把瓦雷利亚的汤拿来吧$R;" +
                            "$R我会等着的$R;");
                        break;
                    case 2:
                        break;
                }

                return;
            }
            if (CountItem(pc, 10002204) >= 1)
            {
                Say(pc, 131, "那是不是瓦雷利亚的汤？$R;" +
                    "$R能不能给我呀？$R;");
                switch (Select(pc, "怎么办呢？", "", "给他", "不给"))
                {
                    case 1:
                        mask.SetValue(BBDLFlags.驚慌的商人第一次對話, true);
                        Say(pc, 131, "瓦雷利亚的汤$R;" +
                            "$R真好吃呀$R;" +
                            "除了有毒性成分以外$R;" +
                            "没有什么可挑剔的$R;" +
                            "$P真是太感谢了$R;" +
                            "$R虽然不能算做谢礼，$R;" +
                            "但还是看一下我的商品吧$R;");
                        switch (Select(pc, "欢迎光临！", "", "买东西", "什么也不做"))
                        {
                            case 1:
                                TakeItem(pc, 10002204, 1);
                                OpenShopBuy(pc, 107);
                                Say(pc, 131, "还需要什么的话$R;" +
                                    "就把瓦雷利亚的汤拿来吧$R;" +
                                    "$R我会等着的$R;");
                                break;
                            case 2:
                                break;
                        }

                        break;
                    case 2:
                        Say(pc, 131, "呀！太过分了$R;");
                        break;
                }
                return;
            }
            Say(pc, 131, "冷死了…$R;" +
                "$R啊！真想吃瓦雷利亚的汤$R;");
        }
    }
}

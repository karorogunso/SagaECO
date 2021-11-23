using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10001000
{
    public class S11000056 : Event
    {
        public S11000056()
        {
            this.EventID = 11000056;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<BFHJFlags> mask = new BitMask<BFHJFlags>(pc.CMask["BFHJ"]);
            if (CountItem(pc, 10009500) >= 1)
            {
                if (!mask.Test(BFHJFlags.諾頓國境警備員第一次對話))
                {
                    Say(pc, 131, "哎呀?$R;" +
                        "這是『銅徽章』喔?$R;" +
                        "$R在諾頓，流行$R;" +
                        "『銀徽章』$R;" +
                        "$P已經集齊得到$R;" +
                        "銀徽章的道具了…$R;" +
                        "$R如果有10個銅徽章的話$R;" +
                        "也可以交換銀徽章$R;");
                    mask.SetValue(BFHJFlags.諾頓國境警備員第一次對話, true);
                    return;
                }
                if (CountItem(pc, 10009500) >= 10)
                {
                    Say(pc, 131, "用10個『銅徽章』$R;" +
                        "交換1個『銀徽章』嗎?$R;");
                    switch (Select(pc, "交換嗎?", "", "交換", "不交喚"))
                    {
                        case 1:

                            if (CheckInventory(pc, 10009600, 1))
                            {
                                TakeItem(pc, 10009500, 10);
                                GiveItem(pc, 10009600, 1);
                                Say(pc, 131, "給10個『銅徽章』$R;" +
                                    "拿到了1個『銀徽章』!$R;");
                                Say(pc, 131, "那麽，只好祈求幸運!$R;");
                                return;
                            }
                            Say(pc, 131, "道具好像太多了$R;");
                            break;
                        case 2:
                            break;
                    }
                    return;
                }
                Say(pc, 131, "哎呀?$R;" +
                    "這是『銅徽章』喔?$R;" +
                    "$R在諾頓，流行$R;" +
                    "『銀徽章』$R;" +
                    "$P已經集齊得到$R;" +
                    "銀徽章的道具了…$R;" +
                    "$R如果有10個銅徽章的話$R;" +
                    "也可以交換銀徽章$R;");
                mask.SetValue(BFHJFlags.諾頓國境警備員第一次對話, true);
                return;
            }
            Say(pc, 131, "諾頓王國現在$R;" +
                "不是奧克魯尼亞混城騎士團北軍所屬團員$R;" +
                "或『諾頓入境許可證』$R;" +
                "持有者的話$R;" +
                "不能入境$R;");
        }
    }
}

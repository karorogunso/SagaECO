using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M10046000
{
    public class S11000167 : Event
    {
        public S11000167()
        {
            this.EventID = 11000167;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<HZFlags> mask = new BitMask<HZFlags>(pc.CMask["HZ"]);
            if (CountItem(pc, 10009600) >= 1)
            {
                if (!mask.Test(HZFlags.阿伊恩薩烏斯國境警備員第一次對話))
                {
                    Say(pc, 131, "這是『銀徽章』阿?$R;" +
                        "$R在阿伊恩薩烏斯$R;" +
                        "雖然流行$R;" +
                        "『金徽章』…$R;" +
                        "$P可以換金徽章的道具$R;" +
                        "我全部都擁有$R;" +
                        "$R如果是銀徽章10個的話$R;" +
                        "就跟金徽章換吧!$R;");
                    mask.SetValue(HZFlags.阿伊恩薩烏斯國境警備員第一次對話, true);
                    return;
                }
                if (CountItem(pc, 10009600) >= 10)
                {
                    Say(pc, 131, "『銀徽章』10個換$R;" +
                        "『金徽章』1個嗎?$R;");
                    switch (Select(pc, "交換嗎?", "", "交換", "不交換"))
                    {
                        case 1:
                            if (CheckInventory(pc, 10009700, 1))
                            {

                                TakeItem(pc, 10009600, 10);
                                GiveItem(pc, 10009700, 1);
                                Say(pc, 131, "給『銀徽章』10個$R;" +
                                    "得到了『金徽章』1個$R;");
                                Say(pc, 131, "希望能得到好東西!$R;");
                                return;
                            }
                            Say(pc, 131, "無法給道具啊!$R;");
                            break;
                        case 2:
                            break;
                    }
                    return;
                }
                Say(pc, 131, "這是『銀徽章』阿?$R;" +
                    "$R在阿伊恩薩烏斯$R;" +
                    "雖然流行$R;" +
                    "『金徽章』…$R;" +
                    "$P可以換金徽章的道具$R;" +
                    "我全部都擁有$R;" +
                    "$R如果是銀徽章10個的話$R;" +
                    "就跟金徽章換吧!$R;");
                return;
            }
            Say(pc, 131, "歡迎來到阿伊恩薩烏斯的聯邦!$R;" +
                "$R阿伊恩薩烏斯聯邦是$R;" +
                "屬於奧克魯尼亞$R;" +
                "只有持『阿伊恩薩烏斯許可證』$R;" +
                "才可以入境$R;");
        }
        void 騎士團(ActorPC pc)
        {
            Say(pc, 131, "屬於奧克魯尼亞混城騎士團$R;" +
                "『南軍』嗎?$R;" +
                "$R請在裡面辦理一下入境手續$R;");
        }
    }
}

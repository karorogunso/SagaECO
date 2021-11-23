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
                    Say(pc, 131, "这是『银徽章』啊?$R;" +
                        "$R在艾恩萨乌斯$R;" +
                        "虽然流行$R;" +
                        "『金徽章』…$R;" +
                        "$P金徽章可以兑换的道具$R;" +
                        "我全部都有$R;" +
                        "$R如果是银徽章10个的话$R;" +
                        "就跟金徽章和你换吧!$R;");
                    mask.SetValue(HZFlags.阿伊恩薩烏斯國境警備員第一次對話, true);
                    return;
                }
                if (CountItem(pc, 10009600) >= 10)
                {
                    Say(pc, 131, "『银徽章』10个换$R;" +
                        "『金徽章』1个吗?$R;");
                    switch (Select(pc, "交换吗?", "", "交换", "不交换"))
                    {
                        case 1:
                            if (CheckInventory(pc, 10009700, 1))
                            {

                                TakeItem(pc, 10009600, 10);
                                GiveItem(pc, 10009700, 1);
                                Say(pc, 131, "给『银徽章』10个$R;" +
                                    "得到了『金徽章』1个$R;");
                                Say(pc, 131, "希望能得到好东西!$R;");
                                return;
                            }
                            Say(pc, 131, "无法给道具啊!$R;");
                            break;
                        case 2:
                            break;
                    }
                    return;
                }
                Say(pc, 131, "这是『银徽章』啊?$R;" +
                    "$R在艾恩萨乌斯$R;" +
                    "虽然流行$R;" +
                    "『金徽章』…$R;" +
                    "$P金徽章可以兑换的道具$R;" +
                    "我全部都有$R;" +
                    "$R如果是银徽章10个的话$R;" +
                    "就跟金徽章和你换吧!$R;");
                return;
            }
            Say(pc, 131, "欢迎来到艾恩萨乌斯联邦!$R;" +
                "$R艾恩萨乌斯联邦是$R;" +
                "属于阿克罗尼亚混成$R;" +
                "骑士团『南军』管辖$R;" +
                "只有持『艾恩萨乌斯入国许可证』$R;" +
                "才可以入境$R;");
        }
        void 騎士團(ActorPC pc)
        {
            Say(pc, 131, "隶属于阿克罗尼亚混成骑士团$R;" +
                "『南军』?$R;" +
                "$R请在里面办理一下入境手续$R;");
        }
    }
}

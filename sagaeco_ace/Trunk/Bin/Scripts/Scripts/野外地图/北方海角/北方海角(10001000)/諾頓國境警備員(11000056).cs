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
                        "这是『铜徽章』喔?$R;" +
                        "$R在诺森很流行$R;" +
                        "『银徽章』$R;" +
                        "$P已经集齐得到$R;" +
                        "银徽章的道具了…$R;" +
                        "$R如果有10个铜徽章的话$R;" +
                        "也可以交换银徽章$R;");
                    mask.SetValue(BFHJFlags.諾頓國境警備員第一次對話, true);
                    return;
                }
                if (CountItem(pc, 10009500) >= 10)
                {
                    Say(pc, 131, "用10个『铜徽章』$R;" +
                        "交换1个『银徽章』吗?$R;");
                    switch (Select(pc, "交换吗?", "", "交换", "不交唤"))
                    {
                        case 1:

                            if (CheckInventory(pc, 10009600, 1))
                            {
                                TakeItem(pc, 10009500, 10);
                                GiveItem(pc, 10009600, 1);
                                Say(pc, 131, "给10个『铜徽章』$R;" +
                                    "拿到了1个『银徽章』!$R;");
                                Say(pc, 131, "那么，祝你好运!$R;");
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
                    "这是『铜徽章』喔?$R;" +
                    "$R在诺森很流行$R;" +
                    "『银徽章』$R;" +
                    "$P已经集齐得到$R;" +
                    "银徽章的道具了…$R;" +
                    "$R如果有10个铜徽章的话$R;" +
                    "也可以交换银徽章$R;");
                mask.SetValue(BFHJFlags.諾頓國境警備員第一次對話, true);
                return;
            }
            Say(pc, 131, "前方是诺森王国$R;" +
                "不是阿克罗尼亚混成骑士团北军所属团员$R;" +
                "或『诺顿入境许可证』$R;" +
                "持有者的话$R;" +
                "不能入境$R;");
        }
    }
}

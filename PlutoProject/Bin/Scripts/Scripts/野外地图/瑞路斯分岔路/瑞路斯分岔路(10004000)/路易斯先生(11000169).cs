using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10004000
{
    public class S11000169 : Event
    {
        public S11000169()
        {
            this.EventID = 11000169;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<RLSFCLFlags> mask = new BitMask<RLSFCLFlags>(pc.CMask["RLSFCL"]);
            if (!mask.Test(RLSFCLFlags.路易斯先生第一次對話))
            {
                Say(pc, 131, "我的『针织无边帽』帅气吧？$R;" +
                    "合成制作的!$R;" +
                    "$R你想不想挑战看看?$R;");
                switch (Select(pc, "要挑战吗?", "", "好啊!挑战!", "烦……"))
                {
                    case 1:
                        Say(pc, 131, "想要制作编织帽的话$R;" +
                            "就要找『针织布』$R;" +
                            "$P前面的『北极熊』就会掉的$R;" +
                            "$R得到了告诉我吧$R;");
                        mask.SetValue(RLSFCLFlags.路易斯先生第一次對話, true);
                        break;
                    case 2:
                        break;
                }
                return;
            }
            if (!mask.Test(RLSFCLFlags.針織鴨舌帽完成))
            {
                if (CountItem(pc, 50022900) >= 1)
                {
                    Say(pc, 131, "完成了!!挺合适啊?$R;" +
                        "$R制作其他道具看看吗?$R;" +
                        "可以教你各种方法喔$R;");
                    mask.SetValue(RLSFCLFlags.針織鴨舌帽完成, true);
                    return;
                }
                if (CountItem(pc, 10000407) >= 1 && CountItem(pc, 10045900) >= 1)
                {
                    Say(pc, 131, "材料都收集好了!$R;" +
                        "那就去裁缝师阿姨家吧$R;" +
                        "$R在阿克罗波利斯上城里$R;" +
                        "$P选择『拜托裁缝』的话$R;" +
                        "阿姨会马上帮忙制作的$R;" +
                        "$R完成的话也会给我看吧?$R;");
                    return;
                }
                if (CountItem(pc, 10045900) >= 1)
                {
                    Say(pc, 131, "弄到『编织布』的话$R;" +
                        "然后再找『风之香水』$R;" +
                        "$R虽然也可以合成制作$R;" +
                        "但是在商人那里直接购买的话方便又快速$R;" +
                        "$P阿克罗波利斯下城里应该有卖的$R;" +
                        "$R要的话，就去买吧$R;");

                    return;
                }
                Say(pc, 131, "想要制作无边帽的话$R;" +
                    "就要找『针织布』$R;" +
                    "$P前面的『北极熊』就会掉的$R;" +
                    "$R得到了告诉我吧$R;");
                return;
            }
            Say(pc, 131, "我帅气吧？$R;" +
                "$R什么都可以问我!$R;");
            switch (Select(pc, "想问什么呢?", "", "关于武器", "关于帽子", "关于眼镜", "关于衣服", "关于袜子", "关于鞋", "没什么可问的"))
            {
                case 1:
                    Say(pc, 131, "啊？这个？是『贵族魔棒』$R;" +
                        "象征魔力的『紫金色的宝珠』$R;" +
                        "是不是很适合我啊…$R;" +
                        "$P制作这个杖的方法?$R;" +
                        "不能那么轻易的教你$R;" +
                        "$R光看是免费的，你觉得用什么做的?$R;");
                    break;
                case 2:
                    Say(pc, 131, "哦？这是？　是『针织无边帽』喔$R;" +
                        "$R再往前走，那里会到更寒泠的$R;" +
                        "我担心会得感冒，所以…$R;" +
                        "$P虽然说北方海角的商人有卖$R;");
                    break;
                case 3:
                    Say(pc, 131, "啊?这个?是『圆框眼镜』$R;" +
                        "$R虽然不好意思，但讲讲这个眼镜的$R;" +
                        "好处吧!$R;" +
                        "$P这个是用在脸上的饰品$R;" +
                        "$R不会让眼睛变好，也不会提高防御力$R;" +
                        "什么功能都没有!$R;" +
                        "$P虽然是那样，可不是很帅吗?$R;" +
                        "$R来，看看我吧!$R;" +
                        "$P制作圆框眼镜的方法?$R;" +
                        "$R这个眼镜是用鈦金属制的!$R;" +
                        "把这个那个这里加工一下$R;" +
                        "还有的，是秘密!$R;");
                    break;
                case 4:
                    Say(pc, 131, "啊?这个?是『保暖外套』$R;" +
                        "$R再往前走，那里会到更寒泠的$R;" +
                        "我担心会得感冒，所以…$R;" +
                        "$P虽然说北方海角的商人有卖$R;");
                    break;
                case 5:
                    Say(pc, 131, "针织紧身裤袜…非常暖和!$R;" +
                        "北边的商人在卖呢$R;");
                    break;
                case 6:
                    Say(pc, 131, "啊?这个?是『工作用靴子』$R;" +
                        "$R在军舰岛上买的$R;" +
                        "$P问一些更能炫耀的就好了!$R;");
                    break;
                case 7:
                    break;
            }
        }
    }
}

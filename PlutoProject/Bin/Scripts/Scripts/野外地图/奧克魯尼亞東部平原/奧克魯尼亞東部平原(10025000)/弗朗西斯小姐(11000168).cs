using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:奧克魯尼亞東部平原(10025000) NPC基本信息:弗朗西斯小姐(11000168) X:110 Y:135
namespace SagaScript.M10025000
{
    public class S11000168 : Event
    {
        public S11000168()
        {
            this.EventID = 11000168;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<World_01> World_01_mask = new BitMask<World_01>(pc.CMask["World_01"]);

            if (!World_01_mask.Test(World_01.翅膀裝飾合成任務完成))
            {
                翅膀裝飾合成任務(pc);
                return;
            }

            Say(pc, 11000168, 131, "我很可爱吧?$R;" +
                                   "$R什么都可以问我喔!$R;", "弗朗西斯小姐");

            switch (Select(pc, "想问什么呢?", "", "关于「武器」", "关于「盾牌」", "关于「头发装饰」", "关于「衣服」", "关于「鞋子」", "没什么想问的"))
            {
                case 1:
                    Say(pc, 11000168, 131, "啊? 这个是『尖铁棍棒』。$R;" +
                                           "$R怎么样? 还好吧??$R;" +
                                           "看起来非常强悍啊!$R;" +
                                           "$P从商人大叔那里购买的『棍棒』，$R;" +
                                           "$R再钉上『螺丝』和 『布』。$R;" +
                                           "$P可是因为无法自己完成$R;" +
                                           "这么困难的工作。$R;" +
                                           "$R所以只好拜托在「下城」的$R;" +
                                           "「铁匠铺」帮忙制作。$R;" +
                                           "$P『螺丝』在「下城」就有卖了，$R;" +
                                           "『布』是用『棉花』精制而成的。$R;" +
                                           "$R虽然收集材料是困难的事情，$R;" +
                                           "但是可以跟别人炫耀，$R;" +
                                           "所以也会很满足吧。$R;", "弗朗西斯小姐");
                    break;

                case 2:
                    Say(pc, 11000168, 131, "啊? 这个是『木盾牌』。$R;" +
                                           "$R怎么了呢?$R;" +
                                           "$P吊桥上的商人有在卖呢。$R;", "弗朗西斯小姐");
                    break;

                case 3:
                    Say(pc, 11000168, 131, "用「咕咕的翅膀」可以合成很多道具喔!$R;", "弗朗西斯小姐");
                    break;

                case 4:
                    Say(pc, 11000168, 131, "啊? 这个是『工作裤』。$R;" +
                                           "$R怎么样?$R;" +
                                           "这是法伊斯特那边的商人在卖的。$R;" +
                                           "$P听说是在「法伊斯特」很流行的工作服呢?$R;", "弗朗西斯小姐");
                    break;

                case 5:
                    Say(pc, 11000168, 131, "『凉鞋』你不也是一开始就穿了吗?$R;" +
                                           "$R问其他能炫耀的东西吧!$R;", "弗朗西斯小姐");
                    break;

                case 6:
                    break;
            }
        }

        void 翅膀裝飾合成任務(ActorPC pc)
        {
            BitMask<World_01> World_01_mask = new BitMask<World_01>(pc.CMask["World_01"]);

            if (World_01_mask.Test(World_01.翅膀裝飾合成任務開始))
            {
                製作翅膀裝飾(pc);
                return;
            }

            Say(pc, 11000168, 131, "这个『翅膀装饰』不错吧?$R;" +
                                   "这是用合成做成的~!$R;" +
                                   "$R你想不想挑战看看呀?$R;", "弗朗西斯小姐");

            switch (Select(pc, "想要挑战看看吗?", "", "好啊! 挑战!", "讨厌……"))
            {
                case 1:
                    World_01_mask.SetValue(World_01.翅膀裝飾合成任務開始, true);

                    Say(pc, 11000168, 131, "要制作『翅膀装饰』的话，$R;" +
                                           "首先要弄到『咕咕的翅膀』才可以!$R;" +
                                           "$P沿着这条路直走，就能找到「咕咕」了。$R;" +
                                           "$R等拿到羽毛之后，$R;" +
                                           "再回来找我说话好吗?$R;", "弗朗西斯小姐");
                    break;

                case 2:
                    break;
            }
        }

        void 製作翅膀裝飾(ActorPC pc)
        {
            BitMask<World_01> World_01_mask = new BitMask<World_01>(pc.CMask["World_01"]);

            if (!World_01_mask.Test(World_01.翅膀裝飾合成任務完成))
            {
                if (CountItem(pc, 10018208) > 0)
                {
                    Say(pc, 11000168, 131, "得到『咕咕的翅膀』后，$R;" +
                                           "要再去「宝石商」那里。$R;" +
                                           "$R「宝石商店」在$R;" +
                                           "「阿克罗波利斯」的「上城」那里。$R;" +
                                           "$P委托他们帮你「饰品制作」的话，$R;" +
                                           "他们就会马上帮你处理的!$R;", "弗朗西斯小姐");
                }
                else
                {
                    Say(pc, 11000168, 131, "要制作『翅膀装饰』的话，$R;" +
                                           "首先要弄到『咕咕的翅膀』才可以!$R;" +
                                           "$P沿者这条路直走，就能找到「咕咕」了。$R;" +
                                           "$R等拿到羽毛之后，$R;" +
                                           "再回来找我说话好吗?$R;", "弗朗西斯小姐");
                }
            }

            if (World_01_mask.Test(World_01.翅膀裝飾合成任務完成) &&
                CountItem(pc, 50031400) > 0)
            {
                World_01_mask.SetValue(World_01.翅膀裝飾合成任務完成, true);

                Say(pc, 11000168, 131, "嘿嘿~♪完成了~!R;" +
                                       "跟我的一模一样啊!$R;" +
                                       "$R其他的道具也想制作吗?$R;" +
                                       "那我以后就多教你几种吧!$R;", "弗朗西斯小姐");
            }
        }
    }
}

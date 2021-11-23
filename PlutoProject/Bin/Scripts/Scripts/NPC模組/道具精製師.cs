using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;

namespace SagaScript
{
    public abstract class 道具精製師 : Event
    {
        public 道具精製師()
        {

        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<World_01> World_01_mask = new BitMask<World_01>(pc.CMask["World_01"]);

            string free;
            int money;

            if (!World_01_mask.Test(World_01.護髮劑合成任務完成))
            {
                護髮劑合成任務(pc);
                return;
            }

            if (!World_01_mask.Test(World_01.已經與道具精製師進行第一次對話))
            {
                初次與道具精製師進行對話(pc);
                return;
            }

            if (pc.Level < 16)
            {
                free = "免费";
                money = 0;
            }
            else
            {
                free = "100金币";
                money = 100;
            }

            switch (Select(pc, "做什么好呢?", "", "打开宝物箱(" + free + ")", "精制道具", "药品合成", "木材加工", "购买维修工具箱", "放弃"))
            {
                case 1:
                    if (pc.Gold >= money)
                    {
                        pc.Gold -= money;

                        OpenTreasureBox(pc);
                    }
                    else
                    {
                        Say(pc, 131, "资金不足喔!$R;");
                    }
                    break;

                case 2:
                    Synthese(pc, 2009, 4);
                    break;

                case 3:
                    Synthese(pc, 2022, 5);
                    break;

                case 4:
                    Synthese(pc, 2020, 3);
                    break;

                case 5:
                    OpenShopBuy(pc, 6);
                    break;

                case 6:
                    break;
            }
        }

        void 初次與道具精製師進行對話(ActorPC pc)
        {
            BitMask<World_01> World_01_mask = new BitMask<World_01>(pc.CMask["World_01"]);

            int selection;

            World_01_mask.SetValue(World_01.已經與道具精製師進行第一次對話, true);

            Say(pc, 131, "我是「阿克罗波利斯」的$R;" +
                         "「行会评议会」派遣的$R;" +
                         "「道具精炼师」。$R;" +
                         "$P负责处理道具的「精制」和「合成」，$R;" +
                         "除此之外，$R;" +
                         "还有负责「宝物箱」的开启。$R;" +
                         "$R万一有打不开的「宝物箱」，$R;" +
                         "就拿到我这里来吧!$R;", "道具精制师");

            selection = Select(pc, "有什么要问的吗?", "", "「精制」是什么?", "「合成」是什么?", "委托「精制」、「合成」的方法", "「宝物箱」是什么?", "没什么想问的");

            while (selection != 5)
            {
                switch (selection)
                {
                    case 1:
                        Say(pc, 131, "所谓的「精制」，$R;" +
                                     "是将道具中的杂质去掉，$R;" +
                                     "变成更加有用的材料。$R;" +
                                     "$P如果你是「生产系职业」，$R;" +
                                     "总有一天，$R;" +
                                     "自己就能做出来的。$R;" +
                                     "$P此外，$R;" +
                                     "同时也有使「重量」和「体积」$R;" +
                                     "变小的效果，所以想搬运道具的时候，$R;" +
                                     "学会「精制」的话会比较方便喔!$R;", "道具精制师");
                        break;

                    case 2:
                        Say(pc, 131, "所谓的「合成」，$R;" +
                                     "是使用2个以上的道具来$R;" +
                                     "制作出新的道具的意思。$R;" +
                                     "$R我所负责的是$R;" +
                                     "「药品合成」和「木材加工」。$R;" +
                                     "$P「药品合成」是把有药效的$R;" +
                                     "粉末和液体混合在一起，$R;" +
                                     "制作出更好效果的药品的方法。$R;" +
                                     "$R「木材加工」是把木头的原料，$R;" +
                                     "加工成其他形态部件的方法。$R;" +
                                     "$P如果你是「生产系职业」的话，$R;" +
                                     "使用不同技能，$R;" +
                                     "就能进行不同的合成，$R;" +
                                     "靠自己也可以完成的。$R;", "道具精制师");
                        break;

                    case 3:
                        Say(pc, 131, "委托的方法很简单!$R;" +
                                     "$R拿着制作材料的状态下，$R;" +
                                     "跟我说话就可以了。$R;" +
                                     "$P用你拿着的材料，$R;" +
                                     "我会告诉你可以$R;" +
                                     "「精制」或「合成」的秘方。$R;" +
                                     "$R然后自己决定做什么跟做几个后，$R;" +
                                     "再按「ok」就可以了。$R;" +
                                     "$P道具不足的時候，$R;" +
                                     "会告诉你什么不足。$R;" +
                                     "$R所以把那个当做参考就可以!$R;" +
                                     "$P「精制」和「合成」$R;" +
                                     "都是有可能会失败的。$R;" +
                                     "$R为了防止失败，$R;" +
                                     "多准备点预备材料的话，$R;" +
                                     "会比较安心的。$R;", "道具精制师");
                        break;

                    case 4:
                        Say(pc, 131, "所谓的「宝物箱」，$R;" +
                                     "是掉在田野或地牢里，$R;" +
                                     "装有道具的箱子。$R;" +
                                     "$P里面装有各种道具，$R;" +
                                     "但是不打开的话，$R;" +
                                     "谁都不知道里面有什么道具。$R;" +
                                     "$R啊! 不打开也可以卖唷!$R;" +
                                     "$P「宝物箱」有三个种类，$R;" +
                                     "『木箱』、『宝物箱』、『集装箱』。$R;" +
                                     "$R特别是『集装箱』，$R;" +
                                     "是属于机械时代的产物。$R;" +
                                     "$R里面装有叫『发掘品』的贵重道具唷!$R;" +
                                     "$P打开1个「宝物箱」需要「100金币」，$R;" +
                                     "但是针对初心者有特别的奖励!$R;" +
                                     "$R到「LV16」为之前全部免费，$R;" +
                                     "所以尽管拿来吧!$R;", "道具精製師");
                        break;
                }

                selection = Select(pc, "有什么要问的吗?", "", "「精制」是什么?", "「合成」是什么?", "委托「精制」、「合成」的方法", "「宝物箱」是什么?", "没什么想问的");
            }
        }

        void 護髮劑合成任務(ActorPC pc)
        {
            BitMask<World_01> World_01_mask = new BitMask<World_01>(pc.CMask["World_01"]);

            if (!World_01_mask.Test(World_01.護髮劑合成任務開始))
            {
                Say(pc, 131, "你好，我是道具精制师。$R;" +
                             "$R现在有为初心者举办$R;" +
                             "发送道具的赠品活动。$R;" +
                             "$P想不想进行一次合成看看呢?$R;", "道具精制师");

                switch (Select(pc, "怎么做呢?", "", "做做看", "不做"))
                {
                    case 1:
                        if (CheckInventory(pc, 10000604, 1) &&
                            CheckInventory(pc, 10001905, 1) &&
                            CheckInventory(pc, 10035400, 1))
                        {
                            World_01_mask.SetValue(World_01.護髮劑合成任務開始, true);

                            PlaySound(pc, 2040, false, 100, 50);
                            GiveItem(pc, 10000604, 1);
                            GiveItem(pc, 10001905, 1);
                            GiveItem(pc, 10035400, 1);
                            Say(pc, 0, 65535, "得到『蜂蜜』、$R;" +
                                              "『树皮精华素』、$R;" +
                                              "『复活药水』!$R;", " ");

                            Say(pc, 131, "这次要做的是$R;" +
                                         "使头发快速生长的神秘药水$R;" +
                                         "『护发剂』。$R;" +
                                         "$P杀人蜂掉的保湿效果良好的$R;" +
                                         "『蜂蜜』!$R;" +
                                         "$R长在小树上，$R;" +
                                         "让头发变得柔顺有光泽的$R;" +
                                         "『树皮精华素』!$R;" +
                                         "$P还有让死去毛孔活起来的$R;" +
                                         "『复活药水』!$R;" +
                                         "$P把这些道具用「药品合成」技能，$R;" +
                                         "合成看看吧?$R;", "道具精制师");

                            switch (Select(pc, "怎么做呢？", "", "做做看", "只收道具"))
                            {
                                case 1:
                                    Synthese(pc, 2022, 5);
                                    break;

                                case 2:
                                    World_01_mask.SetValue(World_01.護髮劑合成任務完成, true);

                                    pc.Fame = pc.Fame - 1;

                                    Say(pc, 131, "太过分了…QAQ!$R;", "道具精制师");
                                    break;
                            }
                        }
                        else
                        {
                            Say(pc, 131, "嗯?$R;" +
                                         "$R无法给您道具啊!$R;" +
                                         "把身上的东西减少后再来吧!$R;", "道具精制师");
                        }
                        break;
                        
                    case 2:
                        World_01_mask.SetValue(World_01.護髮劑合成任務完成, true);
                        break;
                }
            }
            else
            {
                World_01_mask.SetValue(World_01.護髮劑合成任務完成, true);

                Say(pc, 131, "都好了，要看一下嗎?$R;" +
                             "$R以这个方式合成的话，$R;" +
                             "可以制作出有意思的东西。$R;" +
                             "$R你也体验一下各种合成吧!$R;", "道具精制师");
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:上城(10023000) NPC基本信息:撒帕涅(11000508) X:201 Y:129
namespace SagaScript.M10023000
{
    public class S11000508 : Event
    {
        public S11000508()
        {
            this.EventID = 11000508;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<LV35_Clothes_03> LV35_Clothes_03_mask = new BitMask<LV35_Clothes_03>(pc.CMask["LV35_Clothes_03"]);

            BitMask<DFHJFlags> mask = new BitMask<DFHJFlags>(pc.CMask["DFHJ"]);

            if (mask.Test(DFHJFlags.寻找撒帕涅开始) && !mask.Test(DFHJFlags.已找到撒帕涅))
            {
                mask.SetValue(DFHJFlags.已找到撒帕涅, true);
                Say(pc, 131, "什么？$R;");
                ShowEffect(pc, 11000508, 4501);
                Say(pc, 131, "嗯?!$R;" +
                    "你说柳在东国等我?$R;");
                ShowEffect(pc, 11000508, 4508);
                Say(pc, 131, "啊啊原来是我让弟弟在等我的!!$R;" +
                    "$R忘得一干二净了$R;" +
                    "在都市到处逛实在太有意思了…$R;" +
                    "$P哦？知道了!$R;" +
                    "请转告给弟弟我马上就去~$R;");
            }
            if (!LV35_Clothes_03_mask.Test(LV35_Clothes_03.撒帕涅的委託完成))
            {
                撒帕涅的委託(pc);
                return;
            }

            Say(pc, 11000508, 131, "手工的洋服怎么样?$R;" +
                                   "可是我精心制作的呢!$R;", "撒帕涅");

            switch (Select(pc, "想要买什么呢?", "", "用布做的防具", "什么也不买"))
            {
                case 1:
                    OpenShopBuy(pc, 57);
                    break;

                case 2:
                    break;
            }
        }

        void 撒帕涅的委託(ActorPC pc)
        {
            if (pc.CInt["LV35_Clothes_03"] == 0)
            {
                撒帕涅告知缺少的材料(pc);
                return;
            }
            else
            {
                把材料交給撒帕涅(pc);
                return;
            }
        }

        void 撒帕涅告知缺少的材料(ActorPC pc)
        {
            int selection;

            if (pc.Level < 20)
            {
                Say(pc, 11000508, 131, "啊啊…太…太漂亮了!!$R;" +
                                       "$R街上经过的都是美丽的人…$R;" +
                                       "还是「阿高普路斯市」有点不同啊!$R;" +
                                       "$R我很喜欢打扮。$R;" +
                                       "$R今天是为了制作新的洋服，$R;" +
                                       "所以来买洋服材料的，$R;" +
                                       "不过很难找到啊!$R;", "撒帕涅");

                selection = Global.Random.Next(1, 6);

                pc.CInt["LV35_Clothes_03"] = selection;

                switch (pc.CInt["LV35_Clothes_03"])
                {
                    case 1:
                        Say(pc, 11000508, 131, "这个真困难啊?!$R;" +
                                               "$R如果有60匹『布』的话，$R;" +
                                               "可以让给我吗?$R;", "撒帕涅");
                        break;

                    case 2:
                        Say(pc, 11000508, 131, "这个真困难啊?!$R;" +
                                               "$R如果有3份『蓝色粉末』的话，$R;" +
                                               "可以让给我吗?$R;", "撒帕涅");
                        break;

                    case 3:
                        Say(pc, 11000508, 131, "这个真困难啊?!$R;" +
                                               "$R如果有3份『浅绿粉末』的话，$R;" +
                                               "可以让给我吗?$R;", "撒帕涅");
                        break;

                    case 4:
                        Say(pc, 11000508, 131, "这个真困难啊?!$R;" +
                                               "$R如果有3份『橙色粉末』的话，$R;" +
                                               "可以让给我吗?$R;", "撒帕涅");
                        break;

                    case 5:
                        Say(pc, 11000508, 131, "这个真困难啊?!$R;" +
                                               "$R如果有60朵『花』的话，$R;" +
                                               "可以让给我吗?$R;", "撒帕涅");
                        break;

                    case 6:
                        Say(pc, 11000508, 131, "这个真困难啊?!$R;" +
                                               "$R如果有12枝『针』的话，$R;" +
                                               "可以让给我吗?$R;", "撒帕涅");
                        break;
                }
            }
            else
            {
                Say(pc, 11000508, 131, "啊啊…太…太漂亮了!!$R;" +
                                       "$R街上经过的都是美丽的人…$R;" +
                                       "还是「阿克罗波利斯」有点不同啊!$R;" +
                                       "$R我很喜欢打扮。$R;" +
                                       "$R今天是为了制作新的洋服，$R;" +
                                       "所以来买洋服材料的，$R;" +
                                       "不过很难找到啊!$R;", "撒帕涅");

                selection = Global.Random.Next(7, 8);

                pc.CInt["LV35_Clothes_03"] = selection;

                switch (pc.CInt["LV35_Clothes_03"])
                {
                    case 7:
                        Say(pc, 11000508, 131, "这个真困难啊?!$R;" +
                                               "$R如果有12束『花束』的话，$R;" +
                                               "可以让给我吗?$R;", "撒帕涅");
                        break;

                    case 8:
                        Say(pc, 11000508, 131, "这个真困难啊?!$R;" +
                                               "$R如果有15件『皮革』的话，$R;" +
                                               "可以让给我吗?$R;", "撒帕涅");
                        break;
                }
            }
        }

        void 把材料交給撒帕涅(ActorPC pc)
        {
            BitMask<LV35_Clothes_03> LV35_Clothes_03_mask = new BitMask<LV35_Clothes_03>(pc.CMask["LV35_Clothes_03"]);

            switch (pc.CInt["LV35_Clothes_03"])
            {
                case 1:
                    if (CountItem(pc, 10021300) >= 60)
                    {
                        LV35_Clothes_03_mask.SetValue(LV35_Clothes_03.撒帕涅的委託完成, true);

                        PlaySound(pc, 2040, false, 100, 50);
                        TakeItem(pc, 10021300, 60);
                        Say(pc, 0, 0, "把『布』交给她。$R;", " ");

                        Say(pc, 11000508, 131, "啊啊…真是非常感谢啊!$R;" +
                                               "真不知说什么才好…$R;" +
                                               "$P如果来「法伊斯特」的话，$R;" +
                                               "一定要来找我啊!$R;" +
                                               "我会帮您做些好东西的。$R;" +
                                               "$R约定好了啊!!$R;", "撒帕涅");
                    }
                    else
                    {
                        Say(pc, 11000508, 131, "这个真困难啊?!$R;" +
                                               "$R如果有60匹『布』的话，$R;" +
                                               "可以让给我吗?$R;", "撒帕涅");
                    }
                    break;

                case 2:
                    if (CountItem(pc, 10001103) >= 3)
                    {
                        LV35_Clothes_03_mask.SetValue(LV35_Clothes_03.撒帕涅的委託完成, true);

                        PlaySound(pc, 2040, false, 100, 50);
                        TakeItem(pc, 10001103, 3);
                        Say(pc, 0, 0, "把『蓝色粉末』交给她。$R;", " ");

                        Say(pc, 11000508, 131, "啊啊…真是非常感谢啊!$R;" +
                                               "真不知说什么才好…$R;" +
                                               "$P如果来「法伊斯特」的话，$R;" +
                                               "一定要来找我啊!$R;" +
                                               "我会帮您做些好东西的。$R;" +
                                               "$R约定好了啊!!$R;", "撒帕涅");
                    }
                    else
                    {
                        Say(pc, 11000508, 131, "这个真困难啊?!$R;" +
                                               "$R如果有3份『蓝色粉末』的话，$R;" +
                                               "可以让给我吗?$R;", "撒帕涅");
                    }
                    break;

                case 3:
                    if (CountItem(pc, 10001106) >= 3)
                    {
                        LV35_Clothes_03_mask.SetValue(LV35_Clothes_03.撒帕涅的委託完成, true);

                        PlaySound(pc, 2040, false, 100, 50);
                        TakeItem(pc, 10001106, 3);
                        Say(pc, 0, 0, "把『浅绿粉末』交给她。$R;", " ");

                        Say(pc, 11000508, 131, "啊啊…真是非常感谢啊!$R;" +
                                               "真不知说什么才好…$R;" +
                                               "$P如果来「法伊斯特」的话，$R;" +
                                               "一定要来找我啊!$R;" +
                                               "我会帮您做些好东西的。$R;" +
                                               "$R约定好了啊!!$R;", "撒帕涅");
                    }
                    else
                    {
                        Say(pc, 11000508, 131, "这个真困难啊?!$R;" +
                                               "$R如果有3份『浅绿粉末』的话，$R;" +
                                               "可以让给我吗?$R;", "撒帕涅");
                    }
                    break;

                case 4:
                    if (CountItem(pc, 10001108) >= 3)
                    {
                        LV35_Clothes_03_mask.SetValue(LV35_Clothes_03.撒帕涅的委託完成, true);

                        PlaySound(pc, 2040, false, 100, 50);
                        TakeItem(pc, 10001108, 3);
                        Say(pc, 0, 0, "把『橙色粉末』交给她。$R;", " ");

                        Say(pc, 11000508, 131, "啊啊…真是非常感谢啊!$R;" +
                                               "真不知说什么才好…$R;" +
                                               "$P如果来「法伊斯特」的话，$R;" +
                                               "一定要来找我啊!$R;" +
                                               "我会帮您做些好东西的。$R;" +
                                               "$R约定好了啊!!$R;", "撒帕涅");
                    }
                    else
                    {
                        Say(pc, 11000508, 131, "这个真困难啊?!$R;" +
                                               "$R如果有3份『橙色粉末』的话，$R;" +
                                               "可以让给我吗?$R;", "撒帕涅");
                    }
                    break;

                case 5:
                    if (CountItem(pc, 10005300) >= 60)
                    {
                        LV35_Clothes_03_mask.SetValue(LV35_Clothes_03.撒帕涅的委託完成, true);

                        PlaySound(pc, 2040, false, 100, 50);
                        TakeItem(pc, 10005300, 60);
                        Say(pc, 0, 0, "把『花』交给她。$R;", " ");

                        Say(pc, 11000508, 131, "啊啊…真是非常感谢啊!$R;" +
                                               "真不知说什么才好…$R;" +
                                               "$P如果来「法伊斯特」的话，$R;" +
                                               "一定要来找我啊!$R;" +
                                               "我会帮您做些好东西的。$R;" +
                                               "$R约定好了啊!!$R;", "撒帕涅");
                    }
                    else
                    {
                        Say(pc, 11000508, 131, "这个真困难啊?!$R;" +
                                               "$R如果有60朵『花』的话，$R;" +
                                               "可以让给我吗?$R;", "撒帕涅");
                    }
                    break;

                case 6:
                    if (CountItem(pc, 10019700) >= 12)
                    {
                        LV35_Clothes_03_mask.SetValue(LV35_Clothes_03.撒帕涅的委託完成, true);

                        PlaySound(pc, 2040, false, 100, 50);
                        TakeItem(pc, 10019700, 12);
                        Say(pc, 0, 0, "把『针』交给她。$R;", " ");

                        Say(pc, 11000508, 131, "啊啊…真是非常感谢啊!$R;" +
                                               "真不知说什么才好…$R;" +
                                               "$P如果来「法伊斯特」的话，$R;" +
                                               "一定要来找我啊!$R;" +
                                               "我会帮您做些好东西的。$R;" +
                                               "$R约定好了啊!!$R;", "撒帕涅");
                    }
                    else
                    {
                        Say(pc, 11000508, 131, "这个真困难啊?!$R;" +
                                               "$R如果有12枝『针』的话，$R;" +
                                               "可以让给我吗?$R;", "撒帕涅");
                    }
                    break;

                case 7:
                    if (CountItem(pc, 10005400) >= 10)
                    {
                        LV35_Clothes_03_mask.SetValue(LV35_Clothes_03.撒帕涅的委託完成, true);

                        PlaySound(pc, 2040, false, 100, 50);
                        TakeItem(pc, 10005400, 10);
                        Say(pc, 0, 0, "把『花束』交给她。$R;", " ");

                        Say(pc, 11000508, 131, "啊啊…真是非常感谢啊!$R;" +
                                               "真不知说什么才好…$R;" +
                                               "$P如果来「法伊斯特」的话，$R;" +
                                               "一定要来找我啊!$R;" +
                                               "我会帮您做些好东西的。$R;" +
                                               "$R约定好了啊!!$R;", "撒帕涅");
                    }
                    else
                    {
                        Say(pc, 11000508, 131, "这个真困难啊?!$R;" +
                                               "$R如果有12束『花束』的话，$R;" +
                                               "可以让给我吗?$R;", "撒帕涅");
                    }
                    break;

                case 8:
                    if (CountItem(pc, 10020300) >= 15)
                    {
                        LV35_Clothes_03_mask.SetValue(LV35_Clothes_03.撒帕涅的委託完成, true);

                        PlaySound(pc, 2040, false, 100, 50);
                        TakeItem(pc, 10020300, 15);
                        Say(pc, 0, 0, "把『皮革』交给她。$R;", " ");

                        Say(pc, 11000508, 131, "啊啊…真是非常感谢啊!$R;" +
                                               "真不知说什么才好…$R;" +
                                               "$P如果来「法伊斯特」的话，$R;" +
                                               "一定要来找我啊!$R;" +
                                               "我会帮您做些好东西的。$R;" +
                                               "$R约定好了啊!!$R;", "撒帕涅");
                    }
                    else
                    {
                        Say(pc, 11000508, 131, "这个真困难啊?!$R;" +
                                               "$R如果有15件『皮革』的话，$R;" +
                                               "可以让给我吗?$R;", "撒帕涅");
                    }
                    break;
            }
        }
    }
}

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
                Say(pc, 131, "什麼？$R;");
                ShowEffect(pc, 11000508, 4501);
                Say(pc, 131, "嗯?!$R;" +
                    "你說柳在東域等我?$R;");
                ShowEffect(pc, 11000508, 4508);
                Say(pc, 131, "啊啊原來是我讓弟弟在等我的!!$R;" +
                    "$R忘得一乾二淨了$R;" +
                    "在都市到處逛實在太有意思了…$R;" +
                    "$P哦？知道了!$R;" +
                    "請轉告給弟弟我馬上就去~$R;");
            }
            if (!LV35_Clothes_03_mask.Test(LV35_Clothes_03.撒帕涅的委託完成))
            {
                撒帕涅的委託(pc);
                return;
            }

            Say(pc, 11000508, 131, "手工的洋服怎麼樣?$R;" +
                                   "可是我精心製作的呢!$R;", "撒帕涅");

            switch (Select(pc, "想要買什麼呢?", "", "用布做的防具", "什麼也不買"))
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
                                       "$R街上經過的都是美麗的人…$R;" +
                                       "還是「阿高普路斯市」有點不同啊!$R;" +
                                       "$R我很喜歡打扮。$R;" +
                                       "$R今天是為了製作新的洋服，$R;" +
                                       "所以來買洋服材料的，$R;" +
                                       "不過很難找到啊!$R;", "撒帕涅");

                selection = Global.Random.Next(1, 6);

                pc.CInt["LV35_Clothes_03"] = selection;

                switch (pc.CInt["LV35_Clothes_03"])
                {
                    case 1:
                        Say(pc, 11000508, 131, "這個真困難啊?!$R;" +
                                               "$R如果有60匹『布』的話，$R;" +
                                               "可以讓給我嗎?$R;", "撒帕涅");
                        break;

                    case 2:
                        Say(pc, 11000508, 131, "這個真困難啊?!$R;" +
                                               "$R如果有3份『藍色粉末』的話，$R;" +
                                               "可以讓給我嗎?$R;", "撒帕涅");
                        break;

                    case 3:
                        Say(pc, 11000508, 131, "這個真困難啊?!$R;" +
                                               "$R如果有3份『淺綠色粉末』的話，$R;" +
                                               "可以讓給我嗎?$R;", "撒帕涅");
                        break;

                    case 4:
                        Say(pc, 11000508, 131, "這個真困難啊?!$R;" +
                                               "$R如果有3份『橙色粉末』的話，$R;" +
                                               "可以讓給我嗎?$R;", "撒帕涅");
                        break;

                    case 5:
                        Say(pc, 11000508, 131, "這個真困難啊?!$R;" +
                                               "$R如果有60朵『花』的話，$R;" +
                                               "可以讓給我嗎?$R;", "撒帕涅");
                        break;

                    case 6:
                        Say(pc, 11000508, 131, "這個真困難啊?!$R;" +
                                               "$R如果有12枝『針』的話，$R;" +
                                               "可以讓給我嗎?$R;", "撒帕涅");
                        break;
                }
            }
            else
            {
                Say(pc, 11000508, 131, "啊啊…太…太漂亮了!!$R;" +
                                       "$R街上經過的都是美麗的人…$R;" +
                                       "還是「阿高普路斯市」有點不同啊!$R;" +
                                       "$R我很喜歡打扮。$R;" +
                                       "$R今天是為了製作新的洋服，$R;" +
                                       "所以來買洋服材料的，$R;" +
                                       "不過很難找到啊!$R;", "撒帕涅");

                selection = Global.Random.Next(7, 8);

                pc.CInt["LV35_Clothes_03"] = selection;

                switch (pc.CInt["LV35_Clothes_03"])
                {
                    case 7:
                        Say(pc, 11000508, 131, "這個真困難啊?!$R;" +
                                               "$R如果有12束『花束』的話，$R;" +
                                               "可以讓給我嗎?$R;", "撒帕涅");
                        break;

                    case 8:
                        Say(pc, 11000508, 131, "這個真困難啊?!$R;" +
                                               "$R如果有15件『皮革』的話，$R;" +
                                               "可以讓給我嗎?$R;", "撒帕涅");
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
                        Say(pc, 0, 0, "把『布』交給她。$R;", " ");

                        Say(pc, 11000508, 131, "啊啊…真是非常感謝啊!$R;" +
                                               "真不知說什麼才好…$R;" +
                                               "$P如果來「帕斯特」的話，$R;" +
                                               "一定要來找我啊!$R;" +
                                               "我會幫您做些好東西的。$R;" +
                                               "$R約定好了啊!!$R;", "撒帕涅");
                    }
                    else
                    {
                        Say(pc, 11000508, 131, "這個真困難啊?!$R;" +
                                               "$R如果有60匹『布』的話，$R;" +
                                               "可以讓給我嗎?$R;", "撒帕涅");
                    }
                    break;

                case 2:
                    if (CountItem(pc, 10001103) >= 3)
                    {
                        LV35_Clothes_03_mask.SetValue(LV35_Clothes_03.撒帕涅的委託完成, true);

                        PlaySound(pc, 2040, false, 100, 50);
                        TakeItem(pc, 10001103, 3);
                        Say(pc, 0, 0, "把『藍色粉末』交給她。$R;", " ");

                        Say(pc, 11000508, 131, "啊啊…真是非常感謝啊!$R;" +
                                               "真不知說什麼才好…$R;" +
                                               "$P如果來「帕斯特」的話，$R;" +
                                               "一定要來找我啊!$R;" +
                                               "我會幫您做些好東西的。$R;" +
                                               "$R約定好了啊!!$R;", "撒帕涅");
                    }
                    else
                    {
                        Say(pc, 11000508, 131, "這個真困難啊?!$R;" +
                                               "$R如果有3份『藍色粉末』的話，$R;" +
                                               "可以讓給我嗎?$R;", "撒帕涅");
                    }
                    break;

                case 3:
                    if (CountItem(pc, 10001106) >= 3)
                    {
                        LV35_Clothes_03_mask.SetValue(LV35_Clothes_03.撒帕涅的委託完成, true);

                        PlaySound(pc, 2040, false, 100, 50);
                        TakeItem(pc, 10001106, 3);
                        Say(pc, 0, 0, "把『淺綠色粉末』交給她。$R;", " ");

                        Say(pc, 11000508, 131, "啊啊…真是非常感謝啊!$R;" +
                                               "真不知說什麼才好…$R;" +
                                               "$P如果來「帕斯特」的話，$R;" +
                                               "一定要來找我啊!$R;" +
                                               "我會幫您做些好東西的。$R;" +
                                               "$R約定好了啊!!$R;", "撒帕涅");
                    }
                    else
                    {
                        Say(pc, 11000508, 131, "這個真困難啊?!$R;" +
                                               "$R如果有3份『淺綠色粉末』的話，$R;" +
                                               "可以讓給我嗎?$R;", "撒帕涅");
                    }
                    break;

                case 4:
                    if (CountItem(pc, 10001108) >= 3)
                    {
                        LV35_Clothes_03_mask.SetValue(LV35_Clothes_03.撒帕涅的委託完成, true);

                        PlaySound(pc, 2040, false, 100, 50);
                        TakeItem(pc, 10001108, 3);
                        Say(pc, 0, 0, "把『橙色粉末』交給她。$R;", " ");

                        Say(pc, 11000508, 131, "啊啊…真是非常感謝啊!$R;" +
                                               "真不知說什麼才好…$R;" +
                                               "$P如果來「帕斯特」的話，$R;" +
                                               "一定要來找我啊!$R;" +
                                               "我會幫您做些好東西的。$R;" +
                                               "$R約定好了啊!!$R;", "撒帕涅");
                    }
                    else
                    {
                        Say(pc, 11000508, 131, "這個真困難啊?!$R;" +
                                               "$R如果有3份『橙色粉末』的話，$R;" +
                                               "可以讓給我嗎?$R;", "撒帕涅");
                    }
                    break;

                case 5:
                    if (CountItem(pc, 10005300) >= 60)
                    {
                        LV35_Clothes_03_mask.SetValue(LV35_Clothes_03.撒帕涅的委託完成, true);

                        PlaySound(pc, 2040, false, 100, 50);
                        TakeItem(pc, 10005300, 60);
                        Say(pc, 0, 0, "把『花』交給她。$R;", " ");

                        Say(pc, 11000508, 131, "啊啊…真是非常感謝啊!$R;" +
                                               "真不知說什麼才好…$R;" +
                                               "$P如果來「帕斯特」的話，$R;" +
                                               "一定要來找我啊!$R;" +
                                               "我會幫您做些好東西的。$R;" +
                                               "$R約定好了啊!!$R;", "撒帕涅");
                    }
                    else
                    {
                        Say(pc, 11000508, 131, "這個真困難啊?!$R;" +
                                               "$R如果有60朵『花』的話，$R;" +
                                               "可以讓給我嗎?$R;", "撒帕涅");
                    }
                    break;

                case 6:
                    if (CountItem(pc, 10019700) >= 12)
                    {
                        LV35_Clothes_03_mask.SetValue(LV35_Clothes_03.撒帕涅的委託完成, true);

                        PlaySound(pc, 2040, false, 100, 50);
                        TakeItem(pc, 10019700, 12);
                        Say(pc, 0, 0, "把『針』交給她。$R;", " ");

                        Say(pc, 11000508, 131, "啊啊…真是非常感謝啊!$R;" +
                                               "真不知說什麼才好…$R;" +
                                               "$P如果來「帕斯特」的話，$R;" +
                                               "一定要來找我啊!$R;" +
                                               "我會幫您做些好東西的。$R;" +
                                               "$R約定好了啊!!$R;", "撒帕涅");
                    }
                    else
                    {
                        Say(pc, 11000508, 131, "這個真困難啊?!$R;" +
                                               "$R如果有12枝『針』的話，$R;" +
                                               "可以讓給我嗎?$R;", "撒帕涅");
                    }
                    break;

                case 7:
                    if (CountItem(pc, 10005400) >= 10)
                    {
                        LV35_Clothes_03_mask.SetValue(LV35_Clothes_03.撒帕涅的委託完成, true);

                        PlaySound(pc, 2040, false, 100, 50);
                        TakeItem(pc, 10005400, 10);
                        Say(pc, 0, 0, "把『花束』交給她。$R;", " ");

                        Say(pc, 11000508, 131, "啊啊…真是非常感謝啊!$R;" +
                                               "真不知說什麼才好…$R;" +
                                               "$P如果來「帕斯特」的話，$R;" +
                                               "一定要來找我啊!$R;" +
                                               "我會幫您做些好東西的。$R;" +
                                               "$R約定好了啊!!$R;", "撒帕涅");
                    }
                    else
                    {
                        Say(pc, 11000508, 131, "這個真困難啊?!$R;" +
                                               "$R如果有12束『花束』的話，$R;" +
                                               "可以讓給我嗎?$R;", "撒帕涅");
                    }
                    break;

                case 8:
                    if (CountItem(pc, 10020300) >= 15)
                    {
                        LV35_Clothes_03_mask.SetValue(LV35_Clothes_03.撒帕涅的委託完成, true);

                        PlaySound(pc, 2040, false, 100, 50);
                        TakeItem(pc, 10020300, 15);
                        Say(pc, 0, 0, "把『皮革』交給她。$R;", " ");

                        Say(pc, 11000508, 131, "啊啊…真是非常感謝啊!$R;" +
                                               "真不知說什麼才好…$R;" +
                                               "$P如果來「帕斯特」的話，$R;" +
                                               "一定要來找我啊!$R;" +
                                               "我會幫您做些好東西的。$R;" +
                                               "$R約定好了啊!!$R;", "撒帕涅");
                    }
                    else
                    {
                        Say(pc, 11000508, 131, "這個真困難啊?!$R;" +
                                               "$R如果有15件『皮革』的話，$R;" +
                                               "可以讓給我嗎?$R;", "撒帕涅");
                    }
                    break;
            }
        }
    }
}

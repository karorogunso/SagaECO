using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10034000
{
    public class S11000517 : Event
    {
        public S11000517()
        {
            this.EventID = 11000517;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<JobBasic_03> JobBasic_03_mask = new BitMask<JobBasic_03>(pc.CMask["JobBasic_03"]);

            BitMask<Job2X_03> mask = pc.CMask["Job2X_03"];

            if (mask.Test(Job2X_03.第三個問題回答錯誤))//_4A06)
            {
                if (mask.Test(Job2X_03.第二個問題回答正確) && CountItem(pc, 10000350) >= 1)
                {
                    mask.SetValue(Job2X_03.第三個問題回答錯誤, false);
                    //_4A06 = false;
                    Say(pc, 131, "朋友们!尽最大的努力吧!$R;" +
                        "$P我能给的提示是『是吧就那样』$R;");
                    return;
                }
                if (CheckInventory(pc, 10000350, 1))
                {
                    GiveItem(pc, 10000350, 1);
                    Say(pc, 131, "得到了『暗杀者的秘药2』1个!$R;");
                    mask.SetValue(Job2X_03.未獲得暗殺者的內服藥2, false);
                    mask.SetValue(Job2X_03.第二個問題回答正確, true);
                    mask.SetValue(Job2X_03.第三個問題回答錯誤, false);
                    //_4A70 = false;
                    //_4A02 = true;
                    //_4A06 = false;
                    Say(pc, 131, "朋友们!尽最大的努力吧!$R;" +
                        "$P我能给的提示是『是吧就那样』$R;");
                    return;
                }
                mask.SetValue(Job2X_03.未獲得暗殺者的內服藥2, true);
                //_4A70 = true;
                Say(pc, 131, "您的行李太多了$R;" +
                    "无法给您$R;");
                return;
            }
            if (mask.Test(Job2X_03.第二個問題回答正確))//_4A02)
            {
                if (mask.Test(Job2X_03.第二個問題回答正確) && CountItem(pc, 10000350) >= 1)
                {
                    mask.SetValue(Job2X_03.第三個問題回答錯誤, false);
                    //_4A06 = false;
                    Say(pc, 131, "朋友们!尽最大的努力吧!$R;" +
                        "$P我能给的提示是『是吧就那样』$R;");
                    return;
                }
                if (CheckInventory(pc, 10000350, 1))
                {
                    GiveItem(pc, 10000350, 1);
                    Say(pc, 131, "得到了『暗杀者的秘药2』1个!$R;");
                    mask.SetValue(Job2X_03.未獲得暗殺者的內服藥2, false);
                    mask.SetValue(Job2X_03.第二個問題回答正確, true);
                    mask.SetValue(Job2X_03.第三個問題回答錯誤, false);
                    //_4A70 = false;
                    //_4A02 = true;
                    //_4A06 = false;
                    Say(pc, 131, "朋友们!尽最大的努力吧!$R;" +
                        "$P我能给的提示是『是吧就那样』$R;");
                    return;
                }
                mask.SetValue(Job2X_03.未獲得暗殺者的內服藥2, true);
                //_4A70 = true;
                Say(pc, 131, "您的行李太多了$R;" +
                    "无法给您$R;");
                return;
            }
            if (mask.Test(Job2X_03.未獲得暗殺者的內服藥2))//_4A70)
            {
                if (CheckInventory(pc, 10000350, 1))
                {
                    GiveItem(pc, 10000350, 1);
                    Say(pc, 131, "得到了『暗杀者的秘药2』1个!$R;");
                    mask.SetValue(Job2X_03.未獲得暗殺者的內服藥2, false);
                    mask.SetValue(Job2X_03.第二個問題回答正確, true);
                    mask.SetValue(Job2X_03.第三個問題回答錯誤, false);
                    //_4A70 = false;
                    //_4A02 = true;
                    //_4A06 = false;
                    Say(pc, 131, "朋友们!尽最大的努力吧!$R;" +
                        "$P我能给的提示是『是吧就那样』$R;");
                    return;
                }
                mask.SetValue(Job2X_03.未獲得暗殺者的內服藥2, true);
                //_4A70 = true;
                Say(pc, 131, "您的行李太多了$R;" +
                    "无法给您$R;");
                return;
            }
            if (mask.Test(Job2X_03.第二個問題回答錯誤))//_4A05)
            {
                Say(pc, 131, "重新听一次提示后$R;" +
                    "再来吧$R;");
                return;
            }
            if (mask.Test(Job2X_03.第一個問題回答正確))//_4A01)
            {
                Say(pc, 131, "这里是盗贼的考场$R;" +
                    "$R最好不要帮助测试中的人$R;" +
                    "$P……$R;" +
                    "$P但是其他的事情$R;" +
                    "你对咖喱怎么想?$R;");
                switch (Select(pc, "怎么做呢？", "", "我不怎么喜欢咖喱", "吃的!", "喝的!"))
                {
                    case 1:
                        mask.SetValue(Job2X_03.第二個問題回答錯誤, true);
                        //_4A05 = true;
                        Say(pc, 131, "赶紧回去$R;");
                        break;
                    case 2:
                        mask.SetValue(Job2X_03.第二個問題回答錯誤, true);
                        //_4A05 = true;
                        Say(pc, 131, "差得远着呢…$R;");
                        break;
                    case 3:
                        Say(pc, 131, "是吧?!$R;" +
                            "我太喜欢咖喱了!!$R;" +
                            "$R天天吃咖喱也不会讨厌!$R;" +
                            "还不如说是喝掉……$R;" +
                            "$R嗯…$R;" +
                            "$P那说一下提示吧$R;" +
                            "$R提示是『是吧就那样』$R;" +
                            "可以猜到吧$R;" +
                            "$P来拿着这个走吧$R;");
                        if (CheckInventory(pc, 10000350, 1))
                        {
                            GiveItem(pc, 10000350, 1);
                            Say(pc, 131, "得到了『暗杀者的秘药2』1个!$R;");
                            mask.SetValue(Job2X_03.未獲得暗殺者的內服藥2, false);
                            mask.SetValue(Job2X_03.第二個問題回答正確, true);
                            mask.SetValue(Job2X_03.第三個問題回答錯誤, false);
                            //_4A70 = false;
                            //_4A02 = true;
                            //_4A06 = false;
                            Say(pc, 131, "朋友们!尽最大的努力吧!$R;" +
                                "$P我能给的提示是『是吧就那样』$R;");
                            return;
                        }
                        mask.SetValue(Job2X_03.未獲得暗殺者的內服藥2, true);
                        //_4A70 = true;
                        Say(pc, 131, "您的行李太多了$R;" +
                            "无法给您$R;");
                        break;
                }
                return;
            }
            if (mask.Test(Job2X_03.刺客轉職開始))//_4A00)
            {
                Say(pc, 131, "什么?是暗号?$R;" +
                    "$R你在说什么啊?$R;");
                return;
            }

            if (JobBasic_03_mask.Test(JobBasic_03.選擇轉職為盜賊))
            {
                Say(pc, 131, "要放弃做盗贼?$R;" +
                   "$R嗯…现在放弃虽然也可以$R;" +
                   "再次挑战…$R;");
                switch (Select(pc, "怎么做呢？", "", "不放弃!", "放弃…"))
                {
                    case 1:
                        break;
                    case 2:
                        JobBasic_03_mask.SetValue(JobBasic_03.選擇轉職為盜賊, false);

                        SetHomePoint(pc, 10023400, 124, 3);

                        Warp(pc, 10023400, 124, 3);
                        break;
                }
                return;
            }
            Say(pc, 131, "这里是盗贼的考场$R;" +
                "$R最好不要帮助$R;" +
                "测试中的人$R;");
        }
    }
}
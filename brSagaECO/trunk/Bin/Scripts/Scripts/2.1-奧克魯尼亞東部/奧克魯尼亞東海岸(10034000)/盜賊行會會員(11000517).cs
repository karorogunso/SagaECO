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
                    Say(pc, 131, "朋友們!盡最大的努力吧!$R;" +
                        "$P我能給的提示是『是吧就那樣』$R;");
                    return;
                }
                if (CheckInventory(pc, 10000350, 1))
                {
                    GiveItem(pc, 10000350, 1);
                    Say(pc, 131, "得到了『暗殺者的內服藥2』1個!$R;");
                    mask.SetValue(Job2X_03.未獲得暗殺者的內服藥2, false);
                    mask.SetValue(Job2X_03.第二個問題回答正確, true);
                    mask.SetValue(Job2X_03.第三個問題回答錯誤, false);
                    //_4A70 = false;
                    //_4A02 = true;
                    //_4A06 = false;
                    Say(pc, 131, "朋友們!盡最大的努力吧!$R;" +
                        "$P我能給的提示是『是吧就那樣』$R;");
                    return;
                }
                mask.SetValue(Job2X_03.未獲得暗殺者的內服藥2, true);
                //_4A70 = true;
                Say(pc, 131, "您的行李太多了$R;" +
                    "無法給您$R;");
                return;
            }
            if (mask.Test(Job2X_03.第二個問題回答正確))//_4A02)
            {
                if (mask.Test(Job2X_03.第二個問題回答正確) && CountItem(pc, 10000350) >= 1)
                {
                    mask.SetValue(Job2X_03.第三個問題回答錯誤, false);
                    //_4A06 = false;
                    Say(pc, 131, "朋友們!盡最大的努力吧!$R;" +
                        "$P我能給的提示是『是吧就那樣』$R;");
                    return;
                }
                if (CheckInventory(pc, 10000350, 1))
                {
                    GiveItem(pc, 10000350, 1);
                    Say(pc, 131, "得到了『暗殺者的內服藥2』1個!$R;");
                    mask.SetValue(Job2X_03.未獲得暗殺者的內服藥2, false);
                    mask.SetValue(Job2X_03.第二個問題回答正確, true);
                    mask.SetValue(Job2X_03.第三個問題回答錯誤, false);
                    //_4A70 = false;
                    //_4A02 = true;
                    //_4A06 = false;
                    Say(pc, 131, "朋友們!盡最大的努力吧!$R;" +
                        "$P我能給的提示是『是吧就那樣』$R;");
                    return;
                }
                mask.SetValue(Job2X_03.未獲得暗殺者的內服藥2, true);
                //_4A70 = true;
                Say(pc, 131, "您的行李太多了$R;" +
                    "無法給您$R;");
                return;
            }
            if (mask.Test(Job2X_03.未獲得暗殺者的內服藥2))//_4A70)
            {
                if (CheckInventory(pc, 10000350, 1))
                {
                    GiveItem(pc, 10000350, 1);
                    Say(pc, 131, "得到了『暗殺者的內服藥2』1個!$R;");
                    mask.SetValue(Job2X_03.未獲得暗殺者的內服藥2, false);
                    mask.SetValue(Job2X_03.第二個問題回答正確, true);
                    mask.SetValue(Job2X_03.第三個問題回答錯誤, false);
                    //_4A70 = false;
                    //_4A02 = true;
                    //_4A06 = false;
                    Say(pc, 131, "朋友們!盡最大的努力吧!$R;" +
                        "$P我能給的提示是『是吧就那樣』$R;");
                    return;
                }
                mask.SetValue(Job2X_03.未獲得暗殺者的內服藥2, true);
                //_4A70 = true;
                Say(pc, 131, "您的行李太多了$R;" +
                    "無法給您$R;");
                return;
            }
            if (mask.Test(Job2X_03.第二個問題回答錯誤))//_4A05)
            {
                Say(pc, 131, "再聽一次提示後$R;" +
                    "再來吧$R;");
                return;
            }
            if (mask.Test(Job2X_03.第一個問題回答正確))//_4A01)
            {
                Say(pc, 131, "這裡是盜賊的考試場$R;" +
                    "$R最好不要想幫助測試中的人$R;" +
                    "$P……$R;" +
                    "$P但是其他的事情$R;" +
                    "你對咖喱怎麽想?$R;");
                switch (Select(pc, "怎麼做呢？", "", "我不怎麽喜歡咖喱", "吃的!", "喝的!"))
                {
                    case 1:
                        mask.SetValue(Job2X_03.第二個問題回答錯誤, true);
                        //_4A05 = true;
                        Say(pc, 131, "趕緊回去$R;");
                        break;
                    case 2:
                        mask.SetValue(Job2X_03.第二個問題回答錯誤, true);
                        //_4A05 = true;
                        Say(pc, 131, "還遠着呢…$R;");
                        break;
                    case 3:
                        Say(pc, 131, "是吧?!$R;" +
                            "我太喜歡咖喱了!!$R;" +
                            "$R天天吃咖喱也不會討厭!$R;" +
                            "還不如說是喝掉……$R;" +
                            "$R嗯…$R;" +
                            "$P那說一下提示吧$R;" +
                            "$R提示是『是吧就那樣』$R;" +
                            "可以猜到吧$R;" +
                            "$P來拿著這個走吧$R;");
                        if (CheckInventory(pc, 10000350, 1))
                        {
                            GiveItem(pc, 10000350, 1);
                            Say(pc, 131, "得到了『暗殺者的內服藥2』1個!$R;");
                            mask.SetValue(Job2X_03.未獲得暗殺者的內服藥2, false);
                            mask.SetValue(Job2X_03.第二個問題回答正確, true);
                            mask.SetValue(Job2X_03.第三個問題回答錯誤, false);
                            //_4A70 = false;
                            //_4A02 = true;
                            //_4A06 = false;
                            Say(pc, 131, "朋友們!盡最大的努力吧!$R;" +
                                "$P我能給的提示是『是吧就那樣』$R;");
                            return;
                        }
                        mask.SetValue(Job2X_03.未獲得暗殺者的內服藥2, true);
                        //_4A70 = true;
                        Say(pc, 131, "您的行李太多了$R;" +
                            "無法給您$R;");
                        break;
                }
                return;
            }
            if (mask.Test(Job2X_03.刺客轉職開始))//_4A00)
            {
                Say(pc, 131, "什麽?是暗號?$R;" +
                    "$R你在說什麽啊?$R;");
                return;
            }

            if (JobBasic_03_mask.Test(JobBasic_03.選擇轉職為盜賊))
            {
                Say(pc, 131, "要放棄做盜賊?$R;" +
                   "$R嗯…現在放棄雖然也可以$R;" +
                   "再次挑戰…$R;");
                switch (Select(pc, "怎麼做呢？", "", "不放棄!", "放棄…"))
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
            Say(pc, 131, "這裡是盜賊的考試場$R;" +
                "$R最好不要想幫助$R;" +
                "測試中的人$R;");
        }
    }
}
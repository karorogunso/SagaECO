using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10035000
{
    public class S11000519 : Event
    {
        public S11000519()
        {
            this.EventID = 11000519;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<JobBasic_03> JobBasic_03_mask = new BitMask<JobBasic_03>(pc.CMask["JobBasic_03"]);

            BitMask<Job2X_03> mask = pc.CMask["Job2X_03"];



            if (mask.Test(Job2X_03.第二個問題回答錯誤))//_4A05)
            {
                if (mask.Test(Job2X_03.第一個問題回答正確) && CountItem(pc, 10000309) >= 1)
                {
                    mask.SetValue(Job2X_03.第二個問題回答錯誤, false);
                    //_4A05 = false;
                    Say(pc, 131, "這個考試聽説需要一定的時間!$R;" +
                        "無論如何要加油$R;" +
                        "$P我給的提示是『不要想成是食物』$R;");
                    return;
                }
                if (CheckInventory(pc, 10000309, 1))
                {
                    GiveItem(pc, 10000309, 1);
                    Say(pc, 131, "得到了『暗殺者的內服藥1』$R;");
                    mask.SetValue(Job2X_03.未獲得暗殺者的內服藥1, false);
                    mask.SetValue(Job2X_03.第一個問題回答正確, true);
                    mask.SetValue(Job2X_03.第二個問題回答錯誤, false);
                    //_4A69 = false;
                    //_4A01 = true;
                    //_4A05 = false;
                    Say(pc, 131, "這個考試聽説需要一定的時間!$R;" +
                        "無論如何要加油$R;" +
                        "$P我給的提示是『不要想成是食物』$R;");
                    return;
                }
                mask.SetValue(Job2X_03.未獲得暗殺者的內服藥1, true);
                //_4A69 = true;
                Say(pc, 131, "您的行李太多了$R;" +
                    "無法給您$R;");
                return;
            }
            if (mask.Test(Job2X_03.第一個問題回答正確))//_4A01)
            {
                if (mask.Test(Job2X_03.第一個問題回答正確) && CountItem(pc, 10000309) >= 1)
                {
                    mask.SetValue(Job2X_03.第二個問題回答錯誤, false);
                    //_4A05 = false;
                    Say(pc, 131, "這個考試聽説需要一定的時間!$R;" +
                        "無論如何要加油$R;" +
                        "$P我給的提示是『不要想成是食物』$R;");
                    return;
                }
                if (CheckInventory(pc, 10000309, 1))
                {
                    GiveItem(pc, 10000309, 1);
                    Say(pc, 131, "得到了『暗殺者的內服藥1』$R;");
                    mask.SetValue(Job2X_03.未獲得暗殺者的內服藥1, false);
                    mask.SetValue(Job2X_03.第一個問題回答正確, true);
                    //_4A69 = false;
                    //_4A01 = true;
                    mask.SetValue(Job2X_03.第二個問題回答錯誤, false);
                    //_4A05 = false;
                    Say(pc, 131, "這個考試聽説需要一定的時間!$R;" +
                        "無論如何要加油$R;" +
                        "$P我給的提示是『不要想成是食物』$R;");
                    return;
                }
                mask.SetValue(Job2X_03.未獲得暗殺者的內服藥1, true);
                //_4A69 = true;
                Say(pc, 131, "您的行李太多了$R;" +
                    "無法給您$R;");
                return;
            }
            if (mask.Test(Job2X_03.未獲得暗殺者的內服藥1))//_4A69)
            {
                if (CheckInventory(pc, 10000309, 1))
                {
                    GiveItem(pc, 10000309, 1);
                    Say(pc, 131, "得到了『暗殺者的內服藥1』$R;");
                    mask.SetValue(Job2X_03.未獲得暗殺者的內服藥1, false);
                    mask.SetValue(Job2X_03.第一個問題回答正確, true);
                    //_4A69 = false;
                    //_4A01 = true;
                    mask.SetValue(Job2X_03.第二個問題回答錯誤, false);
                    //_4A05 = false;
                    Say(pc, 131, "這個考試聽説需要一定的時間!$R;" +
                        "無論如何要加油$R;" +
                        "$P我給的提示是『不要想成是食物』$R;");
                    return;
                }
                mask.SetValue(Job2X_03.未獲得暗殺者的內服藥1, true);
                //_4A69 = true;
                Say(pc, 131, "您的行李太多了$R;" +
                    "無法給您$R;");
                return;
            }
            if (mask.Test(Job2X_03.第一個問題回答錯誤))//_4A04)
            {
                Say(pc, 131, "再去聽一下提示$R;" +
                    "再過來怎麽樣?$R;");
                return;
            }
            if (mask.Test(Job2X_03.刺客轉職開始))//_4A00)
            {
                Say(pc, 131, "這裡是盜賊的考試場$R;" +
                    "$R最好不要想幫助測試中的人$R;" +
                    "$P……$R;" +
                    "$P有沒有去摩根看看的想法?$R;");
                switch (Select(pc, "怎麼做呢？", "", "摩根是什麽?", "飛上天空", "不…沒想過"))
                {
                    case 1:
                        mask.SetValue(Job2X_03.第一個問題回答錯誤, true);
                        //_4A04 = true;
                        Say(pc, 131, "你真的想要做?$R;");
                        break;
                    case 2:
                        Say(pc, 131, "嗯…對了$R;" +
                            "$P那我把提示說一下$R;" +
                            "$R提示是『不要想成是食物』$R;" +
                            "$P阿!差點忘記了$R;");
                        if (CheckInventory(pc, 10000309, 1))
                        {
                            GiveItem(pc, 10000309, 1);
                            Say(pc, 131, "得到了『暗殺者的內服藥1』$R;");
                            mask.SetValue(Job2X_03.未獲得暗殺者的內服藥1, false);
                            mask.SetValue(Job2X_03.第一個問題回答正確, true);
                            //_4A69 = false;
                            //_4A01 = true;
                            mask.SetValue(Job2X_03.第二個問題回答錯誤, false);
                            //_4A05 = false;
                            Say(pc, 131, "這個考試聽説需要一定的時間!$R;" +
                                "無論如何要加油$R;" +
                                "$P我給的提示是『不要想成是食物』$R;");
                            return;
                        }
                        mask.SetValue(Job2X_03.未獲得暗殺者的內服藥1, true);
                        //_4A69 = true;
                        Say(pc, 131, "您的行李太多了$R;" +
                            "無法給您$R;");
                        break;
                    case 3:
                        mask.SetValue(Job2X_03.第一個問題回答錯誤, true);
                        //_4A04 = true;
                        Say(pc, 131, "原來是沒有夢想的家伙…$R;");
                        break;
                }
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
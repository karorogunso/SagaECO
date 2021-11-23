using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:盜賊行會(30042000) NPC基本信息:盜賊總管(11000017) X:3 Y:3
namespace SagaScript.M30042000
{
    public class S11000017 : Event
    {
        public S11000017()
        {
            this.EventID = 11000017;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<JobBasic_03> JobBasic_03_mask = new BitMask<JobBasic_03>(pc.CMask["JobBasic_03"]);

            BitMask<Job2X_03> mask = pc.CMask["Job2X_03"];

            Say(pc, 11000017, 131, "歡迎光臨!$R;" +
                                   "這裡就是盜賊行會。$R;", "盜賊總管");

            if (JobBasic_03_mask.Test(JobBasic_03.盜賊轉職成功) &&
                !JobBasic_03_mask.Test(JobBasic_03.已經轉職為盜賊))
            {
                盜賊轉職完成(pc);
                return;
            }

            if (pc.Job == PC_JOB.NOVICE && pc.Race != PC_RACE.DEM)
            {
                if (JobBasic_03_mask.Test(JobBasic_03.選擇轉職為盜賊) &&
                    !JobBasic_03_mask.Test(JobBasic_03.已經轉職為盜賊))
                {
                    盜賊轉職任務(pc);
                    return;
                }
                else
                {
                    盜賊簡介(pc);
                    return;
                }
            }

            if (pc.JobBasic == PC_JOB.SCOUT)
            {
                if (mask.Test(Job2X_03.第一個問題回答錯誤))//_4A04)
                {
                    mask.SetValue(Job2X_03.第一個問題回答錯誤, false);
                    //_4A04 = false;
                    Say(pc, 131, "暗號錯了？$R;" +
                        "那没辦法囉$R;" +
                        "$R再告訴您一次，您要好好聽著喔$R;" +
                        "$P提示就是『天空』！！$R;" +
                        "別再忘掉唷！$R;");
                    return;
                }
                if (mask.Test(Job2X_03.刺客轉職開始))//_4A00)
                {
                    進階轉職完成(pc);
                    return;
                }
                if (pc.Inventory.Equipments.Count == 0)
                {
                    Say(pc, 131, "叫您快點穿衣服$R;");
                    return;
                }

                Say(pc, 11000017, 131, pc.Name + "? 對不對?$R;" +
                                       "$R今天是什麼日子啊？$R;", "盜賊總管");
                //EVT1100001760
                switch (Select(pc, "要做些什麼呢?", "", "任務服務台", "出售入國許可證", "販售藥物", "我想轉職", "什麼也不做"))
                {
                    case 1:
                        HandleQuest(pc, 13);
                        break;
                    case 2:
                        Say(pc, 131, "您想去阿伊恩薩烏斯是嗎？$R;");
                        OpenShopBuy(pc, 105);
                        break;
                    case 3:
                        Say(pc, 131, "要小心喔！$R;");
                        OpenShopBuy(pc, 153);
                        break;
                    case 4:
                        if (!mask.Test(Job2X_03.轉職結束))
                        {
                            進階轉職介紹(pc);
                            return;
                        }
                        Say(pc, 131, "不能再轉職了$R;");
                        break;
                    case 5:
                        break;
                }
                //EVENTEND
            }
        }

        void 盜賊簡介(ActorPC pc)
        {
            BitMask<JobBasic_03> JobBasic_03_mask = new BitMask<JobBasic_03>(pc.CMask["JobBasic_03"]);

            int selection;

            Say(pc, 11000017, 131, "我是管理盜賊們的盜賊總管。$R;" +
                                   "$P您好像不屬於我們行會的管轄呀?$R;" +
                                   "$R…這樣看來…$R;" +
                                   "您想不想做『盜賊』呢?$R;", "盜賊總管");

            selection = Select(pc, "想做什麼?", "", "我想成為『盜賊』!", "『盜賊』是什麼樣的職業?", "任務服務台", "什麼也不做");

            while (selection != 4)
            {
                switch (selection)
                {
                    case 1:
                        if (pc.Str > 9)
                        {
                            JobBasic_03_mask.SetValue(JobBasic_03.選擇轉職為盜賊, true);
                            JobBasic_03_mask.SetValue(JobBasic_03.盜賊轉職任務完成, true);
                            //廢除一次職轉職任務
                            /*
                            Say(pc, 11000017, 131, "哈哈，您想成為『盜賊』嗎?$R;" +
                                                   "$R要成為盜賊，$R;" +
                                                   "就先要經過『考驗』。$R;" +
                                                   "$P…要有必死的決心接受考驗嗎?$R;", "盜賊總管");

                            switch (Select(pc, "接受『考驗』嗎?", "", "沒問題", "才不要"))
                            {
                                case 1:
                                    JobBasic_03_mask.SetValue(JobBasic_03.選擇轉職為盜賊, true);

                                    Say(pc, 11000017, 131, "想辦法閃避魔物，$R;" +
                                                           "毫髮無傷的回到這吧。$R;", "盜賊總管");

                                    SetHomePoint(pc, 10035000, 64, 175);

                                    Warp(pc, 10035000, 64, 175);
                                    break;

                                case 2:
                                    Say(pc, 11000017, 131, "不要浪費我的時間…，$R;" +
                                                           "考慮清楚以後再來吧。$R;", "盜賊總管");
                                    break;
                            }
                            */
                            申請轉職為盜賊(pc);
                        }
                        else
                        {
                            Say(pc, 11000017, 131, "力量到達10了以後，$R;" +
                                                   "再來找我吧!$R;", "盜賊總管");
                        }
                        return;

                    case 2:
                        Say(pc, 11000017, 131, "盜賊這職業比較適合$R;" +
                                               "埃米爾族和道米尼族的體質唷!$R;" +
                                               "$R我會仔細解說給您聽的。$R;", "盜賊總管");

                        switch (Select(pc, "還要聽下去嗎?", "", "我要聽", "不聽"))
                        {
                            case 1:
                                Say(pc, 11000017, 131, "『盜賊』是為了達成任務而不擇手段的$R;" +
                                                       "冷酷戰士。$R;" +
                                                       "$R即使情況對自己不利，$R;" +
                                                       "也可把黑夜當作保護色，$R;" +
                                                       "殺敵人一個措手不及。$R;" +
                                                       "$P實力如果再高一點的話，$R;" +
                                                       "在不知不覺間，$R;" +
                                                       "就可以把敵人一一幹掉。$R;" +
                                                       "$P是比較適合觀察力強的人的職業喔。$R;", "盜賊總管");
                                break;

                            case 2:
                                break;
                        }
                        break;

                    case 3:
                        Say(pc, 11000017, 131, "想成為『盜賊』的話，讓我給您介紹任務吧!$R;", "盜賊總管");
                        break;
                }

                selection = Select(pc, "想做什麼?", "", "我想成為『盜賊』!", "『盜賊』是什麼樣的職業?", "任務服務台", "什麼也不做");
            } 
        }

        void 盜賊轉職任務(ActorPC pc)
        {
            BitMask<JobBasic_03> JobBasic_03_mask = new BitMask<JobBasic_03>(pc.CMask["JobBasic_03"]);

            if (!JobBasic_03_mask.Test(JobBasic_03.盜賊轉職任務完成))
            {
                平安回到盜賊行會(pc);
            }

            if (JobBasic_03_mask.Test(JobBasic_03.盜賊轉職任務完成) &&
                !JobBasic_03_mask.Test(JobBasic_03.盜賊轉職成功))
            {
                申請轉職為盜賊(pc);
                return;
            }

        }

        void 平安回到盜賊行會(ActorPC pc)
        {
            BitMask<JobBasic_03> JobBasic_03_mask = new BitMask<JobBasic_03>(pc.CMask["JobBasic_03"]);

            Say(pc, 11000017, 131, "回來了。$R;" +
                                   "$P嗯…$R;" +
                                   "一路上辛苦了。$R;" +
                                   "$R盜賊的真隨就是避免戰鬥，你因該有深刻體會了。$R;" +
                                   "千萬不可以忘記喔!$R;" +
                                   "$P那您是不是要轉職為『盜賊』呢?$R;", "盜賊總管");

            switch (Select(pc, "要轉職為『盜賊』嗎?", "", "轉職為『盜賊』", "還是算了吧"))
            {
                case 1:
                    JobBasic_03_mask.SetValue(JobBasic_03.盜賊轉職任務完成, true);
                    break;

                case 2:
                    Say(pc, 11000017, 131, "再考慮看看吧!$R;", "盜賊總管");
                    break;
            }
        }

        void 申請轉職為盜賊(ActorPC pc)
        {
            BitMask<JobBasic_03> JobBasic_03_mask = new BitMask<JobBasic_03>(pc.CMask["JobBasic_03"]);

            Say(pc, 11000017, 131, "那麼就給您紋上這象徵『盜賊』的$R;" +
                                   "『盜賊紋章』吧!$R;", "盜賊總管");

            if (pc.Inventory.Equipments.Count == 0)
            {
                JobBasic_03_mask.SetValue(JobBasic_03.盜賊轉職成功, true);

                PlaySound(pc, 3087, false, 100, 50);
                ShowEffect(pc, 4131);
                Wait(pc, 3960);

                Say(pc, 11000017, 131, "…$R;" +
                                       "$P好棒啊，$R;" +
                                       "您身上已經烙印了漂亮的紋章。$R;" +
                                       "$R從今以後，$R;" +
                                       "您就成為代表我們的『盜賊』了。$R;", "盜賊總管");

                PlaySound(pc, 4012, false, 100, 50);
                ChangePlayerJob(pc, PC_JOB.SCOUT);

                Say(pc, 0, 0, "您已經轉職為『盜賊』了!$R;", " ");

                Say(pc, 11000017, 131, "先穿上衣服後，再和我說話吧。$R;" +
                                       "有一份小禮物要送給您唷!$R;" +
                                       "$R您先去整理行李後，再來找我吧。$R;", "盜賊總管");
            }
            else
            {
                Say(pc, 11000017, 131, "紋章是烙印在皮膚上的，$R;" +
                                       "先把裝備脫掉吧。$R;", "盜賊總管");
            }
        }

        void 盜賊轉職完成(ActorPC pc)
        {
            BitMask<JobBasic_03> JobBasic_03_mask = new BitMask<JobBasic_03>(pc.CMask["JobBasic_03"]);

            if (pc.Inventory.Equipments.Count != 0)
            {
                JobBasic_03_mask.SetValue(JobBasic_03.已經轉職為盜賊, true);

                Say(pc, 11000017, 131, "給您『怪盜面具』，$R;" +
                                       "$R您就用心練吧。$R;", "盜賊總管");

                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, 50040200, 1);
                Say(pc, 0, 0, "得到『怪盜面具』!$R;", " ");

                LearnSkill(pc, 2042);
                Say(pc, 0, 0, "學到『隱身』!$R;", " ");
            }
            else
            {
                Say(pc, 11000017, 131, "先穿上衣服後，再和我說話吧。$R;", "盜賊總管");
            }
        }

        void 進階轉職介紹(ActorPC pc)
        {
            BitMask<Job2X_03> mask = pc.CMask["Job2X_03"];
            //EVT1100001761
            Say(pc, 131, "如果是您的話，或許有可能成功的$R;" +
                "$R怎麼樣，想不想成為暗殺者？$R;");
            //EVT1100001762
            switch (Select(pc, "做什麼呢?", "", "我要做『暗殺者』喔！", "『暗殺者』是怎樣的職業？", "什麼也不做"))
            {
                case 1:
                    if (pc.JobLevel1 > 29)
                    {
                        Say(pc, 131, "是想成為暗殺者嗎？$R;" +
                            "$R先考驗一下您的力量吧！$R;");
                        switch (Select(pc, "要考驗您的力量嗎?", "", "當然要", "算了吧。"))
                        {
                            case 1:
                                mask.SetValue(Job2X_03.刺客轉職開始, true);
                                //_4A00 = true;
                                Say(pc, 131, "哈哈哈哈！！$R;" +
                                    "為什麼老是這麼嚴肅呢？$R;" +
                                    "$R不要擔心，現在就開始行動吧！$R;" +
                                    "$P現在去找四種藥$R;" +
                                    "$P拿著藥的人在大陸上的各地$R;" +
                                    "$P其中一名可能在海岸附近$R;" +
                                    "$P其他三名，一名在寒冷的地方$R;" +
                                    "一名在炎熱的地方$R;" +
                                    "$R最後一名在西邊的島上。$R;" +
                                    "$P重要的是，就算找到拿藥的人，$R;" +
                                    "但是不知道暗號的話$R還是不能拿到藥的$R;" +
                                    "$P給您點提示吧$R;" +
                                    "$R提示就是『天空』$R;" +
                                    "$P呵呵，這個提示不知道是誰定的$R;" +
                                    "$P現在就出發吧！$R;");
                                break;
                        }
                        return;
                    }
                    Say(pc, 131, "想要成為暗殺者，力量是很重要的。$R;" +
                        "$R先去提升『力量』後，再來吧。$R;" +
                        "其他的以後再説啦。$R;");
                    break;
                case 2:
                    Say(pc, 131, "跟盜賊一樣，這職業比較適合$R;" +
                        "埃米爾族和道米尼族的體質唷$R;" +
                        "$R我會慢慢給您講解的…$R;");
                    switch (Select(pc, "聽下去嗎?", "", "我要聽", "不聽"))
                    {
                        case 1:
                            Say(pc, 131, "暗殺者是不達目標誓不罷休的，$R;" +
                                "是個悲壯的戰士唷$R;" +
                                "$R暗殺者可以把自己隱藏在黑暗中$R;" +
                                "瞬間把敵人殺掉$R;" +
                                "$P使用特殊的道具和毒藥方面$R;" +
                                "也具卓越的才能$R;" +
                                "是個比較適合洞察力高的人的職業喔$R;");
                            //GOTO EVT1100001762
                            break;
                    }
                    break;
            }
        }
        
        void 進階轉職完成 (ActorPC pc)
        {
            BitMask<Job2X_03> mask = pc.CMask["Job2X_03"];

            if (mask.Test(Job2X_03.防禦過高) ||
                mask.Test(Job2X_03.交還暗殺者的內服藥))
            {
                switch (Select(pc, "真的要轉職嗎?", "", "我想成為暗殺者", "還是算了吧。"))
                {
                    case 1:
                        Say(pc, 131, "那麼就給您烙印上這象徵暗殺者的$R;" +
                            "『暗殺者紋章』吧$R;");
                        if (pc.Inventory.Equipments.Count == 0)
                        {
                            mask.SetValue(Job2X_03.轉職結束, true);
                            mask.SetValue(Job2X_03.刺客轉職開始, false);
                            mask.SetValue(Job2X_03.第一個問題回答正確, false);
                            mask.SetValue(Job2X_03.第二個問題回答正確, false);
                            mask.SetValue(Job2X_03.第三個問題回答正確, false);
                            mask.SetValue(Job2X_03.第四個問題回答正確, false);
                            mask.SetValue(Job2X_03.防禦過高, false);
                            mask.SetValue(Job2X_03.交還暗殺者的內服藥, false);
                            ChangePlayerJob(pc, PC_JOB.ASSASSIN);
                            pc.JEXP = 0;
                            PlaySound(pc, 3087, false, 100, 50);
                            ShowEffect(pc, 4131);
                            Wait(pc, 4000);
                            Say(pc, 131, "…$R;" +
                                "$P好棒啊，$R;" +
                                "您身上已經烙印了漂亮的紋章。$R;" +
                                "$R從今以後，$R您就成為代表我們的『暗殺者』了。$R;");
                            PlaySound(pc, 4012, false, 100, 50);
                            Say(pc, 131, "您已轉職為『暗殺者』了。$R;");
                            Say(pc, 131, "現在先穿上衣服吧$R;" +
                                "$P期待您以後有所作為喔$R;");
                            return;
                        }
                        mask.SetValue(Job2X_03.防禦過高, true);
                        Say(pc, 131, "防禦太高的話，就無法烙印紋章了$R;" +
                            "請換上輕便的服裝後，再來吧。$R;");
                        break;
                    case 2:
                        Say(pc, 131, "您對未來感到迷茫嗎？$R;" +
                            "$R先穩定心神，冷靜後再來找我吧。$R;");
                        break;
                }
                return;
            }

            if (CountItem(pc, 10000309) >= 1 && 
                CountItem(pc, 10000350) >= 1 && 
                CountItem(pc, 10000351) >= 1 && 
                CountItem(pc, 10000352) >= 1)
            {
                mask.SetValue(Job2X_03.交還暗殺者的內服藥, true);
                //_4A25 = true;
                Say(pc, 131, "來吧$R;" +
                    "$R看起來平安無事啊。$R;" +
                    "$P…$R;" +
                    "$P一切順利，真是太好了！$R;" +
                    "$R您好像拿了『暗殺者的内服藥』回來，$R;" +
                    "是吧？$R;");
                TakeItem(pc, 10000309, 1);
                TakeItem(pc, 10000350, 1);
                TakeItem(pc, 10000351, 1);
                TakeItem(pc, 10000352, 1);
                Say(pc, 131, "交出『暗殺者的内服藥1』$R;" +
                    "交出『暗殺者的内服藥2』$R;" +
                    "交出『暗殺者的内服藥3』$R;" +
                    "交出『暗殺者的内服藥4』$R;");
                Say(pc, 131, "太簡單了嗎？$R;" +
                    "$P您在任何條件下都能生存$R;" +
                    "$R看來您有資格做暗殺者呢$R;" +
                    "$P真要做暗殺者嗎？$R;");
                //EVT1100001774
                switch (Select(pc, "真的要轉職嗎?", "", "我想成為暗殺者", "還是算了吧。"))
                {
                    case 1:
                        Say(pc, 131, "那麼就給您烙印上這象徵暗殺者的$R;" +
                            "『暗殺者紋章』吧$R;");
                        if (pc.Inventory.Equipments.Count == 0)
                        {
                            mask.SetValue(Job2X_03.轉職結束, true);
                            mask.SetValue(Job2X_03.刺客轉職開始, false);
                            mask.SetValue(Job2X_03.第一個問題回答正確, false);
                            mask.SetValue(Job2X_03.第二個問題回答正確, false);
                            mask.SetValue(Job2X_03.第三個問題回答正確, false);
                            mask.SetValue(Job2X_03.第四個問題回答正確, false);
                            mask.SetValue(Job2X_03.防禦過高, false);
                            mask.SetValue(Job2X_03.交還暗殺者的內服藥, false);
                            //_4A10 = true;
                            //_4A00 = false;
                            //_4A01 = false;
                            //_4A02 = false;
                            //_4A03 = false;
                            //_4A08 = false;
                            //_4A09 = false;
                            //_4A25 = false;
                            ChangePlayerJob(pc, PC_JOB.ASSASSIN);
                            pc.JEXP = 0;
                            //PARAM ME.JOB = 23
                            PlaySound(pc, 3087, false, 100, 50);
                            ShowEffect(pc, 4131);
                            Wait(pc, 4000);
                            Say(pc, 131, "…$R;" +
                                "$P好棒啊，$R;" +
                                "您身上已經烙印了漂亮的紋章。$R;" +
                                "$R從今以後，$R您就成為代表我們的『暗殺者』了。$R;");
                            PlaySound(pc, 4012, false, 100, 50);
                            Say(pc, 131, "您已轉職為『暗殺者』了。$R;");
                            Say(pc, 131, "現在先穿上衣服吧$R;" +
                                "$P期待您以後有所作為喔$R;");
                            return;
                        }
                        mask.SetValue(Job2X_03.防禦過高, true);
                        //_4A09 = true;
                        Say(pc, 131, "防禦太高的話，就無法烙印紋章了$R;" +
                            "請換上輕便的服裝後，再來吧。$R;");
                        break;
                    case 2:
                        Say(pc, 131, "您對未來感到迷茫嗎？$R;" +
                            "$R先穩定心神，冷靜後再來找我吧。$R;");
                        break;
                }
                return;
            }
            Say(pc, 131, "咦？$R;" +
                "還没有搜集齊全呢？$R;" +
                "$R集齊前休想回有唷！$R;");
        }
    }
}

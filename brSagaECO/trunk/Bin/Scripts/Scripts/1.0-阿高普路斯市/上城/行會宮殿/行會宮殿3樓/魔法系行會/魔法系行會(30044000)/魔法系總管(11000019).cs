using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:魔法系行會(30044000) NPC基本信息:魔法系總管(11000019) X:3 Y:2
namespace SagaScript.M30044000
{
    public class S11000019 : Event
    {
        public S11000019()
        {
            this.EventID = 11000019;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<JobBasic_05> JobBasic_05_mask = new BitMask<JobBasic_05>(pc.CMask["JobBasic_05"]);
            BitMask<JobBasic_06> JobBasic_06_mask = new BitMask<JobBasic_06>(pc.CMask["JobBasic_06"]);
            BitMask<Job2X_05> Job2X_05_mask = pc.CMask["Job2X_05"];
            BitMask<Job2X_06> Job2X_06_mask = pc.CMask["Job2X_06"];

            if (Job2X_06_mask.Test(Job2X_06.防禦過高))//_4A13)
            {
                Say(pc, 131, "那麼就給您烙印上這象徵元素術師的$R;" +
                    "『元素術師紋章』吧$R;");
                if (pc.Inventory.Equipments.Count == 0)
                {
                    Job2X_06_mask.SetValue(Job2X_06.轉職完成, true);
                    Job2X_06_mask.SetValue(Job2X_06.進階轉職開始, false);
                    Job2X_06_mask.SetValue(Job2X_06.朝拜大地精靈, false);
                    Job2X_06_mask.SetValue(Job2X_06.朝拜神風精靈, false);
                    Job2X_06_mask.SetValue(Job2X_06.朝拜水之精靈, false);
                    Job2X_06_mask.SetValue(Job2X_06.朝拜炎之精靈, false);
                    Job2X_06_mask.SetValue(Job2X_06.防禦過高, false);
                    Job2X_06_mask.SetValue(Job2X_06.所有問題回答正確, false);
                    ChangePlayerJob(pc, PC_JOB.ELEMENTER);
                    pc.JEXP = 0;
                    //PARAM ME.JOB = 53
                    PlaySound(pc, 3087, false, 100, 50);
                    ShowEffect(pc, 4131);
                    Wait(pc, 4000);
                    Say(pc, 131, "…$R;" +
                        "$P好棒啊，$R;" +
                        "您身上已經烙印了漂亮的紋章。$R;" +
                        "$R從今以後，$R您就成為代表我們的『元素術師』了。$R;");
                    PlaySound(pc, 4012, false, 100, 50);
                    Say(pc, 131, "您已轉職為『元素術師』了。$R;");
                    Say(pc, 131, "以後每天都要很真誠待人喔$R;");
                    return;
                }
                Say(pc, 131, "防禦太高的話，就無法烙印紋章了$R;" +
                    "請換上輕便的服裝後，再來吧。$R;");
                return;
            }

            Say(pc, 11000019, 131, "歡迎光臨!!$R;" +
                                   "$R這裡就是魔法系行會。$R;", "魔法系總管");

            if (Job2X_06_mask.Test(Job2X_06.所有問題回答正確))//_3A99)
            {
                switch (Select(pc, "要轉職成為『元素術師』嗎？", "", "成為元素術師", "算了吧"))
                {
                    case 1:
                        Say(pc, 131, "那麼就給您烙印上這象徵元素術師的$R;" +
                            "『元素術師紋章』吧$R;");
                        if (pc.Inventory.Equipments.Count == 0)
                        {
                            Job2X_06_mask.SetValue(Job2X_06.轉職完成, true);
                            Job2X_06_mask.SetValue(Job2X_06.進階轉職開始, false);
                            Job2X_06_mask.SetValue(Job2X_06.朝拜大地精靈, false);
                            Job2X_06_mask.SetValue(Job2X_06.朝拜神風精靈, false);
                            Job2X_06_mask.SetValue(Job2X_06.朝拜水之精靈, false);
                            Job2X_06_mask.SetValue(Job2X_06.朝拜炎之精靈, false);
                            Job2X_06_mask.SetValue(Job2X_06.防禦過高, false);
                            Job2X_06_mask.SetValue(Job2X_06.所有問題回答正確, false);

                            ChangePlayerJob(pc, PC_JOB.ELEMENTER);
                            pc.JEXP = 0;

                            PlaySound(pc, 3087, false, 100, 50);
                            ShowEffect(pc, 4131);
                            Wait(pc, 4000);
                            Say(pc, 131, "…$R;" +
                                "$P好棒啊，$R;" +
                                "您身上已經烙印了漂亮的紋章。$R;" +
                                "$R從今以後，$R您就成為代表我們的『元素術師』了。$R;");
                            PlaySound(pc, 4012, false, 100, 50);
                            Say(pc, 131, "您已轉職為『元素術師』了。$R;");
                            Say(pc, 131, "以後每天都要很真誠待人喔$R;");
                            return;
                        }
                        Job2X_06_mask.SetValue(Job2X_06.防禦過高, true);
                        //_4A13 = true;
                        Say(pc, 131, "防禦太高的話，就無法烙印紋章了$R;" +
                            "請換上輕便的服裝後，再來吧。$R;");
                        break;
                    case 2:
                        Say(pc, 131, "是嗎？$R;" +
                            "要是想法的話，再來找我吧。$R;");
                        break;
                }
                return;
            }

            if (Job2X_06_mask.Test(Job2X_06.進階轉職開始))//_3A93)
            {
                進階元素術師(pc);
                return;
            }

            if (JobBasic_05_mask.Test(JobBasic_05.魔法師轉職成功) &&
                !JobBasic_05_mask.Test(JobBasic_05.已經轉職為魔法師))
            {
                魔法師轉職完成(pc);
                return;
            }

            if (JobBasic_06_mask.Test(JobBasic_06.元素使轉職成功) &&
                !JobBasic_06_mask.Test(JobBasic_06.已經轉職為元素使))
            {
                元素使轉職完成(pc);
                return;
            }

            if (pc.Job == PC_JOB.NOVICE && pc.Race != PC_RACE.DEM)
            {
                if (JobBasic_05_mask.Test(JobBasic_05.選擇轉職為魔法師) &&
                    !JobBasic_05_mask.Test(JobBasic_05.已經轉職為魔法師))
                {
                    魔法師轉職任務(pc);
                    return;
                }

                else if (JobBasic_06_mask.Test(JobBasic_06.選擇轉職為元素使) &&
                         !JobBasic_06_mask.Test(JobBasic_06.已經轉職為元素使))
                {
                    元素使轉職任務(pc);
                    return;
                }

                else
                {
                    魔法師與元素使簡介(pc);
                    return;
                }
            }

            if (pc.JobBasic == PC_JOB.SHAMAN ||
                pc.JobBasic == PC_JOB.WIZARD)
            {
                if (pc.Inventory.Equipments.Count == 0)
                {
                    Say(pc, 131, "穿這樣的服裝的話，會得感冒的喔$R;");
                    return;
                }

                if (Job2X_05_mask.Test(Job2X_05.轉職開始))//_3A89)
                {
                    Say(pc, 131, "您走吧！努力喔！$R;");
                    return;
                }

                Say(pc, 11000019, 131, pc.Name + "…$R;" +
                                       "$R什麼事啊??$R;", "魔法系總管");

                switch (Select(pc, "做什麼呢?", "", "任務服務台", "出售諾頓入國許可證", "我想轉職", "什麼都不做"))
                {
                    case 1:
                        HandleQuest(pc, 43);
                        break;
                    case 2:
                        Say(pc, 131, "要去諾頓嗎?$R;");
                        OpenShopBuy(pc, 82);
                        break;
                    case 3:
                        進階轉職(pc);
                        break;
                    case 4:
                        break;
                }
            }
        }

        void 魔法師與元素使簡介(ActorPC pc)
        {
            BitMask<JobBasic_05> JobBasic_05_mask = new BitMask<JobBasic_05>(pc.CMask["JobBasic_05"]);
            BitMask<JobBasic_06> JobBasic_06_mask = new BitMask<JobBasic_06>(pc.CMask["JobBasic_06"]);

            int selection;

            Say(pc, 11000019, 131, "管理魔法系行會的總管就是我。$R;" +
                                   "$P哈哈，您是初心者嗎?$R;" +
                                   "$R呵呵…$R;" +
                                   "$P對『魔法師』和『元素使』有興趣的話，$R;" +
                                   "就聽聽我的講解吧。$R;", "魔法系總管");

            selection = Select(pc, "想做什麼?", "", "我想成為『魔法師』!", "『魔法師』是什麼樣的職業?", "我想成為『元素使』!", "『元素使』是什麼樣的職業?!", "任務服務台", "什麼也不做");

            while (selection != 6)
            {
                switch (selection)
                {
                    case 1:
                        JobBasic_05_mask.SetValue(JobBasic_05.選擇轉職為魔法師, true);
                        //廢除一次職轉職任務
                        JobBasic_05_mask.SetValue(JobBasic_05.已經從魔術那裡聽取有關新生魔法的知識, true);
                        JobBasic_05_mask.SetValue(JobBasic_05.魔法師轉職任務完成, true);
                        /*
                        Say(pc, 11000019, 131, "哈哈，您想成為『魔法師』嗎?$R;" +
                                               "$R要成為魔法師，$R;" +
                                               "就先要理解「新生魔法」喔!$R;" +
                                               "$P本來我能教您的話是最好的，$R;" +
                                               "不過我身為總管實在是太忙了。$R;" +
                                               "$P在「阿高普路斯市」的「下城」$R;" +
                                               "有個對「新生魔法」很熟悉的傢伙。$R;" +
                                               "$R當然他沒有我懂得多啦。$R;" +
                                               "他的名字叫「魔術」。$R;" +
                                               "$R那小子雖然有點輕率，$R;" +
                                               "但是也沒有別的人選了。$R;" +
                                               "$R沒有辦法啊~~!$R;" +
                                               "$P去那小子那裡學習「新生魔法」吧。$R;", "魔法系總管");
                        */
                        申請轉職為魔法師(pc);

                        return;

                    case 2:
                        Say(pc, 11000019, 131, "魔法師這職業比較適合$R;" +
                                               "塔妮亞和道米尼族的體質唷!$R;" +
                                               "$R埃米爾選擇這個職業的話，$R;" +
                                               "會很辛苦的。$R;" +
                                               "$P這樣還想聽下去嗎?$R;", "魔法系總管");

                        switch (Select(pc, "還要聽下去嗎?", "", "我要聽", "不聽"))
                        {
                            case 1:
                                Say(pc, 11000019, 131, "『魔法師』這種職業，耗盡一生，$R;" +
                                                       "研究和試驗「新生魔法」。$R;" +
                                                       "$P「新生魔法」並不是火或水等$R;" +
                                                       "屬性的力量，$R;" +
                                                       "而是利用次元力量的一種新魔法。$R;" +
                                                       "$P但是沒有什麼副作用，可以放心。$R;" +
                                                       "哈哈…$R;" +
                                                       "$P因為是新魔法，還不知道威力有多強唷!$R;" +
                                                       "研究出來的話，$R;" +
                                                       "說不定還可以用來打破$R;" +
                                                       "次元障壁的魔法呢!$R;" +
                                                       "$P開始的時候，$R;" +
                                                       "簡單的攻擊魔法和盾牌，$R;" +
                                                       "並可以使用提高防禦力的輔助魔法呢!$R;", "魔法系總管");
                                break;

                            case 2:
                                break;
                        }
                        break;

                    case 3:
                        JobBasic_06_mask.SetValue(JobBasic_06.選擇轉職為元素使, true);
                        //廢除一次職轉職任務
                        JobBasic_06_mask.SetValue(JobBasic_06.已經從雪莉那裡聽取有關屬性魔法的知識, true);
                        JobBasic_06_mask.SetValue(JobBasic_06.元素使轉職任務完成, true);
                        /*
                        Say(pc, 11000019, 131, "哈哈，您想成為『元素使』嗎?$R;" +
                                               "$R要成為元素使，$R;" +
                                               "就先要理解「屬性魔法」。$R;" +
                                               "$P本來我教您的話是最好的。$R;" +
                                               "不過我身為總管實在是太忙了。$R;" +
                                               "$P在「阿高普路斯市」的「下城」$R;" +
                                               "有個對「屬性魔法」很熟悉的人。$R;" +
                                               "$R她比我厲害多呢!$R;" +
                                               "$P她的名字叫「雪莉」。$R;" +
                                               "$R是個美麗又神秘的女生，$R;" +
                                               "要是我年輕個十歲，早就去追了。$R;" +
                                               "$P去她那裡學習「屬性魔法」吧。$R;" +
                                               "$P…一定要幫我向她問好喔。$R;", "魔法系總管");
                        */
                        申請轉職為元素使(pc);
                        return;

                    case 4:
                        Say(pc, 11000019, 131, "『元素使』這職業比較適合$R;" +
                                               "塔妮亞和道米尼族的體質唷!$R;" +
                                               "$R埃米爾選擇這個職業的話$R;" +
                                               "會很辛苦的。$R;" +
                                               "$P這樣還想聽下去嗎?$R;", "魔法系總管");

                        switch (Select(pc, "還要聽下去嗎?", "", "我要聽", "不聽"))
                        {
                            case 1:
                                Say(pc, 11000019, 131, "『元素使』這種職業，耗盡一生，$R;" +
                                                       "研究「屬性魔法」。$R;" +
                                                       "$P「屬性魔法」是利用$R;" +
                                                       "炎、水、風、地之精靈的力量。$R;" +
                                                       "$R是從古代流傳下來的魔法。$R;" +
                                                       "$P除此之外，雖然也有光和闇屬性魔法，$R;" +
                                                       "但有點不同呢。$R;" +
                                                       "$P要使用「光屬性」魔法，$R;" +
                                                       "首先要用光淨化自己的身體。$R;" +
                                                       "$R要使用「闇屬性」魔法，$R;" +
                                                       "要用黑暗浸染自己的魂。$R;" +
                                                       "$P若想知道更多，$R;" +
                                                       "可以去「白之聖堂」和「黑之聖堂」$R;" +
                                                       "打聽一下情報吧。$R;" +
                                                       "$P說到元素使，$R;" +
                                                       "他可以操縱各個屬性能力，$R;" +
                                                       "還會各種攻擊魔法呢。$R;" +
                                                       "$P隊伍裡若有元素使護航的話，$R;" +
                                                       "將會發揮十分超強威力。$R;" +
                                                       "$P但若還不明白屬性的力量的話，$R;" +
                                                       "就沒有什麼意義了。$R;" +
                                                       "$R也可以說是優秀者用的職業喔!$R;", "魔法系總管");
                                break;

                            case 2:
                                break;
                        }
                        break;

                    case 5:
                        break;
                }

                selection = Select(pc, "想做什麼?", "", "我想成為『魔法師』!", "『魔法師』是什麼樣的職業?", "我想成為『元素使』!", "『元素使』是什麼樣的職業?!", "任務服務台", "什麼也不做");
            }
        }

        void 魔法師轉職任務(ActorPC pc)
        {
            BitMask<JobBasic_05> JobBasic_05_mask = new BitMask<JobBasic_05>(pc.CMask["JobBasic_05"]);

            if (!JobBasic_05_mask.Test(JobBasic_05.魔法師轉職任務完成))
            {
                新生魔法相關問題回答(pc);
            }

            if (JobBasic_05_mask.Test(JobBasic_05.魔法師轉職任務完成) &&
                !JobBasic_05_mask.Test(JobBasic_05.魔法師轉職成功))
            {
                申請轉職為魔法師(pc);
                return;
            }
        }

        void 新生魔法相關問題回答(ActorPC pc)
        {
            BitMask<JobBasic_05> JobBasic_05_mask = new BitMask<JobBasic_05>(pc.CMask["JobBasic_05"]);

            if (JobBasic_05_mask.Test(JobBasic_05.已經從魔術那裡聽取有關新生魔法的知識))
            {
                新生魔法相關問題01(pc);
            }
            else
            {
                Say(pc, 11000019, 131, "再跟您說一遍吧。$R;" +
                                       "$R到「阿高普路斯市」的「下城」$R;" +
                                       "去找一個叫「魔術」的人，$R;" +
                                       "學習「新生魔法」的知識吧!$R;", "魔法系總管");
            }
        }

        void 新生魔法相關問題01(ActorPC pc)
        {
            Say(pc, 11000019, 131, "好像學成歸來了?$R;" +
                                   "$P嗯……$R;" +
                                   "那我就要考驗一下您囉?$R;" +
                                   "$P提升防禦力的魔法叫什麼名字?$R;", "魔法系總管");

            switch (Select(pc, "提升防禦力的魔法是?", "", "帝凡斯盾", "元靈護盾", "松木盾"))
            {
                case 1:
                    新生魔法相關問題回答錯誤(pc);
                    break;
                    
                case 2:
                    新生魔法相關問題02(pc);
                    break;

                case 3:
                    新生魔法相關問題回答錯誤(pc);
                    break;
            }
        }

        void 新生魔法相關問題02(ActorPC pc)
        {
            PlaySound(pc, 2040, false, 100, 50);

            Say(pc, 11000019, 131, "哈哈，不錯嘛。$R;" +
                                   "$R那麼下一個問題。$R;" +
                                   "$P以下三個中哪個是攻擊魔法?$R;", "魔法系總管");

            switch (Select(pc, "在這裡面屬於攻擊魔法是?", "", "抑制破壞", "魔法片", "魔法師的殘像"))
            {
                case 1:
                    新生魔法相關問題回答錯誤(pc);
                    break;

                case 2:
                    新生魔法相關問題03(pc);
                    break;

                case 3:
                    新生魔法相關問題回答錯誤(pc);
                    break;
            }
        }

        void 新生魔法相關問題03(ActorPC pc)
        {
            PlaySound(pc, 2040, false, 100, 50);

            Say(pc, 11000019, 131, "好吧，$R;" +
                                   "那麼現在，問您最後一個問題。$R;" +
                                   "$P『魔法師的殘像』的效果是什麼?$R;", "魔法系總管");

            switch (Select(pc, "『魔法師的殘像』的效果是…?", "", "提高斧的攻擊力", "製作分身", "降低對方命中率"))
            {
                case 1:
                    新生魔法相關問題回答錯誤(pc);
                    break;

                case 2:
                    新生魔法相關問題回答正確(pc);
                    break;

                case 3:
                    新生魔法相關問題回答錯誤(pc);
                    break;
            }
        }

        void 新生魔法相關問題回答正確(ActorPC pc)
        {
            BitMask<JobBasic_05> JobBasic_05_mask = new BitMask<JobBasic_05>(pc.CMask["JobBasic_05"]);
            BitMask<JobBasic_06> JobBasic_06_mask = new BitMask<JobBasic_06>(pc.CMask["JobBasic_06"]);

            PlaySound(pc, 2040, false, 100, 50);

            Say(pc, 11000019, 131, "嗯，很好。$R;" +
                                   "像您這麼聰明的人，$R;" +
                                   "應該可以做魔法師了。$R;" +
                                   "$R真的要做『魔法師』嗎?$R;", "魔法系總管");

            switch (Select(pc, "要轉職為『魔法師』嗎?", "", "轉職為『魔法師』", "不了，我想成為『元素使』", "還是算了吧"))
            {
                case 1:
                    JobBasic_05_mask.SetValue(JobBasic_05.魔法師轉職任務完成, true);
                    break;

                case 2:
                    JobBasic_05_mask.SetValue(JobBasic_05.選擇轉職為魔法師, false);
                    JobBasic_05_mask.SetValue(JobBasic_05.已經從魔術那裡聽取有關新生魔法的知識, false);
                    JobBasic_06_mask.SetValue(JobBasic_06.選擇轉職為元素使, true);

                    Say(pc, 11000019, 131, "$R您是想做『元素使』啊?$R;", "魔法系總管");

                    Say(pc, 11000019, 131, "哈哈，您想成為『元素使』嗎?$R;" +
                                           "$R要成為元素使，$R;" +
                                           "就先要理解「屬性魔法」。$R;" +
                                           "$P本來我教您的話是最好的。$R;" +
                                           "不過我身為總管實在是太忙了。$R;" +
                                           "$P在「阿高普路斯市」的「下城」$R;" +
                                           "有個對「屬性魔法」很熟悉的人。$R;" +
                                           "$R她比我厲害多呢!$R;" +
                                           "$P她的名字叫「雪莉」。$R;" +
                                           "$R是個美麗又神秘的女生，$R;" +
                                           "要是我年輕個十歲，早就去追了。$R;" +
                                           "$P去她那裡學習「屬性魔法」吧。$R;" +
                                           "$P…一定要幫我向她問好喔。$R;", "魔法系總管");
                    break;

                case 3:
                    Say(pc, 11000019, 131, "什麼? 不做了?$R;", "魔法系總管");
                    break;
            }
        }

        void 新生魔法相關問題回答錯誤(ActorPC pc)
        {
            BitMask<JobBasic_05> JobBasic_05_mask = new BitMask<JobBasic_05>(pc.CMask["JobBasic_05"]);

            JobBasic_05_mask.SetValue(JobBasic_05.已經從魔術那裡聽取有關新生魔法的知識, false);

            PlaySound(pc, 2041, false, 100, 50);

            Say(pc, 11000019, 131, "…$R;" +
                                   "$P您好像還沒有理解透徹呢?$R;" +
                                   "再去聽一遍吧!$R;", "魔法系總管");
        }

        void 申請轉職為魔法師(ActorPC pc)
        {
            BitMask<JobBasic_05> JobBasic_05_mask = new BitMask<JobBasic_05>(pc.CMask["JobBasic_05"]);

            Say(pc, 11000019, 131, "那麼就給您紋上這象徵『魔法師』的，$R;" +
                                   "『魔法師紋章』吧!$R;", "魔法系總管");

            if (pc.Inventory.Equipments.Count == 0)
            {
                JobBasic_05_mask.SetValue(JobBasic_05.魔法師轉職成功, true);

                PlaySound(pc, 3087, false, 100, 50);
                ShowEffect(pc, 4131);
                Wait(pc, 3960);

                Say(pc, 11000019, 131, "…$R;" +
                                       "$P好棒啊，$R;" +
                                       "您身上已經烙印了漂亮的紋章。$R;" +
                                       "$R從今以後，$R;" +
                                       "您就成為代表我們的『魔法師』了。$R;", "魔法系總管");

                PlaySound(pc, 4012, false, 100, 50);
                ChangePlayerJob(pc, PC_JOB.WIZARD);
                Say(pc, 0, 0, "您已經轉職為『魔法師』了!$R;", " ");

                Say(pc, 11000019, 131, "先穿上衣服後，再和我說話吧。$R;" +
                                       "有一份小禮物，要送給您唷$!R;" +
                                       "$R您先去整理行李後，再來找我吧。$R;", "魔法系總管");
            }
            else
            {
                Say(pc, 11000019, 131, "紋章是烙印在皮膚上的，$R;" +
                                       "先把裝備脫掉吧。$R;", "魔法系總管");
            }
        }

        void 魔法師轉職完成(ActorPC pc)
        {
            BitMask<JobBasic_05> JobBasic_05_mask = new BitMask<JobBasic_05>(pc.CMask["JobBasic_05"]);

            if (pc.Inventory.Equipments.Count != 0)
            {
                JobBasic_05_mask.SetValue(JobBasic_05.已經轉職為魔法師, true);

                Say(pc, 11000019, 131, "給您『蛋白石項鏈墜』，$R;" +
                                       "$R您要繼續努力喔。$R;", "魔法系總管");

                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, 50050800, 1);
                Say(pc, 0, 0, "得到『蛋白石項鏈墜』!$R;", " ");

                LearnSkill(pc, 3001);
                Say(pc, 0, 0, "學到『魔法片』!R;", " ");
            }
            else
            {
                Say(pc, 11000019, 131, "先穿上衣服後，再和我說話吧。$R;", "魔法系總管");
            }
        }

        void 元素使轉職任務(ActorPC pc)
        {
            BitMask<JobBasic_06> JobBasic_06_mask = new BitMask<JobBasic_06>(pc.CMask["JobBasic_06"]);

            if (!JobBasic_06_mask.Test(JobBasic_06.元素使轉職任務完成))
            {
                屬性魔法相關問題回答(pc);
            }

            if (JobBasic_06_mask.Test(JobBasic_06.元素使轉職任務完成) &&
                !JobBasic_06_mask.Test(JobBasic_06.元素使轉職成功))
            {
                申請轉職為元素使(pc);
                return;
            }
        }

        void 屬性魔法相關問題回答(ActorPC pc)
        {
            BitMask<JobBasic_06> JobBasic_06_mask = new BitMask<JobBasic_06>(pc.CMask["JobBasic_06"]);

            if (JobBasic_06_mask.Test(JobBasic_06.已經從雪莉那裡聽取有關屬性魔法的知識))
            {
                屬性魔法相關問題01(pc);
            }
            else
            {
                Say(pc, 11000019, 131, "再跟您說一遍吧。$R;" +
                                       "$R到「阿高普路斯市」的「下城」$R;" +
                                       "去找一個叫「雪莉」的女生，$R;" +
                                       "學習「屬性魔法」的知識吧!$R;", "魔法系總管");
            }
        }

        void 屬性魔法相關問題01(ActorPC pc)
        {
            Say(pc, 11000019, 131, "回來了。$R;" +
                                   "$P嗯…$R;" +
                                   "那我要考驗一下您的實力囉!$R;" +
                                   "$R哪個屬性比「火屬性」強?$R;", "魔法系總管");

            switch (Select(pc, "比「火屬性」強的是?", "", "火屬性", "水屬性", "土屬性", "風屬性"))
            {
                case 1:
                    屬性魔法相關問題回答錯誤(pc);
                    break;

                case 2:
                    屬性魔法相關問題回答錯誤(pc);
                    break;

                case 3:
                    屬性魔法相關問題回答錯誤(pc);
                    break;

                case 4:
                    屬性魔法相關問題02(pc);
                    break;
            }
        }

        void 屬性魔法相關問題02(ActorPC pc)
        {
            PlaySound(pc, 2040, false, 100, 50);

            Say(pc, 11000019, 131, "呵呵，挺不錯啊。$R;" +
                                   "$R那麼下一個問題。$R;" +
                                   "$P哪個屬性比「水屬性」弱?$R;", "魔法系總管");

            switch (Select(pc, "比「水屬性」弱的是?", "", "火屬性", "水屬性", "土屬性", "風屬性"))
            {
                case 1:
                    屬性魔法相關問題回答錯誤(pc);
                    break;

                case 2:
                    屬性魔法相關問題回答錯誤(pc);
                    break;

                case 3:
                    屬性魔法相關問題03(pc);
                    break;

                case 4:
                    屬性魔法相關問題回答錯誤(pc);
                    break;
            }
        }

        void 屬性魔法相關問題03(ActorPC pc)
        {
            PlaySound(pc, 2040, false, 100, 50);

            Say(pc, 11000019, 131, "好!$R;" +
                                   "$R那麼這是最後的問題了。$R;" +
                                   "$P哪個屬性跟「風屬性」沒有關係呢?$R;", "魔法系總管");

            switch (Select(pc, "跟「風屬性」沒有關係的屬性是?", "", "火屬性", "水屬性", "土屬性", "風屬性"))
            {
                case 1:
                    屬性魔法相關問題回答錯誤(pc);
                    break;

                case 2:
                    屬性魔法相關問題回答正確(pc);
                    break;

                case 3:
                    屬性魔法相關問題回答錯誤(pc);
                    break;

                case 4:
                    屬性魔法相關問題回答錯誤(pc);
                    break;
            }
        }

        void 屬性魔法相關問題回答正確(ActorPC pc)
        {
            BitMask<JobBasic_05> JobBasic_05_mask = new BitMask<JobBasic_05>(pc.CMask["JobBasic_05"]);
            BitMask<JobBasic_06> JobBasic_06_mask = new BitMask<JobBasic_06>(pc.CMask["JobBasic_06"]);

            PlaySound(pc, 2040, false, 100, 50);

            Say(pc, 11000019, 131, "沒錯!$R;" +
                                   "$R理解屬性對『元素使』來說，$R;" +
                                   "是非常重要的。$R;" +
                                   "千萬不可以忘記喔!$R;" +
                                   "$P那您是不是要做『元素使』呢?$R;", "魔法系總管");

            switch (Select(pc, "要轉職為『元素使』嗎?", "", "轉職為『元素使』", "不了，我想成為『魔法師』", "還是算了吧"))
            {
                case 1:
                    JobBasic_06_mask.SetValue(JobBasic_06.元素使轉職任務完成, true);
                    break;

                case 2:
                    JobBasic_05_mask.SetValue(JobBasic_05.選擇轉職為魔法師, true);
                    JobBasic_06_mask.SetValue(JobBasic_06.選擇轉職為元素使, false);
                    JobBasic_06_mask.SetValue(JobBasic_06.已經從雪莉那裡聽取有關屬性魔法的知識, false);

                    Say(pc, 11000019, 131, "$R您是想做『魔法師』啊?$R;", "魔法系總管");

                    Say(pc, 11000019, 131, "哈哈，您想成為『魔法師』嗎?$R;" +
                                           "$R要成為魔法師，$R;" +
                                           "就先要理解「新生魔法」喔!$R;" +
                                           "$P本來我能教您的話是最好的，$R;" +
                                           "不過我身為總管實在是太忙了。$R;" +
                                           "$P在「阿高普路斯市」的「下城」$R;" +
                                           "有個對「新生魔法」很熟悉的傢伙。$R;" +
                                           "$R當然他沒有我懂得多啦。$R;" +
                                           "他的名字叫「魔術」。$R;" +
                                           "$R那小子雖然有點輕率，$R;" +
                                           "但是也沒有別的人選了。$R;" +
                                           "$R沒有辦法啊~~!$R;" +
                                           "$P去那小子那裡學習「新生魔法」吧。$R;", "魔法系總管");
                    break;

                case 3:
                    Say(pc, 11000019, 131, "什麼? 不做了?$R;", "魔法系總管");
                    break;
            }
        }

        void 屬性魔法相關問題回答錯誤(ActorPC pc)
        {
            BitMask<JobBasic_06> JobBasic_06_mask = new BitMask<JobBasic_06>(pc.CMask["JobBasic_06"]);

            JobBasic_06_mask.SetValue(JobBasic_06.已經從雪莉那裡聽取有關屬性魔法的知識, false);

            PlaySound(pc, 2041, false, 100, 50);

            Say(pc, 11000019, 131, "…$R;" +
                                   "$P您好像還沒有理解透徹呢?$R;" +
                                   "再去聽一遍吧!$R;", "魔法系總管");
        }

        void 申請轉職為元素使(ActorPC pc)
        {
            BitMask<JobBasic_06> JobBasic_06_mask = new BitMask<JobBasic_06>(pc.CMask["JobBasic_06"]);

            Say(pc, 11000019, 131, "那麼就給您紋上這象徵『元素使』的，$R;" +
                                   "『元素使紋章』吧!$R;", "魔法系總管");

            if (pc.Inventory.Equipments.Count == 0)
            {
                JobBasic_06_mask.SetValue(JobBasic_06.元素使轉職成功, true);

                PlaySound(pc, 3087, false, 100, 50);
                ShowEffect(pc, 4131);
                Wait(pc, 3960);

                Say(pc, 11000019, 131, "…$R;" +
                                       "$P好棒啊，$R;" +
                                       "您身上已經烙印了漂亮的紋章。$R;" +
                                       "$R從今以後，$R;" +
                                       "您就成為代表我們的『元素使』了。$R;", "魔法系總管");

                PlaySound(pc, 4012, false, 100, 50);
                ChangePlayerJob(pc, PC_JOB.SHAMAN);
                Say(pc, 0, 0, "您已經轉職為『元素使』了!$R;", " ");


                Say(pc, 11000019, 131, "先穿上衣服後，再和我說話吧。$R;" +
                                       "有一份小禮物，要送給您唷!$R;" +
                                       "$R您先去整理行李後，再來找我吧。$R;", "魔法系總管");
            }
            else
            {
                Say(pc, 11000019, 131, "紋章是烙印在皮膚上的，$R;" +
                                       "先把裝備脫掉吧。$R;", "魔法系總管");
            }
        }

        void 元素使轉職完成(ActorPC pc)
        {
            BitMask<JobBasic_06> JobBasic_06_mask = new BitMask<JobBasic_06>(pc.CMask["JobBasic_06"]);

            if (pc.Inventory.Equipments.Count != 0)
            {
                JobBasic_06_mask.SetValue(JobBasic_06.已經轉職為元素使, true);

                Say(pc, 11000019, 131, "給您『精靈的剛玉』，$R;" +
                                       "$R您就用心練吧。$R;", "魔法系總管");

                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, 50052400, 1);
                Say(pc, 0, 0, "得到『精靈的剛玉』!$R;", " ");

                switch (Select(pc, "要學習什麼樣的魔法呢?", "", "紅蓮焰", "寒冰箭", "大地震盪", "狂風之刃"))
                {
                    case 1:
                        LearnSkill(pc, 3006);

                        Say(pc, 0, 0, "學到『紅蓮焰』!R;", " ");
                        break;
                    case 2:
                        LearnSkill(pc, 3029);

                        Say(pc, 0, 0, "學到『寒冰箭』!R;", " ");
                        break;
                    case 3:
                        LearnSkill(pc, 3041);

                        Say(pc, 0, 0, "學到『大地震盪』!R;", " ");
                        break;
                    case 4:
                        LearnSkill(pc, 3017);

                        Say(pc, 0, 0, "學到『狂風之刃』!R;", " ");
                        break;
                }
            }
            else
            {
                Say(pc, 11000019, 131, "先穿上衣服後，再和我說話吧。$R;", "魔法系總管");
            }
        }

        void 進階轉職(ActorPC pc)
        {
            BitMask<Job2X_05> Job2X_05_mask = pc.CMask["Job2X_05"];
            BitMask<Job2X_06> Job2X_06_mask = pc.CMask["Job2X_06"];

            if (Job2X_06_mask.Test(Job2X_06.轉職完成))//_3A92 && _3A94)
            {
                Say(pc, 131, "不能再轉職了$R;");
                return;
            }

            if (pc.Job == PC_JOB.SHAMAN && pc.JobLevel1 >= 30)
            {
                Say(pc, 131, "呵呵…還要挑戰更高級別嗎？$R;" +
                    "$R嗯…$R;" +
                    "$P對元素術師有興趣的話$R;" +
                    "再聽聽說明吧$R;");
                int a = 0;
                while (a == 0)
                {
                    switch (Select(pc, "怎麼辦？？", "", "我要做『元素術師』喔！", "『元素術師』是怎樣的職業？", "任務服務台", "什麼都不做"))
                    {
                        case 1:
                            if (pc.Mag > 29)
                            {
                                Job2X_06_mask.SetValue(Job2X_06.進階轉職開始, true);
                                //_3A93 = true;
                                //_3A89 = false;
                                Say(pc, 131, "呵呵…$R;" +
                                    "您想成為元素術師吧？$R;" +
                                    "$R想成為元素術師的話$R;" +
                                    "到火焰、水靈、神風、大地四個精靈$R;" +
                                    "居住的地方一趟$R;" +
                                    "$P得到他們的守護再回來吧$R;" +
                                    "$P這是漫長的旅程$R;" +
                                    "萬事要小心喔！$R;" +
                                    "$P…小心不要失手喔$R;");
                                return;
                            }
                            Say(pc, 131, "想要成為元素術師，魔力是很重要的。$R;" +
                                "$R先去提升『魔力』後，再來吧。$R;" +
                                "其他的以後再説啦。$R;");
                            return;
                        case 2:
                            Say(pc, 131, "跟咒術師一樣，元素術師這職業比較適合$R;" +
                                "塔妮亞和道米尼族的體質唷$R;" +
                                "$R埃米爾選擇這個職業的話$R會很辛苦的。$R;" +
                                "$P這樣還想聽下去嗎？$R;");
                            switch (Select(pc, "聽下去嗎?", "", "我要聽", "不聽"))
                            {
                                case 1:
                                    Say(pc, 131, "元素術師是咒術師的上位職業$R;" +
                                        "$P是能夠得到火焰、水靈、神風、$R;" +
                                        "大地四種精靈擁有，構成世界的四種$R;" +
                                        "力量的職業，所以叫元素術師$R;" +
                                        "$P跟咒術師一樣，$R;" +
                                        "元素術師能夠操縱各屬性的攻撃魔法，$R;" +
                                        "還能控制自己的屬性能力。$R;" +
                                        "$P隊伍裡，如果有元素術師的話$R;" +
                                        "一定會發揮强大的力量。$R;" +
                                        "$P但如果没能透徹理解屬性$R;" +
                                        "力量的話，$R;" +
                                        "$R就不能充分發揮其威力了喔$R;");
                                    break;
                                case 2:
                                    return;
                            }
                            break;
                        case 3:

                            return;
                        case 4:
                            return;
                    }
                }
                return;
            }

            if (pc.Job == PC_JOB.WIZARD && pc.JobLevel1 >= 30)
            {
                Say(pc, 131, "哈哈，還要挑戰更高的級別嗎？$R;" +
                    "呵呵，$R如果對魔導士有興趣的話$R;" +
                    "$P聽聽我的話$R;" +
                    "您再走也不遲。$R;");
                int a = 0;
                while (a == 0)
                {
                    switch (Select(pc, "做什麼呢?", "", "我要做『魔導士』喔！", "『魔導士』是怎樣的職業？", "任務服務台", "什麼也不做"))
                    {
                        case 1:
                            if (pc.Mag > 29)
                            {
                                Job2X_05_mask.SetValue(Job2X_05.轉職開始, true);
                                //_3A89 = true;
                                //_3A99 = false;
                                //_3A95 = false;
                                //_3A93 = false;
                                //_3A96 = false;
                                //_3A97 = false;
                                //_3A98 = false;
                                Say(pc, 131, "呵呵…$R;" +
                                    "想成為魔導士吧？$R;" +
                                    "如果想成為魔導士$R;" +
                                    "必須得到大導師認可$R;" +
                                    "$P『諾頓市』中央有魔法行會總部$R;" +
                                    "去那裡見大導師吧$R;" +
                                    "$P他會告訴您消息的$R;" +
                                    "$P我可以教您的只有這些…$R;" +
                                    "祝您好運$R;");
                                return;
                            }
                            Say(pc, 131, "想要成為魔導士，魔力是很重要的。$R;" +
                                "$R先去提升『魔力』後，再來吧。$R;" +
                                "其他的以後再説啦。$R;");
                            return;
                        case 2:
                            Say(pc, 131, "魔導士這職業比較適合$R;" +
                                "塔妮亞和道米尼族的體質唷$R;" +
                                "$R埃米爾選擇這個職業的話$R會很辛苦的。$R;" +
                                "$P這樣還想聽下去嗎？$R;");
                            switch (Select(pc, "聽下去嗎?", "", "我要聽", "不聽"))
                            {
                                case 1:
                                    Say(pc, 131, "魔導士是魔法師的上位職業$R;" +
                                        "$P魔力要比魔法師高多了呢$R;" +
                                        "$P真想永遠留著那種魔力$R;" +
                                        "…可以的話多好呢…$R;" +
                                        "呵呵呵…$R;" +
                                        "$P如果新生魔法的研究繼續發展$R;" +
                                        "$R突破次元障壁的魔法$R也不算是妙想天開喔…$R;" +
                                        "$P魔導士可以使用至今留傳的輔助魔法$R;" +
                                        "可以說是魔法界向專家喔！$R;");
                                    break;
                                case 2:
                                    return;
                            }
                            break;
                        case 3:
                            return;
                        case 4:
                            return;
                    }
                }
                return;
            }
            Say(pc, 131, "您還未達到申請轉職的條件。$R;");
        }

        void 進階元素術師(ActorPC pc)
        {
            BitMask<Job2X_06> Job2X_06_mask = pc.CMask["Job2X_06"];

            if (Job2X_06_mask.Test(Job2X_06.朝拜大地精靈) &&
                Job2X_06_mask.Test(Job2X_06.朝拜神風精靈) &&
                Job2X_06_mask.Test(Job2X_06.朝拜水之精靈) &&
                Job2X_06_mask.Test(Job2X_06.朝拜炎之精靈))
            {
                Say(pc, 131, "好像真的去了吧$R;" +
                    "$P嗯…$R;" +
                    "那麼問您幾條有關最核心的問題$R;" +
                    "$R不可馬虎了事的…$R;" +
                    "東域哪種屬性最強?$R;");
                switch (Select(pc, "在東域哪種屬性最強??", "", "火焰", "水靈", "大地", "神風"))
                {
                    case 1:
                        break;
                    case 2:
                        PlaySound(pc, 2040, false, 100, 50);
                        Say(pc, 131, "呵呵…不錯，$R那麼下一條問題是在新路寶雪原$R哪一種屬性最強?$R;");
                        switch (Select(pc, "在新路寶雪原哪一種屬性最強?", "", "火焰", "水靈", "大地", "神風"))
                        {
                            case 1:
                                PlaySound(pc, 2040, false, 100, 50);
                                Say(pc, 131, "好，那麼最後一條問題$R在南域哪一種屬性最強？$R;");
                                switch (Select(pc, "在南域哪一種屬性最強？", "", "火焰", "水靈", "大地", "神風"))
                                {
                                    case 1:
                                        break;
                                    case 2:
                                        break;
                                    case 3:
                                        break;
                                    case 4:
                                        Job2X_06_mask.SetValue(Job2X_06.所有問題回答正確, true);
                                        //_3A99 = true;
                                        PlaySound(pc, 2040, false, 100, 50);
                                        Say(pc, 131, "嗯…對元素術師來說$R;" +
                                            "了解屬性是最重要的$R;" +
                                            "不可以忘記喔！$R;" +
                                            "那麼真的要成為『元素術師』嗎？$R;");

                                        switch (Select(pc, "要轉職成為『元素術師』嗎？", "", "成為元素術師", "算了吧"))
                                        {
                                            case 1:
                                                Say(pc, 131, "那麼就給您烙印上這象徵元素術師的$R;" +
                                                    "『元素術師紋章』吧$R;");
                                                if (pc.Inventory.Equipments.Count == 0)
                                                {
                                                    Job2X_06_mask.SetValue(Job2X_06.轉職完成, true);
                                                    Job2X_06_mask.SetValue(Job2X_06.進階轉職開始, false);
                                                    Job2X_06_mask.SetValue(Job2X_06.朝拜大地精靈, false);
                                                    Job2X_06_mask.SetValue(Job2X_06.朝拜神風精靈, false);
                                                    Job2X_06_mask.SetValue(Job2X_06.朝拜水之精靈, false);
                                                    Job2X_06_mask.SetValue(Job2X_06.朝拜炎之精靈, false);
                                                    Job2X_06_mask.SetValue(Job2X_06.防禦過高, false);
                                                    Job2X_06_mask.SetValue(Job2X_06.所有問題回答正確, false);

                                                    ChangePlayerJob(pc, PC_JOB.ELEMENTER);
                                                    pc.JEXP = 0;

                                                    PlaySound(pc, 3087, false, 100, 50);
                                                    ShowEffect(pc, 4131);
                                                    Wait(pc, 4000);
                                                    Say(pc, 131, "…$R;" +
                                                        "$P好棒啊，$R;" +
                                                        "您身上已經烙印了漂亮的紋章。$R;" +
                                                        "$R從今以後，$R您就成為代表我們的『元素術師』了。$R;");
                                                    PlaySound(pc, 4012, false, 100, 50);
                                                    Say(pc, 131, "您已轉職為『元素術師』了。$R;");
                                                    Say(pc, 131, "以後每天都要很真誠待人喔$R;");
                                                    return;
                                                }
                                                Job2X_06_mask.SetValue(Job2X_06.防禦過高, true);
                                                //_4A13 = true;
                                                Say(pc, 131, "防禦太高的話，就無法烙印紋章了$R;" +
                                                    "請換上輕便的服裝後，再來吧。$R;");
                                                return;
                                            case 2:
                                                Say(pc, 131, "是嗎？$R;" +
                                                    "要是想法的話，再來找我吧。$R;");
                                                return;
                                        }
                                        break;
                                }
                                break;
                            case 2:
                                break;
                            case 3:
                                break;
                            case 4:
                                break;
                        }
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                }
                Job2X_06_mask.SetValue(Job2X_06.朝拜大地精靈, false);
                Job2X_06_mask.SetValue(Job2X_06.朝拜神風精靈, false);
                Job2X_06_mask.SetValue(Job2X_06.朝拜水之精靈, false);
                Job2X_06_mask.SetValue(Job2X_06.朝拜炎之精靈, false);
                PlaySound(pc, 2041, false, 100, 50);
                Say(pc, 131, "…$R;" +
                    "$P您看起來還是不太明白，$R;" +
                    "還是再去一趟嗎？$R;");
                return;
            }
            Say(pc, 131, "什麼?那麼快就回來了？$R;" +
                "$R不可馬虎了事的…$R;");
        }
    }
}

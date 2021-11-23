using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:冒險家行會(30050000) NPC基本信息:冒險家總管(11000025) X:3 Y:3
namespace SagaScript.M30050000
{
    public class S11000025 : Event
    {
        public S11000025()
        {
            this.EventID = 11000025;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<JobBasic_11> JobBasic_11_mask = new BitMask<JobBasic_11>(pc.CMask["JobBasic_11"]);

            Say(pc, 11000025, 131, "這裡是冒險家行會。$R;", "冒險家總管");

            if (JobBasic_11_mask.Test(JobBasic_11.冒險家轉職成功) &&
                !JobBasic_11_mask.Test(JobBasic_11.已經轉職為冒險家))
            {
                冒險家轉職完成(pc);
                return;
            }

            if (pc.Job == PC_JOB.NOVICE && pc.Race != PC_RACE.DEM)
            {
                if (JobBasic_11_mask.Test(JobBasic_11.選擇轉職為冒險家) &&
                    !JobBasic_11_mask.Test(JobBasic_11.已經轉職為冒險家))
                {
                    冒險家轉職任務(pc);
                    return;
                }
                else
                {
                    冒險家簡介(pc);
                    return;
                }
            }

            if (pc.Job == PC_JOB.RANGER)
            {
                Say(pc, 131, "這不是" + pc.Name + "嗎？！$R;" +
                    "$R這裡是怎麼了?$R;");
                //EVT1100002560
                switch (Select(pc, "做什麼呢？", "", "任務服務台", "我要轉職", "出售帕斯特入國許可證", "什麼也不做"))
                {
                    case 1:
                        HandleQuest(pc, 45);
                        break;
                    case 2:
                        進階轉職(pc);
                        break;
                    case 3:
                        OpenShopBuy(pc, 150);
                        Say(pc, 131, "帕斯特裡會有行會特派員的。$R;" +
                            "$R去打個招呼吧。$R;");
                        break;
                    case 4:
                        break;
                }
            }
        }

        void 冒險家簡介(ActorPC pc)
        {
            BitMask<JobBasic_11> JobBasic_11_mask = new BitMask<JobBasic_11>(pc.CMask["JobBasic_11"]);

            int selection;

            Say(pc, 11000025, 131, "我是管理冒險家們的冒險家總管。$R;" +
                                   "$P想不想成為『冒險家』呢?$R;" +
                                   "$R想不想聽我說明啊?$R;", "冒險家總管");

            selection = Select(pc, "想做什麼?", "", "我想成為『冒險家』!", "『冒險家』是什麼樣的職業?", "任務服務台", "什麼也不做");

            while (selection != 4)
            {
                switch (selection)
                {
                    case 1:
                        //廢除一次職轉職任務
                        JobBasic_11_mask.SetValue(JobBasic_11.選擇轉職為冒險家, true);
                        JobBasic_11_mask.SetValue(JobBasic_11.冒險家轉職任務完成, true);
                        /*Say(pc, 11000025, 131, "想加入我們一起探險。$R;" +
                                               "$R那就要想辦法取得我的認同。$R;" +
                                               "$P…要接受我的測試嗎?$R;", "冒險家總管");

                        switch (Select(pc, "接受嗎?", "", "沒問題", "才不要"))
                        {
                            case 1:
                                JobBasic_11_mask.SetValue(JobBasic_11.選擇轉職為冒險家, true);

                                Say(pc, 11000025, 131, "想辦法收集『骨頭』2個，$R;" +
                                                       "再回到這吧。$R;", "冒險家總管");
                                break;

                            case 2:
                                Say(pc, 11000025, 131, "……，$R;" +
                                                       "考慮清楚再來吧。$R;", "冒險家總管");
                                break;
                        }
                        */
                        申請轉職為冒險家(pc);
                        return;

                    case 2:
                        Say(pc, 11000025, 131, "冒險家是適合埃米爾族的職業，$R;" +
                                               "其他種族就不適合喔!$R;" +
                                               "$R那還要聽嗎?$R;", "冒險家總管");

                        switch (Select(pc, "還要聽下去嗎?", "", "我要聽", "不聽"))
                        {
                            case 1:
                                Say(pc, 11000025, 131, "『冒險家』代表性的能力是野營。$R;" +
                                                       "即使不在村落的地方也能恢復。$R;" +
                                                       "$P所以出去打獵的時候，$R;" +
                                                       "直到東西裝不下為止，才會回來。$R;" +
                                                       "$P冒險家在多方面都具有豐富的知識。$R;" +
                                                       "也可以從魔物那裡得到稀有的道具。$R;" +
                                                       "$P以『冒險家』的身份進行修煉的話，$R;" +
                                                       "還可以解除鎖頭或者陷阱。$R;" +
                                                       "$R是搜集道具的最佳職業喔!$R;", "冒險家總管");
                                break;

                            case 2:
                                break;
                        }
                        break;

                    case 3:
                        Say(pc, 11000025, 131, "想做事嗎?$R;" +
                                               "$R不好意思，$R;" +
                                               "這裡只給『冒險家』介紹工作$R;" +
                                               "$P怎樣?$R;" +
                                               "要不要成為『冒險家』?$R;", "冒險家總管");
                        break;
                }
                selection = Select(pc, "想做什麼?", "", "我想成為『冒險家』!", "『冒險家』是什麼樣的職業?", "任務服務台", "什麼也不做");
            } 
        }

        void 冒險家轉職任務(ActorPC pc)
        {
            BitMask<JobBasic_11> JobBasic_11_mask = new BitMask<JobBasic_11>(pc.CMask["JobBasic_11"]);

            if (!JobBasic_11_mask.Test(JobBasic_11.冒險家轉職任務完成))
            {
                給予骨頭(pc);
            }

            if (JobBasic_11_mask.Test(JobBasic_11.冒險家轉職任務完成) &&
                !JobBasic_11_mask.Test(JobBasic_11.冒險家轉職成功))
            {
                申請轉職為冒險家(pc);
                return;
            }
        }

        void 給予骨頭(ActorPC pc)
        {
            BitMask<JobBasic_11> JobBasic_11_mask = new BitMask<JobBasic_11>(pc.CMask["JobBasic_11"]);

            if (CountItem(pc, 10006600) > 1)
            {
                Say(pc, 11000025, 131, "好棒!!$R;" +
                                       "$R已經取得『骨頭』了!$R;" +
                                       "$P那要成為『冒險家』嗎?$R;", "冒險家總管");

                switch (Select(pc, "要轉職為『冒險家』嗎?", "", "轉職為『冒險家』", "還是算了吧"))
                {
                    case 1:
                        JobBasic_11_mask.SetValue(JobBasic_11.冒險家轉職任務完成, true);

                        TakeItem(pc, 10006600, 2);
                        break;

                    case 2:
                        break;
                }
            }
            else
            {
                Say(pc, 11000025, 131, "打倒「巴鳴」會掉落『骨頭』。$R;" +
                                       "$R蒐集兩個『骨頭』後，;" +
                                       "再回來找我吧!$R;", "冒險家總管");
            }
        }

        void 申請轉職為冒險家(ActorPC pc)
        {
            BitMask<JobBasic_11> JobBasic_11_mask = new BitMask<JobBasic_11>(pc.CMask["JobBasic_11"]);

            Say(pc, 11000025, 131, "那麼就給您紋上這象徵『冒險家』的，$R;" +
                                   "『冒險家紋章』。$R;", "冒險家總管");

            if (pc.Inventory.Equipments.Count == 0)
            {
                JobBasic_11_mask.SetValue(JobBasic_11.冒險家轉職成功, true);

                PlaySound(pc, 3087, false, 100, 50);
                ShowEffect(pc, 4131);
                Wait(pc, 3960);

                Say(pc, 11000025, 131, "…$R;" +
                                       "$P好棒啊，$R;" +
                                       "您身上已經烙印了漂亮的紋章。$R;" +
                                       "$R從今以後，$R;" +
                                       "您就成為代表我們的『冒險家』了。$R;", "冒險家總管");

                PlaySound(pc, 4012, false, 100, 50);
                ChangePlayerJob(pc, PC_JOB.RANGER);

                Say(pc, 0, 0, "您已經轉職為『冒險家』了!$R;", " ");

                Say(pc, 11000025, 131, "先穿上衣服後，再和我說話吧。$R;" +
                                       "有一份小禮物，要送給您唷!$R;" +
                                       "$R您先去整理行李後，再來找我吧。$R;", "冒險家總管");
            }
            else
            {
                Say(pc, 11000025, 131, "紋章是烙印在皮膚上的，$R;" +
                                       "先把裝備脫掉吧。$R;", "冒險家總管");
            }
        }

        void 冒險家轉職完成(ActorPC pc)
        {
            BitMask<JobBasic_11> JobBasic_11_mask = new BitMask<JobBasic_11>(pc.CMask["JobBasic_11"]);

            if (pc.Inventory.Equipments.Count != 0)
            {
                JobBasic_11_mask.SetValue(JobBasic_11.已經轉職為冒險家, true);

                Say(pc, 11000025, 131, "給您『探頸護目鏡』，$R;" +
                                       "$R您就用心練吧。$R;", "冒險家總管");

                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, 50051600, 1);
                Say(pc, 0, 0, "得到『探頸護目鏡』!$R;", " ");

                LearnSkill(pc, 713);
                Say(pc, 0, 0, "學到『野營』!$R;", " ");
            }
            else
            {
                Say(pc, 11000025, 131, "先穿上衣服後，再和我說話吧。$R;", "冒險家總管");
            }
        }

        void 進階轉職(ActorPC pc)
        {
            BitMask<Job2X_11> Job2X_11_mask = pc.CMask["Job2X_11"];

            if (pc.Job == PC_JOB.RANGER && pc.JobLevel1 >= 30)
            {
                if (Job2X_11_mask.Test(Job2X_11.給予冰花))//_3a59)
                {
                    轉職選擇(pc);
                    return;
                }
                if (CountItem(pc, 50035651) >= 1 && Job2X_11_mask.Test(Job2X_11.轉職開始))//_3a46)
                {
                    Job2X_11_mask.SetValue(Job2X_11.給予冰花, true);
                    //_3a59 = true;
                    TakeItem(pc, 50035651, 1);
                    Say(pc, 131, "這不是傳説中的『冰花』嗎？$R;" +
                        "$R真漂亮啊，我能摸摸看嗎？$R;");
                    Say(pc, 131, "『冰花』融化掉了。$R;");
                    Say(pc, 131, "啊，這！$R;" +
                        "$P什麼，人只要用手摸的話就會融化？$R;" +
                        "真是，太對不起了，怎麼辦呢？$R;" +
                        "哈哈，但是您真的是$R有做探險家的資質呢。$R;" +
                        "您合格了。$R;" +
                        "$R現在開始，您就是『探險家』了$R;");
                    轉職選擇(pc);
                    return;
                }
                Job2X_11_mask.SetValue(Job2X_11.轉職開始, true);
                //_3a46 = true;
                Say(pc, 131, "轉職?$R;" +
                    "想成為『探險家』是嗎？$R;" +
                    "如果是勇敢的您，應該差不多吧。$R;" +
                    "$P但是探險家是尋遍世間寶物的$R探求心豐富的人。$R;" +
                    "$P…如果您不能證明您有這種心的話$R別人不會承認您的。$R;" +
                    "$P這個世界上還有一個古老的種族$R叫愛伊澌族。$R;" +
                    "$R希望您能找到$R據説只有他們才能找到的『冰花』。$R;" +
                    "$P但是誰也不知道他們在哪裡生活，$R而且也不知道$R;" +
                    "世上是不是真有那種花。$R;" +
                    "$R但是聽到那樣的話，$R您不會心動嗎？$R;" +
                    "$P到阿伊恩薩烏斯花店看看吧。$R;" +
                    "應該會有消息吧？?$R;" +
                    "$P那就辛苦您了$R;");
                return;
            }
            Say(pc, 131, "您現在還不能轉職。$R;" +
                "還是先去累積經驗吧。$R;");
        }

        void 轉職選擇(ActorPC pc)
        {

            switch (Select(pc, "真的要轉職嗎?", "", "我想成為探險家", "聽取關於探險家的注意事項", "還是算了吧"))
            {
                case 1:
                    Say(pc, 131, "那麼就給您烙印上這象徵探險家的$R;" +
                        "『探險家紋章』吧$R;");
                    if (pc.Inventory.Equipments.Count == 0)
                    {
                        ChangePlayerJob(pc, PC_JOB.EXPLORER);
                        pc.JEXP = 0;
                        //PARAM ME.JOB = 103
                        PlaySound(pc, 3087, false, 100, 50);
                        ShowEffect(pc, 4131);
                        Wait(pc, 4000);
                        Say(pc, 131, "…$R;" +
                            "$P好棒啊，$R;" +
                            "您身上已經烙印了漂亮的紋章。$R;" +
                            "$R從今以後，$R您就成為代表我們的『探險家』了。$R;");
                        PlaySound(pc, 4012, false, 100, 50);
                        Say(pc, 131, "您已轉職為『探險家』了。$R;");
                        return;
                    }
                    Say(pc, 131, "防禦太高的話，就無法烙印紋章了$R;" +
                        "請換上輕便的服裝後，再來吧。$R;");
                    break;
                case 2:
                    Say(pc, 131, "先跟您説清楚了，$R轉職的話，職業等級會變回1級。$R;" +
                        "$P冒險家的技能$R在轉職以後好像還可以學。$R;" +
                        "$R但是有要注意的地方。$R;" +
                        "$P『技能點數』是按照職業完全分開$R;" +
                        "$R冒險家的技能點數$R只能在冒險家的時候取得。$R;" +
                        "$P轉職之前的技能點數雖然還會留著，$R但是轉職以後就不會再增加了。$R;" +
                        "$P當然，職業等級也不會提高。$R;" +
                        "$R如果還有想學的技能的話$R就在轉職之前學吧。$R;");
                    轉職選擇(pc);
                    break;
                case 3:
                    break;
            }
        }
    }
}
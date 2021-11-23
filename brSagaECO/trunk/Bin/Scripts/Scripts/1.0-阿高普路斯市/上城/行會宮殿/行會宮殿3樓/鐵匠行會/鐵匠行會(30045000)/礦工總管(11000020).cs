using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:礦工行會(30045000) NPC基本信息:礦工總管(11000020) X:3 Y:3
namespace SagaScript.M30045000
{
    public class S11000020 : Event
    {
        public S11000020()
        {
            this.EventID = 11000020;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<JobBasic_09> JobBasic_09_mask = new BitMask<JobBasic_09>(pc.CMask["JobBasic_09"]);

            Say(pc, 11000020, 131, "喂，這裡是礦工行會。$R;", "礦工總管");

            if (JobBasic_09_mask.Test(JobBasic_09.礦工轉職成功) &&
                !JobBasic_09_mask.Test(JobBasic_09.已經轉職為礦工))
            {
                礦工轉職完成(pc);
                return;
            }

            if (pc.Job == PC_JOB.NOVICE && pc.Race != PC_RACE.DEM)
            {
                if (JobBasic_09_mask.Test(JobBasic_09.選擇轉職為礦工) &&
                    !JobBasic_09_mask.Test(JobBasic_09.已經轉職為礦工))
                {
                    礦工轉職任務(pc);
                    return;
                }
                else
                {
                    礦工簡介(pc);
                    return;
                }
            }

            if (pc.Job == PC_JOB.TATARABE)
            {
                Say(pc, 11000020, 131, pc.Name + "…$R;" +
                                       "$R什麼事？$R;", "礦工總管");

                switch (Select(pc, "做什麼好呢?", "", "任務服務台", "我想轉職", "出售帕斯特入國許可證", "什麼也不做"))
                {
                    case 1:
                        HandleQuest(pc, 46);
                        break;
                    case 2:
                        進階轉職(pc);
                        break;
                    case 3:
                        OpenShopBuy(pc, 150);
                        Say(pc, 131, "在帕斯特有行會特派員$R;" +
                            "$R見到他幫我向他問個好吧$R;");
                        break;
                    case 4:
                        break;
                }
            }
        }

        void 礦工簡介(ActorPC pc)
        {
            BitMask<JobBasic_09> JobBasic_09_mask = new BitMask<JobBasic_09>(pc.CMask["JobBasic_09"]);

            int selection;

            Say(pc, 11000020, 131, "我是管理礦工們的礦工總管$R;" +
                                   "$R咦? 您是初心者啊。$R;" +
                                   "$P您對礦工有興趣嗎?$R;" +
                                   "有興趣聽我介紹嗎?$R;", "礦工總管");

            selection = Select(pc, "想做什麼?", "", "我想成為『礦工』!", "『礦工』是什麼樣的職業?", "任務服務台", "什麼也不做");

            while (selection != 4)
            {
                switch (selection)
                {
                    case 1:
                        //廢除一次職轉職任務
                        JobBasic_09_mask.SetValue(JobBasic_09.選擇轉職為礦工, true);
                        JobBasic_09_mask.SetValue(JobBasic_09.礦工轉職任務完成, true);
                        /*Say(pc, 11000020, 131, "您說您想成為『礦工』?$R;" +
                                               "好主意!$R;" +
                                               "現在開始您就是礦工囉!$R;" +
                                               "$P…不是，不是!$R;" +
                                               "如果這麼簡單的話就不好玩了…$R;" +
                                               "$R對吧…$R;" +
                                               "應該要給您一些試煉吧?$R;" +
                                               "$P對了，如果想成為礦工的話!$R;" +
                                               "$R就要搜集三個重要的道具。$R;" +
                                               "$P給您一個提示吧!$R;" +
                                               "是『鐵X』$R;" +
                                               "$P如果您把那三個『鐵X』拿回來的話，$R;" +
                                               "就就讓您做礦工吧!$R;" +
                                               "$R怎麼樣?$R;", "礦工總管");

                        switch (Select(pc, "接受嗎?", "", "沒問題", "才不要"))
                        {
                            case 1:
                                JobBasic_09_mask.SetValue(JobBasic_09.選擇轉職為礦工, true);

                                Say(pc, 11000020, 131, "好，我會期待有好結果的唷。$R;", "礦工總管");
                                break;

                            case 2:
                                break;
                        }
                        */
                        申請轉職為礦工(pc);
                        return;

                    case 2:
                        Say(pc, 11000020, 131, "礦工是適合埃米爾的職業，$R;" +
                                               "$R那您還想聽我解說嗎?$R;", "礦工總管");

                        switch (Select(pc, "還要聽下去嗎?", "", "我要聽", "不聽"))
                        {
                            case 1:
                                Say(pc, 11000020, 131, "『礦工』的專長就是礦物採集呢。$R;" +
                                                       "$R採礦要比別的職業強得多，$R;" +
                                                       "見到岩石可不要放過喔!$R;" +
                                                       "$P將來可以成為修理和$R;" +
                                                       "加工武器的鐵匠呢。$R;" +
                                                       "$P但礦工不擅於使用武器，$R;" +
                                                       "亦不能裝備重的防具，$R;" +
                                                       "所以戰鬥力比較弱。$R;" +
                                                       "$P如果組成隊伍的話，$R;" +
                                                       "通常擔當搬運的角色$R;", "礦工總管");
                                break;

                            case 2:
                                break;
                        }
                        break;

                    case 3:
                        Say(pc, 11000020, 131, "想找工作嗎?$R;" +
                                               "$R不好意思，$R;" +
                                               "如果不是『礦工』的話$R;" +
                                               "現在還不能給您介紹工作呢!$R;" +
                                               "$P想不想成為『礦工』啊?$R;", "礦工總管");
                        break;
                }

                selection = Select(pc, "想做什麼?", "", "我想成為『礦工』!", "『礦工』是什麼樣的職業?", "任務服務台", "什麼也不做");
            } 
        }

        void 礦工轉職任務(ActorPC pc)
        {
            BitMask<JobBasic_09> JobBasic_09_mask = new BitMask<JobBasic_09>(pc.CMask["JobBasic_09"]);

            if (!JobBasic_09_mask.Test(JobBasic_09.礦工轉職任務完成))
            {
                給予鐵塊(pc);
            }

            if (JobBasic_09_mask.Test(JobBasic_09.礦工轉職任務完成) &&
                !JobBasic_09_mask.Test(JobBasic_09.礦工轉職成功))
            {
                申請轉職為礦工(pc);
                return;
            }
        }

        void 給予鐵塊(ActorPC pc)
        {
            BitMask<JobBasic_09> JobBasic_09_mask = new BitMask<JobBasic_09>(pc.CMask["JobBasic_09"]);

            if (CountItem(pc, 10015600) > 2)
            {
                Say(pc, 11000020, 131, "做的很好!!$R;" +
                                       "答案就是『鐵塊』!$R;" +
                                       "$R好，承認您是『礦工』了，$R;" +
                                       "$P真的要成為『礦工』嗎?$R;", "礦工總管");

                switch (Select(pc, "要轉職為『礦工』嗎?", "", "轉職為『礦工』", "還是算了吧"))
                {
                    case 1:
                        JobBasic_09_mask.SetValue(JobBasic_09.礦工轉職任務完成, true);

                        TakeItem(pc, 10015600, 3);
                        break;

                    case 2:
                        Say(pc, 11000020, 131, "考慮清楚再來。$R;", "礦工總管");
                        break;
                }
            }
            else
            {
                Say(pc, 11000020, 131, "給您一個提示吧!是『鐵X』$R;" +
                "$P如果您把那三個『鐵X』拿回來的話，$R;" +
                "就就讓您做礦工吧!$R;", "礦工總管");
            }
        }

        void 申請轉職為礦工(ActorPC pc)
        {
            BitMask<JobBasic_09> JobBasic_09_mask = new BitMask<JobBasic_09>(pc.CMask["JobBasic_09"]);

            Say(pc, 11000020, 131, "那麼就請您收下代表『礦工』的$R;" +
                                   "『礦工紋章』吧。$R;", "礦工總管");

            if (pc.Inventory.Equipments.Count == 0)
            {
                JobBasic_09_mask.SetValue(JobBasic_09.礦工轉職成功, true);

                PlaySound(pc, 3087, false, 100, 50);
                ShowEffect(pc, 4131);
                Wait(pc, 3960);

                Say(pc, 11000020, 131, "…$R;" +
                                       "$P好棒啊，$R;" +
                                       "您身上已經烙印了漂亮的紋章。$R;" +
                                       "$R從今以後，$R;" +
                                       "您就成為『礦工』了。$R;", "礦工總管");

                PlaySound(pc, 4012, false, 100, 50);
                ChangePlayerJob(pc, PC_JOB.TATARABE);

                Say(pc, 0, 0, "您已經轉職為『礦工』了!$R;", " ");

                Say(pc, 11000020, 131, "先穿上衣服後，再和我說話吧。$R;" +
                                       "有一份小禮物，要送給您唷!$R;" +
                                       "$R您先去整理行李後，再來找我吧。$R;", "礦工總管");
            }
            else
            {
                Say(pc, 11000020, 131, "紋章是烙印在皮膚上的，$R;" +
                                       "先把裝備脫掉吧。$R;", "礦工總管");
            }
        }

        void 礦工轉職完成(ActorPC pc)
        {
            BitMask<JobBasic_09> JobBasic_09_mask = new BitMask<JobBasic_09>(pc.CMask["JobBasic_09"]);

            if (pc.Inventory.Equipments.Count != 0)
            {
                JobBasic_09_mask.SetValue(JobBasic_09.已經轉職為礦工, true);

                Say(pc, 11000020, 131, "給您『礦工頭巾』和『礦工火爐』，$R;" +
                                       "$R用『礦工火爐』來煉鐵吧。$R;" +
                                       "會做出來好東西的。$R;", "礦工總管");

                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, 50021001, 1);
                GiveItem(pc, 10033100, 1);
                Say(pc, 0, 0, "得到『礦工頭巾』和『礦工火爐』!$R;", " ");

                LearnSkill(pc, 800);
                Say(pc, 0, 0, "學到 『鐵礦知識』!$R;", " ");
            }
            else
            {
                Say(pc, 11000020, 131, "先穿上衣服後，再和我說話吧。$R;", "礦工總管");
            }
        }

        void 進階轉職(ActorPC pc)
        {
            BitMask<Job2X_09> Job2X_09_mask = pc.CMask["Job2X_09"];
            if (pc.Job == PC_JOB.TATARABE && pc.JobLevel1 >= 30)
            {
                /*
                if (_3a60)
                {
                    鐵匠轉職(pc);
                    return;
                }
                */
                        if (Job2X_09_mask.Test(Job2X_09.使用塞爾曼德的心臟))//_3a45)
                {
                    Say(pc, 131, "呵呵！您想成為鐵匠？$R;" +
                        "不錯不錯$R;" +
                        "$R在您心臟裡的塞爾曼德$R;" +
                        "會一直幫助您的$R;" +
                        "$P從今以後，您就是鐵匠囉$R;");
                    鐵匠轉職(pc);
                    return;
                }
                Job2X_09_mask.SetValue(Job2X_09.轉職開始, true);
                //_3a42 = true;
                Say(pc, 131, "什麼？您想成為鐵匠？$R;" +
                    "好！$R;" +
                    "$R想要成為鐵匠，强健的體魄是很重要的$R;" +
                    "$P去見火焰的火神，鍛練體魄後再回來吧$R;" +
                    "$R火焰的火神就在南邊火山上唷。$R;");
                return;
            }
            Say(pc, 131, "您還未達到申請轉職的條件。$R;");
        }

        void 鐵匠轉職(ActorPC pc)
        {

            switch (Select(pc, "真的要轉職嗎?", "", "我想成為鐵匠", "聽取關於鐵匠的注意事項", "還是算了吧"))
            {
                case 1:
                    Say(pc, 131, "那麼就給您烙印上這象徵鐵匠的$R;" +
                        "『鐵匠紋章』吧$R;");
                    if (pc.Inventory.Equipments.Count == 0)
                    {
                        ChangePlayerJob(pc, PC_JOB.BLACKSMITH);
                        pc.JEXP = 0;
                        //PARAM ME.JOB = 83
                        PlaySound(pc, 3087, false, 100, 50);
                        ShowEffect(pc, 4131);
                        Wait(pc, 4000);
                        Say(pc, 131, "…$R;" +
                            "$P好棒啊，$R;" +
                            "您身上已經烙印了漂亮的紋章。$R;" +
                            "$R從今以後，$R您就成為代表我們的『鐵匠』了。$R;");
                        PlaySound(pc, 4012, false, 100, 50);
                        Say(pc, 131, "您已轉職為『鐵匠』了。$R;");
                        return;
                    }
                    Say(pc, 131, "防禦太高的話，就無法烙印紋章了$R;" +
                        "去把裝備脱掉後再來吧$R;");
                    break;
                case 2:
                    Say(pc, 131, "得先和您説清楚。$R;" +
                        "轉職成為鐵匠的話，$R;" +
                        "您的職業等級會變回1級。$R;" +
                        "$P但好像礦工的技能$R;" +
                        "在轉職以後也能學到。$R;" +
                        "$R還有另外一點得注意的，請留意！$R;" +
                        "$P『技能點數』是根據職業完全分開$R;" +
                        "$R礦工的技能點數$R;" +
                        "只有在身為礦工的時候學到。$R;" +
                        "$P礦工時的技能點數$R;" +
                        "在轉職後還會留下的。$R;" +
                        "但是如果轉職了$R;" +
                        "剩下的技能就不能再學了。$R;" +
                        "$P當然，礦工的技能級數$R;" +
                        "也不會再增加了。$R;" +
                        "$R如果有特別想學的技能，$R;" +
                        "最好是在轉職前的時候學習。$R;");

                    鐵匠轉職(pc);
                    break;
                case 3:
                    break;
            }
        }
    }
}

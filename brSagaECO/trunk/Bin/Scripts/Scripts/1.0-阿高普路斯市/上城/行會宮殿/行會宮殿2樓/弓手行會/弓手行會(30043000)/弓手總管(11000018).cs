using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:弓手行會(30043000) NPC基本信息:弓手總管(11000018) X:3 Y:3
namespace SagaScript.M30043000
{
    public class S11000018 : Event
    {
        public S11000018()
        {
            this.EventID = 11000018;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<JobBasic_04> JobBasic_04_mask = new BitMask<JobBasic_04>(pc.CMask["JobBasic_04"]);

            Say(pc, 11000018, 131, "這裡是弓手行會…$R;" +
                                   "呵呵!$R;", "弓手總管");

            if (JobBasic_04_mask.Test(JobBasic_04.弓手轉職成功) &&
                !JobBasic_04_mask.Test(JobBasic_04.已經轉職為弓手))
            {
                弓手轉職完成(pc);
                return;
            }

            if (pc.Job == PC_JOB.NOVICE && pc.Race != PC_RACE.DEM)
            {
                if (JobBasic_04_mask.Test(JobBasic_04.選擇轉職為弓手) &&
                    !JobBasic_04_mask.Test(JobBasic_04.已經轉職為弓手))
                {
                    弓手轉職任務(pc);
                    return;
                }
                else
                {
                    弓手簡介(pc);
                    return;
                }
            }

            if (pc.JobBasic == PC_JOB.ARCHER)
            {
                Say(pc, 131, pc.Name + "?$R;" +
                    "$R呵呵$R;" +
                    "過的還好嗎？$R;");
                switch (Select(pc, "做什麼呢?", "", "我要轉職！", "聽取冒險意見", "買東西", "出售入國許可證", "什麼也不做"))
                {
                    case 1:
                        進階轉職(pc);
                        break;

                    case 2:
                        Say(pc, 131, "箭的命中率不是很高嗎？$R;" +
                            "$R那樣的話，就點撃魔物以後$R;" +
                            "再好好試試吧。$R;" +
                            "$P紅色力量計滿了的話$R;" +
                            "命中率會提升的唷$R;" +
                            "$R一定要試試喔$R;");
                        break;

                    case 3:
                        OpenShopBuy(pc, 67);
                        break;


                    case 4:
                        OpenShopBuy(pc, 105);
                        break;
                    case 5:
                        break;
                }
            }
        }

        void 弓手簡介(ActorPC pc)
        {
            BitMask<JobBasic_04> JobBasic_04_mask = new BitMask<JobBasic_04>(pc.CMask["JobBasic_04"]);

            int selection;

            Say(pc, 11000018, 131, "我是管理弓手們的弓手總管。$R;" +
                                   "$P咦，您是初心者吧?$R;" +
                                   "$R您想不想做『弓手』呢?$R;" +
                                   "先聽聽我的說明吧。$R;", "弓手總管");

            selection = Select(pc, "想做什麼?", "", "我想成為『弓手』!", "『弓手』是什麼樣的職業?", "任務服務台", "什麼也不做");

            while (selection != 4)
            {
                switch (selection)
                {
                    case 1:
                        //廢除一次職轉職任務
                        JobBasic_04_mask.SetValue(JobBasic_04.選擇轉職為弓手, true);
                        JobBasic_04_mask.SetValue(JobBasic_04.弓手轉職任務完成, true);
                        申請轉職為弓手(pc);
                        break;

                    /*
                    Say(pc, 11000018, 131, "想成為『弓手』?$R;" +
                                           "$這樣啊，$R;" +
                                           "那嘗試做看看『自己做的箭』一個。$R;" +
                                           "$P讓我知道您有具備成為『弓手』的才能。$R;", "弓手總管");

                    switch (Select(pc, "接受嗎?", "", "沒問題", "才不要"))
                    {
                        case 1:
                            JobBasic_04_mask.SetValue(JobBasic_04.選擇轉職為弓手, true);

                            Say(pc, 11000018, 131, "材料是『咕咕的翅膀』一個 +『樹枝』一個。$R;" +
                                                   "找「武器製作所店員」製作『自己做的箭』，$R;" +
                                                   "拿『自己做的箭』一個給我看吧。$R;", "弓手總管");
                            break;

                        case 2:
                            Say(pc, 11000018, 131, "是嗎?$R;" +
                                                   "$R『弓手』做自己所需的武器是必須的呢。$R;", "弓手總管");
                            break;
                    }
                    return;
                    */

                    case 2:
                        Say(pc, 11000018, 131, "弓手這職業比較適合$R;" +
                                               "埃米爾族和道米尼族唷!$R;" +
                                               "$R這樣您還要聽下去嗎?$R;", "弓手總管");

                        switch (Select(pc, "還要聽下去嗎?", "", "我要聽", "不聽"))
                        {
                            case 1:
                                Say(pc, 11000018, 131, "『弓手』是使用箭的職業。$R;" +
                                                       "$R擅長遠距離攻擊，$R;" +
                                                       "所以基本上不可能會受到傷害。$R;" +
                                                       "$P相反，如果近距離戰鬥，$R;" +
                                                       "就不是很吃香了。$R;" +
                                                       "$P但是到將來可以成為$R;" +
                                                       "使用手槍的『神槍手』呢!$R;" +
                                                       "$R所以現在只好忍一忍囉!$R;" +
                                                       "$P弓手行會不像別的職業會介紹任務。$R;" +
                                                       "$R所以在找工作的話，$R;" +
                                                       "就要上「咖啡館」$R;" +
                                                       "或者成為生產系的護衛，$R;" +
                                                       "來賺取報酬吧!$R;", "弓手總管");
                                break;
                                
                            case 2:
                                break;
                        }
                        break;

                    case 3:
                        Say(pc, 11000018, 131, "成為『弓手』我就幫您介紹任務。$R;", "弓手總管");
                        break;

                    case 4:
                        break;
                }

                selection = Select(pc, "想做什麼?", "", "我想成為『弓手』喔", "『弓手』是什麼樣的職業?", "任務服務台", "什麼也不做");
            } 
        }

        void 弓手轉職任務(ActorPC pc)
        {
            BitMask<JobBasic_04> JobBasic_04_mask = new BitMask<JobBasic_04>(pc.CMask["JobBasic_04"]);

            if (!JobBasic_04_mask.Test(JobBasic_04.弓手轉職任務完成))
            {
                給予自己做的箭(pc);
            }

            if (JobBasic_04_mask.Test(JobBasic_04.弓手轉職任務完成) &&
                !JobBasic_04_mask.Test(JobBasic_04.弓手轉職成功))
            {
                申請轉職為弓手(pc);
                return;
            }
        }

        void 給予自己做的箭(ActorPC pc)
        {
            BitMask<JobBasic_04> JobBasic_04_mask = new BitMask<JobBasic_04>(pc.CMask["JobBasic_04"]);

            if (CountItem(pc, 10026401) > 0)
            {
                Say(pc, 11000018, 131, "的確是『自己做的箭』。$R;" +
                                       "$R您真的很厲害啊!$R;" +
                                       "$P我開始期待您的將來了。$R;" +
                                       "$R既然您達成任務了，$R;" +
                                       "從現在開始，您就是『弓手』。$R;", "弓手總管");

                switch (Select(pc, "要轉職為『弓手』嗎?", "", "轉職為『弓手』", "還是算了吧"))
                {
                    case 1:
                        JobBasic_04_mask.SetValue(JobBasic_04.弓手轉職任務完成, true);

                        PlaySound(pc, 2030, false, 100, 50);
                        TakeItem(pc, 10026401, 1);
                        Say(pc, 0, 0, "交出『自己做的箭』!$R;", " ");
                        break;
                        
                    case 2:
                        Say(pc, 11000018, 131, "如果想法變了，再來和我說話吧。$R;", "弓手總管");
                        break;
                }
            }
            else
            {
                Say(pc, 11000018, 131, "材料是『咕咕的翅膀』一個 +『樹枝』一個。$R;" +
                                       "找「武器製作所店員」製作『自己做的箭』，$R;" +
                                       "拿『自己做的箭』一個給我看吧。$R;", "弓手總管");
            }
        }

        void 申請轉職為弓手(ActorPC pc)
        {
            BitMask<JobBasic_04> JobBasic_04_mask = new BitMask<JobBasic_04>(pc.CMask["JobBasic_04"]);

            Say(pc, 11000018, 131, "那麼! 我就給您象徵『弓手』的$R;" +
                                   "『弓手紋章』吧!$R;", "弓手總管");

            if (pc.Inventory.Equipments.Count == 0)
            {
                JobBasic_04_mask.SetValue(JobBasic_04.弓手轉職成功, true);

                PlaySound(pc, 3087, false, 100, 50);
                ShowEffect(pc, 4131);
                Wait(pc, 3960);

                Say(pc, 11000018, 131, "…$R;" +
                                       "$P好棒啊!$R;" +
                                       "您身上已經烙印了漂亮的紋章。$R;" +
                                       "$R從今以後，$R;" +
                                       "您就成為代表我們的『弓手』了。$R;", "弓手總管");

                PlaySound(pc, 4012, false, 100, 50);
                ChangePlayerJob(pc, PC_JOB.ARCHER);

                Say(pc, 0, 0, "您已經轉職為『弓手』了!$R;", " ");

                Say(pc, 11000018, 131, "有一份小禮物，要送給您唷!$R;" +
                                       "先穿上衣服後，再和我說話吧。$R;" +
                                       "$R還有別忘了整理行李喔!$R;", "弓手總管");
            }
            else
            {
                Say(pc, 11000018, 131, "紋章是烙印在皮膚上的，$R;" +
                                       "先把裝備脫掉吧。$R;", "弓手總管");
            }
        }

        void 弓手轉職完成(ActorPC pc)
        {
            BitMask<JobBasic_04> JobBasic_04_mask = new BitMask<JobBasic_04>(pc.CMask["JobBasic_04"]);

            if (pc.Inventory.Equipments.Count != 0)
            {
                JobBasic_04_mask.SetValue(JobBasic_04.已經轉職為弓手, true);

                Say(pc, 11000018, 131, "這是送給您的『練習弓』和『腰箭筒』。$R;" +
                                       "$R您一定要好好加油唷!$R;", "弓手總管");

                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, 60090050, 1);
                GiveItem(pc, 50070400, 1);
                Say(pc, 0, 0, "得到『練習弓』和『腰箭筒』!$R;", " ");

                LearnSkill(pc, 2035);
                Say(pc, 0, 0, "學到『投擲武器製作』!R;", " ");
            }
            else
            {
                Say(pc, 11000018, 131, "先穿上衣服後，再和我說話吧。", "弓手總管");            
            }
        }

        void 進階轉職(ActorPC pc)
        {
            BitMask<Job2X_04> Job2X_04_mask = pc.CMask["Job2X_04"];

            if (CountItem(pc, 10020751) >= 1 && Job2X_04_mask.Test(Job2X_04.進階轉職開始) && pc.Job == PC_JOB.ARCHER)
            {
                Say(pc, 131, "來啦，考試怎樣了？$R;" +
                    "$P呵呵！是『獵人認證書』呢。$R;" +
                    "$R從現在開始，$R您就成為人人羨慕的『獵人』了$R;");
                進階轉職選擇(pc);
                return;
            }

            if (Job2X_04_mask.Test(Job2X_04.進階轉職開始) && pc.Job == PC_JOB.ARCHER)
            {
                Say(pc, 131, "獵人的轉職考試是在$R;" +
                    "『奥克魯尼亞的東海岸』。$R;" +
                    "$R到東海岸的商隊帳篷附近$R;" +
                    "找『帕美拉小姐』，$R;" +
                    "在她那裡參加轉職考試吧。$R;" +
                    "$P從她那裡得到獵人認證書的話$R;" +
                    "就承認您成為『獵人』唷。$R;");
                return;
            }

            if (pc.JobLevel1 > 29 && pc.Job == PC_JOB.ARCHER)
            {
                Job2X_04_mask.SetValue(Job2X_04.進階轉職開始, true);
                //_3a55 = true;
                Say(pc, 131, "您終於達到挑戰高級職業的條件了$R;" +
                    "$P是的。$R;" +
                    "$R也就是從『弓手』轉職為『獵人』。$R;");
                return;
            }

            Say(pc, 131, "不行，$R;" +
                "以您的實力，要轉職太勉强了，$R;" +
                "還是先去累積經驗吧。$R;");
        }

        void 進階轉職選擇(ActorPC pc)
        {
            BitMask<Job2X_04> Job2X_04_mask = pc.CMask["Job2X_04"];

            switch (Select(pc, "真的要轉職嗎?", "", "我想成為獵人", "聽取關於獵人的注意事項", "還是算了吧"))
            {
                case 1:
                    Say(pc, 131, "那麼就給您烙印上這象徵獵人的$R;" +
                        "『獵人紋章』吧$R;");
                    if (pc.Inventory.Equipments.Count == 0)
                    {
                        TakeItem(pc, 10020751, 1);
                        ChangePlayerJob(pc, PC_JOB.STRIKER);
                        pc.JEXP = 0;
                        //PARAM ME.JOB = 33
                        PlaySound(pc, 3087, false, 100, 50);
                        ShowEffect(pc, 4131);
                        Wait(pc, 4000);
                        Say(pc, 131, "…$R;" +
                            "$P好棒啊，$R;" +
                            "您身上已經烙印了漂亮的紋章。$R;" +
                            "$R從今以後，$R您就成為代表我們的『獵人』了。$R;");
                        PlaySound(pc, 4012, false, 100, 50);
                        Say(pc, 131, "您已轉職為『獵人』了。$R;");
                        Job2X_04_mask.SetValue(Job2X_04.進階轉職結束, true);
                        return;
                    }
                    Say(pc, 131, "…$R;" +
                        "防禦太高的話，就無法烙印紋章了$R;" +
                        "請換上輕便的服裝後，再來吧。$R;");
                    break;
                case 2:
                    Say(pc, 131, "先要和您講清楚。$R;" +
                        "成為『獵人』的話，$R職業LV會成為1。$R;" +
                        "$P弓手的技能在轉職以後也可以學到。$R;" +
                        "$R但是有一點要注意的，$R您要聽好了。$R;" +
                        "$P『技能點數』是和職業等級$R是完全分開的。$R;" +
                        "$R學習弓手技能時獲得的技能點數$R;" +
                        "只有在職業是弓手的時候才能累積$R;" +
                        "$P轉職以後雖然不會消失$R;" +
                        "但是弓手技能點數不會再提升$R;" +
                        "$P跟技能點數一樣，$R;" +
                        "轉職後弓手的技能學習等級$R也不會上升的。$R;" +
                        "$P也就是説$R除了現在開始學習的技能以外，$R;" +
                        "以後就不能學習。$R;" +
                        "$R如果還有想學的技能，$R還是學完以後才轉職吧$R;");
                    進階轉職選擇(pc);
                    break;
                case 3:
                    break;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:農夫行會(30047000) NPC基本信息:農夫總管(11000022) X:3 Y:3
namespace SagaScript.M30047000
{
    public class S11000022 : Event
    {
        public S11000022()
        {
            this.EventID = 11000022;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<JobBasic_10> JobBasic_10_mask = new BitMask<JobBasic_10>(pc.CMask["JobBasic_10"]);

            Say(pc, 11000022, 131, "歡迎光臨農夫行會。$R;", "農夫總管");

            if (JobBasic_10_mask.Test(JobBasic_10.農夫轉職成功) &&
                !JobBasic_10_mask.Test(JobBasic_10.已經轉職為農夫))
            {
                農夫轉職完成(pc);
                return;
            }

            if (pc.Job == PC_JOB.NOVICE && pc.Race != PC_RACE.DEM)
            {
                if (JobBasic_10_mask.Test(JobBasic_10.選擇轉職為農夫) &&
                    !JobBasic_10_mask.Test(JobBasic_10.已經轉職為農夫))
                {
                    農夫轉職任務(pc);
                    return;
                }
                else
                {
                    農夫簡介(pc);
                    return;
                }
            }

            if (pc.Job == PC_JOB.FARMASIST)
            {
                Say(pc, 11000022, 131, pc.Name + "?$R;" +
                                       "$R要做什麼？$R;", "農夫總管");

                switch (Select(pc, "要做些什麼呢?", "", "任務服務台", "我想聽簡單的情報喔", "我想轉職", "出售帕斯特入國許可證", "什麼也不做"))
                {
                    case 1:
                        HandleQuest(pc, 44);
                        break;
                    case 2:
                        Say(pc, 131, "聽説有個地方，是農夫專用的田地呢$R;" +
                            "$R是喜歡栽培的人的專用地方喔$R;");
                        break;
                    case 3:
                        Say(pc, 131, "在這裡不行啊$R;" +
                            "和鍊金術師總管談談吧$R;");
                        break;
                    case 4:
                        OpenShopBuy(pc, 150);
                        Say(pc, 131, "農夫行會的總部在帕斯特呢$R;");
                        break;
                    case 5:
                        break;
                }
            }
        }

        void 農夫簡介(ActorPC pc)
        {
            BitMask<JobBasic_10> JobBasic_10_mask = new BitMask<JobBasic_10>(pc.CMask["JobBasic_10"]);

            int selection;

            Say(pc, 11000022, 131, "我是管理農夫們的農夫總管。$R;" +
                                   "$P哦，您還是初心者呢?$R;" +
                                   "$R想不想成為農夫呢?$R;", "農夫總管");

            selection = Select(pc, "想做什麼?", "", "我想成為『農夫』!", "『農夫』是什麼樣的職業?", "任務服務台", "什麼也不做");

            while (selection != 4)
            {
                switch (selection)
                {
                    case 1:
                        //廢除一次職轉職任務
                        JobBasic_10_mask.SetValue(JobBasic_10.選擇轉職為農夫, true);
                        JobBasic_10_mask.SetValue(JobBasic_10.農夫轉職任務完成, true);
                        /*Say(pc, 11000022, 131, "想成為『農夫』嗎?$R;" +
                                               "雖然成為農夫沒有特別的要求，$R;" +
                                               "但是若是這樣的話$R;" +
                                               "是不是就太沒意思了?$R;" +
                                               "$R…所以，$R;" +
                                               "如果想成為農夫的話，$R;" +
                                               "就要拿來兩個對農夫來說$R;" +
                                               "挺重要的道具過來給我吧。$R;" +
                                               "$R提示是『黃麥X』。$R;" +
                                               "$P拿來的話，$R;" +
                                               "就讓您做農夫吧!$R;" +
                                               "$R…那也不錯吧!$R;" +
                                               "怎麼樣?$R;", "農夫總管");

                        switch (Select(pc, "接受嗎?", "", "沒問題", "才不要"))
                        {
                            case 1:
                                JobBasic_10_mask.SetValue(JobBasic_10.選擇轉職為農夫, true);

                                Say(pc, 11000022, 131, "那就辛苦您了。$R;", "農夫總管");
                                break;

                            case 2:
                                Say(pc, 11000022, 131, "什麼，您想算了?$R;", "農夫總管");
                                break;
                        }
                        */
                        申請轉職為農夫(pc);
                        return;

                    case 2:
                        Say(pc, 11000022, 131, "埃米爾族是做農夫的最佳人選。$R;" +
                                               "$R要聽更具體的說明嗎?$R;", "農夫總管");

                        switch (Select(pc, "還要聽下去嗎?", "", "我要聽", "不聽"))
                        {
                            case 1:
                                Say(pc, 11000022, 131, "『農夫』因為是農民，$R;" +
                                                       "所以特別擅長於採集植物。$R;" +
                                                       "$R要採集植物的時候，$R;" +
                                                       "也就正是農夫發揮實力的時候囉。$R;" +
                                                       "$P此外，農夫還能夠靠技能$R;" +
                                                       "製造各種道具呢。$R;" +
                                                       "$R應該說是可以自給自足吧。$R;" +
                                                       "$P做農夫的話，將來還有機會$R;" +
                                                       "擔當活動木偶憑依的特殊的職業呢。$R;" +
                                                       "$P可是農夫不擅於使用武器，$R;" +
                                                       "也不能穿著沉重的裝備，$R;" +
                                                       "所以不適合戰鬥喔。$R;" +
                                                       "$R組成隊伍的話，$R;" +
                                                       "一般擔當搬運的角色呢。$R;", "農夫總管");
                                break;

                            case 2:
                                break;
                        }
                        break;

                    case 3:
                        Say(pc, 11000022, 131, "如果不成為『農夫』的話，$R;" +
                                               "就不能在這裡工作了。$R;" +
                                               "$R您不想成為『農夫』嗎?$R;", "農夫總管");
                        break;
                }
                selection = Select(pc, "想做什麼?", "", "我想成為『農夫』!", "『農夫』是什麼樣的職業?", "任務服務台", "什麼也不做");
            }            
        }

        void 農夫轉職任務(ActorPC pc)
        {
            BitMask<JobBasic_10> JobBasic_10_mask = new BitMask<JobBasic_10>(pc.CMask["JobBasic_10"]);

            if (!JobBasic_10_mask.Test(JobBasic_10.農夫轉職任務完成))
            {
                給予黃麥穗(pc);
            }

            if (JobBasic_10_mask.Test(JobBasic_10.農夫轉職任務完成) &&
                !JobBasic_10_mask.Test(JobBasic_10.農夫轉職成功))
            {
                申請轉職為農夫(pc);
                return;
            }
        }

        void 給予黃麥穗(ActorPC pc)
        {
            BitMask<JobBasic_10> JobBasic_10_mask = new BitMask<JobBasic_10>(pc.CMask["JobBasic_10"]);

            if (CountItem(pc, 10007900) >= 2)
            {
                Say(pc, 11000022, 131, "對，$R;" +
                                       "就是『黃麥穗』。$R;" +
                                       "$R好，現在您就是『農夫』了。$R;" +
                                       "$P真要轉職為『農夫』是吧?$R;", "農夫總管");

                switch (Select(pc, "要轉職為『農夫』嗎?", "", "轉職為『農夫』", "還是算了吧"))
                {
                    case 1:
                        JobBasic_10_mask.SetValue(JobBasic_10.農夫轉職任務完成, true);
                        break;

                    case 2:
                        break;
                }
            }
            else
            {
                Say(pc, 11000022, 131, "提示是『黃麥X』兩個!$R;" +
                                       "$P拿來的話，$R;" +
                                       "就讓您轉職為『農夫』吧!$R;", "農夫總管");
            }
        }

        void 申請轉職為農夫(ActorPC pc)
        {
            BitMask<JobBasic_10> JobBasic_10_mask = new BitMask<JobBasic_10>(pc.CMask["JobBasic_10"]);

            Say(pc, 11000022, 131, "那麼! 我就給您象徵『農夫』的$R;" +
                                   "『農夫紋章』吧!$R;", "農夫總管");

            if (pc.Inventory.Equipments.Count == 0)
            {
                JobBasic_10_mask.SetValue(JobBasic_10.農夫轉職成功, true);

                PlaySound(pc, 3087, false, 100, 50);
                ShowEffect(pc, 4131);
                Wait(pc, 3960);

                Say(pc, 11000022, 131, "…$R;" +
                                       "$P哦，$R;" +
                                       "紋章和您很相配呢。$R;" +
                                       "$R從今以後，$R;" +
                                       "您就成為『農夫』了。$R;", "農夫總管");

                PlaySound(pc, 4012, false, 100, 50);
                ChangePlayerJob(pc, PC_JOB.FARMASIST);

                Say(pc, 0, 0, "您已經轉職為『農夫』了!$R;", " ");

                Say(pc, 11000022, 131, "有一份小禮物，要送給您唷!$R;" +
                                       "先穿上衣服後，再和我說話吧。$R;" +
                                       "$R還有別忘了整理行李喔!$R;", "農夫總管");
            }
            else
            {
                Say(pc, 11000022, 131, "紋章是烙印在皮膚上的，$R;" +
                                       "先把裝備脫掉吧。$R;", "農夫總管");
            }
        }

        void 農夫轉職完成(ActorPC pc)
        {
            BitMask<JobBasic_10> JobBasic_10_mask = new BitMask<JobBasic_10>(pc.CMask["JobBasic_10"]);

            if (pc.Inventory.Equipments.Count != 0)
            {
                JobBasic_10_mask.SetValue(JobBasic_10.已經轉職為農夫, true);

                Say(pc, 11000022, 131, "這是給您的『棉緞帶』。$R;" +
                                       "$R如果把『棉緞帶』和$R;" +
                                       "剛剛收集的『黃麥穗』合成的話，$R;" +
                                       "應該不錯的。$R;", "農夫總管");

                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, 10043700, 1);
                Say(pc, 0, 0, "得到『棉緞帶』!$R;", " ");

                LearnSkill(pc, 3128);
                Say(pc, 0, 0, "學到『支配』!R;", " ");
            }
            else
            {
                Say(pc, 11000022, 131, "先穿上衣服後，再和我說話吧。$R;", "農夫總管");
            }
        }
    }
}

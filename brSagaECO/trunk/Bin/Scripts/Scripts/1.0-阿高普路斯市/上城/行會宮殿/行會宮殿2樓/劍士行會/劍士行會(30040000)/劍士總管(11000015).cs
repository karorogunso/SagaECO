using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:劍士行會(30040000) NPC基本信息:劍士總管(11000015) X:3 Y:3
namespace SagaScript.M30040000
{
    public class S11000015 : Event
    {
        public S11000015()
        {
            this.EventID = 11000015;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<JobBasic_01> JobBasic_01_mask = new BitMask<JobBasic_01>(pc.CMask["JobBasic_01"]);

            Say(pc, 11000015, 131, "歡迎光臨劍士行會呀。$R;", "劍士總管");

            if (JobBasic_01_mask.Test(JobBasic_01.劍士轉職成功) &&
                !JobBasic_01_mask.Test(JobBasic_01.已經轉職為劍士))
            {
                劍士轉職完成(pc);
                return;
            }

            if (pc.Job == PC_JOB.NOVICE && pc.Race != PC_RACE.DEM)
            {
                if (JobBasic_01_mask.Test(JobBasic_01.選擇轉職為劍士) &&
                    !JobBasic_01_mask.Test(JobBasic_01.已經轉職為劍士))
                {
                    劍士轉職任務(pc);
                    return;
                }
                else
                {
                    劍士簡介(pc);
                    return;
                }
            }

            if (pc.JobBasic == PC_JOB.SWORDMAN)
            {
                Say(pc, 11000015, 131, " 這不是" + pc.Name + "嗎?!$R;" +
                                       "$R來得好，$R;" +
                                       "今天來有什麼事嗎?$R;", "劍士總管");
 
                switch (Select(pc, "做什麼好呢?", "", "任務服務台", "我想轉職", "出售入國許可證", "什麼也不做"))
                {
                    case 1:
                        Say(pc, 0, 0, "目前尚未實裝$R;", " ");
                        break;
                    case 2:
                        進階轉職(pc);
                        break;
                    case 3:
                        OpenShopBuy(pc, 105);
                        break;
                    case 4:
                        break;
                }
            }
        }

        void 劍士簡介(ActorPC pc)
        {
            BitMask<JobBasic_01> JobBasic_01_mask = new BitMask<JobBasic_01>(pc.CMask["JobBasic_01"]);

            int selection;

            Say(pc, 11000015, 131, "我是管理劍士們的劍士總管。$R;" +
                                   "$P您好像不屬於我們行會的管轄呀?$R;" +
                                   "$R那麼……$R;" +
                                   "您想不想做『劍士』呢?$R;", "劍士總管");

            selection = Select(pc, "想做什麼?", "", "我想成為『劍士』!", "『劍士』是什麼樣的職業?", "任務服務台", "什麼也不做");

            while (selection != 4)
            {
                switch (selection)
                {
                    case 1:
                        //廢除一次職轉職任務
                        JobBasic_01_mask.SetValue(JobBasic_01.選擇轉職為劍士, true);
                        JobBasic_01_mask.SetValue(JobBasic_01.劍士轉職任務完成, true);
                        /*Say(pc, 11000015, 131, "想成為『劍士』嗎?$R;" +
                                               "$R您看起來應該有點潛力，$R;" +
                                               "先考驗一下您的力量吧!$R;", "劍士總管");

                        switch (Select(pc, "接受『考驗』嗎?", "", "沒問題", "才不要"))
                        {
                            case 1:
                                if (pc.Str > 9)
                                {
                                    Say(pc, 11000015, 131, "您不用擔心，任務很簡單的。$R;" +
                                                           "$P有一種魔物，名字叫做「巴鳴」，$R;" +
                                                           "牠的外型長得跟惡狗一樣。$R;" +
                                                           "任務就是把「巴嗚」打倒就可以了。$R;" +
                                                           "$P啊，別忘了!!$R;" +
                                                           "還要拿到『肉』做為打敗的證據喔!$R;" +
                                                           "$R這樣您就可以成為劍士唷。$R;", "劍士總管");

                                    switch (Select(pc, "接受『考驗』嗎?", "", "沒問題", "才不要"))
                                    {
                                        case 1:
                                            JobBasic_01_mask.SetValue(JobBasic_01.選擇轉職為劍士, true);

                                            Say(pc, 11000015, 131, "……$R;" +
                                                                   "$P很好，$R;" +
                                                                   "我等您回來喔。$R;", "劍士總管");
                                            break;

                                        case 2:
                                            Say(pc, 11000015, 131, "劍士是勇敢代表，$R;" +
                                                                   "充滿勇氣再來吧。$R;", "劍士總管");
                                            break;
                                    }
                                }
                                else
                                {
                                    Say(pc, 11000015, 131, "想成為劍士還是需要一點力量的!$R;" +
                                                           "$P力量到達10以後，再來找我吧。$R;", "劍士總管");
                                }
                                break;

                            case 2:
                                break;
                        }
                        */
                        申請轉職為劍士(pc);
                        return;

                    case 2:
                        Say(pc, 11000015, 131, "劍士這職業比較適合$R;" +
                                               "埃米爾族和道米尼族的體質唷!$R;" +
                                               "$R判斷職業的性質，$R;" +
                                               "是否適合自己的種族是很重要的呀，$R;" +
                                               "還想聽下去嗎?$R;", "劍士總管");

                        switch (Select(pc, "還要聽下去嗎?", "", "我要聽", "不聽"))
                        {
                            case 1:
                                Say(pc, 11000015, 131, "『劍士』主要是使用劍和盾牌的戰士唷!$R;" +
                                                       "當然，還可以使用別的武器。$R;" +
                                                       "$R劍士的最大魅力，$R;" +
                                                       "就是攻擊力非常高唷。$R;" +
                                                       "$P當然防禦力也很高。$R;" +
                                                       "這樣就能成為隊伍裡的盾牌，$R;" +
                                                       "不僅可以戰鬥還能保護隊友安全唷!$R;" +
                                                       "$P可惜搜集能力和搬運能力比較低，$R;" +
                                                       "並不適合一個人單獨行動。$R;" +
                                                       "$R是一個和同伴們互相合作，$R;" +
                                                       "就會散發光彩的職業唷。$R;", "劍士總管");
                                break;

                            case 2:
                                break;
                        }
                        break;

                    case 3:
                        Say(pc, 11000015, 131, "如果想在這裡接任務的話，$R;" +
                                               "首先要具備一些條件喔。$R;" +
                                               "$P至於要具備什麼條件呢?$R;" +
                                               "$R等您成為『劍士』之後，$R;" +
                                               "我再告訴您吧。$R;", "劍士總管");
                        break;
                }

                selection = Select(pc, "想做什麼?", "", "我想成為『劍士』!", "『劍士』是什麼樣的職業?", "任務服務台", "什麼也不做");
            } 
        }

        void 劍士轉職任務(ActorPC pc)
        {
            BitMask<JobBasic_01> JobBasic_01_mask = new BitMask<JobBasic_01>(pc.CMask["JobBasic_01"]);

            if (!JobBasic_01_mask.Test(JobBasic_01.劍士轉職任務完成))
            {
                給予巴鳴身上的肉(pc);
            }

            if (JobBasic_01_mask.Test(JobBasic_01.劍士轉職任務完成) &&
                !JobBasic_01_mask.Test(JobBasic_01.劍士轉職成功))
            {
                申請轉職為劍士(pc);
                return;
            }
        }

        void 給予巴鳴身上的肉(ActorPC pc)
        {
            BitMask<JobBasic_01> JobBasic_01_mask = new BitMask<JobBasic_01>(pc.CMask["JobBasic_01"]);

            if (CountItem(pc, 10006300) > 0)
            {
                Say(pc, 11000015, 131, "哇!! 真的把『肉』帶來了，$R;" +
                                       "看來您做還算不錯唷。$R;" +
                                       "$R我開始期待您的將來了。$R;" +
                                       "$P既然您達成任務了，$R;" +
                                       "從現在開始，您就是『劍士』啦!$R;", "劍士總管");

                switch (Select(pc, "要轉職為『劍士』嗎?", "", "轉職為『劍士』", "還是算了吧"))
                {
                    case 1:
                        JobBasic_01_mask.SetValue(JobBasic_01.劍士轉職任務完成, true);

                        PlaySound(pc, 2030, false, 100, 50);
                        TakeItem(pc, 10006300, 1);
                        Say(pc, 0, 0, "交出『肉』!$R;", " ");
                        break;

                    case 2:
                        Say(pc, 11000015, 131, "考慮清楚再來吧。$R;", "劍士總管");
                        break;
                }
            }
            else
            {
                Say(pc, 11000015, 131, "在「奧克魯尼亞北部平原」$R;" +
                                       "在上去的「瑞路斯山道」。$R;" +
                                       "$R那裡棲息著非常多的「巴鳴」，$R;" +
                                       "但是「巴鳴」很強!$R;" +
                                       "建議多找朋友幫忙打喔~!$R;", "劍士總管");
            }
        }

        void 申請轉職為劍士(ActorPC pc)
        {
            BitMask<JobBasic_01> JobBasic_01_mask = new BitMask<JobBasic_01>(pc.CMask["JobBasic_01"]);

            Say(pc, 11000015, 131, "那麼就替您紋上代表『劍士』的$R;" +
                                   "『劍士紋章』吧。$R;", "劍士總管");

            if (pc.Inventory.Equipments.Count == 0)
            {
                JobBasic_01_mask.SetValue(JobBasic_01.劍士轉職成功, true);

                PlaySound(pc, 3087, false, 100, 50);
                ShowEffect(pc, 4131);
                Wait(pc, 3960);

                Say(pc, 11000015, 131, "…$R;" +
                                       "$P好棒啊，$R;" +
                                       "您身上已經烙印了漂亮的紋章。$R;" +
                                       "$R從今以後，$R;" +
                                       "您就成為『劍士』了。$R;", "劍士總管");

                PlaySound(pc, 4012, false, 100, 50);
                ChangePlayerJob(pc, PC_JOB.SWORDMAN);

                Say(pc, 0, 0, "您已經轉職為『劍士』了!$R;", " ");

                Say(pc, 11000015, 131, "先穿上衣服後，再和我說話吧。$R;" +
                                       "有一份小禮物，要送給您唷!$R;" +
                                       "$R您先去整理行李後，再來找我吧。$R;", "劍士總管");
            }
            else
            {
                Say(pc, 11000015, 131, "紋章是烙印在皮膚上的，$R;" +
                                       "先把裝備脫掉吧。$R;", "劍士總管");
            }
        }

        void 劍士轉職完成(ActorPC pc)
        {
            BitMask<JobBasic_01> JobBasic_01_mask = new BitMask<JobBasic_01>(pc.CMask["JobBasic_01"]);

            if (pc.Inventory.Equipments.Count != 0)
            {
                JobBasic_01_mask.SetValue(JobBasic_01.已經轉職為劍士, true);

                Say(pc, 11000015, 131, "給您『劍士勳章』，$R;" +
                                       "$R用『劍士勳章』代表劍士榮譽。$R;" +
                                       "好好加油喔。$R;", "劍士總管");

                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, 50051300, 1);
                Say(pc, 0, 0, "得到『劍士勳章』!$R;", " ");

                LearnSkill(pc, 2115);
                Say(pc, 0, 0, "學到『居合斬』!$R;", " ");
            }
            else
            {
                Say(pc, 11000015, 131, "先穿上衣服後，再和我說話吧。$R;", "劍士總管");
            }
        }

        void 進階轉職(ActorPC pc)
        {
            BitMask<Job2X_01> Job2X_01_mask = pc.CMask["Job2X_01"];

            if (Job2X_01_mask.Test(Job2X_01.轉職完成))//_3A37)
            {
                if (pc.Inventory.Equipments.Count == 0)
                {
                    Say(pc, 131, "衣服要穿好啊！！！$R;");
                    return;
                }
                Say(pc, 131, "現在還不能轉職，$R;" +
                    "還是先去累積經驗吧。$R;");
                return;
            }

            if (CountItem(pc, 10020600) >= 1)
            {
                Say(pc, 131, "很好，既然取得了認證書，$R那就讓您轉職吧。$R;" +
                    "$R從現在開始，$R您就成為人人羨慕的『光戰士』了$R;");
                進階轉職選擇(pc);
                return;
            }

            if (pc.Inventory.Equipments.Count == 0)
            {
                Say(pc, 131, "衣服要穿好啊！！！$R;");
                return;
            }

            if (Job2X_01_mask.Test(Job2X_01.進階轉職開始))//_3A32)
            {
                Say(pc, 131, "只要到『阿伊恩市』的$R『鳳老頭』那裡$R;" +
                    "拿到認證書的話，$R;" +
                    "就承認您成為『光戰士』唷。$R;");
                return;
            }

            if (pc.Job == PC_JOB.SWORDMAN && pc.JobLevel1 > 29)
            {
                Say(pc, 131, "您終於達到挑戰高級職業的條件了$R;" +
                    "也就是從劍士轉職成光戰士。$R;");

                Say(pc, 131, "只要到『阿伊恩市』的$R『鳳老頭』那裡$R;" +
                    "拿到認證書的話，$R;" +
                    "就承認您成為『光戰士』唷。$R;");
                Job2X_01_mask.SetValue(Job2X_01.進階轉職開始, true);
                //_3A32 = true;
                return;
            }

            Say(pc, 131, "您還未達到申請轉職的條件。$R;" +
                "先以劍士的職業，慢慢培養實力吧。$R;");
        }

        void 進階轉職選擇(ActorPC pc)
        {
            BitMask<Job2X_01> Job2X_01_mask = pc.CMask["Job2X_01"];

            switch (Select(pc, "真的要轉職嗎?", "", "我想成為光戰士", "聽取關於光戰士的注意事項", "還是算了吧"))
            {
                case 1:
                    Say(pc, 131, "那麼就給您烙印上這象徵光戰士的$R;" +
                        "『光戰士紋章』吧$R;");
                    if (pc.Inventory.Equipments.Count == 0)
                    {
                        Say(pc, 131, "最後再向您確認一次，$R;" +
                            "您是真的決定轉職嗎?$R;");
                        switch (Select(pc, "真的要轉職嗎?", "", "成為光戰士", "算了吧"))
                        {
                            case 1:
                                TakeItem(pc, 10020600, 1);
                                Job2X_01_mask.SetValue(Job2X_01.轉職完成, true);
                                //_3A37 = true;
                                ChangePlayerJob(pc, PC_JOB.BLADEMASTER);
                                pc.JEXP = 0;
                                //PARAM ME.JOB = 3
                                PlaySound(pc, 3087, false, 100, 50);
                                ShowEffect(pc, 4131);
                                Wait(pc, 4000);
                                Say(pc, 131, "…$R;" +
                                    "$P好棒啊，$R;" +
                                    "您身上已經烙印了漂亮的紋章。$R;" +
                                    "$R從今以後，$R您就成為代表我們的『光戰士』了。$R;");
                                PlaySound(pc, 4012, false, 100, 50);
                                Say(pc, 131, "您已轉職為『光戰士』了。$R;");
                                break;
                            case 2:
                                Say(pc, 131, "看來您還不想轉職呀？$R;" +
                                    "我想也是，這麼重大的決定$R需要時間慎重思考的吧$R;");
                                break;
                        }
                        return;
                    }
                    Say(pc, 131, "防禦太高的話，就無法烙印紋章了$R;" +
                        "請換上輕便的服裝後，再來吧。$R;");
                    break;
                case 2:
                    Say(pc, 131, "成為『光戰士』的話，$R職業LV會成為1。$R;" +
                        "但是轉職前所擁有的$R;" +
                        "$R技能和技能點數是不會消失的。$R;" +
                        "$P還有轉職之前不能學習的技能，$R;" +
                        "在轉職以後也不可學習的。$R;" +
                        "例如職業等級為30時轉職的話，$R;" +
                        "$R轉職前30級以上的技能$R;" +
                        "就不能學習了，請注意。$R;");
                    進階轉職選擇(pc);
                    break;
                case 3:
                    Say(pc, 131, "看來您還不想轉職呀？$R;" +
                        "我想也是，這麼重大的決定$R需要時間慎重思考的吧$R;");
                    break;
            }
        }
    }
}

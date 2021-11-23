using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:騎士行會(30041000) NPC基本信息:騎士總管(11000016) X:3 Y:3
namespace SagaScript.M30041000
{
    public class S11000016 : Event
    {
        public S11000016()
        {
            this.EventID = 11000016;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<JobBasic_02> JobBasic_02_mask = new BitMask<JobBasic_02>(pc.CMask["JobBasic_02"]);

            Say(pc, 11000016, 131, "歡迎來到騎士行會。$R;", "騎士總管");

            if (JobBasic_02_mask.Test(JobBasic_02.騎士轉職成功) &&
                !JobBasic_02_mask.Test(JobBasic_02.已經轉職為騎士))
            {
                騎士轉職完成(pc);
                return;
            }

            if (pc.Job == PC_JOB.NOVICE && pc.Race != PC_RACE.DEM)
            {
                if (JobBasic_02_mask.Test(JobBasic_02.選擇轉職為騎士) &&
                    !JobBasic_02_mask.Test(JobBasic_02.已經轉職為騎士))
                {
                    騎士轉職任務(pc);
                    return;
                }
                else
                {
                    騎士簡介(pc);
                    return;
                }
            }

            if (pc.JobBasic == PC_JOB.FENCER)
            {
                Say(pc, 11000016, 131, pc.Name + "呀~!$R;" +
                                       "$R好久不見啦!$R;", "騎士總管");
                switch (Select(pc, "要做什麼呢?", "", "出售入國許可證", "我想轉職", "什麼也不做"))
                {
                    case 1:
                        OpenShopBuy(pc, 105);
                        break;
                    case 2:
                        進階轉職(pc);
                        break;
                    case 3:
                        break;
                }
            }
        }

        void 騎士簡介(ActorPC pc)
        {
            BitMask<JobBasic_02> JobBasic_02_mask = new BitMask<JobBasic_02>(pc.CMask["JobBasic_02"]);

            int selection;

            Say(pc, 11000016, 131, "我是管理騎士們的騎士總管。$R;" +
                                   "$R哦，您還是初心者呢!$R;" +
                                   "呵呵…$R;" +
                                   "$P如果沒有特別想做的職業的話，$R;" +
                                   "想不想做騎士呢?$R;" +
                                   "$R先聽聽我的意見吧!$R;", "騎士總管");

            selection = Select(pc, "想做什麼?", "", "我想成為『騎士』!", "『騎士』是什麼樣的職業?", "任務服務台", "什麼也不做");

            while (selection != 4)
            {
                switch (selection)
                {
                    case 1:
                        //廢除一次職轉職任務
                        JobBasic_02_mask.SetValue(JobBasic_02.選擇轉職為騎士, true);
                        JobBasic_02_mask.SetValue(JobBasic_02.騎士轉職任務完成, true);
                        /*
                        Say(pc, 11000016, 131, "想要成為『騎士』?$R;" +
                                               "您這麼說我很高興。$R;" +
                                               "$R但是您真有這個本事嗎?$R;" +
                                               "先考驗一下您的力量吧！$R;", "騎士總管");

                        switch (Select(pc, "接受『考驗』嗎?", "", "沒問題", "才不要"))
                        {
                            case 1:
                                JobBasic_02_mask.SetValue(JobBasic_02.選擇轉職為騎士, true);

                                Say(pc, 11000016, 131, "這裡西邊有一種魔物，名字叫做「殺人蜂」。$R;" +
                                                       "它的外型長得跟蜜蜂一樣。$R;" +
                                                       "$R任務就是把「殺人蜂」殺掉就可以了。$R;" +
                                                       "$P啊，別忘了!!$R;" +
                                                       "還要拿到『蜜蜂的毒針』$R;" +
                                                       "做為打敗的證據喔!$R;" +
                                                       "$P如果您真能打倒「殺人蜂」的話，$R;" +
                                                       "這樣您就可以成為騎士唷。$R;", "騎士總管");
                                break;

                            case 2:
                                Say(pc, 11000016, 131, "算了嗎……，$R;" +
                                                       "再考慮一下吧。$R;", "騎士總管");
                                break;
                        }
                        */
                        申請轉職為騎士(pc);
                        return;

                    case 2:
                        Say(pc, 11000016, 131, "『騎士』這職業比較適合$R;" +
                                               "埃米爾族和道米尼族的體質唷!$R;" +
                                               "$R還要繼續聽嗎?$R;", "騎士總管");

                        switch (Select(pc, "還要聽下去嗎?", "", "我要聽", "不聽"))
                        {
                            case 1:
                                Say(pc, 11000016, 131, "『騎士』主要是使用細劍和槍的華麗戰士。$R;" +
                                                       "$R騎士銳利的突刺攻擊是$R;" +
                                                       "誰也不可能迴避的。$R;" +
                                                       "$P可惜搜集能力和搬運能力比較低，$R;" +
                                                       "並不適合一個人單獨行動。$R;" +
                                                       "$P心被黑暗力量感染的騎士，$R;" +
                                                       "聽說還可以掌握別的力量。$R;" +
                                                       "$P騎士行會，還沒有開放接受任務。$R;" +
                                                       "$R所以在找工作的話，就上「咖啡館」$R;" +
                                                       "或者成為生產系者的護衛，$R;" +
                                                       "來賺取報酬吧!$R;", "騎士總管");
                                break;

                            case 2:
                                break;
                        }
                        break;

                    case 3:
                        Say(pc, 11000016, 131, "等您成為光榮的『騎士』，再找我配給工作。$R;", "騎士總管");
                        break;

                    case 4:
                        break;
                }

                selection = Select(pc, "想做什麼?", "", "我想成為『騎士』!", "『騎士』是什麼樣的職業?", "任務服務台", "什麼也不做");
            } 
        }

        void 騎士轉職任務(ActorPC pc)
        {
            BitMask<JobBasic_02> JobBasic_02_mask = new BitMask<JobBasic_02>(pc.CMask["JobBasic_02"]);

            if (!JobBasic_02_mask.Test(JobBasic_02.騎士轉職任務完成))
            {
                給予蜜蜂的毒針(pc);
            }

            if (JobBasic_02_mask.Test(JobBasic_02.騎士轉職任務完成) &&
                !JobBasic_02_mask.Test(JobBasic_02.騎士轉職成功))
            {
                申請轉職為騎士(pc);
                return;
            }
        }

        void 給予蜜蜂的毒針(ActorPC pc)
        {
            BitMask<JobBasic_02> JobBasic_02_mask = new BitMask<JobBasic_02>(pc.CMask["JobBasic_02"]);

            if (CountItem(pc, 10035200) > 0)
            {
                Say(pc, 11000016, 131, "哇! 真的是『蜜蜂的毒針』。$R;" +
                                       "您真的很厲害啊。$R;" +
                                       "$R我開始期待您的將來了。$R;" +
                                       "$P既然您達成任務了，$R;" +
                                       "從現在開始，您就是『騎士』啦!$R;", "騎士總管");

                switch (Select(pc, "要轉職為『騎士』嗎?", "", "轉職為『騎士』", "還是算了吧"))
                {
                    case 1:
                        JobBasic_02_mask.SetValue(JobBasic_02.騎士轉職任務完成, true);

                        PlaySound(pc, 2030, false, 100, 50);
                        TakeItem(pc, 10035200, 1);
                        Say(pc, 0, 0, "交出『蜜蜂的毒針』了。$R;", " ");
                        break;

                    case 2:
                        Say(pc, 11000016, 131, "不想成為騎士嗎?$R;" +
                                               "$R嘿嘿!$R;" +
                                               "也有這樣的時候吧?$R;" +
                                               "$P沒辦法，$R;" +
                                               "如果想法變了，再來和我說話吧。$R;", "騎士總管");
                        break;
                }
            }
            else
            {
                Say(pc, 11000016, 131, "這裡西邊有一種魔物，名字叫做「殺人蜂」。$R;" +
                                       "它的外型長得跟蜜蜂一樣。$R;" +
                                       "$R任務就是把「殺人蜂」殺掉就可以了。$R;" +
                                       "$P啊，別忘了!!$R;" +
                                       "還要拿到『蜜蜂的毒針』$R;" +
                                       "做為打敗的證據喔!$R;" +
                                       "$P如果您真能打倒「殺人蜂」的話，$R;" +
                                       "這樣您就可以成為騎士唷。$R;", "騎士總管");
            }
        }

        void 申請轉職為騎士(ActorPC pc)
        {
            BitMask<JobBasic_02> JobBasic_02_mask = new BitMask<JobBasic_02>(pc.CMask["JobBasic_02"]);

            Say(pc, 11000016, 131, "那麼我就給您象徵『騎士』的$R;" +
                                   "『騎士紋章』吧。$R;", "騎士總管");

            if (pc.Inventory.Equipments.Count == 0)
            {
                JobBasic_02_mask.SetValue(JobBasic_02.騎士轉職成功, true);

                PlaySound(pc, 3087, false, 100, 50);
                ShowEffect(pc, 4131);
                Wait(pc, 3960);

                Say(pc, 11000016, 131, "…$R;" +
                                       "$P好棒啊!$R;" +
                                       "您身上已經烙印了漂亮的紋章。$R;" +
                                       "$R從今以後，$R;" +
                                       "您就成為代表我們的『騎士』了。$R;", "騎士總管");

                PlaySound(pc, 4012, false, 100, 50);
                ChangePlayerJob(pc, PC_JOB.FENCER);

                Say(pc, 0, 0, "您已經轉職為『騎士』了!$R;", " ");

                Say(pc, 11000016, 131, "有一份小禮物，要送給您唷!$R;" +
                                       "先穿上衣服後，再和我說話吧。$R;" +
                                       "$R還有別忘了整理行李喔!$R;", "騎士總管");
            }
            else
            {
                Say(pc, 11000016, 131, "紋章是烙印在皮膚上的，$R;" +
                                       "先把裝備脫掉吧。$R;", "騎士總管");
            }
        }

        void 騎士轉職完成(ActorPC pc)
        {
            BitMask<JobBasic_02> JobBasic_02_mask = new BitMask<JobBasic_02>(pc.CMask["JobBasic_02"]);

            if (pc.Inventory.Equipments.Count != 0)
            {
                JobBasic_02_mask.SetValue(JobBasic_02.已經轉職為騎士, true);
                Say(pc, 11000016, 131, "這是『幽靈面具』$R;" +
                                       "是只有騎士，才能佩戴的臉部飾品喔。$R;" +
                                       "$R您一定要好好珍藏唷!$R;", "騎士總管");

                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, 50040400, 1);
                Say(pc, 0, 0, "得到『幽靈面具』!$R;", " ");

                LearnSkill(pc, 2138);
                Say(pc, 0, 0, "學到『狂風刺擊』!R;", " ");
            }
            else
            {
                Say(pc, 11000016, 131, "先穿上衣服後，再和我說話吧。", "騎士總管");
            }
        }

        void 進階轉職(ActorPC pc)
        {
            BitMask<Job2X_02> Job2X_02_mask = pc.CMask["Job2X_02"];

            if (Job2X_02_mask.Test(Job2X_02.轉職完成))//_3A38)
            {

                if (pc.Inventory.Equipments.Count == 0)
                {
                    Say(pc, 131, "真是的！$R;" +
                        "要穿整齊點喔！$R;");
                    return;
                }

                Say(pc, 131, "以您的實力還是不行喔，$R;" +
                    "還是多累積點經驗吧。$R;");
                return;
            }

            if (pc.Job == PC_JOB.FENCER && pc.JobLevel1 > 29)
            {

                if (CountItem(pc, 10020600) >= 1)
                {
                    Say(pc, 131, "很好，既然取得了認證書，$R那就讓您轉職吧。$R;" +
                        "$R從現在開始，$R您就成為人人羨慕的『聖騎士』了$R;");
                    進階轉職選擇(pc);
                    return;
                }

                if (Job2X_02_mask.Test(Job2X_02.進階轉職開始))//_3A33)
                {
                    Say(pc, 131, "只要到『阿伊恩市』的$R『鳳老頭』那裡$R;" +
                        "拿到認證書的話，$R;" +
                        "就承認您成為『聖騎士』唷。$R;");
                    return;
                }

                Say(pc, 131, "哈哈，您成長了很多喔$R;" +
                    "$R我想您是時候從『騎士』$R;" +
                    "轉職成『聖騎士』了吧？$R;");

                Say(pc, 131, "只要到『阿伊恩市』的$R『鳳老頭』那裡$R;" +
                    "拿到認證書的話，$R;" +
                    "就承認您成為『聖騎士』唷。$R;");
                Job2X_02_mask.SetValue(Job2X_02.進階轉職開始, true);
                //_3A33 = true;
                return;
            }

            if (pc.Inventory.Equipments.Count == 0)
            {
                Say(pc, 131, "真是的！$R;" +
                    "要穿整齊點喔！$R;");
                return;
            }

            Say(pc, 131, "以您的實力還是不行喔，$R;" +
                "還是多累積點經驗吧。$R;");
        }

        void 進階轉職選擇(ActorPC pc)
        {
            BitMask<Job2X_02> Job2X_02_mask = pc.CMask["Job2X_02"];

            switch (Select(pc, "真的要轉職嗎?", "", "我想成為聖騎士", "聽取關於聖騎士的注意事項", "還是算了吧"))
            {
                case 1:
                    Say(pc, 131, "那麼就給您烙印上這象徵聖騎士的$R;" +
                        "『聖騎士紋章』吧$R;");
                    if (pc.Inventory.Equipments.Count == 0)
                    {
                        Say(pc, 131, "最後再向您確認一次，$R;" +
                            "您是真的決定轉職嗎?$R;");

                        switch (Select(pc, "真的要轉職嗎?", "", "轉職", "不轉職"))
                        {
                            case 1:
                                TakeItem(pc, 10020600, 1);
                                Job2X_02_mask.SetValue(Job2X_02.轉職完成, true);
                                //_3A38 = true;
                                ChangePlayerJob(pc, PC_JOB.KNIGHT);
                                pc.JEXP = 0;
                                //PARAM ME.JOB = 13
                                PlaySound(pc, 3087, false, 100, 50);
                                ShowEffect(pc, 4131);
                                Wait(pc, 4000);
                                Say(pc, 131, "…$R;" +
                                    "$P呵呵，您身上已經烙印了漂亮的紋章。$R;" +
                                    "$R從今以後，$R您就成為代表我們的『聖騎士』了。$R;");
                                PlaySound(pc, 4012, false, 100, 50);
                                Say(pc, 131, "您已轉職為『聖騎士』了。$R;");
                                break;

                            case 2:
                                Say(pc, 131, "您沒想過要成為聖騎士嗎？$R;" +
                                    "$R嗯……$R;" +
                                    "好，那您好好考慮一下吧。$R;");
                                break;
                        }
                        return;
                    }

                    Say(pc, 131, "防禦太高的話，就無法烙印紋章了$R;" +
                        "請換上輕便的服裝後，再來吧。$R;");
                    break;

                case 2:
                    Say(pc, 131, "成為『聖騎士』的話$R職業LV會成為1.$R;" +
                        "但是轉職前所擁有的$R;" +
                        "$R技能和技能點數是不會消失的。$R;" +
                        "$P還有轉職之前不能學習的技能，$R;" +
                        "在轉職以後也不可學習的。$R;" +
                        "例如職業等級為30時轉職的話，$R;" +
                        "$R轉職前30級以上的技能$R;" +
                        "就不能學習了，請注意。$R;" +
                        "$P轉職之前好好想清楚喔！$R;");

                    進階轉職選擇(pc);
                    break;

                case 3:
                    Say(pc, 131, "您沒想過要成為聖騎士嗎？$R;" +
                        "$R嗯……$R;" +
                        "好，那您好好考慮一下吧。$R;");
                    break;
            }
        }
    }
}

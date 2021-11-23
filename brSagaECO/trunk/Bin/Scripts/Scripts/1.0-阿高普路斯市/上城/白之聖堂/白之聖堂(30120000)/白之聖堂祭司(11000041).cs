using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:白之聖堂(30120000) NPC基本信息:白之聖堂祭司(11000041) X:8 Y:7
namespace SagaScript.M30120000
{
    public class S11000041 : Event
    {
        public S11000041()
        {
            this.EventID = 11000041;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<JobBasic_07> JobBasic_07_mask = new BitMask<JobBasic_07>(pc.CMask["JobBasic_07"]);
            BitMask<Puppet_01> Puppet_01_mask = pc.CMask["Puppet_01"];    
            BitMask<Neko_01> Neko_01_cmask = pc.CMask["Neko_01"];
            BitMask<Neko_01> Neko_01_amask = pc.AMask["Neko_01"];
            BitMask<Job2X_07> mask = pc.CMask["Job2X_07"];

            Say(pc, 11000041, 131, "歡迎唷!$R;" +
                                   "$R白之聖堂是所有人的安息之處，$R;" +
                                   "也是祭司行會的根據地。$R;", "白之聖堂祭司");


            if (!Neko_01_amask.Test(Neko_01.桃子任務完成) &&
                Neko_01_cmask.Test(Neko_01.光之精靈對話) &&
                !Neko_01_cmask.Test(Neko_01.再次與祭祀對話))
            {
                Neko_01_cmask.SetValue(Neko_01.再次與祭祀對話, true);
                Say(pc, 131, "什麼？$R用光明精靈的力量也不行嗎？$R;" +
                    "$R對不起…$P還以為以光明精靈的力量可以解决呢。$R;" +
                    "$P如果她的力量不行的話$R就只有當塔妮亞非常憤怒時$R才能把那靈魂趕走了$R;" +
                    "$P別的方法……$R;" +
                    "聽説「凱堤」有附上自己喜歡的物件$R和道具上的習性$R;" +
                    "$P如果拿著能夠吸引「凱堤」的道具$R說不定可以驅走她呢$R;");
                return;
            }
            if (!Neko_01_amask.Test(Neko_01.桃子任務完成) &&
                Neko_01_cmask.Test(Neko_01.與雷米阿對話) &&
                !Neko_01_cmask.Test(Neko_01.與祭祀對話))
            {
                Neko_01_cmask.SetValue(Neko_01.與祭祀對話, true);
                //_2A81 = true;
                Say(pc, 131, "啊……!$R;" +
                    "驅趕凱堤的方法呀……$R;");
                Say(pc, 131, "……$R;" +
                    "$P不是沒有方法$R;" +
                    "如果是擁有光之力的人，$R就可以驅趕靈魂。$R;" +
                    "$P可是…在祭司裡$R我們光之使徒是嚴禁這樣做的。$R;" +
                    "$P雖然很可惜，但我不能夠幫您喔。$R;");
                return;
            }
            

            if (JobBasic_07_mask.Test(JobBasic_07.祭司轉職成功) &&
                !JobBasic_07_mask.Test(JobBasic_07.已經轉職為祭司))
            {
                祭司轉職完成(pc);
                return;
            }

            if (pc.Job == PC_JOB.VATES && mask.Test(Job2X_07.獲得神官的烙印))//_3A88)
            {
                Say(pc, 131, pc.Name + "不是嗎？$R;" +
                    "$R哦，$R能看出您身上「神官的烙印」$R是神官的烙印呢。$R;" +
                    "$R看來您挑戰成功了啊$R;" +
                    "現在承認您的能力了。$R;" +
                    "$P要轉職為「神官」嗎？$R;");
                int a = 0;
                while (a == 0)
                {
                    switch (Select(pc, "轉職嗎?", "", "轉職", "不轉職"))
                    {
                        case 1:
                            Say(pc, 131, "給您代表神官的「神官紋章」$R;");
                            if (pc.Inventory.Equipments.Count == 0)
                            {
                                pc.JEXP = 0;
                                mask.SetValue(Job2X_07.轉職結束, true);
                                mask.SetValue(Job2X_07.聽取說明, false);
                                ChangePlayerJob(pc, PC_JOB.DRUID);
                                PlaySound(pc, 3087, false, 100, 50);
                                ShowEffect(pc, 4131);
                                Wait(pc, 4000);
                                Say(pc, 131, "……$R;" +
                                    "恭喜您$R;" +
                                    "紋章烙印好了$R;" +
                                    "現在您就是「神官」了。$R;");
                                PlaySound(pc, 4012, false, 100, 50);
                                Say(pc, 131, "轉職「神官」成功$R;");
                                return;
                            }
                            Say(pc, 131, "紋章是要烙印在皮膚上的$R;" +
                                "請把裝備脱下來$R;");
                            return;
                        case 2:
                            Say(pc, 131, "啊？您説您不轉職？$R;" +
                                "$R若是那樣的話$R您以前的努力就白費了$R沒關係嗎？$R;");
                            switch (Select(pc, "不轉職為「神官」了嗎?", "", "不轉職", "嗯…可能會轉職…"))
                            {
                                case 1:
                                    mask.SetValue(Job2X_07.聽取說明, false);
                                    mask.SetValue(Job2X_07.轉職開始, false);
                                    mask.SetValue(Job2X_07.獲得神官的烙印, false);
                                    Say(pc, 131, "…知道嗎？$R;" +
                                        "$R要以祭司的身份進行修煉$R那也是一種方法啊，$R我不會太强求的。$R;");
                                    return;
                                case 2:
                                    break;
                            }
                            break;
                    }
                }
                return;
            }
            
            
            if (CountItem(pc, 10047201) >= 1)
            {
                Say(pc, 131, "注入心靈？$R;" +
                    "$P呵呵，好的$R;" +
                    "$R在「空空的心」裡，$R;" +
                    "注入心靈是需要代價的$R;" +
                    "來，挑一個吧。$R;");
                if (Select(pc, "選擇付出代價是?", "", "交出生命的源泉", "交出魔力的源泉", "交出技術的源泉", "算了吧") != 4)
                {
                    Puppet_01_mask.SetValue(Puppet_01.聖堂祭司給予洋鐵的心, true);
                    //_3A24 = true;
                    TakeItem(pc, 10047201, 1);
                    GiveItem(pc, 10047200, 1);
                    Say(pc, 131, "……$R;" +
                        "$R我能夠感受到$R;" +
                        "您捨己為人的精神$R;" +
                        "$P這樣的想法很好！！$R;" +
                        "我就幫您注入心靈吧。$R;");
                    PlaySound(pc, 4006, false, 100, 50);
                    Say(pc, 131, "得到「洋鐵的心」$R;");
                    Say(pc, 131, "啊？需要代價嗎？$R;" +
                        "$R嘿嘿$R;" +
                        "$P對了，没有必要擔心$R;" +
                        "只是想看看您的覺悟$R;" +
                        "讓您受驚了，對不起啊$R;");
                }
                return;
            }
            

            if (pc.Job == PC_JOB.NOVICE && pc.Race != PC_RACE.DEM)
            {
                if (JobBasic_07_mask.Test(JobBasic_07.選擇轉職為祭司) &&
                    !JobBasic_07_mask.Test(JobBasic_07.已經轉職為祭司))
                {
                    祭司轉職任務(pc);
                    return;
                }
                else
                {
                    祭司簡介(pc);
                    return;
                }
            }
            
            if (pc.JobBasic == PC_JOB.VATES)
            {
                if (pc.Inventory.Equipments.Count == 0)
                {
                    Say(pc, 131, "您已經没穿衣服了$R;");
                    return;
                }

                Say(pc, 11000041, 131, pc.Name + "不是嗎?$R;" +
                                       "$R怎麼會這樣?$R;", "白之聖堂祭司");
                if (mask.Test(Job2X_07.轉職結束))
                {
                    switch (Select(pc, "想做什麼呢?", "", "任務服務台", "出售入國許可證", "什麼也不做"))
                    {
                        case 1:
                            HandleQuest(pc, 14);
                            break;

                        case 2:
                            Say(pc, 131, "要去諾頓嗎？$R;");
                            OpenShopBuy(pc, 82);
                            break;

                        case 3:
                            break;
                    }
                }

                switch (Select(pc, "做什麼呢?", "", "任務服務台", "出售諾頓入國許可證", "轉職", "什麼也不做"))
                {
                    case 1:
                        HandleQuest(pc, 14);
                        break;
                    case 2:
                        Say(pc, 131, "要去諾頓嗎？$R;");
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

        void 祭司簡介(ActorPC pc)
        {
            BitMask<JobBasic_07> JobBasic_07_mask = new BitMask<JobBasic_07>(pc.CMask["JobBasic_07"]);

            int selection;

            Say(pc, 11000041, 131, "我是領導祭司行會的祭司總管。$R;" +
                                   "$P我們這個行會，$R;" +
                                   "一直在招募新的會員唷!$R;" +
                                   "$R想不想成為『祭司』呢?$R;", "白之聖堂祭司");

            selection = Select(pc, "想做什麼?", "", "我想成為『祭司』!", "『祭司』是什麼樣的職業?", "任務服務台", "什麼也不做");

            while (selection != 4)
            {
                switch (selection)
                {
                    case 1:
                        JobBasic_07_mask.SetValue(JobBasic_07.選擇轉職為祭司, true);
                        //廢除一次職轉職任務
                        JobBasic_07_mask.SetValue(JobBasic_07.已經從菲爾那裡聽取有關治療魔法的知識, true);
                        JobBasic_07_mask.SetValue(JobBasic_07.祭司轉職任務完成, true);
                        /*
                        Say(pc, 11000041, 131, "啊! 您想成為『祭司』是嗎?$R;", "白之聖堂祭司");

                        Say(pc, 11000041, 131, "在成為『祭司』之前，$R;" +
                                               "您必須對「治療魔法」徹底的瞭解。$R;" +
                                               "$P「下城」裡，$R;" +
                                               "有個熟悉「治療魔法」的人。$R;" +
                                               "$P她的名字是「菲爾」。$R;" +
                                               "$R去見過她以後，再來吧!$R;", "白之聖堂祭司");
                        */
                        申請轉職為祭司(pc);
                        return;

                    case 2:
                        Say(pc, 11000041, 131, "塔妮亞和道米尼族，魔法力量較強，$R;" +
                                               "比較適合做祭司。$R;" +
                                               "$P特別是塔妮亞!!$R;" +
                                               "他們天生就有適合「治療魔法」的體質。$R;" +
                                               "$R祭司中，有很多塔妮亞族呢!$R;" +
                                               "$P還想聽更仔細的解說嗎?$R;", "白之聖堂祭司");

                        switch (Select(pc, "要聽嗎?", "", "聽", "不聽"))
                        {
                            case 1:
                                Say(pc, 11000041, 131, "『祭司』是醫治人的職業。$R;" +
                                                       "$R隊友或者自己受傷的時候，$R;" +
                                                       "可以迅速的復原。$R;" +
                                                       "$P隊伍中，只要存在一名祭司，$R;" +
                                                       "對隊伍非常有幫助唷!$R;" +
                                                       "$P但是攻擊力和防禦力等$R;" +
                                                       "戰鬥能力較弱，所以不適合前線戰鬥。$R;" +
                                                       "$P但是如果繼續成長的話，$R;" +
                                                       "也會有一天得到$R;" +
                                                       "提高力量和速度的能力喔!$R;", "白之聖堂祭司");
                                break;
                                
                            case 2:
                                break;
                        }
                        break;

                    case 3:
                        Say(pc, 11000041, 131, "如果想在這裡接獲任務的話，$R;" +
                                               "就成為『祭司』吧!$R;" +
                                               "$R會給您介紹對人們有益的工作唷!!$R;", "白之聖堂祭司");
                        break;
                }

                selection = Select(pc, "想做什麼?", "", "我想成為『祭司』!", "『祭司』是什麼樣的職業?", "任務服務台", "什麼也不做");
            }
        }

        void 祭司轉職任務(ActorPC pc)
        {
            BitMask<JobBasic_07> JobBasic_07_mask = new BitMask<JobBasic_07>(pc.CMask["JobBasic_07"]);

            if (!JobBasic_07_mask.Test(JobBasic_07.祭司轉職任務完成))
            {
                治療魔法相關問題回答(pc);
            }

            if (JobBasic_07_mask.Test(JobBasic_07.祭司轉職任務完成) &&
                !JobBasic_07_mask.Test(JobBasic_07.祭司轉職成功))
            {
                申請轉職為祭司(pc);
                return;
            }

        }

        void 治療魔法相關問題回答(ActorPC pc)
        {
            BitMask<JobBasic_07> JobBasic_07_mask = new BitMask<JobBasic_07>(pc.CMask["JobBasic_07"]);

            if (JobBasic_07_mask.Test(JobBasic_07.已經從菲爾那裡聽取有關治療魔法的知識))
            {
                問題01(pc);
            }
            else
            {
                Say(pc, 11000041, 131, "「下城」裡，$R;" +
                                       "有個熟悉「治療魔法」的人。$R;" +
                                       "$P她的名字是「菲爾」。$R;" +
                                       "$R去見過她以後，再來吧!$R;", "白之聖堂祭司");
            }
        }

        void 問題01(ActorPC pc)
        {
            Say(pc, 11000041, 131, "您應該聽完說明了吧?$R;" +
                                   "$R您對她所講的話是怎麼想的，$R;" +
                                   "請告訴我吧!$R;" +
                                   "$P您認為，擁有「治療魔法」$R;" +
                                   "只是為了自己嗎?$R;", "白之聖堂祭司");

            switch (Select(pc, "您怎麼想的?", "", "希望能幫到所有人", "只要治療我自己就行了"))
            {
                case 1:
                    問題02(pc);
                    break;

                case 2:
                    問題回答錯誤(pc);
                    return;
            }
        }

        void 問題02(ActorPC pc)
        {
            PlaySound(pc, 2040, false, 100, 50);

            Say(pc, 11000041, 131, "對了!$R;" +
                                   "$R如果欠別人人情的話，$R;" +
                                   "總是想報答的。$R;" +
                                   "$P如果一直懷著這種熱情去冒險，$R;" +
                                   "作為祭司的您，會有幸福的人生唷!$R;" +
                                   "$P那麼這是下一個問題。$R;" +
                                   "$R「治療魔法」$R;" +
                                   "是利用什麼精靈的力量呢?$R;", "白之聖堂祭司");

            switch (Select(pc, "是什麼精靈呢?", "", "誠心精靈", "闇之精靈", "光之精靈"))
            {
                case 1:
                    問題回答錯誤(pc);
                    return;

                case 2:
                    問題回答錯誤(pc);
                    return;

                case 3:
                    問題03(pc);
                    break;
            }
        }

        void 問題03(ActorPC pc)
        {
            PlaySound(pc, 2040, false, 100, 50);

            Say(pc, 11000041, 131, "正確答案!$R;" +
                                   "「治療魔法」是「光屬性」的魔法。$R;" +
                                   "$R修煉時注意$R;" +
                                   "不要錯過光之精靈跟您說的悄悄話。$R;" +
                                   "$P總有一天……$R;" +
                                   "那悄悄話，將會引導您到另一個世界。$R;" +
                                   "$P相信您會好好利用祭司的力量!$R;" +
                                   "$R現在批准您轉職為祭司。$R;" +
                                   "$P最後……$R;" +
                                   "$R您可以發誓，$R;" +
                                   "以後都熱心待人嗎?$R;", "白之聖堂祭司");

            switch (Select(pc, "你願意發誓嗎?", "", "我願意", "才不要"))
            {
                case 1:
                    問題回答正確(pc);
                    break;
                    
                case 2:
                    問題回答錯誤(pc);
                    return;
            }
        }

        void 問題回答正確(ActorPC pc)
        {
            BitMask<JobBasic_07> JobBasic_07_mask = new BitMask<JobBasic_07>(pc.CMask["JobBasic_07"]);

            PlaySound(pc, 2040, false, 100, 50);

            Say(pc, 11000041, 131, "以後請不要忘記那個誓言啊!$R;" +
                                   "$R真的要成為『祭司』嗎?$R;", "白之聖堂祭司");

            switch (Select(pc, "要轉職為『祭司』嗎?", "", "轉職為『祭司』", "還是算了吧"))
            {
                case 1:
                    JobBasic_07_mask.SetValue(JobBasic_07.祭司轉職任務完成, true);
                    break;
                    
                case 2:
                    return;
            }
        }

        void 問題回答錯誤(ActorPC pc)
        {
            BitMask<JobBasic_07> JobBasic_07_mask = new BitMask<JobBasic_07>(pc.CMask["JobBasic_07"]);

            JobBasic_07_mask.SetValue(JobBasic_07.已經從菲爾那裡聽取有關治療魔法的知識, false);

            PlaySound(pc, 2041, false, 100, 50);

            Say(pc, 11000041, 131, "……$R;" +
                                   "$R不好意思，$R;" +
                                   "您好像不適合做祭司呢?$R;" +
                                   "$P想知道為什麼嗎?$R;" +
                                   "$R那您還是再聽一次「菲爾」的話吧!$R;" +
                                   "好嗎?$R;", "白之聖堂祭司");
        }

        void 申請轉職為祭司(ActorPC pc)
        {
            BitMask<JobBasic_07> JobBasic_07_mask = new BitMask<JobBasic_07>(pc.CMask["JobBasic_07"]);

            Say(pc, 11000041, 131, "那麼，$R;" +
                                   "我就給您『祭司紋章』吧!$R;", "白之聖堂祭司");

            if (pc.Inventory.Equipments.Count == 0)
            {
                JobBasic_07_mask.SetValue(JobBasic_07.祭司轉職成功, true);

                PlaySound(pc, 3087, false, 100, 50);
                ShowEffect(pc, 4131);
                Wait(pc, 3960);

                Say(pc, 11000041, 131, "……$R;" +
                                       "$P這紋章是『祭司』的象徵，$R;" +
                                       "今天開始您就是『祭司』了!$R;", "白之聖堂祭司");

                PlaySound(pc, 4012, false, 100, 50);
                ChangePlayerJob(pc, PC_JOB.VATES);
                Say(pc, 0, 0, "您已經轉職為『祭司』了!$R;", " ");

                Say(pc, 11000041, 131, "以後遇到困難時，$R;" +
                                       "隨時到白之聖堂來吧!$R;" +
                                       "$P我有禮物送給您，$R;" +
                                       "先去整理行李，然後再來吧!$R;", "白之聖堂祭司");
            }
            else
            {
                Say(pc, 11000041, 131, "紋章是烙印在皮膚上的，$R;" +
                                       "先把裝備脫掉吧。$R;", "白之聖堂祭司");
            }
        }

        void 祭司轉職完成(ActorPC pc)
        {
            BitMask<JobBasic_07> JobBasic_07_mask = new BitMask<JobBasic_07>(pc.CMask["JobBasic_07"]);

            if (pc.Inventory.Equipments.Count != 0)
            {
                JobBasic_07_mask.SetValue(JobBasic_07.已經轉職為祭司, true);

                Say(pc, 11000041, 131, "這是祭司專用帽子，$R;" +
                                       "請收下吧。$R;", "白之聖堂祭司");

                PlaySound(pc, 2040, false, 100, 50);

                GiveItem(pc, 50021300, 1);
                Say(pc, 0, 0, "得到『成熟的帽』!$R;", " ");

                LearnSkill(pc, 3054);
                Say(pc, 0, 0, "學到『恢復』!$R;", " ");
            }
            else
            {
                Say(pc, 11000041, 131, "請先穿上衣服。$R;", "白之聖堂祭司");
            }
        }

        void 進階轉職(ActorPC pc)
        {

            BitMask<Job2X_07> mask = pc.CMask["Job2X_07"];
            if (mask.Test(Job2X_07.轉職開始))//_3A84)
            {
                Say(pc, 131, "您現在正接受全職的訓練$R;");
                return;
            }
            Say(pc, 131, "嗯。$R您要轉職成為「神官」啊？$R;");
            switch (Select(pc, "要轉職成為「神官」嗎?", "", "轉職", "不轉職"))
            {
                case 1:
                    if (pc.Job == PC_JOB.DRUID)
                    {
                        Say(pc, 131, "您已經是神官了$R;");
                        return;
                    }
                    if (pc.JobLevel1 < 30)
                    {
                        Say(pc, 131, "現在還不適合您$R;" +
                            "我會再來找您$R;" +
                            "$R您還是等著吧$R;");
                        return;
                    }
                    Say(pc, 131, "……$R;" +
                        "$P您具有相當的實力呢$R;");
                    if (!mask.Test(Job2X_07.聽取說明))//_3A83)
                    {
                        Say(pc, 131, "您對神官了解多少呢？$R;" +
                            "需不需要解釋一下$R有關神官的消息呢？$R;");
                        switch (Select(pc, "要聽嗎?", "", "聽", "不聽"))
                        {
                            case 1:
                                Say(pc, 131, "「神官」是比祭司高級的職業$R;" +
                                    "$R不僅借取光明精靈的力量，$R而且還能召喚光明精靈和光之場所。$R;" +
                                    "能夠使出更强力的魔法$R;");
                                Say(pc, 131, "成為「神官」所學到的能力$R會在跟同伴們冒險時，有所幫助的$R;" +
                                    "$P具體來説的話，$R是一種可以借給同伴力量的技能$R;" +
                                    "$P如果能夠活用『祭司』的技能的話，$R您會成為隊伍裡不可缺少的成員$R;");
                                mask.SetValue(Job2X_07.聽取說明, true);
                                //_3A62 = true;
                                break;
                        }
                    }

                    Say(pc, 131, "「神官」一職呢$R在祭司當中，也是特別挑選的$R;" +
                        "$P為了轉職成為神官，$R不得不接受行會艱苦的訓練$R;" +
                        "$R您要挑戰嗎？$R;");
                    switch (Select(pc, "挑戰嗎?", "", "挑戰", "不挑戰"))
                    {
                        case 1:
                            mask.SetValue(Job2X_07.轉職開始, true);
                            //_3A84 = true;
                            Say(pc, 131, "好了！$R;" +
                                "您會受到成為神官之前的磨練的。$R;" +
                                "我們的存在是要治療人，$R您要用您的治療能力救很多人唷!$R;" +
                                "$P因為您有「神官的烙印」的標誌，$R;" +
                                "那是一種刻在身上的烙印$R;" +
                                "$P高級的治療師，看見這標記，$R便會承認您是神官$R;" +
                                "來領取「神官的烙印」吧$R;" +
                                "$P「神官的烙印」可以説是感恩的心，$R得到您治療的人$R會欣然的承認您是「神官」的。$R;" +
                                "$P那麼希望您幸運。$R;");
                            break;
                    }
                    break;
            }
            
        }
    }
}

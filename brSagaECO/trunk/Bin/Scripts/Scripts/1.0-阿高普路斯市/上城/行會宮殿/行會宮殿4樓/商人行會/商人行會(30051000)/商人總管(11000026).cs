using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:商人行會(30051000) NPC基本信息:商人總管(11000026) X:3 Y:3
namespace SagaScript.M30051000
{
    public class S11000026 : Event
    {
        public S11000026()
        {
            this.EventID = 11000026;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<JobBasic_12> JobBasic_12_mask = new BitMask<JobBasic_12>(pc.CMask["JobBasic_12"]);
            BitMask<Job2X_12> Job2X_12_mask = pc.CMask["Job2X_12"];
            BitMask<Neko_04> Neko_04_amask = pc.AMask["Neko_04"];
            BitMask<Neko_04> Neko_04_cmask = pc.CMask["Neko_04"];

            if (Neko_04_amask.Test(Neko_04.任務開始) &&
                !Neko_04_amask.Test(Neko_04.任務結束) && 
                Neko_04_cmask.Test(Neko_04.拿回不明部品) &&
                !Neko_04_cmask.Test(Neko_04.交回不明部品))
            {
                Neko_04_cmask.SetValue(Neko_04.交回不明部品, true);
                TakeItem(pc, 10054000, 1);
                Say(pc, 11000026, 131, "哦$R這是商人交待的東西嗎$R;" +
                    "$R這是？$R;" +
                    "$P看起來像機器人的胳膊啊。$R;" +
                    "$R咦，裡面有商人的信呢。$R;" +
                    "$P呵呵，原來是這樣啊$R;" +
                    "$P能不能幫我和商人説$R一定會轉交給那個人。$R;" +
                    "$R辛苦了$R;");
                return;
            }
            if (Neko_04_amask.Test(Neko_04.任務開始) &&
                !Neko_04_amask.Test(Neko_04.任務結束) &&
                Neko_04_cmask.Test(Neko_04.收到商人的傳達品) &&
                !Neko_04_cmask.Test(Neko_04.被告知襲擊))
            {
                TakeItem(pc, 50071151, 1);
                Neko_04_cmask.SetValue(Neko_04.交出商人的傳達品, true);
                Say(pc, 11000026, 131, "啊！$R是古魯杜交代的東西嗎？$R;" +
                    "$R看看。$R;");
                Wait(pc, 666);
                ShowEffect(pc, 11000026, 5049);
                Wait(pc, 666);
                ShowEffect(pc, 0, 8013);
                PlaySound(pc, 2430, false, 100, 50);
                Wait(pc, 1000);
                ShowEffect(pc, 11000026, 8013);
                PlaySound(pc, 2430, false, 100, 50);
                Say(pc, 0, 131, "一陣閃電$R;" +
                    "$R暈厥$R;");
                Wait(pc, 3333);
                pc.CInt["Neko04_01_Map"] = CreateMapInstance(50002000, 30113000, 13, 1);
                Warp(pc, (uint)pc.CInt["Neko04_01_Map"], 3, 5);
                //EVENTMAP_IN 2 1 3 5 4
                return;
            }//*/

            if (Job2X_12_mask.Test(Job2X_12.防禦過高))
            {
                Say(pc, 131, "那麼就給您烙印上這象徵貿易商的$R;" +
                    "『貿易商紋章』吧$R;");
                if (pc.Inventory.Equipments.Count == 0)
                {
                    Job2X_12_mask.SetValue(Job2X_12.轉職成功, true);
                    Job2X_12_mask.SetValue(Job2X_12.收集結束, false);
                    Job2X_12_mask.SetValue(Job2X_12.聽取貿易商說明, false);
                    Job2X_12_mask.SetValue(Job2X_12.轉職開始, false);
                    Job2X_12_mask.SetValue(Job2X_12.防禦過高, false);
                    Job2X_12_mask.SetValue(Job2X_12.收集花束, false);
                    Job2X_12_mask.SetValue(Job2X_12.給予花束, false);

                    ChangePlayerJob(pc, PC_JOB.TRADER);
                    pc.JEXP = 0;

                    PlaySound(pc, 3087, false, 100, 50);
                    ShowEffect(pc, 4131);
                    Wait(pc, 4000);
                    Say(pc, 131, "…$R;" +
                        "$P恭喜您成為貿易商$R;" +
                        "$R以後您就是『貿易商』了。$R;");
                    PlaySound(pc, 4012, false, 100, 50);
                    Say(pc, 131, "您已轉職成為『貿易商』了$R;");
                    return;
                }

                Say(pc, 131, "防禦太高的話，就無法烙印紋章了$R;" +
                    "請換上輕便的服裝後，再來吧。$R;");
            }

            Say(pc, 11000026, 131, "您好，歡迎光臨商人行會。$R;", "商人總管");

            if (JobBasic_12_mask.Test(JobBasic_12.商人轉職成功) &&
                !JobBasic_12_mask.Test(JobBasic_12.已經轉職為商人))
            {
                商人轉職完成(pc);
                return;
            }

            if (pc.Job == PC_JOB.NOVICE && pc.Race != PC_RACE.DEM)
            {
                if (JobBasic_12_mask.Test(JobBasic_12.選擇轉職為商人) &&
                    !JobBasic_12_mask.Test(JobBasic_12.已經轉職為商人))
                {
                    商人轉職任務(pc);
                    return;
                }
                else
                {
                    商人簡介(pc);
                    return;
                }
            }

            if (pc.JobBasic == PC_JOB.MERCHANT)
            {
                if (pc.Job == PC_JOB.MERCHANT && Job2X_12_mask.Test(Job2X_12.收集結束))//_3A64)
                {
                    Say(pc, 131, pc.Name + "是您嗎？$R;" +
                        "$R呵呵，$R您通過考驗了$R;" +
                        "$R商人行會承認您的力量$R;" +
                        "$P要轉職為『貿易商』嗎？$R;");
                    進階轉職選擇(pc);
                    return;
                }
                if (pc.Inventory.Equipments.Count == 0)
                {
                    Say(pc, 131, "先穿上衣服吧$R;");
                    return;
                }
                Say(pc, 131, pc.Name + "是您嗎?$R;" +
                    "$R今天是什麼事啊？?$R;");
                if (Job2X_12_mask.Test(Job2X_12.轉職成功))//_3A65)
                {
                    進階職業對話(pc);
                    return;
                }

                switch (Select(pc, "要做些什麼呢?", "", "任務服務台", "我想轉職", "出售帕斯特入國許可證", "什麼也不做"))
                {
                    case 1:
                        HandleQuest(pc, 42);
                        break;

                    case 2:
                        if (Job2X_12_mask.Test(Job2X_12.轉職開始))//_3A63)
                        {
                            Say(pc, 131, "$R您現在正在接受轉職的考驗呢$R;");
                            進階職業對話(pc);
                            return;
                        }
                        Say(pc, 131, "哈哈，$R想轉職為『貿易商』啊？$R;");
                        switch (Select(pc, "真的要轉職嗎?", "", "我想成為貿易商", "還是算了吧"))
                        {
                            case 1:
                                進階轉職判斷(pc);
                                break;
                            case 2:
                                break;
                        }
                        break;

                    case 3:
                        OpenShopBuy(pc, 186);
                        Say(pc, 131, "帕斯特有分會，總部在摩根呢$R;" +
                            "$R經過一定要進去看看喔！$R;");
                        break;


                    case 4:
                        break;
                }
            } 
        }

        void 商人簡介(ActorPC pc)
        {
            BitMask<JobBasic_12> JobBasic_12_mask = new BitMask<JobBasic_12>(pc.CMask["JobBasic_12"]);

            int selection;

            Say(pc, 11000026, 131, "我是管理商人行會的商人總管。$R;" +
                                   "$P啊， 您還沒有加入行會呢。$R;" +
                                   "$P怎樣?$R;" +
                                   "想不想成為『商人』呢?$R;", "商人總管");

            selection = Select(pc, "想做什麼?", "", "我想成為『商人』!", "『商人』是什麼樣的職業?", "任務服務台", "什麼也不做");

            while (selection != 4)
            {
                switch (selection)
                {
                    case 1:
                        //廢除一次職轉職任務
                        JobBasic_12_mask.SetValue(JobBasic_12.選擇轉職為商人, true);
                        JobBasic_12_mask.SetValue(JobBasic_12.轉交商人總管的信, true);
                        JobBasic_12_mask.SetValue(JobBasic_12.給予老爺爺的錢包, true);
                        JobBasic_12_mask.SetValue(JobBasic_12.給予老爺爺的手帕, true);
                        JobBasic_12_mask.SetValue(JobBasic_12.給予老爺爺的魔杖, true);
                        JobBasic_12_mask.SetValue(JobBasic_12.給予老爺爺的眼鏡, true);
                        JobBasic_12_mask.SetValue(JobBasic_12.給予老爺爺的褲子, true);
                        JobBasic_12_mask.SetValue(JobBasic_12.給予老爺爺的假牙, true);
                        JobBasic_12_mask.SetValue(JobBasic_12.商人轉職任務完成, true);
                        /*
                        Say(pc, 11000026, 131, "要成為『商人』嗎?$R;" +
                                               "$P沒有別的條件，$R;" +
                                               "但是總得有個測試吧?$R;" +
                                               "$R將信交給在「下城」的$R;" +
                                               "「健忘的老人」吧。$R;" +
                                               "$R那就是轉職的條件?$R;", "商人總管");

                        switch (Select(pc, "接受嗎?", "", "沒問題", "才不要"))
                        {
                            case 1:
                                JobBasic_12_mask.SetValue(JobBasic_12.選擇轉職為商人, true);

                                Say(pc, 11000026, 131, "拜託了$R;", "商人總管");

                                PlaySound(pc, 2030, false, 100, 50);
                                GiveItem(pc, 10043080, 1);
                                Say(pc, 0, 0, "得到『信 (任務)』$R;", " ");
                                break;

                            case 2:
                                Say(pc, 11000026, 131, "考慮清楚再來吧。$R;", "商人總管");
                                break;
                        }
                        */
                        申請轉職為商人(pc);
                        return;

                    case 2:
                        Say(pc, 11000026, 131, "商人是適合埃米爾族做的職業。$R;" +
                                               "$R如果是別的種族就會辛苦一點喔!$R;" +
                                               "$P我會仔細解說給您聽的。$R;", "商人總管");

                        switch (Select(pc, "還要聽下去嗎?", "", "我要聽", "不聽"))
                        {
                            case 1:
                                Say(pc, 11000026, 131, "簡單的說『商人』是擅長買賣的職業。$R;" +
                                                       "$P會以低價買入道具，高價賣出。$R;" +
                                                       "$R所以比起別的職業，是不缺錢的。$R;" +
                                                       "$P而且搬運能力也很出色。$R;" +
                                                       "$P還可以在派遣到各國的商人行會$R;" +
                                                       "所屬商人那，裡$R;" +
                                                       "得到多樣的服務。$R;" +
                                                       "$P一定要多加利用啊。$R;", "商人總管");
                                break;

                            case 2:
                                break;
                        }
                        break;

                    case 3:
                        Say(pc, 11000026, 131, "想要在這裡工作，就要成為『商人』。$R;" +
                                               "$P怎麼樣?$R;" +
                                               "$R您想做『商人』嗎?$R;", "商人總管");
                        break;
                }

                selection = Select(pc, "想做什麼?", "", "我想成為『商人』!", "『商人』是什麼樣的職業?", "任務服務台", "什麼也不做");
            }
        }

        void 商人轉職任務(ActorPC pc)
        {
            BitMask<JobBasic_12> JobBasic_12_mask = new BitMask<JobBasic_12>(pc.CMask["JobBasic_12"]);

            if (!JobBasic_12_mask.Test(JobBasic_12.商人轉職任務完成))
            {
                給予印章(pc);
            }

            if (JobBasic_12_mask.Test(JobBasic_12.商人轉職任務完成) &&
                !JobBasic_12_mask.Test(JobBasic_12.商人轉職成功))
            {
                申請轉職為商人(pc);
                return;
            }
        }

        void 給予印章(ActorPC pc)
        {
            BitMask<JobBasic_12> JobBasic_12_mask = new BitMask<JobBasic_12>(pc.CMask["JobBasic_12"]);

            if (JobBasic_12_mask.Test(JobBasic_12.給予老爺爺的假牙) &&
                CountItem(pc, 80008500) > 0)
            {
                Say(pc, 11000026, 131, "啊，我的印章!$R;", "商人總管");



                Say(pc, 11000026, 131, "竟然把這麼重要的東西給忘掉了。$R;" +
                                       "$R呵呵，真是不好意思。$R;" +
                                       "$P那就承認您是『商人』吧!$R;" +
                                       "$R真的要成為『商人』嗎?$R;", "商人總管");

                switch (Select(pc, "要轉職為『商人』嗎?", "", "轉職為『商人』", "還是算了吧"))
                {
                    case 1:
                        JobBasic_12_mask.SetValue(JobBasic_12.商人轉職任務完成, true);

                        PlaySound(pc, 2040, false, 100, 50);
                        TakeItem(pc, 80008500, 1);
                        Say(pc, 0, 0, "已經轉交印章。$R;", " ");
                        break;

                    case 2:
                        break;
                }
            }
            else
            {
                Say(pc, 11000026, 131, "將信交給在「下城」的$R;" +
                                       "「健忘的老人」吧。$R;" +
                                       "$R這就是就職的條件喔!$R;", "商人總管");
            }
        }

        void 申請轉職為商人(ActorPC pc)
        {
            BitMask<JobBasic_12> JobBasic_12_mask = new BitMask<JobBasic_12>(pc.CMask["JobBasic_12"]);

            Say(pc, 11000026, 131, "那麼就給您烙印上這象徵『商人』的$R;" +
                                   "『商人紋章』吧$R;", "商人總管");

            if (pc.Inventory.Equipments.Count == 0)
            {
                JobBasic_12_mask.SetValue(JobBasic_12.商人轉職成功, true);

                PlaySound(pc, 3087, false, 100, 50);
                ShowEffect(pc, 4131);
                Wait(pc, 3960);

                Say(pc, 11000026, 131, "…$R;" +
                                       "$P恭喜您，烙印好了$R;" +
                                       "您身上已經烙印了漂亮的紋章。$R;" +
                                       "$R從今以後，$R;" +
                                       "您就成為代表我們的『商人』了。$R;", "商人總管");

                PlaySound(pc, 4012, false, 100, 50);
                ChangePlayerJob(pc, PC_JOB.MERCHANT);

                Say(pc, 0, 0, "您已經轉職為『商人』了!$R;", " ");

                Say(pc, 11000026, 131, "穿好衣服再和我說話吧。$R;" +
                                       "$R我要給您禮物，$R;" +
                                       "您先整理您的行李吧?$R;", "商人總管");
            }
            else
            {
                Say(pc, 11000026, 131, "紋章是烙印在皮膚上的，$R;" +
                                       "先把裝備脫掉吧。$R;", "商人總管");
            }
        }

        void 商人轉職完成(ActorPC pc)
        {
            BitMask<JobBasic_12> JobBasic_12_mask = new BitMask<JobBasic_12>(pc.CMask["JobBasic_12"]);

            if (pc.Inventory.Equipments.Count != 0)
            {
                JobBasic_12_mask.SetValue(JobBasic_12.已經轉職為商人, true);
                Say(pc, 11000026, 131, "這是禮物，收下吧。$R;", "商人總管");

                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, 50071300, 1);
                Say(pc, 0, 0, "得到『手挽袋』!$R;", " ");

                LearnSkill(pc, 702);
                Say(pc, 0, 0, "學到『提升體積』!R;", " ");
            }
            else
            {
                Say(pc, 11000026, 131, "穿好衣服再和我說話吧。$R;", "商人總管");
            }
        }

        void 進階職業對話(ActorPC pc)
        {
            BitMask<Job2X_12> Job2X_12_mask = pc.CMask["Job2X_12"];

            if (pc.Job == PC_JOB.GAMBLER && !Job2X_12_mask.Test(Job2X_12.第一次對話))//_7a22)
            {
                Job2X_12_mask.SetValue(Job2X_12.第一次對話, true);
                //_7a22 = true;
                Say(pc, 131, "啊，您竟然成為「勝負師」了。$R;" +
                    "$P已經變成勝負師了就没辦法了。$R;" +
                    "$R一定要誠實的生活啊$R;");
                return;
            }
            /*
            if (!_7a20 && _7a19)
            {
                _7a20 = true;
                Say(pc, 131, "想要變更職業？$R;" +
                    "$P哈哈$R;" +
                    "$R您好像弄錯了。$R;" +
                    "$R貿易商没有隱藏的力量$R;" +
                    "也不能轉職。$R;" +
                    "$P不要想太多，專心經商吧。$R;");
                return;
            }
            */
                        switch (Select(pc, "要做什麼？", "", "任務服務台", "出售帕斯特入國許可證", "什麼也不做。"))
            {
                case 1:
                    Say(pc, 0, 0, "目前尚未實裝$R;", " ");
                    break;
                case 2:
                    OpenShopBuy(pc, 186);
                    Say(pc, 131, "帕斯特有分會，總部在摩根呢$R;" +
                        "$R經過一定要進去看看喔！$R;");
                    break;
                case 3:
                    break;
            }
        }

        void 進階轉職判斷(ActorPC pc)
        {
            BitMask<Job2X_12> Job2X_12_mask = pc.CMask["Job2X_12"];

            if (pc.JobLevel1 < 30)
            {
                Say(pc, 131, "…$R;" +
                    "$P以您現在的實力，還不行。$R;");
                return;
            }

            if (pc.Job == PC_JOB.TRADER)
            {
                Say(pc, 131, "您已經是貿易商了。?$R;");
                return;
            }

            Say(pc, 131, "…$R;" +
                "$P呵呵，$R您好像滿有實力的啊$R;");

            if (!Job2X_12_mask.Test(Job2X_12.聽取貿易商說明))//_3A62)
            {
                Say(pc, 131, "您對『貿易商』了解多少呢？$R;" +
                    "給您解釋一下『貿易商』啊？$R;");
                switch (Select(pc, "要聽説明嗎？", "", "聽", "不聽"))
                {
                    case 1:
                        Say(pc, 131, "『貿易商』是商人的上級職位$R;" +
                            "$P從很久的古代開始就叫『貿易商』$R;" +
                            "$P現在還有「傳達者」或者$R「交換者」的意義。$R;");
                        Say(pc, 131, "『貿易商』的能力$R主要在冒險時候特別有用$R;" +
                            "$P就是雇傭力量大的傭兵來下命令$R;" +
                            "$R如果配合其他技能一起使用的話，$R您將會是在隊伍裡不可或缺的一個呢。$R;" +
                            "$P這是真的阿$R;");
                        Job2X_12_mask.SetValue(Job2X_12.聽取貿易商說明, true);
                        //_3A62 = true;
                        break;
                    case 2:
                        break;
                }
            }

            Say(pc, 131, "『貿易商』是$R在商人裡面挑選出來的商人。$R;" +
                "$P要成為貿易商的話$R要經歷很痛苦的過程$R;" +
                "$R要挑戰嗎？$R;");

            switch (Select(pc, "要挑戰嗎？", "", "挑戰", "不挑戰"))
            {
                case 1:
                    Job2X_12_mask.SetValue(Job2X_12.轉職開始, true);
                    Job2X_12_mask.SetValue(Job2X_12.搜集紋章紙, true);
                    //_3A63 = true;
                    //_3A69 = true;
                    Say(pc, 131, "…好$R;" +
                        "$R『貿易商』必要的能力$R是搜集客人需要的商品，$R且準確轉達的能力。$R;" +
                        "$P給您的任務就是搜集十張紋章紙，$R交給不知道在哪裡的貿易商古魯杜。$R;" +
                        "$P貿易商古魯杜是行會交易額最高$R的貿易商$R;" +
                        "$R而且喜歡親自送到客人那裡$R的特別商人。$R;" +
                        "$P他是非常忙的人，$R所以不知道他現在在哪裡。$R;" +
                        "$R您親自去找找吧。$R;" +
                        "$R但是…$R;" +
                        "$P要搜集完10張以後，一起交給他。$R;" +
                        "$R商品數量準確是得到客人$R信用的重要要素。$R;" +
                        "$P那麼您就全力以赴吧。$R;");
                    break;
                case 2:
                    Say(pc, 131, "是嗎$R;" +
                        "$R那就好好想想吧。$R;");
                    break;
            }
        }

        void 進階轉職選擇(ActorPC pc)
        {
            BitMask<Job2X_12> Job2X_12_mask = pc.CMask["Job2X_12"];

            switch (Select(pc, "真的要轉職嗎?", "", "我想成為貿易商", "還是算了吧"))
            {
                case 1:
                    Say(pc, 131, "那麼就給您烙印上這象徵貿易商的$R;" +
                        "『貿易商紋章』吧$R;");
                    if (pc.Inventory.Equipments.Count == 0)
                    {
                        Job2X_12_mask.SetValue(Job2X_12.轉職成功, true);
                        Job2X_12_mask.SetValue(Job2X_12.收集結束, false);
                        Job2X_12_mask.SetValue(Job2X_12.聽取貿易商說明, false);
                        Job2X_12_mask.SetValue(Job2X_12.轉職開始, false);
                        Job2X_12_mask.SetValue(Job2X_12.防禦過高, false);
                        Job2X_12_mask.SetValue(Job2X_12.收集花束, false);
                        Job2X_12_mask.SetValue(Job2X_12.給予花束, false);

                        ChangePlayerJob(pc, PC_JOB.TRADER);
                        pc.JEXP = 0;

                        PlaySound(pc, 3087, false, 100, 50);
                        ShowEffect(pc, 4131);
                        Wait(pc, 4000);
                        Say(pc, 131, "…$R;" +
                            "$P恭喜您成為貿易商$R;" +
                            "$R以後您就是『貿易商』了。$R;");
                        PlaySound(pc, 4012, false, 100, 50);
                        Say(pc, 131, "您已轉職成為『貿易商』了$R;");
                        return;
                    }
                    Job2X_12_mask.SetValue(Job2X_12.防禦過高, true);
                    //_3A66 = true;
                    Say(pc, 131, "防禦太高的話，就無法烙印紋章了$R;" +
                        "請換上輕便的服裝後，再來吧。$R;");
                    break;
                case 2:
                    Say(pc, 131, "什麼？不轉職嗎？$R;" +
                        "$R那一直所付出的努力就白費了，$R没關係嗎？$R;");
                    switch (Select(pc, "真的要轉職嗎?", "", "不轉職", "轉職"))
                    {
                        case 1:
                            Job2X_12_mask.SetValue(Job2X_12.聽取貿易商說明, false);
                            Job2X_12_mask.SetValue(Job2X_12.轉職開始, false);
                            Job2X_12_mask.SetValue(Job2X_12.收集結束, false);
                            Job2X_12_mask.SetValue(Job2X_12.轉職成功, false);
                            Job2X_12_mask.SetValue(Job2X_12.防禦過高, false);
                            Job2X_12_mask.SetValue(Job2X_12.搜集紋章紙, false);
                            Job2X_12_mask.SetValue(Job2X_12.收集解毒果, false);
                            Job2X_12_mask.SetValue(Job2X_12.收集黃麥粉, false);
                            Job2X_12_mask.SetValue(Job2X_12.收集石油, false);
                            Job2X_12_mask.SetValue(Job2X_12.收集花束, false);
                            Job2X_12_mask.SetValue(Job2X_12.給予紋章紙, false);
                            Job2X_12_mask.SetValue(Job2X_12.給予解毒果, false);
                            Job2X_12_mask.SetValue(Job2X_12.給予黃麥粉, false);
                            Job2X_12_mask.SetValue(Job2X_12.給予石油, false);
                            Job2X_12_mask.SetValue(Job2X_12.給予花束, false);

                            Say(pc, 131, "知道了，$R不會勉强您的$R;");
                            break;
                        case 2:
                            進階轉職選擇(pc);
                            break;
                    }
                    break;
            }
        }
    }
}

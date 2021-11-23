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
                Say(pc, 11000026, 131, "哦$R这是商人交待的东西吗$R;" +
                    "$R这是？$R;" +
                    "$P看起来像机器人的胳膊啊。$R;" +
                    "$R咦，里面有商人的信呢。$R;" +
                    "$P呵呵，原来是这样啊$R;" +
                    "$P能不能帮我和商人说$R一定会转交给那个人。$R;" +
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
                Say(pc, 11000026, 131, "啊！$R是古鲁杜交代的东西吗？$R;" +"$R看看。$R;");
                Wait(pc, 666);
                ShowEffect(pc, 11000026, 5049);
                Wait(pc, 666);
                ShowEffect(pc, 0, 8013);
                PlaySound(pc, 2430, false, 100, 50);
                Wait(pc, 1000);
                ShowEffect(pc, 11000026, 8013);
                PlaySound(pc, 2430, false, 100, 50);
                Say(pc, 0, 131, "一阵闪电$R;" +
                    "$R晕厥$R;");
                Wait(pc, 3333);
                pc.CInt["Neko04_01_Map"] = CreateMapInstance(50002000, 30113000, 13, 1);
                Warp(pc, (uint)pc.CInt["Neko04_01_Map"], 3, 5);
                //EVENTMAP_IN 2 1 3 5 4
                return;
            }//*/

            if (Job2X_12_mask.Test(Job2X_12.防禦過高))
            {
                Say(pc, 131, "那么就给您烙印上这象征贸易家的$R;" +
                    "『贸易家纹章』吧$R;");
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
                        "$P恭喜您成为贸易家$R;" +
                        "$R以后您就是『贸易家』了。$R;");
                    PlaySound(pc, 4012, false, 100, 50);
                    Say(pc, 131, "您已转职成为『贸易家』了$R;");
                    return;
                }

                Say(pc, 131, "防御太高的话，就无法烙印纹章了$R;" +
                    "请换上轻便的服装后，再来吧。$R;");
            }

            Say(pc, 11000026, 131, "您好，欢迎光临商人行会。$R;", "商人总管");

            if (JobBasic_12_mask.Test(JobBasic_12.商人轉職成功) &&
                !JobBasic_12_mask.Test(JobBasic_12.已經轉職為商人))
            {
                商人轉職完成(pc);
                return;
            }

            if (pc.Job == PC_JOB.NOVICE)
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
                    Say(pc, 131, pc.Name + "是您吗？$R;" +
                        "$R呵呵，$R您通过考验了$R;" +
                        "$R商人行会承认您的力量$R;" +
                        "$P要转职为『贸易家』吗？$R;");
                    進階轉職選擇(pc);
                    return;
                }
                if (pc.Inventory.Equipments.Count == 0)
                {
                    Say(pc, 131, "先穿上衣服吧$R;");
                    return;
                }
                Say(pc, 131, pc.Name + "是您吗?$R;" +
                    "$R今天是什么事啊？?$R;");
                if (Job2X_12_mask.Test(Job2X_12.轉職成功))//_3A65)
                {
                    進階職業對話(pc);
                    return;
                }

                switch (Select(pc, "要做些什么呢?", "", "任务服务台", "我想转职", "购买法伊斯特入国许可证", "什么也不做"))
                {
                    case 1:
                        HandleQuest(pc, 42);
                        break;

                    case 2:
                        if (Job2X_12_mask.Test(Job2X_12.轉職開始))//_3A63)
                        {
                            Say(pc, 131, "$R您现在正在接受转职的考验呢$R;");
                            進階職業對話(pc);
                            return;
                        }
                        Say(pc, 131, "哈哈，$R想转职为『贸易家』啊？$R;");
                        switch (Select(pc, "真的要转职吗?", "", "我想成为贸易家", "还是算了吧"))
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
                        Say(pc, 131, "法伊斯特有分会，总部在摩根呢$R;" +
                            "$R经过一定要进去看看喔！$R;");
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
                    Say(pc, 0, 0, "请注意$R;" +
                                   "目前BP系的技能大部分未实装$R;" +
                                   "不能保证技能的使用性没有问题$R;" +
                                   "同样,相关任务也得不到保证$R;" +
                                   "请谨慎选择转职商人?$R;", "谜之音");
            Say(pc, 11000026, 131, "我是管理商人行会的商人总管。$R;" +
                                   "$P啊， 您还没有加入行会呢。$R;" +
                                   "$P怎样?$R;" +
                                   "想不想成为『商人』呢?$R;", "商人总管");

            selection = Select(pc, "想做什么?", "", "我想成为『商人』!", "『商人』是什么样的职业?", "任务服务台", "什么也不做");

            while (selection != 4)
            {
                switch (selection)
                {
                    case 1:
                        Say(pc, 11000026, 131, "要成为『商人』吗?$R;" +
                                               "$P没有别的条件，$R;" +
                                               "但是总得有个测试吧?$R;" +
                                               "$R将信交给在「下城」的$R;" +
                                               "「健忘的老人」吧。$R;" +
                                               "$R那就是转职的条件?$R;", "商人总管");

                        switch (Select(pc, "接受吗?", "", "没问题", "才不要"))
                        {
                            case 1:
                                JobBasic_12_mask.SetValue(JobBasic_12.選擇轉職為商人, true);

                                Say(pc, 11000026, 131, "拜托了$R;", "商人总管");

                                PlaySound(pc, 2030, false, 100, 50);
                                GiveItem(pc, 10043080, 1);
                                Say(pc, 0, 0, "得到『信 (任务)』$R;", " ");
                                break;

                            case 2:
                                Say(pc, 11000026, 131, "考虑清楚再来吧。$R;", "商人总管");
                                break;
                        }
                        return;

                    case 2:
                        Say(pc, 11000026, 131, "商人是适合埃米尔做的职业。$R;" +
                                               "$R如果是别的种族就会辛苦一点喔!$R;" +
                                               "$P我会仔细解说给您听的。$R;", "商人总管");

                        switch (Select(pc, "还要听下去吗?", "", "我要听", "不听"))
                        {
                            case 1:
                                Say(pc, 11000026, 131, "简单的说『商人』是擅长买卖的职业。$R;" +
                                                       "$P会以低价买入道具，高价卖出。$R;" +
                                                       "$R所以比起别的职业，是不缺钱的。$R;" +
                                                       "$P而且搬运能力也很出色。$R;" +
                                                       "$P还可以在派遣到各国的商人行会$R;" +
                                                       "所属商人那里$R;" +
                                                       "得到多样的服务。$R;" +
                                                       "$P一定要多加利用啊。$R;", "商人总管");
                                break;

                            case 2:
                                break;
                        }
                        break;

                    case 3:
                        Say(pc, 11000026, 131, "想要在这里工作，就要成为『商人』。$R;" +
                                               "$P怎么样?$R;" +
                                               "$R您想做『商人』吗?$R;", "商人总管");
                        break;
                }

                selection = Select(pc, "想做什么?", "", "我想成为『商人』!", "『商人』是什么样的职业?", "任务服务台", "什么也不做");
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
                Say(pc, 11000026, 131, "啊，我的印章!$R;", "商人总管");



                Say(pc, 11000026, 131, "竟然把这么重要的东西给忘掉了。$R;" +
                                       "$R呵呵，真是不好意思。$R;" +
                                       "$P那就承认您是『商人』吧!$R;" +
                                       "$R真的要成为『商人』吗?$R;", "商人总管");

                switch (Select(pc, "要转职为『商人』吗?", "", "转职为『商人』", "还是算了吧"))
                {
                    case 1:
                        JobBasic_12_mask.SetValue(JobBasic_12.商人轉職任務完成, true);

                        PlaySound(pc, 2040, false, 100, 50);
                        TakeItem(pc, 80008500, 1);
                        Say(pc, 0, 0, "已经转交印章。$R;", " ");
                        break;

                    case 2:
                        break;
                }
            }
            else
            {
                Say(pc, 11000026, 131, "将信交给在「下城」的$R;" +
                                       "「健忘的老人」吧。$R;" +
                                       "$R这就是就职的条件喔!$R;", "商人总管");
            }
        }

        void 申請轉職為商人(ActorPC pc)
        {
            BitMask<JobBasic_12> JobBasic_12_mask = new BitMask<JobBasic_12>(pc.CMask["JobBasic_12"]);

            Say(pc, 11000026, 131, "那么就给您烙印上这象征『商人』的$R;" +
                                   "『商人纹章』吧$R;", "商人总管");

            if (pc.Inventory.Equipments.Count == 0)
            {
                JobBasic_12_mask.SetValue(JobBasic_12.商人轉職成功, true);

                PlaySound(pc, 3087, false, 100, 50);
                ShowEffect(pc, 4131);
                Wait(pc, 3960);

                Say(pc, 11000026, 131, "…$R;" +
                                       "$P恭喜您，烙印好了$R;" +
                                       "您身上已经烙印了漂亮的纹章。$R;" +
                                       "$R从今以后，$R;" +
                                       "您就成为代表我们的『商人』了。$R;", "商人总管");

                PlaySound(pc, 4012, false, 100, 50);
                ChangePlayerJob(pc, PC_JOB.MERCHANT);

                Say(pc, 0, 0, "您已经转职为『商人』了!$R;", " ");

                Say(pc, 11000026, 131, "穿好衣服再和我说话吧。$R;" +
                                       "$R我要给您礼物，$R;" +
                                       "您先整理您的行李吧?$R;", "商人总管");
            }
            else
            {
                Say(pc, 11000026, 131, "纹章是烙印在皮肤上的，$R;" +
                                       "先把装备脱掉吧。$R;", "商人总管");
            }
        }

        void 商人轉職完成(ActorPC pc)
        {
            BitMask<JobBasic_12> JobBasic_12_mask = new BitMask<JobBasic_12>(pc.CMask["JobBasic_12"]);

            if (pc.Inventory.Equipments.Count != 0)
            {
                JobBasic_12_mask.SetValue(JobBasic_12.已經轉職為商人, true);
                Say(pc, 11000026, 131, "这是礼物，收下吧。$R;", "商人总管");

                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, 50071300, 1);
                Say(pc, 0, 0, "得到『大手提袋』!$R;", " ");

                LearnSkill(pc, 702);
                Say(pc, 0, 0, "学到『背包整理』!R;", " ");
            }
            else
            {
                Say(pc, 11000026, 131, "穿好衣服再和我说话吧。$R;", "商人总管");
            }
        }

        void 進階職業對話(ActorPC pc)
        {
            BitMask<Job2X_12> Job2X_12_mask = pc.CMask["Job2X_12"];

            if (pc.Job == PC_JOB.GAMBLER && !Job2X_12_mask.Test(Job2X_12.第一次對話))//_7a22)
            {
                Job2X_12_mask.SetValue(Job2X_12.第一次對話, true);
                //_7a22 = true;
                Say(pc, 131, "啊，您竟然成为「赌徒」了。$R;" +
                    "$P已经变成赌徒了就没办法了。$R;" +
                    "$R一定要诚实的生活啊$R;");
                return;
            }
            /*
            if (!_7a20 && _7a19)
            {
                _7a20 = true;
                Say(pc, 131, "想要变更职业？$R;" +
                    "$P哈哈$R;" +
                    "$R您好像弄错了。$R;" +
                    "$R贸易家没有隐藏的力量$R;" +
                    "也不能转职。$R;" +
                    "$P不要想太多，专心经商吧。$R;");
                return;
            }
            */
            switch (Select(pc, "要做什么？", "", "任务服务台", "购买法伊斯特入国许可证", "什么也不做。"))
            {
                case 1:
                    Say(pc, 0, 0, "目前尚未实装$R;", " ");
                    break;
                case 2:
                    OpenShopBuy(pc, 186);
                    Say(pc, 131, "法伊斯特有分会，总部在摩根呢$R;" +
                        "$R经过一定要进去看看喔！$R;");
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
                    "$P以您现在的实力，还不行。$R;");
                return;
            }

            if (pc.Job == PC_JOB.TRADER)
            {
                Say(pc, 131, "您已经是贸易家了。?$R;");
                return;
            }

            Say(pc, 131, "…$R;" +
                "$P呵呵，$R您好像蛮有实力的啊$R;");

            if (!Job2X_12_mask.Test(Job2X_12.聽取貿易商說明))//_3A62)
            {
                Say(pc, 131, "您对『贸易家』了解多少呢？$R;" +
                    "要给您解释一下『贸易家』吗？$R;");
                switch (Select(pc, "要听説明吗？", "", "听", "不听"))
                {
                    case 1:
                        Say(pc, 131, "『贸易家』是商人的上级职位$R;" +
                            "$P从很久的古代开始就叫『贸易家』$R;" +
                            "$P现在还有「传达者」或者$R「交换者」的意义。$R;");
                        Say(pc, 131, "『贸易家』的能力$R主要在冒险时候特别有用$R;" +
                            "$P就是雇佣力量大的佣兵来下命令$R;" +
                            "$R如果配合其他技能一起使用的话，$R您将会是在队五裡不可或缺的一个呢。$R;" +
                            "$P这是真的啊$R;");
                        Job2X_12_mask.SetValue(Job2X_12.聽取貿易商說明, true);
                        //_3A62 = true;
                        break;
                    case 2:
                        break;
                }
            }

            Say(pc, 131, "『贸易商』是$R在商人裡面挑选出来的商人。$R;" +
                "$P要成为贸易商的话$R要经歷很痛苦的过程$R;" +
                "$R要挑战吗？$R;");

            switch (Select(pc, "要挑战吗？", "", "挑战", "不挑战"))
            {
                case 1:
                    Job2X_12_mask.SetValue(Job2X_12.轉職開始, true);
                    Job2X_12_mask.SetValue(Job2X_12.搜集紋章紙, true);
                    //_3A63 = true;
                    //_3A69 = true;
                    Say(pc, 131, "…好$R;" +
                        "$R『贸易家』必要的能力$R是搜集客人需要的商品，$R并准确转达的能力。$R;" +
                        "$P给您的任务就是搜集十张纹章纸，$R交给不知道在哪里的贸易家古鲁杜。$R;" +
                        "$P贸易家古鲁杜是行会交易额最高$R的贸易家$R;" +
                        "$R而且喜欢亲自送到客人那里$R的特别商人。$R;" +
                        "$P他是非常忙的人，$R所以不知道他现在在哪里。$R;" +
                        "$R您亲自去找找吧。$R;" +
                        "$R但是…$R;" +
                        "$P要搜集完10张以后，一起交给他。$R;" +
                        "$R商品数量准确是得到客人$R信用的重要要素。$R;" +
                        "$P那么您就全力以赴吧。$R;");
                    break;
                case 2:
                    Say(pc, 131, "是吗$R;" +
                        "$R那就好好想想吧。$R;");
                    break;
            }
        }

        void 進階轉職選擇(ActorPC pc)
        {
            BitMask<Job2X_12> Job2X_12_mask = pc.CMask["Job2X_12"];

            switch (Select(pc, "真的要转职吗?", "", "我想成为贸易商", "还是算了吧"))
            {
                case 1:
                    Say(pc, 131, "那么就给您烙印上这象征贸易家的$R;" +
                        "『贸易家纹章』吧$R;");
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
                            "$P恭喜您成为贸易家$R;" +
                            "$R以后您就是『贸易家』了。$R;");
                        PlaySound(pc, 4012, false, 100, 50);
                        Say(pc, 131, "您已转职成为『贸易家』了$R;");
                        return;
                    }
                    Job2X_12_mask.SetValue(Job2X_12.防禦過高, true);
                    //_3A66 = true;
                    Say(pc, 131, "防御太高的话，就无法烙印纹章了$R;" +
                        "请换上轻便的服装后，再来吧。$R;");
                    break;
                case 2:
                    Say(pc, 131, "什么？不转职吗？$R;" +
                        "$R那一直所付出的努力就白费了，$R没关系吗？$R;");
                    switch (Select(pc, "真的要转职吗?", "", "不转职", "转职"))
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

                            Say(pc, 131, "知道了，$R不会勉强您的$R;");
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

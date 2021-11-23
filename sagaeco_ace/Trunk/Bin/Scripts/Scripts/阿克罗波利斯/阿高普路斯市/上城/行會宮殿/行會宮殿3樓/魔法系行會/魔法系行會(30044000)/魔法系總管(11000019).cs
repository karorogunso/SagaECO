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
                Say(pc, 131, "那么就给您烙印上这象征元素使的$R;" +
                    "『元素使纹章』吧$R;");
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
                        "您身上已经烙印了漂亮的纹章。$R;" +
                        "$R从今以后，$R您就成为代表我们的『元素使』了。$R;");
                    PlaySound(pc, 4012, false, 100, 50);
                    Say(pc, 131, "您已经转职为『元素使』了。$R;");
                    Say(pc, 131, "以后每天都要真诚待人哦$R;");
                    return;
                }
                Say(pc, 131, "防御太高的话，就无法烙印纹章了$R;" +
                    "请换上轻便的服装后，再来吧。$R;");
                return;
            }

            Say(pc, 11000019, 131, "欢迎光临!!$R;" +
                                   "$R这里就是魔法系行会。$R;", "魔法系总管");

            if (Job2X_06_mask.Test(Job2X_06.所有問題回答正確))//_3A99)
            {
                switch (Select(pc, "要转职成为『元素使』吗？", "", "成为元素使", "算了吧"))
                {
                    case 1:
                        Say(pc, 131, "那么就给您烙印上这象征元素使的$R;" +
                            "『元素使纹章』吧$R;");
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
                                "您身上已经烙印了漂亮的纹章。$R;" +
                                "$R从今以后，$R您就成为代表我们的『元素使』了。$R;");
                            PlaySound(pc, 4012, false, 100, 50);
                            Say(pc, 131, "您已转职为『元素使』了。$R;");
                            Say(pc, 131, "以后每天都要真诚待人哦$R;");
                            return;
                        }
                        Job2X_06_mask.SetValue(Job2X_06.防禦過高, true);
                        //_4A13 = true;
                        Say(pc, 131, "防御太高的话，就无法烙印纹章了$R;" +
                            "请换上轻便的服装后，再来吧。$R;");
                        break;
                    case 2:
                        Say(pc, 131, "是吗？$R;" +
                            "要是想法改变的话，再来找我吧。$R;");
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

            if (pc.Job == PC_JOB.NOVICE)
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
                    Say(pc, 131, "穿这样的服装的话，会感冒的哦$R;");
                    return;
                }

                if (Job2X_05_mask.Test(Job2X_05.轉職開始))//_3A89)
                {
                    Say(pc, 131, "您走吧！努力喔！$R;");
                    return;
                }

                Say(pc, 11000019, 131, pc.Name + "…$R;" +
                                       "$R什么事啊??$R;", "魔法系总管");

                switch (Select(pc, "做什么呢?", "", "任务服务台", "购买诺森入国许可证", "我想转职", "什么都不做"))
                {
                    case 1:
                        HandleQuest(pc, 43);
                        break;
                    case 2:
                        Say(pc, 131, "要去诺森吗?$R;");
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

            Say(pc, 11000019, 131, "管理魔法系行会的总管就是我。$R;" +
                                   "$P哈哈，您是初心者吗?$R;" +
                                   "$R呵呵…$R;" +
                                   "$P对『魔法师』和『精灵使』有兴趣的话，$R;" +
                                   "就听听我的讲解吧。$R;", "魔法系总管");

            selection = Select(pc, "想做什么?", "", "我想成为『魔法师』!", "『魔法师』是什么样的职业?", "我想成为『精灵使』!", "『精灵使』是什么样的职业?!", "任务服务台", "什么也不做");

            while (selection != 6)
            {
                switch (selection)
                {
                    case 1:
                        JobBasic_05_mask.SetValue(JobBasic_05.選擇轉職為魔法師, true);

                        Say(pc, 11000019, 131, "哈哈，您想成为『魔法师』吗?$R;" +
                                               "$R要成为魔法师，$R;" +
                                               "就先要理解「新生魔法」喔!$R;" +
                                               "$P本来我能教您的话是最好的，$R;" +
                                               "不过我身为总管实在是太忙了。$R;" +
                                               "$P在「阿克罗波利斯」的「下城」$R;" +
                                               "有个对「新生魔法」很熟悉的傢伙。$R;" +
                                               "$R当然他没有我懂得多啦。$R;" +
                                               "他的名字叫「麦吉克」。$R;" +
                                               "$R那小子虽然有点轻率，$R;" +
                                               "但是也没有别的人选了。$R;" +
                                               "$R没有办法啊~~!$R;" +
                                               "去那小子那里学习「新生魔法」吧。$R;", "魔法系总管");
                        return;

                    case 2:
                        Say(pc, 11000019, 131, "魔法师这职业比较适合$R;" +
                                               "泰达尼亚和多米尼翁的体质哦!$R;" +
                                               "$R埃米尔选择这个职业的话，$R;" +
                                               "会很辛苦的。$R;" +
                                               "$P即使这样还想听下去吗?$R;", "魔法系总管");

                        switch (Select(pc, "还要听下去吗?", "", "我要听", "不听"))
                        {
                            case 1:
                                Say(pc, 11000019, 131, "『魔法师』这个职业，耗尽一生，$R;" +
                                                       "研究和实验「新生魔法」。$R;" +
                                                       "$P「新生魔法」並不是火或水等$R;" +
                                                       "属性的力量，$R;" +
                                                       "而是利用次元力量的一种新魔法。$R;" +
                                                       "$P但是没有什么副作用，可以放心。$R;" +
                                                       "哈哈…$R;" +
                                                       "$P因为是新魔法，还不知道威力有多强呢!$R;" +
                                                       "研究出来的话，$R;" +
                                                       "说不定还有用来打破$R;" +
                                                       "次元障壁的魔法呢!$R;" +
                                                       "$P开始的时候，$R;" +
                                                       "简单的攻击魔法和防御魔法，$R;" +
                                                       "并可以使用提高防御力的辅助魔法呢!$R;", "魔法系总管");
                                break;

                            case 2:
                                break;
                        }
                        break;

                    case 3:
                        JobBasic_06_mask.SetValue(JobBasic_06.選擇轉職為元素使, true);

                        Say(pc, 11000019, 131, "哈哈，您想成为『精灵使』吗?$R;" +
                                               "$R要成为精灵使，$R;" +
                                               "就先要理解「属性魔法」。$R;" +
                                               "$P本来我叫您的话是最好的。$R;" +
                                               "不过我身为总管实在是太忙了。$R;" +
                                               "$P在「阿克罗波利斯」的「下城」$R;" +
                                               "有个对「属性魔法」很熟悉的人。$R;" +
                                               "$R她比我厉害得多呢!$R;" +
                                               "$P她的名字叫「雪莉」。$R;" +
                                               "$R是个美丽又神秘的女生，$R;" +
                                               "要是我年轻个十岁，早就去追了。$R;" +
                                               "$P去她那里学习「属性魔法」吧。$R;" +
                                               "$P…一定要帮我向他问好喔。$R;", "魔法系总管");
                        return;

                    case 4:
                        Say(pc, 11000019, 131, "『精灵使』这职业比较适合$R;" +
                                               "泰达尼亚和多米尼翁的体质哦!$R;" +
                                               "$R埃米尔选择这个职业的话$R;" +
                                               "会很辛苦的。$R;" +
                                               "$P即使这样还想听下去吗?$R;", "魔法系总管");

                        switch (Select(pc, "还要听下去吗?", "", "我要听", "不听"))
                        {
                            case 1:
                                Say(pc, 11000019, 131, "『精灵使』这种职业，耗尽一生，$R;" +
                                                       "研究「属性魔法」。$R;" +
                                                       "$P「属性魔法」是利用$R;" +
                                                       "火、水、风、地之精灵的力量。$R;" +
                                                       "$R是从古代流传下来的魔法。$R;" +
                                                       "$P除此之外，虽然也有光和暗属性魔法，$R;" +
                                                       "但有点不同呢。$R;" +
                                                       "$P要使用「光属性」魔法，$R;" +
                                                       "首先要用光净化自己的身体。$R;" +
                                                       "$R要使用「暗属性」魔法，$R;" +
                                                       "要用黑暗浸染自己的灵魂。$R;" +
                                                       "$P若想知道更多，$R;" +
                                                       "可以去「白之圣堂」和「黑之圣堂」$R;" +
                                                       "打听一下情报吧。$R;" +
                                                       "$P说到精灵使呢，$R;" +
                                                       "是可以操纵各种属性的能力，$R;" +
                                                       "还会各种攻击魔法。$R;" +
                                                       "$P队伍里若有精灵使护航的话，$R;" +
                                                       "将会发挥十分强大的威力。$R;" +
                                                       "$P但若还不明白属性的力量的话，$R;" +
                                                       "就没什么意义了。$R;" +
                                                       "$R也可以说是优秀的职业哦!$R;", "魔法系总管");
                                break;

                            case 2:
                                break;
                        }
                        break;

                    case 5:
                        break;
                }

                selection = Select(pc, "想做什么?", "", "我想成为『魔法师』!", "『魔法师』是什么样的职业?", "我想成为『精灵使』!", "『精灵使』是什么样的职业?!", "任务服务台", "什么也不做");
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
                Say(pc, 11000019, 131, "再跟你说一遍吧。$R;" +
                                       "$R到「阿克罗波利斯」的「下城」$R;" +
                                       "去找一个叫「麦吉克」的人，$R;" +
                                       "学习「新生魔法」的知识吧!$R;", "魔法系总管");
            }
        }

        void 新生魔法相關問題01(ActorPC pc)
        {
            Say(pc, 11000019, 131, "好像学成归来了?$R;" +
                                   "$P嗯……$R;" +
                                   "那我就考验一下你啰?$R;" +
                                   "$P提升防御力的魔法叫什么名字?$R;", "魔法系总管");

            switch (Select(pc, "提升防御力的魔法是?", "", "帝凡斯盾", "原力护盾", "松木盾"))
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

            Say(pc, 11000019, 131, "哈哈，不错嘛。$R;" +
                                   "$R那么下一个问题。$R;" +
                                   "$P以下三个中哪个是攻击魔法?$R;", "魔法系总管");

            switch (Select(pc, "在这里面属于攻击魔法的是?", "", "抑制破坏", "魔法箭", "魔法师的残像"))
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
                                   "那么现在，问您最后一个问题。$R;" +
                                   "$P『魔法师的残像』的效果是什么?$R;", "魔法系总管");

            switch (Select(pc, "『魔法师的残像』的效果是…?", "", "提高斧的攻击力", "制作分身", "降低对方命中率"))
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
                                   "像您这么聪明的人，$R;" +
                                   "应该可以做魔法师了。$R;" +
                                   "$R真的要做『魔法师』吗?$R;", "魔法系总管");

            switch (Select(pc, "要转职为『魔法师』吗?", "", "转职为『魔法师』", "不了，我想成为『精灵使』", "还是算了吧"))
            {
                case 1:
                    JobBasic_05_mask.SetValue(JobBasic_05.魔法師轉職任務完成, true);
                    break;

                case 2:
                    JobBasic_05_mask.SetValue(JobBasic_05.選擇轉職為魔法師, false);
                    JobBasic_05_mask.SetValue(JobBasic_05.已經從魔術那裡聽取有關新生魔法的知識, false);
                    JobBasic_06_mask.SetValue(JobBasic_06.選擇轉職為元素使, true);

                    Say(pc, 11000019, 131, "$R您是想做『精灵使』啊?$R;", "魔法系总管");

                    Say(pc, 11000019, 131, "哈哈，您想成为『精灵使』吗?$R;" +
                                           "$R要成为精灵使，$R;" +
                                           "就先要理解「属性魔法」。$R;" +
                                           "$P本来我叫您的话是最好的。$R;" +
                                           "不过我身为总管实在是太忙了。$R;" +
                                           "$P在「阿克罗波利斯」的「下城」$R;" +
                                           "有个对「属性魔法」很熟悉的人。$R;" +
                                           "$R她比我厉害得多呢!$R;" +
                                           "$P她的名字叫「夏莉」。$R;" +
                                           "$R是个美丽又神秘的女生，$R;" +
                                           "要是我年轻个十岁，早就去追了。$R;" +
                                           "$P去她那里学习「属性魔法」吧。$R;" +
                                           "$P…一定要帮我向他问好喔。$R;", "魔法系总管");
                    break;

                case 3:
                    Say(pc, 11000019, 131, "什么? 不做了?$R;", "魔法系总管");
                    break;
            }
        }

        void 新生魔法相關問題回答錯誤(ActorPC pc)
        {
            BitMask<JobBasic_05> JobBasic_05_mask = new BitMask<JobBasic_05>(pc.CMask["JobBasic_05"]);

            JobBasic_05_mask.SetValue(JobBasic_05.已經從魔術那裡聽取有關新生魔法的知識, false);

            PlaySound(pc, 2041, false, 100, 50);

            Say(pc, 11000019, 131, "…$R;" +
                                   "$P您好像还没有理解透彻呢?$R;" +
                                   "再去听一遍吧!$R;", "魔法系总管");
        }

        void 申請轉職為魔法師(ActorPC pc)
        {
            BitMask<JobBasic_05> JobBasic_05_mask = new BitMask<JobBasic_05>(pc.CMask["JobBasic_05"]);

            Say(pc, 11000019, 131, "那么就给您纹上这象征『魔法师』的，$R;" +
                                   "『魔法师纹章』吧!$R;", "魔法系总管");

            if (pc.Inventory.Equipments.Count == 0)
            {
                JobBasic_05_mask.SetValue(JobBasic_05.魔法師轉職成功, true);

                PlaySound(pc, 3087, false, 100, 50);
                ShowEffect(pc, 4131);
                Wait(pc, 3960);

                Say(pc, 11000019, 131, "…$R;" +
                                       "$P好棒啊，$R;" +
                                       "您身上已经烙印了漂亮的纹章。$R;" +
                                       "$R从今以后，$R;" +
                                       "您就成为代表我们的『魔法师』了。$R;", "魔法系总管");

                PlaySound(pc, 4012, false, 100, 50);
                ChangePlayerJob(pc, PC_JOB.WIZARD);
                Say(pc, 0, 0, "您已经转职为『魔法师』了!$R;", " ");

                Say(pc, 11000019, 131, "先穿上衣服后，再和我说话吧。$R;" +
                                       "有一份小礼物，要送给您哦$!R;" +
                                       "$R您先去整理行李后，再来找我吧。$R;", "魔法系总管");
            }
            else
            {
                Say(pc, 11000019, 131, "纹章是烙印在皮肤上的，$R;" +
                                       "先把装备脱掉吧。$R;", "魔法系总管");
            }
        }

        void 魔法師轉職完成(ActorPC pc)
        {
            BitMask<JobBasic_05> JobBasic_05_mask = new BitMask<JobBasic_05>(pc.CMask["JobBasic_05"]);

            if (pc.Inventory.Equipments.Count != 0)
            {
                JobBasic_05_mask.SetValue(JobBasic_05.已經轉職為魔法師, true);

                Say(pc, 11000019, 131, "给您『蛋白石垂饰』，$R;" +
                                       "$R您要继续努力哦。$R;", "魔法系总管");

                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, 50050800, 1);
                Say(pc, 0, 0, "得到『蛋白石垂饰』!$R;", " ");

                LearnSkill(pc, 3001);
                Say(pc, 0, 0, "学到『魔法箭』!R;", " ");
            }
            else
            {
                Say(pc, 11000019, 131, "先穿上衣服后，再和我说话吧。$R;", "魔法系总管");
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
                Say(pc, 11000019, 131, "再跟您说一遍吧。$R;" +
                                       "$R到「阿克罗波利斯」的「下城」$R;" +
                                       "去找一个叫「雪莉」的女生，$R;" +
                                       "学习「属性魔法」的知识吧!$R;", "魔法系总管");
            }
        }

        void 屬性魔法相關問題01(ActorPC pc)
        {
            Say(pc, 11000019, 131, "回来了。$R;" +
                                   "$P嗯…$R;" +
                                   "那我要考验一下您的实力啰!$R;" +
                                   "$R哪个属性比「火属性」强?$R;", "魔法系总管");

            switch (Select(pc, "比「火属性」强的是?", "", "火属性", "水属性", "地属性", "风属性"))
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

            Say(pc, 11000019, 131, "呵呵，挺不错啊。$R;" +
                                   "$R那么下一个问题。$R;" +
                                   "$P那个属性比「水属性」弱?$R;", "魔法系总管");

            switch (Select(pc, "比「水属性」弱的是?", "", "火属性", "水属性", "地属性", "风属性"))
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
                                   "$R那么这是最后的问题了。$R;" +
                                   "$P哪个属性跟「风属性」没有关系呢?$R;", "魔法系总管");

            switch (Select(pc, "跟「风属性」没有关系的属性是?", "", "火属性", "水属性", "地属性", "风属性"))
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

            Say(pc, 11000019, 131, "没错!$R;" +
                                   "$R理解属性对『精灵使』来说，$R;" +
                                   "是非常重要的。$R;" +
                                   "千万不要忘记喔!$R;" +
                                   "$P那您是不是要做『精灵使』呢?$R;", "魔法系总管");

            switch (Select(pc, "要转职为『精灵使』吗?", "", "转职为『精灵使』", "不了，我想成为『魔法师』", "还是算了吧"))
            {
                case 1:
                    JobBasic_06_mask.SetValue(JobBasic_06.元素使轉職任務完成, true);
                    break;

                case 2:
                    JobBasic_05_mask.SetValue(JobBasic_05.選擇轉職為魔法師, true);
                    JobBasic_06_mask.SetValue(JobBasic_06.選擇轉職為元素使, false);
                    JobBasic_06_mask.SetValue(JobBasic_06.已經從雪莉那裡聽取有關屬性魔法的知識, false);

                    Say(pc, 11000019, 131, "$R您是想做『魔法师』啊?$R;", "魔法系总管");

                    Say(pc, 11000019, 131, "哈哈，您想成为『魔法师』吗?$R;" +
                                           "$R要成为魔法师，$R;" +
                                           "就先要理解「新生魔法」喔!$R;" +
                                           "$P本来我能教您的话是最好的，$R;" +
                                           "不过我身为总管实在是太忙了。$R;" +
                                           "$P在「阿克罗波利斯」的「下城」$R;" +
                                           "有个对「新生魔法」很熟悉的傢伙。$R;" +
                                           "$R当然他没有我懂得多啦。$R;" +
                                           "他的名字叫「麦吉克」。$R;" +
                                           "$R那小子虽然有点轻率，$R;" +
                                           "但是也没有别的人选了。$R;" +
                                           "$R没有办法啊~~!$R;" +
                                           "$P去那小子那里学习「新生魔法」吧。$R;", "魔法系总管");
                    break;

                case 3:
                    Say(pc, 11000019, 131, "什么? 不做了?$R;", "魔法系总管");
                    break;
            }
        }

        void 屬性魔法相關問題回答錯誤(ActorPC pc)
        {
            BitMask<JobBasic_06> JobBasic_06_mask = new BitMask<JobBasic_06>(pc.CMask["JobBasic_06"]);

            JobBasic_06_mask.SetValue(JobBasic_06.已經從雪莉那裡聽取有關屬性魔法的知識, false);

            PlaySound(pc, 2041, false, 100, 50);

            Say(pc, 11000019, 131, "…$R;" +
                                   "$P您好像还没有理解透彻呢?$R;" +
                                   "再去听一遍吧!$R;", "魔法系总管");
        }

        void 申請轉職為元素使(ActorPC pc)
        {
            BitMask<JobBasic_06> JobBasic_06_mask = new BitMask<JobBasic_06>(pc.CMask["JobBasic_06"]);

            Say(pc, 11000019, 131, "那么就给您纹上这象征『精灵使』的，$R;" +
                                   "『精灵使纹章』吧!$R;", "魔法系总管");

            if (pc.Inventory.Equipments.Count == 0)
            {
                JobBasic_06_mask.SetValue(JobBasic_06.元素使轉職成功, true);

                PlaySound(pc, 3087, false, 100, 50);
                ShowEffect(pc, 4131);
                Wait(pc, 3960);

                Say(pc, 11000019, 131, "…$R;" +
                                       "$P好棒啊，$R;" +
                                       "您身上已经烙印了漂亮的纹章。$R;" +
                                       "$R从今以后，$R;" +
                                       "您就成为代表我们的『精灵使』了。$R;", "魔法系总管");

                PlaySound(pc, 4012, false, 100, 50);
                ChangePlayerJob(pc, PC_JOB.SHAMAN);
                Say(pc, 0, 0, "您已经转职为『精灵使』了!$R;", " ");


                Say(pc, 11000019, 131, "先穿上衣服后，再和我说话吧。$R;" +
                                       "有一份小礼物，要送给您哦!$R;" +
                                       "$R您先去整理行李后，再来找我吧。$R;", "魔法系总管");
            }
            else
            {
                Say(pc, 11000019, 131, "纹章是烙印在皮肤上的，$R;" +
                                       "先把装备脱掉吧。$R;", "魔法系总管");
            }
        }

        void 元素使轉職完成(ActorPC pc)
        {
            BitMask<JobBasic_06> JobBasic_06_mask = new BitMask<JobBasic_06>(pc.CMask["JobBasic_06"]);

            if (pc.Inventory.Equipments.Count != 0)
            {
                JobBasic_06_mask.SetValue(JobBasic_06.已經轉職為元素使, true);

                Say(pc, 11000019, 131, "给您『精灵勾玉』，$R;" +
                                       "$R您就用心练吧。$R;", "魔法系总管");

                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, 50052400, 1);
                Say(pc, 0, 0, "得到『精灵勾玉』!$R;", " ");

                switch (Select(pc, "要学习什么样的魔法呢?", "", "火球术", "冰箭术", "大地震荡", "落雷术"))
                {
                    case 1:
                        LearnSkill(pc, 3006);

                        Say(pc, 0, 0, "学到『火球术』!R;", " ");
                        break;
                    case 2:
                        LearnSkill(pc, 3029);

                        Say(pc, 0, 0, "学到『冰箭术』!R;", " ");
                        break;
                    case 3:
                        LearnSkill(pc, 3041);

                        Say(pc, 0, 0, "学到『大地震荡』!R;", " ");
                        break;
                    case 4:
                        LearnSkill(pc, 3017);

                        Say(pc, 0, 0, "学到『落雷术』!R;", " ");
                        break;
                }
            }
            else
            {
                Say(pc, 11000019, 131, "先穿上衣服后，再和我说话吧。$R;", "魔法系总管");
            }
        }

        void 進階轉職(ActorPC pc)
        {
            BitMask<Job2X_05> Job2X_05_mask = pc.CMask["Job2X_05"];
            BitMask<Job2X_06> Job2X_06_mask = pc.CMask["Job2X_06"];

            if (Job2X_06_mask.Test(Job2X_06.轉職完成))//_3A92 && _3A94)
            {
                Say(pc, 131, "不能再转职了$R;");
                return;
            }

            if (pc.Job == PC_JOB.SHAMAN && pc.JobLevel1 >= 30)
            {
                Say(pc, 131, "呵呵…还要挑战更高级别吗？$R;" +
                    "$R嗯…$R;" +
                    "$P对元素使是有兴趣的话$R;" +
                    "再听听说明吧$R;");
                int a = 0;
                while (a == 0)
                {
                    switch (Select(pc, "怎么办？？", "", "我要做『元素使』喔！", "『元素使』是怎样的职业？", "任务服务台", "什么都不做"))
                    {
                        case 1:
                            if (pc.Mag > 29)
                            {
                                Job2X_06_mask.SetValue(Job2X_06.進階轉職開始, true);
                                //_3A93 = true;
                                //_3A89 = false;
                                Say(pc, 131, "呵呵…$R;" +
                                    "您想成为元素使吧？$R;" +
                                    "$R那样的话$R;" +
                                    "到火焰、水灵、神风、大地四个精灵$R;" +
                                    "居住的地方走一趟$R;" +
                                    "$P得到他们的守护在回来吧$R;" +
                                    "$P这是漫长的旅程$R;" +
                                    "万事要小心哦！$R;" +
                                    "$P…小心不要失手哦$R;");
                                return;
                            }
                            Say(pc, 131, "想要成为元素使，魔力是很重要的。$R;" +
                                "$R先去提升『魔力』后，再来吧。$R;" +
                                "其他的以后再说啦。$R;");
                            return;
                        case 2:
                            Say(pc, 131, "跟精灵使一样，元素使这职业比较适合$R;" +
                                "泰达尼亚和多米尼翁的体质哦$R;" +
                                "$R埃米尔选择这个职业的话$R会很辛苦的。$R;" +
                                "$P就算这样还想听下去吗？$R;");
                            switch (Select(pc, "听下去吗?", "", "我要听", "不听"))
                            {
                                case 1:
                                    Say(pc, 131, "元素使是精灵使的上位职业$R;" +
                                        "$P是能够得到火焰、水灵、神风、$R;" +
                                        "大地四种精灵，拥有构成世界的四种$R;" +
                                        "力量的职业，所以叫元素使$R;" +
                                        "$P跟精灵使一样，$R;" +
                                        "元素使能够操纵各属性的攻击魔法，$R;" +
                                        "还能控制自己的属性能力。$R;" +
                                        "$P队伍里，如果有元素使的话$R;" +
                                        "一定会发挥强大的力量。$R;" +
                                        "$P但如果没能透彻理解属性$R;" +
                                        "力量的话，$R;" +
                                        "$R就不能充分发挥其威力了哦$R;");
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
                Say(pc, 131, "哈哈，还要挑战更高的级别吗？$R;" +
                    "呵呵，$R如果对魔导师有兴趣的话$R;" +
                    "$P听听我的话$R;" +
                    "您再走也不迟。$R;");
                int a = 0;
                while (a == 0)
                {
                    switch (Select(pc, "做什么呢?", "", "我要做『魔导师』喔！", "『魔导师』是怎样的职业？", "任务服务台", "什么也不做"))
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
                                    "想成为魔导师吧？$R;" +
                                    "如果是这样$R;" +
                                    "必须得到大导师的认可$R;" +
                                    "$P『诺森』中央有魔法行会总部$R;" +
                                    "去那里见大导师吧$R;" +
                                    "$P他会告诉您消息的$R;" +
                                    "$P我可以教您的只有这些…$R;" +
                                    "祝您好运$R;");
                                return;
                            }
                            Say(pc, 131, "想要成为魔导师，魔力是很重要的。$R;" +
                                "$R先去提升『魔力』后，再来吧。$R;" +
                                "其他的以后再说啦。$R;");
                            return;
                        case 2:
                            Say(pc, 131, "魔导师这个职业比较适合$R;" +
                                "泰达尼亚和多米尼翁的体质哦$R;" +
                                "$R埃米尔选择这个职业的话$R会很辛苦的。$R;" +
                                "$P就算这样还想听下去吗？$R;");
                            switch (Select(pc, "听下去吗?", "", "我要听", "不听"))
                            {
                                case 1:
                                    Say(pc, 131, "魔导师是魔法师的上位职业$R;" +
                                        "$P魔力要比魔法师高得多$R;" +
                                        "$P真相永远留着那种魔力$R;" +
                                        "…可以的话多好呢…$R;" +
                                        "呵呵呵…$R;" +
                                        "$P如果新生魔法继续研究发展$R;" +
                                        "$R突破次元障壁的魔法$R也不算是异想天开啊…$R;" +
                                        "$P魔导师可以使用至今流传的辅助魔法$R;" +
                                        "可以说是魔法界的专家哦！$R;");
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
            Say(pc, 131, "您尚未达到申请转职的条件。$R;");
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
                    "那么问您几条最核心的问题$R;" +
                    "$R可不能马虎了事…$R;" +
                    "东国哪种属性最强?$R;");
                switch (Select(pc, "在东国哪种属性最强??", "", "火焰", "水灵", "大地", "神风"))
                {
                    case 1:
                        break;
                    case 2:
                        PlaySound(pc, 2040, false, 100, 50);
                        Say(pc, 131, "呵呵…不错，$R那么下一个问题是…在斯诺普雪原$R哪一种属性最强?$R;");
                        switch (Select(pc, "在斯诺普雪原哪一种属性最强?", "", "火焰", "水灵", "大地", "神风"))
                        {
                            case 1:
                                PlaySound(pc, 2040, false, 100, 50);
                                Say(pc, 131, "好，最后一个问题$R在南国哪一种属性最强？$R;");
                                switch (Select(pc, "在南国哪一种属性最强？", "", "火焰", "水灵", "大地", "神风"))
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
                                        Say(pc, 131, "嗯…對元素使来说$R;" +
                                            "了解属性是最重要的$R;" +
                                            "不可以忘記喔！$R;" +
                                            "那么真的要成为『元素使』吗？$R;");

                                        switch (Select(pc, "要转职成为『元素使』吗？", "", "成为元素使", "算了吧"))
                                        {
                                            case 1:
                                                Say(pc, 131, "那么就给您烙印上这象征元素使的$R;" +
                                                    "『元素使纹章』吧$R;");
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
                                                        "您身上已经烙印了漂亮的纹章。$R;" +
                                                        "$R从今以后，$R您就成为代表我们的『元素使』了。$R;");
                                                    PlaySound(pc, 4012, false, 100, 50);
                                                    Say(pc, 131, "您已转职为『元素使』了。$R;");
                                                    Say(pc, 131, "以后每天都要真诚待人哦$R;");
                                                    return;
                                                }
                                                Job2X_06_mask.SetValue(Job2X_06.防禦過高, true);
                                                //_4A13 = true;
                                                Say(pc, 131, "防御太高的话，就无法烙印纹章了$R;" +
                                                    "请换上轻便的服装后，再来吧。$R;");
                                                return;
                                            case 2:
                                                Say(pc, 131, "是吗？$R;" +
                                                    "要是有想法的话，再来找我吧。$R;");
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
                    "$P您看起来还是不太明白，$R;" +
                    "还是再去一趟吧？$R;");
                return;
            }
            Say(pc, 131, "什么?那么快就回来了？$R;" +
                "$R不可马虎了事的…$R;");
        }
    }
}

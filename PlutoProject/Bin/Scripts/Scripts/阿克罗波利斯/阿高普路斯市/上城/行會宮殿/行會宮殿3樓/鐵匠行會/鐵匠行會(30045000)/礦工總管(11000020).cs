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

            Say(pc, 11000020, 131, "喂，这里是矿工行会。$R;", "矿工总管");

            if (JobBasic_09_mask.Test(JobBasic_09.礦工轉職成功) &&
                !JobBasic_09_mask.Test(JobBasic_09.已經轉職為礦工))
            {
                礦工轉職完成(pc);
                return;
            }

            if (pc.Job == PC_JOB.NOVICE)
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
                                       "$R什么事？$R;", "矿工总管");

                switch (Select(pc, "做什么好呢?", "", "任务服务台", "我想转职", "购买法伊斯特入国许可证", "什么也不做"))
                {
                    case 1:
                        HandleQuest(pc, 46);
                        break;
                    case 2:
                        進階轉職(pc);
                        break;
                    case 3:
                        OpenShopBuy(pc, 150);
                        Say(pc, 131, "在法伊斯特有行会特派员$R;" +
                            "$R见到他帮我问声好吧$R;");
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
                                Say(pc, 0, 0, "请注意$R;" +
                                   "目前BP系的技能大部分未实装$R;" +
                                   "不能保证除了火心外技能的使用性没有问题$R;" +
                                   "同样,相关任务也得不到保证$R;" +
                                   "请谨慎选择转职矿工?$R;", "谜之音");
            Say(pc, 11000020, 131, "我是管理矿工们的矿工总管$R;" +
                                   "$R咦? 您是初心者啊。$R;" +
                                   "$P您对矿工有兴趣吗?$R;" +
                                   "有兴趣听我介绍吗?$R;", "矿工总管");

            selection = Select(pc, "想做什么?", "", "我想成为『矿工』!", "『矿工』是什么样的职业?", "任务服务台", "什么也不做");

            while (selection != 4)
            {
                switch (selection)
                {
                    case 1:
                        Say(pc, 11000020, 131, "您说您想成为『矿工』?$R;" +
                                               "好主意!$R;" +
                                               "现在开始您就是矿工了!$R;" +
                                               "$P…不是，不是!$R;" +
                                               "如果这么简单的话就不好玩了…$R;" +
                                               "$R对吧…$R;" +
                                               "应该要给您一些试炼吧?$R;" +
                                               "$P对了，如果想成为矿工的话!$R;" +
                                               "$R就要搜集三个重要的道具。$R;" +
                                               "$P给您一个提示吧!$R;" +
                                               "是『铁X』$R;" +
                                               "$P如果您把那三哥『铁X』拿回來的话，$R;" +
                                               "就让您做矿工吧!$R;" +
                                               "$R怎么样?$R;", "矿工总管");

                        switch (Select(pc, "接受吗?", "", "没问题", "才不要"))
                        {
                            case 1:
                                JobBasic_09_mask.SetValue(JobBasic_09.選擇轉職為礦工, true);

                                Say(pc, 11000020, 131, "好，我会期待有好结果的。$R;", "矿工总管");
                                break;

                            case 2:
                                break;
                        }
                        return;

                    case 2:
                        Say(pc, 11000020, 131, "矿工是适合埃米尔的职业，$R;" +
                                               "$R您还想听我解说吗?$R;", "矿工总管");

                        switch (Select(pc, "还要听下去吗?", "", "我要听", "不听"))
                        {
                            case 1:
                                Say(pc, 11000020, 131, "『矿工』的专长就是矿物采集。$R;" +
                                                       "$R采矿要比别的职业强得多，$R;" +
                                                       "见到岩石，可不要放过!$R;" +
                                                       "$P将来可以成为修理和$R;" +
                                                       "加工武器的铁匠呢。$R;" +
                                                       "$P但矿工不擅长使用武器，$R;" +
                                                       "也不能装备重的防具，$R;" +
                                                       "所以战斗力比较弱。$R;" +
                                                       "$P如果组成队伍的话，$R;" +
                                                       "通常担任运输的角色$R;", "矿工总管");
                                break;

                            case 2:
                                break;
                        }
                        break;

                    case 3:
                        Say(pc, 11000020, 131, "想找工作吗?$R;" +
                                               "$R不好意思，$R;" +
                                               "如果不是『矿工』的话$R;" +
                                               "现在还不能给您介绍工作的!$R;" +
                                               "$P想不想成为『矿工』啊?$R;", "矿工总管");
                        break;
                }

                selection = Select(pc, "想做什么?", "", "我想成為『矿工』!", "『矿工』是什么样的职业?", "任务服务台", "什么也不做");
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
                                       "答案就是『铁块』!$R;" +
                                       "$R好，承认您是『矿工』了，$R;" +
                                       "$P真的要成为『矿工』吗?$R;", "矿工总管");

                switch (Select(pc, "要转职为『矿工』吗?", "", "转职为『矿工』", "还是算了吧"))
                {
                    case 1:
                        JobBasic_09_mask.SetValue(JobBasic_09.礦工轉職任務完成, true);

                        TakeItem(pc, 10015600, 3);
                        break;

                    case 2:
                        Say(pc, 11000020, 131, "考虑清楚再来吧。$R;", "矿工总管");
                        break;
                }
            }
            else
            {
                Say(pc, 11000020, 131, "给您一个提示吧!是『铁X』$R;" +
                "$P如果您把那三个『铁X』拿回来的话，$R;" +
                "就让您做矿工吧!$R;", "矿工总管");
            }
        }

        void 申請轉職為礦工(ActorPC pc)
        {
            BitMask<JobBasic_09> JobBasic_09_mask = new BitMask<JobBasic_09>(pc.CMask["JobBasic_09"]);

            Say(pc, 11000020, 131, "那么请您收下这代表『矿工』的$R;" +
                                   "『矿工纹章』吧。$R;", "矿工总管");

            if (pc.Inventory.Equipments.Count == 0)
            {
                JobBasic_09_mask.SetValue(JobBasic_09.礦工轉職成功, true);

                PlaySound(pc, 3087, false, 100, 50);
                ShowEffect(pc, 4131);
                Wait(pc, 3960);

                Say(pc, 11000020, 131, "…$R;" +
                                       "$P好棒啊，$R;" +
                                       "您身上已经烙印了漂亮的纹章。$R;" +
                                       "$R从今以后，$R;" +
                                       "您就成为『矿工』了。$R;", "矿工总管");

                PlaySound(pc, 4012, false, 100, 50);
                ChangePlayerJob(pc, PC_JOB.TATARABE);

                Say(pc, 0, 0, "您已经转职为『矿工』了!$R;", " ");

                Say(pc, 11000020, 131, "先穿上衣服后，再和我说话吧。$R;" +
                                       "有一份小礼物，要送给您哦!$R;" +
                                       "$R您先去整理行李后，再来找我吧。$R;", "矿工总管");
            }
            else
            {
                Say(pc, 11000020, 131, "纹章是烙印在皮肤上的，$R;" +
                                       "先把装备脱掉吧。$R;", "矿工总管");
            }
        }

        void 礦工轉職完成(ActorPC pc)
        {
            BitMask<JobBasic_09> JobBasic_09_mask = new BitMask<JobBasic_09>(pc.CMask["JobBasic_09"]);

            if (pc.Inventory.Equipments.Count != 0)
            {
                JobBasic_09_mask.SetValue(JobBasic_09.已經轉職為礦工, true);

                Say(pc, 11000020, 131, "给您『矿工印花大头巾』和『矿工炉』，$R;" +
                                       "$R用『矿工炉』来炼铁吧。$R;" +
                                       "会做出来好东西的。$R;", "矿工总管");

                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, 50021001, 1);
                GiveItem(pc, 10033100, 1);
                Say(pc, 0, 0, "得到『矿工印花大头巾』和『矿工炉』!$R;", " ");

                LearnSkill(pc, 800);
                Say(pc, 0, 0, "学到 『矿物知识』!$R;", " ");
            }
            else
            {
                Say(pc, 11000020, 131, "先穿上衣服后，再和我说话吧。$R;", "矿工总管");
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
                    Say(pc, 131, "呵呵！您想成为铁匠？$R;" +
                        "不错不错$R;" +
                        "$R在您心脏里的烈焰蜥蜴$R;" +
                        "会一直帮助您的$R;" +
                        "$P从今以后，您就是铁匠了$R;");
                    鐵匠轉職(pc);
                    return;
                }
                Job2X_09_mask.SetValue(Job2X_09.轉職開始, true);
                //_3a42 = true;
                Say(pc, 131, "什么？您想成为铁匠？$R;" +
                    "好！$R;" +
                    "$R想要成为铁匠，强健的体魄是很重要的$R;" +
                    "$P去见火焰的火神，锻炼体魄后再回来吧$R;" +
                    "$R火焰的火神就在南边的山上。$R;");
                return;
            }
            Say(pc, 131, "您还未达到申请转职的条件。$R;");
        }

        void 鐵匠轉職(ActorPC pc)
        {

            switch (Select(pc, "真的要转职吗?", "", "我想成为铁匠", "听取关于铁匠的注意事项", "还是算了吧"))
            {
                case 1:
                    Say(pc, 131, "那么就给您烙印上这象征铁匠的$R;" +
                        "『铁匠纹章』吧$R;");
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
                            "您身上已经烙印了漂亮的纹章。$R;" +
                            "$R从今以后，$R您就成为代表我们的『铁匠』了。$R;");
                        PlaySound(pc, 4012, false, 100, 50);
                        Say(pc, 131, "您已转职为『铁匠』了。$R;");
                        return;
                    }
                    Say(pc, 131, "防御太高的话，就无法烙印纹章了$R;" +
                        "去把装备脱掉后再来吧$R;");
                    break;
                case 2:
                    Say(pc, 131, "得先和您说清楚。$R;" +
                        "转职成为铁匠的话，$R;" +
                        "您的职业等级会变回1级。$R;" +
                        "$P但好像矿工的技能$R;" +
                        "在转职以后也能学到。$R;" +
                        "$R还有另外一点得注意的，请留意！$R;" +
                        "$P『技能点数』是根据职业完全分开$R;" +
                        "$R矿工的技能点数$R;" +
                        "只有在身为矿工的时候学到。$R;" +
                        "$P矿工时的技能点数$R;" +
                        "在转职后还会留下的。$R;" +
                        "但是如果转职了$R;" +
                        "剩下的技能就不能再学了。$R;" +
                        "$P当然，矿工的技能级数$R;" +
                        "也不会再增加了。$R;" +
                        "$R如果有特别想学的技能，$R;" +
                        "最好是转职前就学习好。$R;");

                    鐵匠轉職(pc);
                    break;
                case 3:
                    break;
            }
        }
    }
}

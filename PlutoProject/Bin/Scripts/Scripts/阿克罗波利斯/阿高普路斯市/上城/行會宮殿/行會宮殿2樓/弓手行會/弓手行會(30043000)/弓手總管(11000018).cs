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

            Say(pc, 11000018, 131, "这里是弓箭手行会…$R;" +
                                   "呵呵!$R;", "弓箭手总管");

            if (JobBasic_04_mask.Test(JobBasic_04.弓手轉職成功) &&
                !JobBasic_04_mask.Test(JobBasic_04.已經轉職為弓手))
            {
                弓手轉職完成(pc);
                return;
            }

            if (pc.Job == PC_JOB.NOVICE)
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
                    "过的还好吗？$R;");
                switch (Select(pc, "做什么呢?", "", "我要转职！", "听取冒险意见", "买东西", "购买入国许可证", "什么也不做"))
                {
                    case 1:
                        進階轉職(pc);
                        break;

                    case 2:
                        Say(pc, 131, "箭的命中率不是很高吗？$R;" +
                            "$R那样的话，就点击魔物以后$R;" +
                            "再好好试试吧。$R;" +
                            "$P红色力量计满了的话$R;" +
                            "命中率会提升的$R;" +
                            "$R一定要试试哦$R;");
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

            Say(pc, 11000018, 131, "我是管理弓箭手们的弓箭手总管。$R;" +
                                   "$P咦，您是初心者吧?$R;" +
                                   "$R您想不想做『弓箭手』呢?$R;" +
                                   "先听听我的说明吧。$R;", "弓箭手总管");

            selection = Select(pc, "想做什么?", "", "我想成为『弓箭手』!", "『弓箭手』是什么样的职业?", "任务服务台", "什么也不做");

            while (selection != 4)
            {
                switch (selection)
                {
                    case 1:
                        Say(pc, 11000018, 131, "想成为『弓箭手』?$R;" +
                                               "$这样啊，$R;" +
                                               "那尝试做做看『自己做的箭』一个。$R;" +
                                               "$P让我知道您有具备成为『弓箭手』的才能。$R;", "弓箭手总管");

                        switch (Select(pc, "接受吗?", "", "没问题", "才不要"))
                        {
                            case 1:
                                JobBasic_04_mask.SetValue(JobBasic_04.選擇轉職為弓手, true);

                                Say(pc, 11000018, 131, "材料是『咕咕的羽毛』一个 +『树枝』一个。$R;" +
                                                       "找「武器制作所店员」制作『自己做的箭』，$R;" +
                                                       "拿『自己做的箭』一个给我看吧。$R;", "弓箭手总管");
                                break;
                                
                            case 2:
                                Say(pc, 11000018, 131, "是吗?$R;" +
                                                       "$R『弓箭手』做自己所需的武器是必须的呢。$R;", "弓箭手总管");
                                break;
                        }
                        return;

                    case 2:
                        Say(pc, 11000018, 131, "弓箭手这职业比较适合$R;" +
                                               "埃米尔和多米尼翁哦!$R;" +
                                               "$R这样您还要听下去吗?$R;", "弓箭手总管");

                        switch (Select(pc, "还要听下去吗?", "", "我要听", "不听"))
                        {
                            case 1:
                                Say(pc, 11000018, 131, "『弓箭手』是使用箭的职业。$R;" +
                                                       "$R擅长远距离攻击，$R;" +
                                                       "所以基本上不可能会受到伤害。$R;" +
                                                       "$P相反，如果近距离战斗，$R;" +
                                                       "就不是很吃香了。$R;" +
                                                       "$P但是到将来可以成为$R;" +
                                                       "使用手枪的『神枪手』呢!$R;" +
                                                       "$R所以现在只好忍一忍啰!$R;" +
                                                       "$P弓箭手行会不像别的职业会介绍任务。$R;" +
                                                       "$R所以在找工作的话，$R;" +
                                                       "就要上「咖啡馆」$R;" +
                                                       "或者成为生产系的护卫，$R;" +
                                                       "来赚取报酬吧!$R;", "弓箭手总管");
                                break;
                                
                            case 2:
                                break;
                        }
                        break;

                    case 3:
                        Say(pc, 11000018, 131, "成为『弓箭手』我就帮您介绍任务。$R;", "弓箭手总管");
                        break;

                    case 4:
                        break;
                }

                selection = Select(pc, "想做什么?", "", "我想成为『弓箭手』喔", "『弓箭手』是什么样的职业?", "任务服务台", "什么也不做");
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
                Say(pc, 11000018, 131, "的确是『自己做的箭』。$R;" +
                                       "$R您真的很厉害啊!$R;" +
                                       "$P我开始期待您的将来了。$R;" +
                                       "$R既然您达成任务了，$R;" +
                                       "从现在开始，您就是『弓箭手』。$R;", "弓箭手总管");

                switch (Select(pc, "要转职为『弓箭手』吗?", "", "转职为『弓箭手』", "还是算了吧"))
                {
                    case 1:
                        JobBasic_04_mask.SetValue(JobBasic_04.弓手轉職任務完成, true);

                        PlaySound(pc, 2030, false, 100, 50);
                        TakeItem(pc, 10026401, 1);
                        Say(pc, 0, 0, "交出『自己做的箭』!$R;", " ");
                        break;
                        
                    case 2:
                        Say(pc, 11000018, 131, "如果想法变了，再来和我说话吧。$R;", "弓箭手总管");
                        break;
                }
            }
            else
            {
                Say(pc, 11000018, 131, "材料是『咕咕的羽毛』一个 +『树枝』一个。$R;" +
                                       "找「武器制作所店员」制作『自己做的箭』，$R;" +
                                       "拿『自己做的箭』一个给我看吧。$R;", "弓箭手总管");
            }
        }

        void 申請轉職為弓手(ActorPC pc)
        {
            BitMask<JobBasic_04> JobBasic_04_mask = new BitMask<JobBasic_04>(pc.CMask["JobBasic_04"]);

            Say(pc, 11000018, 131, "那么! 我就给您象征『弓箭手』的$R;" +
                                   "『弓箭手纹章』吧!$R;", "弓箭手总管");

            if (pc.Inventory.Equipments.Count == 0)
            {
                JobBasic_04_mask.SetValue(JobBasic_04.弓手轉職成功, true);

                PlaySound(pc, 3087, false, 100, 50);
                ShowEffect(pc, 4131);
                Wait(pc, 3960);

                Say(pc, 11000018, 131, "…$R;" +
                                       "$P好棒啊!$R;" +
                                       "您身上已经烙印了漂亮的纹章。$R;" +
                                       "$R从今以后，$R;" +
                                       "您就成为代表我们的『弓箭手』了。$R;", "弓箭手总管");

                PlaySound(pc, 4012, false, 100, 50);
                ChangePlayerJob(pc, PC_JOB.ARCHER);

                Say(pc, 0, 0, "您已经转职为『弓箭手』了!$R;", " ");

                Say(pc, 11000018, 131, "有一份小礼物，要送给您哦!$R;" +
                                       "先穿上衣服后，再和我说话吧。$R;" +
                                       "$R还有别忘了整理行李喔!$R;", "弓箭手总管");
            }
            else
            {
                Say(pc, 11000018, 131, "纹章是烙印在皮肤上的，$R;" +
                                       "先把装备脱掉吧。$R;", "弓箭手总管");
            }
        }

        void 弓手轉職完成(ActorPC pc)
        {
            BitMask<JobBasic_04> JobBasic_04_mask = new BitMask<JobBasic_04>(pc.CMask["JobBasic_04"]);

            if (pc.Inventory.Equipments.Count != 0)
            {
                JobBasic_04_mask.SetValue(JobBasic_04.已經轉職為弓手, true);

                Say(pc, 11000018, 131, "这是送给您的『练习弓』和『腰箭筒』。$R;" +
                                       "$R您一定要好好加油唷!$R;", "弓箭手总管");

                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, 60090050, 1);
                GiveItem(pc, 50070400, 1);
                Say(pc, 0, 0, "得到『练习弓』和『腰箭筒』!$R;", " ");

                LearnSkill(pc, 2035);
                Say(pc, 0, 0, "学到『投掷武器制作』!R;", " ");
            }
            else
            {
                Say(pc, 11000018, 131, "先穿上衣服后，再和我说话吧。", "弓箭手总管");            
            }
        }

        void 進階轉職(ActorPC pc)
        {
            BitMask<Job2X_04> Job2X_04_mask = pc.CMask["Job2X_04"];

            if (CountItem(pc, 10020751) >= 1 && Job2X_04_mask.Test(Job2X_04.進階轉職開始) && pc.Job == PC_JOB.ARCHER)
            {
                Say(pc, 131, "来啦，考试怎样了？$R;" +
                    "$P呵呵！是『猎人认证书』呢。$R;" +
                    "$R从现在开始，$R您就成为人人羡慕的『猎人』了$R;");
                進階轉職選擇(pc);
                return;
            }

            if (Job2X_04_mask.Test(Job2X_04.進階轉職開始) && pc.Job == PC_JOB.ARCHER)
            {
                Say(pc, 131, "猎人的专职考试是在$R;" +
                    "『阿克罗尼亚东海岸』。$R;" +
                    "$R到东海岸的商队帳篷附近$R;" +
                    "找『帕美拉小姐』，$R;" +
                    "在她那里参加转职考试吧。$R;" +
                    "$P从她那里得到猎人认证书的话$R;" +
                    "就承认您成为『猎人』唷。$R;");
                return;
            }

            if (pc.JobLevel1 > 29 && pc.Job == PC_JOB.ARCHER)
            {
                Job2X_04_mask.SetValue(Job2X_04.進階轉職開始, true);
                //_3a55 = true;
                Say(pc, 131, "您终于达到挑战高级职业的条件了$R;" +
                    "$P是的。$R;" +
                    "$R也就是从『弓箭手』转职为『猎人』。$R;");
                return;
            }

            Say(pc, 131, "不行，$R;" +
                "以您的实力，要转职太勉强了，$R;" +
                "还是先去累积经验吧。$R;");
        }

        void 進階轉職選擇(ActorPC pc)
        {
            BitMask<Job2X_04> Job2X_04_mask = pc.CMask["Job2X_04"];

            switch (Select(pc, "真的要转职吗?", "", "我想成为猎人", "听取关于猎人的注意事项", "还是算了吧"))
            {
                case 1:
                    Say(pc, 131, "那么就给您烙印上这象征猎人的$R;" +
                        "『猎人纹章』吧$R;");
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
                            "您身上已经烙印了漂亮的纹章。$R;" +
                            "$R从今以后，$R您就成为代表我们的『猎人』了。$R;");
                        PlaySound(pc, 4012, false, 100, 50);
                        Say(pc, 131, "您已转职为『猎人』了。$R;");
                        Job2X_04_mask.SetValue(Job2X_04.進階轉職結束, true);
                        return;
                    }
                    Say(pc, 131, "…$R;" +
                        "防御太高的话，就无法烙印纹章了$R;" +
                        "请换上轻便的服装后，再来吧。$R;");
                    break;
                case 2:
                    Say(pc, 131, "先要和您讲清楚。$R;" +
                        "成为『猎人』的话，$R职业LV会成为1。$R;" +
                        "$P弓箭手的技能在专制以后也可以学到。$R;" +
                        "$R但是有一点要注意的，$R您要听好了。$R;" +
                        "$P『技能点数』是和职业等级$R是完全分开的。$R;" +
                        "$R学习弓箭手技能時获得的技能点数$R;" +
                        "只有在职业是弓箭手的时候才能累积$R;" +
                        "$P转职以后虽然不会消失$R;" +
                        "但是弓箭手技能点数不会再提升$R;" +
                        "$P跟技能点数一样，$R;" +
                        "转职后弓箭手的技能学习等级$R也不会上升的。$R;" +
                        "$P也就是说$R除了现在开始学习的技能以外，$R;" +
                        "以后就不能学习。$R;" +
                        "$R如果还有想学的技能，$R还是学完以后再转职吧$R;");
                    進階轉職選擇(pc);
                    break;
                case 3:
                    break;
            }
        }
    }
}

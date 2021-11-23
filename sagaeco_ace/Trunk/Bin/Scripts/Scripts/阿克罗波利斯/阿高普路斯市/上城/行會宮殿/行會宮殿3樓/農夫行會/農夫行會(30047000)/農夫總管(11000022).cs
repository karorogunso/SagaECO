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

            Say(pc, 11000022, 131, "欢迎光临农夫行会。$R;", "农夫总管");

            if (JobBasic_10_mask.Test(JobBasic_10.農夫轉職成功) &&
                !JobBasic_10_mask.Test(JobBasic_10.已經轉職為農夫))
            {
                農夫轉職完成(pc);
                return;
            }

            if (pc.Job == PC_JOB.NOVICE)
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
                                       "$R要做什么？$R;", "农夫总管");

                switch (Select(pc, "要做些什么呢?", "", "任务服务台", "我想听简单的情报哦", "我想转职", "购买法伊斯特入国许可证", "什么也不做"))
                {
                    case 1:
                        HandleQuest(pc, 44);
                        break;
                    case 2:
                        Say(pc, 131, "听说有个地方，是农夫专用的田地呢$R;" +
                            "$R是喜欢栽培的人专用的地方$R;");
                        break;
                    case 3:
                        Say(pc, 131, "在这里不行啊$R;" +
                            "和炼金术师总管谈谈吧$R;");
                        break;
                    case 4:
                        OpenShopBuy(pc, 150);
                        Say(pc, 131, "农夫行会的总部在法伊斯特呢$R;");
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
                                Say(pc, 0, 0, "请注意$R;" +
                                   "目前BP系的技能大部分未实装$R;" +
                                   "不能保证技能的使用性没有问题$R;" +
                                   "同样,相关任务也得不到保证$R;" +
                                   "请谨慎选择转职农夫?$R;", "谜之音");
            Say(pc, 11000022, 131, "我是管理农夫们的农夫总管。$R;" +
                                   "$P哦，您还是初心者呢?$R;" +
                                   "$R想不想成为农夫呢?$R;", "农夫总管");

            selection = Select(pc, "想做什么?", "", "我想成为『农夫』!", "『农夫』是什么样的职业?", "任务服务台", "什么也不做");

            while (selection != 4)
            {
                switch (selection)
                {
                    case 1:
                        Say(pc, 11000022, 131, "想成为『农夫』吗?$R;" +
                                               "虽然成为农夫没有特别的要求，$R;" +
                                               "但若是这样的话$R;" +
                                               "是不是太没意思了?$R;" +
                                               "$R…所以，$R;" +
                                               "如果想成为农夫的话，$R;" +
                                               "就要拿两个对农夫来说$R;" +
                                               "很重要的东西给我。$R;" +
                                               "$R提示是『巨麦X』。$R;" +
                                               "$P拿来的话，$R;" +
                                               "就让您做农夫吧!$R;" +
                                               "$R…那也不错吧!$R;" +
                                               "怎么样?$R;", "农夫总管");

                        switch (Select(pc, "接受吗?", "", "没问题", "才不要"))
                        {
                            case 1:
                                JobBasic_10_mask.SetValue(JobBasic_10.選擇轉職為農夫, true);

                                Say(pc, 11000022, 131, "那就辛苦您了。$R;", "农夫总管");
                                break;

                            case 2:
                                Say(pc, 11000022, 131, "什么，算了?$R;", "农夫总管");
                                break;
                        }
                        return;

                    case 2:
                        Say(pc, 11000022, 131, "埃米尔是做农夫的最佳人选。$R;" +
                                               "$R要听更具体的说明吗?$R;", "农夫总管");

                        switch (Select(pc, "还要听下去吗?", "", "我要听", "不听"))
                        {
                            case 1:
                                Say(pc, 11000022, 131, "『农夫』因为是农民，$R;" +
                                                       "所以特别擅长于采集植物。$R;" +
                                                       "$R要采集植物的时候，$R;" +
                                                       "也就正是农夫发挥实力的时候。$R;" +
                                                       "$P此外，农夫还能够靠技能$R;" +
                                                       "制造各种道具呢。$R;" +
                                                       "$R应该说是可以自给自足吧。$R;" +
                                                       "$P做农夫的话，将来还有机会$R;" +
                                                       "担当活动木偶凭依的特殊职业。$R;" +
                                                       "$P可是农夫不擅长使用武器，$R;" +
                                                       "也不能穿着沉重的装备，$R;" +
                                                       "所以不适合战斗哦。$R;" +
                                                       "$R组成队伍的话，$R;" +
                                                       "一般担当运输的角色。$R;", "农夫总管");
                                break;

                            case 2:
                                break;
                        }
                        break;

                    case 3:
                        Say(pc, 11000022, 131, "如果不成为『农夫』的话，$R;" +
                                               "就不能在这里工作了。$R;" +
                                               "$R您不想成为『农夫』吗?$R;", "农夫总管");
                        break;
                }
                selection = Select(pc, "想做什么?", "", "我想成为『农夫』!", "『农夫』是什么样的职业?", "任务服务台", "什么也不做");
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
                Say(pc, 11000022, 131, "对，$R;" +
                                       "就是『巨麦穗』。$R;" +
                                       "$R好，现在您就是『农夫』了。$R;" +
                                       "$P真要转职为『农夫』是吧?$R;", "农夫总管");

                switch (Select(pc, "要转职为『农夫』吗?", "", "转职为『农夫』", "还是算了吧"))
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
                Say(pc, 11000022, 131, "提示是『巨麦X』两个!$R;" +
                                       "$P拿来的话，$R;" +
                                       "就让您转职为『农夫』吧!$R;", "农夫总管");
            }
        }

        void 申請轉職為農夫(ActorPC pc)
        {
            BitMask<JobBasic_10> JobBasic_10_mask = new BitMask<JobBasic_10>(pc.CMask["JobBasic_10"]);

            Say(pc, 11000022, 131, "那么! 我就给您这象征『农夫』的$R;" +
                                   "『农夫纹章』吧!$R;", "农夫总管");

            if (pc.Inventory.Equipments.Count == 0)
            {
                JobBasic_10_mask.SetValue(JobBasic_10.農夫轉職成功, true);

                PlaySound(pc, 3087, false, 100, 50);
                ShowEffect(pc, 4131);
                Wait(pc, 3960);

                Say(pc, 11000022, 131, "…$R;" +
                                       "$P哦，$R;" +
                                       "纹章和您很相配呢。$R;" +
                                       "$R从今以后，$R;" +
                                       "您就成为『农夫』了。$R;", "农夫总管");

                PlaySound(pc, 4012, false, 100, 50);
                ChangePlayerJob(pc, PC_JOB.FARMASIST);

                Say(pc, 0, 0, "您已经转职为『农夫』了!$R;", " ");

                Say(pc, 11000022, 131, "有一份小礼物，要送给您哦!$R;" +
                                       "先穿上衣服后，再和我说话吧。$R;" +
                                       "$R还有别忘了整理行李哦!$R;", "农夫总管");
            }
            else
            {
                Say(pc, 11000022, 131, "纹章是烙印在皮肤上的，$R;" +
                                       "先把装备脱掉吧。$R;", "农夫总管");
            }
        }

        void 農夫轉職完成(ActorPC pc)
        {
            BitMask<JobBasic_10> JobBasic_10_mask = new BitMask<JobBasic_10>(pc.CMask["JobBasic_10"]);

            if (pc.Inventory.Equipments.Count != 0)
            {
                JobBasic_10_mask.SetValue(JobBasic_10.已經轉職為農夫, true);

                Say(pc, 11000022, 131, "这是给您的『棉缎带』。$R;" +
                                       "$R如果把『棉缎带』和$R;" +
                                       "刚刚收集的『巨麦穗』合成的话，$R;" +
                                       "应该不错的。$R;", "农夫总管");

                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, 10043700, 1);
                Say(pc, 0, 0, "得到『棉缎带』!$R;", " ");

                LearnSkill(pc, 3128);
                Say(pc, 0, 0, "学到『栽培』!R;", " ");
            }
            else
            {
                Say(pc, 11000022, 131, "先穿上衣服后，再和我说话吧。$R;", "农夫总管");
            }
        }
    }
}

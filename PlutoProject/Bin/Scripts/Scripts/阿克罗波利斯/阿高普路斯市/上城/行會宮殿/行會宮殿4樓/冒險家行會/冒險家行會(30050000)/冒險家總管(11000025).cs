using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:冒險家行會(30050000) NPC基本信息:冒險家總管(11000025) X:3 Y:3
namespace SagaScript.M30050000
{
    public class S11000025 : Event
    {
        public S11000025()
        {
            this.EventID = 11000025;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<JobBasic_11> JobBasic_11_mask = new BitMask<JobBasic_11>(pc.CMask["JobBasic_11"]);

            Say(pc, 11000025, 131, "这里是冒险家行会。$R;", "冒险家总管");

            if (JobBasic_11_mask.Test(JobBasic_11.冒險家轉職成功) &&
                !JobBasic_11_mask.Test(JobBasic_11.已經轉職為冒險家))
            {
                冒險家轉職完成(pc);
                return;
            }

            if (pc.Job == PC_JOB.NOVICE)
            {
                if (JobBasic_11_mask.Test(JobBasic_11.選擇轉職為冒險家) &&!JobBasic_11_mask.Test(JobBasic_11.已經轉職為冒險家))
                {
                    冒險家轉職任務(pc);
                    return;
                }
                else
                {
                    冒險家簡介(pc);
                    return;
                }
            }

            if (pc.Job == PC_JOB.RANGER)
            {
                Say(pc, 131, "这不是" + pc.Name + "吗？！$R;" +
                    "$R有什么事吗?$R;");
                //EVT1100002560
                switch (Select(pc, "做什么呢？", "", "任务服务台", "我要转职", "购买法伊斯特入国许可证", "什么也不做"))
                {
                    case 1:
                        HandleQuest(pc, 45);
                        break;
                    case 2:
                        進階轉職(pc);
                        break;
                    case 3:
                        OpenShopBuy(pc, 150);
                        Say(pc, 131, "法伊斯特里会有行会特派员的。$R;" +
                            "$R去打个招呼吧。$R;");
                        break;
                    case 4:
                        break;
                }
            }
        }

        void 冒險家簡介(ActorPC pc)
        {
            BitMask<JobBasic_11> JobBasic_11_mask = new BitMask<JobBasic_11>(pc.CMask["JobBasic_11"]);

            int selection;
                                Say(pc, 0, 0, "请注意$R;" +
                                   "目前BP系的技能大部分未实装$R;" +
                                   "不能保证技能的使用性没有问题$R;" +
                                   "同样,相关任务也得不到保证$R;" +
                                   "请谨慎选择转职冒险家?$R;", "谜之音");
            Say(pc, 11000025, 131, "我是管理冒险家们的冒险家总管。$R;" +
                                   "$P想不想成为『冒险家』呢?$R;" +
                                   "$R听听我的说明如何?$R;", "冒险家总管");

            selection = Select(pc, "想做什么?", "", "我想成为『冒险家』!", "『冒险家』是什么样的职业?", "任务服务台", "什麼也不做");

            while (selection != 4)
            {
                switch (selection)
                {
                    case 1:
                        Say(pc, 11000025, 131, "想加入我们一起去探险。$R;" +
                                               "$R那就要想办法得到我的认可。$R;" +
                                               "$P…要接受我的测试吗?$R;", "冒险家总管");

                        switch (Select(pc, "接受吗?", "", "没问题", "才不要"))
                        {
                            case 1:
                                JobBasic_11_mask.SetValue(JobBasic_11.選擇轉職為冒險家, true);

                                Say(pc, 11000025, 131, "想办法收集『骨头』2个，$R;" +
                                                       "再回到这吧。$R;", "冒险家总管");
                                break;

                            case 2:
                                Say(pc, 11000025, 131, "……，$R;" +
                                                       "考虑清楚再来吧。$R;", "冒险家总管");
                                break;
                        }
                        return;

                    case 2:
                        Say(pc, 11000025, 131, "冒险家是只适合埃米尔的职业，$R;" +
                                               "其他种族就不太适合!$R;" +
                                               "$R即便如此还要听吗?$R;", "冒险家总管");

                        switch (Select(pc, "还要听下去吗?", "", "我要听", "不听"))
                        {
                            case 1:
                                Say(pc, 11000025, 131, "『冒险家』代表性的能力是野营。$R;" +
                                                       "即使不在村落的地方也能恢复。$R;" +
                                                       "$P所以出去打猎的时候，$R;" +
                                                       "直到东西装不下为止，才会回来。$R;" +
                                                       "$P冒险家在多方面都具有丰富的知识。$R;" +
                                                       "也可以从魔物那里得到稀有的道具。$R;" +
                                                       "$P以『冒险家』的身份进行修炼的话，$R;" +
                                                       "还可以解除枷锁或者陷阱。$R;" +
                                                       "$R是搜集道具的最佳职业!$R;", "冒险家总管");
                                break;

                            case 2:
                                break;
                        }
                        break;

                    case 3:
                        Say(pc, 11000025, 131, "想做事吗?$R;" +
                                               "$R不好意思，$R;" +
                                               "这里只给『冒险家』介绍工作$R;" +
                                               "$P怎样?$R;" +
                                               "要不要成为『冒险家』?$R;", "冒险家总管");
                        break;
                }
                selection = Select(pc, "想做什么?", "", "我想成为『冒险家』!", "『冒险家』是什么样的职业?", "任务服务台", "什么也不做");
            } 
        }

        void 冒險家轉職任務(ActorPC pc)
        {
            BitMask<JobBasic_11> JobBasic_11_mask = new BitMask<JobBasic_11>(pc.CMask["JobBasic_11"]);

            if (!JobBasic_11_mask.Test(JobBasic_11.冒險家轉職任務完成))
            {
                給予骨頭(pc);
            }

            if (JobBasic_11_mask.Test(JobBasic_11.冒險家轉職任務完成) &&
                !JobBasic_11_mask.Test(JobBasic_11.冒險家轉職成功))
            {
                申請轉職為冒險家(pc);
                return;
            }
        }

        void 給予骨頭(ActorPC pc)
        {
            BitMask<JobBasic_11> JobBasic_11_mask = new BitMask<JobBasic_11>(pc.CMask["JobBasic_11"]);

            if (CountItem(pc, 10006600) > 1)
            {
                Say(pc, 11000025, 131, "好棒!!$R;" +
                                       "$R已经取得『骨头』了!$R;" +
                                       "$P那要成为『冒险家』吗?$R;", "冒险家总管");

                switch (Select(pc, "要转职为『冒险家』吗?", "", "转职为『冒险家』", "还是算了吧"))
                {
                    case 1:
                        JobBasic_11_mask.SetValue(JobBasic_11.冒險家轉職任務完成, true);

                        TakeItem(pc, 10006600, 2);
                        break;

                    case 2:
                        break;
                }
            }
            else
            {
                Say(pc, 11000025, 131, "打倒「巴乌」会掉落『骨头』。$R;" +
                                       "$R收集两个『骨头』后，;" +
                                       "再回来找我吧!$R;", "冒险家总管");
            }
        }

        void 申請轉職為冒險家(ActorPC pc)
        {
            BitMask<JobBasic_11> JobBasic_11_mask = new BitMask<JobBasic_11>(pc.CMask["JobBasic_11"]);

            Say(pc, 11000025, 131, "那么就给您纹上这象征『冒险家』的，$R;" +
                                   "『冒险家纹章』。$R;", "冒险家总管");

            if (pc.Inventory.Equipments.Count == 0)
            {
                JobBasic_11_mask.SetValue(JobBasic_11.冒險家轉職成功, true);

                PlaySound(pc, 3087, false, 100, 50);
                ShowEffect(pc, 4131);
                Wait(pc, 3960);

                Say(pc, 11000025, 131, "…$R;" +
                                       "$P好棒啊，$R;" +
                                       "您身上已经烙印了漂亮的纹章。$R;" +
                                       "$R从今以后，$R;" +
                                       "您就成为代表我们的『冒险家』了。$R;", "冒险家总管");

                PlaySound(pc, 4012, false, 100, 50);
                ChangePlayerJob(pc, PC_JOB.RANGER);

                Say(pc, 0, 0, "您已經轉職為『冒險家』了!$R;", " ");

                Say(pc, 11000025, 131, "先穿上衣服後，再和我說話吧。$R;" +
                                       "有一份小禮物，要送給您唷!$R;" +
                                       "$R您先去整理行李後，再來找我吧。$R;", "冒險家總管");
            }
            else
            {
                Say(pc, 11000025, 131, "紋章是烙印在皮膚上的，$R;" +
                                       "先把裝備脫掉吧。$R;", "冒險家總管");
            }
        }

        void 冒險家轉職完成(ActorPC pc)
        {
            BitMask<JobBasic_11> JobBasic_11_mask = new BitMask<JobBasic_11>(pc.CMask["JobBasic_11"]);

            if (pc.Inventory.Equipments.Count != 0)
            {
                JobBasic_11_mask.SetValue(JobBasic_11.已經轉職為冒險家, true);

                Say(pc, 11000025, 131, "给您『护目镜帽』，$R;" +
                                       "$R您就用心练吧。$R;", "冒险家总管");

                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, 50051600, 1);
                Say(pc, 0, 0, "得到『护目镜帽』!$R;", " ");

                LearnSkill(pc, 713);
                Say(pc, 0, 0, "学到『露营』!$R;", " ");
            }
            else
            {
                Say(pc, 11000025, 131, "先穿上衣服后，再和我说话吧。$R;", "冒险家总管");
            }
        }

        void 進階轉職(ActorPC pc)
        {
            BitMask<Job2X_11> Job2X_11_mask = pc.CMask["Job2X_11"];

            if (pc.Job == PC_JOB.RANGER && pc.JobLevel1 >= 30)
            {
                if (Job2X_11_mask.Test(Job2X_11.給予冰花))//_3a59)
                {
                    轉職選擇(pc);
                    return;
                }
                if (CountItem(pc, 50035651) >= 1 && Job2X_11_mask.Test(Job2X_11.轉職開始))//_3a46)
                {
                    Job2X_11_mask.SetValue(Job2X_11.給予冰花, true);
                    //_3a59 = true;
                    TakeItem(pc, 50035651, 1);
                    Say(pc, 131, "这不是传说中的『冰灵之花』吗？$R;" +
                        "$R真漂亮啊，我能摸摸看吗？$R;");
                    Say(pc, 131, "『冰灵之花』融化掉了。$R;");
                    Say(pc, 131, "啊，这！$R;" +
                        "$P什麼，人只要用手摸的话就会融化？$R;" +
                        "真是，太对不起了，怎么办呢？$R;" +
                        "哈哈，但是您真的是$R有做探险家的資質呢。$R;" +
                        "您合格了。$R;" +
                        "$R现在开始，您就是『探险家』了$R;");
                    轉職選擇(pc);
                    return;
                }
                Job2X_11_mask.SetValue(Job2X_11.轉職開始, true);
                //_3a46 = true;
                Say(pc, 131, "转职?$R;" +
                    "想成为『探险家』是吗？$R;" +
                    "如果是勇敢的您，应该差不多吧。$R;" +
                    "$P但是探险家是寻遍世间宝物的$R探求心丰富的人。$R;" +
                    "$P…如果您不能证明您有这种心的话$R别人不会承认您的。$R;" +
                    "$P这个世界上还有一个古老的种族$R叫冰精灵。$R;" +
                    "$R希望您能找到$R据说只有他们才能找到的『冰花』。$R;" +
                    "$P但是谁也不知道他们在哪里生活，$R而且也不知道$R;" +
                    "世上是不是真有那种花。$R;" +
                    "$R但是听到这样的话，$R您不会心动吗？$R;" +
                    "$P 到艾恩萨乌斯的花店看看吧。$R;" +
                    "应该会有消息吧？?$R;" +
                    "$P那就辛苦您了$R;");
                return;
            }
            Say(pc, 131, "您现在还不能转职。$R;" +
                "还是先去累积经验吧。$R;");
        }

        void 轉職選擇(ActorPC pc)
        {

            switch (Select(pc, "真的要转职吗?", "", "我想成为探险家", "听取关于探险家的注意事项", "还是算了吧"))
            {
                case 1:
                    Say(pc, 131, "那么就给您烙印上这象征探险家的$R;" +
                        "『探险家纹章』吧$R;");
                    if (pc.Inventory.Equipments.Count == 0)
                    {
                        ChangePlayerJob(pc, PC_JOB.EXPLORER);
                        pc.JEXP = 0;
                        //PARAM ME.JOB = 103
                        PlaySound(pc, 3087, false, 100, 50);
                        ShowEffect(pc, 4131);
                        Wait(pc, 4000);
                        Say(pc, 131, "…$R;" +
                            "$P好棒啊，$R;" +
                            "您身上已经烙印了漂亮的纹章。$R;" +
                            "$R从今以后，$R您就成为代表我们的『探险家』了。$R;");
                        PlaySound(pc, 4012, false, 100, 50);
                        Say(pc, 131, "您已转职为『探险家』了。$R;");
                        return;
                    }
                    Say(pc, 131, "防御太高的话，就无法烙印纹章了$R;" +
                        "请换上轻便的服装后，再来吧。$R;");
                    break;
                case 2:
                    Say(pc, 131, "先跟您说清楚了，$R转职的话，职业等级会变回1级。$R;" +
                        "$P冒险家的技能$R在转职以后好像还可以学。$R;" +
                        "$R但是有要注意的地方。$R;" +
                        "$P『技能点数』是按照职业完全分开的$R;" +
                        "$R冒险家的职业点数$R只能在冒险家的时候取得。$R;" +
                        "$P转职之前的技能点数虽然还会留着，$R但是转职以后就不会再增加了。$R;" +
                        "$P当然，职业等级也不会提高。$R;" +
                        "$R如果还有想学的技能$R就在转职之前学吧。$R;");
                    轉職選擇(pc);
                    break;
                case 3:
                    break;
            }
        }
    }
}
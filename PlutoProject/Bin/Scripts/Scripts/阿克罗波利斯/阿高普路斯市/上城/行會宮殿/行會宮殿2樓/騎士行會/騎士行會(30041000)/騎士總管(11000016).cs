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

            Say(pc, 11000016, 131, "欢迎来到骑士行会。$R;", "骑士总管");

            if (JobBasic_02_mask.Test(JobBasic_02.騎士轉職成功) &&
                !JobBasic_02_mask.Test(JobBasic_02.已經轉職為騎士))
            {
                騎士轉職完成(pc);
                return;
            }

            if (pc.Job == PC_JOB.NOVICE)
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
                                       "$R好久不见啦!$R;", "骑士总管");
                switch (Select(pc, "要做什么呢?", "", "购买入国许可证", "我想转职", "什么也不做"))
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

            Say(pc, 11000016, 131, "我是管理骑士们的骑士总管。$R;" +
                                   "$R哦，您还是初心者呢!$R;" +
                                   "呵呵…$R;" +
                                   "$P如果没有特别想做的职业的话，$R;" +
                                   "想不想做骑士呢?$R;" +
                                   "$R先听听我的意见吧!$R;", "骑士总管");

            selection = Select(pc, "想做什么?", "", "我想成为『骑士』!", "『骑士』是什么样的职业?", "任务服务台", "什么也不做");

            while (selection != 4)
            {
                switch (selection)
                {
                    case 1:
                        Say(pc, 11000016, 131, "想要成为『骑士』?$R;" +
                                               "您这么说我很高兴。$R;" +
                                               "$R但是您真的有这个本事吗?$R;" +
                                               "先考验一下您的力量吧！$R;", "骑士总管");

                        switch (Select(pc, "接受『考验』吗?", "", "没问题", "才不要"))
                        {
                            case 1:
                                JobBasic_02_mask.SetValue(JobBasic_02.選擇轉職為騎士, true);

                                Say(pc, 11000016, 131, "这里西边有一种魔物，名字叫做「杀人蜂」。$R;" +
                                                       "它的外形长得跟蜜蜂一样。$R;" +
                                                       "$R任务就是把「杀人蜂」杀掉就可以了。$R;" +
                                                       "$P啊，別忘了!!$R;" +
                                                       "还要拿到『蜜蜂的毒针』$R;" +
                                                       "作为胜利的证据喔!$R;" +
                                                       "$P如果您真能打倒「杀人蜂」的话，$R;" +
                                                       "这样您就可以成为骑士唷。$R;", "骑士总管");
                                break;

                            case 2:
                                Say(pc, 11000016, 131, "算了吗……，$R;" +
                                                       "再考虑一下吧。$R;", "骑士总管");
                                break;
                        }
                        return;

                    case 2:
                        Say(pc, 11000016, 131, "『骑士』这职业比较适合$R;" +
                                               "埃米尔和多米尼翁的体质哦!$R;" +
                                               "$R还要继续听吗?$R;", "骑士总管");

                        switch (Select(pc, "还要听下去吗?", "", "我要听", "不听"))
                        {
                            case 1:
                                Say(pc, 11000016, 131, "『骑士』主要是使用细剑和枪矛的华丽战士。$R;" +
                                                       "$R骑士锐利的突刺攻击是$R;" +
                                                       "谁也不可能回避的。$R;" +
                                                       "$P可惜搜集能力和搬运能力比较低，$R;" +
                                                       "并不适合一个人单独行动。$R;" +
                                                       "$P心被黑暗力量感染的骑士，$R;" +
                                                       "听说还可以掌握别的力量。$R;" +
                                                       "$P骑士行会，还没有开放接受任务。$R;" +
                                                       "$R所以在找工作的话，就上「咖啡馆」$R;" +
                                                       "或者成为生产系的护卫，$R;" +
                                                       "来赚取报酬吧!$R;", "骑士总管");
                                break;

                            case 2:
                                break;
                        }
                        break;

                    case 3:
                        Say(pc, 11000016, 131, "等您成为光荣的『骑士』，再找我配给工作。$R;", "骑士总管");
                        break;

                    case 4:
                        break;
                }

                selection = Select(pc, "想做什么?", "", "我想成为『骑士』!", "『骑士』是什么样的职业?", "任务服务台", "什么也不做");
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
                Say(pc, 11000016, 131, "哇! 真的是『蜜蜂的毒针』。$R;" +
                                       "您真的很厉害啊。$R;" +
                                       "$R我开始期待您的将来了。$R;" +
                                       "$P既然您达成任务了，$R;" +
                                       "从现在开始，您就是『骑士』啦!$R;", "骑士总管");

                switch (Select(pc, "要转职为『骑士』吗?", "", "转职为『騎士』", "還是算了吧"))
                {
                    case 1:
                        JobBasic_02_mask.SetValue(JobBasic_02.騎士轉職任務完成, true);

                        PlaySound(pc, 2030, false, 100, 50);
                        TakeItem(pc, 10035200, 1);
                        Say(pc, 0, 0, "交出『蜜蜂的毒針』了。$R;", " ");
                        break;

                    case 2:
                        Say(pc, 11000016, 131, "不想成为骑士吗?$R;" +
                                               "$R嘿嘿!$R;" +
                                               "也有这样的时候吧?$R;" +
                                               "$P没办法，$R;" +
                                               "如果想法变了，再来和我说话吧。$R;", "骑士总管");
                        break;
                }
            }
            else
            {
                Say(pc, 11000016, 131, "这里西边有一种魔物，名字叫做「杀人蜂」。$R;" +
                                       "它的外形长的跟蜜蜂一样。$R;" +
                                       "$R任务就是把「杀人蜂」杀掉就可以了。$R;" +
                                       "$P啊，別忘了!!$R;" +
                                       "还要拿到『蜜蜂的毒针』$R;" +
                                       "作为胜利的证据喔!$R;" +
                                       "$P如果您真能打倒「杀人蜂」的话，$R;" +
                                       "这样您就可以成为骑士唷。$R;", "骑士总管");
            }
        }

        void 申請轉職為騎士(ActorPC pc)
        {
            BitMask<JobBasic_02> JobBasic_02_mask = new BitMask<JobBasic_02>(pc.CMask["JobBasic_02"]);

            Say(pc, 11000016, 131, "那么我就给您象征『骑士』的$R;" +
                                   "『骑士纹章』吧。$R;", "骑士总管");

            if (pc.Inventory.Equipments.Count == 0)
            {
                JobBasic_02_mask.SetValue(JobBasic_02.騎士轉職成功, true);

                PlaySound(pc, 3087, false, 100, 50);
                ShowEffect(pc, 4131);
                Wait(pc, 3960);

                Say(pc, 11000016, 131, "…$R;" +
                                       "$P好棒啊!$R;" +
                                       "您身上已经烙印了漂亮的纹章。$R;" +
                                       "$R从今以后，$R;" +
                                       "您就成为代表我们的『骑士』了。$R;", "骑士总管");

                PlaySound(pc, 4012, false, 100, 50);
                ChangePlayerJob(pc, PC_JOB.FENCER);

                Say(pc, 0, 0, "您已经转职为『骑士』了!$R;", " ");

                Say(pc, 11000016, 131, "有一份小礼物，要送给您哦!$R;" +
                                       "先穿上衣服后，再和我说话吧。$R;" +
                                       "$R还有别忘了整理行李哦!$R;", "骑士总管");
            }
            else
            {
                Say(pc, 11000016, 131, "纹章是烙印在皮肤上的，$R;" +
                                       "先把装备脱掉吧。$R;", "骑士总管");
            }
        }

        void 騎士轉職完成(ActorPC pc)
        {
            BitMask<JobBasic_02> JobBasic_02_mask = new BitMask<JobBasic_02>(pc.CMask["JobBasic_02"]);

            if (pc.Inventory.Equipments.Count != 0)
            {
                JobBasic_02_mask.SetValue(JobBasic_02.已經轉職為騎士, true);
                Say(pc, 11000016, 131, "这是『幻灵面具』$R;" +
                                       "是只有骑士，才能佩戴的脸部饰品喔。$R;" +
                                       "$R您一定要好好珍藏唷!$R;", "骑士总管");

                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, 50040400, 1);
                Say(pc, 0, 0, "得到『幻灵面具』!$R;", " ");

                LearnSkill(pc, 2138);
                Say(pc, 0, 0, "学到『狂风突刺』!R;", " ");
            }
            else
            {
                Say(pc, 11000016, 131, "先穿上衣服后，再和我说话吧。", "骑士总管");
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
                        "要穿整齐点喔！$R;");
                    return;
                }

                Say(pc, 131, "以您的实力还是不行喔，$R;" +
                    "还是多累积点经验吧。$R;");
                return;
            }

            if (pc.Job == PC_JOB.FENCER && pc.JobLevel1 > 29)
            {

                if (CountItem(pc, 10020600) >= 1)
                {
                    Say(pc, 131, "很好，既然取得了认证书，$R那就让您转职吧。$R;" +
                        "$R从现在开始，$R您就成为人人羡慕的『圣骑士』了$R;");
                    進階轉職選擇(pc);
                    return;
                }

                if (Job2X_02_mask.Test(Job2X_02.進階轉職開始))//_3A33)
                {
                    Say(pc, 131, "只要到『艾恩萨乌斯』的$R『凤老头』那里$R;" +
                        "拿到认证书的话，$R;" +
                        "就承认您成为『圣骑士』唷。$R;");
                    return;
                }

                Say(pc, 131, "哈哈，您成长了很多喔$R;" +
                    "$R我想您是时候从『骑士』$R;" +
                    "转职成『圣骑士』了吧？$R;");

                Say(pc, 131, "只要到『艾恩萨乌斯』的$R『凤老头』那里$R;" +
                    "拿到认证书的话，$R;" +
                    "就承认您成为『圣骑士』唷。$R;");
                Job2X_02_mask.SetValue(Job2X_02.進階轉職開始, true);
                //_3A33 = true;
                return;
            }

            if (pc.Inventory.Equipments.Count == 0)
            {
                Say(pc, 131, "真是的！$R;" +
                    "要穿整齐点喔！$R;");
                return;
            }

            Say(pc, 131, "以您的实力还是不行喔，$R;" +
                "还是多累积点经验吧。$R;");
        }

        void 進階轉職選擇(ActorPC pc)
        {
            BitMask<Job2X_02> Job2X_02_mask = pc.CMask["Job2X_02"];

            switch (Select(pc, "真的要转职吗?", "", "我想成为圣骑士", "听取关于圣骑士的注意事项", "还是算了吧"))
            {
                case 1:
                    Say(pc, 131, "那么就给您烙印上这象征圣骑士的$R;" +
                        "『圣骑士纹章』吧$R;");
                    if (pc.Inventory.Equipments.Count == 0)
                    {
                        Say(pc, 131, "最后再向您确认一次，$R;" +
                            "您是真的决定转职吗?$R;");

                        switch (Select(pc, "真的要转职吗?", "", "转职", "不转职"))
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
                                    "$P呵呵，您身上已经烙印了漂亮的纹章。$R;" +
                                    "$R从今以后，$R您就成为代表我们的『圣骑士』了。$R;");
                                PlaySound(pc, 4012, false, 100, 50);
                                Say(pc, 131, "您已转职为『圣骑士』了。$R;");
                                break;

                            case 2:
                                Say(pc, 131, "您没想过要成为圣骑士吗？$R;" +
                                    "$R嗯……$R;" +
                                    "好，那您好好考虑一下吧。$R;");
                                break;
                        }
                        return;
                    }

                    Say(pc, 131, "防御太高的话，就无法烙印纹章了$R;" +
                        "请换上轻便的服装后，再来吧。$R;");
                    break;

                case 2:
                    Say(pc, 131, "成为『圣骑士』的话$R职业LV会成为1.$R;" +
                        "但是转职前所拥有的$R;" +
                        "$R技能和技能点数是不会消失的。$R;" +
                        "$P还有转职之前不能学习的技能，$R;" +
                        "在转职后也不可以学习的。$R;" +
                        "例如职业等级为30时转职的话，$R;" +
                        "$R职业前30级以上的技能$R;" +
                        "就不能学习了，请注意。$R;" +
                        "$P转职之前好好想清楚喔！$R;");

                    進階轉職選擇(pc);
                    break;

                case 3:
                    Say(pc, 131, "您没想过要成为圣骑士吗？$R;" +
                        "$R嗯……$R;" +
                        "好，那您好好考虑一下吧。$R;");
                    break;
            }
        }
    }
}

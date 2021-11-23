using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:劍士行會(30040000) NPC基本信息:劍士總管(11000015) X:3 Y:3
namespace SagaScript.M30040000
{
    public class S11000015 : Event
    {
        public S11000015()
        {
            this.EventID = 11000015;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<JobBasic_01> JobBasic_01_mask = new BitMask<JobBasic_01>(pc.CMask["JobBasic_01"]);

            Say(pc, 11000015, 131, "欢迎光临剑士行会。$R;", "劍士總管");

            if (JobBasic_01_mask.Test(JobBasic_01.劍士轉職成功) &&
                !JobBasic_01_mask.Test(JobBasic_01.已經轉職為劍士))
            {
                劍士轉職完成(pc);
                return;
            }

            if (pc.Job == PC_JOB.NOVICE )
            {
                if (JobBasic_01_mask.Test(JobBasic_01.選擇轉職為劍士) &&
                    !JobBasic_01_mask.Test(JobBasic_01.已經轉職為劍士))
                {
                    劍士轉職任務(pc);
                    return;
                }
                else
                {
                    劍士簡介(pc);
                    return;
                }
            }

            if (pc.JobBasic == PC_JOB.SWORDMAN)
            {
                Say(pc, 11000015, 131, " 这不是" + pc.Name + "吗?!$R;" +
                                       "$R来得好，$R;" +
                                       "今天来有什么事吗?$R;", "剑士总管");
 
                switch (Select(pc, "做什么好呢?", "", "任务服务台", "我想转职", "购买入国许可证", "什么也不做"))
                {
                    case 1:
                        Say(pc, 0, 0, "目前尚未实装$R;", " ");
                        break;
                    case 2:
                        進階轉職(pc);
                        break;
                    case 3:
                        OpenShopBuy(pc, 105);
                        break;
                    case 4:
                        break;
                }
            }
        }

        void 劍士簡介(ActorPC pc)
        {
            BitMask<JobBasic_01> JobBasic_01_mask = new BitMask<JobBasic_01>(pc.CMask["JobBasic_01"]);

            int selection;

            Say(pc, 11000015, 131, "我是管理剑士们的剑士总管。$R;" +
                                   "$P您好像不属于我们行会的管辖呀?$R;" +
                                   "$R那么……$R;" +
                                   "您想不想做『剑士』呢?$R;", "剑士总管");

            selection = Select(pc, "想做什么?", "", "我想成为『剑士』!", "『剑士』是什么样的职业?", "任务服务台", "什么也不做");

            while (selection != 4)
            {
                switch (selection)
                {
                    case 1:
                        Say(pc, 11000015, 131, "想成为『剑士』吗?$R;" +
                                               "$R您看起来应该有点潜力，$R;" +
                                               "先考验一下您的力量吧!$R;", "剑士总管");

                        switch (Select(pc, "接受『考验』吗?", "", "没问题", "才不要"))
                        {
                            case 1:
                                if (pc.Str > 9)
                                {
                                    Say(pc, 11000015, 131, "您不用担心，任务很简单的。$R;" +
                                                           "$P有一种魔物，名字叫做「巴乌」，$R;" +
                                                           "它的外形长得跟恶狗一样。$R;" +
                                                           "任务就是把「巴乌」打倒就可以了。$R;" +
                                                           "$P啊，別忘了!!$R;" +
                                                           "还要拿到『肉』作为大摆的证据哦!$R;" +
                                                           "$R这样您就可以成为剑士唷。$R;", "剑士总管");

                                    switch (Select(pc, "接受『考验』吗?", "", "没问题", "才不要"))
                                    {
                                        case 1:
                                            JobBasic_01_mask.SetValue(JobBasic_01.選擇轉職為劍士, true);

                                            Say(pc, 11000015, 131, "……$R;" +
                                                                   "$P很好，$R;" +
                                                                   "我等您回来喔。$R;", "剑士总管");
                                            break;

                                        case 2:
                                            Say(pc, 11000015, 131, "剑士是勇敢的代表，$R;" +
                                                                   "充满勇气再来吧。$R;", "剑士总管");
                                            break;
                                    }
                                }
                                else
                                {
                                    Say(pc, 11000015, 131, "想成为剑士还是需要一点力量的!$R;" +
                                                           "$P力量达到10以后，再来找我吧。$R;", "剑士总管");
                                }
                                break;

                            case 2:
                                break;
                        }
                        return;

                    case 2:
                        Say(pc, 11000015, 131, "剑士这职业比较适合$R;" +
                                               "埃米尔和多米尼翁的体质哦!$R;" +
                                               "$R判断职业的性质，$R;" +
                                               "是否适合自己的种族是很重要的啊，$R;" +
                                               "还想听下去吗?$R;", "剑士总管");

                        switch (Select(pc, "还要听下去吗?", "", "我要听", "不听"))
                        {
                            case 1:
                                Say(pc, 11000015, 131, "『剑士』主要是使用剑和盾牌的战士!$R;" +
                                                       "当然，还可以使用别的武器。$R;" +
                                                       "$R剑士的最大魅力，$R;" +
                                                       "就是攻击力非常的高。$R;" +
                                                       "$P当然,防御力也很高。$R;" +
                                                       "这样就能成为队伍里的盾牌，$R;" +
                                                       "不仅可以战斗还能保护队友的安全!$R;" +
                                                       "$P可惜搜集能力和搬运能力比较低，$R;" +
                                                       "并不适合一个人单独行动。$R;" +
                                                       "$R是一个和同伴们互相合作，$R;" +
                                                       "就会散发光彩的职业。$R;", "剑士总管");
                                break;

                            case 2:
                                break;
                        }
                        break;

                    case 3:
                        Say(pc, 11000015, 131, "如果想在这里接任务的话，$R;" +
                                               "首先要具备一些条件哦。$R;" +
                                               "$P至于要具备什么条件呢?$R;" +
                                               "$R等您成为『剑士』之后，$R;" +
                                               "我再告诉您吧。$R;", "剑士总管");
                        break;
                }

                selection = Select(pc, "想做什么?", "", "我想成为『剑士』!", "『剑士』是什么样的职业?", "任务服务台", "什么也不做");
            } 
        }

        void 劍士轉職任務(ActorPC pc)
        {
            BitMask<JobBasic_01> JobBasic_01_mask = new BitMask<JobBasic_01>(pc.CMask["JobBasic_01"]);

            if (!JobBasic_01_mask.Test(JobBasic_01.劍士轉職任務完成))
            {
                給予巴鳴身上的肉(pc);
            }

            if (JobBasic_01_mask.Test(JobBasic_01.劍士轉職任務完成) &&
                !JobBasic_01_mask.Test(JobBasic_01.劍士轉職成功))
            {
                申請轉職為劍士(pc);
                return;
            }
        }

        void 給予巴鳴身上的肉(ActorPC pc)
        {
            BitMask<JobBasic_01> JobBasic_01_mask = new BitMask<JobBasic_01>(pc.CMask["JobBasic_01"]);

            if (CountItem(pc, 10006300) > 0)
            {
                Say(pc, 11000015, 131, "哇!! 真的把『肉』带来了，$R;" +
                                       "看来您做的还算不错。$R;" +
                                       "$R我开始期待您的将来了。$R;" +
                                       "$P既然您达成任务了，$R;" +
                                       "从现在开始，您就是『剑士』啦!$R;", "剑士总管");

                switch (Select(pc, "要转职为『剑士』吗?", "", "转职为『剑士』", "还是算了吧"))
                {
                    case 1:
                        JobBasic_01_mask.SetValue(JobBasic_01.劍士轉職任務完成, true);

                        PlaySound(pc, 2030, false, 100, 50);
                        TakeItem(pc, 10006300, 1);
                        Say(pc, 0, 0, "交出『肉』!$R;", " ");
                        break;

                    case 2:
                        Say(pc, 11000015, 131, "考虑清楚再来吧。$R;", "剑士总管");
                        break;
                }
            }
            else
            {
                Say(pc, 11000015, 131, "在「阿克罗尼亚北部平原」$R;" +
                                       "再上去的「斯诺普山道」。$R;" +
                                       "$R那里栖息着非常多的「巴乌」，$R;" +
                                       "但是「巴乌」很强!$R;" +
                                       "建议多找些朋友帮忙打!$R;", "剑士总管");
            }
        }

        void 申請轉職為劍士(ActorPC pc)
        {
            BitMask<JobBasic_01> JobBasic_01_mask = new BitMask<JobBasic_01>(pc.CMask["JobBasic_01"]);

            Say(pc, 11000015, 131, "那么就替您纹上代表『剑士』的$R;" +
                                   "『剑士纹章』吧。$R;", "剑士总管");

            if (pc.Inventory.Equipments.Count == 0)
            {
                JobBasic_01_mask.SetValue(JobBasic_01.劍士轉職成功, true);

                PlaySound(pc, 3087, false, 100, 50);
                ShowEffect(pc, 4131);
                Wait(pc, 3960);

                Say(pc, 11000015, 131, "…$R;" +
                                       "$P好棒啊，$R;" +
                                       "您身上已经烙印了漂亮的纹章。$R;" +
                                       "$R从今以后，$R;" +
                                       "您就成为『剑士』了。$R;", "剑士总管");

                PlaySound(pc, 4012, false, 100, 50);
                ChangePlayerJob(pc, PC_JOB.SWORDMAN);

                Say(pc, 0, 0, "您已经转职为『剑士』了!$R;", " ");

                Say(pc, 11000015, 131, "先穿上衣服后，再和我说话吧。$R;" +
                                       "有一份小礼物，要送给您!$R;" +
                                       "$R您先去整理行李后，再来找我吧。$R;", "剑士总管");
            }
            else
            {
                Say(pc, 11000015, 131, "纹章是烙印在皮肤上的，$R;" +
                                       "先把装备脱掉吧。$R;", "剑士总管");
            }
        }

        void 劍士轉職完成(ActorPC pc)
        {
            BitMask<JobBasic_01> JobBasic_01_mask = new BitMask<JobBasic_01>(pc.CMask["JobBasic_01"]);

            if (pc.Inventory.Equipments.Count != 0)
            {
                JobBasic_01_mask.SetValue(JobBasic_01.已經轉職為劍士, true);

                Say(pc, 11000015, 131, "给您『剑士的证明』，$R;" +
                                       "$R用『剑士的证明』代表剑士荣誉。$R;" +
                                       "好好加油喔。$R;", "剑士总管");

                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, 50051300, 1);
                Say(pc, 0, 0, "得到『剑士的证明』!$R;", " ");

                LearnSkill(pc, 2115);
                Say(pc, 0, 0, "学到『聚合一段』!$R;", " ");
            }
            else
            {
                Say(pc, 11000015, 131, "先穿上衣服后，再和我说话吧。$R;", "剑士总管");
            }
        }

        void 進階轉職(ActorPC pc)
        {
            BitMask<Job2X_01> Job2X_01_mask = pc.CMask["Job2X_01"];

            if (Job2X_01_mask.Test(Job2X_01.轉職完成))//_3A37)
            {
                if (pc.Inventory.Equipments.Count == 0)
                {
                    Say(pc, 131, "衣服要穿好啊！！！$R;");
                    return;
                }
                Say(pc, 131, "现在还不能转职，$R;" +
                    "还是先去累积经验吧。$R;");
                return;
            }

            if (CountItem(pc, 10020600) >= 1)
            {
                Say(pc, 131, "很好，既然取得了认证书，$R那就让您转职吧。$R;" +
                    "$R从现在开始，$R您就成为人人羡慕的『剑圣』了$R;");
                進階轉職選擇(pc);
                return;
            }

            if (pc.Inventory.Equipments.Count == 0)
            {
                Say(pc, 131, "衣服要穿好啊！！！$R;");
                return;
            }

            if (Job2X_01_mask.Test(Job2X_01.進階轉職開始))//_3A32)
            {
                Say(pc, 131, "只要到『艾恩萨乌斯』的$R『凤老头』那里$R;" +
                    "拿到认证书的话，$R;" +
                    "就承认您成为『剑圣』。$R;");
                return;
            }

            if (pc.Job == PC_JOB.SWORDMAN && pc.JobLevel1 > 29)
            {
                Say(pc, 131, "您终于达到挑战高级职业的条件了$R;" +
                    "也就是从剑士转职成剑圣。$R;");

                Say(pc, 131, "只要到『艾恩萨乌斯』的$R『凤老头』那里$R;" +
                    "拿到认证书的话，$R;" +
                    "就承认您成为『剑圣』。$R;");
                Job2X_01_mask.SetValue(Job2X_01.進階轉職開始, true);
                //_3A32 = true;
                return;
            }

            Say(pc, 131, "您还未达到申请转职的条件。$R;" +
                "先以剑士的职业，慢慢培养实力吧。$R;");
        }

        void 進階轉職選擇(ActorPC pc)
        {
            BitMask<Job2X_01> Job2X_01_mask = pc.CMask["Job2X_01"];

            switch (Select(pc, "真的要转职吗?", "", "我想成为剑圣", "听取关于剑圣的注意事项", "还是算了吧"))
            {
                case 1:
                    Say(pc, 131, "那么就给您烙印上这象征剑圣的$R;" +
                        "『剑圣纹章』吧$R;");
                    if (pc.Inventory.Equipments.Count == 0)
                    {
                        Say(pc, 131, "最后再向您确认一次，$R;" +
                            "您是真的决定转职吗?$R;");
                        switch (Select(pc, "真的要转职吗?", "", "成为剑圣", "算了吧"))
                        {
                            case 1:
                                TakeItem(pc, 10020600, 1);
                                Job2X_01_mask.SetValue(Job2X_01.轉職完成, true);
                                //_3A37 = true;
                                ChangePlayerJob(pc, PC_JOB.BLADEMASTER);
                                pc.JEXP = 0;
                                //PARAM ME.JOB = 3
                                PlaySound(pc, 3087, false, 100, 50);
                                ShowEffect(pc, 4131);
                                Wait(pc, 4000);
                                Say(pc, 131, "…$R;" +
                                    "$P好棒啊，$R;" +
                                    "您身上已经烙印了漂亮的纹章。$R;" +
                                    "$R从今以后，$R您就成为代表我们的『剑圣』了。$R;");
                                PlaySound(pc, 4012, false, 100, 50);
                                Say(pc, 131, "您已转职为『剑圣』了。$R;");
                                break;
                            case 2:
                                Say(pc, 131, "看来您还不想转职呀？$R;" +
                                    "我想也是，这么重大的决定$R需要时间慎重思考的吧$R;");
                                break;
                        }
                        return;
                    }
                    Say(pc, 131, "防御太高的话，就无法烙印纹章了$R;" +
                        "请换上轻便的服装后，再来吧。$R;");
                    break;
                case 2:
                    Say(pc, 131, "成为『剑圣』的話，$R职业LV会成为1。$R;" +
                        "但是转职前所拥有的$R;" +
                        "$R技能和技能点数是不会消失的。$R;" +
                        "$P还有转职之前不能学习的技能，$R;" +
                        "在转职以后也不可学习的。$R;" +
                        "例如职业等级为30时转职的话，$R;" +
                        "$R转职前30级以上的技能$R;" +
                        "就不能学习了，请注意。$R;");
                    進階轉職選擇(pc);
                    break;
                case 3:
                    Say(pc, 131, "看来您还不想转职呀？$R;" +
                        "我想也是，这么重大的决定$R需要时间慎重思考的吧$R;");
                    break;
            }
        }
    }
}

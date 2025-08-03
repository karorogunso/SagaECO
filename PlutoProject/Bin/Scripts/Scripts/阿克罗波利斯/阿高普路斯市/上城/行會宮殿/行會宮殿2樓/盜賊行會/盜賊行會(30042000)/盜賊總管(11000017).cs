using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:盜賊行會(30042000) NPC基本信息:盜賊總管(11000017) X:3 Y:3
namespace SagaScript.M30042000
{
    public class S11000017 : Event
    {
        public S11000017()
        {
            this.EventID = 11000017;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<JobBasic_03> JobBasic_03_mask = new BitMask<JobBasic_03>(pc.CMask["JobBasic_03"]);

            BitMask<Job2X_03> mask = pc.CMask["Job2X_03"];

            Say(pc, 11000017, 131, "欢迎光临!$R;" +
                                   "这里就是盗贼行会。$R;", "盗贼总管");

            if (JobBasic_03_mask.Test(JobBasic_03.盜賊轉職成功) &&
                !JobBasic_03_mask.Test(JobBasic_03.已經轉職為盜賊))
            {
                盜賊轉職完成(pc);
                return;
            }

            if (pc.Job == PC_JOB.NOVICE)
            {
                if (JobBasic_03_mask.Test(JobBasic_03.選擇轉職為盜賊) &&
                    !JobBasic_03_mask.Test(JobBasic_03.已經轉職為盜賊))
                {
                    盜賊轉職任務(pc);
                    return;
                }
                else
                {
                    盜賊簡介(pc);
                    return;
                }
            }

            if (pc.JobBasic == PC_JOB.SCOUT)
            {
                if (mask.Test(Job2X_03.第一個問題回答錯誤))//_4A04)
                {
                    mask.SetValue(Job2X_03.第一個問題回答錯誤, false);
                    //_4A04 = false;
                    Say(pc, 131, "暗号错了？$R;" +
                        "那没办法啰$R;" +
                        "$R再告诉您一次，您要好好听着喔$R;" +
                        "$P提示就是『天空』！！$R;" +
                        "别再忘掉唷！$R;");
                    return;
                }
                if (mask.Test(Job2X_03.刺客轉職開始))//_4A00)
                {
                    進階轉職完成(pc);
                    return;
                }
                if (pc.Inventory.Equipments.Count == 0)
                {
                    Say(pc, 131, "叫您快点穿衣服$R;");
                    return;
                }

                Say(pc, 11000017, 131, pc.Name + "? 对不对?$R;" +
                                       "$R今天是什么日子啊？$R;", "盗贼总管");
                //EVT1100001760
                switch (Select(pc, "要做些什么呢?", "", "任务服务台", "购买入国许可证", "购买药物", "我想转职", "什么也不做"))
                {
                    case 1:
                        HandleQuest(pc, 13);
                        break;
                    case 2:
                        Say(pc, 131, "您想去艾恩萨乌斯吗？$R;");
                        OpenShopBuy(pc, 105);
                        break;
                    case 3:
                        Say(pc, 131, "要小心喔！$R;");
                        OpenShopBuy(pc, 153);
                        break;
                    case 4:
                        if (!mask.Test(Job2X_03.轉職結束))
                        {
                            進階轉職介紹(pc);
                            return;
                        }
                        Say(pc, 131, "不能再转职了$R;");
                        break;
                    case 5:
                        break;
                }
                //EVENTEND

                switch (Select(pc, "想做什么呢?", "", "任务服务台", "出售入国许可证", "我想转职", "什么也不做"))
                {
                    case 1:
                        break;

                    case 2:
                        break;

                    case 3:
                        break;

                    case 4:
                        break;
                }
            }
        }

        void 盜賊簡介(ActorPC pc)
        {
            BitMask<JobBasic_03> JobBasic_03_mask = new BitMask<JobBasic_03>(pc.CMask["JobBasic_03"]);

            int selection;

            Say(pc, 11000017, 131, "我是管理盗贼们的盗贼总管。$R;" +
                                   "$P您好像不属于我们行会的管辖呀?$R;" +
                                   "$R…这样看来…$R;" +
                                   "您想不想做『盗贼』呢?$R;", "盗贼总管");

            selection = Select(pc, "想做什么?", "", "我想成为『盗贼』!", "『盗贼』是什么样的职业?", "任务服务台", "什么也不做");

            while (selection != 4)
            {
                switch (selection)
                {
                    case 1:
                        if (pc.Str > 9)
                        {
                            Say(pc, 11000017, 131, "哈哈，您想成为『盗贼』吗?$R;" +
                                                   "$R要成为盗贼，$R;" +
                                                   "就先要经过『考验』。$R;" +
                                                   "$P…有必死的决心来接受考验吗?$R;", "盗贼总管");

                            switch (Select(pc, "接受『考验』吗?", "", "没问题", "才不要"))
                            {
                                case 1:
                                    JobBasic_03_mask.SetValue(JobBasic_03.選擇轉職為盜賊, true);

                                    Say(pc, 11000017, 131, "想办法闪避魔物，$R;" +
                                                           "毫发无伤的回到这里吧。$R;", "盗贼总管");

                                    SetHomePoint(pc, 10035000, 64, 175);

                                    Warp(pc, 10035000, 64, 175);
                                    break;

                                case 2:
                                    Say(pc, 11000017, 131, "不要浪费我的时间…，$R;" +
                                                           "考虑清楚以后再来吧。$R;", "盗贼总管");
                                    break;
                            }
                        }
                        else
                        {
                            Say(pc, 11000017, 131, "力量达到10了以后，$R;" +
                                                   "再来找我吧!$R;", "盗贼总管");
                        }
                        return;

                    case 2:
                        Say(pc, 11000017, 131, "盗贼这职业比较适合$R;" +
                                               "埃米尔或多米尼翁的体质!$R;" +
                                               "$R我会仔细解说给您听的。$R;", "盗贼总管");

                        switch (Select(pc, "还要听下去吗?", "", "我要听", "不听"))
                        {
                            case 1:
                                Say(pc, 11000017, 131, "『盗贼』是为了达成任务而不择手段的$R;" +
                                                       "冷酷战士。$R;" +
                                                       "$R即使情况对自己不利，$R;" +
                                                       "也可以把黑夜当作保护色，$R;" +
                                                       "杀敌人一个措手不及。$R;" +
                                                       "$P实力如果再高一点的话，$R;" +
                                                       "在不知不觉间，$R;" +
                                                       "就可以把敌人一一干掉。$R;" +
                                                       "$P是比较适合观察力强的人的职业喔。$R;", "盗贼总管");
                                break;

                            case 2:
                                break;
                        }
                        break;

                    case 3:
                        Say(pc, 11000017, 131, "想成为『盗贼』的话，让我给您介绍任务吧!$R;", "盗贼总管");
                        break;
                }

                selection = Select(pc, "想做什么?", "", "我想成为『盗贼』!", "『盗贼』是什么样的职业?", "任务服务台", "什么也不做");
            } 
        }

        void 盜賊轉職任務(ActorPC pc)
        {
            BitMask<JobBasic_03> JobBasic_03_mask = new BitMask<JobBasic_03>(pc.CMask["JobBasic_03"]);

            if (!JobBasic_03_mask.Test(JobBasic_03.盜賊轉職任務完成))
            {
                平安回到盜賊行會(pc);
            }

            if (JobBasic_03_mask.Test(JobBasic_03.盜賊轉職任務完成) &&
                !JobBasic_03_mask.Test(JobBasic_03.盜賊轉職成功))
            {
                申請轉職為盜賊(pc);
                return;
            }

        }

        void 平安回到盜賊行會(ActorPC pc)
        {
            BitMask<JobBasic_03> JobBasic_03_mask = new BitMask<JobBasic_03>(pc.CMask["JobBasic_03"]);

            Say(pc, 11000017, 131, "回来了。$R;" +
                                   "$P嗯…$R;" +
                                   "一路上辛苦了。$R;" +
                                   "$R盗贼的精髓就是避免战斗，您应该有深刻体会了。$R;" +
                                   "千万不要忘记喔!$R;" +
                                   "$P那您是不是要转职为『盗贼』呢?$R;", "盗贼总管");

            switch (Select(pc, "要转职为『盗贼』吗?", "", "转职为『盗贼』", "还是算了吧"))
            {
                case 1:
                    JobBasic_03_mask.SetValue(JobBasic_03.盜賊轉職任務完成, true);
                    break;

                case 2:
                    Say(pc, 11000017, 131, "再考虑看看吧!$R;", "盗贼总管");
                    break;
            }
        }

        void 申請轉職為盜賊(ActorPC pc)
        {
            BitMask<JobBasic_03> JobBasic_03_mask = new BitMask<JobBasic_03>(pc.CMask["JobBasic_03"]);

            Say(pc, 11000017, 131, "那么就给您纹上这象征『盗贼』的$R;" +
                                   "『盗贼纹章』吧!$R;", "盗贼总管");

            if (pc.Inventory.Equipments.Count == 0)
            {
                JobBasic_03_mask.SetValue(JobBasic_03.盜賊轉職成功, true);

                PlaySound(pc, 3087, false, 100, 50);
                ShowEffect(pc, 4131);
                Wait(pc, 3960);

                Say(pc, 11000017, 131, "…$R;" +
                                       "$P好棒啊，$R;" +
                                       "您身上已经烙印了漂亮的纹章。$R;" +
                                       "$R从今以后，$R;" +
                                       "您就成为代表我们的『盗贼』了。$R;", "盗贼总管");

                PlaySound(pc, 4012, false, 100, 50);
                ChangePlayerJob(pc, PC_JOB.SCOUT);

                Say(pc, 0, 0, "您已经转职为『盗贼』了!$R;", " ");

                Say(pc, 11000017, 131, "先穿上衣服后，再和我说话吧。$R;" +
                                       "有一份小礼物送给您唷!$R;" +
                                       "$R您先去整理行李后，再来找我吧。$R;", "盗贼总管");
            }
            else
            {
                Say(pc, 11000017, 131, "纹章是烙印在皮肤上的，$R;" +
                                       "先把装备脱掉吧。$R;", "盗贼总管");
            }
        }

        void 盜賊轉職完成(ActorPC pc)
        {
            BitMask<JobBasic_03> JobBasic_03_mask = new BitMask<JobBasic_03>(pc.CMask["JobBasic_03"]);

            if (pc.Inventory.Equipments.Count != 0)
            {
                JobBasic_03_mask.SetValue(JobBasic_03.已經轉職為盜賊, true);

                Say(pc, 11000017, 131, "给您『怪盗面具』，$R;" +
                                       "$R您就用心练吧。$R;", "盗贼总管");

                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, 50040200, 1);
                Say(pc, 0, 0, "得到『怪盗面具』!$R;", " ");

                LearnSkill(pc, 2042);
                Say(pc, 0, 0, "学到『隐匿』!$R;", " ");
            }
            else
            {
                Say(pc, 11000017, 131, "先穿上衣服后，再和我说话吧。$R;", "盗贼总管");
            }
        }

        void 進階轉職介紹(ActorPC pc)
        {
            BitMask<Job2X_03> mask = pc.CMask["Job2X_03"];
            //EVT1100001761
            Say(pc, 131, "如果是您的话，或许有可能成功的$R;" +
                "$R怎么样，想不想成为暗杀者？$R;");
            //EVT1100001762
            switch (Select(pc, "做什么呢?", "", "我要做『暗杀者』喔！", "『暗杀者』是怎样的职业？", "什么也不做"))
            {
                case 1:
                    if (pc.JobLevel1 > 29)
                    {
                        Say(pc, 131, "是想成为暗杀者吗？$R;" +
                            "$R先考验一下您的力量吧！$R;");
                        switch (Select(pc, "要考研您的力量吗?", "", "当然要", "算了吧。"))
                        {
                            case 1:
                                mask.SetValue(Job2X_03.刺客轉職開始, true);
                                //_4A00 = true;
                                Say(pc, 131, "哈哈哈哈！！$R;" +
                                    "为什么老是这么严肃呢？$R;" +
                                    "$R不要担心，现在就开始行动吧！$R;" +
                                    "$P现在去找四种药$R;" +
                                    "$P拿着药的人在大陆上的各地$R;" +
                                    "$P其中一名可能在海岸附近$R;" +
                                    "$P其他三名，一名在寒冷的地方$R;" +
                                    "一名在炎热的地方$R;" +
                                    "$R最后一名在西边的岛上。$R;" +
                                    "$P重要的是，就算找到拿药的人，$R;" +
                                    "但是不知道暗号的话$R还是不能拿到药的$R;" +
                                    "$P给您点提示吧$R;" +
                                    "$R提示就是『天空』$R;" +
                                    "$P呵呵，这个提示不知道是谁定的$R;" +
                                    "$P现在就出发吧！$R;");
                                break;
                        }
                        return;
                    }
                    Say(pc, 131, "想要成为暗杀者，力量是很重要的。$R;" +
                        "$R先去提升『力量』后，再来吧。$R;" +
                        "其他的以后再说啦。$R;");
                    break;
                case 2:
                    Say(pc, 131, "跟盗贼一样，这职业比较适合$R;" +
                        "埃米尔和多米尼翁的体质唷$R;" +
                        "$R我会慢慢给您讲解的…$R;");
                    switch (Select(pc, "听下去吗?", "", "我要听", "不听"))
                    {
                        case 1:
                            Say(pc, 131, "暗杀者是不达目的誓不罢休的，$R;" +
                                "是个悲壮的战士唷$R;" +
                                "$R暗杀者可以把自己隐藏在黑暗中$R;" +
                                "瞬间把敌人杀掉$R;" +
                                "$P使用特殊的道具和毒药方面$R;" +
                                "也具备卓越的才能$R;" +
                                "是个比较适合洞察力高的人的职业喔$R;");
                            //GOTO EVT1100001762
                            break;
                    }
                    break;
            }
        }
        
        void 進階轉職完成 (ActorPC pc)
        {
            BitMask<Job2X_03> mask = pc.CMask["Job2X_03"];

            if (mask.Test(Job2X_03.防禦過高) ||
                mask.Test(Job2X_03.交還暗殺者的內服藥))
            {
                switch (Select(pc, "真的要转职吗?", "", "我想成为暗杀者", "还是算了吧。"))
                {
                    case 1:
                        Say(pc, 131, "那么就给您烙印上这象征暗杀者的$R;" +
                            "『暗杀者纹章』吧$R;");
                        if (pc.Inventory.Equipments.Count == 0)
                        {
                            mask.SetValue(Job2X_03.轉職結束, true);
                            mask.SetValue(Job2X_03.刺客轉職開始, false);
                            mask.SetValue(Job2X_03.第一個問題回答正確, false);
                            mask.SetValue(Job2X_03.第二個問題回答正確, false);
                            mask.SetValue(Job2X_03.第三個問題回答正確, false);
                            mask.SetValue(Job2X_03.第四個問題回答正確, false);
                            mask.SetValue(Job2X_03.防禦過高, false);
                            mask.SetValue(Job2X_03.交還暗殺者的內服藥, false);
                            ChangePlayerJob(pc, PC_JOB.ASSASSIN);
                            pc.JEXP = 0;
                            PlaySound(pc, 3087, false, 100, 50);
                            ShowEffect(pc, 4131);
                            Wait(pc, 4000);
                            Say(pc, 131, "…$R;" +
                                "$P好棒啊，$R;" +
                                "您身上已经烙印了漂亮的纹章。$R;" +
                                "$R从今以后，$R您就成为代表我们的『暗杀者』了。$R;");
                            PlaySound(pc, 4012, false, 100, 50);
                            Say(pc, 131, "您已转职为『暗杀者』了。$R;");
                            Say(pc, 131, "现在先穿上衣服吧$R;" +
                                "$P期待您以后有所作为喔$R;");
                            return;
                        }
                        mask.SetValue(Job2X_03.防禦過高, true);
                        Say(pc, 131, "防御太高的话，就无法烙印纹章了$R;" +
                            "请换上轻便的服装后，再来吧。$R;");
                        break;
                    case 2:
                        Say(pc, 131, "您对未来感到迷茫吗？$R;" +
                            "$R先稳定心神，冷静后再来找我吧。$R;");
                        break;
                }
                return;
            }

            if (CountItem(pc, 10000309) >= 1 && 
                CountItem(pc, 10000350) >= 1 && 
                CountItem(pc, 10000351) >= 1 && 
                CountItem(pc, 10000352) >= 1)
            {
                mask.SetValue(Job2X_03.交還暗殺者的內服藥, true);
                //_4A25 = true;
                Say(pc, 131, "来吧$R;" +
                    "$R看起来平安无事啊。$R;" +
                    "$P…$R;" +
                    "$P一切顺利，真是太好了！$R;" +
                    "$R您好像拿了『暗杀者的秘药』回来，$R;" +
                    "是吧？$R;");
                TakeItem(pc, 10000309, 1);
                TakeItem(pc, 10000350, 1);
                TakeItem(pc, 10000351, 1);
                TakeItem(pc, 10000352, 1);
                Say(pc, 131, "交出『暗杀者的秘药1』$R;" +
                    "交出『暗杀者的秘药2』$R;" +
                    "交出『暗杀者的秘药3』$R;" +
                    "交出『暗杀者的秘药4』$R;");
                Say(pc, 131, "太简单了吗？$R;" +
                    "$P您在任何条件下都能生存$R;" +
                    "$R看来您有资格做暗杀者呢$R;" +
                    "$P真的要做暗杀者吗？$R;");
                //EVT1100001774
                switch (Select(pc, "真的要转职吗?", "", "我想成为暗杀者", "还是算了吧。"))
                {
                    case 1:
                        Say(pc, 131, "那么就给您烙印上这象征暗杀者的$R;" +
                            "『暗杀者纹章』吧$R;");
                        if (pc.Inventory.Equipments.Count == 0)
                        {
                            mask.SetValue(Job2X_03.轉職結束, true);
                            mask.SetValue(Job2X_03.刺客轉職開始, false);
                            mask.SetValue(Job2X_03.第一個問題回答正確, false);
                            mask.SetValue(Job2X_03.第二個問題回答正確, false);
                            mask.SetValue(Job2X_03.第三個問題回答正確, false);
                            mask.SetValue(Job2X_03.第四個問題回答正確, false);
                            mask.SetValue(Job2X_03.防禦過高, false);
                            mask.SetValue(Job2X_03.交還暗殺者的內服藥, false);
                            //_4A10 = true;
                            //_4A00 = false;
                            //_4A01 = false;
                            //_4A02 = false;
                            //_4A03 = false;
                            //_4A08 = false;
                            //_4A09 = false;
                            //_4A25 = false;
                            ChangePlayerJob(pc, PC_JOB.ASSASSIN);
                            pc.JEXP = 0;
                            //PARAM ME.JOB = 23
                            PlaySound(pc, 3087, false, 100, 50);
                            ShowEffect(pc, 4131);
                            Wait(pc, 4000);
                            Say(pc, 131, "…$R;" +
                                "$P好棒啊，$R;" +
                                "您身上已经烙印了漂亮的纹章。$R;" +
                                "$R从今以后，$R您就成为代表我们的『暗杀者』了。$R;");
                            PlaySound(pc, 4012, false, 100, 50);
                            Say(pc, 131, "您已转职为『暗杀者』了。$R;");
                            Say(pc, 131, "现在先穿上衣服吧$R;" +
                                "$P期待您以后有所作为喔$R;");
                            return;
                        }
                        mask.SetValue(Job2X_03.防禦過高, true);
                        //_4A09 = true;
                        Say(pc, 131, "防御太高的话，就无法烙印纹章了$R;" +
                            "请换上轻便的服装后，再来吧。$R;");
                        break;
                    case 2:
                        Say(pc, 131, "您对未来感到迷茫吗？$R;" +
                            "$R先稳定心神，冷静后再来找我吧。$R;");
                        break;
                }
                return;
            }
            Say(pc, 131, "咦？$R;" +
                "还没有搜集齐全呢？$R;" +
                "$R集齐前休想回来唷！$R;");
        }
    }
}

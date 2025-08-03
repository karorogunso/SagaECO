using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:白之聖堂(30120000) NPC基本信息:白之聖堂祭司(11000041) X:8 Y:7
namespace SagaScript.M30120000
{
    public class S11000041 : Event
    {
        public S11000041()
        {
            this.EventID = 11000041;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<JobBasic_07> JobBasic_07_mask = new BitMask<JobBasic_07>(pc.CMask["JobBasic_07"]);
            BitMask<Puppet_01> Puppet_01_mask = pc.CMask["Puppet_01"];    
            BitMask<Neko_01> Neko_01_cmask = pc.CMask["Neko_01"];
            BitMask<Neko_01> Neko_01_amask = pc.AMask["Neko_01"];
            BitMask<Job2X_07> mask = pc.CMask["Job2X_07"];

            Say(pc, 11000041, 131, "欢迎!$R;" +
                                   "$R白之圣堂是所有人的安息之处，$R;" +
                                   "也是祭司行会的根据地。$R;", "白之圣堂祭司");


            if (!Neko_01_amask.Test(Neko_01.桃子任務完成) &&
                Neko_01_cmask.Test(Neko_01.光之精靈對話) &&
                !Neko_01_cmask.Test(Neko_01.再次與祭祀對話))
            {
                Neko_01_cmask.SetValue(Neko_01.再次與祭祀對話, true);
                Say(pc, 131, "什么？$R用光明精灵的力量也不行吗？$R;" +
                    "$R对不起…$P还以为以光明精灵的力量可以解决呢。$R;" +
                    "$P如果她的力量不行的话$R就只有当泰达尼亚非常愤怒时$R才能把那灵魂赶走了$R;" +
                    "$P别的方法……$R;" +
                    "听说「猫灵」有附上自己喜欢的物件$R和道具上的习性$R;" +
                    "$P如果拿着能够吸引「猫灵」的道具$R說不定可以驱走她呢$R;");
                return;
            }
            if (!Neko_01_amask.Test(Neko_01.桃子任務完成) &&
                Neko_01_cmask.Test(Neko_01.與雷米阿對話) &&
                !Neko_01_cmask.Test(Neko_01.與祭祀對話))
            {
                Neko_01_cmask.SetValue(Neko_01.與祭祀對話, true);
                //_2A81 = true;
                Say(pc, 131, "啊……!$R;" +
                    "驱赶猫灵的方法呀……$R;");
                Say(pc, 131, "……$R;" +
                    "$P不是没有方法$R;" +
                    "如果是拥有光之力的人，$R就可以驱赶灵魂。$R;" +
                    "$P可是…在祭司里$R我们光之使徒是严禁这样做的。$R;" +
                    "$P虽然很可惜，但我不能够帮您喔。$R;");
                return;
            }
            

            if (JobBasic_07_mask.Test(JobBasic_07.祭司轉職成功) &&
                !JobBasic_07_mask.Test(JobBasic_07.已經轉職為祭司))
            {
                祭司轉職完成(pc);
                return;
            }

            if (pc.Job == PC_JOB.VATES && mask.Test(Job2X_07.獲得神官的烙印))//_3A88)
            {
                Say(pc, 131, pc.Name + "不是嗎？$R;" +
                    "$R哦，$R能看出您身上「神官的烙印」$R是神官的烙印呢。$R;" +
                    "$R看来您挑战成功了啊$R;" +
                    "现在成人您的能力了。$R;" +
                    "$P要转职为「神官」吗？$R;");
                int a = 0;
                while (a == 0)
                {
                    switch (Select(pc, "转职吗?", "", "转职", "不转职"))
                    {
                        case 1:
                            Say(pc, 131, "给您代表神官的「神官纹章」$R;");
                            if (pc.Inventory.Equipments.Count == 0)
                            {
                                pc.JEXP = 0;
                                mask.SetValue(Job2X_07.轉職結束, true);
                                mask.SetValue(Job2X_07.聽取說明, false);
                                ChangePlayerJob(pc, PC_JOB.DRUID);
                                PlaySound(pc, 3087, false, 100, 50);
                                ShowEffect(pc, 4131);
                                Wait(pc, 4000);
                                Say(pc, 131, "……$R;" +
                                    "恭喜您$R;" +
                                    "纹章烙印好了$R;" +
                                    "现在您就是「神官」了。$R;");
                                PlaySound(pc, 4012, false, 100, 50);
                                Say(pc, 131, "转职「神官」成功$R;");
                                return;
                            }
                            Say(pc, 131, "纹章是要烙印在皮肤上的$R;" +
                                "请把装备脱下来$R;");
                            return;
                        case 2:
                            Say(pc, 131, "啊？您说您不转职？$R;" +
                                "$R若是那样的话$R您以前的努力就白费了$R没关系吗？$R;");
                            switch (Select(pc, "不转职为「神官」了吗?", "", "不转职", "嗯…可能会转职…"))
                            {
                                case 1:
                                    mask.SetValue(Job2X_07.聽取說明, false);
                                    mask.SetValue(Job2X_07.轉職開始, false);
                                    mask.SetValue(Job2X_07.獲得神官的烙印, false);
                                    Say(pc, 131, "…知道嗎？$R;" +
                                        "$R要以祭司的身份进行修炼$R那也是一种方法啊，$R我不会太强求的。$R;");
                                    return;
                                case 2:
                                    break;
                            }
                            break;
                    }
                }
                return;
            }
            
            
            if (CountItem(pc, 10047201) >= 1)
            {
                Say(pc, 131, "注入心灵？$R;" +
                    "$P呵呵，好的$R;" +
                    "$R在「空空的心」里，$R;" +
                    "注入心灵是需要代价的$R;" +
                    "来，挑一个吧。$R;");
                if (Select(pc, "选择付出的代价是?", "", "交出生命的源泉", "交出魔力的源泉", "交出技术的源泉", "算了吧") != 4)
                {
                    Puppet_01_mask.SetValue(Puppet_01.聖堂祭司給予洋鐵的心, true);
                    //_3A24 = true;
                    TakeItem(pc, 10047201, 1);
                    GiveItem(pc, 10047200, 1);
                    Say(pc, 131, "……$R;" +
                        "$R我能够感受到$R;" +
                        "您设计为人的精神$R;" +
                        "$P这样的想法很好！！$R;" +
                        "我就帮您注入心灵吧。$R;");
                    PlaySound(pc, 4006, false, 100, 50);
                    Say(pc, 131, "得到「洋铁的心」$R;");
                    Say(pc, 131, "啊？需要代价吗？$R;" +
                        "$R嘿嘿$R;" +
                        "$P對了，没有必要担心$R;" +
                        "只是想看看您的觉悟$R;" +
                        "让您受惊了，对不起啊$R;");
                }
                return;
            }
            

            if (pc.Job == PC_JOB.NOVICE)
            {
                if (JobBasic_07_mask.Test(JobBasic_07.選擇轉職為祭司) &&
                    !JobBasic_07_mask.Test(JobBasic_07.已經轉職為祭司))
                {
                    祭司轉職任務(pc);
                    return;
                }
                else
                {
                    祭司簡介(pc);
                    return;
                }
            }
            
            if (pc.JobBasic == PC_JOB.VATES)
            {
                if (pc.Inventory.Equipments.Count == 0)
                {
                    Say(pc, 131, "您已经没穿衣服了$R;");
                    return;
                }

                Say(pc, 11000041, 131, pc.Name + "不是吗?$R;" +
                                       "$R怎么会这样?$R;", "白之圣堂祭司");
                if (mask.Test(Job2X_07.轉職結束))
                {
                    switch (Select(pc, "想做什么呢?", "", "任务服务台", "购买入国许可证", "什么也不做"))
                    {
                        case 1:
                            HandleQuest(pc, 14);
                            break;

                        case 2:
                            Say(pc, 131, "要去诺森吗？$R;");
                            OpenShopBuy(pc, 82);
                            break;

                        case 3:
                            break;
                    }
                }

                switch (Select(pc, "做什么呢?", "", "任务服务台", "购买诺森入国许可证", "转职", "什么也不做"))
                {
                    case 1:
                        HandleQuest(pc, 14);
                        break;
                    case 2:
                        Say(pc, 131, "要去诺森吗？$R;");
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

        void 祭司簡介(ActorPC pc)
        {
            BitMask<JobBasic_07> JobBasic_07_mask = new BitMask<JobBasic_07>(pc.CMask["JobBasic_07"]);

            int selection;

            Say(pc, 11000041, 131, "我是领导祭司行会的祭司总管。$R;" +
                                   "$P我们这个行会，$R;" +
                                   "一直在招募新的会员唷!$R;" +
                                   "$R想不想成为『祭司』呢?$R;", "白之圣堂祭司");

            selection = Select(pc, "想做什么?", "", "我想成为『祭司』!", "『祭司』是什么样的职业?", "任务服务台", "什么也不做");

            while (selection != 4)
            {
                switch (selection)
                {
                    case 1:
                        JobBasic_07_mask.SetValue(JobBasic_07.選擇轉職為祭司, true);

                        Say(pc, 11000041, 131, "啊! 您想成为『祭司』是吗?$R;", "白之圣堂祭司");

                        Say(pc, 11000041, 131, "在成为『祭司』之前，$R;" +
                                               "您必须对「治疗魔法」彻底的了解。$R;" +
                                               "$P「下城」里，$R;" +
                                               "有个熟悉「治疗魔法」的人。$R;" +
                                               "$P她的名字是「塞伊菈」。$R;" +
                                               "$R去见过她以后，再来吧!$R;", "白之圣堂祭司");
                        return;

                    case 2:
                        Say(pc, 11000041, 131, "泰达尼亚和多米尼翁，魔法力量较强，$R;" +
                                               "比較适合做祭司。$R;" +
                                               "$P特別是泰达尼亚!!$R;" +
                                               "他們天生就有适合「治疗魔法」的体质。$R;" +
                                               "$R祭司中，有很多泰达尼亚呢!$R;" +
                                               "$P还想听更仔细的解说吗?$R;", "白之圣堂祭司");

                        switch (Select(pc, "要听吗?", "", "听", "不听"))
                        {
                            case 1:
                                Say(pc, 11000041, 131, "『祭司』是医治人的职业。$R;" +
                                                       "$R队友或者自己受伤的时候，$R;" +
                                                       "可以迅速的复原。$R;" +
                                                       "$P队伍中，只要存在一名祭司，$R;" +
                                                       "对队伍非常有帮助唷!$R;" +
                                                       "$P但是攻击力和防御力等$R;" +
                                                       "战斗能力较弱，所以不适合前线战斗。$R;" +
                                                       "$P但是如果继续成长的话，$R;" +
                                                       "也会有一天得到$R;" +
                                                       "提高力量和速度的能力喔!$R;", "白之圣堂祭司");
                                break;
                                
                            case 2:
                                break;
                        }
                        break;

                    case 3:
                        Say(pc, 11000041, 131, "如果想在这里接获任务的話，$R;" +
                                               "就成为『祭司』吧!$R;" +
                                               "$R会给您介绍对人们有益的工作唷!!$R;", "白之圣堂祭司");
                        break;
                }

                selection = Select(pc, "想做什么?", "", "我想成为『祭司』!", "『祭司』是什么样的职业?", "任务服务台", "什么也不做");
            }
        }

        void 祭司轉職任務(ActorPC pc)
        {
            BitMask<JobBasic_07> JobBasic_07_mask = new BitMask<JobBasic_07>(pc.CMask["JobBasic_07"]);

            if (!JobBasic_07_mask.Test(JobBasic_07.祭司轉職任務完成))
            {
                治療魔法相關問題回答(pc);
            }

            if (JobBasic_07_mask.Test(JobBasic_07.祭司轉職任務完成) &&
                !JobBasic_07_mask.Test(JobBasic_07.祭司轉職成功))
            {
                申請轉職為祭司(pc);
                return;
            }

        }

        void 治療魔法相關問題回答(ActorPC pc)
        {
            BitMask<JobBasic_07> JobBasic_07_mask = new BitMask<JobBasic_07>(pc.CMask["JobBasic_07"]);

            if (JobBasic_07_mask.Test(JobBasic_07.已經從菲爾那裡聽取有關治療魔法的知識))
            {
                問題01(pc);
            }
            else
            {
                Say(pc, 11000041, 131, "「下城」里，$R;" +
                                       "有个熟悉「治疗魔法」的人。$R;" +
                                       "$P她的名字是「塞伊菈」。$R;" +
                                       "$R去见过她以后，再来吧!$R;", "白之圣堂祭司");
            }
        }

        void 問題01(ActorPC pc)
        {
            Say(pc, 11000041, 131, "您应该听完说明了吧?$R;" +
                                   "$R您对她所讲的话是怎么想的，$R;" +
                                   "请告诉我吧!$R;" +
                                   "$P您认为，拥有「治疗魔法」$R;" +
                                   "只是为了自己吗?$R;", "白之圣堂祭司");

            switch (Select(pc, "您怎么想的?", "", "希望能帮到所有人", "只要治疗我自己就行了"))
            {
                case 1:
                    問題02(pc);
                    break;

                case 2:
                    問題回答錯誤(pc);
                    return;
            }
        }

        void 問題02(ActorPC pc)
        {
            PlaySound(pc, 2040, false, 100, 50);

            Say(pc, 11000041, 131, "对了!$R;" +
                                   "$R如果欠別人人情的話，$R;" +
                                   "总是想报答的。$R;" +
                                   "$P如果一直怀着这种热情去冒险，$R;" +
                                   "作为祭司的您，会有幸福的人生唷!$R;" +
                                   "$P那么这是下一个问题。$R;" +
                                   "$R「治疗魔法」$R;" +
                                   "是利用什么精灵的力量呢?$R;", "白之圣堂祭司");

            switch (Select(pc, "是什么精灵呢?", "", "诚心精灵", "暗之精灵", "光之精灵"))
            {
                case 1:
                    問題回答錯誤(pc);
                    return;

                case 2:
                    問題回答錯誤(pc);
                    return;

                case 3:
                    問題03(pc);
                    break;
            }
        }

        void 問題03(ActorPC pc)
        {
            PlaySound(pc, 2040, false, 100, 50);

            Say(pc, 11000041, 131, "正确答案!$R;" +
                                   "「治疗魔法」是「光属性」的魔法。$R;" +
                                   "$R修炼时注意$R;" +
                                   "不要错过光之精灵跟您说的悄悄话。$R;" +
                                   "$P总有一天……$R;" +
                                   "那悄悄話，将会引导您到另一个世界。$R;" +
                                   "$P相信您会好好利用祭司的力量!$R;" +
                                   "$R现在批准您转职为祭司。$R;" +
                                   "$P最后……$R;" +
                                   "$R您可以发誓，$R;" +
                                   "以后都热心待人吗?$R;", "白之圣堂祭司");

            switch (Select(pc, "你愿意发誓吗?", "", "我愿意", "才不要"))
            {
                case 1:
                    問題回答正確(pc);
                    break;
                    
                case 2:
                    問題回答錯誤(pc);
                    return;
            }
        }

        void 問題回答正確(ActorPC pc)
        {
            BitMask<JobBasic_07> JobBasic_07_mask = new BitMask<JobBasic_07>(pc.CMask["JobBasic_07"]);

            PlaySound(pc, 2040, false, 100, 50);

            Say(pc, 11000041, 131, "以后请不要忘记那个誓言啊!$R;" +
                                   "$R真的要成为『祭司』吗?$R;", "白之聖堂祭司");

            switch (Select(pc, "要转职为『祭司』吗?", "", "转职为『祭司』", "还是算了吧"))
            {
                case 1:
                    JobBasic_07_mask.SetValue(JobBasic_07.祭司轉職任務完成, true);
                    break;
                    
                case 2:
                    return;
            }
        }

        void 問題回答錯誤(ActorPC pc)
        {
            BitMask<JobBasic_07> JobBasic_07_mask = new BitMask<JobBasic_07>(pc.CMask["JobBasic_07"]);

            JobBasic_07_mask.SetValue(JobBasic_07.已經從菲爾那裡聽取有關治療魔法的知識, false);

            PlaySound(pc, 2041, false, 100, 50);

            Say(pc, 11000041, 131, "……$R;" +
                                   "$R不好意思，$R;" +
                                   "您好像不适合做祭司呢?$R;" +
                                   "$P想知道为什么吗?$R;" +
                                   "$R那您还是再听一次「塞伊菈」的话吧!$R;" +
                                   "好吗?$R;", "白之圣堂祭司");
        }

        void 申請轉職為祭司(ActorPC pc)
        {
            BitMask<JobBasic_07> JobBasic_07_mask = new BitMask<JobBasic_07>(pc.CMask["JobBasic_07"]);

            Say(pc, 11000041, 131, "那么，$R;" +
                                   "我就给您『祭司纹章』吧!$R;", "白之圣堂祭司");

            if (pc.Inventory.Equipments.Count == 0)
            {
                JobBasic_07_mask.SetValue(JobBasic_07.祭司轉職成功, true);

                PlaySound(pc, 3087, false, 100, 50);
                ShowEffect(pc, 4131);
                Wait(pc, 3960);

                Say(pc, 11000041, 131, "……$R;" +
                                       "$P这纹章是『祭司』的象徽，$R;" +
                                       "今天开始您就是『祭司』了!$R;", "白之圣堂祭司");

                PlaySound(pc, 4012, false, 100, 50);
                ChangePlayerJob(pc, PC_JOB.VATES);
                Say(pc, 0, 0, "您已经转职为『祭司』了!$R;", " ");

                Say(pc, 11000041, 131, "以后有困难时，$R;" +
                                       "随时到白之圣堂来吧!$R;" +
                                       "$P我有礼物送给您，$R;" +
                                       "先去整理行李，然后再来吧!$R;", "白之圣堂祭司");
            }
            else
            {
                Say(pc, 11000041, 131, "纹章是烙印在皮肤上的，$R;" +
                                       "先把装备脱掉吧。$R;", "白之圣堂祭司");
            }
        }

        void 祭司轉職完成(ActorPC pc)
        {
            BitMask<JobBasic_07> JobBasic_07_mask = new BitMask<JobBasic_07>(pc.CMask["JobBasic_07"]);

            if (pc.Inventory.Equipments.Count != 0)
            {
                JobBasic_07_mask.SetValue(JobBasic_07.已經轉職為祭司, true);

                Say(pc, 11000041, 131, "这是祭司专用的帽子，$R;" +
                                       "请收下吧。$R;", "白之圣堂祭司");

                PlaySound(pc, 2040, false, 100, 50);

                GiveItem(pc, 50021300, 1);
                Say(pc, 0, 0, "得到『圣帽』!$R;", " ");

                LearnSkill(pc, 3054);
                Say(pc, 0, 0, "学到『恢复』!$R;", " ");
            }
            else
            {
                Say(pc, 11000041, 131, "请先穿上衣服。$R;", "白之圣堂祭司");
            }
        }

        void 進階轉職(ActorPC pc)
        {

            BitMask<Job2X_07> mask = pc.CMask["Job2X_07"];
            if (mask.Test(Job2X_07.轉職開始))//_3A84)
            {
                Say(pc, 131, "您现在正接受全职的训练$R;");
                return;
            }
            Say(pc, 131, "嗯。$R您要转职成为「神官」啊？$R;");
            switch (Select(pc, "要转职成为「神官」吗?", "", "转职", "不转职"))
            {
                case 1:
                    if (pc.Job == PC_JOB.DRUID)
                    {
                        Say(pc, 131, "您已经是神官了$R;");
                        return;
                    }
                    if (pc.JobLevel1 < 30)
                    {
                        Say(pc, 131, "現在还不适合您$R;" +
                            "我会再来找您$R;" +
                            "$R您还是等着吧$R;");
                        return;
                    }
                    Say(pc, 131, "……$R;" +
                        "$P您具有相当的实力呢$R;");
                    if (!mask.Test(Job2X_07.聽取說明))//_3A83)
                    {
                        Say(pc, 131, "您对神官了解多少呢？$R;" +
                            "需不需要解释一下$R有关神官的消息呢？$R;");
                        switch (Select(pc, "要听吗?", "", "听", "不听"))
                        {
                            case 1:
                                Say(pc, 131, "「神官」是比祭司高级的职业$R;" +
                                    "$R不仅借取光明精灵的力量，$R而且还能召唤光明精灵和光之场所。$R;" +
                                    "能够使出更强力的魔法$R;");
                                Say(pc, 131, "成为「神官」所学到的能力$R会在跟同伴们冒险时，有所帮助的$R;" +
                                    "$P具体来说的话，$R是一种可以借给同伴力量的技能$R;" +
                                    "$P如果能够活用『祭司』的技能的话，$R您会成为队伍里不可缺少的成员$R;");
                                mask.SetValue(Job2X_07.聽取說明, true);
                                //_3A62 = true;
                                break;
                        }
                    }

                    Say(pc, 131, "「神官」一职呢$R在祭司当中，也是特別挑选的$R;" +
                        "$P为了转职成为神官，$R不得不接受行会艰苦的训练$R;" +
                        "$R您要挑战吗？$R;");
                    switch (Select(pc, "挑战吗?", "", "挑战", "不挑战"))
                    {
                        case 1:
                            mask.SetValue(Job2X_07.轉職開始, true);
                            //_3A84 = true;
                            Say(pc, 131, "好了！$R;" +
                                "您会受到成为神官之前的磨练的。$R;" +
                                "我们的存在是要治疗人，$R您要用您的治疗能力救很多人唷!$R;" +
                                "$P因为您有「神官的烙印」的标志，$R;" +
                                "那是一种刻在身上的烙印$R;" +
                                "$P高级的治疗师，看见这标记，$R便会承认您是神官$R;" +
                                "来领取「神官的烙印」吧$R;" +
                                "$P「神官的烙印」可以说是感恩的心，$R得到您治疗的人$R会欣然的承认您是「神官」的。$R;" +
                                "$P那么希望您幸运。$R;");
                            break;
                    }
                    break;
            }
            
        }
    }
}

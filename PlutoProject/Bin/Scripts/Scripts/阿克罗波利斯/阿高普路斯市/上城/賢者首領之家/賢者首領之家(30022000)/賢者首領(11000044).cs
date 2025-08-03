using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
using SagaDB.Item;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:賢者首領之家(30022000) NPC基本信息:賢者首領(11000044) X:3 Y:1
namespace SagaScript.M30022000
{
    public class S11000044 : Event
    {
        public S11000044()
        {
            this.EventID = 11000044;

            //任務服務台相關設定
            this.questTransportSource = "来了吗?!$R;" +
                                        "你能帮我把这个东西转交给别人吗?$R;" +
                                        "$R不好意思，$R;" +
                                        "因为实在是太忙了，$R;" +
                                        "所以没时间亲自拿过去。$R;" +
                                        "$P提醒你一下，$R;" +
                                        "这是非常重要的道具，$R;" +
                                        "小心点不要把它弄坏唷!$R;";

            this.transport = "转交人是我从前的弟子。$R;" +
                                             "$R听说现在居住在「诺森」那里。$R;" +
                                             "$P她是个十分优秀的小子，$R;" +
                                             "但是说什么要继承父业，$R;" +
                                             "成为一名优秀的政治家。$R;" +
                                             "$R还真是浪费了她优秀的资质。$R;";

            this.questTransportCompleteSrc = "转交给对方了吗?$R;" +
                                             "实在是太感谢了!!$R;" +
                                             "$R报酬请到「任务服务台」领取吧!;";
        }

        public override void OnEvent(ActorPC pc)
        {                                                                    //任務：賢者首領的請求

            if (pc.Job != PC_JOB.NOVICE &&
                pc.Job != PC_JOB.SWORDMAN &&
                pc.Job != PC_JOB.FENCER &&
                pc.Job != PC_JOB.SCOUT &&
                pc.Job != PC_JOB.ARCHER &&
                pc.Job != PC_JOB.WIZARD &&
                pc.Job != PC_JOB.SHAMAN &&
                pc.Job != PC_JOB.VATES &&
                pc.Job != PC_JOB.WARLOCK &&
                pc.Job != PC_JOB.TATARABE &&
                pc.Job != PC_JOB.FARMASIST &&
                pc.Job != PC_JOB.RANGER &&
                pc.Job != PC_JOB.MERCHANT &&
                CountItem(pc, 10053800) > 0)
            {
                Say(pc, 131, "$R您好像拿著『禁书』呢$R;" +
                    "$R想转职吗？$R;");
                轉職(pc);
                return;
            }
            正常對話(pc);
        }

        void 正常對話(ActorPC pc)
        {
            BitMask<Magic_Book> Magic_Book_mask = new BitMask<Magic_Book>(pc.CMask["Magic_Book"]);

            if (!Magic_Book_mask.Test(Magic_Book.賢者首領給予知識之書) ||
                !Magic_Book_mask.Test(Magic_Book.賢者首領給予魔法之書))
            {
                賢者首領的請求(pc);
                return;
            }

            switch (Select(pc, "想要做什么事情啊?", "", "看右侧书柜", "看左侧书柜", "看书柜上面", "『秘方书』是什么?", "什么也不做"))
            {
                case 1:
                    Say(pc, 11000044, 131, "好的，自己过去看看吧!$R;" +
                                           "$R我很忙的，$R;" +
                                           "请不要再来打扰我。$R;", "贤者首领");

                    OpenShopBuy(pc, 102);
                    break;

                case 2:
                    Say(pc, 11000044, 131, "好的，自己过去看看吧!$R;" +
                                           "$R我很忙的，$R;" +
                                           "请不要再来打扰我。$R;", "贤者首领");

                    OpenShopBuy(pc, 103);
                    break;

                case 3:
                    Say(pc, 11000044, 131, "你这小子…倒懂人情世固…$R;" +
                                           "$R明白了!$R;" +
                                           "不卖你怎么可以呢?$R;" +
                                           "除非你不认同我说的话!$R;", "贤者首领");

                    OpenShopBuy(pc, 187);
                    break;

                case 4:
                    Say(pc, 11000044, 131, "呵呵…$R;" +
                                           "对『秘方书』有兴趣啊?$R;" +
                                           "$P这是记录「合成秘方」的『秘方收集本』。$R;" +
                                           "$R是按照技能类别分著的书，$R;" +
                                           "购买有兴趣的技能书也不错喔!$R;" +
                                           "$P就算没有技能，$R;" +
                                           "技能书中也记载了一些基本的秘方。$R;" +
                                           "$P刚开始只能看到很少的秘方，$R;" +
                                           "多做几次合成的话，$R;" +
                                           "能看到的秘方就会慢慢增加。$R;" +
                                           "$P啊…是啊!!$R;" +
                                           "最后甚至会出现，$R;" +
                                           "文献中没记载的隐藏秘方呢!!" +
                                           "$R那试试看也没坏处啊!$R;" +
                                           "$R努力的做合成试试看吧！$R;", "贤者首领");
                    break;

                case 5:
                    Say(pc, 11000044, 131, "干嘛? 别妨碍我休息啊!$R;" +
                                           "这副身体很累耶!$R;", "贤者首领");
                    break;
            }
        }

        void 賢者首領的請求(ActorPC pc)
        {
            BitMask<Magic_Book> Magic_Book_mask = new BitMask<Magic_Book>(pc.CMask["Magic_Book"]);                                                                      //任務：賢者首領的請求

            switch (Select(pc, "想要做什么事情啊?", "", "看右侧书柜", "看左侧书柜", "看书柜上面", "『秘方书』是什么?", "讲故事", "什么也不做"))
            {
                case 1:
                    Say(pc, 11000044, 131, "好的，自己过去看看吧!$R;" +
                                           "$R我很忙的，$R;" +
                                           "请不要再来打扰我。$R;", "贤者首领");

                    OpenShopBuy(pc, 102);
                    break;

                case 2:
                    Say(pc, 11000044, 131, "好的，自己过去看看吧!$R;" +
                                           "$R我很忙的，$R;" +
                                           "请不要再来打扰我。$R;", "贤者首领");

                    OpenShopBuy(pc, 103);
                    break;

                case 3:
                    Say(pc, 11000044, 131, "你这小子…倒懂人情世固…$R;" +
                                           "$R明白了!$R;" +
                                           "不卖你怎么可以呢?$R;" +
                                           "除非你不认同我说的话!$R;", "贤者首领");

                    OpenShopBuy(pc, 187);
                    break;

                case 4:
                    Say(pc, 11000044, 131, "呵呵…$R;" +
                                           "对『秘方书』有兴趣啊?$R;" +
                                           "$P这是记录「合成秘方」的『秘方收集本』。$R;" +
                                           "$R是按照技能类别分著的书，$R;" +
                                           "购买有兴趣的技能书也不错喔!$R;" +
                                           "$P就算没有技能，$R;" +
                                           "技能书中也记载了一些基本的秘方。$R;" +
                                           "$P刚开始只能看到很少的秘方，$R;" +
                                           "多做几次合成的话，$R;" +
                                           "能看到的秘方就会慢慢增加。$R;" +
                                           "$P啊…是啊!!$R;" +
                                           "最后甚至会出现，$R;" +
                                           "文献中没记载的隐藏秘方呢!!" +
                                           "$R那试试看也没坏处啊!$R;" +
                                           "$R努力的做合成试试看吧！$R;", "贤者首领");
                    break;

                case 5:
                    if (!Magic_Book_mask.Test(Magic_Book.賢者首領給予魔法之書) &&
                        pc.Level >= 25)
                    {
                        賢者首領給予魔法之書(pc);
                        return;
                    }

                    if (!Magic_Book_mask.Test(Magic_Book.賢者首領給予知識之書) &&
                        pc.Level >= 15)
                    {
                        賢者首領給予知識之書(pc);
                        return;
                    }

                    Say(pc, 11000044, 131, "干嘛? 别妨碍我休息啊!$R;" +
                                           "这副身体很累耶!$R;", "贤者首领");
                    break;

                case 6:
                    Say(pc, 11000044, 131, "干嘛? 别妨碍我休息啊!$R;" +
                                           "这副身体很累耶!$R;", "贤者首领");
                    break;
            }
        }

        void 賢者首領給予知識之書(ActorPC pc)
        {
            BitMask<Magic_Book> Magic_Book_mask = new BitMask<Magic_Book>(pc.CMask["Magic_Book"]);                                                                      //任務：賢者首領的請求

            Say(pc, 11000044, 131, "大事不妙!! 『纹章纸』都没了啊…$R;" +
                                   "$P怎么办?$R;" +
                                   "$P如果拿100张『纹章纸』过来给我，$R;" +
                                   "我会用我写的『知识之书』跟你交换的。$R;", "贤者首领");

            if (CountItem(pc, 10024900) >= 100)
            {
                switch (Select(pc, "想不想交换『知识之书』呢?", "", "交换", "不交换"))
                {
                    case 1:
                        Magic_Book_mask.SetValue(Magic_Book.賢者首領給予知識之書, true);

                        TakeItem(pc, 10024900, 100);
                        GiveItem(pc, 60080000, 1);
                        PlaySound(pc, 2030, false, 100, 50);
                        Say(pc, 0, 0, "得到『知识之书』!$R;", " ");

                        Say(pc, 11000044, 131, "好啊好啊…$R;" +
                                               "$R我很忙的啊! 不要再来妨碍了。$R;", "贤者首领");
                        break;

                    case 2:
                        break;
                }
            }
        }

        void 賢者首領給予魔法之書(ActorPC pc)
        {
            BitMask<Magic_Book> Magic_Book_mask = new BitMask<Magic_Book>(pc.CMask["Magic_Book"]);                                                                      //任務：賢者首領的請求

            Say(pc, 11000044, 131, "大事不妙!! 『纹章纸』都没了啊…$R;" +
                                   "$P怎么办?$R;" +
                                   "$P如果拿200张『纹章纸』过来给我，$R;" +
                                   "我会用我写的『魔法之书』跟你交换的。$R;", "贤者首领");

            if (CountItem(pc, 10024900) >= 200)
            {
                switch (Select(pc, "想不想交换『魔法之书』?", "", "交换", "不交换"))
                {
                    case 1:
                        Magic_Book_mask.SetValue(Magic_Book.賢者首領給予魔法之書, true);

                        TakeItem(pc, 10024900, 200);
                        GiveItem(pc, 60080100, 1);
                        PlaySound(pc, 2030, false, 100, 50);
                        Say(pc, 0, 0, "得到『魔法之书』!$R;", " ");

                        Say(pc, 11000044, 131, "好啊好啊…$R;" +
                                               "$R我很忙的啊! 不要再来妨碍了。$R;", "贤者首领");
                        break;

                    case 2:
                        break;
                }
            }
        }

        void 轉職(ActorPC pc)
        {
            BitMask<Technique> Technique_mask = pc.CMask["Technique"];

            switch (Select(pc, "转职吗？", "", "转职", "听听说明", "转职时的注意要点", "算了"))
            {
                case 1:
                    if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.UPPER_BODY))
                    {
                        if (pc.Inventory.Equipments[EnumEquipSlot.UPPER_BODY].ItemID == 50000063)
                        {
                            開始(pc);
                            return;
                        }
                    }
                    if (pc.Inventory.Equipments.Count == 0)
                    {
                        開始(pc);
                        return;
                    }
                    Say(pc, 131, "$R身上罩著什么东西就不能转职了。$R;" +
                        "$R先把身上的脱掉$R;");
                    if (Technique_mask.Test(Technique.給予轉職服裝))
                    {
                        Say(pc, 131, "$R啊，应该给您『转职服装』了啊。$R;" +
                            "$R换上以后再和我说话。$R;");
                        return;
                    }
                    if (CountItem(pc, 50000063) >= 1)
                    {
                        Say(pc, 131, "$R您拿著的不是『转职服装』吗？$R;" +
                            "$R换上以后再和我说话。$R;");
                        return;
                    }
                    switch (Select(pc, "脱吗？", "", "脱吧", "不要"))
                    {
                        case 1:
                            Say(pc, 131, "脱完了再和我説话$R;");
                            break;
                        case 2:
                            Say(pc, 131, "我已经是上了年纪的老人了。$R;" +
                                "$R到底有什么害羞的？$R;" +
                                "$P知道了啦，给您这个吧$R;");
                            if (CheckInventory(pc, 50000063, 1))
                            {
                                Technique_mask.SetValue(Technique.給予轉職服裝, true);
                                //_6A81 = true;
                                GiveItem(pc, 50000063, 1);
                                Say(pc, 131, "得到『转职用衣服』。$R;");
                                Say(pc, 131, "$R穿著这个的话就可以直接转职了。$R;" +
                                    "$R穿上以后再来和我説话吧。$R;");
                                return;
                            }
                            Say(pc, 131, "行李太多了，$R整理完了再来找我$R;");
                            break;
                    }
                    break;
                case 2:
                    Technique_mask.SetValue(Technique.聽取說明, true);
                    //_6A76 = true;
                    Say(pc, 131, "「职业变更」是指$R;" +
                        "使用『禁书』的能力$R将封印的职业能力唤醒$R;" +
                        "$R因为您们原来的『职业』上有$R藴藏著更多样的能力。$R;" +
                        "$P『禁书』拥有唤醒您们职业$R潜在力量的能力。$R;" +
                        "$P这世上的某个地方$R一定能找到记忆的漏斗。$R;" +
                        "$R它可以唤醒您心裡和身体裡$R的潜在能力$R;" +
                        "$P如果想恢復往日的经验的话，$R;" +
                        "就使用『记忆之沙』吧，$R;" +
                        "使用越多，职业级数的减退就越少。$R;" +
                        "$P如果想恢復现在的经验$R就选择要维持的技能吧。$R;" +
                        "$P现在该知道那本书的真正意义了吧$R;" +
                        "$R只要打开那本书就可以转职了。$R;");
                    轉職(pc);
                    break;
                case 3:
                    Say(pc, 131, "$R变更「职业」$R要注意两件事情。$R;" +
                        "$P第一，在「转职」的时候$R一定要选择预设「保存技能」$R;" +
                        "$P「保存技能」是在「转职」以后$R继续维持原有技能的「技能」$R;" +
                        "$R由「技术职业」转职到「专门职业」$R;" +
                        "或是由「技术职业」转职到「专门职业」$R都可以带技能过去$R;" +
                        "$P可以每10级职业LV才可以使用一个$R;" +
                        "$R所以，若想「职业」变更的话，$R最好是在「等级」是10倍数的时候做$R;" +
                        "$P「保存技能」可以在「职业变更窗」的$R转移技能中选取$R;" +
                        "$P不要在短时间内不停「变更职业」喔$R;" +
                        "$P还有一点要注意的$R;" +
                        "$R变更职业时，$R未达100%的「职业LV」$R经验值会失去$R;" +
                        "$P即使用「记忆之沙」也无法找回$R;" +
                        "$P刚升级时才「职业变更」的话，$R损失的经验值最少$R;");
                    轉職(pc);
                    break;
                case 4:
                    正常對話(pc);
                    break;
            }
        }

        void 開始(ActorPC pc)
        {
            /*
            if (pc.Job == PC_JOB.TRADER && !_7a17)
            {
                _7a19 = true;
                Say(pc, 131, "$R贸易使是管钱的，$R所以行会相当严格呢。$R;" +
                    "$R以我的力量也不能转职。$R;" +
                    "$P如果一定想转职的话$R就找商人总管谈谈吧。$R;");
                return;
            }
            */
            Say(pc, 131, "$R如果想恢复往日的经验的话，$R就使用『记忆之沙』吧，$R使用越多，$R职业级数的减退就越少。$R;" +
                "$R过去和现在联系在一起$R才是真正的成长呢。$R;");

            if (JobSwitch(pc))
            {
                /*
                if (!_2b45)
                {
                    _2b45 = true;
                    PlaySound(pc, 3087, false, 100, 50);
                    ShowEffect(pc, 4131);
                    Wait(pc, 4000);
                    PlaySound(pc, 4012, false, 100, 50);
                    Say(pc, 131, "转职$R;");
                    Say(pc, 131, "下次开始$R就主动準备『禁书』吧。$R;");
                    return;
                }
                */
                PlaySound(pc, 3087, false, 100, 50);
                ShowEffect(pc, 4131);
                Wait(pc, 4000);
                PlaySound(pc, 4012, false, 100, 50);
                Say(pc, 131, "给了『禁书』。$R;");
                Say(pc, 131, "变更了职业。$R;");
                TakeItem(pc, 10053800, 1);
            }
        }
    }
}
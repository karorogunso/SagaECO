using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10034000
{
    public class S11000346 : Event
    {
        public S11000346()
        {
            this.EventID = 11000346;
            this.leastQuestPoint = 1;
            this.notEnoughQuestPoint = "想要考试的话，先完成其他的任务$R;" +
    "需要剩下的任务点数『1』$R;";
            this.gotNormalQuest = "生活在那边洞里地下5层的『木鱼』$R;" +
    "是水属性的魔物$R;" +
    "所以火焰之箭是最有效果的!$R;" +
    "那!安全归来吧!$R;";
            this.questFailed = "失败了?!真是遗憾…$R;" +
    "那下次再挑战吧!$R;";
            this.questCompleted = "呀呼~哈哈哈哈!!$R;" +
    "厉害!$R;" +
    "帅气的通过了磨练!$R;" +
    "$R那发一下奖赏$R;" +
    "把报酬给弓手总管，就会成为『猎人』$R;";
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<DHAFlags> mask = new BitMask<DHAFlags>(pc.CMask["DHA"]);
            BitMask<Job2X_04> Job2X_04_mask = pc.CMask["Job2X_04"];

            if (!mask.Test(DHAFlags.帕美拉小姐第一次對話))
            {
                mask.SetValue(DHAFlags.帕美拉小姐第一次對話, true);
                Say(pc, 131, "噢呵呵呵呵!!$R;" +
                    "我是玫瑰猎人帕美拉小姐$R;");
                return;
            }

            if (!mask.Test(DHAFlags.获得宠物) && pc.JobBasic == PC_JOB.ARCHER)
            {
                獲得寵物(pc);
                return;
            }

            if (Job2X_04_mask.Test(Job2X_04.進階轉職開始) && !Job2X_04_mask.Test(Job2X_04.進階轉職結束))
            {
                進階轉職(pc);
                return;
            }

            Say(pc, 131, "噢呵呵呵呵!!$R;" +
                "我是玫瑰猎人帕美拉小姐$R;");

        }

        void 獲得寵物(ActorPC pc)
        {
            BitMask<DHAFlags> mask = new BitMask<DHAFlags>(pc.CMask["DHA"]);

            Say(pc, 131, "不知什么地方跟平常不一样$R;");
            switch (Select(pc, "怎么做呢？", "", "先观察一下", "跟平常一样搭话"))
            {
                case 1:
                    Say(pc, 131, "呵呵，容易寂寞啊♪$R;" +
                        "好啊好啊♪$R;" +
                        "$R快点长大要成为大人啊~?$R;" +
                        "阿~好可爱啊!!$R;");
                    switch (Select(pc, "怎么做呢？", "", "漠然置之", "干什么"))
                    {
                        case 1:
                            break;

                        case 2:
                            ShowEffect(pc, 11000346, 4501);
                            Say(pc, 376, "啊!$R;" +
                                "$R你们!去一边待着!$R;");
                            Say(pc, 131, "……$R;" +
                                "$P啊…哈哈…$R;" +
                                "有什么事？$R;");
                            switch (Select(pc, "怎么做呢？", "", "藏了什么?", "我看到的都会保密的"))
                            {
                                case 1:
                                    Say(pc, 131, "什么都不是$R;" +
                                        "没什么事情的话去别的地方吧$R;");
                                    break;

                                case 2:
                                    Say(pc, 131, "那…那个!稍等!$R;" +
                                        "$R好像误会了什么…什么都没有啊$R;");

                                    Select(pc, "怎么做呢？", "", "只是笑一笑", "看到了另外一面啊");

                                    Say(pc, 131, "那…那是…听一下啊!$R;" +
                                        "$R我只是在找能成为$R;" +
                                        "这些宠物主人的人啊$R;" +
                                        "$P跟妈妈分开了啊，很可怜的$R;" +
                                        "$R看到在草丛里哭泣所以我在保护啊$R;" +
                                        "作为冒险家不是应该的吗$R;" +
                                        "$P阿!对了!$R;" +
                                        "$R你想成为这些家伙们的主人吗?$R;");

                                    switch (Select(pc, "怎么做呢？", "", "成为洛克鸟蛋的主人", "成为小白狼的主人"))
                                    {
                                        case 1:
                                            mask.SetValue(DHAFlags.获得宠物, true);
                                            GiveItem(pc, 10050800, 1);
                                            Say(pc, 131, "……$R;" +
                                                "$P孩子一定要幸福啊$R;" +
                                                "万一有事情，什么时候回来都可以…$R;" +
                                                "$P知道了??$R;" +
                                                "$R若是造成不幸的话$R;" +
                                                "我绝对不会原谅的!$R;");
                                            Say(pc, 131, "成了『洛克鸟』的主人!$R;");
                                            break;

                                        case 2:
                                            mask.SetValue(DHAFlags.获得宠物, true);
                                            GiveItem(pc, 10049900, 1);
                                            Say(pc, 131, "……$R;" +
                                                "$P孩子一定要幸福啊$R;" +
                                                "万一有事情，什么时候回来都可以…$R;" +
                                                "$P知道了??$R;" +
                                                "$R若是造成不幸的话$R;" +
                                                "我绝对不会原谅的!$R;");
                                            Say(pc, 131, "成了『小白狼』的主人!$R;");
                                            break;
                                    }
                                    break;
                            }
                            break;
                    }
                    break;

                case 2:
                    Say(pc, 131, "噢呵呵呵呵!!$R;" +
                        "我是玫瑰猎人帕美拉小姐$R;");
                    break;
            }
        }

        void 進階轉職(ActorPC pc)
        {
            BitMask<Job2X_04> Job2X_04_mask = pc.CMask["Job2X_04"];

            if (Job2X_04_mask.Test(Job2X_04.進階轉職開始) && pc.Job == PC_JOB.ARCHER)
            {

                if (CountItem(pc, 10020751) >= 1)
                {
                    Say(pc, 131, "请去弓手总管那里。$R;");
                    return;
                }

                if (Job2X_04_mask.Test(Job2X_04.試煉開始))//_3a58)
                {
                    if (pc.Quest != null)
                    {
                        if (pc.Quest.Status == SagaDB.Quests.QuestStatus.COMPLETED)
                        {
                            HandleQuest(pc, 24);
                            return;
                        }
                    }


                    switch (Select(pc, "想要考试吗?", "", "稍等!", "是的!想要考试"))
                    {
                        case 1:
                            break;
                        case 2:
                            HandleQuest(pc, 24);
                            break;
                    }
                    return;
                }

                if (Job2X_04_mask.Test(Job2X_04.獵人的試煉))//_3a57)
                {
                    Say(pc, 131, "得到猎人的试炼了吗？$R;");
                    switch (Select(pc, "准备好就开始吧?", "", "稍等!", "是的!请开始吧!"))
                    {
                        case 1:
                            break;

                        case 2:
                            if (pc.Quest != null)
                            {
                                Say(pc, 131, "想要考试的话，其他的任务结束$R;" +
                                    "再过来$R;");
                                return;
                            }

                            HandleQuest(pc, 24);
                            if (pc.Quest != null)
                            {
                                if (pc.Quest.ID == 10031100)
                                {
                                    Job2X_04_mask.SetValue(Job2X_04.試煉開始, true);
                                }
                            }
                            break;
                    }
                    return;
                }

                if (Job2X_04_mask.Test(Job2X_04.收集火焰之箭))
                {
                    if (Job2X_04_mask.Test(Job2X_04.收集火焰之箭) && CountItem(pc, 10026500) >= 100)
                    {
                        Job2X_04_mask.SetValue(Job2X_04.獵人的試煉, true);
                        //_3a57 = true;
                        Say(pc, 131, "拿了100個來嗎？$R;" +
                            "那么数数看吧!$R;" +
                            "$P一，二，三，四，$R五，六，七，八$R;" +
                            "九，十，十一，十二，$R十三，十四，$R十五，十六$R;" +
                            "$P十七，十八，$R十九，二十$R;" +
                            "二十一，二十二，$R二十三，二十四$R;" +
                            "$P二十五，二十六，$R二十七，二十八$R;" +
                            "二十九，三十，$R三十一，三十二$R;" +
                            "$P三十三，三十四，$R三十五，三十六$R;" +
                            "三十七，三十八，$R三十九，四十$R;" +
                            "$P四十一，四十二，$R四十三，四十四$R;" +
                            "四十五，四十六，$R四十七，四十八$R;" +
                            "$P四十九，五十，$R五十一，五十二$R;" +
                            "五十三，五十四，$R五十五，五十六$R;" +
                            "$P五十七，五十八，$R五十九，六十$R;" +
                            "六十一，六十二，$R六十三，六十四$R;" +
                            "$P六十五，六十六，$R六十七，六十八$R;" +
                            "六十九，七十，$R七十一，七十二$R;" +
                            "$P七十三，七十四，$R七十五，七十六$R;" +
                            "七十七，七十八，$R七十九，八十$R;" +
                            "$P八十一，八十二，$R八十三，八十四$R;" +
                            "八十五，八十六，$R八十七，八十八$R;" +
                            "$P八十九，九十，$R九十一，九十二$R;" +
                            "九十三，九十四，$R九十五，九十六$R;" +
                            "$P九十七$R;" +
                            "九十八$R;" +
                            "九十九…$R;" +
                            "$P一百…$R;" +
                            "嗯…一百个没有错$R;" +
                            "$R呵呵…我不会马虎的$R;");
                        Say(pc, 131, "下一样需要的是『治愈药水』$R;" +
                            "但是数数也是麻烦的$R;" +
                            "以后亲自准备看看吧$R;" +
                            "$P不知道该怎么做?$R;" +
                            "真是…为什么收集了火焰之箭$R;" +
                            "都不知道吗?$R;" +
                            "$R为了用弓箭把魔物击退才收集的!$R;" +
                            "$P下次会接受使用属性弓箭$R;" +
                            "攻击的训练!$R;" +
                            "$R攻击魔物属性弱点的话$R;" +
                            "可以带来比平时更大的伤害的$R;" +
                            "$P把所有属性的弓箭都带着$R;" +
                            "根据魔物换各种属性$R;" +
                            "最擅长的就是猎人啊!$R;" +
                            "$R哈哈哈哈!$R;" +
                            "$P为了接受训练，准备好后再来吧$R;" +
                            "$R会在这里等的!$R;");
                        return;
                    }
                    Say(pc, 131, "首先开始进行$R;" +
                        "作为弓手基本的收集弓箭!$R;" +
                        "$R收集『火焰之箭』100个来吧!$R;");
                    switch (Select(pc, "知道入手方法吗?", "", "当然", "不知道"))
                    {
                        case 1:
                            break;
                        case 2:
                            Say(pc, 131, "哦哈哈哈!!那就说一下!$R;" +
                                "$R火焰之箭$R只要给沙漠的『火焰精灵』$R;" +
                                "带『火焰召唤石』和『箭』过去的话$R;" +
                                "他会帮忙制作的$R;");
                            break;
                    }
                    return;
                }
                Say(pc, 131, "噢哈哈哈!!$R;" +
                    "我是玫瑰猎人帕美拉小姐$R;" +
                    "你就是想成为猎人的人？$R;" +
                    pc.Name + "是吧?!$R;" +
                    "$P我是猎人转职的负责人$R;" +
                    "那就进行转职测试了!$R;" +
                    "做好心理准备了吗?$R;");
                switch (Select(pc, "做好心理准备了吗?", "", "稍等!", "是的!请开始"))
                {
                    case 1:
                        break;
                    case 2:
                        if (Job2X_04_mask.Test(Job2X_04.收集火焰之箭) && CountItem(pc, 10026500) >= 100)
                        {
                            Job2X_04_mask.SetValue(Job2X_04.獵人的試煉, true);
                            //_3a57 = true;
                            Say(pc, 131, "拿了100個來嗎？$R;" +
                                "那么数数看吧!$R;" +
                                "$P一，二，三，四，$R五，六，七，八$R;" +
                                "九，十，十一，十二，$R十三，十四，$R十五，十六$R;" +
                                "$P十七，十八，$R十九，二十$R;" +
                                "二十一，二十二，$R二十三，二十四$R;" +
                                "$P二十五，二十六，$R二十七，二十八$R;" +
                                "二十九，三十，$R三十一，三十二$R;" +
                                "$P三十三，三十四，$R三十五，三十六$R;" +
                                "三十七，三十八，$R三十九，四十$R;" +
                                "$P四十一，四十二，$R四十三，四十四$R;" +
                                "四十五，四十六，$R四十七，四十八$R;" +
                                "$P四十九，五十，$R五十一，五十二$R;" +
                                "五十三，五十四，$R五十五，五十六$R;" +
                                "$P五十七，五十八，$R五十九，六十$R;" +
                                "六十一，六十二，$R六十三，六十四$R;" +
                                "$P六十五，六十六，$R六十七，六十八$R;" +
                                "六十九，七十，$R七十一，七十二$R;" +
                                "$P七十三，七十四，$R七十五，七十六$R;" +
                                "七十七，七十八，$R七十九，八十$R;" +
                                "$P八十一，八十二，$R八十三，八十四$R;" +
                                "八十五，八十六，$R八十七，八十八$R;" +
                                "$P八十九，九十，$R九十一，九十二$R;" +
                                "九十三，九十四，$R九十五，九十六$R;" +
                                "$P九十七$R;" +
                                "九十八$R;" +
                                "九十九…$R;" +
                                "$P一百…$R;" +
                                "嗯…一百个没有错$R;" +
                                "$R呵呵…我不会马虎的$R;");
                            Say(pc, 131, "下一样需要的是『治愈药水』$R;" +
                                "但是数数也是麻烦的$R;" +
                                "以后亲自准备看看吧$R;" +
                                "$P不知道该怎么做?$R;" +
                                "真是…为什么收集了火焰之箭$R;" +
                                "都不知道吗?$R;" +
                                "$R为了用弓箭把魔物击退才收集的!$R;" +
                                "$P下次会接受使用属性弓箭$R;" +
                                "攻击的训练!$R;" +
                                "$R攻击魔物属性弱点的话$R;" +
                                "可以带来比平时更大的伤害的$R;" +
                                "$P把所有属性的弓箭都带着$R;" +
                                "根据魔物换各种属性$R;" +
                                "最擅长的就是猎人啊!$R;" +
                                "$R哈哈哈哈!$R;" +
                                "$P为了接受训练，准备好后再来吧$R;" +
                                "$R会在这里等的!$R;");
                            return;
                        }
                        Job2X_04_mask.SetValue(Job2X_04.收集火焰之箭, true);
                        //_3a56 = true;
                        Say(pc, 131, "首先开始进行$R;" +
                            "作为弓手基本的收集弓箭!$R;" +
                            "$R收集『火焰之箭』100个来吧!$R;");
                        switch (Select(pc, "知道入手方法吗?", "", "当然", "不知道"))
                        {
                            case 1:
                                break;
                            case 2:
                                Say(pc, 131, "哦哈哈哈!!那就请教一下!$R;" +
                                    "$R火焰之箭$R只要给沙漠的『火焰精灵』$R;" +
                                    "带『火焰召唤石』和『箭』过去的话$R;" +
                                    "他会帮忙制作的$R;");
                                break;
                        }
                        break;
                }
                return;
            }
            Say(pc, 131, "噢呵呵呵呵!!$R;" +
                "我是玫瑰猎人帕美拉小姐$R;");
        }
    }
}

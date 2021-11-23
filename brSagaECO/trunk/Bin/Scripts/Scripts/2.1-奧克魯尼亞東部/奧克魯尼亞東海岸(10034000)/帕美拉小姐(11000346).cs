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
            this.notEnoughQuestPoint = "想要考試的話，先完成其他的任務$R;" +
    "需要剩下的任務點數『1』$R;";
            this.gotNormalQuest = "生活在那邊洞裡地下5層的『木魚』$R;" +
    "是水屬性的魔物$R;" +
    "所以火焰之箭是最有效果的!$R;" +
    "那!安全歸來吧!$R;";
            this.questFailed = "失敗了?!真是遺憾…$R;" +
    "那下次再挑戰吧!$R;";
            this.questCompleted = "呀呼~哈哈哈哈!!$R;" +
    "厲害!$R;" +
    "帥氣的通過了磨練!$R;" +
    "$R那發一下獎賞$R;" +
    "把報酬給弓手總管，就會成爲『獵人』$R;";
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<DHAFlags> mask = new BitMask<DHAFlags>(pc.CMask["DHA"]);
            BitMask<Job2X_04> Job2X_04_mask = pc.CMask["Job2X_04"];

            if (!mask.Test(DHAFlags.帕美拉小姐第一次對話))
            {
                mask.SetValue(DHAFlags.帕美拉小姐第一次對話, true);
                Say(pc, 131, "噢呵呵呵呵!!$R;" +
                    "我是玫瑰獵人帕美拉小姐$R;");
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
                "我是玫瑰獵人帕美拉小姐$R;");

        }

        void 獲得寵物(ActorPC pc)
        {
            BitMask<DHAFlags> mask = new BitMask<DHAFlags>(pc.CMask["DHA"]);

            Say(pc, 131, "不知什麽地方跟平常不一樣$R;");
            switch (Select(pc, "怎麼做呢？", "", "先觀察一下", "跟平常一樣搭話"))
            {
                case 1:
                    Say(pc, 131, "呵呵，容易寂寞啊♪$R;" +
                        "好啊好啊♪$R;" +
                        "$R快點長大要成爲大人啊~?$R;" +
                        "阿~好可愛啊!!$R;");
                    switch (Select(pc, "怎麼做呢？", "", "漠然置之", "幹什麽"))
                    {
                        case 1:
                            break;

                        case 2:
                            ShowEffect(pc, 11000346, 4501);
                            Say(pc, 376, "啊!$R;" +
                                "$R你們!去一邊待著!$R;");
                            Say(pc, 131, "……$R;" +
                                "$P啊…哈哈…$R;" +
                                "有什麼事？$R;");
                            switch (Select(pc, "怎麼做呢？", "", "藏了什麽?", "我看到的都會保密的"))
                            {
                                case 1:
                                    Say(pc, 131, "什麽都不是$R;" +
                                        "沒什麽事情的話去別的地方吧$R;");
                                    break;

                                case 2:
                                    Say(pc, 131, "那…那個!稍等!$R;" +
                                        "$R好像誤會了什麽…什麽都沒有啊$R;");

                                    Select(pc, "怎麼做呢？", "", "只是笑一笑", "看到了另外一面啊");

                                    Say(pc, 131, "那…那是…聽一下啊!$R;" +
                                        "$R我只是在找能成爲$R;" +
                                        "這些寵物主人的人啊$R;" +
                                        "$P跟媽媽分開了阿，很可憐的$R;" +
                                        "$R看到在草叢裡哭泣所以我在保護阿$R;" +
                                        "作爲冒險家不是應該的嗎$R;" +
                                        "$P阿!對了!$R;" +
                                        "$R你想成爲這些家伙們的主人嗎?$R;");

                                    switch (Select(pc, "怎麼做呢？", "", "成爲洛克鳥蛋的主人", "成爲小白狼的主人"))
                                    {
                                        case 1:
                                            mask.SetValue(DHAFlags.获得宠物, true);
                                            GiveItem(pc, 10050800, 1);
                                            Say(pc, 131, "……$R;" +
                                                "$P孩子一定要幸福啊$R;" +
                                                "萬一有事情，什麽時候回來都可以…$R;" +
                                                "$P知道了??$R;" +
                                                "$R把這造成不幸的話$R;" +
                                                "我絕對不會原諒的!$R;");
                                            Say(pc, 131, "成了『洛克鳥蛋』的主人!$R;");
                                            break;

                                        case 2:
                                            mask.SetValue(DHAFlags.获得宠物, true);
                                            GiveItem(pc, 10049900, 1);
                                            Say(pc, 131, "……$R;" +
                                                "$P孩子一定要幸福啊$R;" +
                                                "萬一有事情，什麽時候回來都可以…$R;" +
                                                "$P知道了??$R;" +
                                                "$R把這造成不幸的話$R;" +
                                                "我絕對不會原諒的!$R;");
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
                        "我是玫瑰獵人帕美拉小姐$R;");
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
                    Say(pc, 131, "請去弓手總管那裡。$R;");
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


                    switch (Select(pc, "想要考試嗎?", "", "稍等!", "是的!想要考試"))
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
                    Say(pc, 131, "得到獵人的試煉了嗎？$R;");
                    switch (Select(pc, "準備好就開始吧?", "", "稍等!", "是的!請開始吧!"))
                    {
                        case 1:
                            break;

                        case 2:
                            if (pc.Quest != null)
                            {
                                Say(pc, 131, "想要考試的話，其他的任務結束$R;" +
                                    "再過來$R;");
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
                            "那麼數數看吧!$R;" +
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
                            "嗯…一百個沒有錯$R;" +
                            "$R呵呵…我不會馬虎的$R;");
                        Say(pc, 131, "下一樣需要的是『治癒藥水』$R;" +
                            "但是數數也是麻煩的$R;" +
                            "以後親自準備看看吧$R;" +
                            "$P不知道該怎麽做?$R;" +
                            "真是…爲什麽收集了火焰之箭$R;" +
                            "都不知道嗎?$R;" +
                            "$R爲了用弓箭把魔物擊退才收集的!$R;" +
                            "$P下次會接受使用屬性弓箭$R;" +
                            "攻擊的訓練!$R;" +
                            "$R攻擊魔物屬性弱點的話$R;" +
                            "可以帶來比平時更大的傷害的$R;" +
                            "$P把所有屬性的弓箭都帶着$R;" +
                            "根據魔物換著各種屬性$R;" +
                            "最擅長戰鬥的就是獵人阿!$R;" +
                            "$R哈哈哈哈!$R;" +
                            "$P爲了接受訓練，準備好後再來吧$R;" +
                            "$R會在這裡等的!$R;");
                        return;
                    }
                    Say(pc, 131, "首先開始進行$R;" +
                        "作爲弓手基本的收集弓箭!$R;" +
                        "$R收集『火焰之箭』100個來吧!$R;");
                    switch (Select(pc, "知道入手方法嗎?", "", "當然", "不知道"))
                    {
                        case 1:
                            break;
                        case 2:
                            Say(pc, 131, "哦哈哈哈!!那就請教一下!$R;" +
                                "$R火焰之箭$R只要給沙漠的『火焰精靈』$R;" +
                                "帶『火焰召喚石』和『箭』過去的話$R;" +
                                "他會幫製作的$R;");
                            break;
                    }
                    return;
                }
                Say(pc, 131, "噢哈哈哈!!$R;" +
                    "我是玫瑰獵人帕美拉小姐$R;" +
                    "你就是想成為獵人的人？$R;" +
                    pc.Name + "是吧?!$R;" +
                    "$P我是獵人轉職的負責人$R;" +
                    "那就進行轉職測試了!$R;" +
                    "做好心理準備了嗎?$R;");
                switch (Select(pc, "做好心裡準備了嗎?", "", "稍等!", "是的!請開始"))
                {
                    case 1:
                        break;
                    case 2:
                        if (Job2X_04_mask.Test(Job2X_04.收集火焰之箭) && CountItem(pc, 10026500) >= 100)
                        {
                            Job2X_04_mask.SetValue(Job2X_04.獵人的試煉, true);
                            //_3a57 = true;
                            Say(pc, 131, "拿了100個來嗎？$R;" +
                                "那麼數數看吧!$R;" +
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
                                "嗯…一百個沒有錯$R;" +
                                "$R呵呵…我不會馬虎的$R;");
                            Say(pc, 131, "下一樣需要的是『治癒藥水』$R;" +
                                "但是數數也是麻煩的$R;" +
                                "以後親自準備看看吧$R;" +
                                "$P不知道該怎麽做?$R;" +
                                "真是…爲什麽收集了火焰之箭$R;" +
                                "都不知道嗎?$R;" +
                                "$R爲了用弓箭把魔物擊退才收集的!$R;" +
                                "$P下次會接受使用屬性弓箭$R;" +
                                "攻擊的訓練!$R;" +
                                "$R攻擊魔物屬性弱點的話$R;" +
                                "可以帶來比平時更大的傷害的$R;" +
                                "$P把所有屬性的弓箭都帶着$R;" +
                                "根據魔物換著各種屬性$R;" +
                                "最擅長戰鬥的就是獵人阿!$R;" +
                                "$R哈哈哈哈!$R;" +
                                "$P爲了接受訓練，準備好後再來吧$R;" +
                                "$R會在這裡等的!$R;");
                            return;
                        }
                        Job2X_04_mask.SetValue(Job2X_04.收集火焰之箭, true);
                        //_3a56 = true;
                        Say(pc, 131, "首先開始進行$R;" +
                            "作爲弓手基本的收集弓箭!$R;" +
                            "$R收集『火焰之箭』100個來吧!$R;");
                        switch (Select(pc, "知道入手方法嗎?", "", "當然", "不知道"))
                        {
                            case 1:
                                break;
                            case 2:
                                Say(pc, 131, "哦哈哈哈!!那就請教一下!$R;" +
                                    "$R火焰之箭$R只要給沙漠的『火焰精靈』$R;" +
                                    "帶『火焰召喚石』和『箭』過去的話$R;" +
                                    "他會幫製作的$R;");
                                break;
                        }
                        break;
                }
                return;
            }
            Say(pc, 131, "噢呵呵呵呵!!$R;" +
                "我是玫瑰獵人帕美拉小姐$R;");
        }
    }
}

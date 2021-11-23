using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30081000
{
    public class S11000298 : Event
    {
        public S11000298()
        {
            this.EventID = 11000298;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<AYEFlags> mask = pc.CMask["AYE"];

            if (pc.Job == PC_JOB.TATARABE
              ||pc.Job == PC_JOB.BLACKSMITH
              ||pc.Job == PC_JOB.MACHINERY) 
            {
                if (CountItem(pc, 10018900) > 0)
                {
                    Say(pc, 131, "您拿來的凱特靈炮馬上就要壞掉了。$R;" +
                        "勸您分解以後重組吧。$R;" +
                        "還有，給您一個特別服務阿，$R;" +
                        "可以改造您的凱特靈炮，$R提升它的威力呢。$R;" +
                        "$P要裝備和改造您的凱特靈炮嗎?$R;");
                    switch (Select(pc, "怎麼辦呢？", "", "好", "算了"))
                    {
                        case 1:
                            Say(pc, 131, "謝謝啦$R;" +
                                "$R分解裝備和改造，$R費用一共10萬金幣唷$R;");
                            switch (Select(pc, "支付10萬金幣裝備改造嗎？", "", "做", "不做"))
                            {
                                case 1:
                                    if (pc.Gold > 99999)
                                    {
                                        PlaySound(pc, 2030, false, 100, 50);
                                        Say(pc, 131, "寵物隨著成長，$R外貌也會不同喔$R;" +
                                            "$R請注意$R;");
                                        switch (Select(pc, "怎麼辦呢？", "", "做", "算了"))
                                        {
                                            case 1:
                                                Say(pc, 131, "謝謝$R;" +
                                                    "$R那就開始了$R;");
                                                PlaySound(pc, 4001, false, 100, 50);
                                                //FADE OUT BLACK
                                                Wait(pc, 5000);
                                                //FADE IN
                                                Wait(pc, 1000);
                                                pc.Gold -= 100000;
                                                //PETCHANGE TRANSFORM 10018901 ALL
                                                Say(pc, 131, "完成了。$R;" +
                                                    "$R好好感受一下新凱特靈炮的威力吧！$R;");
                                                break;
                                            case 2:
                                                Say(pc, 131, "是嗎？可惜啊$R;");
                                                break;
                                        }
                                        return;
                                    }
                                    Say(pc, 131, "錢好像不夠啊$R;");
                                    break;
                                case 2:
                                    Say(pc, 131, "是嗎？可惜啊$R;");
                                    break;
                            }
                            break;
                        case 2:
                            Say(pc, 131, "是嗎？可惜啊$R;");
                            break;
                    }
                    return;
                }
            }

            if (mask.Test(AYEFlags.與老闆對話))//_2a50)
            {
                switch (Select(pc, "歡迎光臨大工廠！", "", "買東西", "買凱特靈炮", "洽談", "什麼也不做"))
                {
                    case 1:
                        OpenShopBuy(pc, 98);
                        Say(pc, 131, "感謝！$R;");
                        break;
                    case 2:
                        凱特靈炮(pc);
                        break;
                    case 3:
                        Say(pc, 131, "我們公司會拜託$R;" +
                            "有名的冒險者做事唷。$R;" +
                            "$R不好意思，請問您是……?$R;" +
                            "$P做這些事，或是擊退魔物，$R;" +
                            "是不是還沒有什麼經驗呢??$R;");
                        break;
                }
                return;
            }

            switch (Select(pc, "歡迎光臨大工廠！", "", "任務服務台", "買東西", "買凱特靈炮", "什麼也不做"))
            {
                case 1:
                    HandleQuest(pc, 21);
                    break;
                case 2:
                    OpenShopBuy(pc, 98);
                    Say(pc, 131, "感謝！$R;");
                    break;
                case 3:
                    凱特靈炮(pc);
                    break;
            }
        }

        void 凱特靈炮(ActorPC pc)
        {
            switch (Select(pc, "有許可証嗎？", "", "拿出許可証", "許可証是什麼?"))
            {
                case 1:
                    if (CountItem(pc, 10048000) >= 1)
                    {
                        Say(pc, 131, "確認了。$R;" +
                            "凱特靈炮一個30萬金幣$R;");
                        switch (Select(pc, "30萬金幣，買嗎 ？", "", "買", "太貴了"))
                        {
                            case 1:
                                if (pc.Gold > 299999)
                                {
                                    if (CheckInventory(pc, 10018900, 1))
                                    {
                                        GiveItem(pc, 10018900, 1);
                                        TakeItem(pc, 10048000, 1);
                                        pc.Gold -= 300000;
                                        Say(pc, 131, "謝謝唷，$R;" +
                                            "很重的，小心啊$R;");
                                        return;
                                    }
                                    Say(pc, 131, "行李好像太多了，整理後再來吧$R;");
                                    return;
                                }
                                Say(pc, 131, "錢不夠啊$R;");
                                break;
                            case 2:
                                Say(pc, 131, "嗯，有點貴呢。$R;" +
                                    "這個是秘密，別告訴人$R;" +
                                    "我可以給您開其他條件喔$R;" +
                                    "$P由現在開始，委託您的事情中$R;" +
                                    "只要您成功完成最少一件$R;" +
                                    "就送您一個凱特靈炮作報酬唷$R;" +
                                    "怎麼樣？這個條件可以吧?$R;");
                                if (pc.Quest != null)
                                {
                                    Say(pc, 131, "先完成現在進行的任務$R;" +
                                        "然後再來吧$R;");
                                    return;
                                }
                                Say(pc, 131, "這個任務$R;" +
                                    "消耗任務點數『3』$R;");
                                if (pc.QuestRemaining < 3)
                                {
                                    Say(pc, 131, "任務點數，累積超過『3』的話，$R;" +
                                        "再來找我吧。$R;");
                                    return;
                                }
                                break;
                        }
                        return;
                    }
                    Say(pc, 131, "好像沒有許可証吧$R;" +
                        "$R對不起，沒有許可証的話$R;" +
                        "不能把凱特靈炮賣給您的。$R;");
                    break;
                case 2:
                    Say(pc, 131, "許可証是『合同大廈』發行的$R;" +
                        "『凱特靈銷售許可証』。$R;" +
                        "$R沒有許可証的話$R;" +
                        "不能把凱特靈炮賣給您的。$R;" +
                        "$P合同大廈是上面很大的建築。$R;" +
                        "$R去看看嗎?$R;");
                    break;
            }
        }
    }
}
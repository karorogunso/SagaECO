using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M10042000
{
    public class S11000539 : Event
    {
        public S11000539()
        {
            this.EventID = 11000539;

            this.leastQuestPoint = 1;
            this.questFailed = "任務失敗$R;" +
    "下次再挑戰吧$R;";
            this.notEnoughQuestPoint = "現在沒有什麼要您幫忙唷$R;" +
    "$R下次再來吧$R;";
            this.gotNormalQuest = "在這裡接到的任務$R;" +
    "是「隊伍」用的擊退任務喔$R;" +
    "$R隊伍中如果有人接受一樣任務的話$R;" +
    "可以共享擊退魔物數量$R;" +
    "$P一起合作的話$R;" +
    "執行任務會比較容易唷$R;";
            this.questCompleted = "辛苦了$R;" +
    "$R任務成功$R;" +
    "請領取報酬吧。$R;" +
    "$P將您手上的戒指$R;" +
    "轉交給上城的老人$R;" +
    "肯定會有好事情的哦$R;";
            this.questCanceled = "會等待您的再次挑戰的。$R;";
        }

        public override void OnEvent(ActorPC pc)
        {
            int selection;
            BitMask<JLFlags> mask = new BitMask<JLFlags>(pc.CMask["JL"]);
            if (!mask.Test(JLFlags.D大師第一次對話))
            {
                Say(pc, 131, "在這裡接到的任務$R;" +
                    "是『隊伍』用的擊退任務$R;" +
                    "$R隊伍中如果有人接受一樣的任務$R;" +
                    "可以共享擊退的魔物數量$R;" +
                    "$P一起合作的話$R;" +
                    "執行任務會比較容易唷$R;");
                mask.SetValue(JLFlags.D大師第一次對話, true);
            }
            else
            {
                Say(pc, 255, "那麼！要去回廊寵物養殖場?$R;");
            }
            selection = Select(pc, "有什麼事啊？", "", "進入無限回廊", "任務服務台", "這建築是什麼？", "返回");
            while (selection != 4)
            {
                switch (selection)
                {
                    case 1:
                        Say(pc, 131, "如果不接受任務的話，$R;" +
                        "進入這個建築物就沒意義了$R;" +
                        "$R那您還要進去嗎？$R;");
                        進入回廊(pc);
                        return;
                    case 2:
                        HandleQuest(pc, 18);
                        return;
                    case 3:
                        Say(pc, 131, "這建築物是模仿某建築建造的。$R;" +
                            "現在作為修煉場所$R也會對一般人開放喔。$R;" +
                            "$P若想使用的話，就接受我的任務吧。$R;" +
                            "$R任務內容是為了$R獲取各階段進級必需的重點道具$R;" +
                            "$R為了任務成功，要努力啊！$R;" +
                            "$P當然，成功的話$R;" +
                            "會得到相應的報酬唷$R;");
                        break;
                    case 4:
                        return;
                }
                selection = Select(pc, "有什麼事啊？", "", "進入無限回廊", "任務服務台", "這建築是什麼？", "返回");
            }
        }

        void 進入回廊(ActorPC pc)
        {
            if (pc.Skills2.Count == 706 ||
                pc.SkillsReserve.Count == 706)
            {
                Say(pc, 131, "哦哦！$R;" +
                    pc.Name + "，是您嗎？$R;" +
                    "想進入無限回廊嗎？$R;" +
                    "您的到來，真是我的榮幸啊$R;" +
                    "不論如何，都要好好招待您吧！$R;" +
                    "$P您可能已經知道吧$R;" +
                    "不接受任務的話，進去也沒有意義阿$R;" +
                    "$R還要進去嗎？$R;");
                switch (Select(pc, "想去幾樓呢？", "", "無限回廊B1F", "無限回廊B11F（手續費4000金幣）", "無限回廊B21F（手續費8000金幣）", "無限回廊B31F（手續費12000金幣）", "還是算了吧"))
                {
                    case 1:
                        Warp(pc, 20070000, 23, 84);
                        break;
                    case 2:
                        Say(pc, 131, "可以略過中層，從11樓開始$R;" +
                            "但是需要手續費4000金幣阿$R;" +
                            "$R沒關係嗎？$R;");
                        switch (Select(pc, "支付4000金幣嗎？", "", "算了吧", "支付後進入11樓"))
                        {
                            case 1:
                                break;
                            case 2:
                                if (pc.Gold > 3999)
                                {
                                    pc.Gold -= 4000;
                                    Warp(pc, 20070010, 23, 84);
                                    return;
                                }
                                Say(pc, 131, "錢好像不夠啊$R;" +
                                    "$P這是為了建築的維修費$R;" +
                                    "請您幫幫忙吧$R;");
                                break;
                        }
                        break;
                    case 3:
                        Say(pc, 131, "可以略過中層，從21樓開始$R;" +
                            "但是需要手續費8000金幣阿$R;" +
                            "$R沒關係嗎？$R;");
                        switch (Select(pc, "支付8000金幣嗎？", "", "算了吧", "支付後進入21樓"))
                        {
                            case 1:
                                break;
                            case 2:
                                if (pc.Gold > 7999)
                                {
                                    pc.Gold -= 8000;
                                    Warp(pc, 20070020, 23, 84);
                                    return;
                                }
                                Say(pc, 131, "錢好像不夠啊$R;" +
                                    "$P這是為了建築的維修費$R;" +
                                    "請您幫幫忙吧$R;");
                                break;
                        }
                        break;
                    case 4:
                        Say(pc, 131, "可以略過中層，從31樓開始$R;" +
                            "但是需要手續費12000金幣阿$R;" +
                            "$R沒關係嗎？$R;");
                        switch (Select(pc, "支付12000金幣嗎？", "", "算了吧", "支付後進入31樓"))
                        {
                            case 1:
                                break;
                            case 2:
                                if (pc.Gold > 11999)
                                {
                                    pc.Gold -= 12000;
                                    Warp(pc, 20070030, 23, 84);
                                    return;
                                }
                                Say(pc, 131, "錢好像不夠啊$R;" +
                                    "$P這是為了建築的維修費$R;" +
                                    "請您幫幫忙吧$R;");
                                break;
                        }
                        break;
                    case 5:
                        break;
                }
                return;
            }

            Say(pc, 131, "如果不接受任務的話，$R;" +
                "進入這個建築物就沒意義了$R;" +
                "$R那您還要進去嗎？$R;");
            switch (Select(pc, "想去幾樓呢？", "", "無限回廊B1F", "無限回廊B11F（手續費5000金幣）", "無限回廊B21F（手續費10000金幣）", "無限回廊31F（手續費15000金幣）", "還是算了吧"))
            {
                case 1:
                    Warp(pc, 20070000, 23, 84);
                    break;
                case 2:
                    Say(pc, 131, "可以略過中層，從11樓開始$R;" +
                        "但是需要手續費5000金幣阿$R;" +
                        "$R沒關係嗎？$R;");
                    switch (Select(pc, "支付5000金幣嗎？", "", "算了吧", "支付後進入11樓"))
                    {
                        case 1:
                            break;
                        case 2:
                            if (pc.Gold > 4999
                            )
                            {
                                pc.Gold -= 5000;
                                Warp(pc, 20070010, 23, 84);
                                return;
                            }
                            Say(pc, 131, "錢好像不夠啊$R;" +
                                "$P這是為了建築的維修費$R;" +
                                "請您幫幫忙吧$R;");
                            break;
                    }
                    break;
                case 3:
                    Say(pc, 131, "可以略過中層，從21樓開始$R;" +
                        "但是需要手續費10000金幣阿$R;" +
                        "$R沒關係嗎？$R;");
                    switch (Select(pc, "支付10000金幣嗎？", "", "算了吧", "支付後進入21樓"))
                    {
                        case 1:
                            break;
                        case 2:
                            if (pc.Gold > 9999)
                            {
                                pc.Gold -= 10000;
                                Warp(pc, 20070020, 23, 84);
                                return;
                            }
                            Say(pc, 131, "錢好像不夠啊$R;" +
                                "$P這是為了建築的維修費$R;" +
                                "請您幫幫忙吧$R;");
                            break;
                    }
                    break;
                case 4:
                    Say(pc, 131, "可以略過中層，從31樓開始$R;" +
                        "但是需要手續費15000金幣阿$R;" +
                        "$R沒關係嗎？$R;");
                    switch (Select(pc, "支付15000金幣嗎？", "", "算了吧", "支付後進入31樓"))
                    {
                        case 1:
                            break;
                        case 2:
                            if (pc.Gold > 14999)
                            {
                                pc.Gold -= 15000;
                                Warp(pc, 20070030, 23, 84);
                                return;
                            }
                            Say(pc, 131, "錢好像不夠啊$R;" +
                                "$P這是為了建築的維修費$R;" +
                                "請您幫幫忙吧$R;");
                            break;
                    }
                    break;
                case 5:
                    break;
            }
        }
    }
}
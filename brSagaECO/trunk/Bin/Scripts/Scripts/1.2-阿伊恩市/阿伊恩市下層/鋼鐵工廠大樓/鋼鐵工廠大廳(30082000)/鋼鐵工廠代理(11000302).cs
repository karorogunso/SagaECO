using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30082000
{
    public class S11000302 : Event
    {
        public S11000302()
        {
            this.EventID = 11000302;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<AYEFlags> mask = pc.CMask["AYE"];
            if (!mask.Test(AYEFlags.與鋼鐵工廠老闆對話))//_0c38)
            {
                switch (Select(pc, "歡迎，這裡是鋼鐵工廠！", "", "製造武器", "強化裝備", "強化裝備的注意事項", "洽談", "什麼也不做"))
                {
                    case 1:
                        switch (Select(pc, "做什麼呢？", "", "製造武器", "製造防具", "煉製金屬", "製造『箭』", "不做"))
                        {
                            case 1:
                                switch (Select(pc, "想製作什麼?", "", "製作武器", "製作魔杖", "製作弓", "放棄"))
                                {
                                    case 1:
                                        Synthese(pc, 2010, 10);
                                        break;
                                    case 2:
                                        Synthese(pc, 2021, 5);
                                        break;
                                    case 3:
                                        Synthese(pc, 2034, 5);
                                        break;
                                }
                                break;
                            case 2:
                                Synthese(pc, 2017, 5);
                                break;
                            case 3:
                                Synthese(pc, 2051, 3);
                                break;
                            case 4:
                                Synthese(pc, 2035, 5);
                                break;
                        }
                        break;
                    case 2:
                        強化說明(pc);
                        break;
                    case 3:
                        Say(pc, 131, "強化武器會給道具帶來負擔的喔。$R;" +
                            "一個裝備最多可以強化『十次』。$R;" +
                            "強化越多次就越容易失敗毀滅，$R;" +
                            "所以要注意啊。$R;" +
                            "$P但是在強化時破碎了，$R;" +
                            "手續費也是不會退還的阿。$R;" +
                            "敬請留意$R;" +
                            "但是強化一兩次是不會破碎的，$R;" +
                            "請放心。$R;" +
                            "$P啊！還有要強化的裝備$R是要脫下來的，$R小心點給我吧$R;");
                        break;
                    case 4:
                        Say(pc, 131, "老闆只會給有名的冒險者$R;" +
                            "介紹任務阿$R;" +
                            "$P……您$R;" +
                            pc.Name + "?$R;" +
                            "$P……好像還不夠資格$R;" +
                            "沒聽過呢！$R;" +
                            "多工作，累積更多經驗再來吧！$R;");
                        break;
                }
                return;
            }
            switch (Select(pc, "歡迎，這裡是鋼鐵工廠！", "", "任務服務台", "買東西", "製造武器", "強化裝備", "強化裝備的注意事項", "什麼也不做"))
            {
                case 1:
                    HandleQuest(pc, 45);
                    break;
                case 2:
                    OpenShopBuy(pc, 97);
                    break;
                case 3:
                    switch (Select(pc, "做什麼呢？", "", "製造武器", "製造防具", "煉製金屬", "製造『箭』", "不做"))
                    {
                        case 1:
                            switch (Select(pc, "想製作什麼?", "", "製作武器", "製作魔杖", "製作弓", "放棄"))
                            {
                                case 1:
                                    Synthese(pc, 2010, 10);
                                    break;
                                case 2:
                                    Synthese(pc, 2021, 5);
                                    break;
                                case 3:
                                    Synthese(pc, 2034, 5);
                                    break;
                            }
                            break;
                        case 2:
                            Synthese(pc, 2017, 5);
                            break;
                        case 3:
                            Synthese(pc, 2051, 3);
                            break;
                        case 4:
                            Synthese(pc, 2035, 5);
                            break;
                    }
                    break;
                case 4:
                    強化說明(pc);
                    break;
                case 5:
                    Say(pc, 131, "強化武器會給道具帶來負擔的喔。$R;" +
                        "一個裝備最多可以強化『十次』。$R;" +
                        "強化越多次就越容易失敗毀滅，$R;" +
                        "所以要注意啊。$R;" +
                        "$P但是在強化時破碎了，$R;" +
                        "手續費也是不會退還的阿。$R;" +
                        "敬請留意$R;" +
                        "但是強化一兩次是不會破碎的，$R;" +
                        "請放心。$R;" +
                        "$P啊！還有要強化的裝備$R是要脫下來的，$R小心點給我吧$R;");
                    break;
            }
        }

        void 強化說明(ActorPC pc)
        {
            BitMask<AYEFlags> mask = pc.CMask["AYE"];

            if (!mask.Test(AYEFlags.聽取強化是說明))//_2a67)
            {
                mask.SetValue(AYEFlags.聽取強化是說明, true);
                //_2a67 = true;
                Say(pc, 131, "您是第一次使用$R;" +
                    "我們這裡的強化裝備服務嗎？$R;" +
                    "那就簡單說明一下，如何？$R;");
                switch (Select(pc, "要聽說明嗎？", "", "聽", "不聽"))
                {
                    case 1:
                        Say(pc, 131, "強化裝備是提高武器$R;" +
                            "或者防具性能的技術唷。$R;" +
                            "是我們公司獨有的技術呢$R;" +
                            "$P想得到這服務$R;" +
                            "必須滿足以下幾個條件。$R;" +
                            "$R首先要有適合催化的『道具』$R;" +
                            "$P另外，要向我們公司$R;" +
                            "支付『手續費5000金幣』阿$R;" +
                            "$P要強化的裝備$R是要脫下來的，$R小心點給我吧$R;" +
                            "催化道具的不同，$R;" +
                            "強化結果也不同唷。$R;" +
                            "$R請準備好$R;" +
                            "與裝備適合的催化道具吧。$R;");
                        if (pc.Fame < 10)
                        {
                            Say(pc, 131, "既然已經來到這裡了$R;" +
                                "就送您一個催化道具吧。$R;" +
                                "$P…$R;" +
                                "$P客人，您好像是初心者呢。$R;" +
                                "現在裝備還夠用$R;" +
                                "還不需要強化裝備吧？$R;" +
                                "以後多點去冒險，$R;" +
                                "真的需要強化裝備的時候$R;" +
                                "再來吧。$R;" +
                                "$R到時再送您禮物。$R;");
                            return;
                        }
                        break;
                    case 2:
                        if (!mask.Test(AYEFlags.得到生命結晶)&& pc.Fame > 9)//_Xa13 
                        {
                            Say(pc, 131, "既然已經來到這裡了$R;" +
                                "就送您一個催化道具吧。$R;");
                            if (CheckInventory(pc, 90000043, 1))
                            {
                                mask.SetValue(AYEFlags.得到生命結晶, true);
                                //_Xa13 = true;
                                GiveItem(pc, 90000043, 1);
                                Say(pc, 131, "這是『生命的結晶』。$R;" +
                                    "使用這個的話$R;" +
                                    "可以製造提升最大HP的『防具』阿$R;" +
                                    "$R試一下吧$R;");
                                return;
                            }
                            Say(pc, 131, "整理行李後，減少道具$R;" +
                                "再來吧。$R;");
                            return;
                        }
                        break;
                }
                return;
            }
            if (!mask.Test(AYEFlags.得到生命結晶) && pc.Fame > 9)//_Xa13
            {
                Say(pc, 131, "既然已經來到這裡了$R;" +
                    "就送您一個催化道具吧。$R;");
                if (CheckInventory(pc, 90000043, 1))
                {
                    mask.SetValue(AYEFlags.得到生命結晶, true);
                    //_Xa13 = true;
                    GiveItem(pc, 90000043, 1);
                    Say(pc, 131, "這是『生命的結晶』。$R;" +
                        "使用這個的話$R;" +
                        "可以製造提升最大HP的『防具』阿$R;" +
                        "$R試一下吧$R;");
                    return;
                }
                Say(pc, 131, "整理行李後，減少道具$R;" +
                    "再來吧。$R;");
                return;
            }
            Say(pc, 131, "每強化一次$R;" +
                "就要5000金幣手續費阿$R;" +
                "$R費用會自動轉賬，$R;" +
                "只要確認身上的錢就可以了。$R;");
            if (pc.Gold < 5000)
                Say(pc, 131, "這服務需要手續費5000金幣$R;" +
                    "您現在的現金，好像不太夠呢。$R;");
            else if (CountItem(pc, 90000043) == 0 &&
                CountItem(pc, 90000044) == 0 &&
                CountItem(pc, 90000045) == 0 &&
                CountItem(pc, 90000046) == 0)
                Say(pc, 131, "沒有拿催化道具呢。$R;");
            else if (!ItemEnhance(pc))
                Say(pc, 131, "沒拿材料道具呢。$R;");
        }
    }
}
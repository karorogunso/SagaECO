using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:下城(10024000) NPC基本信息:西軍騎士團團員(11000905) X:133 Y:50
namespace SagaScript.M10024000
{
    public class S11000905 : Event
    {
        public S11000905()
        {
            this.EventID = 11000905;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<INSD> mask = pc.CMask["INSD"];

            if (pc.Account.GMLevel >= 100)
            {
                switch (Select(pc, "GM用選擇列表", "", "進入廣場", "普通玩家對話", "什麽都不做"))
                { 
                    case 1:
                        Warp(pc, 20080012, 22, 28);
                        return;

                    case 3:
                        return;
                }
            }

            if (pc.Fame < 10)
            {
                Say(pc, 131, "…不好意思$R;" +
                    "可能對你是困難的事情$R;");
                return;
            }
            if (mask.Test(INSD.西軍團員要求協助))//_0C65)
            {
                Say(pc, 131, "現在都準備好了嗎?$R;" +
                    "那我來帶路到遺跡吧$R;");
                Warp(pc, 20080012, 22, 28);
                return;
            }
            if (mask.Test(INSD.西軍團員詢問遺跡))//_0C66)
            {
                Say(pc, 131, "混城騎士團很期待你的協助呢!$R;");
                switch (Select(pc, "怎麼做呢?", "", "想去遺跡!", "什麼都不是"))
                {
                    case 1:
                        mask.SetValue(INSD.西軍團員要求協助, true);
                        //_0C65 = true;
                        Say(pc, 131, "真的很感謝您能幫忙!$R;" +
                            "準備好的再跟我說話吧!$R;");
                        break;
                    case 2:
                        break;
                }
                return;
            }
            Say(pc, 131, "喂!你啊!$R;" +
                "看你從格鬥場過來$R;" +
                "對自己的實力應該很有自信吧！$R;" +
                "$R想協助我們到遺跡調查嗎?$R;");
            switch (Select(pc, "怎麼辦？", "", "不協助!", "遺跡?"))
            {
                case 1:
                    break;
                case 2:
                    mask.SetValue(INSD.西軍團員詢問遺跡, true);
                    //_0C66 = true;
                    Say(pc, 131, "是一個超大的遺跡。到現在為止$R;" +
                        "發現的遺跡中，沒有一個能相比的$R;" +
                        "$R各個國家都在留意$R;" +
                        "現在這個國家的傢伙們也派了調查隊$R;" +
                        "$P混城騎士團這次也合作調查呢$R;" +
                        "平時互相虎視眈眈$R;" +
                        "偶爾也應該這樣吧?$R;" +
                        "$R啊…不好意思$R;" +
                        "$P回到正題$R;" +
                        "怎麼樣?想協助調查嗎?$R;");
                    switch (Select(pc, "怎麼做呢?", "", "協助", "放棄"))
                    {
                        case 1:
                            mask.SetValue(INSD.西軍團員要求協助, true);
                            //_0C65 = true;
                            Say(pc, 131, "真的很感謝您能幫忙!$R;" +
                                "準備好的再跟我說話吧!$R;");
                            break;
                        case 2:
                            break;
                    }
                    break;
            }
        }
    }
}

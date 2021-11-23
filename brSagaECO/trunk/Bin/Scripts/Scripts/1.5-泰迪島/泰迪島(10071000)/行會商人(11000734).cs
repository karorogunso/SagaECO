using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:泰迪島(10071000) NPC基本信息:行會商人(11000734) X:128 Y:220
namespace SagaScript.M10071000
{
    public class S11000734 : Event
    {
        public S11000734()
        {
            this.EventID = 11000734;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Tinyis_Land_01> Tinyis_Land_01_mask = new BitMask<Tinyis_Land_01>(pc.CMask["Tinyis_Land_01"]);

            if (!Tinyis_Land_01_mask.Test(Tinyis_Land_01.已經與行會商人進行第一次對話))
            {
                初次與行會商人進行對話(pc);
            }

            Say(pc, 11000734, 131, "歡迎光臨!!$R;", "行會商人");

            switch (Select(pc, "歡迎光臨!!", "", "買東西", "賣東西", "什麼也不做"))
            {
                case 1:
                    OpenShopBuy(pc, 148);
                    break;

                case 2:
                    OpenShopSell(pc, 148);
                    break;

                case 3:
                    break;
            }
        }

        void 初次與行會商人進行對話(ActorPC pc)
        {
            BitMask<Tinyis_Land_01> Tinyis_Land_01_mask = new BitMask<Tinyis_Land_01>(pc.CMask["Tinyis_Land_01"]);

            Tinyis_Land_01_mask.SetValue(Tinyis_Land_01.已經與行會商人進行第一次對話, true);

            Say(pc, 11000734, 131, "您好，$R;" +
                                   "我是屬於「商人行會」的「行會商人」唷!$R;" +
                                   "$R以冒險者為販賣東西的對象呢。$R;" +
                                   "$P聽說這裡有可以做買賣的好地方，$R;" +
                                   "所以就乘坐飛空庭飛到了這座島。$R;" +
                                   "$P商人是只要生意好，$R;" +
                                   "哪兒都可以去的。$R;" +
                                   "$R哈哈!!$R;", "行會商人");

            Say(pc, 11000734, 131, "什麼? 什麼叫「飛空庭」?$R;" +
                                   "$R「飛空庭」，故名思義，$R;" +
                                   "是指飛上天空的『庭院』的意思。$R;" +
                                   "$P我們商人主要用它來運東西唷。$R;" +
                                   "$R但用來栽培農作物，$R;" +
                                   "或安裝大的帆到處旅行也是可以的。$R;" +
                                   "$P最近，$R;" +
                                   "流行在庭院上，$R;" +
                                   "蓋自己的『房子』呢。$R;" +
                                   "$P您也去趕緊準備一艘吧。$R;", "行會商人");

            Say(pc, 11000734, 131, "哎呀，說了一些廢話。$R;" +
                                   "$R在飛行途中遇到了暴風雨，$R;" +
                                   "失去了意識，$R;" +
                                   "醒來一看，就在這座島了。$R;" +
                                   "$P為了減少「飛空庭」的東西，$R;" +
                                   "現在特價大拍賣。$R;" +
                                   "$R一定要光臨呀!!$R;", "行會商人");

            Say(pc, 11000734, 131, "錢不夠的話，$R;" +
                                   "可以在這座島上，$R;" +
                                   "搜集各種物品賣給我唷。$R;" +
                                   "$R我會賣你便宜一點的。$R;", "行會商人");
        }
    }
}





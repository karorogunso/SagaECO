using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30153000
{
    public class S11001131 : Event
    {
        public S11001131()
        {
            this.EventID = 11001131;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 10013350) >= 5 && CountItem(pc, 10009107) >= 5 && CountItem(pc, 10011600) >= 5 && CountItem(pc, 10014500) >= 5)
            {
                Say(pc, 255, "能不能把您的$R;" +
                    "『金光藍寶珠破片』5個$R;" +
                    "『月亮的雕刻』5個$R;" +
                    "『心的破片』5個$R;" +
                    "『水晶破片』5個$R;" +
                    "$R給我呢？$R;");
                switch (Select(pc, "怎麼做呀？", "", "不給", "給他吧"))
                {
                    case 1:
                        break;
                    case 2:
                        if (CheckInventory(pc, 10011000, 1))
                        {
                            TakeItem(pc, 10013350, 5);
                            TakeItem(pc, 10009107, 5);
                            TakeItem(pc, 10011600, 5);
                            TakeItem(pc, 10014500, 5);
                            GiveItem(pc, 10011000, 1);
                            Say(pc, 255, "謝謝！$R;" +
                                "$R報答的東西雖小，$R請收下『奇怪的水晶』吧。$R;" +
                                "$P這是愛伊斯族的生命源泉唷。$R;" +
                                "我們愛伊斯人是在水晶裡誕生的。$R;" +
                                "$R去我的故鄉愛伊斯島吧，$R族長會報答您的。$R;" +
                                "$P旅遊途中偶然相遇，$R不過現在想跟他在一起，$R;" +
                                "所以把這個水晶交託給您…$R;");
                            PlaySound(pc, 2040, false, 100, 50);
                            Say(pc, 131, "得到了『奇怪的水晶』$R;");
                            return;
                        }
                        Say(pc, 255, "謝謝！$R;" +
                            "$R報答的東西雖小，$R請收下『奇怪的水晶』吧。$R;" +
                            "$R把行李減輕後，再來吧。$R;");
                        break;
                }
                return;
            }
            if (pc.Marionette != null)
            {
                Say(pc, 255, "做實驗需要$R;" +
                    "『金光藍寶珠破片』5個$R;" +
                    "『月亮的雕刻』5個$R;" +
                    "『心的破片』5個$R;" +
                    "『水晶破片』5個$R;" +
                    "$R如果有的話，能不能分給我？$R;");
                return;
            }
            Say(pc, 255, "歡迎光臨~$R;" +
                "找卡米羅先生有事嗎？$R;");
        }
    }
}
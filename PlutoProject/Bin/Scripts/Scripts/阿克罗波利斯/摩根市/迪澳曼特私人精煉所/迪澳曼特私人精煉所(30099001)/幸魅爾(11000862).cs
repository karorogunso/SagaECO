using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30099001
{
    public class S11000862 : Event
    {
        public S11000862()
        {
            this.EventID = 11000862;
        }

        public override void OnEvent(ActorPC pc)
        {
            /*
            if (_6a69)
            {
                Say(pc, 131, "證據好像起效果了$R;" +
                    "這裡的煉制所亂成一團了！$R;" +
                    "$R我在這裡繼續調查情況$R;" +
                    "他們幹的壞事$R;" +
                    "比我想像的還要嚴重呀$R;");
                return;
            }

            if (CountItem(pc, 10020002) > 0)
            {
                Say(pc, 131, "噓！小聲點！$R;" +
                    "$P看樣子找到證據了…$R;" +
                    "$R我在這裡繼續調查情況$R;" +
                    "他們幹的壞事$R;" +
                    "比我想像的還要嚴重呀$R;");
                return;
            }

            if (_6a73)
            {
                Say(pc, 131, "您只要跟我打招呼，就會被懷疑的$R;" +
                    "$P拿到證據的小子，逃到了光之塔$R;" +
                    "趕快追過去！快阿$R;");
                return;
            }

            if (_6a72)
            {
                Say(pc, 255, "……$R;");
                switch (Select(pc, "怎麼辦呢？", "", "奇怪的小子", "受比坳列塔委託來到了這裡…"))
                {
                    case 1:
                        break;
                    case 2:
                        _6a73 = true;
                        Say(pc, 131, "噓！小點聲$R;" +
                            "$P您是比坳列塔的聯絡員嗎？$R;" +
                            "$R聽好了！$R;" +
                            "沒有時間了，簡單說一下吧$R;" +
                            "$P拿到證據的小子，逃到了光之塔$R;" +
                            "找到那個小子搶回證據吧$R;" +
                            "$R我現在被監視，$R;" +
                            "只要有異常行為，就會前功盡棄的$R;" +
                            "請您幫幫我！拜託了$R;");
                        break;
                }
                return;
            }
            */
            Say(pc, 255, "……$R;");
        }
    }
}
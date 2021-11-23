using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10065000
{
    public class S11000213 : Event
    {
        public S11000213()
        {
            this.EventID = 11000213;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "這裡是女王陛下居住的$R;" +
                "『諾頓宮殿』唷$R;");
        }

        void 万圣节(ActorPC pc)
        {

            //EVT1100021301
            //GUIDE OFF
            //SWITCH START

            //,,FLAG ON 0b25 EVT1100021300,
            //,,FLAG ON 0b23 EVT1100021307,
            //,,FLAG ON 0b22 EVT1100021306,
            //,,FLAG ON 0b21 EVT1100021305,
            //,,FLAG ON 0b20 EVT1100021304,
            //,,FLAG ON 0b19 EVT1100021303,
            //,,FLAG ON 0b18 EVT1100021302,
            //SWITCH END
            //GOTO EVT1100021300
            //EVENTEND
            //EVT1100021302
            Say(pc, 131, "女王陛下吩咐您去$R;" +
                "魔法行會總部是嗎？$R;" +
                "$P魔法行會總部就在前面$R;" +
                "$R我用箭頭指路，$R;" +
                "跟著去就可以了$R;" +
                "$P如果不清楚再問問吧$R;");
            //GUIDE ON 42 67
            //EVENTEND
            //EVT1100021303
            Say(pc, 131, "啊？什麼？$R;" +
                "這次調查關於『巴路基石』$R;" +
                "去魔法商店是嗎？$R;" +
                "$R魔法商店位於商人地區$R;" +
                "我給您指路吧$R;");
            //GUIDE ON 66 152
            //EVENTEND
            //EVT1100021304
            Say(pc, 131, "熟悉諾頓市的人是嗎？$R;" +
                "$R啊！有呀$R;" +
                "有一個小子喜歡裝做$R;" +
                "熟悉很多傳聞或內情阿$R;" +
                "$P可能在橋的附近$R;" +
                "不知道今天在不在$R;" +
                "去找一找吧$R;");
            //EVENTEND
            //EVT1100021305
            Say(pc, 131, "咖啡館？$R;" +
                "商人區裡有兩間咖啡館$R;" +
                "給您介紹著名的地方吧$R;");
            //GUIDE ON 66 186
            //EVENTEND
            //EVT1100021306
            Say(pc, 131, "到我們諾頓王國軍來，$R有什麼事情要辦嗎？$R;" +
                "$R我現在是上班時間$R;" +
                "請您到本部吧$R;" +
                "$P本部就在附近$R;" +
                "入口處有武僧站崗$R;" +
                "很快就能找到的$R;" +
                "$R西邊是男武僧本部$R;" +
                "東邊是女武僧本部喔$R;");
            //if (ME.SEX = 1)

            {
                //Call(EVT1100021308);
                return;
            }
            //GUIDE ON 70 5
            //EVENTEND
            //EVT1100021307
            //Say(pc, 131, "事情辦的怎麼樣呢？$R;");
            //EVENTEND
            //EVT1100021308
            //GUIDE ON 33 5
            //EVENTEND
        }
    }
}
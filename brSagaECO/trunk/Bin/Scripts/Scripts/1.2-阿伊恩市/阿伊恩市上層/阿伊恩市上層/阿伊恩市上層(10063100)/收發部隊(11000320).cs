using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10063100
{
    public class S11000320 : Event
    {
        public S11000320()
        {
            this.EventID = 11000320;
        }

        public override void OnEvent(ActorPC pc)
        {
            NavigateCancel(pc);
            Say(pc, 131, "我們是收發部隊。$R;" +
                "知道關於這個城市的所有資料唷。$R;");
            switch (Select(pc, "要介紹嗎？", "", "空軍本部", "議會", "傭兵軍團本部", "不"))
            {
                case 1:
                    Say(pc, 131, "那個地方$R;" +
                        "就是空軍本部阿。$R;");
                    break;
                case 2:
                    Say(pc, 131, "中央的大建築物$R;" +
                        "就是議會喔。$R;");
                    Navigate(pc, 45, 103);
                    break;
                case 3:
                    Say(pc, 131, "從議會再往前走一會兒$R;" +
                        "就是傭兵軍團本部喔。$R;");
                    Navigate(pc, 44, 156);
                    break;
            }
        }
    }
}
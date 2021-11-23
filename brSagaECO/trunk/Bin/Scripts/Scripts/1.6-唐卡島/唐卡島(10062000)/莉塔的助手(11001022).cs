using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10062000
{
    public class S11001022 : Event
    {
        public S11001022()
        {
            this.EventID = 11001022;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.Marionette != null)
            {
                Say(pc, 131, "我們老師太馬虎了$R;");
                Say(pc, 11001056, 131, "莉塔老師也是嗎？$R;" +
                    "$R利迪阿的房間很髒$R;" +
                    "$R真不知該怎麼辦。$R;");
                Say(pc, 131, "活動木偶研究固然重要$R;" +
                    "$R但看完書就扔掉，$R;" +
                    "文件掉了也不撿，$R;" +
                    "這樣，再漂亮也嫁不出去的呀$R;");
                Say(pc, 11001056, 131, "對阿對阿$R;" +
                    "真是讓人操心呀$R;");
                return;
            }
            Say(pc, 131, "我們在這個私人工作室，$R;" +
                "做老師的助手$R;" +
                "$R我們的老師叫莉塔，$R;" +
                "雖然很年輕，但實力超強的唷$R;" +
                "活動木偶工匠$R;" +
                "$P特別是製作綠礦石精靈的$R;" +
                "傑出人物。$R;" +
                "$R您一定要見識一下呀！$R;");
        }
    }
}
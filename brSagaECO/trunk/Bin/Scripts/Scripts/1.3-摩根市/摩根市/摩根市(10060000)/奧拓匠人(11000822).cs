using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10060000
{
    public class S11000822 : Event
    {
        public S11000822()
        {
            this.EventID = 11000822;

        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "量太少了$R;" +
                "$R西邊賣的東西也許會好點嗎？$R;");
            Say(pc, 11000811, 131, "不過您也知道西邊有$R;" +
                "「商人行會總部」吧？$R;" +
                "$P那裡總部代表不會讓給我們的？$R;" +
                "$R為了蓋那個建築物花了數千萬金幣$R;");
            Say(pc, 131, "那也得交換一下$R;" +
                "$R雖然不會讓步…$R;" +
                "那調查一下，東邊有沒有好的位置吧$R;");
            Say(pc, 11000811, 131, "知道了$R;");
        }
    }
}
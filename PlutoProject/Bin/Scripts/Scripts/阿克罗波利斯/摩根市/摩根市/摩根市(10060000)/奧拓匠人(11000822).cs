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
                "$R西边卖的东西也许会好点吗？$R;");
            Say(pc, 11000811, 131, "不过您也知道西边有$R;" +
                "「商人行会总部」吧？$R;" +
                "$P那里总部代表不会让给我们的？$R;" +
                "$R为了盖那个建筑物花了数千万金币$R;");
            Say(pc, 131, "那也得交换一下$R;" +
                "$R虽然不会让步…$R;" +
                "那调查一下，东边有没有好的位置吧$R;");
            Say(pc, 11000811, 131, "知道了$R;");
        }
    }
}
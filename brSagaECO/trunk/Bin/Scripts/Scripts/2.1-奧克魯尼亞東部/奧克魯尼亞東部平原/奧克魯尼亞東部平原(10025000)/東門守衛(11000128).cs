using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:奧克魯尼亞東部平原(10025000) NPC基本信息:東門守衛(11000128) X:61 Y:121
namespace SagaScript.M10025000
{
    public class S11000128 : Event
    {
        public S11000128()
        {
            this.EventID = 11000128;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11000128, 131, "照著這條路往東邊走的話，$R;" +
                                   "就是「帕斯特島」了。$R;" +
                                   "$R路上要小心才可以啊!$R;", "東門守衛");
        }
    }
}

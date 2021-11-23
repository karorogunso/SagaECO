using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:奧克魯尼亞西部平原(10022000) NPC基本信息:西門守衛(11000130) X:134 Y:61
namespace SagaScript.M10022000
{
    public class S11000130 : Event
    {
        public S11000130()
        {
            this.EventID = 11000130;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11000130, 131, "進入「奧克魯尼亞大陸西部」的冒險，$R;" +
                                   "等實力增強了以後，再來會比較好。$R;", "西門守衛");
        }
    }
}

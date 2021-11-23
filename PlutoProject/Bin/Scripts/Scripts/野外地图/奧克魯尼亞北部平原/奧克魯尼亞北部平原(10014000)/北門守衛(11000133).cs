using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:奧克魯尼亞北部平原(10014000) NPC基本信息:北門守衛(11000133) X:134 Y:61
namespace SagaScript.M10014000
{
    public class S11000133 : Event
    {
        public S11000133()
        {
            this.EventID = 11000133;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11000133, 131, "照着这条路往北边走的话，$R;" +
                                   "会出走到「阿克罗尼亚大陆北部」，$R;" +
                                   "过了眼前的那座桥就是「诺森岛」了。$R;", "北门守卫");
        }
    }
}

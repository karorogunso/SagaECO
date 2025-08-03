using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:奧克魯尼亞西部平原(10022000) NPC基本信息:路標(12002017) X:154 Y:253
namespace SagaScript.M10022000
{
    public class S12002017 : Event
    {
        public S12002017()
        {
            this.EventID = 12002017;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, 0, "前面是危险地带!!$R;" +
                          "赶快回去吧!!$R;", " "); 
        }
    }
}

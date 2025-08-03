using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:奧克魯尼亞北部平原(10014000) NPC基本信息:北門守衛(11000134) X:134 Y:61
namespace SagaScript.M10014000
{
    public class S11000134 : Event
    {
        public S11000134()
        {
            this.EventID = 11000134;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11000134, 131, "去北边的时候，$R;" +
                                   "要做好保暖措施呀。$R;", "北门守卫");
        }
    }
}

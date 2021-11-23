using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:東方海角(10018003) NPC基本信息:初階冒險者(11001429) X:61 Y:57
namespace SagaScript.M10018003
{
    public class S11001429 : Event
    {
        public S11001429()
        {
            this.EventID = 11001429;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11001429, 0, "跟前辈学了很多东西。$R;" +
                                 "$R您要一起学吗?$R;", "初阶冒险者");
        }
    }
}

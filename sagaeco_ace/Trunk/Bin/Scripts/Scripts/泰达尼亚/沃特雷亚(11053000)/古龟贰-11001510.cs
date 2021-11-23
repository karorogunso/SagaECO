using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地图:沃特雷亚(11053000)NPC基本信息:11001510-古龟贰- X:43 Y:173
namespace SagaScript.M11053000
{
    public class S11001510 : Event
    {
    public S11001510()
        {
            this.EventID = 11001510;
        }


        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "呼～呼～$R;");
        }
    }
}

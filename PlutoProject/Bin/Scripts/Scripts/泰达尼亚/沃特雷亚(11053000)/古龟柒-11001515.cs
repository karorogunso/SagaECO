using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地图:沃特雷亚(11053000)NPC基本信息:11001515-古龟柒- X:70 Y:228
namespace SagaScript.M11053000
{
    public class S11001515 : Event
    {
    public S11001515()
        {
            this.EventID = 11001515;
        }


        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "呼嘶...............$R;");
        }
    }
}

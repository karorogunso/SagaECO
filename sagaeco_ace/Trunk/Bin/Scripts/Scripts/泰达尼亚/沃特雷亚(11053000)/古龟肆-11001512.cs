using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地图:沃特雷亚(11053000)NPC基本信息:11001512-古龟肆- X:62 Y:200
namespace SagaScript.M11053000
{
    public class S11001512 : Event
    {
    public S11001512()
        {
            this.EventID = 11001512;
        }


        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "呼嘶....呼嘶......$R;");
        }
    }
}

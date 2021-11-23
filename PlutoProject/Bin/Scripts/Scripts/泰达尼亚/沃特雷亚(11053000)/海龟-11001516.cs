using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地图:沃特雷亚(11053000)NPC基本信息:11001516-海龟- X:52 Y:230
namespace SagaScript.M11053000
{
    public class S11001516 : Event
    {
    public S11001516()
        {
            this.EventID = 11001516;
        }


        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "嗯???$R;");
        }
    }
}

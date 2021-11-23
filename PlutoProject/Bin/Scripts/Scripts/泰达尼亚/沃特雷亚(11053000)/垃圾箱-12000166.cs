using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地图:沃特雷亚(11053000)NPC基本信息:12000166-垃圾箱- X:27 Y:218
namespace SagaScript.M11053000
{
    public class S12000166 : Event
    {
    public S12000166()
        {
            this.EventID = 12000166;
        }


        public override void OnEvent(ActorPC pc)
        {
            NPCTrade(pc);
        }
    }
}

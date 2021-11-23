using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地图:沃特雷亚(11053000)NPC基本信息:11001514-古龟陆- X:37 Y:163
namespace SagaScript.M11053000
{
    public class S11001514 : Event
    {
    public S11001514()
        {
            this.EventID = 11001514;
        }


        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "嘎嘎$R;");
        }
    }
}

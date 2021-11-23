using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地图:沃特雷亚(11053000)NPC基本信息:11001473-贝露丹迪- X:103 Y:127
namespace SagaScript.M11053000
{
    public class S11001473 : Event
    {
    public S11001473()
        {
            this.EventID = 11001473;
        }


        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "我是从哪儿来...$R而又去了哪里呢$R");
        }
    }
}

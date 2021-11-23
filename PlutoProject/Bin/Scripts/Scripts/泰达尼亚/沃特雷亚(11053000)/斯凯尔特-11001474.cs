using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地图:沃特雷亚(11053000)NPC基本信息:11001474-斯凯尔特- X:112 Y:140
namespace SagaScript.M11053000
{
    public class S11001474 : Event
    {
    public S11001474()
        {
            this.EventID = 11001474;
        }


        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "我要飞走了QAQ");
        }
    }
}

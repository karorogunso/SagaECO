using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地图:沃特雷亚(11053000)NPC基本信息:11001472-乌尔兹- X:97 Y:121
namespace SagaScript.M11053000
{
    public class S11001472 : Event
    {
    public S11001472()
        {
            this.EventID = 11001472;
        }


        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "我爱的人已经飞走鸟♪$R爱我的人他------$R还没有来到♪♪♪");
	    Say(pc, 0, "（还是离她远点吧）");
        }
    }
}

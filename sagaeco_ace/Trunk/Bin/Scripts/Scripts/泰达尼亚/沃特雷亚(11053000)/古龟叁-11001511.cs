using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地图:沃特雷亚(11053000)NPC基本信息:11001511-古龟叁- X:30 Y:201
namespace SagaScript.M11053000
{
    public class S11001511 : Event
    {
    public S11001511()
        {
            this.EventID = 11001511;
        }


        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "你好啊冒险者.....呼哧...$R你看我是这些王八里$R唯一一个讲人话的...$R;");
            Say(pc, 0, "请您救救我们吧$R;");
        }
    }
}

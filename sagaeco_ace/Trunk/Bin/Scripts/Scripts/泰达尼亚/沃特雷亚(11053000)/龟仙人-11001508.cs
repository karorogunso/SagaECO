using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地图:沃特雷亚(11053000)NPC基本信息:11001508-龟仙人- X:53 Y:231
namespace SagaScript.M11053000
{
    public class S11001508 : Event
    {
    public S11001508()
        {
            this.EventID = 11001508;
        }


        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "这里好久没有人来了!$R欢迎你啊!远方的冒险者!$R;");
            Say(pc, 0, "几年来...$R我们一直在期待着$R冒险者的来访....$R可是...为什么没有人$R再来了呢!!!");
        }
    }
}

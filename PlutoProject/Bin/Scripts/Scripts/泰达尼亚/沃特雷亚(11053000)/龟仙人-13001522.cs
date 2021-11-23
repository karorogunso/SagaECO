using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地图:沃特雷亚(11053000)NPC基本信息:13001522-龟仙人- X:53 Y:231
namespace SagaScript.M11053000
{
    public class S13001522 : Event
    {
    public S13001522()
        {
            this.EventID = 13001522;
        }


        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "这里好久没有人来了!$R欢迎你啊!远方的冒险者!$R;");
            Say(pc, 0, "唉!!$R;");
        }
    }
}
